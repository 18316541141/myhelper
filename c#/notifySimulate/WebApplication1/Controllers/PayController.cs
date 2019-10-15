using CommonHelper.Helper.CommonEntity;
using CommonWeb.Filter.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Controllers.Common;
using WebApplication1.Entity;
using WebApplication1.Service;

namespace WebApplication1.Controllers
{
    /// <summary>
    /// 模拟微信支付请求，支付成功后会回调。
    /// </summary>
    public class PayController:FastController
    {
        public PayService PayService { set; get; }

        /// <summary>
        /// jsapi版的微信支付模拟。
        /// </summary>
        [AllowAnonymous]
        public JsonResult JsapiPay(GetBrandWCPayRequestParams getBrandWCPayRequestParams)
        {
            return MyJson(new JsapiPayResult
            {
                err_msg = getBrandWCPayRequestParams.paySign == PayService.GetBrandWCPayRequestParamsSign(getBrandWCPayRequestParams, "key")? "get_brand_wcpay_request:ok" : "签名错误。"
            });
        }
    }
}