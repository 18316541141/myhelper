using CommonHelper.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebApplication1.App_Start;
using WebApplication1.Entity;
using WebApplication1.Entity.Common;
using WebApplication1.Service;

namespace WebApplication1.Filter.Common
{
    /// <summary>
    /// 签名校验拦截器
    /// </summary>
    public class SignAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 必须包含的参数
        /// </summary>
        public string[] RequiredKeys { set; get; }

        public SignAttribute(string[] requiredKeys)
        {
            RequiredKeys = requiredKeys;
        }

        /// <summary>
        /// 签名处理规则：
        ///     signChar=sha1(key1=val1&key2=val2&key3=val3...signKey=signKey&signSecret=signSecret).toUpper()
        ///     把其他请求参数按照字典顺序拼接=、&后，拼接：signKey、signSecret，进行sha1加密并转为全大写（不需要转义），得到signChar参数，
        ///     发送请求时要带上signChar，signKey参数。
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            SystemService systemService = DependencyResolver.Current.GetService<SystemService>();
            HttpContextBase httpContextBase = filterContext.HttpContext;
            HttpRequestBase request = httpContextBase.Request;
            NameValueCollection headers = httpContextBase.Request.Headers;
            NameValueCollection queryString = request.QueryString;
            string[] allKeys=queryString.AllKeys;
            foreach (string requiredKey in RequiredKeys)
            {
                if (!allKeys.Contains(requiredKey))
                {
                    HttpResponseBase response = httpContextBase.Response;
                    response.ContentEncoding = Encoding.UTF8;
                    response.ContentType = "application/json;charset=UTF-8";
                    response.StatusCode = 200;
                    response.Write(JsonConvert.SerializeObject(new Result
                    {
                        code = -12,
                        msg = $"签名错误，参数不完整，必须包含{string.Join(",", RequiredKeys)}参数！",
                    }));
                    filterContext.Result = new EmptyResult();
                    return;
                }
            }
            Array.Sort(allKeys);
            StringBuilder sb = new StringBuilder();
            string connChar = "";
            foreach (string key in allKeys)
            {
                if(key!= "signChar" && key != "signKey" && RequiredKeys.Contains(key))
                {
                    sb.Append(connChar).Append(key).Append("=").Append(queryString[key]);
                    connChar = "&";
                }
            }
            string signSecret=systemService.FindSecretByKey(queryString["signKey"]);
            sb.Append("&signKey=").Append(queryString["signKey"]).Append("&signSecret=").Append(signSecret);
            string signChar = EncrypHelper.GetSha1FromStr(sb.ToString()).ToUpper();
            if (signChar == queryString["signChar"])
            {
                base.OnActionExecuting(filterContext);
            }
            else
            {
                HttpResponseBase response = httpContextBase.Response;
                response.ContentEncoding = Encoding.UTF8;
                response.ContentType = "application/json;charset=UTF-8";
                response.StatusCode = 200;
                response.Write(JsonConvert.SerializeObject(new Result
                {
                    code = -12,
                    msg="签名错误，操作失败！",
                }));
                filterContext.Result = new EmptyResult();
            }
        }
    }
}