using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper.FindAttr
{
    /// <summary>
    /// 规则枚举
    /// </summary>
    public enum RuleEnum
    {
        XPath,JsonPath, PrefixSuffix
    }

    /// <summary>
    /// 寻找数据的特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple =true)]
    public class FindDataAttr : Attribute
    {
        /// <summary>
        /// 对于前缀后缀匹配规则，必须是前缀后缀为一组。
        /// </summary>
        /// <param name="ruleEnum">枚举规则</param>
        /// <param name="ruleArray">候选规则，可以配置多个，仅用于JsonPath, PrefixSuffix</param>
        public FindDataAttr(RuleEnum ruleEnum,params string[] ruleArray)
        {
            RuleEnum = ruleEnum;
            RuleArray = ruleArray;
        }

        public RuleEnum RuleEnum {private set; get; }

        /// <summary>
        /// 候选规则，可以配置多个
        /// </summary>
        public string[] RuleArray { private set; get; }

        /// <summary>
        /// 默认值
        /// </summary>
        public object DefaultVal{ set; get; }
    }
}