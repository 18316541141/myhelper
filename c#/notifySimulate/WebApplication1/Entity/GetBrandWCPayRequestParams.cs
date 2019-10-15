using CommonHelper.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
namespace WebApplication1.Entity
{
    /// <summary>
    /// jsapi支付模拟接口的请求参数
    /// </summary>
    public class GetBrandWCPayRequestParams
    {
        /// <summary>
        /// 公众号名称，由商户传入
        /// </summary>
        public string appId { set; get; }

        /// <summary>
        /// 时间戳，自1970年以来的秒数
        /// </summary>
        public string timeStamp { set; get; }

        /// <summary>
        /// 随机串
        /// </summary>
        public string nonceStr { set; get; }

        /// <summary>
        /// 统一下单接口返回的prepay_id参数值，提交格式如
        /// </summary>
        public string package { set; get; }

        /// <summary>
        /// 签名类型，默认为MD5，支持HMAC-SHA256和MD5。注意此处需与统一下单的签名类型一致
        /// </summary>
        public string signType { set; get; }

        /// <summary>
        /// 签名
        /// </summary>
        public string paySign { set; get; }
    }
}