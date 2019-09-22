package com.txj.common.entity;

/**
 * 上传文件的返回值
 * @author admin
 *
 */
public class UploadFilesResult {
	/**
	 * 文件扩展名
	 */
	private String extension;
	/**
	 * 文件的sha1
	 */
	private String sha1;
	
	public String getExtension() {
		return extension;
	}
	public void setExtension(String extension) {
		this.extension = extension;
	}
	public String getSha1() {
		return sha1;
	}
	public void setSha1(String sha1) {
		this.sha1 = sha1;
	}
}
