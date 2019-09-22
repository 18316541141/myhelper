package web.template.entity.db1.codeGenerator;

import java.util.Date;

/**
 * 
 */
public class IRobotUser {

	public IRobotUser() {
	}

	/**
	 * 主键id
	 */
	private Long iuId;

	public final void setIuId(final Long iuId) {
		this.iuId = iuId;
	}

	public final Long getIuId() {
		return this.iuId;
	}

	/**
	 * 用户名
	 */
	private String iuUsername;

	public final void setIuUsername(final String iuUsername) {
		this.iuUsername = iuUsername;
	}

	public final String getIuUsername() {
		return this.iuUsername;
	}

	/**
	 * 密码
	 */
	private String iuPassword;

	public final void setIuPassword(final String iuPassword) {
		this.iuPassword = iuPassword;
	}

	public final String getIuPassword() {
		return this.iuPassword;
	}

	/**
	 * 签名的唯一标识
	 */
	private String iuSignKey;

	public final void setIuSignKey(final String iuSignKey) {
		this.iuSignKey = iuSignKey;
	}

	public final String getIuSignKey() {
		return this.iuSignKey;
	}

	/**
	 * 签名密钥
	 */
	private String iuSignSecret;

	public final void setIuSignSecret(final String iuSignSecret) {
		this.iuSignSecret = iuSignSecret;
	}

	public final String getIuSignSecret() {
		return this.iuSignSecret;
	}

	/**
	 * 用户创建日期
	 */
	private Date iuCreateDate;

	public final void setIuCreateDate(final Date iuCreateDate) {
		this.iuCreateDate = iuCreateDate;
	}

	public final Date getIuCreateDate() {
		return this.iuCreateDate;
	}

}