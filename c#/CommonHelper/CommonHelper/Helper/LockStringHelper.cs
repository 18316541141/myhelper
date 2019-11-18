using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
namespace CommonHelper.Helper
{
    /// <summary>
    /// 字符串锁帮助类
    /// </summary>
    public static class LockStringHelper
    {
        /// <summary>
        /// 存放锁的表
        /// </summary>
        static Dictionary<string, string> _LockMap { set; get; }

        /// <summary>
        /// 最近一次清理时间
        /// </summary>
        static DateTime _LastClearDateTime { set; get; }

        static LockStringHelper()
        {
            _LockMap = new Dictionary<string, string>();
            _LastClearDateTime = DateTime.Now;
        }

        /// <summary>
        /// 根据字符串分配一个锁对象，使得该锁对象对同字符串锁定，不同
        /// 字符串则通过。
        /// </summary>
        /// <param name="lockString">锁字符串</param>
        /// <param name="clearBeforeMinutes">手动清理x分钟前的空闲锁对象，默认是1分钟前</param>
        /// <returns></returns>
        public static object DistributionLock(string lockString,int clearBeforeMinutes = 1)
        {
            object temp;
            lock (_LockMap)
            {
                if ((DateTime.Now - _LastClearDateTime).TotalMinutes > clearBeforeMinutes)
                {
                    List<string> keys =_LockMap.Keys.ToList();
                    foreach (string key in keys)
                    {
                        temp = _LockMap[key];
                        if (Monitor.TryEnter(temp))
                        {
                            _LockMap.Remove(key);
                            Monitor.Exit(temp);
                        }
                    }
                }
                if (_LockMap.ContainsKey(lockString))
                {
                    return _LockMap[lockString];
                }
                else
                {
                    _LockMap.Add(lockString, lockString);
                    return lockString;
                }
            }
        }
    }
}
