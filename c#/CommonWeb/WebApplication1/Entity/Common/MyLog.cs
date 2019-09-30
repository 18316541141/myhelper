using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net.Core;
using CommonHelper.CommonEntity;
using Snowflake.Net;
using System.Reflection;
using System.Diagnostics;
using CommonWeb.Repository;

namespace CommonWeb.Entity.Common
{
    /// <summary>
    /// 自定义的log日志代理类，用法和原本的ILog一致，但容易扩展
    /// </summary>
    public sealed class MyLog : ILog
    {
        ILog Log { set; get; }

        public IdWorker IdWorker { set; get; }

        public LogEntityRepository Repository { set; get; }

        Assembly Assembly { set; get; }

        public MyLog()
        {
            Assembly = Assembly.GetExecutingAssembly();
            Log=LogManager.GetLogger("Log4net.config");
            LastClearDate = DateTime.MinValue;
        }

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
            OtherProps(new StackTrace().GetFrame(1).GetMethod());
            Log.Error(message);
        }

        public void Error(object message, Exception exception)
        {
            OtherProps(exception.TargetSite);
            Log.Error(message, exception);
        }

        public void ErrorFormat(string format, object arg0)
        {
            OtherProps(new StackTrace().GetFrame(1).GetMethod());
            Log.ErrorFormat(format, arg0);
        }

        public void ErrorFormat(string format, params object[] args)
        {
            OtherProps(new StackTrace().GetFrame(1).GetMethod());
            Log.ErrorFormat(format, args);
        }

        public void ErrorFormat(IFormatProvider provider, string format, params object[] args)
        {
            OtherProps(new StackTrace().GetFrame(1).GetMethod());
            Log.ErrorFormat(provider, format, args);
        }

        public void ErrorFormat(string format, object arg0, object arg1)
        {
            OtherProps(new StackTrace().GetFrame(1).GetMethod());
            Log.ErrorFormat(format, arg0, arg1);
        }

        public void ErrorFormat(string format, object arg0, object arg1, object arg2)
        {
            OtherProps(new StackTrace().GetFrame(1).GetMethod());
            Log.ErrorFormat(format, arg0, arg1, arg2);
        }

        public void Fatal(object message)
        {
            OtherProps(new StackTrace().GetFrame(1).GetMethod());
            Log.Fatal(message);
        }

        public void Fatal(object message, Exception exception)
        {
            OtherProps(exception.TargetSite);
            Log.Fatal(message, exception);
        }

        public void FatalFormat(string format, object arg0)
        {
            OtherProps(new StackTrace().GetFrame(1).GetMethod());
            Log.FatalFormat(format, arg0);
        }

        public void FatalFormat(string format, params object[] args)
        {
            OtherProps(new StackTrace().GetFrame(1).GetMethod());
            Log.FatalFormat(format, args);
        }

        public void FatalFormat(IFormatProvider provider, string format, params object[] args)
        {
            OtherProps(new StackTrace().GetFrame(1).GetMethod());
            Log.FatalFormat(provider, format, args);
        }

        public void FatalFormat(string format, object arg0, object arg1)
        {
            OtherProps(new StackTrace().GetFrame(1).GetMethod());
            Log.FatalFormat(format, arg0, arg1);
        }

        public void FatalFormat(string format, object arg0, object arg1, object arg2)
        {
            OtherProps(new StackTrace().GetFrame(1).GetMethod());
            Log.FatalFormat(format, arg0, arg1, arg2);
        }

        public void Info(object message)
        {
            OtherProps(new StackTrace().GetFrame(1).GetMethod());
            Log.Info(message);
        }

        public void Info(object message, Exception exception)
        {
            OtherProps(exception.TargetSite);
            Log.Info(message, exception);
        }

        public void InfoFormat(string format, object arg0)
        {
            OtherProps(new StackTrace().GetFrame(1).GetMethod());
            Log.InfoFormat(format, arg0);
        }

        public void InfoFormat(string format, params object[] args)
        {
            OtherProps(new StackTrace().GetFrame(1).GetMethod());
            Log.InfoFormat(format, args);
        }

        public void InfoFormat(IFormatProvider provider, string format, params object[] args)
        {
            OtherProps(new StackTrace().GetFrame(1).GetMethod());
            Log.InfoFormat(provider, format,args);
        }

        public void InfoFormat(string format, object arg0, object arg1)
        {
            OtherProps(new StackTrace().GetFrame(1).GetMethod());
            Log.InfoFormat(format, arg0, arg1);
        }

        public void InfoFormat(string format, object arg0, object arg1, object arg2)
        {
            OtherProps(new StackTrace().GetFrame(1).GetMethod());
            Log.InfoFormat(format, arg0, arg1, arg2);
        }

        public void Warn(object message)
        {
            OtherProps(new StackTrace().GetFrame(1).GetMethod());
            Log.Warn(message);
        }

        public void Warn(object message, Exception exception)
        {
            OtherProps(exception.TargetSite);
            Log.Warn(message, exception);
        }

        public void WarnFormat(string format, object arg0)
        {
            OtherProps(new StackTrace().GetFrame(1).GetMethod());
            Log.WarnFormat(format, arg0);
        }

        public void WarnFormat(string format, params object[] args)
        {
            OtherProps(new StackTrace().GetFrame(1).GetMethod());
            Log.WarnFormat(format, args);
        }

        public void WarnFormat(IFormatProvider provider, string format, params object[] args)
        {
            OtherProps(new StackTrace().GetFrame(1).GetMethod());
            Log.WarnFormat(provider, format, args);
        }

        public void WarnFormat(string format, object arg0, object arg1)
        {
            OtherProps(new StackTrace().GetFrame(1).GetMethod());
            Log.WarnFormat(format, arg0, arg1);
        }

        public void WarnFormat(string format, object arg0, object arg1, object arg2)
        {
            OtherProps(new StackTrace().GetFrame(1).GetMethod());
            Log.WarnFormat(format, arg0, arg1, arg2);
        }

        /// <summary>
        /// 最近一次的日志清理日期，以实现，
        /// </summary>
        DateTime LastClearDate;

        void OtherProps(MethodBase methodBase)
        {
            LogicalThreadContext.Properties["Id"] = IdWorker.NextId();
            LogicalThreadContext.Properties["Username"] = HttpContext.Current?.User.Identity.Name;
            LogicalThreadContext.Properties["ProjectName"] = Assembly.GetName().Name;
            LogicalThreadContext.Properties["TypeName"] = methodBase.DeclaringType.FullName;
            LogicalThreadContext.Properties["FuncName"] = methodBase.Name;
            if ((DateTime.Now - LastClearDate).TotalDays > 1)
            {
                lock (typeof(MyLog))
                {
                    if ((DateTime.Now - LastClearDate).TotalDays > 1)
                    {
                        DateTime prevYear = DateTime.Now.AddYears(-1);
                        Repository.Delete(a => a.CreateDate <= prevYear);
                        LastClearDate = DateTime.Now;
                    }
                }
            }
        }
    }
}