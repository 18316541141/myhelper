using CommonHelper.FindAttr;
using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
namespace CommonHelper.Helper
{
    /// <summary>
    /// 寻找数据的帮助类
    /// </summary>
    public static class FindDataHelper
    {
        /// <summary>
        /// 从身份证号中提取性别信息
        /// </summary>
        /// <param name="idCardNum">身份证号</param>
        /// <returns>返回性别</returns>
        public static string FindSexFromIDCard(string idCardNum)
        {
            if (idCardNum.Length == 18)
                return Convert.ToInt32(idCardNum[16]) % 2 == 0 ? "女" : "男";
            else if (idCardNum.Length == 15)
                return Convert.ToInt32(idCardNum[14]) % 2 == 0 ? "女" : "男";
            else
                return "";
        }

        /// <summary>
        /// 从身份证号中提取出生日期
        /// </summary>
        /// <param name="idCardNum">身份证号</param>
        /// <returns>返回出生日期</returns>
        public static string FindBirthdayFromIDCard(string idCardNum)
        {
            if (idCardNum.Length == 18)
                return DateTime.ParseExact(idCardNum.Substring(6, 8), "yyyyMMdd", CultureInfo.CurrentCulture).ToString("yyyy-MM-dd");
            else if (idCardNum.Length == 15)
                return DateTime.ParseExact(idCardNum.Substring(6, 6), "yyMMdd", CultureInfo.CurrentCulture).ToString("yyyy-MM-dd");
            else
                return "";
        }

        /// <summary>
        /// 在一个html节点中，从多个xpath中寻找节点，找到则返回
        /// </summary>
        /// <param name="htmlNode">html节点</param>
        /// <param name="xpaths">多个xpath</param>
        /// <returns>返回最先匹配的节点</returns>
        public static HtmlNode FindDataByXPath(HtmlNode htmlNode, string[] xpaths)
        {
            HtmlNode retNode;
            foreach (string xpath in xpaths)
            {
                retNode = htmlNode.SelectSingleNode(xpath);
                if (retNode != null) return retNode;
            }
            return null;
        }

        /// <summary>
        /// 从json对象中使用jsonPath寻找json对象
        /// </summary>
        /// <param name="jtoken">json对象</param>
        /// <param name="jsonPaths">多个jsonPath</param>
        /// <returns>返回最先寻找到的结果</returns>
        public static JToken FindDataByJsonPath(JToken jtoken, string[] jsonPaths)
        {
            JToken retToken;
            foreach (string jsonPath in jsonPaths)
            {
                retToken = jtoken.SelectToken(jsonPath);
                if (retToken != null) return retToken;
            }
            return null;
        }

        /// <summary>
        /// 在一个xml节点中，从多个xpath中寻找节点，找到则返回
        /// </summary>
        /// <param name="xmlElement">xml节点</param>
        /// <param name="xpaths">多个xpath</param>
        /// <returns>返回最先匹配的节点</returns>
        public static XmlNode FindDataByXPath(XmlElement xmlElement, string[] xpaths)
        {
            XmlElement retEle;
            foreach (string xpath in xpaths)
            {
                retEle = (XmlElement)xmlElement.SelectSingleNode(xpath);
                if (retEle != null) return retEle;
            }
            return null;
        }

        /// <summary>
        /// 使用前缀查找数据，然后截取前缀，得到前缀后面的数据
        /// </summary>
        /// <param name="text">源文本</param>
        /// <param name="prefix">前缀</param>
        /// <returns>返回匹配结果</returns>
        public static string FindDataByPrefix(string text, string prefix)
        {
            int startIndex = text.IndexOf(prefix);
            if (startIndex == -1)
                throw new Exception($"前缀：“{prefix}”没有找到匹配结果。");
            startIndex = startIndex + prefix.Length;
            return text.Substring(startIndex);
        }

        /// <summary>
        /// 使用后缀查找数据，然后截取后缀，得到后缀前面的数据
        /// </summary>
        /// <param name="text"></param>
        /// <param name="suffix"></param>
        /// <returns></returns>
        public static string FindDataBySuffix(string text, string suffix)
        {
            int endIndex = text.IndexOf(suffix);
            if (endIndex == -1)
                throw new Exception($"后缀：“{suffix}”没有找到匹配结果。");
            return text.Substring(0, endIndex);
        }

        /// <summary>
        /// 使用前缀和后缀查找数据
        /// </summary>
        /// <param name="text"></param>
        /// <param name="prefix"></param>
        /// <param name="suffix"></param>
        /// <returns></returns>
        public static string FindDataByPrefixAndSuffix(string text, char prefix, char suffix)
        {
            int startIndex = text.IndexOf(prefix);
            if (startIndex == -1)
                throw new Exception($"前缀：“{prefix}”没有找到匹配结果。");
            startIndex = startIndex + 1;
            int endIndex = text.IndexOf(suffix, startIndex);
            if (endIndex == -1)
                throw new Exception($"后缀：“{suffix}”没有找到匹配结果。");
            return text.Substring(startIndex, endIndex - startIndex);
        }

        /// <summary>
        /// 使用前缀和后缀查找数据，然后截取前缀和后缀，得到中间的数据
        /// </summary>
        /// <param name="text">源文本</param>
        /// <param name="prefix">前缀</param>
        /// <param name="suffix">后缀</param>
        /// <param name="startIndex">数据起始位置</param>
        /// <param name="endIndex">数据结束位置</param>
        /// <returns>返回匹配结果</returns>
        public static string FindDataByPrefixAndSuffix(string text, string prefix, string suffix)
        {
            int startIndex = text.IndexOf(prefix);
            if (startIndex == -1)
                throw new Exception($"前缀：“{prefix}”没有找到匹配结果。");
            startIndex = startIndex + prefix.Length;
            int endIndex = text.IndexOf(suffix, startIndex);
            if (endIndex == -1)
                throw new Exception($"后缀：“{suffix}”没有找到匹配结果。");
            return text.Substring(startIndex, endIndex - startIndex);
        }

        /// <summary>
        /// 使用前缀和后缀查找数据，然后把匹配数据全部去除
        /// </summary>
        /// <param name="text">源文本</param>
        /// <param name="prefix">前缀</param>
        /// <param name="suffix">后缀</param>
        /// <returns>去除匹配项后返回的数据</returns>
        public static string RemoveAllByPrefixAndSuffix(string text, string prefix, string suffix)
        {
            for (int startFindIndex = 0;;)
            {
                int startIndex = text.IndexOf(prefix, startFindIndex);
                if (startIndex == -1)
                {
                    return text;
                }
                int endIndex = text.IndexOf(suffix, startIndex) + suffix.Length;
                if (endIndex == -1)
                {
                    return text;
                }
                text = text.Substring(0, startIndex) + text.Substring(endIndex);
                startFindIndex = startIndex;
            }
        }

        /// <summary>
        /// 使用前缀和后缀查找数据，然后把匹配数据替换为新数据，只替换首个匹配项。
        /// </summary>
        /// <param name="text">源文本</param>
        /// <param name="prefix">前缀</param>
        /// <param name="suffix">后缀</param>
        /// <param name="newStr">新数据</param>
        /// <returns>返回替换后的数据</returns>
        public static string ReplaceByPrefixAndSuffix(string text, string prefix, string suffix, string newStr)
        {
            int startIndex = text.IndexOf(prefix);
            if (startIndex == -1)
                throw new Exception($"前缀：“{prefix}”没有找到匹配结果。");
            int endIndex = text.IndexOf(suffix, startIndex)+ suffix.Length;
            if (endIndex == -1)
                throw new Exception($"后缀：“{suffix}”没有找到匹配结果。");
            return text.Substring(0, startIndex) + newStr + text.Substring(endIndex);
        }

        /// <summary>
        /// 使用前缀和后缀查找数据，然后截取前缀和后缀，得到中间的数据
        /// </summary>
        /// <param name="text">源文本</param>
        /// <param name="prefix">前缀</param>
        /// <param name="suffix">后缀</param>
        /// <param name="findStartIndex">起始查找位置</param>
        /// <param name="startIndex">数据起始位置</param>
        /// <param name="endIndex">数据结束位置</param>
        /// <returns>返回匹配结果</returns>
        public static string FindDataByPrefixAndSuffix(string text, string prefix, string suffix,int findStartIndex, out int startIndex, out int endIndex)
        {
            startIndex = text.IndexOf(prefix, findStartIndex);
            if (startIndex == -1)
                throw new Exception($"前缀：“{prefix}”没有找到匹配结果。");
            startIndex = startIndex + prefix.Length;
            endIndex = text.IndexOf(suffix, startIndex);
            if (endIndex == -1)
                throw new Exception($"后缀：“{suffix}”没有找到匹配结果。");
            return text.Substring(startIndex, endIndex - startIndex);
        }

        /// <summary>
        /// 从多组前缀和后缀查找数据
        /// </summary>
        /// <param name="text">源文本</param>
        /// <param name="prefixAndSuffix">多组前缀、后缀，两个一组。</param>
        /// <returns>当有匹配结果时返回，没有则抛出异常</returns>
        public static string FindDataByPrefixAndSuffix(string text, string[] prefixAndSuffix)
        {
            if (prefixAndSuffix.Length == 0) throw new Exception("没有任何前缀和后缀。");
            if (prefixAndSuffix.Length % 2 != 0) throw new Exception("前缀和后缀数量不一致。");
            Exception lastEx = null;
            for (int i = 0, len = prefixAndSuffix.Length; i < len; i += 2)
                try
                {
                    return FindDataByPrefixAndSuffix(text, prefixAndSuffix[i], prefixAndSuffix[i + 1]);
                }
                catch (Exception ex)
                {
                    lastEx = ex;
                    continue;
                }
            throw lastEx;
        }

        /// <summary>
        /// 使用前缀和后缀查找数据
        /// </summary>
        /// <param name="htmlNode">源html节点对象</param>
        /// <param name="prefix">前缀</param>
        /// <param name="suffix">后缀</param>
        /// <returns></returns>
        public static string FindDataByPrefixAndSuffix(HtmlNode htmlNode, string prefix, string suffix)
        {
            int startIndex, endIndex;
            return FindDataByPrefixAndSuffix(htmlNode.OuterHtml, prefix, suffix);
        }

        /// <summary>
        /// 使用前缀和后缀查找数据
        /// </summary>
        /// <param name="xmlNode">源xml节点对象</param>
        /// <param name="prefix">前缀</param>
        /// <param name="suffix">后缀</param>
        /// <returns></returns>
        public static string FindDataByPrefixAndSuffix(XmlNode xmlNode, string prefix, string suffix)
        {
            int startIndex, endIndex;
            return FindDataByPrefixAndSuffix(xmlNode.OuterXml, prefix, suffix);
        }

        /// <summary>
        /// 从文本中寻找数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="text"></param>
        /// <returns></returns>
        public static T FindDataByPrefixAndSuffix<T>(string text)
        {
            Type type = typeof(T);
            object obj = type.GetConstructor(Type.EmptyTypes).Invoke(null);
            FindDataConvert findDataConvert = null;
            if (type.IsDefined(typeof(FindDataConvertAttr), false))
            {
                FindDataConvertAttr findDataConvertAttr = (FindDataConvertAttr)type.GetCustomAttribute(typeof(FindDataConvertAttr));
                findDataConvert = (FindDataConvert)findDataConvertAttr.Type.GetConstructor(Type.EmptyTypes).Invoke(null);
            }
            foreach (PropertyInfo propertyInfo in type.GetProperties())
                foreach (Attribute attr in propertyInfo.GetCustomAttributes(false))
                    if (attr.GetType() == typeof(FindDataAttr))
                    {
                        FindDataAttr findDataAttr = (FindDataAttr)attr;
                        if (findDataAttr.RuleEnum == RuleEnum.PrefixSuffix)
                        {
                            string dataStr = FindDataByPrefixAndSuffix(text, findDataAttr.RuleArray);
                            dynamic convertAfter;
                            if (findDataConvert != null && findDataConvert.ConvertTo(propertyInfo.Name, dataStr, out convertAfter))
                                propertyInfo.SetValue(obj, convertAfter);
                            else
                                propertyInfo.SetValue(obj, string.IsNullOrEmpty(dataStr) || string.IsNullOrEmpty(dataStr.Trim()) ? findDataAttr.DefaultVal : dataStr);
                            continue;
                        }
                    }
            return (T)obj;
        }

        /// <summary>
        /// 从html中寻找数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="htmlDocument"></param>
        /// <returns></returns>
        public static T FindDataByXPath<T>(HtmlDocument htmlDocument)
        {
            HtmlNode htmlNode = htmlDocument.DocumentNode;
            Type type = typeof(T);
            object obj = type.GetConstructor(Type.EmptyTypes).Invoke(null);
            FindDataConvertAttr findDataConvertAttr = (FindDataConvertAttr)type.GetCustomAttribute(typeof(FindDataConvertAttr));
            FindDataConvert findDataConvert = (FindDataConvert)findDataConvertAttr.Type.GetConstructor(Type.EmptyTypes).Invoke(null);
            foreach (PropertyInfo propertyInfo in type.GetProperties())
                foreach (Attribute attr in propertyInfo.GetCustomAttributes(false))
                    if (attr.GetType() == typeof(FindDataAttr))
                    {
                        FindDataAttr findDataAttr = (FindDataAttr)attr;
                        if (findDataAttr.RuleEnum == RuleEnum.XPath)
                        {
                            HtmlNode node = FindDataByXPath(htmlNode, findDataAttr.RuleArray);
                            dynamic convertAfter;
                            if (findDataConvert.ConvertTo(propertyInfo.Name, node, out convertAfter))
                                propertyInfo.SetValue(obj, convertAfter);
                            else
                                propertyInfo.SetValue(obj, findDataAttr.DefaultVal);
                            continue;
                        }
                    }
            return (T)obj;
        }

        /// <summary>
        /// 从xml寻找数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jobject"></param>
        /// <returns></returns>
        public static T FindDataByXPath<T>(XmlDocument xmlDocument)
        {
            XmlElement xmlElement = xmlDocument.DocumentElement;
            Type type = typeof(T);
            object obj = type.GetConstructor(Type.EmptyTypes).Invoke(null);
            FindDataConvertAttr findDataConvertAttr = (FindDataConvertAttr)type.GetCustomAttribute(typeof(FindDataConvertAttr));
            FindDataConvert findDataConvert = (FindDataConvert)findDataConvertAttr.Type.GetConstructor(Type.EmptyTypes).Invoke(null);
            foreach (PropertyInfo propertyInfo in type.GetProperties())
                foreach (Attribute attr in propertyInfo.GetCustomAttributes(false))
                    if (attr.GetType() == typeof(FindDataAttr))
                    {
                        FindDataAttr findDataAttr = (FindDataAttr)attr;
                        if (findDataAttr.RuleEnum == RuleEnum.XPath)
                        {
                            XmlElement xmlEle = (XmlElement)FindDataByXPath(xmlElement, findDataAttr.RuleArray);
                            dynamic convertAfter;
                            if (findDataConvert.ConvertTo(propertyInfo.Name, xmlEle, out convertAfter))
                                propertyInfo.SetValue(obj, convertAfter);
                            else
                                propertyInfo.SetValue(obj, findDataAttr.DefaultVal);
                            continue;
                        }
                    }
            return (T)obj;
        }

        /// <summary>
        /// 从json对象寻找数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jobject"></param>
        /// <returns></returns>
        public static T FindDataByJsonPath<T>(JObject jobject)
        {
            Type type = typeof(T);
            object obj = type.GetConstructor(Type.EmptyTypes).Invoke(null);
            FindDataConvertAttr findDataConvertAttr = (FindDataConvertAttr)type.GetCustomAttribute(typeof(FindDataConvertAttr));
            FindDataConvert findDataConvert = (FindDataConvert)findDataConvertAttr.Type.GetConstructor(Type.EmptyTypes).Invoke(null);
            foreach (PropertyInfo propertyInfo in type.GetProperties())
                foreach (Attribute attr in propertyInfo.GetCustomAttributes(false))
                    if (attr.GetType() == typeof(FindDataAttr))
                    {
                        FindDataAttr findDataAttr = (FindDataAttr)attr;
                        if (findDataAttr.RuleEnum == RuleEnum.JsonPath)
                        {
                            JToken token = FindDataByJsonPath(jobject, findDataAttr.RuleArray);
                            dynamic convertAfter;
                            if (findDataConvert.ConvertTo(propertyInfo.Name, token, out convertAfter))
                                propertyInfo.SetValue(obj, convertAfter);
                            else
                                propertyInfo.SetValue(obj, findDataAttr.DefaultVal);
                            continue;
                        }
                    }
            return (T)obj;
        }
    }
}