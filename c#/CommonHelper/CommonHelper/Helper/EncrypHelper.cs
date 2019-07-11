using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper.Helper
{
    /// <summary>
    /// 加密帮助类
    /// </summary>
    public class EncrypHelper
    {
        /// <summary>
        /// 读取指定路径的图片，返回该图片的base64
        /// </summary>
        /// <param name="imgFilePath">图片路径</param>
        /// <returns>返回图片的base64</returns>
        public static string ImgFileToBase64(string imgFilePath)
        {
            using (Image image = Image.FromFile(imgFilePath))
                return ImageToBase64(image);
        }

        /// <summary>
        /// 图片的Image对象转base64
        /// </summary>
        /// <param name="image">图片的Image对象</param>
        /// <returns>返回图片的base64</returns>
        public static string ImageToBase64(Image image)
        {
            using (Bitmap bitmap = new Bitmap(image))
                return BitmapToBase64(bitmap);
        }

        /// <summary>
        /// 图片的Bitmap对象转base64
        /// </summary>
        /// <param name="bitmap">图片的Bitmap对象</param>
        /// <returns>返回图片的base64</returns>
        public static string BitmapToBase64(Bitmap bitmap)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Jpeg);
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        /// <summary>
        /// 图片输入流转base64
        /// </summary>
        /// <param name="inputStream"></param>
        /// <returns></returns>
        public static string ImgStreamToBase64(Stream inputStream)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                StreamHelper.FastCopyStream(inputStream, memoryStream, false);
                return Convert.ToBase64String(memoryStream.ToArray());
            }
        }

        /// <summary>
        /// 使用MD5对文本加密
        /// </summary>
        /// <param name="input">原文</param>
        /// <returns>密文</returns>
        public static string GetMD5FromStr(string input)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] data = Encoding.UTF8.GetBytes(input);
            byte[] md5data = md5.ComputeHash(data);
            md5.Clear();
            string str = "";
            for (int i = 0; i < md5data.Length; i++)
                str += md5data[i].ToString("x").PadLeft(2, '0');
            return str;
        }

        /// <summary>
        /// byte数组转为md5字符串
        /// </summary>
        /// <param name="bytedata"></param>
        /// <returns></returns>
        public static string GetMD5HashFromByte(byte[] byteData)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(byteData);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 图片对象转md5字符串
        /// </summary>
        /// <param name="img">图片对象</param>
        /// <returns></returns>
        public static string GetMD5HashFromImg(Image img)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                img.Save(memoryStream, ImageFormat.Bmp);
                return GetMD5HashFromByte(memoryStream.ToArray());
            }
        }

        /// <summary>
        /// 获取文件的MD5
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetMD5HashFromFile(string fileName)
        {
            try
            {
                using (FileStream file = new FileStream(fileName, FileMode.Open))
                using (BufferedStream buffStream = new BufferedStream(file))
                {
                    MD5 md5 = new MD5CryptoServiceProvider();
                    byte[] retVal = md5.ComputeHash(buffStream);
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < retVal.Length; i++)
                        sb.Append(retVal[i].ToString("x2"));
                    return sb.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("GetMD5HashFromFile() fail,error:" + ex.Message);
            }
        }

        /// <summary>
        /// Sha1加密
        /// </summary>
        /// <param name="input">原文</param>
        /// <returns>密文</returns>
        public static string GetSha1FromStr(string input)
        {
            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
            byte[] str1 = Encoding.UTF8.GetBytes(input);
            byte[] str2 = sha1.ComputeHash(str1);
            sha1.Clear();
            (sha1 as IDisposable).Dispose();
            StringBuilder sc = new StringBuilder();
            for (int i = 0; i < str2.Length; i++)
                sc.Append(str2[i].ToString("x2"));
            return sc.ToString();
        }

        /// <summary>
        /// 获取图片对象的sha1值
        /// </summary>
        /// <param name="img">图片对象</param>
        /// <returns>返回sha1值</returns>
        public static string GetSha1FromImage(Image img)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                img.Save(memoryStream, ImageFormat.Bmp);
                return GetSha1FromByte(memoryStream.ToArray());
            }
        }

        /// <summary>
        /// 获取byte数组的sha1值
        /// </summary>
        /// <param name="data">byte数组</param>
        /// <returns>返回sha1值</returns>
        public static string GetSha1FromByte(byte[] data)
        {
            try
            {
                SHA1 sha1 = new SHA1CryptoServiceProvider();
                byte[] retval = sha1.ComputeHash(data);
                StringBuilder sc = new StringBuilder();
                for (int i = 0; i < retval.Length; i++)
                    sc.Append(retval[i].ToString("x2"));
                return sc.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("GetMD5HashFromFile() fail,error:" + ex.Message);
            }
        }

        /// <summary>
        /// 获取该路径文件的sha1值
        /// </summary>
        /// <param name="filePath">指定路径的文件</param>
        /// <returns>返回sha1</returns>
        public static string GetSha1FromFilePath(string filePath)
        {
            using (FileStream stream = File.OpenRead(filePath))
                return GetSha1FromStream(stream);
        }

        /// <summary>
        /// 获取该数据流的sha1值
        /// </summary>
        /// <param name="stream">数据流</param>
        /// <returns>返回sha1值</returns>
        public static string GetSha1FromStream(Stream stream)
        {
            try
            {
                SHA1 sha1 = new SHA1CryptoServiceProvider();
                byte[] retval = sha1.ComputeHash(stream);
                StringBuilder sc = new StringBuilder();
                for (int i = 0; i < retval.Length; i++)
                    sc.Append(retval[i].ToString("x2"));
                return sc.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("GetMD5HashFromFile() fail,error:" + ex.Message);
            }
        }

        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="text">要加密的内容</param>
        /// <param name="key">密钥</param>
        /// <returns>返回加密后的字符串</returns>
        public static string DesEncrypt(string text, string key)
        {
            //访问数据加密标准(DES)算法的加密服务提供程序 (CSP) 版本的包装对象
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            des.Mode = CipherMode.ECB;
            des.Padding = PaddingMode.PKCS7;
            des.Key = Encoding.UTF8.GetBytes(key); //建立加密对象的密钥和偏移量
            des.IV = Encoding.UTF8.GetBytes(key);  //原文使用ASCIIEncoding.ASCII方法的GetBytes方法  |||  如果是用ECB模式，则IV不管是什么都不会影响加密/解密的结果

            byte[] inputByteArray = Encoding.UTF8.GetBytes(text);//把字符串放到byte数组中

            MemoryStream ms = new MemoryStream();//创建其支持存储区为内存的流　
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);//定义将数据流链接到加密转换的流
            cs.Write(inputByteArray, 0, inputByteArray.Length);//上面已经完成了把加密后的结果放到内存中去
            cs.FlushFinalBlock();

            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }

        /// <summary>
        /// DES 解密过程
        /// </summary>
        /// <param name="text">被解密的字符串</param>
        /// <param name="key">密钥（只支持8个字节的密钥，同前面的加密密钥相同）</param>
        /// <returns>返回被解密的字符串</returns>
        public static string DesDecrypt(string text, string key)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            des.Mode = CipherMode.ECB;
            des.Padding = PaddingMode.PKCS7;
            byte[] inputByteArray = new byte[text.Length / 2];
            for (int x = 0; x < text.Length / 2; x++)
            {
                int i = (Convert.ToInt32(text.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }

            des.Key = Encoding.UTF8.GetBytes(key); //建立加密对象的密钥和偏移量，此值重要，不能修改
                                                   //des.IV = Encoding.UTF8.GetBytes(sKey);// 如果是用ECB模式，则IV不管是什么都不会影响加密/解密的结果
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            return System.Text.Encoding.UTF8.GetString(ms.ToArray());
        }
    }
}
