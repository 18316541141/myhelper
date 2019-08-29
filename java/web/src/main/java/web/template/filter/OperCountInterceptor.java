package web.template.filter;
import java.util.Date;
import java.util.HashMap;
import java.util.Map;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import org.springframework.web.servlet.HandlerInterceptor;
import com.fasterxml.jackson.databind.ObjectMapper;

import web.template.entity.common.Result;
public class OperCountInterceptor  implements HandlerInterceptor {

	public ObjectMapper objectMapper;

	/**
	 * 最近一次清理缓存的时间
	 */
	public Date lastClearDate;
	
	/**
	 * 次数限制，当次数达到countLimit次时，无法访问。
	 */
	public int countLimit;
	
	/**
	 * 清除的毫秒数，每隔ClearMillisecond毫秒清理一次。
	 */
	public long clearMillisecond;
	
	/**
	 * 记录近期访客的操作记录
	 */
	public Map<String, OperRecord> recordMap;
	
	public OperCountInterceptor(int countLimit){
		this.countLimit=countLimit;
		recordMap=new HashMap<String, OperCountInterceptor.OperRecord>();
		lastClearDate=new Date();
	}
	
	/**
	 * 操作次数限制的拦截器，超过一定的次数后，需要隔一段时间才能
     * 再次操作
	 */
	public boolean preHandle(HttpServletRequest request, HttpServletResponse response, Object handler)
			throws Exception {
		String operKey=request.getRemoteHost()+request.getRequestURI();
		if(recordMap.containsKey(operKey)){
			OperRecord operRecord=recordMap.get(operKey);
			long totalMilliseconds =System.currentTimeMillis()-operRecord.getLastOperMillisecond();
			long count =(totalMilliseconds - totalMilliseconds % clearMillisecond) / clearMillisecond;
			operRecord.setCount(operRecord.getCount() > count ? operRecord.getCount() - count : 0);
			if (operRecord.getCount() >= countLimit)
            {
                response.setCharacterEncoding("UTF-8");
                response.setContentType("application/json;charset=UTF-8");
                response.setStatus(200);
                response.getWriter().write(objectMapper.writeValueAsString(new Result(-1, "操作过于频繁、请稍后重试！",null)));
                return false;
            }
		}
		return true;
	}
	
	public ObjectMapper getObjectMapper() {
		return objectMapper;
	}

	public void setObjectMapper(ObjectMapper objectMapper) {
		this.objectMapper = objectMapper;
	}
	
	public class OperRecord{

        /**
         * 操作标识，用于区分操作者和操作目标
         */
        public String operKey;

        /**
         * 操作次数
         */
        public long count;

        /**
         * 最近一次的操作毫秒
         */
        public long lastOperMillisecond;

		public String getOperKey() {
			return operKey;
		}

		public void setOperKey(String operKey) {
			this.operKey = operKey;
		}

		public long getCount() {
			return count;
		}

		public void setCount(long count) {
			this.count = count;
		}

		public long getLastOperMillisecond() {
			return lastOperMillisecond;
		}

		public void setLastOperMillisecond(long lastOperMillisecond) {
			this.lastOperMillisecond = lastOperMillisecond;
		}
    }
}
