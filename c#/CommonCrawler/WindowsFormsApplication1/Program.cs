using Autofac;
using Autofac.Extras.DynamicProxy;
using log4net;
using log4net.Config;
using Snowflake.Net;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;

namespace WindowsFormsApplication1
{
    static class Program
    {
        public static IContainer Container { private set; get; }

        [DllImport("kernel32.dll")]
        public static extern Boolean AllocConsole();

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
        }
    }
}
