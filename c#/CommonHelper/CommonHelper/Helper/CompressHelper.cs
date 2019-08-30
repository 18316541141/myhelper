using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper.Helper
{
    /// <summary>
    /// 压缩、解压缩类
    /// </summary>
    public class CompressHelper
    {

        /// <summary>
        /// 图片压缩(降低质量以减小文件的大小)
        /// </summary>
        /// <param name="srcStream">传入的流对象</param>
        /// <param name="destStream">压缩后的Stream对象</param>
        /// <param name="level">压缩等级，0到100，0 最差质量，100 最佳</param>
        public static void ImgCompress(Stream srcStream, Stream destStream, long level=50) =>
            ImgCompress(new Bitmap(srcStream), destStream, level);

        /// <summary>
        /// 根据mimeType获取图片的格式
        /// </summary>
        /// <param name="mimeType">mimeType</param>
        /// <returns>返回图片格式信息</returns>
        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }

        /// <summary>
        /// 图片压缩(降低质量以减小文件的大小)
        /// </summary>
        /// <param name="srcBitmap">传入的Bitmap对象</param>
        /// <param name="destStream">压缩后的Stream对象</param>
        /// <param name="level">压缩等级，0到100，0 最差质量，100 最佳</param>
        public static void ImgCompress(Bitmap srcBitmap, Stream destStream, long level = 50)
        {
            ImageCodecInfo myImageCodecInfo;
            System.Drawing.Imaging.Encoder myEncoder;
            EncoderParameter myEncoderParameter;
            EncoderParameters myEncoderParameters;

            // Get an ImageCodecInfo object that represents the JPEG codec.
            myImageCodecInfo = GetEncoderInfo("image/jpeg");

            // Create an Encoder object based on the GUID

            // for the Quality parameter category.
            myEncoder = System.Drawing.Imaging.Encoder.Quality;

            // Create an EncoderParameters object.
            // An EncoderParameters object has an array of EncoderParameter
            // objects. In this case, there is only one

            // EncoderParameter object in the array.
            myEncoderParameters = new EncoderParameters(1);

            // Save the bitmap as a JPEG file with 给定的 quality level
            myEncoderParameter = new EncoderParameter(myEncoder, level);
            myEncoderParameters.Param[0] = myEncoderParameter;
            srcBitmap.Save(destStream, myImageCodecInfo, myEncoderParameters);
            using (srcBitmap) { }
            using (destStream) { }
        }

        /// <summary>
        /// 使用gzip方式解压数据
        /// </summary>
        /// <param name="inputStream">源数据输入流</param>
        /// <returns>返回解压后的数据字节数组</returns>
        public static byte[] GZipDecompress(Stream inputStream)
        {
            byte[] buf = new byte[1024];
            using (GZipStream decompressedStream = new GZipStream(inputStream, CompressionMode.Decompress, true))
            using (MemoryStream ms = new MemoryStream())
            {
                int count = 0;
                do
                {
                    count = decompressedStream.Read(buf, 0, buf.Length);
                    ms.Write(buf, 0, count);
                    ms.Flush();
                }
                while (count > 0);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// 使用gzip方式解压数据
        /// </summary>
        /// <param name="data">源数据</param>
        /// <param name="fileName">解压后数据保存的文件全名（含路径）</param>
        /// <returns>返回解压后数据长度</returns>
        public static int GZipDecompress(byte[] data, string fileName)
        {
            int iRet = 0;
            byte[] buf = new byte[1024];

            FileStream fs = new FileStream(fileName, FileMode.Create);
            try
            {
                MemoryStream ms = new MemoryStream(data);
                GZipStream decompressedStream = new GZipStream(ms, CompressionMode.Decompress, true);

                int count = 0;
                do
                {
                    count = decompressedStream.Read(buf, 0, buf.Length);
                    fs.Write(buf, 0, count);
                    fs.Flush();
                }
                while (count > 0);

                iRet = (int)fs.Length;

            }
            finally
            {
                fs.Close();
            }
            return iRet;
        }

        /// <summary>
        /// 使用gzip方式对字符串进行压缩，并返回base64
        /// </summary>
        /// <param name="text">文本字符串</param>
        /// <param name="encoding">编码方式</param>
        /// <returns></returns>
        public static string GZipCompressString(string text, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }
            using (MemoryStream ms = new MemoryStream(encoding.GetBytes(text)))
            {
                return Convert.ToBase64String(GZipCompress(ms));
            }
        }

        /// <summary>
        /// 使用gzip方式对base64字符串进行解压，并返回解压后字符串
        /// </summary>
        /// <param name="text">文本字符串</param>
        /// <param name="encoding">编码方式</param>
        /// <returns></returns>
        public static string GZipDecompressString(string text, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }
            using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(text)))
            {
                return encoding.GetString(GZipDecompress(ms));
            }
        }

        /// <summary>
        /// 使用gzip方式压缩数据
        /// </summary>
        /// <param name="inputSteam">需要压缩的数据流</param>
        /// <returns>返回压缩后的byte数组</returns>
        public static byte[] GZipCompress(Stream inputSteam)
        {
            //压缩后的MemoryStream
            using (MemoryStream ms = new MemoryStream()) {
                GZipStream compressedStream = new GZipStream(ms, CompressionMode.Compress, true);

                byte[] buf = new byte[1024];
                int count = 0;
                do
                {
                    count = inputSteam.Read(buf, 0, buf.Length);
                    compressedStream.Write(buf, 0, count);
                }
                while (count > 0);

                inputSteam.Close();
                compressedStream.Close();

                // 写入压缩
                return ms.ToArray();
            }
        }

        /// <summary>
        /// 使用gzip方式压缩数据
        /// </summary>
        /// <param name="fileName">要压缩的文件全名（含路径）</param>
        /// <returns>返回压缩后的字节数据</returns>
        public static byte[] GZipCompress(string fileName)
        {
            //压缩后的MemoryStream
            using (MemoryStream ms = new MemoryStream())
            {
                // 写入压缩
                GZipStream compressedStream = new GZipStream(ms, CompressionMode.Compress, true);
                FileStream fs = new FileStream(fileName, FileMode.Open);
                byte[] buf = new byte[1024];
                int count = 0;
                do
                {
                    count = fs.Read(buf, 0, buf.Length);
                    compressedStream.Write(buf, 0, count);
                }
                while (count > 0);

                fs.Close();
                compressedStream.Close();
                return ms.ToArray();
            }

        }
    }
}
