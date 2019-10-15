using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Entity
{
    /// <summary>
    /// jsapi支付的返回值
    /// </summary>
    public class JsapiPayResult
    {
        /// <summary>
        /// 返回结果
        /// get_brand_wcpay_request:ok	支付成功
        /// get_brand_wcpay_request:fail 支付失败
        /// get_brand_wcpay_request:cancel 支付过程中用户取消
        /// </summary>
        public string err_msg { set; get; }
    }
}