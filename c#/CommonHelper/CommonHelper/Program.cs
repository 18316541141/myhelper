using CommonHelper.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            AllocConsole();
            NonNegativeDoubleLimitValue non = new NonNegativeDoubleLimitValue(100,2, LimitValueRule.LESS_OR_EQUALS_MANUAL, "A", "B", "C", "D", "E");
            non.UpdateVal("A", .2);
            non.UpdateThreadHold(77);
            non.AddVal("F",2.34);
            non.UpdateVal("B", .2);
            non.UpdateThreadHold(87);
            non.AddVal("G", 5.67);
            non.UpdateVal("C", .2);
            non.UpdateThreadHold(100.86);
            non.AddVal("I", 4.44);
            non.UpdateVal("D", .2);
            non.UpdateThreadHold(6.66);
            non.AddVal("J", .22);
            non.UpdateVal("E", .2);
            non.UpdateThreadHold(99.99);
            non.AddVal("K", .13);
            Console.ReadKey();
        }
        /// <summary>
        /// 数字转中文
        /// </summary>
        /// <param name="number">eg: 22</param>
        /// <returns></returns>
        //public static string NumberToChinese(this int number)
        //{
        //    string res = string.Empty;
        //    string str = number.ToString();
        //    string schar = str.Substring(0, 1);
        //    switch (schar)
        //    {
        //        case "1":
        //            res = "一";
        //            break;
        //        case "2":
        //            res = "二";
        //            break;
        //        case "3":
        //            res = "三";
        //            break;
        //        case "4":
        //            res = "四";
        //            break;
        //        case "5":
        //            res = "五";
        //            break;
        //        case "6":
        //            res = "六";
        //            break;
        //        case "7":
        //            res = "七";
        //            break;
        //        case "8":
        //            res = "八";
        //            break;
        //        case "9":
        //            res = "九";
        //            break;
        //        default:
        //            res = "零";
        //            break;
        //    }
        //    if (str.Length > 1)
        //    {
        //        switch (str.Length)
        //        {
        //            case 2:
        //            case 6:
        //                res += "十";
        //                break;
        //            case 3:
        //            case 7:
        //                res += "百";
        //                break;
        //            case 4:
        //                res += "千";
        //                break;
        //            case 5:
        //                res += "万";
        //                break;
        //            default:
        //                res += "";
        //                break;
        //        }
        //        res += NumberToChinese(int.Parse(str.Substring(1, str.Length - 1)));
        //    }
        //    return res;
        //}
    }
}