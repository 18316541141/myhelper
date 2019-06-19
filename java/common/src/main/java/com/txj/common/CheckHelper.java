package com.txj.common;

import java.net.URI;
import java.text.DateFormat;
import java.text.SimpleDateFormat;

public class CheckHelper {
	
	/**
	 * 检查是否为正确的url格式
	 * @param url
	 * @return
	 */
	public static boolean isUrl(String url)
    {
		try {			
			URI.create(url);
			return true;
		} catch (Exception e) {
			return false;
		}
    }
	
	/**
	 * 检查是否为移动电话号码
	 * @param phone
	 * @return
	 */
	public static boolean isMobilePhone(String mobilePhone){
		return String.valueOf(mobilePhone).matches("^(13[0-9]|14[5-9]|15[012356789]|166|17[0-8]|18[0-9]|19[8-9])[0-9]{8}$");	
	}
	
	/**
	 * 军官证校验
	 * @param certOfOfficers
	 * @return
	 */
	public static boolean isCertOfOfficers(String certOfOfficers){
		return String.valueOf(certOfOfficers).matches("^南字第(\\d{8})号|北字第(\\d{8})号|沈字第(\\d{8})号|兰字第(\\d{8})号|成字第(\\d{8})号|济字第(\\d{8})号|广字第(\\d{8})号|海字第(\\d{8})号|空字第(\\d{8})号|参字第(\\d{8})号|政字第(\\d{8})号|后字第(\\d{8})号|装字第(\\d{8})号$");
	}
	
	/**
	 * 港澳通行证校验
	 * @param passport
	 * @return
	 */
	public static boolean isHKAndMCPassport(String passport){
		return String.valueOf(passport).matches("^[HMhm]{1}([0-9]{10}|[0-9]{8})$");
	}
	
	/**
	 * 检查是否为固定电话号码
	 * @param phone
	 * @return
	 */
	public static boolean isPhone(String phone){
		return String.valueOf(phone).matches("(^[0-9]{3,4}\\-[0-9]{3,8}$)|(^[0-9]{3,8}$)|(^\\([0-9]{3,4}\\)[0-9]{3,8}$)|(^0{0,1}13[0-9]{9}$)");
	}
	
	/**
	 * 检查字符串是不是18位工商登记号
	 * @param businessRegisterCode
	 * @return
	 */
	public static boolean IsBusinessRegisterCode18(String businessRegisterCode){
		if (businessRegisterCode.length() != 18)
            return false;
        if (!businessRegisterCode.matches("^([0-9ABCDEFGHJKLMNPQRTUWXY]{2})([0-9]{6})([0-9ABCDEFGHJKLMNPQRTUWXY]{9})([0-9Y])$"))
            return false;
        String str = "0123456789ABCDEFGHJKLMNPQRTUWXY";
        int[] ws = { 1, 3, 9, 27, 19, 26, 16, 17, 20, 29, 25, 13, 8, 24, 10, 30, 28 };
        String[] codes = new String[2];
        codes[0] = businessRegisterCode.substring(0, businessRegisterCode.length() - 1);
        codes[1] = businessRegisterCode.substring(businessRegisterCode.length() - 1);
        int sum = 0;
        for (int i = 0; i < 17; i++)
            sum += str.indexOf(codes[0].charAt(i)) * ws[i];
        int c18 = 31 - (sum % 31);
        String c18Temp=null;
        if (c18 == 31)
        	c18Temp = "Y";
        else if (c18 == 30)
        	c18Temp = "0";
        else
        	c18Temp=String.valueOf(c18);
        if (!codes[1].equals(c18Temp))
            return false;
        return true;
	}
	
	/**
	 * 检验是否为15位的工商登记号
	 * @param businessRegisterCode
	 * @return
	 */
    public static boolean isBusinessRegisterCode15(String businessRegisterCode)
    {
        if ("".equals(businessRegisterCode) || " ".equals(businessRegisterCode)) return false;
        else if (businessRegisterCode.length()!= 15) return false;
        else if (1 == getCheckCode(businessRegisterCode, false)) return true;
        return false;
    }
    
    static int getCheckCode(String businessLicense, boolean getCheckCode)
    {
        int result = -1;
        if (null == businessLicense || businessLicense.trim().equals("") || businessLicense.length() != 15)
            return result;
        else
        {
            int ti = 0;
            int si = 0; // pi|11+ti
            int cj = 0; // （si||10==0？10：si||10）*2
            int pj = 10; // pj=cj|11==0?10:cj|11
            for (int i = 0; i < businessLicense.length(); i++)
            {
                ti = Integer.parseInt(businessLicense.substring(i, 1));
                si = pj + ti;
                cj = (0 == si % 10 ? 10 : si % 10) * 2;
                pj = (cj % 11) == 0 ? 10 : (cj % 11);
                if (i == businessLicense.length() - 2 && getCheckCode)
                {
                    result = (1 - pj < 0 ? 11 - pj : 1 - pj) % 10;// 返回营业执照注册号的校验码
                    return result;
                }
                if (i == businessLicense.length() - 1)
                {
                    result = si % 10; // 返回1 表示是一个有效营业执照号
                }
            }
        }
        return result;
    }
    
    /**
     * 检查字符串是不是为“统一社会信用代码”
     * @param unifiedSocialCreditCode
     * @return
     */
    public static boolean isUnifiedSocialCreditCode(String unifiedSocialCreditCode)
    {
        //18位校验及大写校验
        if (unifiedSocialCreditCode.length() != 18 || unifiedSocialCreditCode.matches("^[0-9A-Z]+$") == false) return false;
        else
        {
            String Ancode;//统一社会信用代码的每一个值
            int Ancodevalue;//统一社会信用代码每一个值的权重 
            int total = 0;
            int[] weightedfactors = new int[] { 1, 3, 9, 27, 19, 26, 16, 17, 20, 29, 25, 13, 8, 24, 10, 30, 28 };//加权因子 
            String str = "0123456789ABCDEFGHJKLMNPQRTUWXY";
            //不用I、O、S、V、Z 
            for (int i = 0; i < unifiedSocialCreditCode.length() - 1; i++)
            {
                Ancode = unifiedSocialCreditCode.substring(i, 1);
                Ancodevalue = str.indexOf(Ancode);
                total = total + Ancodevalue * weightedfactors[i];
                //权重与加权因子相乘之和 
            }
            int logiccheckcode = 31 - total % 31;
            if (logiccheckcode == 31)
                logiccheckcode = 0;
            String Str = "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,J,K,L,M,N,P,Q,R,T,U,W,X,Y";
            String[] Array_Str = Str.split(",");
            String logiccheckcodeStr = Array_Str[logiccheckcode];
            String checkcode = unifiedSocialCreditCode.substring(17, 1);
            return logiccheckcodeStr.equals(checkcode);
        }
    }

    /**
     * 检查字符串是不是组织机构代码
     * @param organCode
     * @return
     */
    public static boolean isOrganCode(String organCode)
    {
        if (organCode == null) return false;
        organCode = organCode.toUpperCase();

        int[] ws = { 3, 7, 9, 10, 5, 8, 4, 2 };
        String str = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        if (!organCode.matches("^[0-9A-Z]{8}\\-[0-9X]$")) return false;
        int sum = 0;
        for (int i = 0; i < 8; i++)
            sum += str.indexOf(organCode.charAt(i)) * ws[i];

        int c9 = 11 - (sum % 11);
        String sc9 = String.valueOf(c9);

        if (11 == c9) sc9 = "0";
        else if (10 == c9) sc9 = "X";
        return sc9.equals(String.valueOf(organCode.charAt(9)));
    }
    
    /**
     * 检查号牌号码是否合法
     * @param plateNo
     * @return
     */
    public static boolean checkPlateNo(String plateNo){
    	return String.valueOf(plateNo).matches("^(([京津沪渝冀豫云辽黑湘皖鲁新苏浙赣鄂桂甘晋蒙陕吉闽贵粤青藏川宁琼使领][A - Z](([0 - 9]{ 5}[DF])|([DF]([A-HJ-NP-Z0-9])[0-9]{4})))|([京津沪渝冀豫云辽黑湘皖鲁新苏浙赣鄂桂甘晋蒙陕吉闽贵粤青藏川宁琼使领][A-Z][A-HJ-NP-Z0-9]{4}[A-HJ-NP-Z0-9挂学警港澳使领]))$");
    }
    
    /**
     * 15位身份证验证
     * @param Id
     * @return
     */
    public static boolean checkIDCard15(String Id)
    {
        long n = 0;
        try {			
        	if (Long.parseLong(Id) < Math.pow(10, 14))
        		return false;//数字验证
		} catch (Exception e) {
				return false;
		}
        String address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
        if (address.indexOf(Id.substring(0, 2)) == -1)
            return false;//省份验证
        String birth = new StringBuilder(Id.substring(6, 12)).insert(4, "-").insert(2, "-").toString();
        DateFormat dateFormat=new SimpleDateFormat("yyyy-MM-dd");
        try {
        	dateFormat.parse(birth);
        	return true;
		} catch (Exception e) {
			return false;
		}
    }
    
    /**
     * 18位身份证验证
     * @param id
     * @return
     */
    public static boolean checkIDCard18(String id) {
        char[] ch = id.toCharArray();
        boolean flag1 = verForm(id);
        boolean flag2 = verify(ch);
        if (flag1 == true && flag2 == true) {
            return true;
        }
        return false;
    }
    
    private static boolean verForm(String num) {
        String reg = "^\\d{15}$|^\\d{17}[0-9Xx]$";
        if (!num.matches(reg)) {
            return false;
        }
        return true;
    }
    
    /**
     * 校验端口号是否合法
     * @param port	端口号
     * @return
     */
    public static boolean checkPort(String port)
    {
        try {			
        	int intPort=Integer.parseInt(port);
        	if (intPort < 0 || intPort > 65535)
        		return false;
        	return true;
		} catch (Exception e) {
			return false;
		}
    }
    
    private static boolean verify(char[] id) {
        int sum = 0;
        int w[] = { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };
        char[] ch = { '1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2' };
        for (int i = 0; i < id.length - 1; i++) {
            sum += (id[i] - '0') * w[i];
        }
        int c = sum % 11;
        char code = ch[c];
        char last = id[id.length - 1];
        last = last == 'x' ? 'X' : last;
        return last == code;
    }
}