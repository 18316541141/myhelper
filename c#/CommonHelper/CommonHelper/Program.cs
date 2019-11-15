using CommonHelper.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
namespace PA_Robot
{
    static class Program
    {

        [DllImport("kernel32.dll")]
        public static extern Boolean AllocConsole();

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool ret = GuessHelper.GuessIDCard18Exist("************000000");
            Console.WriteLine();
        }
        static void test(string lockName)
        {
            lock (string.Intern(lockName))
            {
                for (var i =0;i<5;i++)
                {
                    Console.WriteLine($"当前线程正在占用：{lockName}，线程号：{Thread.CurrentThread.ManagedThreadId}");
                    Thread.Sleep(1000);
                }
            }
        }
    }
}