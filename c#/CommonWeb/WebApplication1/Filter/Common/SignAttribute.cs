using CommonHelper.Helper;
using CommonHelper.Helper.CommonEntity;
using CommonWeb.Intf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
namespace CommonWeb.Filter.Common
{
    /// <summary>
    /// 签名校验拦截器
    /// </summary>
    public sealed class SignAttribute : ActionFilterAttribute
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
            IUserService signFilterService = DependencyResolver.Current.GetService<IUserService>();
            HttpContextBase httpContextBase = filterContext.HttpContext;
            HttpRequestBase request = httpContextBase.Request;
            NameValueCollection headers = httpContextBase.Request.Headers;
            NameValueCollection paramz = request.Params;
			HttpResponseBase response = httpContextBase.Response;
            string[] allKeys=paramz.AllKeys;
            foreach (string requiredKey in RequiredKeys)
            {
                if (!allKeys.Contains(requiredKey))
                {
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
                    sb.Append(connChar).Append(key).Append("=").Append(paramz[key]);
                    connChar = "&";
                }
            }
            string signSecret= signFilterService.FindSecretByKey(paramz["signKey"]);
			if (signSecret == null)
            {
                response.ContentEncoding = Encoding.UTF8;
                response.ContentType = "application/json;charset=UTF-8";
                response.StatusCode = 200;
                response.Write(JsonConvert.SerializeObject(new Result
                {
                    code = -12,
                    msg = "签名错误，signKey或signSecret错误！",
                }));
                filterContext.Result = new EmptyResult();
                return;
            }
            sb.Append("&signKey=").Append(paramz["signKey"]).Append("&signSecret=").Append(signSecret);
            string signChar = EncrypHelper.GetSha1FromStr(sb.ToString()).ToUpper();
            if (signChar == paramz["signChar"].ToUpper())
            {
                base.OnActionExecuting(filterContext);
            }
            else
            {
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