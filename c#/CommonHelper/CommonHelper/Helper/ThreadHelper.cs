using CommonHelper.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace CommonHelper.Helper
{
    /// <summary>
    /// 线程帮助类
    /// </summary>
    public static class ThreadHelper
    {

        /// <summary>
        /// 冷却时间类型
        /// </summary>
        public enum CooldownType
        {
            /// <summary>
            /// 循环冷却，循环的执行冷却时间，在冷却时间内禁止操作，冷却时间外允许操作
            /// </summary>
            LOOP,
            /// <summary>
            /// 顺序冷却，按顺序执行冷却时间，在冷却时间内禁止操作，冷却时间外允许操作，当冷却顺序执行到
            /// 最后则永久禁止操作
            /// </summary>
            ORDER,
        }

        /// <summary>
        /// 冷却时间处理
        /// </summary>
        public sealed class CooldownTime
        {
            /// <summary>
            /// 冷却时间类型
            /// </summary>
            private CooldownType _cooldownType { set; get; }

            /// <summary>
            /// 冷却时间间隔列表
            /// </summary>
            private long[] _millisecondSpans { set; get; }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="coolType">冷却类型</param>
            /// <param name="millisecondSpans">毫秒冷却时间间隔</param>
            public CooldownTime(CooldownType cooldownType, params long[] millisecondSpans)
            {
                _cooldownType = cooldownType;
                _millisecondSpans = millisecondSpans;
            }

            /// <summary>
            /// 冷却结束时间
            /// </summary>
            private DateTime _last { set; get; }

            /// <summary>
            /// 最近一次使用的冷却时间下标
            /// </summary>
            private uint _index { set; get; }

            /// <summary>
            /// 是否在冷却时间之外
            /// </summary>
            /// <returns>在冷却时间之外返回false</returns>
            public bool OutCooldownTime()
            {
                lock (this)
                {
                    long millisecondSpan = _millisecondSpans[_index % _millisecondSpans.Length];
                    if (_last < DateTime.Now)
                    {
                        _index++;
                        if (_cooldownType == CooldownType.ORDER && _index >= _millisecondSpans.Length)
                        {
                            _last = DateTime.MaxValue;
                        }
                        else
                        {
                            _last = DateTime.Now.AddMilliseconds(millisecondSpan);
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// 睡眠很长的一段时间
        /// </summary>
        /// <param name="milliseconds"></param>
        public static void Sleep(long milliseconds)
        {
            long Rem = milliseconds % int.MaxValue;
            long Times = (milliseconds - Rem) / int.MaxValue;
            for (long j = 0; j < Times; j++)
                Thread.Sleep(int.MaxValue);
            Thread.Sleep(Convert.ToInt32(Rem.ToString()));
        }

        /// <summary>
        /// 等待池表
        /// </summary>
        static Dictionary<string, List<AutoResetEvent>> _waitMap;
        static Dictionary<string, string> _controllerVersionMap;
        static Dictionary<string, UserEditLockVal> _editLockMap;

        /// <summary>
        /// 事件队列，避免重复创建事件
        /// </summary>
        static Queue<AutoResetEvent> _autoResetEventQueue;

        static ThreadHelper()
        {
            _waitMap = new Dictionary<string, List<AutoResetEvent>>();
            _controllerVersionMap = new Dictionary<string, string>();
            _editLockMap = new Dictionary<string, UserEditLockVal>();
            _autoResetEventQueue = new Queue<AutoResetEvent>();
            _RandomIndex = new Random();
        }


        /// <summary>
        /// 比较控制器的版本号，如果相同的则返回true，否则返回false，并返回最新版本号
        /// </summary>
        /// <param name="controllerName">控制器名称</param>
        /// <param name="userVersion">用户的版本号</param>
        /// <param name="newestVersion">这是一个返回值，不论版本号有没有变化，都返回最新版本号</param>
        public static bool CompareControllerVersion(string controllerName, string userVersion, out string newestVersion)
        {
            controllerName = controllerName + "Version";
            lock (_controllerVersionMap)
            {
                try
                {
                    if (_controllerVersionMap.ContainsKey(controllerName))
                    {
                        newestVersion = _controllerVersionMap[controllerName];
                        return _controllerVersionMap[controllerName] == userVersion;
                    }
                    else
                    {
                        _controllerVersionMap.Add(controllerName, newestVersion = Guid.NewGuid().ToString());
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 当修改数据时，如果涉及到控制器版本号更改，则使用该方法修改
        /// </summary>
        /// <param name="controllerName">控制器名称</param>
        /// <param name="editCallback">修改回调函数</param>
        /// <returns></returns>
        public static string EditControllerVersion(string controllerName)
        {
            controllerName = controllerName + "Version";
            lock (_controllerVersionMap)
            {
                string newestVersion = Guid.NewGuid().ToString();
                if (_controllerVersionMap.ContainsKey(controllerName))
                    _controllerVersionMap[controllerName] = newestVersion;
                else
                    _controllerVersionMap.Add(controllerName, newestVersion);
                return newestVersion;
            }
        }

        /// <summary>
        /// 批量等待，可以多次调用，并且可以被BatchSet统一唤醒
        /// </summary>
        /// <param name="controllerName">等待的控制器名称</param>
        /// <param name="waitSeconds">等待秒数</param>
        public static void BatchWait(string controllerName, int millisecondsTimeout = -1)
        {
            controllerName = controllerName + "Wait";
            AutoResetEvent autoResetEvent;
            lock (_waitMap)
            {
                if (!_waitMap.ContainsKey(controllerName))
                {
                    lock (_waitMap)
                    {
                        _waitMap.Add(controllerName, new List<AutoResetEvent>());
                    }
                }
                if (_autoResetEventQueue.Count == 0)
                {
                    autoResetEvent = new AutoResetEvent(false);
                }
                else
                {
                    autoResetEvent = _autoResetEventQueue.Dequeue();
                }
                _waitMap[controllerName].Add(autoResetEvent);
            }
            bool ret = autoResetEvent.WaitOne(millisecondsTimeout);
            if (!ret && _waitMap.ContainsKey(controllerName))
            {
                lock (_waitMap)
                {
                    if (_waitMap.ContainsKey(controllerName))
                    {
                        _waitMap[controllerName].Remove(autoResetEvent);
                    }
                    if (_autoResetEventQueue.Count < 1000)
                    {
                        _autoResetEventQueue.Enqueue(autoResetEvent);
                    }
                    else
                    {
                        autoResetEvent.Dispose();
                    }
                }
            }
        }

        /// <summary>
        /// 随机下标
        /// </summary>
        static Random _RandomIndex { set; get; }

        /// <summary>
        /// 随机唤醒一个线程
        /// </summary>
        /// <param name="controllerName"></param>
        public static void RandomSetOne(string controllerName)
        {
            controllerName = controllerName + "Wait";
            if (_waitMap.ContainsKey(controllerName))
            {
                lock (_waitMap)
                {
                    if (_waitMap.ContainsKey(controllerName))
                    {
                        List<AutoResetEvent> autoResetEventList = _waitMap[controllerName];
                        int removeIndex = _RandomIndex.Next(0, autoResetEventList.Count - 1);
                        AutoResetEvent autoResetEvent = autoResetEventList[removeIndex];
                        autoResetEvent.Set();
                        autoResetEventList.RemoveAt(removeIndex);
                        if (_autoResetEventQueue.Count < 1000)
                        {
                            _autoResetEventQueue.Enqueue(autoResetEvent);
                        }
                        else
                        {
                            autoResetEvent.Dispose();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 批量唤醒
        /// </summary>
        /// <param name="controllerName">等待的控制器名称</param>
        public static void BatchSet(string controllerName)
        {
            controllerName = controllerName + "Wait";
            if (_waitMap.ContainsKey(controllerName))
            {
                lock (_waitMap)
                {
                    if (_waitMap.ContainsKey(controllerName))
                    {
                        List<AutoResetEvent> autoResetEventList = _waitMap[controllerName];
                        foreach (AutoResetEvent autoResetEvent in autoResetEventList)
                        {
                            autoResetEvent.Set();
                            if (_autoResetEventQueue.Count < 1000)
                            {
                                _autoResetEventQueue.Enqueue(autoResetEvent);
                            }
                            else
                            {
                                autoResetEvent.Dispose();
                            }
                        }
                        _waitMap.Remove(controllerName);
                    }
                }
            }
        }

        /// <summary>
        /// 延长修改锁定时间
        /// </summary>
        /// <param name="key">延长的key值</param>
        /// <param name="username">延长的用户</param>
        public static void AddEditLockLimitDate(string key, string username)
        {
            if (_editLockMap.ContainsKey(key))
            {
                lock (_editLockMap)
                {
                    if (_editLockMap.ContainsKey(key) && _editLockMap[key]?.Username == username)
                        _editLockMap[key].LimitDate.AddSeconds(5);
                }
            }
        }

        /// <summary>
        /// 对于多个用户可同时修改的界面需要使用锁。
        /// </summary>
        /// <param name="key">数据主键</param>
        /// <param name="editUsername">想要修改该数据的用户</param>
        /// <param name="username">输出占用的用户名</param>
        public static bool UserEditLock(string key, string editUsername, out string username)
        {
            UserEditLockVal val = new UserEditLockVal { Username = editUsername, LimitDate = DateTime.Now.AddSeconds(10) };
            lock (_editLockMap)
            {
                EnumerableHelper.DelEleFromMap(_editLockMap, (value) => value.LimitDate < DateTime.Now);
                if (_editLockMap.ContainsKey(key))
                {
                    if (_editLockMap[key].Username != editUsername && _editLockMap[key].LimitDate >= DateTime.Now)
                    {
                        username = _editLockMap[key].Username;
                        return false;
                    }
                    else
                    {
                        username = null;
                        _editLockMap[key] = val;
                        return true;
                    }
                }
                else
                {
                    username = null;
                    _editLockMap.Add(key, val);
                    return true;
                }
            }
        }

        /// <summary>
        /// 多用户修改同一个界面时的锁对象
        /// </summary>
        private class UserEditLockVal
        {
            public string Username { set; get; }

            public DateTime LimitDate { set; get; }
        }
    }
}