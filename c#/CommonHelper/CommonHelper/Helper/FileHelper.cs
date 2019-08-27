using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;

namespace CommonHelper.Helper
{
    /// <summary>
    /// 文件帮助类
    /// </summary>
    public class FileHelper
    {
        /// <summary>
        /// 文件分级保存，如果文件夹中有大量文件，会影响读文件的速度，
        /// 所以需要对文件进行分组保存，默认情况下只有一级，
        /// 使用该方法会把文件保存变成多级。例如有这样的一个文件夹testDir：
        ///     testDir
        ///         file1.txt
        ///         file2.txt
        ///         image1.txt
        ///         image2.txt
        /// 通过该方法变成2级后testDir文件夹变成：
        ///     testDir
        ///         fi
        ///             file1.txt
        ///             file2.txt
        ///         im
        ///             image1.txt
        ///             image2.txt
        /// 统一都是取文件名前两位作为分组依据
        /// </summary>
        /// <param name="basePath"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public static string FileGroupSave(string basePath,int level)
        {

            return null;
        }

        /// <summary>
        /// 保存图片，并使用sha1给图片命名，最后返回图片的sha1，会释放图片
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="basePath"></param>
        /// <returns></returns>
        public static string SaveBitmapBySha1(Bitmap bitmap, string basePath)
        {
            Directory.CreateDirectory(basePath);
            string guid = Guid.NewGuid().ToString();
            using (bitmap)
            {
                using (MemoryStream outputStream = new MemoryStream())
                {
                    bitmap.Save(outputStream, ImageFormat.Jpeg);
                    using (MemoryStream inputStream = new MemoryStream(outputStream.ToArray()))
                    {
                        return SaveFileNameBySha1(inputStream, basePath);
                    }
                }
            }
        }

        /// <summary>
        /// 保存图片，并使用sha1给图片命名，最后返回图片的sha1，会释放图片
        /// </summary>
        /// <param name="image"></param>
        /// <param name="basePath"></param>
        /// <returns></returns>
        public static string SaveImageBySha1(Image image, string basePath)
        {
            using (Bitmap bitmap = new Bitmap(image))
            {
                return SaveBitmapBySha1(bitmap, basePath);
            }
        }

        /// <summary>
        /// 保存文件，并使用sha1给文件命名，最后返回文件的sha1，会关闭流
        /// </summary>
        /// <param name="inputStream">文件的输入流</param>
        /// <param name="basePath">文件夹位置</param>
        /// <returns>返回文件的sha1</returns>
        public static string SaveFileNameBySha1(Stream inputStream, string basePath)
        {
            Directory.CreateDirectory(basePath);
            string guid = Guid.NewGuid().ToString();
            StreamHelper.CopyStream(inputStream, File.OpenWrite(basePath + Path.DirectorySeparatorChar + guid));
            string SHA1;
            using (Stream guidStream= File.OpenRead(basePath + Path.DirectorySeparatorChar + guid))
            {
                SHA1 = EncrypHelper.GetSha1FromStream(guidStream);
            }
            using (Mutex mutex = new Mutex(false, SHA1))
            {
                mutex.WaitOne();
                if (File.Exists(basePath + Path.DirectorySeparatorChar + SHA1))
                {
                    try
                    {
                        File.Delete(basePath + Path.DirectorySeparatorChar + guid);
                    }
                    catch (Exception)
                    {

                    }
                }
                else
                {
                    File.Move(basePath + Path.DirectorySeparatorChar + guid, basePath + Path.DirectorySeparatorChar + SHA1);
                }
                mutex.ReleaseMutex();
            }
            return SHA1;
        }

        /// <summary>
        /// 保存文件，并使用md5给文件命名
        /// </summary>
        /// <param name="InputStream">文件的流</param>
        /// <param name="BasePath">保存文件的目录</param>
        /// <returns>返回文件的md5名称</returns>
        public static string SaveFileNameByMd5(Stream inputStream, string basePath)
        {
            Directory.CreateDirectory(basePath);
            string _Guid = Guid.NewGuid().ToString();
            StreamHelper.CopyStream(inputStream, File.OpenWrite(basePath + Path.DirectorySeparatorChar + _Guid));
            string MD5 = EncrypHelper.GetMD5HashFromFile(basePath + Path.DirectorySeparatorChar + _Guid);
            if (File.Exists(basePath + Path.DirectorySeparatorChar + MD5))
                File.Delete(basePath + Path.DirectorySeparatorChar + _Guid);
            else
                File.Move(basePath + Path.DirectorySeparatorChar + _Guid, basePath + Path.DirectorySeparatorChar + MD5);
            return MD5;
        }

        /// <summary>
        /// 保存文件，并使用sha1给文件命名
        /// </summary>
        /// <param name="FilePath"></param>
        /// <param name="BasePath"></param>
        /// <returns></returns>
        public static string SaveFileNameBySHA1(string filePath, string basePath)
        {
            Directory.CreateDirectory(basePath);
            string sha1 = EncrypHelper.GetSha1FromFilePath(filePath);
            if (File.Exists(basePath + Path.DirectorySeparatorChar + sha1))
                File.Delete(filePath);
            else
                File.Move(filePath, basePath + Path.DirectorySeparatorChar + sha1);
            return sha1;
        }

        /// <summary>
        /// 保存文件，并使用md5给文件命名，如果存在同名会删掉原来的文件。
        /// </summary>
        /// <param name="FilePath"></param>
        /// <param name="BasePath"></param>
        /// <returns></returns>
        public static string SaveFileNameByMd5(string filePath, string basePath)
        {
            Directory.CreateDirectory(basePath);
            string MD5 = EncrypHelper.GetMD5HashFromFile(filePath);
            if (File.Exists(basePath + Path.DirectorySeparatorChar + MD5))
                File.Delete(filePath);
            else
                File.Move(filePath, basePath + Path.DirectorySeparatorChar + MD5);
            return MD5;
        }

        /// <summary>
        /// 获取文件前四个字节，用于判断文件类型，并返回文件类型
        /// </summary>
        /// <param name="Path">文件路径</param>
        /// <returns>前两个字节拼接后的字符串</returns>
        public static FileTypeEnum GetFileType(string path)=>
            GetFileType(File.OpenRead(path));


        /// <summary>
        /// 获取文件前四个字节，用于判断文件类型，并返回文件类型
        /// </summary>
        /// <param name="inputStream">文件输入流</param>
        /// <returns>前两个字节拼接后的字符串</returns>
        public static FileTypeEnum GetFileType(Stream inputStream)
        {
            byte[] buff = new byte[4];
            using (BinaryReader reader = new BinaryReader(inputStream))
                reader.Read(buff, 0, buff.Length);
            int ret = (buff[3] << 24) | (buff[2] << 16) | (buff[1] << 8) | buff[0];
            foreach (FileTypeEnum fileTypeEnum in Enum.GetValues(typeof(FileTypeEnum)))
            {
                if ((long)fileTypeEnum == ret)
                    return fileTypeEnum;
            }
            return FileTypeEnum.OTHER;
        }
    }
}
