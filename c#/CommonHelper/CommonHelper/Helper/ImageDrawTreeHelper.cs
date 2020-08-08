using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

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

        /*
            <drawImg width="" height="" left="" top="" backgroundPath="" backgroundUrl="" rotate="">
                <drawImg width="" height="" left="" top="" backgroundPath="" backgroundUrl="" rotate="">
                
                </drawImg>
                <drawText width="" height="" left="" top="" text="" fontFamily="" fontSize="" fontStyle="">
                    
                </drawText>
            </drawImg>
         */

        /// <summary>
        /// 对常量属性进行赋值
        /// </summary>
        /// <param name="drawImg"></param>
        /// <param name="xmlAttr"></param>
        void AttrConstSet(DrawText drawText, XmlAttribute xmlAttr)
        {
            if (string.Equals(xmlAttr.Name, "Width", StringComparison.CurrentCultureIgnoreCase))
            {
                int width;
                if (int.TryParse(xmlAttr.Value, out width))
                {
                    drawText.Width = width;
                }
            }
            else if (string.Equals(xmlAttr.Name, "Height", StringComparison.CurrentCultureIgnoreCase))
            {
                int height;
                if (int.TryParse(xmlAttr.Value, out height))
                {
                    drawText.Height = height;
                }
            }
            else if (string.Equals(xmlAttr.Name, "Left", StringComparison.CurrentCultureIgnoreCase))
            {
                int left;
                if (int.TryParse(xmlAttr.Value, out left))
                {
                    drawText.Left = left;
                }
            }
            else if (string.Equals(xmlAttr.Name, "Top", StringComparison.CurrentCultureIgnoreCase))
            {
                int top;
                if (int.TryParse(xmlAttr.Value, out top))
                {
                    drawText.Top = top;
                }
            }
            else if (string.Equals(xmlAttr.Name, "Text", StringComparison.CurrentCultureIgnoreCase))
            {
                drawText.Text = xmlAttr.Value;
            }
            else if (string.Equals(xmlAttr.Name, "FontFamily", StringComparison.CurrentCultureIgnoreCase))
            {
                drawText.FontFamily = xmlAttr.Value;
            }
            else if (string.Equals(xmlAttr.Name, "FontSize", StringComparison.CurrentCultureIgnoreCase))
            {
                int fontSize;
                if (int.TryParse(xmlAttr.Value, out fontSize))
                {
                    drawText.FontSize = fontSize;
                }
            }
            else if (string.Equals(xmlAttr.Name, "FontStyle", StringComparison.CurrentCultureIgnoreCase))
            {
                foreach (var item in Enum.GetValues(typeof(FontStyle)))
                {
                    if (string.Equals(xmlAttr.Value, item.ToString(), StringComparison.CurrentCultureIgnoreCase))
                    {
                        drawText.FontStyle = (FontStyle)item;
                        break;
                    }
                }
            }
            else if (string.Equals(xmlAttr.Name, "FontColor", StringComparison.CurrentCultureIgnoreCase))
            {
                string color = xmlAttr.Value.Trim();
                drawText.FontColor = ColorDescToColor(color);
            }
        }


        public Color ColorDescToColor(string colorDesc)
        {
            if (!string.IsNullOrEmpty(colorDesc))
            {
                colorDesc = colorDesc.Trim();
                int[] rgb = new int[3];
                if (colorDesc.StartsWith("#"))
                {
                    if (colorDesc.Length == 4)
                    {
                        for (int i = 1, j = 0; i < 4; i++, j++)
                        {
                            if (colorDesc[i] >= '0' && colorDesc[i] <= '9')
                            {
                                rgb[j] = colorDesc[i] - '0';
                            }
                            else if (colorDesc[i] >= 'a' && colorDesc[i] <= 'f')
                            {
                                rgb[j] = colorDesc[i] - 'a' + 10;
                            }
                            rgb[j] += rgb[j] * 16;
                        }
                        return Color.FromArgb(rgb[0], rgb[1], rgb[2]);
                    }
                    else if (colorDesc.Length == 7)
                    {
                        for (int i = 2, j = 0; i < 8; i += 2, j++)
                        {
                            if (colorDesc[i] >= '0' && colorDesc[i] <= '9')
                            {
                                rgb[j] = colorDesc[i] - '0';
                            }
                            else if (colorDesc[i] >= 'a' && colorDesc[i] <= 'f')
                            {
                                rgb[j] = colorDesc[i] - 'a' + 10;
                            }
                            rgb[j] += (colorDesc[i - 1] - 'a' + 10) * 15;
                        }
                        return Color.FromArgb(rgb[0], rgb[1], rgb[2]);
                    }
                }
                else if (colorDesc.StartsWith("rgb(") && colorDesc.EndsWith(")"))
                {
                    string[] rgbStrs = colorDesc.Remove(colorDesc.Length - 1, 1).Remove(0, 4).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0, parseInt = 0; i < rgbStrs.Length; i++)
                    {
                        if (int.TryParse(rgbStrs[i].Trim(), out parseInt))
                        {
                            rgb[i] = parseInt;
                        }
                    }
                    return Color.FromArgb(rgb[0], rgb[1], rgb[2]);
                }
            }
            return Color.Black;
        }

        /// <summary>
        /// 对常量属性进行赋值
        /// </summary>
        /// <param name="drawImg"></param>
        /// <param name="xmlAttr"></param>
        void AttrConstSet(DrawImg drawImg, XmlAttribute xmlAttr)
        {
            if (string.Equals(xmlAttr.Name, "Width", StringComparison.CurrentCultureIgnoreCase))
            {
                int width;
                if (int.TryParse(xmlAttr.Value, out width))
                {
                    drawImg.Width = width;
                }
            }
            else if (string.Equals(xmlAttr.Name, "Height", StringComparison.CurrentCultureIgnoreCase))
            {
                int height;
                if (int.TryParse(xmlAttr.Value, out height))
                {
                    drawImg.Height = height;
                }
            }
            else if (string.Equals(xmlAttr.Name, "Left", StringComparison.CurrentCultureIgnoreCase))
            {
                int left;
                if (int.TryParse(xmlAttr.Value, out left))
                {
                    drawImg.Left = left;
                }
            }
            else if (string.Equals(xmlAttr.Name, "Top", StringComparison.CurrentCultureIgnoreCase))
            {
                int top;
                if (int.TryParse(xmlAttr.Value, out top))
                {
                    drawImg.Top = top;
                }
            }
            else if (string.Equals(xmlAttr.Name, "BackgroundPath", StringComparison.CurrentCultureIgnoreCase))
            {
                drawImg.BackgroundPath = xmlAttr.Value;
            }
            else if (string.Equals(xmlAttr.Name, "BackgroundUrl", StringComparison.CurrentCultureIgnoreCase))
            {
                drawImg.BackgroundUrl = xmlAttr.Value;
            }
            else if (string.Equals(xmlAttr.Name, "Rotate", StringComparison.CurrentCultureIgnoreCase))
            {
                foreach (var item in Enum.GetValues(typeof(RotateFlipType)))
                {
                    if (string.Equals(xmlAttr.Value, item.ToString(), StringComparison.CurrentCultureIgnoreCase))
                    {
                        drawImg.Rotate = (RotateFlipType)item;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 对变量属性进行赋值
        /// </summary>
        /// <param name="drawText"></param>
        /// <param name="xmlAttr"></param>
        /// <param name="param"></param>
        void AttrVarSet(DrawText drawText, XmlAttribute xmlAttr, Dictionary<string, object> param)
        {
            if (param.ContainsKey(xmlAttr.Value))
            {
                try
                {
                    if (string.Equals(xmlAttr.Name, "ref-Width", StringComparison.CurrentCultureIgnoreCase))
                    {
                        drawText.Width = (int)param[xmlAttr.Value];
                    }
                    else if (string.Equals(xmlAttr.Name, "ref-Height", StringComparison.CurrentCultureIgnoreCase))
                    {
                        drawText.Height = (int)param[xmlAttr.Value];
                    }
                    else if (string.Equals(xmlAttr.Name, "ref-Left", StringComparison.CurrentCultureIgnoreCase))
                    {
                        drawText.Left = (int)param[xmlAttr.Value];
                    }
                    else if (string.Equals(xmlAttr.Name, "ref-Top", StringComparison.CurrentCultureIgnoreCase))
                    {
                        drawText.Top = (int)param[xmlAttr.Value];
                    }
                    else if (string.Equals(xmlAttr.Name, "ref-Text", StringComparison.CurrentCultureIgnoreCase))
                    {
                        drawText.Text = (string)param[xmlAttr.Value];
                    }
                    else if (string.Equals(xmlAttr.Name, "ref-FontFamily", StringComparison.CurrentCultureIgnoreCase))
                    {
                        drawText.FontFamily = (string)param[xmlAttr.Value];
                    }
                    else if (string.Equals(xmlAttr.Name, "ref-FontSize", StringComparison.CurrentCultureIgnoreCase))
                    {
                        drawText.FontSize = (int)param[xmlAttr.Value];
                    }
                    else if (string.Equals(xmlAttr.Name, "ref-FontStyle", StringComparison.CurrentCultureIgnoreCase))
                    {
                        drawText.FontStyle = (FontStyle)param[xmlAttr.Value];
                    }
                    else if (string.Equals(xmlAttr.Name, "ref-FontColor", StringComparison.CurrentCultureIgnoreCase))
                    {
                        drawText.FontColor = (Color)param[xmlAttr.Value];
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"设置动态属性时出错，错误原因：{ex.Message}");
                }
            }
        }

        /// <summary>
        /// 对变量属性进行赋值
        /// </summary>
        /// <param name="drawImg"></param>
        /// <param name="xmlAttr"></param>
        /// <param name="param"></param>
        void AttrVarSet(DrawImg drawImg, XmlAttribute xmlAttr, Dictionary<string, object> param)
        {
            if (param.ContainsKey(xmlAttr.Value))
            {
                try
                {
                    if (string.Equals(xmlAttr.Name, "ref-Width", StringComparison.CurrentCultureIgnoreCase))
                    {
                        drawImg.Width = (int)param[xmlAttr.Value];
                    }
                    else if (string.Equals(xmlAttr.Name, "ref-Height", StringComparison.CurrentCultureIgnoreCase))
                    {
                        drawImg.Height = (int)param[xmlAttr.Value];
                    }
                    else if (string.Equals(xmlAttr.Name, "ref-Left", StringComparison.CurrentCultureIgnoreCase))
                    {
                        drawImg.Left = (int)param[xmlAttr.Value];
                    }
                    else if (string.Equals(xmlAttr.Name, "ref-Top", StringComparison.CurrentCultureIgnoreCase))
                    {
                        drawImg.Top = (int)param[xmlAttr.Value];
                    }
                    else if (string.Equals(xmlAttr.Name, "ref-BackgroundPath", StringComparison.CurrentCultureIgnoreCase))
                    {
                        drawImg.BackgroundPath = (string)param[xmlAttr.Value];
                    }
                    else if (string.Equals(xmlAttr.Name, "ref-BackgroundUrl", StringComparison.CurrentCultureIgnoreCase))
                    {
                        drawImg.BackgroundUrl = (string)param[xmlAttr.Value];
                    }
                    else if (string.Equals(xmlAttr.Name, "ref-BackgroundImage", StringComparison.CurrentCultureIgnoreCase))
                    {
                        drawImg.BackgroundImage = (Image)param[xmlAttr.Value];
                    }
                    else if (string.Equals(xmlAttr.Name, "ref-BackgroundColor", StringComparison.CurrentCultureIgnoreCase))
                    {
                        drawImg.BackgroundColor = (Color)param[xmlAttr.Value];
                    }
                    else if (string.Equals(xmlAttr.Name, "ref-Rotate", StringComparison.CurrentCultureIgnoreCase))
                    {
                        drawImg.Rotate = (RotateFlipType)param[xmlAttr.Value];
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"设置动态属性时出错，错误原因：{ex.Message}");
                }
            }
        }

        /// <summary>
        /// 把xml转化为drawImg树
        /// </summary>
        /// <param name="xml">xml</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public DrawImg XmlToDrawImg(string xml, Dictionary<string, object> param = null)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            List<Draw> drawList = NodeToDrawImg(xmlDoc.ChildNodes, param);
            return (DrawImg)drawList[0];
        }

        /// <summary>
        /// 把xml转化为图片
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public Image XmlToImage(string xml, Dictionary<string, object> param = null)
        {
            DrawImg drawImg = XmlToDrawImg(xml, param);
            Image image = DrawTree(drawImg);
            Dispose(drawImg);
            return image;
        }

        /// <summary>
        /// 把节点解析成DrawImg对象
        /// </summary>
        /// <param name="xmlNodeList"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        List<Draw> NodeToDrawImg(XmlNodeList xmlNodeList, Dictionary<string, object> param = null)
        {
            List<Draw> drawList = new List<Draw>();
            bool paramIsNotNull = param != null;
            foreach (XmlNode xmlNode in xmlNodeList)
            {
                if (xmlNode.NodeType == XmlNodeType.Element)
                {
                    if (string.Equals(xmlNode.Name, "drawImg", StringComparison.CurrentCultureIgnoreCase))
                    {
                        DrawImg drawImg = new DrawImg();
                        foreach (XmlAttribute xmlAttr in xmlNode.Attributes)
                        {
                            if (paramIsNotNull)
                            {
                                AttrVarSet(drawImg, xmlAttr, param);
                            }
                            AttrConstSet(drawImg, xmlAttr);
                        }
                        drawList.Add(drawImg);
                        if (xmlNode.HasChildNodes)
                        {
                            drawImg.DrawList = NodeToDrawImg(xmlNode.ChildNodes, param);
                        }
                    }
                    else if (string.Equals(xmlNode.Name, "drawText", StringComparison.CurrentCultureIgnoreCase))
                    {
                        DrawText drawText = new DrawText();
                        foreach (XmlAttribute xmlAttr in xmlNode.Attributes)
                        {
                            if (paramIsNotNull)
                            {
                                AttrVarSet(drawText, xmlAttr, param);
                            }
                            AttrConstSet(drawText, xmlAttr);
                        }
                        drawList.Add(drawText);
                    }
                }
            }
            return drawList;
        }

        /// <summary>
        /// 根据drawImg树结构进行绘图
        /// </summary>
        /// <param name="drawImg">drawImg树结构</param>
        /// <returns>返回绘制的图片</returns>
        public Image DrawTree(DrawImg drawImg)
        {
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
                if (drawImg.BackgroundColor != null)
                {
                    g.FillRectangle(new SolidBrush(drawImg.BackgroundColor), new Rectangle(0, 0, drawImg.Width, drawImg.Height));
                }
                if (background != null)
                {
                    g.DrawImage(background, new Rectangle(0, 0, drawImg.Width, drawImg.Height));
                }
                if (drawImg.DrawList != null && drawImg.DrawList.Count > 0)
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
                            g.DrawString(tempDrawText.Text, font, new SolidBrush(tempDrawText.FontColor), rectangleF);
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
        /// 文字颜色
        /// </summary>
        public Color FontColor { set; get; }
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
        public Color BackgroundColor { set; get; }

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
            return new DrawImg
            {
                DrawList = DrawList.ToList(),
                BackgroundPath = BackgroundPath,
                BackgroundUrl = BackgroundUrl,
                BackgroundImage = BackgroundImage,
                BackgroundColor = BackgroundColor,
                Rotate = Rotate,
                Height = Height,
                Width = Width,
                Left = Left,
                Top = Top
            };
        }
    }
}
