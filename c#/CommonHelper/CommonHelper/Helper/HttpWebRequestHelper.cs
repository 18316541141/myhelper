using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Web;
using System.Xml;
namespace CommonHelper.Helper
{

    /// <summary>
    /// ip代理接口
    /// </summary>
    public interface ProxyIp
    {
        /// <summary>
        /// 是否启用动态ip代理
        /// </summary>
        /// <param name="reqUrl">请求url，判断是否启用ip代理的依据</param>
        /// <param name="webProxy">这是一个返回值，返回动态ip代理的对象</param>
        /// <returns>如果启用返回true</returns>
        bool EnableWebProxy(string reqUrl, out WebProxy webProxy);
    }

    /// <summary>
    /// 默认的ip代理（默认不使用）
    /// </summary>
    public class DefaultProxyIp : ProxyIp
    {
        /// <summary>
        /// 默认不使用ip代理
        /// </summary>
        /// <param name="reqUrl">请求url，判断是否启用ip代理的依据</param>
        /// <param name="webProxy">这是一个返回值，返回动态ip代理的对象</param>
        /// <returns>如果启用返回true</returns>
        public bool EnableWebProxy(string reqUrl, out WebProxy webProxy)
        {
            webProxy = null;
            return false;
        }
    }

    /// <summary>
    /// 检查是否被踢出登录，由用户自定义判断是否被踢出登录的逻辑
    /// </summary>
    /// <typeparam name="E"></typeparam>
    public interface CheckLogout
    {
        /// <summary>
        /// 前置检查，根据报文头推算出是否被踢出登录
        /// </summary>
        /// <param name="Response"></param>
        /// <returns></returns>
        bool IsLogout(WebResponse Response);

        /// <summary>
        /// 后置检查，根据报文内容判断是否被踢出登录
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        bool IsLogoutAfter(string text);
    }

    /// <summary>
    /// 遇到异常时会检测该异常是否需要重新发起请求。
    /// </summary>
    public interface TryExceptionHandle
    {
        /// <summary>
        /// 检测遇到该异常时否需要重新再发一次请求
        /// </summary>
        /// <param name="reqUrl">请求的url</param>
        /// <param name="ex">异常对象</param>
        /// <returns>如果需要重新发送请求，则返回true</returns>
        bool CheckTry(string reqUrl, Exception ex);
    }

    /// <summary>
    /// 默认的登出检测，始终认为当前用户不会被踢出
    /// </summary>
    public class DefaultCheckLogout : CheckLogout
    {
        public bool IsLogout(WebResponse Response) => false;

        public bool IsLogoutAfter(string text) => false;
    }

    /// <summary>
    /// 默认的异常重试器，直接抛出异常，没有其他处理
    /// </summary>
    public class DefaultTryExceptionHandle : TryExceptionHandle
    {
        public bool CheckTry(string reqUrl,Exception ex) => false;
    }

    /// <summary>
    /// 使用异步http请求的回调函数
    /// </summary>
    public delegate void AsyncCallback(WebResponse webResponse);

    /// <summary>
    /// 使用异步http请求的报错回调函数
    /// </summary>
    /// <param name="ex">异常对象</param>
    public delegate void AsyncFailCallback(Exception ex);


    /// <summary>
    /// http请求帮助类
    /// </summary>
    public static class HttpWebRequestHelper
    {
        /// <summary>
        /// 异步http请求帮助类
        /// </summary>
        public static class AsyncReq
        {
            static int _executeCount;
            public static int MaxExecuteCount { set; get; }
            static string _controllerName;
            static AsyncReq()
            {
                _executeCount = 0;
                MaxExecuteCount = 8;
                _controllerName = Guid.NewGuid().ToString();
            }

            public static void HttpPost(AsyncCallback asyncCallback, AsyncFailCallback asyncFailCallback, string RequestUrl, Dictionary<string, string> Query, string Json, CookieContainer CookieContainer = null) =>
                HttpPost(asyncCallback, asyncFailCallback, RequestUrl + "?" + QueryParamsMapToStr(Query, RequestUrl.LoadRequestEncoding()), Json, CookieContainer);
            
            public static void HttpGet(AsyncCallback asyncCallback, AsyncFailCallback asyncFailCallback, string RequestUrl, Dictionary<string, string> Query, CookieContainer CookieContainer = null) =>
                HttpGet(asyncCallback, asyncFailCallback, RequestUrl + "?" + QueryParamsMapToStr(Query, RequestUrl.LoadRequestEncoding()), CookieContainer);

            public static void HttpPost(AsyncCallback asyncCallback, AsyncFailCallback asyncFailCallback, string RequestUrl, Dictionary<string, string> Query, Dictionary<string, string> Param, CookieContainer CookieContainer = null) =>
                HttpPost(asyncCallback, asyncFailCallback, RequestUrl + "?" + QueryParamsMapToStr(Query, RequestUrl.LoadRequestEncoding()), Param, CookieContainer);

            public static void HttpPost(AsyncCallback asyncCallback, AsyncFailCallback asyncFailCallback, string RequestUrl, Dictionary<string, string> Query, Dictionary<string, dynamic> Param, CookieContainer CookieContainer = null) =>
                HttpPost(asyncCallback, asyncFailCallback, RequestUrl + "?" + QueryParamsMapToStr(Query, RequestUrl.LoadRequestEncoding()), Param, CookieContainer);

            public static void HttpGet(AsyncCallback asyncCallback, AsyncFailCallback asyncFailCallback, string RequestUrl, CookieContainer CookieContainer = null)
            {
                Interlocked.Increment(ref _executeCount);
                while (_executeCount > MaxExecuteCount)
                    ThreadHelper.BatchWait(_controllerName, 30000);
                ThreadPool.QueueUserWorkItem((obj) => {
                    try
                    {
                        asyncCallback(HttpWebRequestHelper.HttpGet(RequestUrl,CookieContainer));
                    }
                    catch (Exception ex)
                    {
                        asyncFailCallback(ex);
                    }
                    Interlocked.Decrement(ref _executeCount);
                    ThreadHelper.BatchSet(_controllerName);
                });
            }

            public static void HttpPost(AsyncCallback asyncCallback, AsyncFailCallback asyncFailCallback, string RequestUrl, Dictionary<string, dynamic> Param, CookieContainer CookieContainer = null)
            {
                Interlocked.Increment(ref _executeCount);
                while (_executeCount > MaxExecuteCount)
                    ThreadHelper.BatchWait(_controllerName,30000);
                ThreadPool.QueueUserWorkItem((obj)=> {
                    try
                    {
                        asyncCallback(HttpWebRequestHelper.HttpPost(RequestUrl, Param, CookieContainer));
                    }
                    catch (Exception ex)
                    {
                        asyncFailCallback(ex);
                    }
                Interlocked.Decrement(ref _executeCount);
                    ThreadHelper.BatchSet(_controllerName);
                });
            }

            public static void HttpPost(AsyncCallback asyncCallback, AsyncFailCallback asyncFailCallback, string RequestUrl, Dictionary<string, string> Param, CookieContainer CookieContainer = null)
            {
                Interlocked.Increment(ref _executeCount);
                while (_executeCount > MaxExecuteCount)
                    ThreadHelper.BatchWait(_controllerName, 30000);
                ThreadPool.QueueUserWorkItem((obj) => {
                    try
                    {
                        asyncCallback(HttpWebRequestHelper.HttpPost(RequestUrl, Param, CookieContainer));
                    }
                    catch (Exception ex)
                    {
                        asyncFailCallback(ex);
                    }
                Interlocked.Decrement(ref _executeCount);
                    ThreadHelper.BatchSet(_controllerName);
                });
            }

            public static void HttpPost(AsyncCallback asyncCallback, AsyncFailCallback asyncFailCallback, string RequestUrl, string Body, CookieContainer CookieContainer = null)
            {
                Interlocked.Increment(ref _executeCount);
                while (_executeCount > MaxExecuteCount)
                    ThreadHelper.BatchWait(_controllerName, 30000);
                ThreadPool.QueueUserWorkItem((obj) => {
                    try
                    {
                        asyncCallback(HttpWebRequestHelper.HttpPost(RequestUrl, Body, CookieContainer));
                    }
                    catch (Exception ex)
                    {
                        asyncFailCallback(ex);
                    }
                    Interlocked.Decrement(ref _executeCount);
                    ThreadHelper.BatchSet(_controllerName);
                });
            }
        }

        /// <summary>
        /// 初始化操作
        /// </summary>
        static HttpWebRequestHelper()
        {
            _httpCfg = new XmlDocument();
            if (File.Exists(Environment.CurrentDirectory + Path.DirectorySeparatorChar + "http.xml"))
                _httpCfg.LoadXml(File.ReadAllText(Environment.CurrentDirectory + Path.DirectorySeparatorChar + "http.xml"));
            DefaultProxyIp = new DefaultProxyIp();
            DefaultCheckLogout = new DefaultCheckLogout();
            DefaultTryExceptionHandle = new DefaultTryExceptionHandle();
        }

        /// <summary>
        /// 默认的异常重试器，可以被重置
        /// </summary>
        public static TryExceptionHandle DefaultTryExceptionHandle { set; private get; }

        /// <summary>
        /// 默认的ip代理类，可以被重置
        /// </summary>
        public static ProxyIp DefaultProxyIp { set; private get; }

        /// <summary>
        /// 默认的登出检测器，可以被重置
        /// </summary>
        public static CheckLogout DefaultCheckLogout { set; private get; }

        /// <summary>
        /// 解析响应报文，把报文内容以图片对象的形式返回
        /// </summary>
        /// <param name="Response"></param>
        /// <returns></returns>
        public static Image GetImage(this WebResponse Response) {
            if (DefaultCheckLogout.IsLogout(Response))
                throw new Exception("已退出登录！(ERROR:-3)");
            else
                return Image.FromStream(Response.GetInput());
        }

        /// <summary>
        /// 解析响应报文，把报文内容以Bitmap对象的形式返回
        /// </summary>
        /// <param name="Response"></param>
        /// <returns></returns>
        public static Bitmap GetBitmap(this WebResponse Response) {
            if (DefaultCheckLogout.IsLogout(Response))
                throw new Exception("已退出登录！(ERROR:-3)");
            else
                return new Bitmap(Response.GetInput());
        }

        /// <summary>
        /// 解析响应报文，把报文内容以输入流的形式返回
        /// </summary>
        /// <param name="Response"></param>
        /// <returns></returns>
        public static BufferedStream GetInput(this WebResponse Response)
        {
            if (DefaultCheckLogout.IsLogout(Response))
                throw new Exception("已退出登录！(ERROR:-3)");
            else
                return new BufferedStream(Response.GetResponseStream());
        }

        /// <summary>
        /// 解析响应报文，把报文内容输出到输出流，会自动关闭输出流
        /// </summary>
        /// <param name="Response">响应报文</param>
        /// <param name="OutputStream">输出流</param>
        public static void Output(this WebResponse Response, Stream OutputStream)
        {
            if (DefaultCheckLogout.IsLogout(Response))
                throw new Exception("已退出登录！(ERROR:-3)");
            else
            {
                byte[] buff = new byte[4096];
                using (Response)
                    StreamHelper.FastCopyStream(Response.GetResponseStream(), OutputStream);
            }
        }

        /// <summary>
        /// 解析响应报文，把报文内容以xml的形式返回
        /// </summary>
        /// <param name="Response">响应报文</param>
        /// <returns>返回xml</returns>
        public static XmlElement GetXml(this WebResponse Response)
        {
            XmlDocument Doc = new XmlDocument();
            Doc.LoadXml(Response.GetText());
            return Doc.DocumentElement;
        }

        /// <summary>
        /// 解析响应报文，把报文内容以字符串的形式返回
        /// </summary>
        /// <param name="Response">响应报文</param>
        /// <returns>返回报文内容字符串</returns>
        public static string GetText(this WebResponse Response)
        {
            if (DefaultCheckLogout.IsLogout(Response))
            {
                throw new Exception("已退出登录！(ERROR:-3)");
            }
            else
            {
                string text;
                using (Response)
                {
                    string ContentEncoding = Response.Headers[HttpResponseHeader.ContentEncoding];
                    Encoding encoding = LoadResponseEncoding(Response.ResponseUri.ToString());
                    if (ContentEncoding == "gzip")
                    {
                        using (Stream Stream = Response.GetResponseStream())
                        {
                            text = encoding.GetString(CompressHelper.GZipDecompress(Stream));
                        }
                    }
                    else
                    {
                        using (StreamReader Reader = new StreamReader(Response.GetResponseStream(), encoding))
                        {
                            text = Reader.ReadToEnd();
                        }
                    }
                }
                if (DefaultCheckLogout.IsLogoutAfter(text))
                {
                    throw new Exception("已退出登录！(ERROR:-3)");
                }
                return text;
            }
        }

        /// <summary>
        /// 解析响应报文，把报文内容以html的形式返回
        /// </summary>
        /// <param name="Response">响应报文</param>
        /// <returns>响应报文的html</returns>
        public static HtmlNode GetHtmlNode(this WebResponse Response)
        {
            using (Response)
            {
                HtmlDocument Doc = new HtmlDocument();
                Doc.LoadHtml(Response.GetText());
                return Doc.DocumentNode;
            }
        }

        /// <summary>
        /// 解析响应报文，把报文内容以json对象的形式返回
        /// </summary>
        /// <param name="Response">响应报文</param>
        /// <returns>响应报文的json对象</returns>
        public static JObject GetJsonObj(this WebResponse Response)
        {
            string text=Response.GetText();
            return JObject.Parse(text);
        }

        /// <summary>
        /// 解析响应报文，把报文内容以json集合的形式返回
        /// </summary>
        /// <param name="Response">响应报文</param>
        /// <returns>响应报文的json集合</returns>
        public static JArray GetJsonArray(this WebResponse Response) =>
            JArray.Parse(Response.GetText());

        /// <summary>
        /// http.config配置的xml解析器，该配置用于匹配特定的url使用特定的报文头。
        /// </summary>
        static XmlDocument _httpCfg;

        /// <summary>
        /// 发送既含有url参数，又含有表单参数的post请求，上传文件用
        /// </summary>
        /// <param name="RequestUrl">请求的url</param>
        /// <param name="Query">url参数</param>
        /// <param name="Param">表单参数，上传文件用</param>
        /// <param name="CookieContainer">cookie容器</param>
        /// <returns>响应报文</returns>
        public static WebResponse HttpPost(this string RequestUrl, Dictionary<string, string> Query, Dictionary<string, dynamic> Param, CookieContainer CookieContainer = null) =>
            HttpPost(RequestUrl + "?" + QueryParamsMapToStr(Query, RequestUrl.LoadRequestEncoding()), Param, CookieContainer);

        /// <summary>
        /// 发送只含有表单参数的post请求，支持上传文件
        /// </summary>
        /// <param name="RequestUrl">请求的url</param>
        /// <param name="Param">表单参数，上传文件用</param>
        /// <param name="CookieContainer">cookie容器</param>
        /// <returns>响应报文</returns>
        public static WebResponse HttpPost(this string RequestUrl, Dictionary<string, dynamic> Param, CookieContainer CookieContainer = null)
        {
            TRY_AGAIN:
            string boundary = "---------------" + DateTime.Now.Ticks.ToString("x");
            byte[] beginBoundaryByte = Encoding.ASCII.GetBytes("--" + boundary + "\r\n");
            byte[] endBoundaryByte = Encoding.ASCII.GetBytes("--" + boundary + "--\r\n");
            byte[] newLine = Encoding.ASCII.GetBytes("\r\n");

            HttpWebRequest Req = (HttpWebRequest)WebRequest.Create(RequestUrl);
            WebProxy webProxy;
            if(DefaultProxyIp.EnableWebProxy(RequestUrl,out webProxy))
                Req.Proxy = webProxy;

            Req.Method = "POST";
            Req.ContentType = "multipart/form-data;boundary=" + boundary;
            Req.LoadConfig(RequestUrl);
            if (CookieContainer != null) Req.CookieContainer = CookieContainer;
            byte[] cache = new byte[4096];
            Type Type;
            string key;
            using (Stream stream = new BufferedStream(Req.GetRequestStream()))
            {
                Encoding reqEncoding=RequestUrl.LoadRequestEncoding();
                foreach (KeyValuePair<string, dynamic> kv in Param)
                {
                    Type = kv.Value.GetType();
                    key = kv.Key;
                    stream.Write(beginBoundaryByte, 0, beginBoundaryByte.Length);
                    if (typeof(string) == Type)
                    {
                        string value = kv.Value;
                        byte[] temp = reqEncoding.GetBytes($"Content-Disposition: form-data; name=\"{key}\" \r\n\r\n{value}");
                        stream.Write(temp, 0, temp.Length);
                    }
                    else if (typeof(FileInfo) == Type)
                    {
                        FileInfo f = kv.Value;
                        string filename = f.Name;
                        byte[] temp = reqEncoding.GetBytes($"Content-Disposition: form-data; name=\"{key}\"; filename=\"{filename}\"\r\nContent-Type: application/octet-stream\r\n\r\n");
                        stream.Write(temp, 0, temp.Length);
                        using (Stream input = new BufferedStream(f.OpenRead()))
                        {
                            for (int len; (len = input.Read(cache, 0, cache.Length)) > 0;)
                                stream.Write(cache, 0, len);
                        }
                    }
                    else if (typeof(Stream) == Type)
                    {
                        string filename = key;
                        byte[] temp = reqEncoding.GetBytes($"Content-Disposition: form-data; name=\"{key}\"; filename=\"{filename}\"\r\nContent-Type: application/octet-stream\r\n\r\n");
                        stream.Write(temp, 0, temp.Length);
                        using (Stream input = kv.Value)
                        {
                            for (int len; (len = input.Read(cache, 0, cache.Length)) > 0;)
                                stream.Write(cache, 0, len);
                        }
                    }
                    else if (typeof(Bitmap) == Type)
                    {
                        string filename = key;
                        byte[] temp = reqEncoding.GetBytes($"Content-Disposition: form-data; name=\"{key}\"; filename=\"{filename}\"\r\nContent-Type: application/octet-stream\r\n\r\n");
                        stream.Write(temp, 0, temp.Length);
                        using (Bitmap Bitmap = kv.Value)
                            Bitmap.Save(stream, ImageFormat.Bmp);
                    }
                    stream.Write(newLine, 0, newLine.Length);
                }
                stream.Write(endBoundaryByte, 0, endBoundaryByte.Length);
            }
            try
            {
                return Req.GetResponse();
            }
            catch (Exception ex)
            {
                if (DefaultTryExceptionHandle.CheckTry(RequestUrl, ex))
                    goto TRY_AGAIN;
                else
                    throw ex;
            }
        }

        /// <summary>
        /// 发送只含有表单参数的post请求
        /// </summary>
        /// <param name="RequestUrl">请求的url</param>
        /// <param name="Param">表单参数</param>
        /// <param name="CookieContainer">cookie容器</param>
        /// <returns></returns>
        public static WebResponse HttpPost(this string RequestUrl, Dictionary<string, string> Param, CookieContainer CookieContainer = null)
        {
            TRY_AGAIN:
            HttpWebRequest Req = (HttpWebRequest)WebRequest.Create(RequestUrl);
            WebProxy webProxy;
            if (DefaultProxyIp.EnableWebProxy(RequestUrl, out webProxy))
                Req.Proxy = webProxy;
            Req.Method = "POST";
            Req.ContentType = "application/x-www-form-urlencoded";
            Req.LoadConfig(RequestUrl);
            if (CookieContainer != null) Req.CookieContainer = CookieContainer;
            using (Stream stream = Req.GetRequestStream())
            {
                byte[] b = RequestUrl.LoadRequestEncoding().GetBytes(QueryParamsMapToStr(Param, RequestUrl.LoadRequestEncoding()));
                stream.Write(b, 0, b.Length);
            }
            try
            {
                return Req.GetResponse();
            }
            catch (Exception ex)
            {
                if (DefaultTryExceptionHandle.CheckTry(RequestUrl,ex))
                    goto TRY_AGAIN;
                else
                    throw ex;
            }
        }

        /// <summary>
        /// 发送既含有表单参数，又含有url参数的post请求
        /// </summary>
        /// <param name="RequestUrl">请求的url</param>
        /// <param name="Query">url参数</param>
        /// <param name="Param">表单参数</param>
        /// <param name="CookieContainer">cookie容器</param>
        /// <returns></returns>
        public static WebResponse HttpPost(this string RequestUrl, Dictionary<string, string> Query, Dictionary<string, string> Param, CookieContainer CookieContainer = null) =>
            HttpPost(RequestUrl + "?" + QueryParamsMapToStr(Query, RequestUrl.LoadRequestEncoding()), Param, CookieContainer);

        /// <summary>
        /// 发送只有报文体的post请求
        /// </summary>
        /// <param name="RequestUrl"></param>
        /// <param name="Body"></param>
        /// <param name="CookieContainer"></param>
        /// <returns>响应报文</returns>
        public static WebResponse HttpPost(this string RequestUrl, string Body, CookieContainer CookieContainer = null)
        {
            TRY_AGAIN:
            HttpWebRequest Req = (HttpWebRequest)WebRequest.Create(RequestUrl);
            WebProxy webProxy;
            if (DefaultProxyIp.EnableWebProxy(RequestUrl, out webProxy))
                Req.Proxy = webProxy;
            Req.Method = "POST";
            Req.ContentType = "application/json";
            Req.LoadConfig(RequestUrl);
            if (CookieContainer != null) Req.CookieContainer = CookieContainer;
            using (Stream stream = Req.GetRequestStream())
            {
                byte[] b = RequestUrl.LoadRequestEncoding().GetBytes(Body);
                stream.Write(b, 0, b.Length);
            }
            try
            {
                return Req.GetResponse();
            }
            catch (Exception ex)
            {
                if (DefaultTryExceptionHandle.CheckTry(RequestUrl, ex))
                    goto TRY_AGAIN;
                else
                    throw ex;
            }
        }

        /// <summary>
        /// 发送只有url参数和json报文体的post请求
        /// </summary>
        /// <param name="RequestUrl">url</param>
        /// <param name="Query">url参数</param>
        /// <param name="Json">json报文体</param>
        /// <param name="CookieContainer">cookie</param>
        /// <returns>响应报文</returns>
        public static WebResponse HttpPost(this string RequestUrl, Dictionary<string, string> Query, string Json, CookieContainer CookieContainer = null) =>
            HttpPost(RequestUrl + "?" + QueryParamsMapToStr(Query, RequestUrl.LoadRequestEncoding()), Json, CookieContainer);

        /// <summary>
        /// 发送只有url参数的get请求
        /// </summary>
        /// <param name="RequestUrl">url</param>
        /// <param name="Query">url参数</param>
        /// <param name="CookieContainer">cookie</param>
        /// <returns></returns>
        public static WebResponse HttpGet(this string RequestUrl, Dictionary<string, string> Query, CookieContainer CookieContainer = null) =>
            HttpGet(RequestUrl + "?" + QueryParamsMapToStr(Query, RequestUrl.LoadRequestEncoding()), CookieContainer);

        /// <summary>
        /// 发送只有url的get请求
        /// </summary>
        /// <param name="RequestUrl">url</param>
        /// <param name="CookieContainer">cookie</param>
        /// <returns>响应报文</returns>
        public static WebResponse HttpGet(this string RequestUrl, CookieContainer CookieContainer = null)
        {
            TRY_AGAIN:
            HttpWebRequest Req = (HttpWebRequest)WebRequest.Create(RequestUrl);
            WebProxy webProxy;
            if (DefaultProxyIp.EnableWebProxy(RequestUrl, out webProxy))
                Req.Proxy = webProxy;
            Req.Method = "GET";
            Req.LoadConfig(RequestUrl);
            if (CookieContainer != null) Req.CookieContainer = CookieContainer;
            try
            {
                return Req.GetResponse();
            }
            catch (Exception ex)
            {
                if (DefaultTryExceptionHandle.CheckTry(RequestUrl, ex))
                    goto TRY_AGAIN;
                else
                    throw ex;
            }
        }

        /// <summary>
        /// 发送soap请求报文
        /// </summary>
        /// <param name="requestUrl">url</param>
        /// <param name="body">请求报文体</param>
        /// <param name="header">请求报文头，可选，但传入之后会覆盖默认的header</param>
        public static XmlElement SoapReq(string requestUrl, string body, string header=null)
        {
            TRY_AGAIN:
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(requestUrl);
            req.Method = "POST";
            req.ContentType = "text/xml; charset=utf-8";
            req.LoadConfig(requestUrl);
            if (string.IsNullOrEmpty(header))
                header = requestUrl.LoadSoapHeader();
            string soapMsg =$@"
            <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"">
                <soapenv:Header>
                    {header}
                </soapenv:Header>
                <soapenv:Body>
                    {body}
                </soapenv:Body>
            </soapenv:Envelope>";
            StringBuilder sb = new StringBuilder();
            foreach (string row in soapMsg.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
                sb.Append(row.Trim());
            using (Stream stream = req.GetRequestStream())
            {
                byte[] b = requestUrl.LoadRequestEncoding().GetBytes(sb.ToString());
                stream.Write(b, 0, b.Length);
            }
            try
            {
                return req.GetResponse().GetXml();
            }
            catch (Exception ex)
            {
                if (DefaultTryExceptionHandle.CheckTry(requestUrl, ex))
                    goto TRY_AGAIN;
                else
                    throw ex;
            }
        }

        /// <summary>
        /// 读取配置文件中该soap请求的消息头
        /// </summary>
        /// <param name="requestUrl">请求url</param>
        /// <returns>返回该请求的url的soap消息头</returns>
        static string LoadSoapHeader(this string requestUrl)
        {
            foreach (XmlElement ele in _httpCfg.SelectNodes("//Url"))
                foreach (string UrlPrefix in ele.GetAttribute("prefix").Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                    if (!string.IsNullOrEmpty(UrlPrefix.TrimEnd('\r', '\n')) && requestUrl.StartsWith(UrlPrefix.TrimEnd('\r', '\n')))
                    {
                        XmlNode soapHeader = ele.SelectSingleNode("SoapHeader");
                        if (soapHeader != null) return soapHeader.InnerText;
                    }
            return "";
        }

        /// <summary>
        /// 读取配置文件中该请求的报文编码方式
        /// </summary>
        /// <param name="RequestUrl">请求url</param>
        /// <returns>返回编码方式</returns>
        static Encoding LoadRequestEncoding(this string RequestUrl)
        {
            foreach (XmlElement ele in _httpCfg.SelectNodes("//Url"))
                foreach (string UrlPrefix in ele.GetAttribute("prefix").Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                    if (!string.IsNullOrEmpty(UrlPrefix.TrimEnd('\r', '\n')) && RequestUrl.StartsWith(UrlPrefix.TrimEnd('\r', '\n')))
                    {
                        XmlNode Encoding = ele.SelectSingleNode("RequestEncoding");
                        if (Encoding != null) return System.Text.Encoding.GetEncoding(Encoding.InnerText);
                    }
            return Encoding.UTF8;
        }

        /// <summary>
        /// 读取配置文件中该响应的报文编码方式
        /// </summary>
        /// <param name="responseUrl">响应请求的url</param>
        /// <returns>返回编码方式</returns>
        static Encoding LoadResponseEncoding(string responseUrl)
        {
            foreach (XmlElement ele in _httpCfg.SelectNodes("//Url"))
                foreach (string UrlPrefix in ele.GetAttribute("prefix").Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                    if (!string.IsNullOrEmpty(UrlPrefix.TrimEnd('\r', '\n')) && responseUrl.StartsWith(UrlPrefix.TrimEnd('\r', '\n')))
                    {
                        XmlNode Encoding = ele.SelectSingleNode("ResponseEncoding");
                        if (Encoding != null) return System.Text.Encoding.GetEncoding(Encoding.InnerText);
                    }
            return Encoding.UTF8;
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
                foreach (string UrlPrefix in ele.GetAttribute("prefix").Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
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
                        if(SOAPAction!=null) req.Headers.Set("SOAPAction", SOAPAction.InnerText);
                        XmlNode KeepAlive = ele.SelectSingleNode("KeepAlive");
                        if (KeepAlive != null) req.KeepAlive = KeepAlive.InnerText.Trim() == "true";
                        XmlNode AllowAutoRedirect = ele.SelectSingleNode("AllowAutoRedirect");
                        if(AllowAutoRedirect!=null) req.AllowAutoRedirect= AllowAutoRedirect.InnerText.Trim() == "true";
                    }
            return req;
        }

        /// <summary>
        /// 把param里面的key和value值转化为url上的请求参数和值。
        /// </summary>
        /// <param name="param">请求参数</param>
        /// <param name="Encoding">编码方式</param>
        /// <returns>返回url上的请求参数和值</returns>
        static string QueryParamsMapToStr(Dictionary<string, string> param, Encoding Encoding = null)
        {
            StringBuilder sb = new StringBuilder();
            string connChar = "";
            foreach (KeyValuePair<string, string> kv in param)
            {
                sb.Append(connChar).Append(kv.Key).Append("=");
                if (kv.Key == "skey")
                    sb.Append(kv.Value);
                else
                    sb.Append(HttpUtility.UrlEncode(kv.Value, Encoding));
                connChar = "&";
            }
            return sb.ToString();
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
