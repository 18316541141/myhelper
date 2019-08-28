package com.txj.common;
import org.apache.http.protocol.HttpCoreContext;
/**
 * ip地址帮助类
 * @author admin
 */
public class IpHelper {
	/**
	 * 获取本机在外网的ip地址
	 * @return
	 */
	public static String getOuterNetIP(){
		HttpCoreContext httpCoreContext=new HttpCoreContext();
		String text=HttpClientHelper.getText(HttpClientHelper.httpGet("http://pv.sohu.com/cityjson?ie=utf-8", httpCoreContext), "http://pv.sohu.com/cityjson?ie=utf-8");
		return FindDataHelper.FindDataByPrefixAndSuffix(text, "var returnCitySN = {\"cip\": \"", "\", \"cid\":");
    }
	
	/**
	 * ip地址转为整数
	 * @param ip
	 * @return
	 */
	public static int ipToInt(String ip){
		String[] parts=ip.split("\\.");
		int total=0;
		for(int i=0,len=parts.length;i<len;i++){
			total+=Integer.parseInt(parts[i])<<((3-i)*8);
		}
		return total;
	}
}
