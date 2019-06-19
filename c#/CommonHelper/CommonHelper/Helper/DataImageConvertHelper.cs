using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using ZXing.QrCode.Internal;
namespace CommonHelper.Helper
{
    /// <summary>
    /// 数据和图片互转的帮助类
    /// </summary>
    public static class DataImageConvertHelper
    {
        /// <summary>
        /// 条形码图片转为文本
        /// </summary>
        /// <param name="barCodeImg">条形码图片</param>
        /// <returns></returns>
        public static string BarCodeImgToText(Bitmap barCodeImg)
        {
            BarcodeReader reader = new BarcodeReader();
            reader.Options.CharacterSet = "UTF-8";
            Result result = reader.Decode(barCodeImg);
            return result == null ? "" : result.Text;
        }

        /// <summary>
        /// 文本转条码图片
        /// </summary>
        /// <param name="text">转为条码的文本</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="barcodeFormat">条码格式，默认用：EAN_13</param>
        /// <returns></returns>
        public static Bitmap TextToBarCodeImg(string text,int width, int height, BarcodeFormat barcodeFormat=BarcodeFormat.EAN_13)
        {
            return new BarcodeWriter()
            {
                Format = barcodeFormat,
                Options = new EncodingOptions()
                {
                    Width = width,
                    Height = height,
                    Margin = 2
                }
            }.Write(text);
        }

        /// <summary>
        /// 文本转二维码
        /// </summary>
        /// <param name="text">内容</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="charSet">编码方式，默认是：UTF-8</param>
        /// <returns></returns>
        public static Bitmap TextToQrcodeImg(string text, int width,string charSet= "UTF-8")
        {
            return new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions()
                {
                    DisableECI = true,
                    CharacterSet = charSet,
                    Width = width,
                    Height = width,
                    Margin = 1
                }
            }.Write(text);
        }

        /// <summary>
        /// 生成带Logo的二维码
        /// </summary>
        /// <param name="text">内容</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        public static Bitmap TextToQrcodeImg(string text,Bitmap logo, int width)
        {
            //构造二维码写码器
            MultiFormatWriter writer = new MultiFormatWriter();
            Dictionary<EncodeHintType, object> hint = new Dictionary<EncodeHintType, object>();
            hint.Add(EncodeHintType.CHARACTER_SET, "UTF-8");
            hint.Add(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.H);
            //hint.Add(EncodeHintType.MARGIN, 2);//旧版本不起作用，需要手动去除白边

            //生成二维码 
            BitMatrix bm = writer.encode(text, BarcodeFormat.QR_CODE, width + 30, width + 30, hint);
            BarcodeWriter barcodeWriter = new BarcodeWriter();
            Bitmap map = barcodeWriter.Write(bm);

            //获取二维码实际尺寸
            int[] rectangle = bm.getEnclosingRectangle();

            //计算插入图片的大小和位置
            int middleW = Math.Min((int)(rectangle[2] / 3), logo.Width);
            int middleH = Math.Min((int)(rectangle[3] / 3), logo.Height);
            int middleL = (map.Width - middleW) / 2;
            int middleT = (map.Height - middleH) / 2;

            Bitmap bmpimg = new Bitmap(map.Width, map.Height, PixelFormat.Format32bppArgb);
            using (Graphics g = Graphics.FromImage(bmpimg))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.DrawImage(map, 0, 0, width, width);
                //白底将二维码插入图片
                g.FillRectangle(Brushes.White, middleL, middleT, middleW, middleH);
                g.DrawImage(logo, middleL, middleT, middleW, middleH);
            }
            return bmpimg;
        }

        /// <summary>
        /// 识别二维码图片的内容，不回收图片，需要用户自行回收
        /// </summary>
        /// <param name="imgPath">图片二维码的路径</param>
        /// <param name="candidateCount">候选区数量</param>
        /// <param name="characterSet">内容编码方式</param>
        /// <returns></returns>
        public static string QrcodeImgToText(string imgPath, int candidateCount = 10, string characterSet = "UTF-8")
        {
            using (Bitmap qrcodeImg=new Bitmap(imgPath))
            {
                return QrcodeImgToText(qrcodeImg, candidateCount, characterSet );
            }
        }

        /// <summary>
        /// 识别二维码图片的内容，不回收图片，需要用户自行回收
        /// </summary>
        /// <param name="qrcodeImg">图片二维码</param>
        /// <param name="candidateCount">候选区数量</param>
        /// <param name="characterSet">内容编码方式</param>
        /// <returns></returns>
        public static string QrcodeImgToText(Bitmap qrcodeImg, int candidateCount = 10,string characterSet = "UTF-8")
        {
            List<Rectangle> CandidateRectList = new List<Rectangle>();
            int offsetWidth, rectWidth;
            if (qrcodeImg.Width >= qrcodeImg.Height)
            {
                offsetWidth = qrcodeImg.Width - qrcodeImg.Height;
                rectWidth = qrcodeImg.Height;
                CandidateRectList.Add(new Rectangle(0, 0, rectWidth, rectWidth));
                int addWidth = (offsetWidth - (offsetWidth % candidateCount)) / candidateCount;
                for (int i = 0; i < candidateCount; i++)
                    CandidateRectList.Add(new Rectangle(0, offsetWidth % candidateCount + addWidth * i, rectWidth, rectWidth));
            }
            else
            {
                offsetWidth = qrcodeImg.Height - qrcodeImg.Width;
                rectWidth = qrcodeImg.Width;
                CandidateRectList.Add(new Rectangle(0, 0, rectWidth, rectWidth));
                int addWidth = (offsetWidth - (offsetWidth % candidateCount)) / candidateCount;
                for (int i = 0; i < candidateCount; i++)
                    CandidateRectList.Add(new Rectangle(offsetWidth % candidateCount + addWidth * i, 0, rectWidth, rectWidth));
            }
            BarcodeReader reader = new BarcodeReader();
            reader.Options.CharacterSet = characterSet;
            foreach (Rectangle rectangle in CandidateRectList)
            {
                using (Bitmap newBitmap = new Bitmap(rectangle.Width, rectangle.Height))
                {
                    using (var g = Graphics.FromImage(newBitmap))
                    {
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        g.DrawImage(qrcodeImg, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
                    }
                    return reader.Decode(new Bitmap(newBitmap))?.Text;
                }
            }
            return null;
        }

        /// <summary>
        /// 检查图片是否为微信收款二维码
        /// </summary>
        /// <param name="src">源图片</param>
        /// <param name="candidateCount">候选区数量</param>
        /// <returns>返回检查结果</returns>
        public static bool CheckPayQrcode(this Bitmap src, int candidateCount = 10)
        {
            return Convert.ToString(QrcodeImgToText(src, 10, "UTF-8")).StartsWith("wxp://");
        }
    }
}