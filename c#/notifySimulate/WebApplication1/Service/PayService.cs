using CommonHelper.Helper;
using CommonWeb.Filter.Common;
using CommonWeb.Service.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using WebApplication1.Entity;
namespace WebApplication1.Service
{
    /// <summary>
    /// 支付业务类
    /// </summary>
    public class PayService : BaseService
    {

        public void CheckGetBrandWCPayRequestParamsSign(GetBrandWCPayRequestParams getBrandWCPayRequestParams)
        {
            if (getBrandWCPayRequestParams == null)
            {
                JsapiPayRet("参数不能为空！");
            }
            if (string.IsNullOrEmpty(getBrandWCPayRequestParams.nonceStr))
            {
                JsapiPayRet("随机字符串nonceStr不能为空！");
            }
            else
            {
                if (getBrandWCPayRequestParams.nonceStr.Length > 32)
                {
                    JsapiPayRet("随机字符串nonceStr长度不能大于32个字符！");
                }
            }

        }

        /// <summary>
        /// 对jsapi支付的参数进行签名加密
        /// </summary>
        /// <param name="getBrandWCPayRequestParams">jsapi参数</param>
        /// <param name="key">加密用的key</param>
        /// <returns></returns>
        public string GetBrandWCPayRequestParamsSign(GetBrandWCPayRequestParams getBrandWCPayRequestParams, string key)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("appId=").Append(getBrandWCPayRequestParams.appId)
                .Append("&nonceStr=").Append(getBrandWCPayRequestParams.nonceStr)
                .Append("&package=").Append(getBrandWCPayRequestParams.package)
                .Append("&signType=").Append(getBrandWCPayRequestParams.signType)
                .Append("&timeStamp=").Append(getBrandWCPayRequestParams.timeStamp)
                .Append("&key=").Append(key);
            if (getBrandWCPayRequestParams.signType == "MD5")
            {
                return EncryptHelper.GetMD5FromStr(sb.ToString());
            }
            else if (getBrandWCPayRequestParams.signType == "HMAC-SHA256")
            {
                return EncryptHelper.HmacSha256(sb.ToString());
            }
            else
            {
                JsapiPayRet("signType错误，目前只支持MD5或HMAC-SHA256加密。");
                return null;
            }
        }

        /// <summary>
        /// 直接返回json结果，终止后面的代码处理
        /// </summary>
        /// <param name="msg">异常信息</param>
        /// <param name="modularEnum">需指明这是由哪一个模块抛出的异常</param>
        /// <param name="code">错误码</param>
        protected void JsapiPayRet(string err_msg)
        {
            throw new Exception(MyErrorAttribute.ErrorPrefix + JsonConvert.SerializeObject(new JsapiPayResult
            {
                err_msg = err_msg
            }));
        }
    }
}