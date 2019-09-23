package web.template.params;
import java.util.Date;

import web.template.orderBy.HeartbeatEntityOrderBy;

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
		* 机器人id
		*/
	private String robotId;

	public final void setRobotId(final String robotId){
		this.robotId=robotId;
	}

	public final String getRobotId(){
		return this.robotId;
	}

	/**
		* 机器人id
		*/
	private String robotIdLike;

	public final void setRobotIdLike(final String robotIdLike){
		this.robotIdLike=robotIdLike;
	}

	public final String getRobotIdLike(){
		return this.robotIdLike;
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