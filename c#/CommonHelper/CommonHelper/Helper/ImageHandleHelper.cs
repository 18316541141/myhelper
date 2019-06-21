using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;
namespace CommonHelper.Helper
{
    /// <summary>
    /// 图像处理帮助类
    /// </summary>
    public static class ImageHandleHelper
    {
        /// <summary>
        /// 按照切点进行切图，按行切割
        /// </summary>
        /// <param name="bitmap">原图</param>
        /// <param name="cutPoints">切点</param>
        /// <param name="autoDispose">是否自动回收</param>
        /// <returns>返回切割后图片</returns>
        public static Bitmap[] CutPicByRow(Image img, int[] cutPoints, bool autoDispose = true)
        {
            Bitmap bitmap = null;
            try
            {
                return CutPicByRow(bitmap = new Bitmap(img), cutPoints, autoDispose);
            }
            finally
            {
                if (autoDispose)
                {
                    using (img) { }
                    using (bitmap) { }
                }
            }
        }


        /// <summary>
        /// 按照切点进行切图，按行切割
        /// </summary>
        /// <param name="bitmap">原图</param>
        /// <param name="cutPoints">切点</param>
        /// <param name="autoDispose">是否自动回收</param>
        /// <returns>返回切割后图片</returns>
        public static Bitmap[] CutPicByRow(Bitmap bitmap, int[] cutPoints, bool autoDispose = true)
        {
            int[] newCutPoints = new int[cutPoints.Length + 2];
            newCutPoints[0] = 0;
            newCutPoints[newCutPoints.Length - 1] = bitmap.Width;
            Array.Copy(cutPoints, 0, newCutPoints, 1, cutPoints.Length);
            Rectangle cutRect;
            Bitmap[] ret = new Bitmap[cutPoints.Length + 1];
            for (int i = 0, len = newCutPoints.Length - 1; i < len; i++)
            {
                cutRect = new Rectangle(0, newCutPoints[i],bitmap.Width , newCutPoints[i + 1] - newCutPoints[i]);
                ret[i] = CutPicByRect(bitmap, cutRect, false);
            }
            if (autoDispose)
            {
                using (bitmap) { }
            }
            return ret;
        }

        /// <summary>
        /// 按照切点进行切图，按列切割
        /// </summary>
        /// <param name="img">原图</param>
        /// <param name="cutPoints">切点</param>
        /// <param name="autoDispose">是否自动回收</param>
        /// <returns>返回切割后图片</returns>
        public static Bitmap[] CutPicByCol(Image img, int[] cutPoints, bool autoDispose = true)
        {
            Bitmap bitmap = null;
            try
            {
                return CutPicByCol(bitmap = new Bitmap(img), cutPoints, autoDispose);
            }
            finally
            {
                if (autoDispose)
                {
                    using (img) { }
                    using (bitmap) { }
                }
            }
        }

        /// <summary>
        /// 按照切点进行切图，按列切割
        /// </summary>
        /// <param name="bitmap">原图</param>
        /// <param name="cutPoints">切点</param>
        /// <param name="autoDispose">是否自动回收</param>
        /// <returns>返回切割后图片</returns>
        public static Bitmap[] CutPicByCol(Bitmap bitmap, int[] cutPoints, bool autoDispose = true)
        {
            int[] newCutPoints = new int[cutPoints.Length + 2];
            newCutPoints[0] = 0;
            newCutPoints[newCutPoints.Length - 1] = bitmap.Width;
            Array.Copy(cutPoints, 0, newCutPoints, 1, cutPoints.Length);
            Rectangle cutRect;
            Bitmap[] ret = new Bitmap[cutPoints.Length + 1];
            for (int i = 0, len = newCutPoints.Length - 1; i < len; i++)
            {
                cutRect = new Rectangle(newCutPoints[i], 0, newCutPoints[i + 1] - newCutPoints[i], bitmap.Height);
                ret[i] = CutPicByRect(bitmap, cutRect, false);
            }
            if (autoDispose)
            {
                using (bitmap) { }
            }
            return ret;
        }

        /// <summary>
        /// 根据矩形范围，截取部分图片
        /// </summary>
        /// <param name="bitmap">原图</param>
        /// <param name="cutRect">截取范围</param>
        /// <returns>返回截取后的图片</returns>
        public static Bitmap CutPicByRect(Bitmap bitmap, Rectangle cutRect, bool autoDispose = true)
        {
            Bitmap cutRet = new Bitmap(cutRect.Width, cutRect.Height);
            using (Graphics g = Graphics.FromImage(cutRet))
                g.DrawImage(bitmap, 0 - cutRect.X, 0 - cutRect.Y, bitmap.Width, bitmap.Height);
            if (autoDispose)
            {
                using (bitmap) { }
            }
            return cutRet;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="autoDispose"></param>
        /// <returns></returns>
        public static Bitmap CutPicByRect(Bitmap bitmap,int x,int y,int width,int height, bool autoDispose = true)
        {
            return CutPicByRect(bitmap,new Rectangle(x, y, width, height), autoDispose);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="autoDispose"></param>
        /// <returns></returns>
        public static Image CutPicByRect(Image image, int x, int y, int width, int height, bool autoDispose = true)
        {
            return CutPicByRect(image, new Rectangle(x, y, width, height), autoDispose);
        }

        /// <summary>
        /// 根据矩形范围，截取部分图片
        /// </summary>
        /// <param name="img">原图</param>
        /// <param name="cutRect">截取范围</param>
        /// <returns>返回截取后的图片</returns>
        public static Image CutPicByRect(Image img, Rectangle cutRect, bool autoDispose = true)
        {
            Bitmap bitmap = null;
            try
            {
                return CutPicByRect(bitmap = new Bitmap(img), cutRect, autoDispose);
            }
            finally
            {
                if (autoDispose)
                {
                    using (img) { }
                    using (bitmap) { }
                }
            }
        }

        /// <summary>
        /// 屏幕截图
        /// </summary>
        /// <param name="x">x坐标</param>
        /// <param name="y">y坐标</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns>返回截取的图片</returns>
        public static Bitmap CopyScreen(int x, int y, int width, int height) =>
            CopyScreen(new Rectangle(x, y, width, height));

        /// <summary>
        /// 屏幕截图
        /// </summary>
        /// <param name="rect">截图范围</param>
        /// <returns>返回截取的图片</returns>
        public static Bitmap CopyScreen(Rectangle rect)
        {
            Bitmap bitmap = new Bitmap(rect.Width, rect.Height, PixelFormat.Format24bppRgb);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.CopyFromScreen(rect.X, rect.Y, 0, 0, rect.Size);
                g.Dispose();
            }
            GC.Collect();
            return bitmap;
        }

        /// <summary>
        /// 全屏幕截图
        /// </summary>
        /// <returns>返回全屏幕截图</returns>
        public static Bitmap CopyFullScreen() => CopyScreen(new Rectangle(0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height));

        /// <summary>
        /// 默认的腐蚀核
        /// </summary>
        public static int[,] DefaultCorrosionCore
        {
            set { }
            get
            {
                return new int[,]
                {
                    {-1,-1 }, {0,-1 }, {1,-1 },
                    {-1,0 }, { 0,0} ,{1,0},
                    {-1,1 }, {0,1 }, {1,1},
                };
            }
        }

        /// <summary>
        /// 默认的膨胀核
        /// </summary>
        public static int[,] DefaultExpandCode
        {
            set { }
            get
            {
                return new int[,]
                {
                    {-1,-1 }, {0,-1 }, {1,-1 },
                    {-1,0 }, { 0,0} ,{1,0},
                    {-1,1 }, {0,1 }, {1,1},
                };
            }
        }

        /// <summary>
        /// 二值图的图像膨胀
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static byte[,] PicExpand(int[,] matrix)
        {
            //膨胀核
            int[,] core = DefaultExpandCode;
            byte[,] newMatrix = new byte[matrix.GetLength(0), matrix.GetLength(1)];
            for (int x = 0, len_x = matrix.GetLength(1); x < len_x; x++)
                for (int y = 0, len_y = matrix.GetLength(0); y < len_y; y++)
                {
                    if (matrix[y, x] == 1)
                    {
                        for (int i = 0, len_i = core.GetLength(0); i < len_i; i++)
                            if (x + core[i, 0] < len_x && x + core[i, 0] > -1 && y + core[i, 1] < len_y && y + core[i, 1] > -1)
                                newMatrix[y + core[i, 1], x + core[i, 0]] = 0;
                    }
                }
            return newMatrix;
        }

        /// <summary>
        /// 二值图的图像腐蚀
        /// </summary>
        /// <returns></returns>
        public static byte[,] PicCorrosion(byte[,] matrix)
        {
            int[,] core = DefaultCorrosionCore;
            byte[,] newMatrix = new byte[matrix.GetLength(0), matrix.GetLength(1)];
            for (int x = 0, len_x = matrix.GetLength(1); x < len_x; x++)
                for (int y = 0, len_y = matrix.GetLength(0); y < len_y; y++)
                {
                    int k = 0;
                    for (int i = 0, len_i = core.GetLength(0); i < len_i; i++)
                        if (x + core[i, 0] < len_x && x + core[i, 0] > -1 && y + core[i, 1] < len_y && y + core[i, 1] > -1 && matrix[y + core[i, 1], x + core[i, 0]] == 255)
                            k++;
                    newMatrix[y, x] = (byte)(k == core.GetLength(0) ? 255 : 0);
                }
            return newMatrix;
        }

        /// <summary>
        /// 识别图片验证码，并返回图片验证码内容
        /// </summary>
        /// <param name="codeImage">图片验证码</param>
        /// <param name="type">识别的类型：1.纯数字 2.纯英文 3.英文数字混合</param>
        /// <param name="figures">识别的位数，0表示不确定、1表示1位、2表示2为。。。如此类推</param>
        /// <returns>返回图片验证码内容</returns>
        public static string VerifyCodeRecognise(Image codeImage, int type = 3, int figures = 4)
        {
            string serviceURL = "http://183.2.233.247:5018/RecognitionService";
            string result = "";
            string base64string = "";
            ImageFormat format = codeImage.RawFormat;
            using (MemoryStream ms1 = new MemoryStream())
            {
                codeImage.Save(ms1, ImageFormat.Png);
                byte[] buffer = new byte[ms1.Length];
                ms1.Seek(0, SeekOrigin.Begin);
                ms1.Read(buffer, 0, buffer.Length);
                base64string = Convert.ToBase64String(buffer);
            }
            DataContractJsonSerializer obj = new DataContractJsonSerializer(typeof(string));
            MemoryStream ms = new MemoryStream();
            //34中的3表示（3.英文数字混合 2.纯英文 1.纯数字）
            obj.WriteObject(ms, $"{base64string}|{type}{figures}"); // + "|34"  30表示不确定验证码位数（慢） 31表示1位 32表示2位 33表示3位 34表示4位 默认35（5位）...39表示9位 
            byte[] byteSend = ms.ToArray();
            ms.Close();
            //定义传输
            WebClient test = new WebClient();
            test.Headers.Add("Content-Type", "application/json");
            test.Headers.Add("ContentLength", byteSend.Length.ToString());
            //提交
            byte[] responseData = test.UploadData(serviceURL, "POST", byteSend);
            //回传
            if (responseData == null) return result;
            result = Encoding.GetEncoding("UTF-8").GetString(responseData).Replace("\"", "");
            return result;
        }

        /// <summary>
        /// 心理学家认为，人类看一样东西的时候绿色占比是最多的，
        /// 绿色约占了58.7%、红色占29.9%、蓝色占11.4%，按照这个
        /// 比例去设置灰度图则可以保证保留更多的图像信息
        /// </summary>
        /// <param name="rgbMatrix">图片的rgb矩阵</param>
        /// <returns>返回图片的灰度矩阵</returns>
        public static byte[,] ToGrayByPsy(this byte[,,] rgbMatrix)
        {
            byte[,] grayMatrix = new byte[rgbMatrix.GetLength(0), rgbMatrix.GetLength(1)];
            byte gray;
            for (int i = 0, len_i = rgbMatrix.GetLength(0); i < len_i; i++)
                for (int Sum, j = 0, len_j = rgbMatrix.GetLength(1); j < len_j; j++)
                {
                    Sum = rgbMatrix[i, j, 0] * 299 + rgbMatrix[i, j, 1] * 587 + rgbMatrix[i, j, 2] * 114;
                    gray = (byte)(Sum % 1000 == 0 ? Sum / 1000 : Sum / 1000 + 1);
                    grayMatrix[i, j] = gray;
                }
            return grayMatrix;
        }

        /// <summary>
        /// 心理学家认为，人类看一样东西的时候绿色占比是最多的，
        /// 绿色约占了58.7%、红色占29.9%、蓝色占11.4%，按照这个
        /// 比例去设置灰度图则可以保证保留更多的图像信息
        /// </summary>
        /// <param name="image">彩色图</param>
        /// <returns>灰度图矩阵</returns>
        public static byte[,] ToGrayByPsy(this Image image)
        {
            using (Bitmap bitmap = new Bitmap(image))
                return ToGrayByPsy(bitmap);
        }

        /// <summary>
        /// 心理学家认为，人类看一样东西的时候绿色占比是最多的，
        /// 绿色约占了58.7%、红色占29.9%、蓝色占11.4%，按照这个
        /// 比例去设置灰度图则可以保证保留更多的图像信息
        /// </summary>
        /// <param name="Bitmap">彩色图</param>
        /// <returns>灰度图矩阵</returns>
        public static byte[,] ToGrayByPsy(this Bitmap bitmap)
        {
            byte[,] grayMatrix = new byte[bitmap.Height, bitmap.Width];
            Color Color;
            byte gray;
            for (int i = 0, len_i = bitmap.Width; i < len_i; i++)
                for (int Sum, j = 0, len_j = bitmap.Height; j < len_j; j++)
                {
                    Color = bitmap.GetPixel(i, j);
                    Sum = Color.R * 299 + Color.G * 587 + Color.B * 114;
                    gray = (byte)(Sum % 1000 == 0 ? Sum / 1000 : Sum / 1000 + 1);
                    grayMatrix[j, i] = gray;
                }
            return grayMatrix;
        }

        /// <summary>
        /// 获取矩阵的子矩阵
        /// </summary>
        /// <param name="matrix">源矩阵</param>
        /// <param name="startX">起始x坐标（包含）</param>
        /// <param name="startY">起始y坐标（包含）</param>
        /// <param name="endX">结束x坐标（不包含）</param>
        /// <param name="endY">结束y坐标（不包含）</param>
        /// <returns>返回子矩阵</returns>
        public static byte[,] GetSubMatrix(this byte[,] matrix, int startX, int startY, int endX, int endY)
        {
            byte[,] subMatrix = new byte[endY - startY, endX - startX];
            for (int x = startX; x < endX; x++)
                for (int y = startY; y < endY; y++)
                    subMatrix[y - startY, x - startX] = matrix[y, x];
            return subMatrix;
        }

        /// <summary>
        /// 灰度矩阵或二值化矩阵转为图片对象Bitmap
        /// </summary>
        /// <param name="matrix">灰度矩阵或二值化矩阵</param>
        /// <returns></returns>
        public static Bitmap MatrixToImg(byte[,] matrix)
        {
            Bitmap bitmap = new Bitmap(matrix.GetLength(1), matrix.GetLength(0));
            for (int x = 0, lenX = matrix.GetLength(1); x < lenX; x++)
                for (int y = 0, lenY = matrix.GetLength(0); y < lenY; y++)
                    bitmap.SetPixel(x, y, Color.FromArgb(matrix[y, x], matrix[y, x], matrix[y, x]));
            return bitmap;
        }

        public static Bitmap GetLbpImage(this byte[,] grayMatrix)
        {
            Bitmap bitmap = new Bitmap(grayMatrix.GetLength(1), grayMatrix.GetLength(0));
            //lbp核
            int[,] lbpCore = new int[,] {
                {-1,-1 }, {0,-1 }, {1,-1 },
                {-1,0 },           {1,0 },
                {-1,1 },  {0,1 },  {1,1 }
            };
            for (int x = 1, lenX = grayMatrix.GetLength(1) - 1; x < lenX; x++)
                for (int y = 1, lenY = grayMatrix.GetLength(0) - 1; y < lenY; y++)
                {
                    int tempVn = 0;
                    for (int lc = 0; lc < 8; lc++)
                        if (grayMatrix[y + lbpCore[lc, 1], x + lbpCore[lc, 0]] > grayMatrix[y, x])
                            tempVn += 1 << (7 - lc);
                    int minVn = tempVn;
                    for (int i = 0; i < 8; i++)
                    {
                        tempVn = ((85 & 0x01) << 7) | (tempVn >> 1 & 0x7f);
                        if (tempVn < minVn) minVn = tempVn;
                    }
                    bitmap.SetPixel(x, y, Color.FromArgb(minVn, minVn, minVn));
                }
            return bitmap;
        }

        /// <summary>
        /// 使用Lbp获取图像的特征向量。并对lbp进行等价处理
        /// </summary>
        /// <param name="grayMatrix">灰度矩阵</param>
        /// <returns>返回特征向量</returns>
        public static double[,] GetVectorByLbp(this byte[,] grayMatrix)
        {
            //lbp核
            int[,] lbpCore = new int[,] {
                {-1,-1 }, {0,-1 }, {1,-1 },
                {-1,0 },           {1,0 },
                {-1,1 },  {0,1 },  {1,1 }
            };
            HashSet<int> allVn = new HashSet<int>();
            for (int addIndex = 0, tempNum = 0; addIndex < 8; addIndex++)
            {
                tempNum += 1 << addIndex;
                for (int moveIndex = 0; moveIndex < 8 - addIndex; moveIndex++)
                {
                    if (tempNum << moveIndex != 0x0) allVn.Add(tempNum << moveIndex);
                    if ((~(tempNum << moveIndex) & 0xff) != 0x0) allVn.Add(~(tempNum << moveIndex) & 0xff);
                }
            }
            List<int> allVnList = new List<int>(allVn);

            double[,] vector = new double[allVnList.Count, 1];
            for (int x = 1, lenX = grayMatrix.GetLength(1) - 1; x < lenX; x++)
                for (int y = 1, lenY = grayMatrix.GetLength(0) - 1; y < lenY; y++)
                {
                    int tempVn = 0;
                    for (int lc = 0; lc < 8; lc++)
                        if (grayMatrix[y + lbpCore[lc, 1], x + lbpCore[lc, 0]] > grayMatrix[y, x])
                            tempVn += 1 << (7 - lc);
                    int minVn = tempVn;
                    for (int i = 0; i < 8; i++)
                    {
                        tempVn = ((85 & 0x01) << 7) | (tempVn >> 1 & 0x7f);
                        if (tempVn < minVn) minVn = tempVn;
                    }
                    if (allVnList.Contains(minVn))
                        vector[allVnList.IndexOf(minVn), 0]++;
                }
            return vector;
        }
    }
}