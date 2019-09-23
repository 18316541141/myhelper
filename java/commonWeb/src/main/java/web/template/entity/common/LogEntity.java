package web.template.entity.common;

import java.util.Date;

/**
 * 
 */
public class LogEntity {

	public LogEntity() {
	}

	/**
	 * 主键id，由分布式雪花id生成
	 */
	private Long id;

	public final void setId(final Long id) {
		this.id = id;
	}

	public final Long getId() {
		return this.id;
	}

	/**
	 * 日志日期
	 */
	private Date createDate;

	public final void setCreateDate(final Date createDate) {
		this.createDate = createDate;
	}

	public final Date getCreateDate() {
		return this.createDate;
	}

	/**
	 * 日志分级
	 */
	private String level;

	public final void setLevel(final String level) {
		this.level = level;
	}

	public final String getLevel() {
		return this.level;
	}

	/**
	 * 线程号
	 */
	private String threadNo;

	public final void setThreadNo(final String threadNo) {
		this.threadNo = threadNo;
	}

	public final String getThreadNo() {
		return this.threadNo;
	}

	/**
	 * 日志内容
	 */
	private String message;

	public final void setMessage(final String message) {
		this.message = message;
	}

	public final String getMessage() {
		return this.message;
	}

	/**
	 * 日志发生的项目名
	 */
	private String projectName;

	public final void setProjectName(final String projectName) {
		this.projectName = projectName;
	}

	public final String getProjectName() {
		return this.projectName;
	}

	/**
	 * 日志发生的类型
	 */
	private String typeName;

	public final void setTypeName(final String typeName) {
		this.typeName = typeName;
	}

	public final String getTypeName() {
		return this.typeName;
	}

	/**
	 * 日志发生的方法名称
	 */
	private String funcName;

	public final void setFuncName(final String funcName) {
		this.funcName = funcName;
	}

	public final String getFuncName() {
		return this.funcName;
	}

	/**
	 * 日志的异常堆栈信息
	 */
	private String exception;

	public final void setException(final String exception) {
		this.exception = exception;
	}

	public final String getException() {
		return this.exception;
	}

}