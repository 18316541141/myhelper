package com.txj.common;
import java.awt.Image;
import java.io.BufferedInputStream;
import java.io.File;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.io.UnsupportedEncodingException;
import java.net.URLEncoder;
import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import java.util.Map.Entry;
import java.util.UUID;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;
import java.util.concurrent.atomic.AtomicInteger;

import javax.imageio.ImageIO;
import org.apache.commons.io.IOUtils;
import org.apache.commons.lang3.StringUtils;
import org.apache.http.HttpEntity;
import org.apache.http.HttpResponse;
import org.apache.http.NameValuePair;
import org.apache.http.client.HttpClient;
import org.apache.http.client.entity.UrlEncodedFormEntity;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.client.methods.HttpRequestBase;
import org.apache.http.entity.StringEntity;
import org.apache.http.entity.mime.MultipartEntityBuilder;
import org.apache.http.impl.client.HttpClients;
import org.apache.http.message.BasicNameValuePair;
import org.apache.http.protocol.HttpContext;
import org.dom4j.Document;
import org.dom4j.DocumentException;
import org.dom4j.Element;
import org.dom4j.Node;
import org.dom4j.io.SAXReader;
import org.jsoup.Jsoup;
import com.fasterxml.jackson.databind.JsonNode;
import com.fasterxml.jackson.databind.ObjectMapper;
public class HttpClientHelper {
	/**
	 * 检查是否被踢出登录，由用户自定义判断是否被踢出登录的逻辑
	 * @author admin
	 */
    public interface CheckLogout
    {
    	boolean isLogout(HttpResponse response);
    }
	
	/**
	 * 遇到异常时会检测该异常是否需要重新发起请求。
	 * @author admin
	 */
    public interface TryExceptionHandle
    {
    	/**
    	 * 检测遇到该异常时否需要重新再发一次请求
    	 * @param reqUrl	请求的url
    	 * @param ex	异常对象
    	 * @return	如果需要重新发送请求，则返回true
    	 */
        boolean checkTry(String reqUrl, Exception ex);
    }
    
    /**
     * 默认的异常重试器，直接抛出异常，没有其他处理
     * @author admin
     */
    public static class DefaultTryExceptionHandle implements TryExceptionHandle
    {
        public boolean checkTry(String reqUrl,Exception ex){
        	return false;
        }
    }
	
    /**
     * 默认的登出检测，始终认为当前用户不会被踢出
     * @author admin
     */
    public static class DefaultCheckLogout implements CheckLogout
    {
        public boolean isLogout(HttpResponse response) {
        	return false;
        }
    }
    
    private static Element HttpCfg;
    
    
    static{
    	DefaultCheckLogout = new DefaultCheckLogout();
    	DefaultTryExceptionHandle = new DefaultTryExceptionHandle();
    	SAXReader reader=new SAXReader();
    	try {
    		HttpCfg = reader.read(new File("http.xml")).getRootElement();
		} catch (DocumentException e) {
			throw new RuntimeException(e);
		}
    }
    
    private static TryExceptionHandle DefaultTryExceptionHandle;
    
    private static CheckLogout DefaultCheckLogout;
    
	public static void setDefaultTryExceptionHandle(TryExceptionHandle defaultTryExceptionHandle) {
		DefaultTryExceptionHandle = defaultTryExceptionHandle;
	}

	public static void setDefaultCheckLogout(CheckLogout defaultCheckLogout) {
		DefaultCheckLogout = defaultCheckLogout;
	}

	/**
	 * 异步http的回调函数
	 * @author admin
	 */
	public interface AsyncCallback{
		/**
		 * 异步http调用成功时回调函数
		 * @param response
		 */
		void success(HttpResponse response);
		
		/**
		 * 异步http调用失败时回调函数
		 * @param throwable
		 */
		void fail(Throwable throwable);
	}
	
	public static class AsyncReq{
		private static AtomicInteger executeCount;
		private static int maxExecuteCount;
        private static String controllerName;
        private static ExecutorService cachedThreadPool ;
        static
        {
        	cachedThreadPool = Executors.newCachedThreadPool();
            executeCount = new AtomicInteger(0);
            maxExecuteCount = 8;
            controllerName = UUID.randomUUID().toString();
        }
		public static int getMaxExecuteCount() {
			return maxExecuteCount;
		}
		public static void setMaxExecuteCount(int maxExecuteCount) {
			AsyncReq.maxExecuteCount = maxExecuteCount;
		}
		
		public static void httpGet(AsyncCallback asyncCallback,String requestUrl, HttpContext context){
			executeCount.incrementAndGet();
			while(executeCount.get()>=maxExecuteCount){
				ThreadHelper.batchWait(controllerName, 30000);
			}
			cachedThreadPool.execute(()->{
				try {
					asyncCallback.success(HttpClientHelper.httpGet(requestUrl,context));
				} catch (Throwable t) {
					asyncCallback.fail(t);
				}
				executeCount.decrementAndGet();
                ThreadHelper.batchSet(controllerName);
			});
		}
		
		public static void httpPost(AsyncCallback asyncCallback,Map<String, Object> param,String requestUrl,HttpContext context){
			executeCount.incrementAndGet();
			while(executeCount.get()>=maxExecuteCount){
				ThreadHelper.batchWait(controllerName, 30000);
			}
			cachedThreadPool.execute(()->{
				try {					
					asyncCallback.success(HttpClientHelper.httpPost(param, requestUrl, context));
				} catch (Throwable t) {
					asyncCallback.fail(t);
				}
				executeCount.decrementAndGet();
                ThreadHelper.batchSet(controllerName);
			});
		}
		
		public static void HttpPost(AsyncCallback asyncCallback,String requestUrl, Map<String, String> param, HttpContext context){
			executeCount.incrementAndGet();
			while(executeCount.get()>=maxExecuteCount){
				ThreadHelper.batchWait(controllerName, 30000);
			}
			cachedThreadPool.execute(()->{
				try {
					asyncCallback.success(HttpClientHelper.httpPost(requestUrl, param, context));
				} catch (Throwable t) {
					asyncCallback.fail(t);
				}
				executeCount.decrementAndGet();
                ThreadHelper.batchSet(controllerName);
			});
		}
		
		public static void HttpPost(AsyncCallback asyncCallback, String requestUrl, String body, HttpContext context){
			executeCount.incrementAndGet();
			while(executeCount.get()>=maxExecuteCount){
				ThreadHelper.batchWait(controllerName, 30000);
			}
			cachedThreadPool.execute(()->{
				try {
					asyncCallback.success(HttpClientHelper.httpPost(requestUrl, body, context));
				} catch (Throwable t) {
					asyncCallback.fail(t);
				}
				executeCount.decrementAndGet();
                ThreadHelper.batchSet(controllerName);
			});
		}
	}

	/**
	 * 发送含有url参数的get请求
	 * @param requestUrl
	 * @param query
	 * @param context
	 * @return
	 */
	public static HttpResponse httpGet(String requestUrl, Map<String, String> query,HttpContext context){
		return httpGet(requestUrl+"?"+queryParamsMapToStr(query, loadRequestEncoding(requestUrl)),context);
	}
	
	/**
	 * 发送只有url的get请求
	 * @param requestUrl	url
	 * @param context
	 * @return
	 */
	public static HttpResponse httpGet(String requestUrl, HttpContext context){
		HttpGet httpGet = new HttpGet(requestUrl);
		LoadConfig(httpGet, requestUrl);
		HttpClient innerHttpClient = HttpClients.createDefault();
		TRY_AGAIN:
		try {
			return innerHttpClient.execute(httpGet, context);
		} catch (IOException e) {
			if(DefaultTryExceptionHandle.checkTry(requestUrl, e)){
				break TRY_AGAIN;
			}else{				
				throw new RuntimeException(e);
			}
		}
		return null;
	}
	
	/**
	 * 发送既含有url参数，又含有表单参数的post请求，上传文件用
	 * @param param		url参数
	 * @param requestUrl	请求的url
	 * @param query
	 * @param context
	 * @return
	 */
	public static HttpResponse httpPost(Map<String, Object> param,String requestUrl,Map<String, String> query, HttpContext context){
		return httpPost(param,requestUrl+"?"+queryParamsMapToStr(query,loadRequestEncoding(requestUrl)), context);
	}
	
	/**
	 * 发送只含有表单参数的post请求，支持上传文件
	 * @param param		表单参数，上传文件用
	 * @param requestUrl	请求的url
	 * @param context
	 * @return
	 */
	public static HttpResponse httpPost(Map<String, Object> param,String requestUrl, HttpContext context){
		MultipartEntityBuilder multipartEntityBuilder = MultipartEntityBuilder.create();
		for(Entry<String, Object> entry:param.entrySet()){
			 if (entry.getValue() instanceof File) {
				 multipartEntityBuilder.addBinaryBody(entry.getKey(), (File) entry.getValue());
			}else{
				multipartEntityBuilder.addTextBody(entry.getKey(), (String)entry.getValue());
			}
		}
		return httpPost(requestUrl,multipartEntityBuilder.build(),context);
	}
	
	/**
	 * 发送只有报文体的post请求
	 * @param requestUrl	请求的url
	 * @param body	字符串报文体
	 * @param context	
	 * @return
	 */
	public static HttpResponse httpPost(String requestUrl, String body, HttpContext context){
		StringEntity	stringEntity=new StringEntity(body,loadRequestEncoding(requestUrl));
		return httpPost(requestUrl, stringEntity, context);
	}

	/**
	 * 发送既含有表单参数，又含有url参数的post请求
	 * @param requestUrl	请求的url
	 * @param query	url参数
	 * @param param		表单参数
	 * @param context
	 * @return
	 */
	public static HttpResponse httpPost(String requestUrl,Map<String, String> query,Map<String, String> param, HttpContext context){
		return httpPost(requestUrl+"?"+queryParamsMapToStr(query,loadRequestEncoding(requestUrl)), param, context);
	}

	/**
	 * 把param里面的key和value值转化为url上的请求参数和值。
	 * 
	 * @param param
	 *            请求参数
	 * @param encoding
	 *            编码方式
	 * @return 返回url上的请求参数和值
	 */
	private static String queryParamsMapToStr(Map<String, String> param, String encoding) {
		StringBuilder sb = new StringBuilder();
		String connChar = "";
		for (Entry<String, String> kv : param.entrySet()) {
			try {
				sb.append(connChar).append(kv.getKey()).append("=").append(URLEncoder.encode(kv.getValue(), encoding));
			} catch (UnsupportedEncodingException e) {
				throw new RuntimeException(e);
			}
			connChar = "&";
		}
		return sb.toString();
	}

	/**
	 * 发送只含有表单参数的post请求
	 * 
	 * @param requestUrl
	 *            请求的url
	 * @param param
	 *            表单参数
	 * @param context
	 * @return
	 */
	public static HttpResponse httpPost(String requestUrl, Map<String, String> param, HttpContext context) {
		List<NameValuePair> paramPairs = new ArrayList<NameValuePair>();
		for (Entry<String, String> entry : param.entrySet()) {
			paramPairs.add(new BasicNameValuePair(entry.getKey(), entry.getValue()));
		}
		UrlEncodedFormEntity httpEntity;
		try {
			httpEntity = new UrlEncodedFormEntity(paramPairs, loadRequestEncoding(requestUrl));
		} catch (UnsupportedEncodingException e) {
			throw new RuntimeException(e);
		}
		return httpPost(requestUrl, httpEntity, context);
	}
	
	/**
	 * 
	 * @param requestUrl
	 * @param httpEntity
	 * @param context
	 * @return
	 */
	private static HttpResponse httpPost(String requestUrl, HttpEntity httpEntity, HttpContext context) {
		HttpPost httpPost = new HttpPost(requestUrl);
		LoadConfig(httpPost, requestUrl);
		HttpClient innerHttpClient = HttpClients.createDefault();
		httpPost.setEntity(httpEntity);
		TRY_AGAIN:
		try {
			return innerHttpClient.execute(httpPost, context);
		} catch (IOException e) {
			if(DefaultTryExceptionHandle.checkTry(requestUrl, e)){
				break TRY_AGAIN;
			}else{	
				throw new RuntimeException(e);
			}
		}
		return null;
	}

	/**
	 * 解析响应报文，把报文内容以图片对象的形式返回
	 * 
	 * @param httpResponse
	 *            响应报文
	 * @return 返回图片对象
	 */
	public static Image getImage(HttpResponse httpResponse) {
		if (DefaultCheckLogout.isLogout(httpResponse)){
			throw new RuntimeException("已退出登录！(ERROR:-3)");
		}else{
			try {
				return ImageIO.read(httpResponse.getEntity().getContent());
			} catch (IOException e) {
				throw new RuntimeException(e);
			}
		}
	}

	/**
	 * 解析响应报文，把报文内容以输入流的形式返回
	 * 
	 * @param httpResponse
	 * @return 返回输入流
	 */
	public static BufferedInputStream getInput(HttpResponse httpResponse) {
		if (DefaultCheckLogout.isLogout(httpResponse)){
			throw new RuntimeException("已退出登录！(ERROR:-3)");
		}else{			
			try {
				return new BufferedInputStream(httpResponse.getEntity().getContent());
			} catch (IOException e) {
				throw new RuntimeException(e);
			}
		}
	}

	/**
	 * 解析响应报文，把报文内容输出到输出流，会自动关闭输出流
	 * 
	 * @param httpResponse
	 *            响应报文
	 * @param outputStream
	 *            输出流
	 */
	public static void output(HttpResponse httpResponse, OutputStream outputStream) {
		if (DefaultCheckLogout.isLogout(httpResponse)){
			throw new RuntimeException("已退出登录！(ERROR:-3)");
		}else{
			try {
				IOUtils.copy(httpResponse.getEntity().getContent(), outputStream);
			} catch (IOException e) {
				throw new RuntimeException(e);
			}
		}
	}

	/**
	 * 解析响应报文，把报文内容以xml的形式返回
	 * 
	 * @param httpResponse
	 *            响应报文
	 * @return 返回xml根元素
	 */
	public static Element getXml(HttpResponse httpResponse,String requestUrl) {
		if (DefaultCheckLogout.isLogout(httpResponse)){
			throw new RuntimeException("已退出登录！(ERROR:-3)");
		}else{
			String encoding=loadResponseEncoding(requestUrl);
			SAXReader saxReader = new SAXReader();
			Document doc;
			try {
				doc = saxReader.read(new InputStreamReader(httpResponse.getEntity().getContent(),encoding));
				return doc.getRootElement();
			} catch (IOException e) {
				throw new RuntimeException(e);
			} catch (UnsupportedOperationException e) {
				throw new RuntimeException(e);
			} catch (DocumentException e) {
				throw new RuntimeException(e);
			}
		}
	}

	/**
	 * 解析响应报文，把报文内容以字符串的形式返回
	 * 
	 * @param httpResponse
	 *            响应报文
	 * @param requestUrl
	 * 				请求的url            
	 * @return 返回报文内容字符串
	 */
	public static String getText(HttpResponse httpResponse,String requestUrl) {
		if (DefaultCheckLogout.isLogout(httpResponse)){
			throw new RuntimeException("已退出登录！(ERROR:-3)");
		}else{
			String encoding=loadResponseEncoding(requestUrl);
			HttpEntity httpEntity = httpResponse.getEntity();
			int rest = (int) (httpEntity.getContentLength() % Integer.MAX_VALUE);
			int partCount = (int) ((httpEntity.getContentLength() - rest) / Integer.MAX_VALUE);
			byte[] buffer = null;
			StringBuilder sb = new StringBuilder();
			try {
				for (int i = 0; i < partCount; i++) {
					IOUtils.read(httpEntity.getContent(), new byte[Integer.MAX_VALUE]);
					sb.append(new String(buffer, encoding));
				}
				IOUtils.read(httpEntity.getContent(), new byte[rest]);
				sb.append(new String(buffer, encoding));
			} catch (IOException e) {
				throw new RuntimeException(e);
			}
			return sb.toString();
		}
	}

	/**
	 * 解析响应报文，把报文内容以html的形式返回
	 * 
	 * @param httpResponse
	 * @return
	 */
	public static org.jsoup.nodes.Document getHtmlDoc(HttpResponse httpResponse,String requestUrl) {
		if (DefaultCheckLogout.isLogout(httpResponse)){
			throw new RuntimeException("已退出登录！(ERROR:-3)");
		}else{
			return Jsoup.parse(getText(httpResponse,requestUrl));
		}
	}

	/**
	 * 解析响应报文，把报文内容以json的形式返回
	 * 
	 * @param httpResponse
	 * @return
	 */
	public static JsonNode getJson(HttpResponse httpResponse) {
		if (DefaultCheckLogout.isLogout(httpResponse)){
			throw new RuntimeException("已退出登录！(ERROR:-3)");
		}else{			
			ObjectMapper objectMapper = new ObjectMapper();
			try {
				return objectMapper.readTree(httpResponse.getEntity().getContent());
			} catch (IOException e) {
				throw new RuntimeException(e);
			}
		}
	}
	
	/**
	 * 读取配置文件中该请求的报文编码方式
	 * @param requestUrl	请求url
	 * @return	返回编码方式
	 */
	static String loadRequestEncoding(String requestUrl)
    {
        for (Object obj : HttpCfg.selectNodes("//Url")){        	
        	Element ele=(Element)obj;
        	for (String urlPrefix : ele.attribute("prefix").getText().split("\\s")){        		
        		if (StringUtils.isNotBlank(urlPrefix.trim()) && requestUrl.startsWith(urlPrefix.trim()))
        		{
        			Node encoding = ele.selectSingleNode("RequestEncoding");
        			if (encoding != null) return encoding.getText();
        		}
        	}
        }
        return "UTF-8";
    }
	
	/**
	 * 读取配置文件中该响应的报文编码方式
	 * @param requestUrl	响应请求的url
	 * @return	返回编码方式
	 */
	static String loadResponseEncoding(String requestUrl)
    {
        for (Object obj : HttpCfg.selectNodes("//Url")){        	
        	Element ele=(Element)obj;
        	for (String urlPrefix : ele.attribute("prefix").getText().split("\\s")){        		
        		if (StringUtils.isNotBlank(urlPrefix.trim()) && requestUrl.startsWith(urlPrefix.trim()))
        		{
        			Node encoding = ele.selectSingleNode("ResponseEncoding");
        			if (encoding != null) return encoding.getText();
        		}
        	}
        }
        return "UTF-8";
    }
	
	/**
	 * 根据url匹配项，加载自定义配置。
	 * @param reqBase
	 * @param requestUrl
	 */
	static void LoadConfig(HttpRequestBase reqBase, String requestUrl){
		for (Object obj : HttpCfg.selectNodes("//Url")){        	
        	Element ele=(Element)obj;
        	for (String urlPrefix : ele.attribute("prefix").getText().split("\\s")){
        		if (StringUtils.isNotBlank(urlPrefix.trim()) && requestUrl.startsWith(urlPrefix.trim()))
        		{
        			Node contentType= ele.selectSingleNode("ContentType");
					if(contentType!=null){
						reqBase.setHeader("Content-Type", contentType.getText());
					}
					Node authorization= ele.selectSingleNode("Authorization");
					if(authorization!=null){
						reqBase.setHeader("Authorization", authorization.getText());
					}
					Node userAgent= ele.selectSingleNode("UserAgent");
					if(userAgent!=null){
						reqBase.setHeader("User-Agent", userAgent.getText());
					}
					Node accept= ele.selectSingleNode("Accept");
					if(accept!=null){
						reqBase.setHeader("Accept", accept.getText());
					}
					Node pragma=ele.selectSingleNode("Pragma");
					if(pragma!=null){
						reqBase.setHeader("Pragma", pragma.getText());
					}
					Node origin = ele.selectSingleNode("Origin");
					if(origin!=null){
						reqBase.setHeader("Origin", origin.getText());
					}
					Node referer = ele.selectSingleNode("Referer");
					if(referer!=null){
						reqBase.setHeader("Referer", referer.getText());
					}
					Node connection = ele.selectSingleNode("Connection");
					if(connection!=null){
						reqBase.setHeader("Connection ", connection.getText());
					}
					Node acceptEncoding = ele.selectSingleNode("AcceptEncoding");
                    if (acceptEncoding != null){
                    	reqBase.setHeader("Accept-Encoding", acceptEncoding.getText());
                    }
                    Node acceptLanguage = ele.selectSingleNode("AcceptLanguage");
                    if (acceptLanguage != null){
                    	reqBase.setHeader("Accept-Language", acceptLanguage.getText());
                    }
        		}
        	}
    	}
	}
	
}