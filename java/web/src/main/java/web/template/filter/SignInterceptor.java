package web.template.filter;
import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import java.util.Set;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import org.springframework.web.servlet.HandlerInterceptor;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.txj.common.EncrypHelper;
import web.template.entity.common.Result;
public class SignInterceptor  implements HandlerInterceptor {
	
	public Map<String,Set<String>> paramsMap;
	
	public ObjectMapper objectMapper; 
	
	/**
	 * 签名拦截器，对于部分匿名访问的接口，可能需要用签名才能进行访问
	 */
	public boolean preHandle(HttpServletRequest request, HttpServletResponse response, Object handler)
			throws Exception {
		Set<String> set=paramsMap.get(request.getRequestURI());
		for(String key:set){
			String[] vals=request.getParameterValues(key);
			if(vals==null || vals.length==0){
				response.setCharacterEncoding("UTF-8");
                response.setContentType("application/json;charset=UTF-8");
                response.setStatus(200);
                response.getWriter().write(objectMapper.writeValueAsString(new Result(-12,"签名错误，参数不完整，必须包含"+set.toString()+"参数！",null)));
				return false;
			}
		}
		List<String> keys=new ArrayList<String>(set);
		keys.sort((a,b)->a.compareTo(b));
		StringBuilder sb = new StringBuilder();
		String connChar = "";
		for(String key:keys){
			if(!"signChar".equals(key) &&  !"signKey".equals(key) && set.contains(key)){
				String[] vals=request.getParameterValues(key);
				sb.append(connChar).append(key).append("=").append(vals[0]);
                connChar = "&";
            }
		}
		String[] signKeys=request.getParameterValues("signKey");
		String[] signSecrets=request.getParameterValues("signSecret");
		String[] signChars=request.getParameterValues("signChar");
		if(signKeys==null || signSecrets==null || signChars==null || signKeys.length==0 || signSecrets.length==0 || signChars.length==0){
			response.setCharacterEncoding("UTF-8");
            response.setContentType("application/json;charset=UTF-8");
            response.setStatus(200);
            response.getWriter().write(objectMapper.writeValueAsString(new Result(-12,"签名错误，signKey或signSecret错误！",null)));
			return false;
		}
		if(!EncrypHelper.getSha1FromString(sb.append("&signKey=").append(signKeys[0]).append("&signSecret=").append(signSecrets[0]).toString()).equals(signChars[0])){
			response.setCharacterEncoding("UTF-8");
            response.setContentType("application/json;charset=UTF-8");
            response.setStatus(200);
            response.getWriter().write(objectMapper.writeValueAsString(new Result(-12,"签名错误！",null)));
		}
		return true;
	}

	public Map<String, Set<String>> getParamsMap() {
		return paramsMap;
	}

	public void setParamsMap(Map<String, Set<String>> paramsMap) {
		this.paramsMap = paramsMap;
	}

	public ObjectMapper getObjectMapper() {
		return objectMapper;
	}

	public void setObjectMapper(ObjectMapper objectMapper) {
		this.objectMapper = objectMapper;
	}
}
