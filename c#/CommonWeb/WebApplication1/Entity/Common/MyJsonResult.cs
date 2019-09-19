using CommonHelper.Helper.CommonJsonConverter;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace CommonWeb.Entity.Common
{
    /// <summary>
    /// 自定义jsonResult，可以控制日期的格式化，以及是否使用jsonp跨域
    /// </summary>
    public sealed class MyJsonResult:JsonResult
    {
        /// <summary>
        /// long类型数据转字符串
        /// </summary>
        public static LongConverter LongConverter;

        /// <summary>
        /// 日期类型数据转换器
        /// </summary>
        public static Dictionary<string, IsoDateTimeConverter> IsoDateTimeConverterMap;

        static MyJsonResult()
        {
            LongConverter = new LongConverter();
            IsoDateTimeConverterMap = new Dictionary<string, IsoDateTimeConverter>();
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
                if (!IsoDateTimeConverterMap.ContainsKey(FormateStr))
                {
                    lock (IsoDateTimeConverterMap)
                    {
                        if (!IsoDateTimeConverterMap.ContainsKey(FormateStr))
                        {
                            IsoDateTimeConverterMap.Add(FormateStr, new IsoDateTimeConverter
                            {
                                DateTimeFormat = FormateStr
                            });
                        }
                    }
                }
                string jsonString = JsonConvert.SerializeObject(Data, IsoDateTimeConverterMap[FormateStr], LongConverter);
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