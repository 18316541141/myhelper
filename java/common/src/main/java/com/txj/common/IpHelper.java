package com.txj.common;
import org.apache.http.protocol.HttpCoreContext;
public class IpHelper {
	public static String getOuterNetIP(){
		HttpCoreContext httpCoreContext=new HttpCoreContext();
		String text=HttpClientHelper.getText(HttpClientHelper.httpGet("http://pv.sohu.com/cityjson?ie=utf-8", httpCoreContext), "http://pv.sohu.com/cityjson?ie=utf-8");
		return text;
    }
	public static void main(String[] args) {
		String text=IpHelper.getOuterNetIP();
		System.out.println(text);
	}
}
