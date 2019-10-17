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
        /// 等待到指定时间，如果没到指定时间则阻塞，直到等到指定时间；
        /// 如果超过了指定时间，则直接往下执行。
        /// </summary>
        /// <param name="datetime">指定时间</param>
        /// <param name="waitToDateTimeHandle">
        ///     当使用WaitToDateTime时，可能需要做一些周期性的操作，例如：读秒、发送心跳包...
        ///     该委托就是可以在使用WaitToDateTime时执行这些操作。注意：waitToDateTimeHandle该
        ///     委托是不间断调用，需要在内部指定睡眠时间。
        /// </param>
        public static void WaitToDateTime(DateTime datetime, ThreadStart waitToDateTimeHandle = null)
        {
            double totalMs =(datetime - DateTime.Now).TotalMilliseconds;
            if (totalMs>0)
            {
                CancellationTokenSource cts = new CancellationTokenSource();
                if (waitToDateTimeHandle != null)
                {
                    new Thread(() => 
                    {
                        while (cts.IsCancellationRequested)
                        {
                            waitToDateTimeHandle();
                        }
                    }).Start();
                }
                ThreadHelper.Sleep(Convert.ToInt64(totalMs));
                cts.Cancel();
            }
        }

        /// <summary>
        /// 检查今天是否国家法定工作日
        /// </summary>
        /// <returns>如果是返回true</returns>
        public static bool TodayIsPrcWorkDay() => IsPrcWorkDay(DateTime.Now);
    }
}
