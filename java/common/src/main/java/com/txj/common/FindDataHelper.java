package com.txj.common;
public class FindDataHelper {
	/**
	 * 使用前缀查找数据，然后截取前缀，得到前缀后面的数据
	 * @param text		源文本
	 * @param prefix	前缀
	 * @return	返回匹配结果
	 */
    public static String findDataByPrefix(String text, String prefix) {
        int startIndex = text.indexOf(prefix);
        if (startIndex == -1)
            throw new RuntimeException("前缀：“"+prefix+"”没有找到匹配结果。");
        return text.substring(startIndex + prefix.length());
    }

    /**
     * 使用后缀查找数据，然后截取后缀，得到后缀前面的数据
     * @param text		源文本
     * @param suffix	后缀
     * @return	返回匹配结果
     */
    public static String FindDataBySuffix(String text, String suffix){
        int endIndex = text.indexOf(suffix);
        if (endIndex == -1)
            throw new RuntimeException("后缀：“"+suffix+"”没有找到匹配结果。");
        return text.substring(0, endIndex);
    }

    /**
     * 使用前缀和后缀查找数据，然后截取前缀和后缀，得到中间的数据
     * @param text	 源文本
     * @param prefix	前缀
     * @param suffix 后缀
     * @return	返回匹配结果
     */
    public static String FindDataByPrefixAndSuffix(String text,String prefix,String suffix){
        int startIndex = text.indexOf(prefix);
        if (startIndex == -1)
            throw new RuntimeException("前缀：“"+prefix+"”没有找到匹配结果。");
        startIndex = startIndex + prefix.length();
        int endIndex = text.indexOf(suffix, startIndex);
        if(endIndex==-1)
            throw new RuntimeException("后缀：“"+suffix+"”没有找到匹配结果。");
        return text.substring(startIndex,endIndex);
    }
}