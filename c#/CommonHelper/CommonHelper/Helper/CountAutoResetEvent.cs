using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommonHelper.Helper
{
    /// <summary>
    /// 这是AutoResetEvent类的扩展，当调用Set之后完成了事件，在调用下一次WaitOne方法前如果
    /// 发现有多次调用Set方法后，会先记录调用次数Count，在调用WaitOne时不会阻塞，只会减少Count，
    /// 只有当Count为0时才会真正的阻塞
    /// </summary>
    public class CountAutoResetEvent
    {
        AutoResetEvent _autoResetEvent;
        long _count;
        public CountAutoResetEvent(bool initialState)
        {
            _count = -1;
            _autoResetEvent = new AutoResetEvent(initialState);
        }

        public bool Set()
        {
            Interlocked.Increment(ref _count);
            return _autoResetEvent.Set();
        }

        public bool WaitOne()
        {
            if (Interlocked.Read(ref _count) > 0)
            {
                Interlocked.Decrement(ref _count);
                return true;
            }
            else
            {
                Interlocked.Decrement(ref _count);
                return _autoResetEvent.WaitOne();
            }
        }

        public bool WaitOne(TimeSpan timeout)
        {
            if (Interlocked.Read(ref _count) > 0)
            {
                Interlocked.Decrement(ref _count);
                return true;
            }
            else
            {
                Interlocked.Decrement(ref _count);
                return _autoResetEvent.WaitOne(timeout);
            }
        }

        public bool WaitOne(int millisecondsTimeout)
        {
            if (Interlocked.Read(ref _count) > 0)
            {
                Interlocked.Decrement(ref _count);
                return true;
            }
            else
            {
                Interlocked.Decrement(ref _count);
                return _autoResetEvent.WaitOne(millisecondsTimeout);
            }
        }

    }
}
