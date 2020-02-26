using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CommonHelper.Helper
{
    /// <summary>
    /// 校验帮助类
    /// </summary>
    public static class CheckHelper
    {
        /// <summary>
        /// 检查是否存在重复，用于校验一组数据是否重复
        /// </summary>
        /// <param name="stringList"></param>
        /// <returns></returns>
        public static bool CheckRepeat(List<string> stringList)
        {
            HashSet<string> hashSet = new HashSet<string>();
            foreach (string str in stringList)
            {
                if (string.IsNullOrEmpty(str))
                {
                    hashSet.Add("");
                }
                else
                {
                    hashSet.Add(str.Trim());
                }
            }
            return hashSet.Count < stringList.Count;
        }

        /// <summary>
        /// 检查是否为正确的url格式
        /// </summary>
        /// <param name="url">url字符串</param>
        /// <returns>如果正确返回true</returns>
        public static bool IsUrl(string url)
        {
            Uri result;
            return Uri.TryCreate(url, UriKind.Absolute,out result);
        }

        /// <summary>
        /// 检查是否为移动电话号码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsMobilePhone(this string str)=>new Regex("^(13[0-9]|14[5-9]|15[012356789]|166|17[0-8]|18[0-9]|19[1-9])[0-9]{8}$").IsMatch(str);

        /// <summary>
        /// 军官证校验
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsCertOfOfficers(string str)=>new Regex("^南字第(\\d{8})号|北字第(\\d{8})号|沈字第(\\d{8})号|兰字第(\\d{8})号|成字第(\\d{8})号|济字第(\\d{8})号|广字第(\\d{8})号|海字第(\\d{8})号|空字第(\\d{8})号|参字第(\\d{8})号|政字第(\\d{8})号|后字第(\\d{8})号|装字第(\\d{8})号$").IsMatch(str);

        /// <summary>
        /// 港澳通行证校验
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsHKAndMCPassport(string str) => new Regex("^[HMhm]{1}([0-9]{10}|[0-9]{8})$").IsMatch(str);

        /// <summary>
        /// 检查是否为固定电话号码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsPhone(this string str)
        {
            Regex reg2 = new Regex(@"(^[0-9]{3,4}\-[0-9]{3,8}$)|(^[0-9]{3,8}$)|(^\([0-9]{3,4}\)[0-9]{3,8}$)|(^0{0,1}13[0-9]{9}$)");
            if (!reg2.IsMatch(str))
                return false;
            else
                return true;
        }

        /// <summary>
        /// 检查字符串是不是工商登记号，15位或18位都可以检测到。
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static bool IsBusinessRegisterCode(this string code)
        {
            return IsBusinessRegisterCode18(code) || IsBusinessRegisterCode15(code);
        }

        /// <summary>
        /// 检查字符串是不是18位工商登记号
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsBusinessRegisterCode18(this string code)
        {
            if (code.Length != 18)
                return false;
            Regex reg = new Regex("^([0-9ABCDEFGHJKLMNPQRTUWXY]{2})([0-9]{6})([0-9ABCDEFGHJKLMNPQRTUWXY]{9})([0-9Y])$");
            if (!reg.IsMatch(code))
                return false;
            string str = "0123456789ABCDEFGHJKLMNPQRTUWXY";
            int[] ws = { 1, 3, 9, 27, 19, 26, 16, 17, 20, 29, 25, 13, 8, 24, 10, 30, 28 };
            string[] codes = new string[2];
            codes[0] = code.Substring(0, code.Length - 1);
            codes[1] = code.Substring(code.Length - 1);
            int sum = 0;
            for (var i = 0; i < 17; i++)
                sum += str.IndexOf(codes[0][i]) * ws[i];
            dynamic c18 = 31 - (sum % 31);
            if (c18 == 31)
                c18 = "Y";
            else if (c18 == 30)
                c18 = "0";
            if (!codes[1].Equals(c18))
                return false;
            return true;
        }

        /// <summary>
        /// 检验是否为15位的工商登记号
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsBusinessRegisterCode15(this string str)
        {
            if ("".Equals(str) || " ".Equals(str)) return false;
            else if (str.Length != 15) return false;
            else if (1 == getCheckCode(str, false)) return true;
            return false;
        }

        static int getCheckCode(string businessLicense, bool getCheckCode)
        {
            int result = -1;
            if (null == businessLicense || businessLicense.Trim().Equals("") || businessLicense.Length != 15)
                return result;
            else
            {
                int ti = 0;
                int si = 0; // pi|11+ti
                int cj = 0; // （si||10==0？10：si||10）*2
                int pj = 10; // pj=cj|11==0?10:cj|11
                for (int i = 0; i < businessLicense.Length; i++)
                {
                    ti = Convert.ToInt32(businessLicense.Substring(i, 1));
                    si = pj + ti;
                    cj = (0 == si % 10 ? 10 : si % 10) * 2;
                    pj = (cj % 11) == 0 ? 10 : (cj % 11);
                    if (i == businessLicense.Length - 2 && getCheckCode)
                    {
                        result = (1 - pj < 0 ? 11 - pj : 1 - pj) % 10;// 返回营业执照注册号的校验码
                        return result;
                    }
                    if (i == businessLicense.Length - 1)
                    {
                        result = si % 10; // 返回1 表示是一个有效营业执照号
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 检查字符串是不是为“统一社会信用代码”
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsUnifiedSocialCreditCode(this string Code)
        {
            Regex patrn = new Regex("^[0-9A-Z]+$");
            //18位校验及大写校验
            if ((Code.Length != 18) || (patrn.IsMatch(Code) == false)) return false;
            else
            {
                string Ancode;//统一社会信用代码的每一个值
                int Ancodevalue;//统一社会信用代码每一个值的权重 
                int total = 0;
                int[] weightedfactors = new int[] { 1, 3, 9, 27, 19, 26, 16, 17, 20, 29, 25, 13, 8, 24, 10, 30, 28 };//加权因子 
                string str = "0123456789ABCDEFGHJKLMNPQRTUWXY";
                //不用I、O、S、V、Z 
                for (var i = 0; i < Code.Length - 1; i++)
                {
                    Ancode = Code.Substring(i, 1);
                    Ancodevalue = str.IndexOf(Ancode);
                    total = total + Ancodevalue * weightedfactors[i];
                    //权重与加权因子相乘之和 
                }
                dynamic logiccheckcode = 31 - total % 31;
                if (logiccheckcode == 31)
                    logiccheckcode = 0;
                string Str = "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,J,K,L,M,N,P,Q,R,T,U,W,X,Y";
                string[] Array_Str = Str.Split(',');
                logiccheckcode = Array_Str[logiccheckcode];
                var checkcode = Code.Substring(17, 1);
                return logiccheckcode.Equals(checkcode);
            }
        }

        /// <summary>
        /// 检查字符串是不是组织机构代码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsOrganCode(this string code)
        {
            if (code == null) return false;
            code = code.ToUpper();

            int[] ws = { 3, 7, 9, 10, 5, 8, 4, 2 };
            string str = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Regex regex = new Regex("^[0-9A-Z]{8}\\-[0-9X]$");
            if (!regex.IsMatch(code)) return false;
            int sum = 0;
            for (int i = 0; i < 8; i++)
                sum += str.IndexOf(code[i]) * ws[i];

            int c9 = 11 - (sum % 11);
            string sc9 = Convert.ToString(c9);

            if (11 == c9) sc9 = "0";
            else if (10 == c9) sc9 = "X";
            return sc9.Equals(Convert.ToString(code[9]));
        }

        /// <summary>
        /// 检查号牌号码是否合法
        /// </summary>
        /// <param name="plateNo">号牌号码</param>
        /// <returns>如果号牌号码合法则返回true，否则返回false</returns>
        public static bool CheckPlateNo(string plateNo) =>
            Regex.IsMatch(plateNo, "^(([京津沪渝冀豫云辽黑湘皖鲁新苏浙赣鄂桂甘晋蒙陕吉闽贵粤青藏川宁琼使领][A - Z](([0 - 9]{ 5}[DF])|([DF]([A-HJ-NP-Z0-9])[0-9]{4})))|([京津沪渝冀豫云辽黑湘皖鲁新苏浙赣鄂桂甘晋蒙陕吉闽贵粤青藏川宁琼使领][A-Z][A-HJ-NP-Z0-9]{4}[A-HJ-NP-Z0-9挂学警港澳使领]))$");

        /// <summary>
        /// 15位身份证验证
        /// </summary>
        /// <param name="Id">身份证号</param>
        /// <returns></returns>
        public static bool CheckIDCard15(string Id)
        {
            long n = 0;
            if (long.TryParse(Id, out n) == false || n < Math.Pow(10, 14))
                return false;//数字验证
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(Id.Remove(2)) == -1)
                return false;//省份验证
            string birth = Id.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
                return false;//生日验证
            return true;//符合15位身份证标准
        }

        /// <summary>
        /// 18位身份证验证
        /// </summary>
        /// <param name="Id">身份证号</param>
        /// <returns></returns>
        public static bool CheckIDCard18(string Id)
        {
            if (string.IsNullOrEmpty(Id) || Id.Length != 18)
            {
                return false;
            }
            long n = 0;
            if (long.TryParse(Id.Remove(17), out n) == false || n < Math.Pow(10, 16) || long.TryParse(Id.Replace('x', '0').Replace('X', '0'), out n) == false)
                return false;//数字验证
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(Id.Remove(2)) == -1)
                return false;//省份验证
            string birth = Id.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
                return false;//生日验证
            if (!IdCard18CheckCode(Id))
                return false;//校验码验证
            return true;//符合GB11643-1999标准
        }

        /// <summary>
        /// 身份证后6位验证
        /// </summary>
        /// <param name="Id">身份证号</param>
        /// <returns></returns>
        public static bool CheckIDCardEnd6(string id)
        {
            Regex idCardEnd6Match = new Regex("\\d{5}[\\dXx]");
            if (idCardEnd6Match.IsMatch(id))
            {
                int day = Convert.ToInt32(id.Substring(0, 2));
                return day > 0 || day < 32;
            }
            return false;
        }

        /// <summary>
        /// 18位身份证的校验权值
        /// </summary>
        static int[] _idCard18CheckWeight = new int[] { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };

        /// <summary>
        /// 18位身份证对应余数的校验码
        /// </summary>
        static char[] _arrVarifyCode = new char[] { '1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2' };

        /// <summary>
        /// 检查该身份证的第18位校验码和证件是否校验通过，如果
        /// 校验通过返回true，否则返回false
        /// </summary>
        /// <param name="idCard18">18位的证件号码</param>
        /// <returns></returns>
        public static bool IdCard18CheckCode(string idCard18)
        {
            idCard18 = idCard18.ToUpper();
            int sum=0;
            for (int i=0;i<17 ;i++)
                sum += Convert.ToInt32($"{idCard18[i]}") * _idCard18CheckWeight[i];
            return _arrVarifyCode[sum % 11]==idCard18[17];
        }

        /// <summary>
        /// 检查字符串是不是身份证，包括15、18位
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static bool CheckIDCard(this string Id)
        {
            if (Id.Length == 15) return CheckIDCard15(Id);
            else if (Id.Length == 18) return CheckIDCard18(Id);
            return false;
        }

        /// <summary>
        /// 校验ipv4格式是否合法
        /// </summary>
        /// <param name="ipv4">ipv4地址</param>
        /// <returns>如果合法返回true</returns>
        public static bool CheckIpv4(string ipv4)=>
            new Regex(@"^(25[0-5]|2[0-4]\d|[0-1]?\d?\d)(\.(25[0-5]|2[0-4]\d|[0-1]?\d?\d)){3}$").IsMatch(Convert.ToString(ipv4));

        /// <summary>
        /// 校验mac格式是否合法
        /// </summary>
        /// <param name="mac">mac地址</param>
        /// <returns>如果合法返回true</returns>
        public static bool CheckMac(string mac)
        {
            return new Regex("^((([a-fA-F0-9]{2}:){5})|(([a-fA-F0-9]{2}-){5}))[a-fA-F0-9]{2}$").IsMatch(Convert.ToString(mac));
        }

        /// <summary>
        /// 校验端口号是否合法
        /// </summary>
        /// <param name="port">端口号</param>
        /// <returns>如果合法返回true</returns>
        public static bool CheckPort(string port)
        {
            int portNum;
            if (int.TryParse(port, out portNum))
            {
                if (portNum < 0 || portNum > 65535)
                    return false;
            }
            else
                return false;
            return true;
        }

        /// <summary>
        /// 校验银行卡号是否合法
        /// </summary>
        /// <param name="bankCardCode"></param>
        /// <returns></returns>
        public static bool CheckBankCardCode(string bankCardCode)
        {
            Regex regex = new Regex("^\\d{14,19}$");
            if (regex.IsMatch(bankCardCode))
            {
                int sum = 0;
                for (int i=0,num,len_i= bankCardCode.Length; i<len_i;i++)
                {
                    num = Convert.ToInt32(Convert.ToString(bankCardCode[i]));
                    if (i % 2 == 0)
                    {
                        string tempNum = Convert.ToString(num * 2);
                        for (int j=0,len_j=tempNum.Length;j<len_j ;j++)
                        {
                            sum += Convert.ToInt32(Convert.ToString(tempNum[j]));
                        }
                    }
                    else
                    {
                        sum += num;
                    }
                }
                return sum % 10 == 0;
            }
            return false;
        }
    }
}