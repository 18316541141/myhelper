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
            MethodBase methodBase = new StackTrace().GetFrame(1).GetMethod();
            LogicalThreadContext.Properties["Id"] = IdWorker.NextId();
            LogicalThreadContext.Properties["Namespace"] = Assembly.GetExecutingAssembly().FullName;
            LogicalThreadContext.Properties["TypeName"] = methodBase.DeclaringType.FullName;
            LogicalThreadContext.Properties["MethodName"] = methodBase.DeclaringType.Name;
            Log.Error(message);
        }

        public void Error(object message, Exception exception)
        {
            MethodBase methodBase = exception.TargetSite;
            LogicalThreadContext.Properties["Id"] = IdWorker.NextId();
            LogicalThreadContext.Properties["Namespace"] = Assembly.GetExecutingAssembly().FullName;
            LogicalThreadContext.Properties["TypeName"] = methodBase.DeclaringType.FullName;
            LogicalThreadContext.Properties["MethodName"] = methodBase.DeclaringType.Name;
            Log.Error(message, exception);
        }

        public void ErrorFormat(string format, object arg0)
        {
            MethodBase methodBase = new StackTrace().GetFrame(1).GetMethod();
            LogicalThreadContext.Properties["Id"] = IdWorker.NextId();
            LogicalThreadContext.Properties["Namespace"] = Assembly.GetExecutingAssembly().FullName;
            LogicalThreadContext.Properties["TypeName"] = methodBase.DeclaringType.FullName;
            LogicalThreadContext.Properties["MethodName"] = methodBase.DeclaringType.Name;
            Log.ErrorFormat(format, arg0);
        }

        public void ErrorFormat(string format, params object[] args)
        {
            MethodBase methodBase = new StackTrace().GetFrame(1).GetMethod();
            LogicalThreadContext.Properties["Id"] = IdWorker.NextId();
            LogicalThreadContext.Properties["Namespace"] = Assembly.GetExecutingAssembly().FullName;
            LogicalThreadContext.Properties["TypeName"] = methodBase.DeclaringType.FullName;
            LogicalThreadContext.Properties["MethodName"] = methodBase.DeclaringType.Name;
            Log.ErrorFormat(format, args);
        }

        public void ErrorFormat(IFormatProvider provider, string format, params object[] args)
        {
            MethodBase methodBase = new StackTrace().GetFrame(1).GetMethod();
            LogicalThreadContext.Properties["Id"] = IdWorker.NextId();
            LogicalThreadContext.Properties["Namespace"] = Assembly.GetExecutingAssembly().FullName;
            LogicalThreadContext.Properties["TypeName"] = methodBase.DeclaringType.FullName;
            LogicalThreadContext.Properties["MethodName"] = methodBase.DeclaringType.Name;
            Log.ErrorFormat(provider, format, args);
        }

        public void ErrorFormat(string format, object arg0, object arg1)
        {
            MethodBase methodBase = new StackTrace().GetFrame(1).GetMethod();
            LogicalThreadContext.Properties["Id"] = IdWorker.NextId();
            LogicalThreadContext.Properties["Namespace"] = Assembly.GetExecutingAssembly().FullName;
            LogicalThreadContext.Properties["TypeName"] = methodBase.DeclaringType.FullName;
            LogicalThreadContext.Properties["MethodName"] = methodBase.DeclaringType.Name;
            Log.ErrorFormat(format, arg0, arg1);
        }

        public void ErrorFormat(string format, object arg0, object arg1, object arg2)
        {
            MethodBase methodBase = new StackTrace().GetFrame(1).GetMethod();
            LogicalThreadContext.Properties["Id"] = IdWorker.NextId();
            LogicalThreadContext.Properties["Namespace"] = Assembly.GetExecutingAssembly().FullName;
            LogicalThreadContext.Properties["TypeName"] = methodBase.DeclaringType.FullName;
            LogicalThreadContext.Properties["MethodName"] = methodBase.DeclaringType.Name;
            Log.ErrorFormat(format, arg0, arg1, arg2);
        }

        public void Fatal(object message)
        {
            MethodBase methodBase = new StackTrace().GetFrame(1).GetMethod();
            LogicalThreadContext.Properties["Id"] = IdWorker.NextId();
            LogicalThreadContext.Properties["Namespace"] = Assembly.GetExecutingAssembly().FullName;
            LogicalThreadContext.Properties["TypeName"] = methodBase.DeclaringType.FullName;
            LogicalThreadContext.Properties["MethodName"] = methodBase.DeclaringType.Name;
            Log.Fatal(message);
        }

        public void Fatal(object message, Exception exception)
        {
            MethodBase methodBase = exception.TargetSite;
            LogicalThreadContext.Properties["Id"] = IdWorker.NextId();
            LogicalThreadContext.Properties["Namespace"] = Assembly.GetExecutingAssembly().FullName;
            LogicalThreadContext.Properties["TypeName"] = methodBase.DeclaringType.FullName;
            LogicalThreadContext.Properties["MethodName"] = methodBase.DeclaringType.Name;
            Log.Fatal(message, exception);
        }

        public void FatalFormat(string format, object arg0)
        {
            MethodBase methodBase = new StackTrace().GetFrame(1).GetMethod();
            LogicalThreadContext.Properties["Id"] = IdWorker.NextId();
            LogicalThreadContext.Properties["Namespace"] = Assembly.GetExecutingAssembly().FullName;
            LogicalThreadContext.Properties["TypeName"] = methodBase.DeclaringType.FullName;
            LogicalThreadContext.Properties["MethodName"] = methodBase.DeclaringType.Name;
            Log.FatalFormat(format, arg0);
        }

        public void FatalFormat(string format, params object[] args)
        {
            MethodBase methodBase = new StackTrace().GetFrame(1).GetMethod();
            LogicalThreadContext.Properties["Id"] = IdWorker.NextId();
            LogicalThreadContext.Properties["Namespace"] = Assembly.GetExecutingAssembly().FullName;
            LogicalThreadContext.Properties["TypeName"] = methodBase.DeclaringType.FullName;
            LogicalThreadContext.Properties["MethodName"] = methodBase.DeclaringType.Name;
            Log.FatalFormat(format, args);
        }

        public void FatalFormat(IFormatProvider provider, string format, params object[] args)
        {
            MethodBase methodBase = new StackTrace().GetFrame(1).GetMethod();
            LogicalThreadContext.Properties["Id"] = IdWorker.NextId();
            LogicalThreadContext.Properties["Namespace"] = Assembly.GetExecutingAssembly().FullName;
            LogicalThreadContext.Properties["TypeName"] = methodBase.DeclaringType.FullName;
            LogicalThreadContext.Properties["MethodName"] = methodBase.DeclaringType.Name;
            Log.FatalFormat(provider, format, args);
        }

        public void FatalFormat(string format, object arg0, object arg1)
        {
            MethodBase methodBase = new StackTrace().GetFrame(1).GetMethod();
            LogicalThreadContext.Properties["Id"] = IdWorker.NextId();
            LogicalThreadContext.Properties["Namespace"] = Assembly.GetExecutingAssembly().FullName;
            LogicalThreadContext.Properties["TypeName"] = methodBase.DeclaringType.FullName;
            LogicalThreadContext.Properties["MethodName"] = methodBase.DeclaringType.Name;
            Log.FatalFormat(format, arg0, arg1);
        }

        public void FatalFormat(string format, object arg0, object arg1, object arg2)
        {
            MethodBase methodBase = new StackTrace().GetFrame(1).GetMethod();
            LogicalThreadContext.Properties["Id"] = IdWorker.NextId();
            LogicalThreadContext.Properties["Namespace"] = Assembly.GetExecutingAssembly().FullName;
            LogicalThreadContext.Properties["TypeName"] = methodBase.DeclaringType.FullName;
            LogicalThreadContext.Properties["MethodName"] = methodBase.DeclaringType.Name;
            Log.FatalFormat(format, arg0, arg1, arg2);
        }

        public void Info(object message)
        {
            MethodBase methodBase = new StackTrace().GetFrame(1).GetMethod();
            LogicalThreadContext.Properties["Id"] = IdWorker.NextId();
            LogicalThreadContext.Properties["Namespace"] = Assembly.GetExecutingAssembly().FullName;
            LogicalThreadContext.Properties["TypeName"] = methodBase.DeclaringType.FullName;
            LogicalThreadContext.Properties["MethodName"] = methodBase.DeclaringType.Name;
            Log.Info(message);
        }

        public void Info(object message, Exception exception)
        {
            MethodBase methodBase = exception.TargetSite;
            LogicalThreadContext.Properties["Id"] = IdWorker.NextId();
            LogicalThreadContext.Properties["Namespace"] = Assembly.GetExecutingAssembly().FullName;
            LogicalThreadContext.Properties["TypeName"] = methodBase.DeclaringType.FullName;
            LogicalThreadContext.Properties["MethodName"] = methodBase.DeclaringType.Name;
            Log.Info(message, exception);
        }

        public void InfoFormat(string format, object arg0)
        {
            MethodBase methodBase = new StackTrace().GetFrame(1).GetMethod();
            LogicalThreadContext.Properties["Id"] = IdWorker.NextId();
            LogicalThreadContext.Properties["Namespace"] = Assembly.GetExecutingAssembly().FullName;
            LogicalThreadContext.Properties["TypeName"] = methodBase.DeclaringType.FullName;
            LogicalThreadContext.Properties["MethodName"] = methodBase.DeclaringType.Name;
            Log.InfoFormat(format, arg0);
        }

        public void InfoFormat(string format, params object[] args)
        {
            MethodBase methodBase = new StackTrace().GetFrame(1).GetMethod();
            LogicalThreadContext.Properties["Id"] = IdWorker.NextId();
            LogicalThreadContext.Properties["Namespace"] = Assembly.GetExecutingAssembly().FullName;
            LogicalThreadContext.Properties["TypeName"] = methodBase.DeclaringType.FullName;
            LogicalThreadContext.Properties["MethodName"] = methodBase.DeclaringType.Name;
            Log.InfoFormat(format, args);
        }

        public void InfoFormat(IFormatProvider provider, string format, params object[] args)
        {
            MethodBase methodBase = new StackTrace().GetFrame(1).GetMethod();
            LogicalThreadContext.Properties["Id"] = IdWorker.NextId();
            LogicalThreadContext.Properties["Namespace"] = Assembly.GetExecutingAssembly().FullName;
            LogicalThreadContext.Properties["TypeName"] = methodBase.DeclaringType.FullName;
            LogicalThreadContext.Properties["MethodName"] = methodBase.DeclaringType.Name;
            Log.InfoFormat(provider, format,args);
        }

        public void InfoFormat(string format, object arg0, object arg1)
        {
            MethodBase methodBase = new StackTrace().GetFrame(1).GetMethod();
            LogicalThreadContext.Properties["Id"] = IdWorker.NextId();
            LogicalThreadContext.Properties["Namespace"] = Assembly.GetExecutingAssembly().FullName;
            LogicalThreadContext.Properties["TypeName"] = methodBase.DeclaringType.FullName;
            LogicalThreadContext.Properties["MethodName"] = methodBase.DeclaringType.Name;
            Log.InfoFormat(format, arg0, arg1);
        }

        public void InfoFormat(string format, object arg0, object arg1, object arg2)
        {
            MethodBase methodBase = new StackTrace().GetFrame(1).GetMethod();
            LogicalThreadContext.Properties["Id"] = IdWorker.NextId();
            LogicalThreadContext.Properties["Namespace"] = Assembly.GetExecutingAssembly().FullName;
            LogicalThreadContext.Properties["TypeName"] = methodBase.DeclaringType.FullName;
            LogicalThreadContext.Properties["MethodName"] = methodBase.DeclaringType.Name;
            Log.InfoFormat(format, arg0, arg1, arg2);
        }

        public void Warn(object message)
        {
            MethodBase methodBase = new StackTrace().GetFrame(1).GetMethod();
            LogicalThreadContext.Properties["Id"] = IdWorker.NextId();
            LogicalThreadContext.Properties["Namespace"] = Assembly.GetExecutingAssembly().FullName;
            LogicalThreadContext.Properties["TypeName"] = methodBase.DeclaringType.FullName;
            LogicalThreadContext.Properties["MethodName"] = methodBase.DeclaringType.Name;
            Log.Warn(message);
        }

        public void Warn(object message, Exception exception)
        {
            MethodBase methodBase = exception.TargetSite;
            LogicalThreadContext.Properties["Id"] = IdWorker.NextId();
            LogicalThreadContext.Properties["Namespace"] = Assembly.GetExecutingAssembly().FullName;
            LogicalThreadContext.Properties["TypeName"] = methodBase.DeclaringType.FullName;
            LogicalThreadContext.Properties["MethodName"] = methodBase.DeclaringType.Name;
            Log.Warn(message, exception);
        }

        public void WarnFormat(string format, object arg0)
        {
            MethodBase methodBase = new StackTrace().GetFrame(1).GetMethod();
            LogicalThreadContext.Properties["Id"] = IdWorker.NextId();
            LogicalThreadContext.Properties["Namespace"] = Assembly.GetExecutingAssembly().FullName;
            LogicalThreadContext.Properties["TypeName"] = methodBase.DeclaringType.FullName;
            LogicalThreadContext.Properties["MethodName"] = methodBase.DeclaringType.Name;
            Log.WarnFormat(format, arg0);
        }

        public void WarnFormat(string format, params object[] args)
        {
            MethodBase methodBase = new StackTrace().GetFrame(1).GetMethod();
            LogicalThreadContext.Properties["Id"] = IdWorker.NextId();
            LogicalThreadContext.Properties["Namespace"] = Assembly.GetExecutingAssembly().FullName;
            LogicalThreadContext.Properties["TypeName"] = methodBase.DeclaringType.FullName;
            LogicalThreadContext.Properties["MethodName"] = methodBase.DeclaringType.Name;
            Log.WarnFormat(format, args);
        }

        public void WarnFormat(IFormatProvider provider, string format, params object[] args)
        {
            MethodBase methodBase = new StackTrace().GetFrame(1).GetMethod();
            LogicalThreadContext.Properties["Id"] = IdWorker.NextId();
            LogicalThreadContext.Properties["Namespace"] = Assembly.GetExecutingAssembly().FullName;
            LogicalThreadContext.Properties["TypeName"] = methodBase.DeclaringType.FullName;
            LogicalThreadContext.Properties["MethodName"] = methodBase.DeclaringType.Name;
            Log.WarnFormat(provider, format, args);
        }

        public void WarnFormat(string format, object arg0, object arg1)
        {
            MethodBase methodBase = new StackTrace().GetFrame(1).GetMethod();
            LogicalThreadContext.Properties["Id"] = IdWorker.NextId();
            LogicalThreadContext.Properties["Namespace"] = Assembly.GetExecutingAssembly().FullName;
            LogicalThreadContext.Properties["TypeName"] = methodBase.DeclaringType.FullName;
            LogicalThreadContext.Properties["MethodName"] = methodBase.DeclaringType.Name;
            Log.WarnFormat(format, arg0, arg1);
        }

        public void WarnFormat(string format, object arg0, object arg1, object arg2)
        {
            MethodBase methodBase = new StackTrace().GetFrame(1).GetMethod();
            LogicalThreadContext.Properties["Id"] = IdWorker.NextId();
            LogicalThreadContext.Properties["Namespace"] = Assembly.GetExecutingAssembly().FullName;
            LogicalThreadContext.Properties["TypeName"] = methodBase.DeclaringType.FullName;
            LogicalThreadContext.Properties["MethodName"] = methodBase.DeclaringType.Name;
            Log.WarnFormat(format, arg0, arg1, arg2);
        }
    }
}