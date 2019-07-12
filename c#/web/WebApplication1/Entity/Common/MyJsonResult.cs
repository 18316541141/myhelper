﻿using System;
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
                response.ContentEncoding = this.ContentEncoding;
            }

            if (this.Data != null)
            {
                JavaScriptSerializer jss = new JavaScriptSerializer();
                string jsonString = jss.Serialize(Data);
                string p = @"\\/Date\((\d+)\)\\/";
                MatchEvaluator matchEvaluator = new MatchEvaluator(this.ConvertJsonDateToDateString);
                Regex reg = new Regex(p);
                jsonString = reg.Replace(jsonString, matchEvaluator);
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

        /// <summary> 
        /// 将Json序列化的时间由/Date(1294499956278)转为字符串 .
        /// </summary> 
        /// <param name="m">正则匹配</param>
        /// <returns>格式化后的字符串</returns>
        private string ConvertJsonDateToDateString(Match m)
        {
            string result = string.Empty;
            DateTime dt = new DateTime(1970, 1, 1);
            dt = dt.AddMilliseconds(long.Parse(m.Groups[1].Value));
            dt = dt.ToLocalTime();
            result = dt.ToString(FormateStr);
            return result;
        }
    }
}