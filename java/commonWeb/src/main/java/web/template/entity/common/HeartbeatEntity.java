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
	private Date lastHeartbeat;

	public final void setLastHeartbeat(final Date lastHeartbeat) {
		this.lastHeartbeat = lastHeartbeat;
	}

	public final Date getLastHeartbeat() {
		return this.lastHeartbeat;
	}

	/**
	 * 机器人id
	 */
	private String robotId;

	public final void setRobotId(final String robotId) {
		this.robotId = robotId;
	}

	public final String getRobotId() {
		return this.robotId;
	}

}