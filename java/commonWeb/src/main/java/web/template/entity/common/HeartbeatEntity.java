package web.template.entity.common;

import java.util.Date;

/**
 * 心跳监测表
 */
public class HeartbeatEntity {

	public HeartbeatEntity() {
	}

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
	private Date lastHeartbeatTime;

	public final void setLastHeartbeatTime(final Date lastHeartbeatTime) {
		this.lastHeartbeatTime = lastHeartbeatTime;
	}

	public final Date getLastHeartbeatTime() {
		return this.lastHeartbeatTime;
	}

	public String statusDesc;

	public final String getStatusDesc() {
		if (getStatus() == null) {
			return "";
		} else {
			if (getStatus().byteValue() == 0) {
				return "已停止";
			} else if (getStatus().byteValue() == 1) {
				return "运行中";
			}
		}
		return "";
	}

	public final void setStatusDesc(String statusDesc) {
	}

	public Byte status;

	public final Byte getStatus() {
		if (getLastHeartbeatTime() == null) {
			return null;
		} else {
			if ((System.currentTimeMillis() - getLastHeartbeatTime().getTime()) > 600000) {
				return 0;
			} else {
				return 1;
			}
		}
	}

	public final void setStatus(Byte status) {
	}

	/**
	 * 机器人的ip地址
	 */
	private String robotIp;

	public final void setRobotIp(final String robotIp) {
		this.robotIp = robotIp;
	}

	public final String getRobotIp() {
		return this.robotIp;
	}

	/**
	 * 机器人备注
	 */
	private String remark;

	public final void setRemark(final String remark) {
		this.remark = remark;
	}

	public final String getRemark() {
		return this.remark;
	}

	/**
	 * 扩展字段
	 */
	private String extendField;

	public final void setExtendField(final String extendField) {
		this.extendField = extendField;
	}

	public final String getExtendField() {
		return this.extendField;
	}

	/**
	 * 监视服务器的url
	 */
	private String monitorServer;

	public final void setMonitorServer(final String monitorServer) {
		this.monitorServer = monitorServer;
	}

	public final String getMonitorServer() {
		return this.monitorServer;
	}

}