using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper.Helper
{
    /// <summary>
    /// 硬件控制操作类
    /// </summary>
    public class HardwareHelper
    {
        /// <summary>
        /// windows操作系统的消息钩子
        /// </summary>
        /// <param name="hWnd">消息接收者</param>
        /// <param name="Msg">消息类型</param>
        /// <param name="wParam">消息子类型，例如：显示器开关</param>
        /// <param name="lParam">消息的参数，例如：开启1、关闭2</param>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

        /// <summary>
        /// 所有窗体都能接收的顶级消息
        /// </summary>
        static readonly IntPtr HWND_BROADCAST = new IntPtr(0xffff);

        /// <summary>
        /// 关闭显示器
        /// </summary>
        public static void MonitorOff()
        {
            uint WM_SYSCOMMAND = 0x112; //系统消息
            int SC_MONITORPOWER = 0xF170; //关闭显示器的系统命令
            int MonitorPowerOff = 2; //消息参数：2为PowerOff, 1为省电状态，-1为开机
            SendMessage(HWND_BROADCAST, WM_SYSCOMMAND, SC_MONITORPOWER, MonitorPowerOff);
        }

        /// <summary>
        /// 开启显示器
        /// </summary>
        public static void MonitorOn()
        {
            uint WM_SYSCOMMAND = 0x112; //系统消息
            int SC_MONITORPOWER = 0xF170; //关闭显示器的系统命令
            int MonitorPowerOff = -1; //消息参数：2为PowerOff, 1为省电状态，-1为开机
            SendMessage(HWND_BROADCAST, WM_SYSCOMMAND, SC_MONITORPOWER, MonitorPowerOff);
        }
    }
}
