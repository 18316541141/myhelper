package web.template.service.common;
import java.io.File;
import org.apache.logging.log4j.Logger;
import org.springframework.beans.factory.annotation.Autowired;
import com.txj.common.SnowFlakeHelper;
import com.txj.common.exception.ResultException;
import web.template.entity.common.MyLog;
/**
 * 业务基础类，所有的service都必须继承
 * @author admin
 */
public abstract class BaseService {
	/**
	 * 跨平台的斜杠
	 */
	protected static char s;
	
	static{
		s=File.separatorChar;
	}
	
	@Autowired
	protected SnowFlakeHelper snowFlakeHelper;
	
	/**
	 * 日志输出类
	 */
	protected Logger log;
	
	public BaseService(){
		log=MyLog.getLogger();
	}
	
	/**
	 * 异常往外抛出统一处理的方法
	 * @param msg 异常信息
	 */
	protected void ret(String msg){
		ret(msg, -1);
	}
	
	/**
	 * 异常往外抛出统一处理的方法
	 * @param msg 异常信息
	 * @param code 异常的错误码
	 */
	protected void ret(String msg, int code)
    {
		ResultException resultException=new ResultException(msg);
		resultException.getResult().setCode(code);
        throw resultException;
    }
	
	/**
	 * 生成一个id
	 * @return
	 */
	public Long nextId(){
		return snowFlakeHelper.nextId();
	}
}
