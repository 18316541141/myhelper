package web.template.orderBy;

/**
 * 心跳监测表
 */
public final class HeartbeatEntityOrderBy {

	/**
	 * 主键id
	 */
	private boolean id;

	public final void setId(final boolean id) {
		this.id = id;
	}

	public final boolean getId() {
		return this.id;
	}

	/**
	 * 最近一次的心跳时间
	 */
	private boolean lastHeartbeatTime;

	public final void setLastHeartbeatTime(final boolean lastHeartbeatTime) {
		this.lastHeartbeatTime = lastHeartbeatTime;
	}

	public final boolean getLastHeartbeatTime() {
		return this.lastHeartbeatTime;
	}

	/**
	 * 机器人的ip地址
	 */
	private boolean robotMac;

	public final void setRobotMac(final boolean robotMac) {
		this.robotMac = robotMac;
	}

	public final boolean getRobotMac() {
		return this.robotMac;
	}

	/**
	 * 机器人备注
	 */
	private boolean remark;

	public final void setRemark(final boolean remark) {
		this.remark = remark;
	}

	public final boolean getRemark() {
		return this.remark;
	}

	/**
	 * 扩展字段
	 */
	private boolean extendField;

	public final void setExtendField(final boolean extendField) {
		this.extendField = extendField;
	}

	public final boolean getExtendField() {
		return this.extendField;
	}

	/**
	 * 监视服务器的url
	 */
	private boolean monitorServer;

	public final void setMonitorServer(final boolean monitorServer) {
		this.monitorServer = monitorServer;
	}

	public final boolean getMonitorServer() {
		return this.monitorServer;
	}

}