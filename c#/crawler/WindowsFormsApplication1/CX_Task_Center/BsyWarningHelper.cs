
using log4net;
using System;
using System.Xml;
using WindowsFormsApplication1.ServiceReference1;

namespace CX_Task_Center.Code.Message
{
    /// <summary>
    /// 办事易预警帮助类
    /// </summary>
    public class BsyWarningHelper
    {
        /*
         * 通用模板一：
         ---------------------------------------------模板结构------------------------------------------------
            <warning type='01' openId='$openId$'>
                <title>$title$</title>
                <source title='报警来源'>$source$</source>
                <datetime title='发生时间'>$datetime$</datetime>
                <content title='报警内容'>$content$</content>
                <remark>$remark$</remark>
                <url><![CDATA[$url$]]></url>
            </warning>
        ---------------------------------------------实际效果------------------------------------------------
        |系统报警通知                |
        _____________________________
        |{title}                     |
        
        |报警来源：{source}          |
        
        |发生时间：{datetime}        |

        |报警内容：{content}         |

        |{remark}                    |
        _____________________________
        |详情 >>                     | （点击“详情”后跳转{url}）
        _____________________________
         */

        public BsyWarningHelper()
        {
            MessageSenderSoapClient = new MessageSenderSoapClient();
        }

        public ILog log { set; get; }

        /// <summary>
        /// 发生报警，传入模板xml参数
        /// </summary>
        /// <param name="templateXml"></param>
        public void SendWarning(string templateXml)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement warning;
            XmlElement title;
            XmlElement source;
            XmlElement datetime;
            XmlElement content;
            XmlElement remark;
            XmlElement url;
            try
            {
                doc.LoadXml(templateXml);
                warning = (XmlElement)doc.SelectSingleNode("//warning");
                if (warning.GetAttribute("type") == "01")
                {
                    title = (XmlElement)doc.SelectSingleNode("//title");
                    source = (XmlElement)doc.SelectSingleNode("//source");
                    datetime = (XmlElement)doc.SelectSingleNode("//datetime");
                    content = (XmlElement)doc.SelectSingleNode("//content");
                    remark = (XmlElement)doc.SelectSingleNode("//remark");
                    url = (XmlElement)doc.SelectSingleNode("//url");
                }
                else
                {
                    throw new Exception("模板不存在。");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("模板解析错误，请确保模板结构正确。");
            }
            try
            {
                MessageSenderSoapClient.SendWeiXinForWarning(warning.GetAttribute("openId"), "{\"first\":{\"value\":\"" + title.InnerText + "\"},\"keynote1\":{\"value\":\"" + source.InnerText + "\"},\"keynote2\":{\"value\":\"" + datetime.InnerText + "\"},\"keynote3\":{\"value\":\"" + content.InnerText + "\"},\"remark\":{\"value\":\"" + remark.InnerText + "\"}}", url.InnerText);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
            }
        }

        /// <summary>
        /// 办事易发送类
        /// </summary>
        private MessageSenderSoapClient MessageSenderSoapClient { set; get; }
    }
}