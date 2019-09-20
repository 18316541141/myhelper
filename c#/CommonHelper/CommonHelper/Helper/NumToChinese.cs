using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper.Helper
{
    /// <summary>
    /// 数字转中文
    /// </summary>
    public static class NumToChinese
    {
        /// <summary>
        /// 数字中文
        /// </summary>
        readonly static Dictionary<char, char> ChineseNumMap;

        /// <summary>
        /// 数字单位
        /// </summary>
        readonly static char[] ChineseUnits;

        static NumToChinese()
        {

            ChineseNumMap = new Dictionary<char, char>
            {
                ['0'] = '〇',
                ['1'] = '一',
                ['2'] = '二',
                ['3'] = '三',
                ['4'] = '四',
                ['5'] = '五',
                ['6'] = '六',
                ['7'] = '七',
                ['8'] = '八',
                ['9'] = '九',
                ['.'] = '点'
            };
            ChineseUnits = new char[] {' ','十','百', '千', '万', '十', '百', '千', '亿', '十', '百', '千', '兆', '京', '十', '百', '千', '垓', '十', '百', '千', '秭', '十', '百', '千', '穰', '十', '百', '千', '沟', '十', '百', '千', '涧', '十', '百', '千', '正', '十', '百', '千', '载', '十', '百', '千', '极' };
        }

        /// <summary>
        /// 编号转中文，编号是指代号，逐个数字去读的
        /// </summary>
        /// <param name="no"></param>
        /// <returns></returns>
        public static string NoConvertToChinese(string no)
        {
            char[] chinese = new char[no.Length];
            for (int i = 0, len = no.Length; i < len; i++)
            {
                if (!ChineseNumMap.ContainsKey(no[i]))
                {
                    throw new Exception("该参数可能不是纯数字，只能包含数字0-9和小数点！");
                }
                chinese[i] = ChineseNumMap[no[i]];
            }
            return new string(chinese);
        }

        /// <summary>
        /// 数字转中文，数字转中文不能直接逐个数字去读，要按照个、十、百、位去读
        /// </summary>
        /// <param name="num">整数类型的数字</param>
        /// <returns></returns>
        public static string NumConvertToChinese(long num)
        {
            return NumConvertToChinese(Convert.ToString(num));
        }

        /// <summary>
        /// 数字转中文，数字转中文不能直接逐个数字去读，要按照个、十、百、位去读
        /// </summary>
        /// <param name="num">浮点类型的数字</param>
        /// <returns></returns>
        public static string NumConvertToChinese(double num)
        {
            return NumConvertToChinese(Convert.ToString(num));
        }

        /// <summary>
        /// 数字转中文，数字转中文不能直接逐个数字去读，要按照个、十、百、位去读，并且
        /// 按照口语习惯进行转换，例如“二”读作“两”
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string NumConvertToChineseSpeak(long num)
        {
            string ret = NumConvertToChinese(Convert.ToString(num));
            if (ret == "二")
            {
                ret = "两";
            }
            return ret;
        }

        /// <summary>
        /// 数字转中文，数字转中文不能直接逐个数字去读，要按照个、十、百、位去读
        /// </summary>
        /// <param name="num">字符串类型的数字</param>
        /// <returns></returns>
        public static string NumConvertToChinese(string num)
        {
            string beforeNumPart = num.Contains('.')?FindDataHelper.FindDataBySuffix(num, "."): num;
            StringBuilder beforeChinesePart = new StringBuilder();
            for (int j=0,i= beforeNumPart.Length-1;i>=0 ;i--,j++)
            {
                char n =ChineseNumMap[beforeNumPart[i]];
                if (n != '〇')
                {
                    beforeChinesePart.Insert(0, ChineseUnits[j]);
                }
                beforeChinesePart.Insert(0, n);
            }
            string full = beforeChinesePart.ToString();
            full = full.StartsWith("一十") ? full.Substring(1) : full;
            while (full.IndexOf("〇〇") > -1)
            {
                full = full.Replace("〇〇", "〇");
            }
            if (!full.StartsWith("〇"))
            {
                full = full.TrimEnd('〇');
            }
            string afterChinesePart = num.Contains('.')? "点" + NoConvertToChinese(FindDataHelper.FindDataByPrefix(num, ".")):"";
            return full+ afterChinesePart;
        }
    }
}
