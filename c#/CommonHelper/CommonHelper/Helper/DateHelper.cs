using CommonHelper.Helper;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace CommonHelper.Helper
{
    /// <summary>
    /// 日期帮助类
    /// </summary>
    public static class DateHelper
    {

        /// <summary>
        /// 今天是否为国家法定工作日的缓存
        /// </summary>
        static bool _isWorkDayCache;

        /// <summary>
        /// 今天日期缓存
        /// </summary>
        static DateTime _workDayDateCache;

        /// <summary>
        /// 检查指定日期是否为国家法定工作日
        /// </summary>
        /// <param name="date">指定日期</param>
        /// <returns>如果是返回true</returns>
        public static bool IsPrcWorkDay(DateTime date)
        {
            lock (typeof(DateHelper))
            {
                if (_workDayDateCache.ToString("yyyyMMdd") == date.ToString("yyyyMMdd"))
                    return _isWorkDayCache;
                else
                    while (true)
                        try
                        {
                            _workDayDateCache = date;
                            JObject retJson = HttpWebRequestHelper.HttpGet($"http://api.goseek.cn/Tools/holiday?date={date.ToString("yyyyMMdd")}").GetJsonObj();
                            return _isWorkDayCache = Convert.ToString(retJson["data"]) == "0";
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            Console.WriteLine("检查法定工作日出错，10秒后重试...");
                            Thread.Sleep(10000);
                        }

            }
        }

        /// <summary>
        /// 检查今天是否国家法定工作日
        /// </summary>
        /// <returns>如果是返回true</returns>
        public static bool TodayIsPrcWorkDay() => IsPrcWorkDay(DateTime.Now);
    }
}
