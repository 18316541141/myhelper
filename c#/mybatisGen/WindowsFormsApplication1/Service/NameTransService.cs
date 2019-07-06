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
    public class NameTransService
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
        /// 下划线命名转大驼峰命名法，USER_ACCOUNT -> UserAccount
        /// </summary>
        /// <param name="underline"></param>
        /// <returns></returns>
        public string UnderlineToBigHump(string underline)
        {
            return HumpToBigHump(UnderlineToHump(underline));
        }
    }
}
