using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static QRCoder.QRCodeGenerator;

namespace CommonHelper.Helper
{
    /// <summary>
    /// 二维码相关的帮助类
    /// </summary>
    public static class QRCodeHelper
    {
        /// <summary>
        /// 创建二维码流
        /// </summary>
        /// <param name="content">二维码内容</param>
        /// <param name="imageFormat">二维码流的编码格式，默认是ImageFormat.Jpeg</param>
        /// <param name="icon">二维码中间图标</param>
        /// <returns></returns>
        public static Stream CreateQRCodeStream(string content, ImageFormat imageFormat = null, Bitmap icon = null)
        {
            if(imageFormat == null)
            {
                imageFormat = ImageFormat.Jpeg;
            }
            Bitmap bitmap = CreateQRCodeBitmap(content, icon);
            MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, imageFormat);
            ms.Position = 0;
            return ms;
        }

        /// <summary>
        /// 创建二维码点阵图
        /// </summary>
        /// <param name="content">二维码内容</param>
        /// <param name="icon">二维码中间图标</param>
        /// <returns></returns>
        public static Bitmap CreateQRCodeBitmap(string content,Bitmap icon = null)
        {
            QRCodeGenerator qrCodeGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrCodeGenerator.CreateQrCode(content, ECCLevel.H);
            QRCode qrcode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrcode.GetGraphic(5, Color.Black, Color.White, icon, 15, 6, false);
            return qrCodeImage;
        }
    }
}
