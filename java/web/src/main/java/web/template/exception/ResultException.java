package web.template.exception;

import web.template.entity.common.Result;
/**
 * 统一往外抛出的异常
 * @author admin
 */
public class ResultException extends RuntimeException {
	
	/**
	 * 异常内部的结果
	 */
	private Result result;
	
    public ResultException() {
        super();
    }


    public ResultException(String message) {
        super(message);
        result=new Result(-1, message, null);
    }


    public ResultException(String message, Throwable cause) {
        super(message, cause);
        result=new Result(-1, message, null);
    }


    public ResultException(Throwable cause) {
        super(cause);
    }


    protected ResultException(String message, Throwable cause,
                               boolean enableSuppression,
                               boolean writableStackTrace) {
        super(message, cause, enableSuppression, writableStackTrace);
        result=new Result(-1, message, null);
    }


	public Result getResult() {
		return result;
	}


	public void setResult(Result result) {
		this.result = result;
	}
}