package com.txj.common;
import java.util.Map.Entry;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.Map;
/**
 * 可迭代元素的帮助类
 * @author admin
 *
 */
public class IteratorHelper {
	public interface MatchRule<T> {
		boolean isMatch(T val);
	}

	/**
	 * 删除map匹配的元素，该方法不是线程安全。
	 * 
	 * @param map
	 *            被删除的map
	 * @param matchRule
	 *            匹配规则
	 */
	public static <K, V> void delEleFromMap(Map<K, V> map, MatchRule<V> matchRule) {
		List<K> removeKeyList = new ArrayList<>();
		for (Entry<K, V> keyVal : map.entrySet()) {
			if (matchRule.isMatch(keyVal.getValue())) {
				removeKeyList.add(keyVal.getKey());
			}
		}
		for (K key : removeKeyList) {
			map.remove(key);
		}
	}

	/**
	 * 删除list匹配的元素，该方法不是线程安全的
	 * 
	 * @param list
	 *            被删除的list
	 * @param matchRule
	 *            匹配规则
	 */
	public static <V> void delEleFromList(List<V> list, MatchRule<V> matchRule) {
		for (int i = list.size() - 1; i > -1; i--) {
			if (matchRule.isMatch(list.get(i))) {
				list.remove(list.get(i));
			}
		}
	}
	
	/**
	 * 数组反序
	 * @param varray
	 */
	public static  void ArrayReverse(char[] varray){
		char temp;
		for(int i=0,len=(varray.length-varray.length%2)/2;i<len;i++){
			temp=varray[len-i-1];
			varray[len-i-1]=varray[i];
			varray[i]=temp;
		}
	}
	
	/**
	 * 数组反序
	 * @param varray
	 */
	public static  void ArrayReverse(int[] varray){
		int temp;
		for(int i=0,len=(varray.length-varray.length%2)/2;i<len;i++){
			temp=varray[len-i-1];
			varray[len-i-1]=varray[i];
			varray[i]=temp;
		}
	}
	
	/**
	 * 数组反序
	 * @param varray
	 */
	public static <V> void ArrayReverse(V[] varray){
		V temp;
		for(int i=0,len=(varray.length-varray.length%2)/2;i<len;i++){
			temp=varray[len-i-1];
			varray[len-i-1]=varray[i];
			varray[i]=temp;
		}
	}
}
