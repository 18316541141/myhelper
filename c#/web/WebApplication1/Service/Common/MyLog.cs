using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net.Core;
using CommonHelper.CommonEntity;
using Snowflake.Net;
using System.Reflection;

namespace WebApplication1.Service.Common
{
    /// <summary>
    /// 自定义的log日志代理类，用法和原本的ILog一致，但容易扩展
    /// </summary>
    public class MyLog : ILog
    {
        public ILog Log { set; get; }

        public IdWorker IdWorker { set; get; }

        public bool IsDebugEnabled
        {
            get
            {
                return Log.IsDebugEnabled;
            }
        }

        public bool IsErrorEnabled
        {
            get
            {
                return Log.IsErrorEnabled;
            }
        }

        public bool IsFatalEnabled
        {
            get
            {
                return Log.IsFatalEnabled;
            }
        }

        public bool IsInfoEnabled
        {
            get
            {
                return Log.IsInfoEnabled;
            }
        }

        public bool IsWarnEnabled
        {
            get
            {
                return Log.IsWarnEnabled;
            }
        }

        public ILogger Logger
        {
            get
            {
                return Log.Logger;
            }
        }

        public void Debug(object message)
        {
            Log.Debug(message);
        }

        public void Debug(object message, Exception exception)
        {
            Log.Debug(message, exception);
        }

        public void DebugFormat(string format, object arg0)
        {
            Log.DebugFormat(format,arg0);
        }

        public void DebugFormat(string format, params object[] args)
        {
            Log.DebugFormat(format, args);
        }

        public void DebugFormat(IFormatProvider provider, string format, params object[] args)
        {
            Log.DebugFormat(provider, format, args);
        }

        public void DebugFormat(string format, object arg0, object arg1)
        {
            Log.DebugFormat(format, arg0, arg1);
        }

        public void DebugFormat(string format, object arg0, object arg1, object arg2)
        {
            Log.DebugFormat(format, arg0, arg1, arg2);
        }

        public void Error(object message)
        {
            Log.Error(message);
        }

        public void Error(object message, Exception exception)
        {

            MethodBase methodBase = MethodBase.GetCurrentMethod();
            LogicalThreadContext.Properties["Id"] = IdWorker.NextId();
            LogicalThreadContext.Properties["Namespace"] = Assembly.GetExecutingAssembly().FullName;
            LogicalThreadContext.Properties["TypeName"] = methodBase.DeclaringType.FullName;
            LogicalThreadContext.Properties["MethodName"] = methodBase.DeclaringType.Name;
            Log.Error(message, exception);
        }

        public void ErrorFormat(string format, object arg0)
        {
            Log.ErrorFormat(format, arg0);
        }

        public void ErrorFormat(string format, params object[] args)
        {
            Log.ErrorFormat(format, args);
        }

        public void ErrorFormat(IFormatProvider provider, string format, params object[] args)
        {
            Log.ErrorFormat(provider, format, args);
        }

        public void ErrorFormat(string format, object arg0, object arg1)
        {
            Log.ErrorFormat(format, arg0, arg1);
        }

        public void ErrorFormat(string format, object arg0, object arg1, object arg2)
        {
            Log.ErrorFormat(format, arg0, arg1, arg2);
        }

        public void Fatal(object message)
        {
            Log.Fatal(message);
        }

        public void Fatal(object message, Exception exception)
        {
            Log.Fatal(message, exception);
        }

        public void FatalFormat(string format, object arg0)
        {
            Log.FatalFormat(format, arg0);
        }

        public void FatalFormat(string format, params object[] args)
        {
            Log.FatalFormat(format, args);
        }

        public void FatalFormat(IFormatProvider provider, string format, params object[] args)
        {
            Log.FatalFormat(provider, format, args);
        }

        public void FatalFormat(string format, object arg0, object arg1)
        {
            Log.FatalFormat(format, arg0, arg1);
        }

        public void FatalFormat(string format, object arg0, object arg1, object arg2)
        {
            Log.FatalFormat(format, arg0, arg1, arg2);
        }

        public void Info(object message)
        {
            Log.Info(message);
        }

        public void Info(object message, Exception exception)
        {
            Log.Info(message, exception);
        }

        public void InfoFormat(string format, object arg0)
        {
            Log.InfoFormat(format, arg0);
        }

        public void InfoFormat(string format, params object[] args)
        {
            Log.InfoFormat(format, args);
        }

        public void InfoFormat(IFormatProvider provider, string format, params object[] args)
        {
            Log.InfoFormat(provider, format,args);
        }

        public void InfoFormat(string format, object arg0, object arg1)
        {
            Log.InfoFormat(format, arg0, arg1);
        }

        public void InfoFormat(string format, object arg0, object arg1, object arg2)
        {
            Log.InfoFormat(format, arg0, arg1, arg2);
        }

        public void Warn(object message)
        {
            Log.Warn(message);
        }

        public void Warn(object message, Exception exception)
        {
            Log.Warn(message, exception);
        }

        public void WarnFormat(string format, object arg0)
        {
            Log.WarnFormat(format, arg0);
        }

        public void WarnFormat(string format, params object[] args)
        {
            Log.WarnFormat(format, args);
        }

        public void WarnFormat(IFormatProvider provider, string format, params object[] args)
        {
            Log.WarnFormat(provider, format, args);
        }

        public void WarnFormat(string format, object arg0, object arg1)
        {
            Log.WarnFormat(format, arg0, arg1);
        }

        public void WarnFormat(string format, object arg0, object arg1, object arg2)
        {
            Log.WarnFormat(format, arg0, arg1, arg2);
        }
    }
}