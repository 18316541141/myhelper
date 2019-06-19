using CommonHelper.Guess;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CommonHelper.Helper
{
    /// <summary>
    /// 猜想帮助类，日常生活中存在大量的信息都是含有*号的，例如手机号码：183****1141、
    /// 身份证：4406811993****5911，这些数据都可以根据它的生成规则来推算出完整号码
    /// </summary>
    public class GuessHelper
    {
        /// <summary>
        /// 所有可能的行政区划编码
        /// </summary>
        static GuessList _prcAreaCodeGuessList;

        /// <summary>
        /// 所有可能的年份
        /// </summary>
        static GuessList _yearGuessList;

        /// <summary>
        /// 所有可能的月、日
        /// </summary>
        static GuessList _monthDayGuessList;

        /// <summary>
        /// 所有可能的顺序号
        /// </summary>
        static GuessList _seqNumGuessList;

        /// <summary>
        /// 所有可能的身份证校验码
        /// </summary>
        static GuessList _checkCodeGuessList;

        static GuessHelper()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("Guess.xml");
            _prcAreaCodeGuessList = new GuessList(doc.SelectSingleNode("//PrcAreaCode").InnerText);
            _monthDayGuessList = new GuessList(doc.SelectSingleNode("//MonthDay").InnerText);
            _yearGuessList = new GuessList(1955, DateTime.Now.AddYears(-16).Year);
            _seqNumGuessList = new GuessList(1,999);

            List<string> checkCodeList = new List<string>();
            for (int i = 0; i <= 9; i++)
                checkCodeList.Add($"{i}");
            checkCodeList.Add("X");
            _checkCodeGuessList = new GuessList(checkCodeList);
        }

        /// <summary>
        /// 猜想含有*号的身份证号码的完整号码
        /// </summary>
        /// <param name="idCard18">18位身份证号</param>
        /// <param name="unknowChar">未知符号，通常是*号</param>
        /// <returns>返回可能的猜测结果</returns>
        public static List<string> GuessIDCard18(string idCard18,char unknowChar= '*')
        {
            if (idCard18.Length != 18)
                throw new Exception("这个不是18位身份证号。");
            List<string> allRet = new List<string>();
            string prcAreaCode = idCard18.Substring(0,6);
            List<string> prcAreaCodeGuessList = _prcAreaCodeGuessList.ExcludeNoMatching(prcAreaCode,unknowChar);
            if (prcAreaCodeGuessList.Count == 0) return allRet;
            string year=idCard18.Substring(6, 4);
            List<string> yearGuessList = _yearGuessList.ExcludeNoMatching(year, unknowChar);
            if (yearGuessList.Count == 0) return allRet;
            string monthDay=idCard18.Substring(10, 4);
            List<string> monthDayGuessList = _monthDayGuessList.ExcludeNoMatching(monthDay, unknowChar);
            if (monthDayGuessList.Count == 0) return allRet;
            string seqNum = idCard18.Substring(14,3);
            List<string> seqNumGuessList = _seqNumGuessList.ExcludeNoMatching(seqNum,unknowChar);
            if (seqNumGuessList.Count == 0) return allRet;
            string checkCode = idCard18.Substring(17,1);
            List<string> checkCodeGuessList = _checkCodeGuessList.ExcludeNoMatching(checkCode,unknowChar);
            if (checkCodeGuessList.Count == 0) return allRet;

            List<string>[] guessLists = new List<string>[] { prcAreaCodeGuessList, yearGuessList , monthDayGuessList, seqNumGuessList , checkCodeGuessList };

            int[] indexLens = new int[guessLists.Length];
            for (int i = 0, len = guessLists.Length; i < len; i++)
                indexLens[i] = guessLists[i].Count;

            List<int[]> allCombineList = CombineHelper.AllCombineList(indexLens);
            StringBuilder sb = new StringBuilder();
            string ret;
            foreach (int[] combineArray in allCombineList)
            {
                for (int i = 0, len = combineArray.Length; i < len; i++)
                    sb.Append(guessLists[i][combineArray[i]]);
                ret = sb.ToString();
                if (CheckHelper.IdCard18CheckCode(ret))
                    allRet.Add(ret);
                sb.Clear();
            }
            return allRet;
        }

        /// <summary>
        /// 猜测移动电话号码
        /// </summary>
        /// <param name="mobilePhone">11位移动电话号码</param>
        /// <param name="unknowChar">未知符号，通常是*号</param>
        /// <returns>返回可能的猜测结果</returns>
        public static List<string> GuessMobilePhone(string mobilePhone, char unknowChar = '*')
        {
            return null;
        }
    }
}