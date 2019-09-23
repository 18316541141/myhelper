package web.template.setNullParams;

/**
 * 
 */
public final class LogEntitySetNullParams {

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
	private boolean createDate;

	public final void setCreateDate(final boolean createDate) {
		this.createDate = createDate;
	}

	public final boolean getCreateDate() {
		return this.createDate;
	}

	/**
	 * 日志分级
	 */
	private boolean level;

	public final void setLevel(final boolean level) {
		this.level = level;
	}

	public final boolean getLevel() {
		return this.level;
	}

	/**
	 * 线程号
	 */
	private boolean threadNo;

	public final void setThreadNo(final boolean threadNo) {
		this.threadNo = threadNo;
	}

	public final boolean getThreadNo() {
		return this.threadNo;
	}

	/**
	 * 日志内容
	 */
	private boolean message;

	public final void setMessage(final boolean message) {
		this.message = message;
	}

	public final boolean getMessage() {
		return this.message;
	}

	/**
	 * 日志发生的项目名
	 */
	private boolean projectName;

	public final void setProjectName(final boolean projectName) {
		this.projectName = projectName;
	}

	public final boolean getProjectName() {
		return this.projectName;
	}

	/**
	 * 日志发生的类型
	 */
	private boolean typeName;

	public final void setTypeName(final boolean typeName) {
		this.typeName = typeName;
	}

	public final boolean getTypeName() {
		return this.typeName;
	}

	/**
	 * 日志发生的方法名称
	 */
	private boolean funcName;

	public final void setFuncName(final boolean funcName) {
		this.funcName = funcName;
	}

	public final boolean getFuncName() {
		return this.funcName;
	}

	/**
	 * 日志的异常堆栈信息
	 */
	private boolean exception;

	public final void setException(final boolean exception) {
		this.exception = exception;
	}

	public final boolean getException() {
		return this.exception;
	}

}