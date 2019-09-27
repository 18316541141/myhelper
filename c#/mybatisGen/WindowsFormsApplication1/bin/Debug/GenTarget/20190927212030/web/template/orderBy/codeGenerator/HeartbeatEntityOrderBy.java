package web.template.orderBy.codeGenerator;
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
		 * 机器人的ip地址
		 */
		private boolean robotIp;

		public final void setRobotIp(final boolean robotIp){
			this.robotIp=robotIp;
		}

		public final boolean getRobotIp(){
			return this.robotIp;
		}



		/**
		 * 机器人备注
		 */
		private boolean remark;

		public final void setRemark(final boolean remark){
			this.remark=remark;
		}

		public final boolean getRemark(){
			return this.remark;
		}



		/**
		 * 扩展字段
		 */
		private boolean extendField;

		public final void setExtendField(final boolean extendField){
			this.extendField=extendField;
		}

		public final boolean getExtendField(){
			return this.extendField;
		}



		/**
		 * 监视服务器的url
		 */
		private boolean monitorServer;

		public final void setMonitorServer(final boolean monitorServer){
			this.monitorServer=monitorServer;
		}

		public final boolean getMonitorServer(){
			return this.monitorServer;
		}


}