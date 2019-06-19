using CommonHelper.Helper;
using OpenCvHelper.ImageOper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace OpenCvHelper.Helper
{
    public struct Rect
    {
        public int left;
        public int top;
        public int right;
        public int buttom;
    }
    /// <summary>
    /// windows操作鼠标类。
    /// </summary>
    public class SendData
    {
        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        private static extern int SendMessageA(IntPtr hwnd, int wMsg, IntPtr wParam, string lParam);

        [DllImport("user32.dll")]
        private static extern int GetWindowRect(IntPtr hwnd, out Rect lpRect);

        [DllImport("User32.dll", EntryPoint = "FindWindowA")]
        private static extern IntPtr FindWindowA(string lpClassName, string lpWindowName);
        [DllImport("User32.dll", EntryPoint = "FindWindowEx")]
        private static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpClassName, string lpWindowName);
        [DllImport("user32.dll", EntryPoint = "GetWindowTextA")]
        public static extern int GetWindowTextA(
                IntPtr hWnd, StringBuilder lpString, int nMaxCount);
        [DllImport("User32")]
        public static extern bool SendMessageA(
        IntPtr hWnd,      // handle to destination window
        int Msg,       // message
         int wParam,  // first message parameter
        IntPtr lParam   // second message parameter
        );
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowThreadProcessId(IntPtr hwnd, out int ID);
        [DllImport("kernel32.dll")]
        public static extern int OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        static extern bool ReadProcessMemory(uint hProcess, IntPtr lpBaseAddress,
           IntPtr lpBuffer, uint nSize, ref uint lpNumberOfBytesRead);
        /// <summary>
        /// 
        /// </summary>
        const int PROCESS_ALL_ACCESS = 0x1F0FFF;
        const int PROCESS_VM_READ = 0x0010;
        const int PROCESS_VM_WRITE = 0x0020;


        /// <summary>
        /// 设置鼠标的位置
        /// </summary>
        /// <param name="x">设置的x坐标</param>
        /// <param name="y">设置的y坐标</param>
        [System.Runtime.InteropServices.DllImport("user32")]
        public static extern void SetCursorPos(int x, int y);

        /// <summary>
        /// 鼠标操作事件
        /// </summary>
        /// <param name="dwFlags">操作类型</param>
        /// <param name="dx">操作后改变的x坐标</param>
        /// <param name="dy">操作后改变的y坐标</param>
        /// <param name="cButtons"></param>
        /// <param name="dwExtraInfo"></param>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImport("user32")]
        public static extern int mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        /// <summary>
        /// 鼠标的移动操作
        /// </summary>
        public const int MOUSEEVENTF_MOVE = 0x0001;

        /// <summary>
        /// 鼠标的左键按下操作
        /// </summary>
        public const int MOUSEEVENTF_LEFTDOWN = 0x0002;

        /// <summary>
        /// 鼠标的左键弹起操作
        /// </summary>
        public const int MOUSEEVENTF_LEFTUP = 0x0004;

        /// <summary>
        /// 鼠标的右键按下操作
        /// </summary>
        public const int MOUSEEVENTF_RIGHTDOWN = 0x0008;

        /// <summary>
        /// 鼠标的右键弹起操作
        /// </summary>
        public const int MOUSEEVENTF_RIGHTUP = 0x0010;

        /// <summary>
        /// 鼠标的中间滑轮按下操作
        /// </summary>
        public const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;

        /// <summary>
        /// 鼠标的中间滑轮弹起操作
        /// </summary>
        public const int MOUSEEVENTF_MIDDLEUP = 0x0040;


        public const int MOUSEEVENTF_ABSOLUTE = 0x8000;
        ///*////////////////////////////////////////////////////////////////////
    }
    /// <summary>
    /// 鼠标控制类
    /// </summary>
    public class MouseHelper
    {
        public enum MouseClickType : byte
        {
            /// <summary>
            /// 左点击
            /// </summary>
            LeftClick,
            /// <summary>
            /// 按下左按钮，不松开。
            /// </summary>
            LeftPress,
            /// <summary>
            /// 松开左按钮
            /// </summary>
            LeftRelease,
            /// <summary>
            /// 右点击
            /// </summary>
            RightClick,
            /// <summary>
            /// 中间滚轮点击
            /// </summary>
            MiddleClick,
            /// <summary>
            /// 长按鼠标左键
            /// </summary>
            LongLeftClick
        }

        /// <summary>
        /// 鼠标移动
        /// </summary>
        /// <param name="x">x坐标</param>
        /// <param name="y">y坐标</param>
        public static void MouseMove(int x, int y)=> SendData.SetCursorPos(x, y);

        /// <summary>
        /// 鼠标移动到特定矩形的中心点
        /// </summary>
        /// <param name="x">矩形左上角的x坐标</param>
        /// <param name="y">矩形左上角的y坐标</param>
        /// <param name="bitmap">和特定矩形有相同宽度和高度的bitmap</param>
        public static void MouseMoveCenter(int x, int y,Bitmap bitmap)=>
            MouseMoveCenter(x, y, bitmap.Width, bitmap.Height);

        /// <summary>
        /// 鼠标移动到特定矩形的中心点
        /// </summary>
        /// <param name="x">矩形左上角的x坐标</param>
        /// <param name="y">矩形左上角的y坐标</param>
        /// <param name="width">矩形的宽度</param>
        /// <param name="height">矩形的高度</param>
        public static void MouseMoveCenter(int x, int y,int width,int height)=>
            MouseMove(x + (width>>1), y+ (height>>1));


        /// <summary>
        /// 鼠标移动
        /// </summary>
        /// <param name="Point"></param>
        public static void MouseMove(Point Point) => MouseMove(Point.X, Point.Y);

        /// <summary>
        /// 鼠标操作
        /// </summary>
        /// <param name="ClickType"></param>
        public static void MouseClick(MouseClickType ClickType)
        {
            switch (ClickType)
            {
                case MouseClickType.LeftClick:
                    SendData.mouse_event(SendData.MOUSEEVENTF_LEFTDOWN | SendData.MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                    break;
                case MouseClickType.RightClick:
                    SendData.mouse_event(SendData.MOUSEEVENTF_RIGHTDOWN | SendData.MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                    break;
                case MouseClickType.LongLeftClick:
                    SendData.mouse_event(SendData.MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                    break;
                case MouseClickType.LeftPress:
                    SendData.mouse_event(SendData.MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                    break;
                case MouseClickType.LeftRelease:
                    SendData.mouse_event(SendData.MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                    break;
            }
        }

        /// <summary>
        /// 点击按钮，先按下按钮不松开，然后观察按钮是否发生样式变化；如果
        /// 没有发生变化，则一段时间后再次检查，直到发生变化后才松开按钮，形成一次
        /// 点击事件。如果按下很长时间都没有发生变化，则认为手机模拟器卡死。
        /// </summary>
        /// <param name="x">观察范围左上角的x坐标</param>
        /// <param name="y">观察范围左上角的y坐标</param>
        /// <param name="bitmap">和观察范围有相同宽度和高度的bitmap对象</param>
        public static void ClickBtn(int x,int y,Bitmap bitmap)=>
            ClickBtn(new Rectangle(x, y, bitmap.Width, bitmap.Height));

        /// <summary>
        /// 点击按钮，先按下按钮不松开，然后观察按钮是否发生样式变化；如果
        /// 没有发生变化，则一段时间后再次检查，直到发生变化后才松开按钮，形成一次
        /// 点击事件。如果按下很长时间都没有发生变化，则认为手机模拟器卡死。
        /// </summary>
        /// <param name="ClickRange">观察范围</param>
        public static void ClickBtn(Rectangle ClickRange)
        {
            Bitmap Before = ImageHandleHelper.CopyScreen(ClickRange);
            Bitmap After;
            for (int i = 0; i < 200; i++)
            {
                if (OpencvImageHelper.CheckChange(ClickRange, 4))
                {
                    if (i % 10 == 0)
                        MouseClick(MouseClickType.LeftPress);
                    if (OpencvImageHelper.Compare2Images(Before, After = ImageHandleHelper.CopyScreen(ClickRange), 0) != 0)
                    {
                        Before.Dispose();
                        Before = After;
                        MouseClick(MouseClickType.LeftRelease);
                        break;
                    }
                    After.Dispose();
                }
            }
            for (int i = 0; i < 200; i++)
            {
                if (OpencvImageHelper.CheckChange(ClickRange, 4))
                {
                    using (Before) { }
                    return;
                }
            }
            MouseMove(ClickRange.X > ClickRange.Width ? 0 : ClickRange.X + ClickRange.Width + 1000, 0);
            MouseClick(MouseClickType.LeftRelease);
            throw new Exception("界面卡死。。(ERROR:-999)");
        }

        /// <summary>
        /// 长按一个按钮，并观察画面是否发生变化，如果有变化则长按事件完成。
        /// 如果长时间没有变化，则认为手机模拟器卡死。
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public static void LongPressBtn(int x, int y,int width,int height)=>
            LongPressBtn(new Rectangle(x, y, width, height));

        /// <summary>
        /// 长按一个按钮，并观察画面是否发生变化，如果有变化则长按事件完成。
        /// 如果长时间没有变化，则认为手机模拟器卡死。
        /// </summary>
        /// <param name="pressRange">观察范围</param>
        public static void LongPressBtn(Rectangle pressRange)
        {
            using (Bitmap Before = ImageHandleHelper.CopyScreen(pressRange))
            {
                for (int i = 0, x, y; i < 1000; i++)
                {
                    if (i % 10 == 0)
                    {
                        MouseClick(MouseClickType.LeftPress);
                        Thread.Sleep(500);
                    }
                    if (!ImageHandleHelper.CopyScreen(pressRange).OCRImages(Before, out x, out y, true, .9))
                    {
                        MouseClick(MouseClickType.LeftRelease);
                        return;
                    }
                }
            }
            throw new Exception("界面卡死！(ERROR:-999)");
        }

        /// <summary>
        /// 重复点击同一个按钮，直到界面发生变化时才停止点击，用于确保
        /// 界面卡顿也能触发点击事件。
        /// </summary>
        /// <param name="x">观察范围左上角的x坐标</param>
        /// <param name="y">观察范围左上角的y坐标</param>
        /// <param name="bitmap">和观察范围有相同宽度和高度的bitmap</param>
        public static void RepeatClickBtn(int x,int y,Bitmap bitmap)=>
            RepeatClickBtn(new Rectangle(x,y, bitmap.Width,bitmap.Height));

        /// <summary>
        /// 重复点击同一个按钮，直到界面发生变化时才停止点击，用于确保
        /// 界面卡顿也能触发点击事件。
        /// </summary>
        /// <param name="ChangeRange">需要观察的变化范围</param>
        public static void RepeatClickBtn(Rectangle ChangeRange)
        {
            Bitmap Before = ImageHandleHelper.CopyScreen(ChangeRange);
            for (int i = 0; i < 1000; i++)
            {
                if (i % 10 == 0)
                    MouseHelper.MouseClick(MouseClickType.LeftClick);
                Thread.Sleep(25);
                if (OpencvImageHelper.Compare2Images(Before, ImageHandleHelper.CopyScreen(ChangeRange), 1) != 0)
                    return;
            }
            throw new Exception("界面卡死！(ERROR:-999)");
        }
    }
}