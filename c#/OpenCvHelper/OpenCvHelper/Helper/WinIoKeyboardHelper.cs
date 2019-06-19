using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace openCvHelper.Helper
{
    /// <summary>
    /// 基于winio的鼠标操作帮助类
    /// </summary>
    public class WinIoKeyboardHelper
    {
        /// <summary>
        /// 初始化winio
        /// </summary>
        /// <returns>初始化成功返回true，否则返回false</returns>
        [DllImport("WinIo32.dll")]
        static extern bool InitializeWinIo();

        /// <summary>
        /// 关闭winIo
        /// </summary>
        [DllImport("WinIo32.dll")]
        static extern void ShutdownWinIo();

        /// <summary>
        /// 通过键盘控制端口，读取键盘缓冲区指定字节数的数据
        /// </summary>
        /// <param name="wPortAddr">键盘控制端口</param>
        /// <param name="pdwPortVal">读取特定字节的数据返回值</param>
        /// <param name="bSize">读取字节数</param>
        /// <returns></returns>
        [DllImport("WinIo32.dll")]
        static extern bool GetPortVal(IntPtr wPortAddr, out int pdwPortVal, byte bSize);

        /// <summary>
        /// 通过键盘控制端口，写入键盘缓冲区指定字节数的数据
        /// </summary>
        /// <param name="wPortAddr"></param>
        /// <param name="dwPortVal"></param>
        /// <param name="bSize"></param>
        /// <returns></returns>
        [DllImport("WinIo32.dll")]
        static extern bool SetPortVal(uint wPortAddr, IntPtr dwPortVal,byte bSize);

        /// <summary>
        /// 获取键入的按钮的key值
        /// </summary>
        /// <param name="Ucode"></param>
        /// <param name="uMapType"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        static extern int MapVirtualKey(uint Ucode, uint uMapType);

        const int KBC_KEY_CMD = 0x64;
        const int KBC_KEY_DATA = 0x60;


        /// <summary>
        /// 模拟键盘松开按键
        /// </summary>
        /// <param name="vKeyCoad">松开的按键</param>
        static void KeyUp(Keys vKeyCoad)
        {
            int btScancode = 0;
            btScancode = MapVirtualKey((uint)vKeyCoad, 0);
            KBCWait4IBE();
            SetPortVal(KBC_KEY_CMD, (IntPtr)0xD2, 1);
            KBCWait4IBE();
            SetPortVal(KBC_KEY_DATA, (IntPtr)0x60, 1);
            KBCWait4IBE();
            SetPortVal(KBC_KEY_CMD, (IntPtr)0xD2, 1);
            KBCWait4IBE();
            SetPortVal(KBC_KEY_DATA, (IntPtr)(btScancode | 0x80), 1);
        }

        /// <summary>
        /// 模拟键盘按下按键
        /// </summary>
        /// <param name="vKeyCoad">按下的按键</param>
        static void KeyDown(Keys vKeyCoad)
        {
            int btScancode = 0;
            btScancode = MapVirtualKey((uint)vKeyCoad, 0);
            KBCWait4IBE();
            SetPortVal(KBC_KEY_CMD, (IntPtr)0xD2, 1);
            KBCWait4IBE();
            SetPortVal(KBC_KEY_DATA, (IntPtr)0x60, 1);
            KBCWait4IBE();
            SetPortVal(KBC_KEY_CMD, (IntPtr)0xD2, 1);
            KBCWait4IBE();
            SetPortVal(KBC_KEY_DATA, (IntPtr)btScancode, 1);
        }

        /// <summary>
        /// 不断地读取键盘缓冲区的数据，当缓冲区没有数据时，可以
        /// 确保输入的值是确实输入了。避免缓冲区空间不足导致键入失败。
        /// </summary>
        static void KBCWait4IBE()
        {
            int dwVal = 0;
            do
            {
                bool flag = GetPortVal((IntPtr)0x64, out dwVal, 1);
            }
            while ((dwVal & 0x2) > 0);
        }

        /// <summary>
        /// 初始化winio
        /// </summary>
        static void Initialize()
        {
            if (InitializeWinIo())
            {
                KBCWait4IBE();
            }
        }

        /// <summary>
        /// 注销winio
        /// </summary>
        static void Shutdown()
        {
            ShutdownWinIo();
            KBCWait4IBE();
        }

        /// <summary>
        /// 按下，然后松开按键
        /// </summary>
        /// <param name="vKeyCoad"></param>
        public static void KeyDownUp(Keys vKeyCoad)
        {
            Initialize();
            KeyDown(vKeyCoad);
            Thread.Sleep(100);
            KeyUp(vKeyCoad);
            Shutdown();
        }
    }
}