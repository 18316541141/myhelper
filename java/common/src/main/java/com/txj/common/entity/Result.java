package com.txj.common.entity;

public class Result {
	private int code;
	private String msg;
	private Object data;
	private Object permissions;
	public int getCode() {
		return code;
	}
	public void setCode(int code) {
		this.code = code;
	}
	public String getMsg() {
		return msg;
	}
	public void setMsg(String msg) {
		this.msg = msg;
	}
	public Object getData() {
		return data;
	}
	public void setData(Object data) {
		this.data = data;
	}
	public Result() {
		super();
	}
	public Result(int code, String msg, Object data) {
		super();
		this.code = code;
		this.msg = msg;
		this.data = data;
	}
	public Object getPermissions() {
		return permissions;
	}
	public void setPermissions(Object permissions) {
		this.permissions = permissions;
	}
}
