using CommonHelper.FindAttr;
using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CommonHelper.FindAttr
{
    /// <summary>
    /// 对寻找到的数据进行类型转换的接口类
    /// </summary>
    public abstract class FindDataConvert
    {
        /// <summary>
        /// 转换方法，如果做过转换则返回true，否则返回false。
        /// </summary>
        /// <param name="propName">属性名称</param>
        /// <param name="value">属性值</param>
        /// <param name="convertAfter">返回转换后的值</param>
        /// <returns>返回是否做过转换</returns>
        public virtual bool ConvertTo(string propName, string value, out dynamic convertAfter)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 这是针对xpath寻找得到的数据使用的转换方法
        /// </summary>
        /// <param name="propName">属性名称</param>
        /// <param name="node">寻找到的xml节点</param>
        /// <param name="convertAfter">转换后的值</param>
        /// <returns>返回是否做过转换</returns>
        public virtual bool ConvertTo(string propName, XmlElement xmlEle, out dynamic convertAfter)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 这是针对xpath寻找html中的数据使用的转换方法
        /// </summary>
        /// <param name="propName">属性名称</param>
        /// <param name="node">寻找到的html节点</param>
        /// <param name="convertAfter">转换后的值</param>
        /// <returns>返回是否做过转换</returns>
        public virtual bool ConvertTo(string propName, HtmlNode node, out dynamic convertAfter)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 这是针对jsonPath寻找json对象中的数据使用的转换方法
        /// </summary>
        /// <param name="propName">属性名称</param>
        /// <param name="token">寻找到的json对象</param>
        /// <param name="convertAfter">转换后的值</param>
        /// <returns>返回是否做过转换</returns>
        public virtual bool ConvertTo(string propName,JToken token, out dynamic convertAfter)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 寻找数据后对数据进行转化的特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class FindDataConvertAttr :Attribute
    {
        public Type Type { set; get; }
        public FindDataConvertAttr(Type type)
        {
            Type = type;
        }
    }
}
