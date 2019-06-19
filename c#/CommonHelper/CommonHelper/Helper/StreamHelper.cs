using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper.Helper
{
    /// <summary>
    /// 流帮助类
    /// </summary>
    public class StreamHelper
    {
        /// <summary>
        /// 拷贝流，这里直接使用参数传入的流进行拷贝操作，效率可能比不上使用缓冲流。
        /// </summary>
        /// <param name="Src">源输入流</param>
        /// <param name="Target">目标输出流</param>
        /// <param name="AutoClose">是否自动关闭Stream</param>
        public static void CopyStream(Stream src, Stream target, bool autoClose = true)
        {
            byte[] Buff = new byte[4096];
            for (int Len; (Len = src.Read(Buff, 0, Buff.Length)) > 0;)
                target.Write(Buff, 0, Len);
            if (autoClose)
            {
                src.Dispose();
                target.Dispose();
            }
        }

        /// <summary>
        /// 快速的拷贝流，这里不够是什么流都会使用缓冲流进行操作，如果本身就是缓冲流就
        /// 直接使用CopyStream
        /// </summary>
        /// <param name="src"></param>
        /// <param name="target"></param>
        /// <param name="autoClose"></param>
        public static void FastCopyStream(Stream src, Stream target, bool autoClose = true)
        {
            byte[] Buff = new byte[4096];
            using (Stream buffSrc = new BufferedStream(src))
            using (Stream buffTarget = new BufferedStream(target))
            {
                for (int Len; (Len = buffSrc.Read(Buff, 0, Buff.Length)) > 0;)
                    buffTarget.Write(Buff, 0, Len);
                buffSrc.Dispose();
                buffTarget.Dispose();
                if (autoClose)
                {
                    src.Dispose();
                    target.Dispose();
                }
            }
        }
    }
}
