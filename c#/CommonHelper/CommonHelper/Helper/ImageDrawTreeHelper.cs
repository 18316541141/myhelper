using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper.Helper
{
    /// <summary>
    /// 根据draw树结构进行绘图
    /// </summary>
    public class ImageDrawTreeHelper
    {
        /// <summary>
        /// 执行与释放或重置非托管资源相关的应用程序定义的任务
        /// </summary>
        public void Dispose(DrawImg drawImg)
        {
            bool hasChild = drawImg.DrawList != null && drawImg.DrawList.Count > 0;
            if (hasChild)
            {
                foreach (Draw childDraw in drawImg.DrawList)
                {
                    if (childDraw.GetType() == typeof(DrawImg))
                    {
                        DrawImg tempDrawImg = (DrawImg)childDraw;
                        using (tempDrawImg.BackgroundImage) { }
                        Dispose(tempDrawImg);
                    }
                }
            }
            else
            {
                using (drawImg.BackgroundImage) { }
            }
        }

        /// <summary>
        /// 根据drawImg树结构进行绘图
        /// </summary>
        /// <param name="drawImg">drawImg树结构</param>
        /// <returns>返回绘制的图片</returns>
        public Image DrawTree(DrawImg drawImg)
        {
            bool hasChild = drawImg.DrawList != null && drawImg.DrawList.Count > 0;
            Bitmap bitmap = new Bitmap(drawImg.Width, drawImg.Height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                Image background = null;
                if (!string.IsNullOrEmpty(drawImg.BackgroundUrl))
                {
                    WebResponse webResponse = HttpWebRequestHelper.HttpGet(drawImg.BackgroundUrl);
                    background = webResponse.GetBitmap();
                }
                else if (!string.IsNullOrEmpty(drawImg.BackgroundPath))
                {
                    background = Image.FromFile(drawImg.BackgroundPath);
                }
                else if (drawImg.BackgroundImage != null)
                {
                    background = drawImg.BackgroundImage;
                }
                if (background != null)
                {
                    g.DrawImage(background, new Rectangle(0, 0, drawImg.Width, drawImg.Height));
                }
                if (hasChild)
                {
                    foreach (Draw childDraw in drawImg.DrawList)
                    {
                        if (childDraw.GetType() == typeof(DrawImg))
                        {
                            DrawImg tempDrawImg = (DrawImg)childDraw;
                            using (Image childImage = DrawTree(tempDrawImg))
                            {
                                childImage.RotateFlip(tempDrawImg.Rotate);
                                g.DrawImage(childImage, new Rectangle(childDraw.Left, childDraw.Top, childImage.Width, childImage.Height));
                            }
                        }
                        else if (childDraw.GetType() == typeof(DrawText))
                        {
                            DrawText tempDrawText = (DrawText)childDraw;
                            Font font = new Font(tempDrawText.FontFamily, tempDrawText.FontSize, tempDrawText.FontStyle);
                            RectangleF rectangleF = new RectangleF(tempDrawText.Left, tempDrawText.Top, tempDrawText.Width, tempDrawText.Height);
                            g.DrawString(tempDrawText.Text, font, tempDrawText.Brush, rectangleF);
                        }
                    }
                }
                g.Save();
            }
            return bitmap;
        }
    }

    /// <summary>
    /// 绘画对象
    /// </summary>
    public class Draw
    {
        /// <summary>
        /// 宽度
        /// </summary>
        public int Width { set; get; }

        /// <summary>
        /// 高度
        /// </summary>
        public int Height { set; get; }

        /// <summary>
        /// 相对父图片x坐标
        /// </summary>
        public int Left { set; get; }

        /// <summary>
        /// 相对父图片y坐标
        /// </summary>
        public int Top { set; get; }
    }

    /// <summary>
    /// 画文字对象
    /// </summary>
    public class DrawText : Draw
    {
        /// <summary>
        /// 文字内容
        /// </summary>
        public string Text { set; get; }

        /// <summary>
        /// 字体
        /// </summary>
        public string FontFamily { set; get; }

        /// <summary>
        /// 字号
        /// </summary>
        public float FontSize { set; get; }

        /// <summary>
        /// 文字样式
        /// </summary>
        public FontStyle FontStyle { set; get; }

        /// <summary>
        /// 画笔纹理
        /// </summary>
        public Brush Brush { set; get; }
    }

    /// <summary>
    /// 画图片对象
    /// </summary>
    public class DrawImg : Draw, ICloneable
    {
        public DrawImg()
        {
            DrawList = new List<Draw>();
        }

        /// <summary>
        /// 子绘画对象，子绘画对象的left，top相对于父图片的左上角
        /// </summary>
        public List<Draw> DrawList { set; get; }

        /// <summary>
        /// 背景图片路径
        /// </summary>
        public string BackgroundPath { set; get; }

        /// <summary>
        /// 背景图片url
        /// </summary>
        public string BackgroundUrl { set; get; }

        /// <summary>
        /// 背景图片对象
        /// </summary>
        public Image BackgroundImage { set; get; }

        /// <summary>
        /// 背景颜色
        /// </summary>
        public string BackgroundColor { set; get; }

        /// <summary>
        /// 旋转方式
        /// </summary>
        public RotateFlipType Rotate { set; get; }

        /// <summary>
        /// 克隆实体
        /// </summary>
        /// <returns>返回克隆体</returns>
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
