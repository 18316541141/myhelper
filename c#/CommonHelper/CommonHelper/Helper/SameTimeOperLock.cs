using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommonHelper.Helper
{
    /// <summary>
    /// 同时操作锁，限制最多同时有多少个线程进行操作
    /// </summary>
    public class SameTimeOperLock
    {
        /// <summary>
        /// 最大同时操作的线程数
        /// </summary>
        int _MaxThreadCount { set; get; }

        /// <summary>
        /// 当前执行中的线程数
        /// </summary>
        int _ExecuteCount;

        /// <summary>
        /// 等待池名称
        /// </summary>
        string _PoolName { set; get; }

        /// <summary>
        /// 构造函数，传入最大同时操作的线程数
        /// </summary>
        /// <param name="maxThreadCount">最大同时操作的线程数</param>
        public SameTimeOperLock(int maxThreadCount)
        {
            _MaxThreadCount = maxThreadCount;
            _ExecuteCount = 0;
            _PoolName = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// 当达到最大运行线程数时，则等待，如果没有达到时通过。
        /// </summary>
        public void LockOnMaxThread()
        {
            Interlocked.Increment(ref _ExecuteCount);
            while (_ExecuteCount > _MaxThreadCount)
            {
                ThreadHelper.BatchWait(_PoolName);
            }
        }

        /// <summary>
        /// 释放一个线程。
        /// </summary>
        public void UnLock()
        {
            Interlocked.Decrement(ref _ExecuteCount);
            ThreadHelper.BatchSet(_PoolName);
        }
    }
}
