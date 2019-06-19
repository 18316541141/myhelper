using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommonHelper.Helper
{
    /// <summary>
    /// 进程帮助类
    /// </summary>
    public static class ProcessHelper
    {
        /// <summary>
        /// 运行一个进程
        /// </summary>
        /// <param name="appPath">传入应用的路径</param>
        /// <param name="arguments">启动时传入的参数</param>
        public static Process ExcuteProcess(string appPath, string arguments = "")
        {
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = appPath;    // 指定路径
            info.Arguments = "";
            info.WindowStyle = ProcessWindowStyle.Normal;   // 设置窗体
            if (!string.IsNullOrEmpty(arguments))
            {
                info.Arguments = arguments;
            }
            return Process.Start(info);
        }

        /// <summary>
        /// 寻找符合条件的进程。
        /// </summary>
        /// <param name="findProcessesParam"></param>
        /// <returns></returns>
        public static Process[] FindProcesses(FindProcessesParam findProcessesParam)
        {
            Process[] processes = Process.GetProcesses();
            //processes[0].
            if (!string.IsNullOrEmpty(findProcessesParam.ProcessName))
            {
                processes = processes.Where(a => a.ProcessName.Contains(findProcessesParam.ProcessName)).ToArray();
            }
            if (findProcessesParam.HasMainWindowTitle.HasValue)
            {
                processes = processes.Where(a => findProcessesParam.HasMainWindowTitle.Value ? !string.IsNullOrEmpty(a.MainWindowTitle) : string.IsNullOrEmpty(a.MainWindowTitle)).ToArray();
            }
            if (!string.IsNullOrEmpty(findProcessesParam.MainWindowTitle))
            {
                processes = processes.Where(a => a.MainWindowTitle.Contains(findProcessesParam.MainWindowTitle)).ToArray();
            }
            //DateTime t=processes[0].StartTime;
            //if (findProcessesParam.StartTimeAsc)
            //{
            //    processes = processes.OrderBy(a=>a.StartTime).ToArray();
            //}
            //if (findProcessesParam.StartTimeDesc)
            //{
            //    processes = processes.OrderByDescending(a => a.StartTime).ToArray();
            //}
            return processes;
        }

        /// <summary>
        /// 寻找符合条件的第一个进程
        /// </summary>
        /// <param name="findProcessesParam"></param>
        /// <returns></returns>
        public static Process FirstProcess(FindProcessesParam findProcessesParam) =>
            FindProcesses(findProcessesParam)[0];

        /// <summary>
        /// 进程重启，并返回重启后的新进程对象
        /// </summary>
        /// <param name="process"></param>
        /// <param name="arguments">进程启动时参数</param>
        /// <returns>返回重启后的新进程对象</returns>
        public static Process ProcessRestart(this Process process, string arguments = "")
        {
            using (process)
            {
                string fileName = process.MainModule.FileName;
                process.Kill();
                return ExcuteProcess(fileName, arguments);
            }
        }

        /// <summary>
        /// 使得一个窗口进程始终在最前面
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        /// 使得一个窗口进程始终在最前面
        /// </summary>
        /// <param name="process">窗口进程</param>
        public static void SetForegroundWindow(this Process process) =>
            SetForegroundWindow(process.MainWindowHandle);

        /// <summary>
        /// 移动进程位置
        /// </summary>
        /// <param name="process">进程对象</param>
        /// <param name="x">移动到x坐标</param>
        /// <param name="y">移动到y坐标</param>
        public static void ProcessMove(this Process process, int x, int y)
        {
            IntPtr intPtr = process.MainWindowHandle;
            RECT rect;
            rect.Left = 0;
            rect.Top = 0;
            rect.Right = 0;
            rect.Bottom = 0;
            GetWindowRect(intPtr, ref rect);
            MoveWindow(intPtr, x, y, rect.Right - rect.Left, rect.Bottom - rect.Top, true);
        }

        /// <summary>
        /// 移动进程和四个边缘的距离
        /// </summary>
        /// <param name="process">进程对象</param>
        /// <param name="top">和最上边的边缘距离，-1则自动设置</param>
        /// <param name="right">和最右边的边缘距离，-1则自动设置</param>
        /// <param name="bottom">和最下边的边缘距离，-1则自动设置</param>
        /// <param name="left">和最左边的边缘距离，-1则自动设置</param>
        public static void ProcessMove(this Process process, int top, int right, int bottom, int left)
        {
            int x = 0;
            if (left != -1)
            {
                x = left;
            }
            int y = 0;
            if (top != -1)
            {
                y = top;
            }
            IntPtr intPtr = process.MainWindowHandle;
            RECT rect;
            rect.Left = 0;
            rect.Top = 0;
            rect.Right = 0;
            rect.Bottom = 0;
            GetWindowRect(intPtr, ref rect);
            Rectangle screenArea = Screen.GetWorkingArea(new Point());
            if (right != -1)
            {
                x = screenArea.Width - rect.Right + rect.Left - right;
            }
            if (bottom != -1)
            {
                y = screenArea.Height - rect.Bottom + rect.Top - bottom;
            }
            MoveWindow(intPtr, x, y, rect.Right - rect.Left, rect.Bottom - rect.Top, true);

        }

        /// <summary>
        /// 改变进程的窗口大小的内置宽度和高度
        /// </summary>
        public static class InSize
        {
            /// <summary>
            /// 传入该宽度，则直接使用进程窗口原来的宽度
            /// </summary>
            public static int ProcessWidth = -1;

            /// <summary>
            /// 传入该高度，则直接使用进程窗口原来的高度
            /// </summary>
            public static int ProcessHeight = -2;

            /// <summary>
            /// 传入该宽度，则直接使用屏幕的宽度
            /// </summary>
            public static int WinWidth = -3;

            /// <summary>
            /// 传入该高度，则直接使用屏幕的高度
            /// </summary>
            public static int WinHeight = -4;
        }

        /// <summary>
        /// 改变进程的窗口大小
        /// </summary>
        /// <param name="process">进程对象</param>
        /// <param name="width">改变后的宽度</param>
        /// <param name="height">改变后的高度</param>
        public static void ProcessSize(this Process process, int width, int height)
        {
            IntPtr intPtr = process.MainWindowHandle;
            RECT rect;
            rect.Left = 0;
            rect.Top = 0;
            rect.Right = 0;
            rect.Bottom = 0;
            GetWindowRect(intPtr, ref rect);
            Rectangle screenArea = Screen.GetWorkingArea(new Point());
            if (InSize.WinWidth == width)
            {
                width = screenArea.Width;
            }
            else if (InSize.ProcessWidth == width)
            {
                width = rect.Right - rect.Left;
            }
            if (InSize.WinHeight == height)
            {
                height = screenArea.Height;
            }
            else if (InSize.ProcessHeight == height)
            {
                height = rect.Bottom - rect.Top;
            }
            bool ret = MoveWindow(intPtr, rect.Left, rect.Top, width, height, true);
        }

        /// <summary>
        /// 顶置窗口进程
        /// </summary>
        /// <param name="process"></param>
        public static void ProcessTop(this Process process)
        {
            IntPtr intPtr = process.MainWindowHandle;
            RECT rect;
            rect.Left = 0;
            rect.Top = 0;
            rect.Right = 0;
            rect.Bottom = 0;
            GetWindowRect(intPtr, ref rect);
            SetWindowPos(intPtr, -1, rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top, 1 | 4);
        }

        /// <summary>
        /// 使用win32Api移动并改变窗体大小
        /// </summary>
        /// <param name="hWnd">窗体的句柄</param>
        /// <param name="X">移动到的X坐标</param>
        /// <param name="Y">移动到的Y坐标</param>
        /// <param name="nWidth">改变的宽度</param>
        /// <param name="nHeight">改变的高度度</param>
        /// <param name="bRepaint">是否重绘窗口</param>
        /// <returns></returns>
        [DllImportAttribute("user32.dll", EntryPoint = "MoveWindow")]
        static extern bool MoveWindow(System.IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        /// <summary>
        /// 使用win32Api获取窗口大小所需的结构体返回值
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        struct RECT
        {
            public int Left; //左上角x坐标
            public int Top; //左上角y坐标
            public int Right; //右下角x坐标
            public int Bottom; //右下角y坐标
        }

        /// <summary>
        /// 使用win32Api获取窗口相对屏幕的左、上、右、下的位置
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="lpRect"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);

        /// <summary>
        /// 设置窗口位置和状态
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <param name="hWndlnsertAfter">处于某窗口的后面</param>
        /// <param name="X">X坐标</param>
        /// <param name="Y">Y坐标</param>
        /// <param name="cx">宽度</param>
        /// <param name="cy">高度</param>
        /// <param name="Flags">窗口状态</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, int hWndlnsertAfter, int X, int Y, int cx, int cy, uint Flags);
    }

    /// <summary>
    /// 寻找进程的参数
    /// </summary>
    public class FindProcessesParam
    {
        /// <summary>
        /// 进程名称，模糊匹配
        /// </summary>
        public string ProcessName { set; get; }

        /// <summary>
        /// 进程是否具有主窗口标题，如果true则会搜索出所有含有主窗口的进程，
        /// 一般用于查询出有窗口的进程
        /// </summary>
        public bool? HasMainWindowTitle { set; get; }

        /// <summary>
        /// 进程主窗口的标题，模糊查询条件
        /// </summary>
        public string MainWindowTitle { set; get; }

        /// <summary>
        /// 进程创建时间升序排列
        /// </summary>
        public bool StartTimeAsc { set; get; }

        /// <summary>
        /// 进程创建时间降序排列
        /// </summary>
        public bool StartTimeDesc { set; get; }
    }
}