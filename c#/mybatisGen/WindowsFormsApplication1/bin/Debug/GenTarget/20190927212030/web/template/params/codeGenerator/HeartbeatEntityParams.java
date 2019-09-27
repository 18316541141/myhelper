package web.template.params.codeGenerator;
import java.util.Date;
/**
 * 心跳监测表
 */
public final class HeartbeatEntityParams {

	/**
		* 主键id
		*/
	private Long id;

	public final void setId(final Long id){
		this.id=id;
	}

	public final Long getId(){
		return this.id;
	}

	/**
		* 最近一次的心跳时间
		*/
	private Date lastHeartbeat;

	public final void setLastHeartbeat(final Date lastHeartbeat){
		this.lastHeartbeat=lastHeartbeat;
	}

	public final Date getLastHeartbeat(){
		return this.lastHeartbeat;
	}

	/**
		* 最近一次的心跳时间
		*/
	private Date lastHeartbeatStart;

	public final void setLastHeartbeatStart(final Date lastHeartbeatStart){
		this.lastHeartbeatStart=lastHeartbeatStart;
	}

	public final Date getLastHeartbeatStart(){
		return this.lastHeartbeatStart;
	}

	/**
		* 最近一次的心跳时间
		*/
	private Date lastHeartbeatEnd;

	public final void setLastHeartbeatEnd(final Date lastHeartbeatEnd){
		this.lastHeartbeatEnd=lastHeartbeatEnd;
	}

	public final Date getLastHeartbeatEnd(){
		return this.lastHeartbeatEnd;
	}

	/**
		* 机器人的ip地址
		*/
	private String robotIp;

	public final void setRobotIp(final String robotIp){
		this.robotIp=robotIp;
	}

	public final String getRobotIp(){
		return this.robotIp;
	}

	/**
		* 机器人的ip地址
		*/
	private String robotIpLike;

	public final void setRobotIpLike(final String robotIpLike){
		this.robotIpLike=robotIpLike;
	}

	public final String getRobotIpLike(){
		return this.robotIpLike;
	}

	/**
		* 机器人备注
		*/
	private String remark;

	public final void setRemark(final String remark){
		this.remark=remark;
	}

	public final String getRemark(){
		return this.remark;
	}

	/**
		* 机器人备注
		*/
	private String remarkLike;

	public final void setRemarkLike(final String remarkLike){
		this.remarkLike=remarkLike;
	}

	public final String getRemarkLike(){
		return this.remarkLike;
	}

	/**
		* 扩展字段
		*/
	private String extendField;

	public final void setExtendField(final String extendField){
		this.extendField=extendField;
	}

	public final String getExtendField(){
		return this.extendField;
	}

	/**
		* 扩展字段
		*/
	private String extendFieldLike;

	public final void setExtendFieldLike(final String extendFieldLike){
		this.extendFieldLike=extendFieldLike;
	}

	public final String getExtendFieldLike(){
		return this.extendFieldLike;
	}

	/**
		* 监视服务器的url
		*/
	private String monitorServer;

	public final void setMonitorServer(final String monitorServer){
		this.monitorServer=monitorServer;
	}

	public final String getMonitorServer(){
		return this.monitorServer;
	}

	/**
		* 监视服务器的url
		*/
	private String monitorServerLike;

	public final void setMonitorServerLike(final String monitorServerLike){
		this.monitorServerLike=monitorServerLike;
	}

	public final String getMonitorServerLike(){
		return this.monitorServerLike;
	}

	/**
	 * 升序排列
	 */
	private HeartbeatEntityOrderBy orderByAsc;

	public final void setOrderByAsc(final HeartbeatEntityOrderBy orderByAsc){
		this.orderByAsc=orderByAsc;
	}

	public final HeartbeatEntityOrderBy getOrderByAsc(){
		return this.orderByAsc;
	}

	/**
	 * 降序排列
	 */
	private HeartbeatEntityOrderBy orderByDesc;

	public final void setOrderByDesc(final HeartbeatEntityOrderBy orderByDesc){
		this.orderByDesc=orderByDesc;
	}

	public final HeartbeatEntityOrderBy getOrderByDesc(){
		return this.orderByDesc;
	}
}