package web.template.interceptor;
import java.util.Map;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import org.springframework.web.servlet.HandlerInterceptor;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.txj.common.entity.Result;
/**
 * 报文大小限制，当请求报文内容超出限制，
 * 则阻断请求
 * @author admin
 */
public class SizeFilterInterceptor implements HandlerInterceptor  {
	
	private ObjectMapper objectMapper;
	
	/**
	 * 全局默认的报文大小限制
	 */
	private SizeInfo defaultSizeInfo;
	
	/**
	 * 定制的报文大小限制
	 */
	private Map<String,SizeInfo> sizeInfoMap;
	
	public SizeFilterInterceptor(SizeInfo defaultSizeInfo,Map<String,SizeInfo> sizeInfoMap){
		this.defaultSizeInfo=defaultSizeInfo;
		this.sizeInfoMap=sizeInfoMap;
	}
	
	public boolean preHandle(HttpServletRequest request, HttpServletResponse response, Object handler)
			throws Exception{
		SizeInfo sizeInfo;
		if(sizeInfoMap.containsKey(request.getRequestURI())){
			sizeInfo=sizeInfoMap.get(request.getRequestURI());
		}else{			
			sizeInfo= defaultSizeInfo;
		}
		if(sizeInfo.getSize()>=request.getContentLength()){
			return Boolean.TRUE;				
		}else{
			response.setCharacterEncoding("UTF-8");
			response.setContentType("application/json;charset=UTF-8");
			response.setStatus(200);
			response.getWriter().write(objectMapper.writeValueAsString(new Result(-1, "操作过于频繁、请稍后重试！", null)));
			return Boolean.FALSE;
		}
	}
	
	public ObjectMapper getObjectMapper() {
		return objectMapper;
	}

	public void setObjectMapper(ObjectMapper objectMapper) {
		this.objectMapper = objectMapper;
	}

	public Map<String, SizeInfo> getSizeInfoMap() {
		return sizeInfoMap;
	}

	public void setSizeInfoMap(Map<String, SizeInfo> sizeInfoMap) {
		this.sizeInfoMap = sizeInfoMap;
	}

	public SizeInfo getDefaultSizeInfo() {
		return defaultSizeInfo;
	}

	public void setDefaultSizeInfo(SizeInfo defaultSizeInfo) {
		this.defaultSizeInfo = defaultSizeInfo;
	}

	/**
	 * 报文尺寸信息
	 * @author admin
	 */
	public static class SizeInfo{
		/**
		 * 附件最大限制，单位：字节
		 */
        public int size;
        
        /**
         * 提示信息
         */
        public String msg;

		public SizeInfo() {
			super();
		}

		public SizeInfo(int size, String msg) {
			super();
			this.size = size;
			this.msg = msg;
		}

		public int getSize() {
			return size;
		}

		public void setSize(int size) {
			this.size = size;
		}

		public String getMsg() {
			return msg;
		}

		public void setMsg(String msg) {
			this.msg = msg;
		}
	}
}
