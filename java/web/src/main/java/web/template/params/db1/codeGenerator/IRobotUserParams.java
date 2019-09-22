package web.template.params.db1.codeGenerator;
import java.util.Date;

import web.template.orderBy.db1.codeGenerator.IRobotUserOrderBy;
/**
 * 
 */
public final class IRobotUserParams {

	/**
		* 主键id
		*/
	private Long iuId;

	public final void setIuId(final Long iuId){
		this.iuId=iuId;
	}

	public final Long getIuId(){
		return this.iuId;
	}

	/**
		* 用户名
		*/
	private String iuUsername;

	public final void setIuUsername(final String iuUsername){
		this.iuUsername=iuUsername;
	}

	public final String getIuUsername(){
		return this.iuUsername;
	}

	/**
		* 用户名
		*/
	private String iuUsernameLike;

	public final void setIuUsernameLike(final String iuUsernameLike){
		this.iuUsernameLike=iuUsernameLike;
	}

	public final String getIuUsernameLike(){
		return this.iuUsernameLike;
	}

	/**
		* 密码
		*/
	private String iuPassword;

	public final void setIuPassword(final String iuPassword){
		this.iuPassword=iuPassword;
	}

	public final String getIuPassword(){
		return this.iuPassword;
	}

	/**
		* 密码
		*/
	private String iuPasswordLike;

	public final void setIuPasswordLike(final String iuPasswordLike){
		this.iuPasswordLike=iuPasswordLike;
	}

	public final String getIuPasswordLike(){
		return this.iuPasswordLike;
	}

	/**
		* 签名的唯一标识
		*/
	private String iuSignKey;

	public final void setIuSignKey(final String iuSignKey){
		this.iuSignKey=iuSignKey;
	}

	public final String getIuSignKey(){
		return this.iuSignKey;
	}

	/**
		* 签名的唯一标识
		*/
	private String iuSignKeyLike;

	public final void setIuSignKeyLike(final String iuSignKeyLike){
		this.iuSignKeyLike=iuSignKeyLike;
	}

	public final String getIuSignKeyLike(){
		return this.iuSignKeyLike;
	}

	/**
		* 签名密钥
		*/
	private String iuSignSecret;

	public final void setIuSignSecret(final String iuSignSecret){
		this.iuSignSecret=iuSignSecret;
	}

	public final String getIuSignSecret(){
		return this.iuSignSecret;
	}

	/**
		* 签名密钥
		*/
	private String iuSignSecretLike;

	public final void setIuSignSecretLike(final String iuSignSecretLike){
		this.iuSignSecretLike=iuSignSecretLike;
	}

	public final String getIuSignSecretLike(){
		return this.iuSignSecretLike;
	}

	/**
		* 用户创建日期
		*/
	private Date iuCreateDate;

	public final void setIuCreateDate(final Date iuCreateDate){
		this.iuCreateDate=iuCreateDate;
	}

	public final Date getIuCreateDate(){
		return this.iuCreateDate;
	}

	/**
		* 用户创建日期
		*/
	private Date iuCreateDateStart;

	public final void setIuCreateDateStart(final Date iuCreateDateStart){
		this.iuCreateDateStart=iuCreateDateStart;
	}

	public final Date getIuCreateDateStart(){
		return this.iuCreateDateStart;
	}

	/**
		* 用户创建日期
		*/
	private Date iuCreateDateEnd;

	public final void setIuCreateDateEnd(final Date iuCreateDateEnd){
		this.iuCreateDateEnd=iuCreateDateEnd;
	}

	public final Date getIuCreateDateEnd(){
		return this.iuCreateDateEnd;
	}

	/**
	 * 升序排列
	 */
	private IRobotUserOrderBy orderByAsc;

	public final void setOrderByAsc(final IRobotUserOrderBy orderByAsc){
		this.orderByAsc=orderByAsc;
	}

	public final IRobotUserOrderBy getOrderByAsc(){
		return this.orderByAsc;
	}

	/**
	 * 降序排列
	 */
	private IRobotUserOrderBy orderByDesc;

	public final void setOrderByDesc(final IRobotUserOrderBy orderByDesc){
		this.orderByDesc=orderByDesc;
	}

	public final IRobotUserOrderBy getOrderByDesc(){
		return this.orderByDesc;
	}
}