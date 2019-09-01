package web.template.interceptor;

import java.util.Iterator;
import java.util.Map;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.springframework.web.servlet.HandlerInterceptor;

import com.fasterxml.jackson.databind.ObjectMapper;

import web.template.entity.common.Result;

/**
 * 操作间隔拦截器，以ip为标识，对每次操作的间隔时间 进行限制
 * 
 * @author admin
 *
 */
public class OperIntervalInterceptor implements HandlerInterceptor {

	/**
	 * json转换的工具类
	 */
	public ObjectMapper objectMapper;

	/**
	 * 间隔毫秒数
	 */
	public Map<String, Long> intervalMillisMap;

	/**
	 * 近期访客的访问时间
	 */
	public Map<String, Long> recordMap;

	/**
	 * 最近一次清理日期
	 */
	public long lastClearMills;

	public OperIntervalInterceptor(Map<String, Long> intervalMillisMap) {
		this.intervalMillisMap = intervalMillisMap;
		lastClearMills = System.currentTimeMillis();
	}

	public boolean preHandle(HttpServletRequest request, HttpServletResponse response, Object handler)
			throws Exception {
		String operKey = request.getRemoteAddr() + request.getRequestURI();
		boolean temp;
		if (recordMap.containsKey(operKey)) {
			temp = System.currentTimeMillis() - recordMap.get(operKey) > intervalMillisMap.get(request.getRequestURI());
		} else {
			temp = Boolean.FALSE;
		}
		recordMap.put(operKey, System.currentTimeMillis());
		if (temp) {
			response.setCharacterEncoding("UTF-8");
			response.setContentType("application/json;charset=UTF-8");
			response.setStatus(200);
			response.getWriter().write(objectMapper.writeValueAsString(new Result(-1, "操作过于频繁、请稍后重试！", null)));
			return Boolean.FALSE;
		} else {
			return Boolean.TRUE;
		}
	}

	public void afterCompletion(HttpServletRequest request, HttpServletResponse response, Object handler, Exception ex)
			throws Exception {
		if(System.currentTimeMillis()-recordMap.get(request.getRequestURI())>60){
			String tkey;
			for(Iterator<String> it=recordMap.keySet().iterator();it.hasNext();){
				tkey=it.next();
				if(tkey.endsWith(request.getRequestURI()) && recordMap.containsKey(tkey) && System.currentTimeMillis()-recordMap.get(request.getRequestURI())>intervalMillisMap.get(request.getRequestURI())){
					recordMap.remove(tkey);
				}
			}
			lastClearMills=System.currentTimeMillis();
		}
	}

	public ObjectMapper getObjectMapper() {
		return objectMapper;
	}

	public void setObjectMapper(ObjectMapper objectMapper) {
		this.objectMapper = objectMapper;
	}
}
