package com.txj.common;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.Random;
public class PasswordHelper {
	/**
	 * 密码强度等级
	 * 
	 * @author admin
	 *
	 */
	public enum PasswordLevel {
		None, // 空密码
		Very_Week, // 安全性很低
		Week, // 安全性较低
		Good, // 安全性一般
		Strong, // 安全性较好
		Very_Strong // 安全性非常好
	}

	/**
	 * 生成随机密码
	 * 
	 * @param len
	 *            密码长度
	 * @param type
	 *            密码类型，生成规则，规则如下： 001（只含数字）、010（只含小写字母）、100（只含大写字母）；或者左边情况的混合。
	 * @return
	 */
	public static String RandomPassword(int len, int type) {
		List<Character> randomCharArray = new ArrayList<Character>();
		if ((type & 1) == 1)
			randomCharArray.addAll(Arrays.asList(new Character[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' }));
		if (((type >> 1) & 1) == 1)
			randomCharArray.addAll(Arrays.asList(new Character[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
					'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' }));
		if (((type >> 2) & 1) == 1)
			randomCharArray.addAll(Arrays.asList(new Character[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
					'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' }));
		String str = "";
		Random next = new Random();
		for (int i = 0; i < len; i++)
			str += randomCharArray.get(next.nextInt(randomCharArray.size()));
		return str;
	}

	/**
	 * 获取密码强度等级
	 * 
	 * @param paswword
	 * @return
	 */
	public static PasswordLevel getPasswordLevel(String paswword) {
		int nScore = getPasswordScore(paswword);
		if (nScore >= 0 && nScore < 20)
			return PasswordLevel.Very_Week;
		if (nScore >= 20 && nScore < 40)
			return PasswordLevel.Week;
		if (nScore >= 40 && nScore < 60)
			return PasswordLevel.Good;
		if (nScore >= 60 && nScore < 80)
			return PasswordLevel.Strong;
		if (nScore >= 80)
			return PasswordLevel.Very_Strong;
		return PasswordLevel.None;
	}

	/**
	 * 获取密码强度评分
	 * @param password	密码
	 * @return	返回强度评分
	 */
    public static int getPasswordScore(String password)
    {
        password = password.replaceAll("s+",""); //去空格
        if (password.length() == 0) return 0;
        String lowerPasswordString = password.toLowerCase();//全部转为小写形式
        int score = 0;//得分分数
        int nLen = password.length();//密码字符总个数
        int nAlphaUC = 0; //大写英文字母个数
        int nAlphaLC = 0;  //小写英文字母个数
        int nNumber = 0;  //数字字符个数
        int nSymbol = 0;  //特殊字符个数
        int nMidChar = 0; //密码中间部分出现数字或者特殊字符的个数
        int nRequirements = 0;//达到最低需求四个条件的个数
        int nRepChar = 0; //重复字符的个数
        int nConsecAlphaUC = 0; //连续大写字母个数
        int nConsecAlphaLC = 0; //连续小写字母个数
        int nConsecNumber = 0;  //连续数字字符个数
        int nSeqAlpha = 0;//依顺序出现的字母序列的个数
        int nSeqNumber = 0;//依顺序出现的数字序列的个数
        int nTmpAlphaUC = 0;
        int nTmpAlphaLC = 0;
        int nTmpNumber = 0;

        for (int i = 0; i < nLen; i++)
        {
            if (Character.isUpperCase(password.charAt(i)))
            {
                if (nTmpAlphaUC != 0) if ((nTmpAlphaUC + 1) == i) nConsecAlphaUC++;
                nTmpAlphaUC = i;
                nAlphaUC++;
            }
            else if (Character.isLowerCase(password.charAt(i)))
            {
                if (nTmpAlphaLC != 0) if ((nTmpAlphaLC + 1) == i) nConsecAlphaLC++;
                nTmpAlphaLC = i;
                nAlphaLC++;
            }
            else if (Character.isDigit(password.charAt(i)))
            {
                if (i > 0 && i < (nLen - 1))
                {
                    nMidChar++;
                }
                if (nTmpNumber != 0) if ((nTmpNumber + 1) == i) nConsecNumber++;
                nTmpNumber = i;
                nNumber++;
            }
            else
            {
                if (i > 0 && i < (nLen - 1))
                {
                    nMidChar++;
                }
                nSymbol++;
            }

            for (int j = 0; j < nLen; j++)
            {
                if (i != j && lowerPasswordString.charAt(i)==lowerPasswordString.charAt(j))
                {
                    nRepChar++;
                }
            }
        }
        String sAlphas = "abcdefghijklmnopqrstuvwxyz";
        String sNumerics = "01234567890";
        int nSeqCount = 3;//规则:依顺序出现三个以上

        for (int i = 0; i < sAlphas.length() - nSeqCount; i++)
        {
            String sFwd = sAlphas.substring(i, nSeqCount);
            char[] sRev = sFwd.toCharArray();
            IteratorHelper.ArrayReverse(sRev);
            if (lowerPasswordString.indexOf(sFwd) != -1
                    || lowerPasswordString.indexOf(new String(sRev)) != -1) nSeqAlpha++;
        }

        for (int i = 0; i < sNumerics.length() - nSeqCount; i++)
        {
            String sFwd = sNumerics.substring(i, nSeqCount);
            char[] sRev = sFwd.toCharArray();
            IteratorHelper.ArrayReverse(sRev);
            if (lowerPasswordString.indexOf(sFwd) != -1
                    || lowerPasswordString.indexOf(new String(sRev)) != -1) nSeqNumber++;
        }
        score += nLen * 4;     //密码长度，加分
        score += nAlphaUC * 2;  // 大写字母字符个数，加分
        score += nAlphaLC * 2;  // 小写字母字符个数，加分
        score += nNumber * 4;   // 数字字符个数，加分
        score += nSymbol * 6;   // 特殊字符个数，加分
        score += nMidChar * 2;  // 密码中间部分出现数字或者特殊字符, 加分
        if (nAlphaUC > 0) nRequirements++;
        if (nAlphaLC > 0) nRequirements++;
        if (nNumber > 0) nRequirements++;
        if (nSymbol > 0) nRequirements++;
        if (nLen >= 8 && nRequirements >= 3) score += (nRequirements + 1) * 2; //满足最低需求, 加分
        if ((nAlphaLC > 0 || nAlphaUC > 0) && nSymbol == 0 && nNumber == 0) score -= nLen; //只有英文字母字符,扣分
        if (nAlphaLC == 0 && nAlphaUC == 0 && nSymbol == 0 && nNumber > 0) score -= nLen; //只有数字字符,扣分
        score -= nRepChar; //出现重复字符(忽略大小写), 扣分
        score -= nConsecAlphaUC * 2;   //大写字母连续（例如AD）,扣分
        score -= nConsecAlphaLC * 2;   //小写字母连续（例如AD）,扣分
        score -= nConsecNumber * 2;    //数字字符连续（例如13）,扣分
        score -= nSeqAlpha * 3;      //字母依顺序出现 (三个以上，例如abc）,扣分
        score -= nSeqNumber * 3;      //数字依顺序出现 (三个以上，例如123）,扣分
        if (score < 0) score = 0; if (score > 100) score = 100;
        return score;
    }
}