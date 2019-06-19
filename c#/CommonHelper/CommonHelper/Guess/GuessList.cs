using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper.Guess
{
    /// <summary>
    /// 自定义排除规则
    /// </summary>
    /// <param name="srcStr">预排除元素</param>
    /// <returns>如果返回true则保留、返回false则排除</returns>
    public delegate bool ExcludeRule(string srcStr);

    /// <summary>
    /// 猜想数组，用于保存或排除猜想的可能结果
    /// </summary>
    public class GuessList
    {
        /// <summary>
        /// 所有可能性的集合
        /// </summary>
        List<string> _srcList { get; set; }

        /// <summary>
        /// 直接传入所有可能的集合
        /// </summary>
        /// <param name="srcList"></param>
        public GuessList(List<string> srcList)
        {
            _srcList = srcList;
        }

        /// <summary>
        /// 当所有可能的集合是个连续的数字时使用该构造函数
        /// </summary>
        /// <param name="startNum">第一个数字</param>
        /// <param name="endNum">最后一个数字</param>
        /// <param name="fillLen">是否补足长度，使得所有数字长度统一</param>
        public GuessList(int startNum,int endNum,bool fillLen=true)
        {
            _srcList = new List<string>();
            int totalWidth=Convert.ToString(endNum).Length;
            for (; startNum <= endNum; startNum++)
                if(fillLen)
                    _srcList.Add($"{startNum}".PadLeft(3,'0'));
                else
                    _srcList.Add($"{startNum}");
        }

        /// <summary>
        /// 传入字符串数组的json数组，格式是：["字符串1","字符串2","字符串3"....]。作为所有可能的集合
        /// </summary>
        /// <param name="srcList"></param>
        public GuessList(JArray srcList)
        {
            _srcList = new List<string>();
            foreach (JValue jvalue in srcList)
                _srcList.Add(Convert.ToString(jvalue));
        }

        /// <summary>
        /// 传入格式是["字符串1","字符串2","字符串3"....]的json字符串，作为所有可能的集合
        /// </summary>
        /// <param name="jarrayStr"></param>
        public GuessList(string jarrayStr) : this(JArray.Parse(jarrayStr)){ }

        /// <summary>
        /// 排除不匹配的元素
        /// </summary>
        /// <param name="guessStr">要猜想的字符串</param>
        /// <param name="unknowChar">字符串中的未知字符</param>
        /// <returns></returns>
        public List<string> ExcludeNoMatching(string guessStr,char unknowChar)
        {
            List<string> restList=null;
            List<string> tempList=new List<string>(_srcList);
            for (int i=0,len= guessStr.Length;i<len ;i++)
            {
                restList = new List<string>();
                if (guessStr[i] != unknowChar)
                {
                    foreach (string str in tempList)
                        if (guessStr[i] == str[i])
                            restList.Add(str);
                    tempList = restList;
                }
            }
            return tempList;
        }

        /// <summary>
        /// 自定义一个排除规则对数据集进行排除
        /// </summary>
        /// <param name="excludeRule">自定义的排除规则</param>
        /// <returns>返回排除后的剩余的数据集</returns>
        public List<string> ExcludeNoMatching(ExcludeRule excludeRule)
        {
            List<string> restList = new List<string>();
            foreach (string str in _srcList)
                if (excludeRule(str)) restList.Add(str);
            return restList;
        }
    }
}
