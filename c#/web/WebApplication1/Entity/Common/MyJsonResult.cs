using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace WebApplication1.Entity.Common
{
    /// <summary>
    /// 自定义jsonResult，可以控制日期的格式化，以及是否使用jsonp跨域
    /// </summary>
    public class MyJsonResult:JsonResult
    {
        static MyJsonResult()
        {
            JsonSerializerSettings setting = new JsonSerializerSettings();
            JsonConvert.DefaultSettings = new Func<JsonSerializerSettings>(() =>
            {
                //日期类型默认格式化处理
                setting.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;
                setting.DateFormatString = "yyyy-MM-dd HH:mm:ss";

                //空值处理
                setting.NullValueHandling = NullValueHandling.Ignore;

                return setting;
            });
        }

        /// <summary>
        /// 格式化字符串
        /// </summary>
        public string FormateStr { get; set; }

        /// <summary>
        /// 回调函数名称，当该属性不为空时，则认为是跨域请求
        /// </summary>
        public string Callback { get; set; }

        /// <summary>
        /// 重写执行视图
        /// </summary>
        /// <param name="context">上下文</param>
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            HttpResponseBase response = context.HttpContext.Response;

            if (string.IsNullOrEmpty(Callback))
            {
                if (string.IsNullOrEmpty(this.ContentType))
                {
                    response.ContentType = this.ContentType;
                }
                else
                {
                    response.ContentType = "application/json";
                }
            }
            else
            {
                response.ContentType = "application/javascript";
            }

            if (this.ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }

            if (Data != null)
            {
                string jsonString = JsonConvert.SerializeObject(Data, new IsoDateTimeConverter
                {
                    DateTimeFormat = FormateStr
                });
                if (string.IsNullOrEmpty(Callback))
                {
                    response.Write(jsonString);
                }
                else
                {
                    response.Write($"{Callback}({jsonString})");
                }
            }
        }
    }
}