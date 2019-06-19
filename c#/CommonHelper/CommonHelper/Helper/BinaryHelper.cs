using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper.Helper
{
    /// <summary>
    /// 二值化帮助类
    /// </summary>
    public class BinaryHelper
    {
        /// <summary>
        /// 获取灰度图的灰度直方图
        /// </summary>
        /// <param name="bitmap">灰度图</param>
        /// <returns>返回灰度直方</returns>
        public static int[] GrayRect(Bitmap bitmap)
        {
            int[] grayRect = new int[256];
            for (int i = 0, len_i = bitmap.Width; i < len_i; i++)
                for (int j = 0, len_j = bitmap.Height; j < len_j; j++)
                    grayRect[bitmap.GetPixel(i, j).R]++;
            return grayRect;
        }

        /// <summary>
        /// 获取灰度图的灰度直方图
        /// </summary>
        /// <param name="image">灰度图</param>
        /// <returns>返回灰度直方</returns>
        public static int[] GrayRect(Image image)
        {
            using (Bitmap bitmap = new Bitmap(image))
                return GrayRect(bitmap);
        }

        /// <summary>
        /// 获取灰度矩阵的灰度直方图
        /// </summary>
        /// <param name="grayMatrix">灰度矩阵</param>
        /// <returns>返回灰度直方</returns>
        public static int[] GrayRect(byte[,] grayMatrix)
        {
            int[] grayRect = new int[256];
            for (int x = 0,lenX= grayMatrix.GetLength(1); x < lenX; x++)
                for (int y = 0,lenY= grayMatrix.GetLength(0); y < lenY; y++)
                    grayRect[grayMatrix[y,x]]++;
            return grayRect;
        }

        /// <summary>
        /// 根据全局的平均灰度确定二值化的阈值。
        /// </summary>
        /// <param name="histGram">灰度直方图</param>
        /// <returns>返回二值化的阈值</returns>
        public static int GetMeanThreshold(int[] histGram)
        {
            int Sum = 0, Amount = 0;
            for (int Y = 0; Y < 256; Y++)
            {
                Amount += histGram[Y];
                Sum += Y * histGram[Y];
            }
            return Sum / Amount;
        }

        /// <summary>
        /// 使用固定阈值二值化
        /// </summary>
        /// <param name="matrix">灰度矩阵</param>
        /// <param name="threshold">固定的阈值</param>
        /// <returns>返回二值化矩阵</returns>
        public static byte[,] GetBinaryMatrix(byte[,] matrix,int threshold)
        {
            byte[,] binaryMatrix = new byte[matrix.GetLength(0), matrix.GetLength(1)];
            for (int x = 0, lenX = matrix.GetLength(1); x < lenX; x++)
                for (int y = 0, lenY = matrix.GetLength(0); y < lenY; y++)
                    binaryMatrix[y, x] = (byte)(matrix[y, x]>threshold?255:0);
            return binaryMatrix;
        }

        /// <summary>
        /// 根据灰度占比大于一个阈值时确定二值化的阈值
        /// </summary>
        /// <param name="histGram">灰度直方图</param>
        /// <param name="tile">百分比阈值</param>
        /// <returns>返回二值化的阈值</returns>
        public static int GetPTileThreshold(int[] histGram, int tile = 50)
        {
            int Y, Amount = 0, Sum = 0;
            for (Y = 0; Y < 256; Y++) Amount += histGram[Y];        //  像素总数
            for (Y = 0; Y < 256; Y++)
            {
                Sum = Sum + histGram[Y];
                if (Sum >= Amount * tile / 100) return Y;
            }
            return -1;
        }

        /// <summary>
        /// 基于谷底最小值的灰度确定二值化的阈值
        /// </summary>
        /// <param name="histGram">灰度直方图</param>
        /// <returns>返回二值化阈值</returns>
        public static int GetIntermodesThreshold(int[] histGram)
        {
            int Y, Iter = 0, Index;
            double[] HistGramC = new double[256];           // 基于精度问题，一定要用浮点数来处理，否则得不到正确的结果
            double[] HistGramCC = new double[256];          // 求均值的过程会破坏前面的数据，因此需要两份数据
            for (Y = 0; Y < 256; Y++)
            {
                HistGramC[Y] = histGram[Y];
                HistGramCC[Y] = histGram[Y];
            }
            // 通过三点求均值来平滑直方图
            while (IsDimodal(HistGramCC) == false)
            {                                               // 判断是否已经是双峰的图像了      
                HistGramCC[0] = (HistGramC[0] + HistGramC[0] + HistGramC[1]) / 3;                   // 第一点
                for (Y = 1; Y < 255; Y++)
                    HistGramCC[Y] = (HistGramC[Y - 1] + HistGramC[Y] + HistGramC[Y + 1]) / 3;       // 中间的点
                HistGramCC[255] = (HistGramC[254] + HistGramC[255] + HistGramC[255]) / 3;           // 最后一点
                System.Buffer.BlockCopy(HistGramCC, 0, HistGramC, 0, 256 * sizeof(double));         // 备份数据，为下一次迭代做准备
                Iter++;
                if (Iter >= 10000) return -1;                                                       // 似乎直方图无法平滑为双峰的，返回错误代码
            }
            // 阈值为两峰值的平均值
            int[] Peak = new int[2];
            for (Y = 1, Index = 0; Y < 255; Y++)
                if (HistGramCC[Y - 1] < HistGramCC[Y] && HistGramCC[Y + 1] < HistGramCC[Y]) Peak[Index++] = Y - 1;
            return ((Peak[0] + Peak[1]) / 2);
        }
        static bool IsDimodal(double[] HistGram)
        {
            int Count = 0;
            for (int Y = 1; Y < 255; Y++)
            {
                if (HistGram[Y - 1] < HistGram[Y] && HistGram[Y + 1] < HistGram[Y])
                {
                    Count++;
                    if (Count > 2) return false;
                }
            }
            return Count == 2;
        }

        /// <summary>
        /// 基于双峰的平均值确定二值化的阈值
        /// </summary>
        /// <param name="histGram">灰度直方图</param>
        /// <returns>返回双峰平均值</returns>
        public static int GetTwoPeaksAvgThreshold(int[] histGram)
        {
            int Y, Iter = 0, Index;
            double[] HistGramC = new double[256];           // 基于精度问题，一定要用浮点数来处理，否则得不到正确的结果
            double[] HistGramCC = new double[256];          // 求均值的过程会破坏前面的数据，因此需要两份数据
            for (Y = 0; Y < 256; Y++)
            {
                HistGramC[Y] = histGram[Y];
                HistGramCC[Y] = histGram[Y];
            }
            // 通过三点求均值来平滑直方图
            while (IsDimodal(HistGramCC) == false)
            {                                               // 判断是否已经是双峰的图像了      
                HistGramCC[0] = (HistGramC[0] + HistGramC[0] + HistGramC[1]) / 3;                   // 第一点
                for (Y = 1; Y < 255; Y++)
                    HistGramCC[Y] = (HistGramC[Y - 1] + HistGramC[Y] + HistGramC[Y + 1]) / 3;       // 中间的点
                HistGramCC[255] = (HistGramC[254] + HistGramC[255] + HistGramC[255]) / 3;           // 最后一点
                System.Buffer.BlockCopy(HistGramCC, 0, HistGramC, 0, 256 * sizeof(double));         // 备份数据，为下一次迭代做准备
                Iter++;
                if (Iter >= 10000) return -1;                                                       // 似乎直方图无法平滑为双峰的，返回错误代码
            }
            // 阈值为两峰值的平均值
            int[] Peak = new int[2];
            for (Y = 1, Index = 0; Y < 255; Y++)
                if (HistGramCC[Y - 1] < HistGramCC[Y] && HistGramCC[Y + 1] < HistGramCC[Y]) Peak[Index++] = Y - 1;
            return ((Peak[0] + Peak[1]) / 2);
        }
    }
}
