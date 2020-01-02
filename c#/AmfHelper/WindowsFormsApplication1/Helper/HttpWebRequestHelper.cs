using FluorineFx.IO;
using FluorineFx.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CommonHelper.Helper
{
    /// <summary>
    /// http请求帮助类
    /// </summary>
    public static partial class AmfHelper
    {
        /// <summary>
        /// 发送基于x-amf3的http请求
        /// </summary>
        /// <param name="requestUrl">请求url</param>
        /// <param name="amfMessage">amf3的报文体</param>
        /// <param name="cookieContainer">请求的cookie</param>
        public static WebResponse Amf3RequestHelper(string requestUrl, AMFMessage amfMessage, CookieContainer cookieContainer = null)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(requestUrl);
            req.Method = "POST";
            req.ContentType = "application/x-amf";
            req.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36";
            req.CookieContainer = cookieContainer;
            req.LoadConfig(requestUrl);
            using (Stream stream = req.GetRequestStream())
            {
                using (AMFSerializer AMFSerializer = new AMFSerializer(stream))
                {
                    AMFSerializer.WriteMessage(amfMessage);
                }
            }
            try
            {
                return req.GetResponse();
            }
            catch (WebException ex)
            {
                if (ex.Message.Contains("502"))
                {
                    Console.WriteLine("连接远程服务器时发生错误！", ex);
                    throw new Exception("地址：" + requestUrl + "连接失败，(ERROR:-1)");
                }
                throw ex;
            }
        }

        /// <summary>
        /// http.config配置的xml解析器，该配置用于匹配特定的url使用特定的报文头。
        /// </summary>
        static XmlDocument _httpCfg;

        /// <summary>
        /// 初始化操作
        /// </summary>
        static AmfHelper()
        {
            _httpCfg = new XmlDocument();
            if (File.Exists(Environment.CurrentDirectory + Path.DirectorySeparatorChar + "http.xml"))
            {
                _httpCfg.LoadXml(File.ReadAllText(Environment.CurrentDirectory + Path.DirectorySeparatorChar + "http.xml"));
            }
        }

        /// <summary>
        /// 解析响应报文，把报文内容以amf对象的形式返回
        /// </summary>
        /// <param name="response">响应报文</param>
        /// <returns>返回amf信息对象</returns>
        public static AMFMessage GetAMFMessage(this WebResponse response)
        {
            using (Stream Stream = response.GetResponseStream())
            {
                using (AMFDeserializer AMFDeserializer = new AMFDeserializer(Stream))
                {
                    return AMFDeserializer.ReadAMFMessage();
                }
            }
        }

        /// <summary>
        /// 解析响应报文，把报文内容以字符串的形式返回
        /// </summary>
        /// <param name="response">响应报文</param>
        /// <returns>返回报文内容字符串</returns>
        public static string GetText(this WebResponse response)
        {
            using (Stream stream = response.GetResponseStream())
            {
                using (AMFDeserializer amfDeserializer = new AMFDeserializer(stream))
                {
                    AcknowledgeMessage msg = (AcknowledgeMessage)amfDeserializer.ReadAMFMessage().Bodies[0].Content;
                    return Convert.ToString(msg.body);
                }
            }
        }

        /// <summary>
        /// 根据url匹配项，加载自定义配置。
        /// </summary>
        /// <param name="req"></param>
        /// <param name="RequestUrl"></param>
        /// <returns></returns>
        static HttpWebRequest LoadConfig(this HttpWebRequest req, string RequestUrl)
        {
            foreach (XmlElement ele in _httpCfg.SelectNodes("//Url"))
            {
                foreach (string UrlPrefix in ele.GetAttribute("prefix").Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (RequestUrl.StartsWith(UrlPrefix.TrimEnd('\r', '\n')) && !string.IsNullOrEmpty(UrlPrefix.TrimEnd('\r', '\n')))
                    {
                        XmlNode ContentType = ele.SelectSingleNode("ContentType");
                        if (ContentType != null) req.ContentType = ContentType.InnerText;
                        XmlNode Authorization = ele.SelectSingleNode("Authorization");
                        if (Authorization != null) req.Headers.Set(HttpRequestHeader.Authorization, Authorization.InnerText);
                        XmlNode UserAgent = ele.SelectSingleNode("UserAgent");
                        if (UserAgent != null) req.UserAgent = UserAgent.InnerText;
                        XmlNode Accept = ele.SelectSingleNode("Accept");
                        if (Accept != null) req.Accept = Accept.InnerText;
                        XmlNode Pragma = ele.SelectSingleNode("Pragma");
                        if (Pragma != null) req.Headers.Set(HttpRequestHeader.Pragma, Pragma.InnerText);
                        XmlNode Origin = ele.SelectSingleNode("Origin");
                        if (Origin != null) req.Headers.Set("Origin", Origin.InnerText);
                        XmlNode Referer = ele.SelectSingleNode("Referer");
                        if (Referer != null) req.Referer = Referer.InnerText;
                        XmlNode Connection = ele.SelectSingleNode("Connection");
                        if (Connection != null) SetHeaderValue(req.Headers, "Connection", Connection.InnerText);
                        XmlNode AcceptEncoding = ele.SelectSingleNode("AcceptEncoding");
                        if (AcceptEncoding != null) req.Headers.Set(HttpRequestHeader.AcceptEncoding, AcceptEncoding.InnerText);
                        XmlNode AcceptLanguage = ele.SelectSingleNode("AcceptLanguage");
                        if (AcceptLanguage != null) req.Headers.Set(HttpRequestHeader.AcceptLanguage, AcceptLanguage.InnerText);
                        XmlNode SOAPAction = ele.SelectSingleNode("SOAPAction");
                        if (SOAPAction != null) req.Headers.Set("SOAPAction", SOAPAction.InnerText);
                        XmlNode KeepAlive = ele.SelectSingleNode("KeepAlive");
                        if (KeepAlive != null) req.KeepAlive = KeepAlive.InnerText.Trim() == "true";
                        XmlNode AllowAutoRedirect = ele.SelectSingleNode("AllowAutoRedirect");
                        if (AllowAutoRedirect != null) req.AllowAutoRedirect = AllowAutoRedirect.InnerText.Trim() == "true";
                    }
                }
            }
            return req;
        }

        /// <summary>
        /// 部分报文头只能通过该方法去设置，不能直接设置
        /// </summary>
        /// <param name="header"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        static void SetHeaderValue(WebHeaderCollection header, string name, string value)
        {
            var property = typeof(WebHeaderCollection).GetProperty("InnerCollection",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            if (property != null)
            {
                var collection = property.GetValue(header, null) as NameValueCollection;
                collection[name] = value;
            }
        }
    }
}
