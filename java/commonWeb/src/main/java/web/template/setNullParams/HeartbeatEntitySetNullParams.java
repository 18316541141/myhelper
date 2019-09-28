package web.template.setNullParams;

/**
 * 心跳监测表
 */
public final class HeartbeatEntitySetNullParams {

	/**
	 * 主键id
	 */
	private Long id;

	public final void setId(final Long id) {
		this.id = id;
	}

	public final Long getId() {
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
	 * 机器人id
	 */
	private boolean robotMac;

	public final void setRobotMac(final boolean robotMac) {
		this.robotMac = robotMac;
	}

	public final boolean getRobotMac() {
		return this.robotMac;
	}

}