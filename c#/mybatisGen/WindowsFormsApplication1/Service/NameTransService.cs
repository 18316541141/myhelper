using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.Service
{
    /// <summary>
    /// 命名法转化服务
    /// </summary>
    public sealed class NameTransService
    {
        /// <summary>
        /// 下划线命名法转驼峰命名法，例如：USER_ACCOUNT -> userAccount
        /// </summary>
        /// <param name="underline">下划线命名法变量</param>
        /// <returns></returns>
        public string UnderlineToHump(string underline)
        {
            underline = underline.ToLower();
            int lineIndex;
            string before;
            while ((lineIndex = underline.IndexOf("_")) >-1)
            {
                before= new string(new char[] { '_', underline[lineIndex + 1]});
                underline = underline.Replace(before, underline[lineIndex + 1].ToString().ToUpper());
            }
            return underline;
        }

        /// <summary>
        /// 驼峰命名法转大驼峰命名法，userAccount -> UserAccount
        /// </summary>
        /// <returns></returns>
        public string HumpToBigHump(string hump)
        {
            return hump[0].ToString().ToUpper()+hump.Substring(1, hump.Length-1);
        }

        /// <summary>
        /// 大驼峰命名法转驼峰命名法，UserAccount -> userAccount
        /// </summary>
        /// <param name="bigHump"></param>
        /// <returns></returns>
        public string BigHumpToHump(string bigHump)
        {
            return bigHump[0].ToString().ToLower() + bigHump.Substring(1, bigHump.Length - 1);
        }

        /// <summary>
        /// 下划线命名转大驼峰命名法，USER_ACCOUNT -> UserAccount
        /// </summary>
        /// <param name="underline"></param>
        /// <returns></returns>
        public string UnderlineToBigHump(string underline)
        {
            return HumpToBigHump(UnderlineToHump(underline));
        }

        /// <summary>
        /// 把单数字母转复数形式
        ///     1.以“s"“x”“ch”“sh”结尾的加“es”
        ///     2.以辅音字母加“y”结尾的变“y”为“i”再加“es”
        ///     3.以“f”或“fe”结尾的变“f”“fe”为“v”再加“es”
        ///     4.其他的直接加s，特殊情况忽略不计
        /// </summary>
        /// <param name="single"></param>
        /// <returns></returns>
        public string SingleToComplex(string single)
        {
            if (single.EndsWith("s") || single.EndsWith("x") || single.EndsWith("ch") || single.EndsWith("sh"))
            {
                return single + "es";
            }
            else if (single.EndsWith("y") && !"aeiou".Contains(single[single.Length - 2]))
            {
                return single.Substring(0, single.Length-1) + "ies";
            }
            else if (single.EndsWith("f"))
            {
                return single.Substring(0, single.Length - 1) + "ves";
            }
            else if (single.EndsWith("fe"))
            {
                return single.Substring(0, single.Length - 2) + "ves";
            }
            else
            {
                return single+"s";
            }
        }
    }
}
