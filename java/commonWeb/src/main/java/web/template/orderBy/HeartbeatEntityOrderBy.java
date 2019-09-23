package web.template.orderBy;
/**
 * 心跳监测表
 */
public final class HeartbeatEntityOrderBy {


		/**
		 * 主键id
		 */
		private boolean id;

		public final void setId(final boolean id){
			this.id=id;
		}

		public final boolean getId(){
			return this.id;
		}


		/**
		 * 最近一次的心跳时间
		 */
		private boolean lastHeartbeat;

		public final void setLastHeartbeat(final boolean lastHeartbeat){
			this.lastHeartbeat=lastHeartbeat;
		}

		public final boolean getLastHeartbeat(){
			return this.lastHeartbeat;
		}




		/**
		 * 机器人id
		 */
		private boolean robotId;

		public final void setRobotId(final boolean robotId){
			this.robotId=robotId;
		}

		public final boolean getRobotId(){
			return this.robotId;
		}


}