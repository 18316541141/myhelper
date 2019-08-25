package web.template.entity.codeGenerator;

import java.util.Date;
/**
 * 微信商务助手二维码支付任务表
 */
public class IRobotQrCodePayTask {
	
	public IRobotQrCodePayTask() { }


		/**
		 * 自增id
		 */
		private Integer irTaskID;

		public void setIrTaskID(Integer irTaskID){
			this.irTaskID=irTaskID;
		}

		public Integer getIrTaskID(){
			return this.irTaskID;
		}



		/**
		 * 任务单号(办事易全送过来保证必须唯一)
		 */
		private String irOrderNo;

		public void setIrOrderNo(String irOrderNo){
			this.irOrderNo=irOrderNo;
		}

		public String getIrOrderNo(){
			return this.irOrderNo;
		}


		/**
		 * 微信昵称
		 */
		private String irWeiXinNickName;

		public void setIrWeiXinNickName(String irWeiXinNickName){
			this.irWeiXinNickName=irWeiXinNickName;
		}

		public String getIrWeiXinNickName(){
			return this.irWeiXinNickName;
		}


		/**
		 * 微信头像图片相对路径
		 */
		private String irWeiXinHeaderImage;

		public void setIrWeiXinHeaderImage(String irWeiXinHeaderImage){
			this.irWeiXinHeaderImage=irWeiXinHeaderImage;
		}

		public String getIrWeiXinHeaderImage(){
			return this.irWeiXinHeaderImage;
		}


		/**
		 * 二维码图片相对路径
		 */
		private String irQrCodeImagePath;

		public void setIrQrCodeImagePath(String irQrCodeImagePath){
			this.irQrCodeImagePath=irQrCodeImagePath;
		}

		public String getIrQrCodeImagePath(){
			return this.irQrCodeImagePath;
		}


		/**
		 * 处理状态,0:未处理,1:处理完毕,2:处理失败
		 */
		private Integer irHandleState;

		public void setIrHandleState(Integer irHandleState){
			this.irHandleState=irHandleState;
		}

		public Integer getIrHandleState(){
			return this.irHandleState;
		}



		/**
		 * 处理情况信息，描述处理过程中的详细问题
		 */
		private String irHandleMessage;

		public void setIrHandleMessage(String irHandleMessage){
			this.irHandleMessage=irHandleMessage;
		}

		public String getIrHandleMessage(){
			return this.irHandleMessage;
		}


		/**
		 * 处理时间
		 */
		private Date irHandleTime;

		public void setIrHandleTime(Date irHandleTime){
			this.irHandleTime=irHandleTime;
		}

		public Date getIrHandleTime(){
			return this.irHandleTime;
		}



		/**
		 * 任务登记时间
		 */
		private Date irCreateTime;

		public void setIrCreateTime(Date irCreateTime){
			this.irCreateTime=irCreateTime;
		}

		public Date getIrCreateTime(){
			return this.irCreateTime;
		}



		/**
		 * 处理结果截图的json列表
		 */
		private String irReportPicPathJson;

		public void setIrReportPicPathJson(String irReportPicPathJson){
			this.irReportPicPathJson=irReportPicPathJson;
		}

		public String getIrReportPicPathJson(){
			return this.irReportPicPathJson;
		}


		/**
		 * 收款金额，单位：分
		 */
		private Integer irTakeMoney;

		public void setIrTakeMoney(Integer irTakeMoney){
			this.irTakeMoney=irTakeMoney;
		}

		public Integer getIrTakeMoney(){
			return this.irTakeMoney;
		}



		/**
		 * 机器人编号
		 */
		private String irRobotId;

		public void setIrRobotId(String irRobotId){
			this.irRobotId=irRobotId;
		}

		public String getIrRobotId(){
			return this.irRobotId;
		}


		/**
		 * 支付备注
		 */
		private String irRemark;

		public void setIrRemark(String irRemark){
			this.irRemark=irRemark;
		}

		public String getIrRemark(){
			return this.irRemark;
		}


		/**
		 * 推送状态, 0:未推送, 1:推送成功, 2:推送失败
		 */
		private Integer irPushState;

		public void setIrPushState(Integer irPushState){
			this.irPushState=irPushState;
		}

		public Integer getIrPushState(){
			return this.irPushState;
		}



		/**
		 * 推送时间
		 */
		private Date irPushTime;

		public void setIrPushTime(Date irPushTime){
			this.irPushTime=irPushTime;
		}

		public Date getIrPushTime(){
			return this.irPushTime;
		}



		/**
		 * 办事易的ScanPayNotify接口返回结果
		 */
		private String irScanPayNotifyRet;

		public void setIrScanPayNotifyRet(String irScanPayNotifyRet){
			this.irScanPayNotifyRet=irScanPayNotifyRet;
		}

		public String getIrScanPayNotifyRet(){
			return this.irScanPayNotifyRet;
		}


		/**
		 * 该支付订单支付成功时回调。
		 */
		private String irScanPayNotifyUrl;

		public void setIrScanPayNotifyUrl(String irScanPayNotifyUrl){
			this.irScanPayNotifyUrl=irScanPayNotifyUrl;
		}

		public String getIrScanPayNotifyUrl(){
			return this.irScanPayNotifyUrl;
		}


}