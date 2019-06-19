using CommonHelper.Helper;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace OpenCvHelper.ImageOper
{
    public static class OpencvImageHelper
    {

        /// <summary>
        /// 检查当前微信app页是否在变化。如果检查多次界面一样时，
        /// 认为界面不动，如果有任意两次不一样，则认为界面是动的
        /// </summary>
        /// <param name="CheckRange">检查范围</param>
        /// <param name="Times">检查次数</param>
        /// <returns>返回true，界面不动；返回false，界面在动</returns>
        public static bool CheckChange(Rectangle CheckRange, int Times = 8)
        {
            int SameTimes = 0;
            using (Bitmap Before = ImageHandleHelper.CopyScreen(CheckRange))
            {
                for (int i = 0,x,y; i < Times; i++)
                {
                    Thread.Sleep(12);
                    Bitmap After = ImageHandleHelper.CopyScreen(CheckRange);
                    if (After.OCRImages(Before, out x, out y)) SameTimes++;
                    else return false;
                }
            }
            return SameTimes == Times;
        }

        /// <summary>
        /// 从大图中寻找小图片
        /// </summary>
        /// <param name="Src">大图片</param>
        /// <param name="Target">小图片</param>
        /// <param name="x">小图片在大图片的位置x坐标</param>
        /// <param name="y">小图片在大图片的位置y坐标</param>
        /// <param name="SrcAutoDispose">是否自动释放大图</param>
        /// <param name="ocr_rate">相似度</param>
        /// <returns>如果能找到则返回true</returns>
        public static bool OCRImages(this Bitmap Src, Bitmap Target, out int x, out int y, bool SrcAutoDispose = true, double ocr_rate = 0.8)
        {
            x = 0;
            y = 0;
            if (Src.Width < Target.Width || Src.Height < Target.Height)
                return false;
            if (Target == null)
                return false;
            Image<Bgr, byte> TemplateModel = null;
            Image<Bgr, byte> OrderModel = null;
            try
            {
                TemplateModel = new Image<Bgr, byte>(Target);
                OrderModel = new Image<Bgr, byte>(Src);
                Image<Gray, byte> TemplateModel2 = TemplateModel.Convert<Gray, byte>();
                Image<Gray, byte> OrderModel2 = OrderModel.Convert<Gray, byte>();
                Image<Gray, float> c = OrderModel2.MatchTemplate(TemplateModel2, TemplateMatchingType.CcoeffNormed);
                try
                {
                    double min = 0, max = 0;
                    Point maxp = new Point(0, 0);
                    Point minp = new Point(0, 0);
                    CvInvoke.MinMaxLoc(c, ref min, ref max, ref minp, ref maxp);
                    x = maxp.X;
                    y = maxp.Y;
                    return max >= ocr_rate;
                }
                finally
                {
                    TemplateModel2.Dispose();
                    OrderModel2.Dispose();
                    c.Dispose();
                }
            }
            catch (Exception ex)
            {
                return true;
            }
            finally
            {
                TemplateModel.Dispose();
                OrderModel.Dispose();
                if (SrcAutoDispose)
                    Src.Dispose();
            }
        }



        /// <summary>
        /// 观察某一个地方是否发生变化，如果发生变化则返回，
        /// 如果不发生变化则阻塞，直到真正发生变化或观察超时为止
        /// </summary>
        /// <param name="Before">变化前截图</param>
        /// <param name="Range">观察范围</param>
        /// <param name="AutoDispose">是否自动是否变化前截图</param>
        public static void ObserveChange(Bitmap Before, Rectangle Range, bool AutoDispose = true)
        {
            for (int i = 0,x,y; i < 1000; i++)
            {
                Thread.Sleep(10);
                if (!ImageHandleHelper.CopyScreen(Range).OCRImages(Before,out x,out y))
                {
                    if (AutoDispose)
                    {
                        using (Before) { }
                    }
                    return;
                }
            }
            throw new Exception("界面卡死！(ERROR:-999)");
        }

        /// <summary>
        /// 比较2图片差异性
        /// </summary>
        /// <param name="BitMap_baseImage"></param>
        /// <param name="BitMap_intPutImage"></param>
        /// <param name="AutoDispose">
        ///     是否自动回收图片，当AutoDispose的二进制是10：回收BitMap_baseImage，
        ///     01：回收BitMap_intPutImage，11：回收BitMap_baseImage、BitMap_intPutImage，
        ///     00：不回收
        /// </param>
        /// <returns>如果相同则返回0</returns>
        public static double Compare2Images(Bitmap BitMap_baseImage, Bitmap BitMap_intPutImage, int AutoDispose = 3)
        {
            Image<Bgr, byte> basei1 = new Image<Bgr, byte>(BitMap_baseImage);
            Image<Bgr, byte> basei2 = new Image<Bgr, byte>(BitMap_intPutImage);
            try
            {
                Mat baseImage = basei1.Mat;
                Mat inputImage = basei2.Mat;

                if (baseImage.IsEmpty | inputImage.IsEmpty | baseImage.NumberOfChannels != 3 | inputImage.NumberOfChannels != 3 | baseImage.Size != inputImage.Size | baseImage.Depth != inputImage.Depth)
                    return -1;

                ImageComparator ic = new ImageComparator();
                ic.setBaseImage(baseImage);
                ic.setInputImage(inputImage);
                ic.setNumberOfBins(8);
                return ic.calculateDifference();
            }
            finally
            {
                basei1.Dispose();
                basei2.Dispose();
                if ((AutoDispose >> 1) % 2 == 1)
                    BitMap_baseImage.Dispose();
                if (AutoDispose % 2 == 1)
                    BitMap_intPutImage.Dispose();
            }
        }
    }

    public class ImageComparator
    {
        private Mat baseImg;
        private Mat inputImg;
        int nBins;//每个颜色通道使用的箱子数量  
        public ImageComparator()
        {
            nBins = 8;//默认8个箱子  
        }
        public void setNumberOfBins(int n)
        {
            nBins = n;
        }
        public void setBaseImage(Mat img)
        {
            baseImg = img;
        }
        public void setInputImage(Mat img)
        {
            inputImg = img;
        }
        public double calculateDifference()//简化代码量，直接默认输入图像都是BGR三通道彩色图像  
        {
            DenseHistogram[] histogramOfBaseImage = new DenseHistogram[3];
            DenseHistogram[] histogramOfInputImage = new DenseHistogram[3];
            for (int i = 0; i < 3; i++)
            {
                histogramOfBaseImage[i] = new DenseHistogram(nBins, new RangeF(0f, 255.1f));
                histogramOfBaseImage[i].Calculate<byte>(new Image<Gray, byte>[] { baseImg.ToImage<Bgr, byte>().Split()[i] }, false, null);
                histogramOfInputImage[i] = new DenseHistogram(nBins, new RangeF(0f, 255.1f));
                histogramOfInputImage[i].Calculate<byte>(new Image<Gray, byte>[] { inputImg.ToImage<Bgr, byte>().Split()[i] }, false, null);
            }
            Image<Bgr, float> histOfBaseImg = new Image<Bgr, float>(nBins, 1);
            Image<Bgr, float> histOfInputImg = new Image<Bgr, float>(nBins, 1);
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < nBins; j++)
                {
                    histOfBaseImg.Data[0, j, i] = (float)histogramOfBaseImage[i].GetBinValues()[j];
                    histOfInputImg.Data[0, j, i] = (float)histogramOfInputImage[i].GetBinValues()[j];
                }
            return CvInvoke.CompareHist(histOfBaseImg, histOfInputImg, HistogramCompMethod.Bhattacharyya);//也可以不用巴氏系数，采用其他比较方法  
        }
    }
}
