package web.template.params;

import java.util.Date;

import web.template.orderBy.LogEntityOrderBy;

/**
 * 
 */
public final class LogEntityParams {

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
	 * 日志日期
	 */
	private Date createDateStart;

	public final void setCreateDateStart(final Date createDateStart) {
		this.createDateStart = createDateStart;
	}

	public final Date getCreateDateStart() {
		return this.createDateStart;
	}

	/**
	 * 日志日期
	 */
	private Date createDateEnd;

	public final void setCreateDateEnd(final Date createDateEnd) {
		this.createDateEnd = createDateEnd;
	}

	public final Date getCreateDateEnd() {
		return this.createDateEnd;
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
	 * 日志分级
	 */
	private String levelLike;

	public final void setLevelLike(final String levelLike) {
		this.levelLike = levelLike;
	}

	public final String getLevelLike() {
		return this.levelLike;
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
	 * 线程号
	 */
	private String threadNoLike;

	public final void setThreadNoLike(final String threadNoLike) {
		this.threadNoLike = threadNoLike;
	}

	public final String getThreadNoLike() {
		return this.threadNoLike;
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
	 * 日志内容
	 */
	private String messageLike;

	public final void setMessageLike(final String messageLike) {
		this.messageLike = messageLike;
	}

	public final String getMessageLike() {
		return this.messageLike;
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
	 * 日志发生的项目名
	 */
	private String projectNameLike;

	public final void setProjectNameLike(final String projectNameLike) {
		this.projectNameLike = projectNameLike;
	}

	public final String getProjectNameLike() {
		return this.projectNameLike;
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
	 * 日志发生的类型
	 */
	private String typeNameLike;

	public final void setTypeNameLike(final String typeNameLike) {
		this.typeNameLike = typeNameLike;
	}

	public final String getTypeNameLike() {
		return this.typeNameLike;
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
	 * 日志发生的方法名称
	 */
	private String funcNameLike;

	public final void setFuncNameLike(final String funcNameLike) {
		this.funcNameLike = funcNameLike;
	}

	public final String getFuncNameLike() {
		return this.funcNameLike;
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

	/**
	 * 日志的异常堆栈信息
	 */
	private String exceptionLike;

	public final void setExceptionLike(final String exceptionLike) {
		this.exceptionLike = exceptionLike;
	}

	public final String getExceptionLike() {
		return this.exceptionLike;
	}

	/**
	 * 升序排列
	 */
	private LogEntityOrderBy orderByAsc;

	public final void setOrderByAsc(final LogEntityOrderBy orderByAsc) {
		this.orderByAsc = orderByAsc;
	}

	public final LogEntityOrderBy getOrderByAsc() {
		return this.orderByAsc;
	}

	/**
	 * 降序排列
	 */
	private LogEntityOrderBy orderByDesc;

	public final void setOrderByDesc(final LogEntityOrderBy orderByDesc) {
		this.orderByDesc = orderByDesc;
	}

	public final LogEntityOrderBy getOrderByDesc() {
		return this.orderByDesc;
	}
}