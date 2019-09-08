package web.template.setNullParams.db1.codeGenerator;
import java.util.Date;
/**
 * 微信商务助手二维码支付任务表
 */
public class IRobotQrCodePayTaskSetNullParams {

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
		private boolean irOrderNo;

		public void setIrOrderNo(boolean irOrderNo){
			this.irOrderNo=irOrderNo;
		}

		public boolean getIrOrderNo(){
			return this.irOrderNo;
		}


		/**
		 * 微信昵称
		 */
		private boolean irWeiXinNickName;

		public void setIrWeiXinNickName(boolean irWeiXinNickName){
			this.irWeiXinNickName=irWeiXinNickName;
		}

		public boolean getIrWeiXinNickName(){
			return this.irWeiXinNickName;
		}


		/**
		 * 微信头像图片相对路径
		 */
		private boolean irWeiXinHeaderImage;

		public void setIrWeiXinHeaderImage(boolean irWeiXinHeaderImage){
			this.irWeiXinHeaderImage=irWeiXinHeaderImage;
		}

		public boolean getIrWeiXinHeaderImage(){
			return this.irWeiXinHeaderImage;
		}


		/**
		 * 二维码图片相对路径
		 */
		private boolean irQrCodeImagePath;

		public void setIrQrCodeImagePath(boolean irQrCodeImagePath){
			this.irQrCodeImagePath=irQrCodeImagePath;
		}

		public boolean getIrQrCodeImagePath(){
			return this.irQrCodeImagePath;
		}


		/**
		 * 处理状态,0:未处理,1:处理完毕,2:处理失败
		 */
		private boolean irHandleState;

		public void setIrHandleState(boolean irHandleState){
			this.irHandleState=irHandleState;
		}

		public boolean getIrHandleState(){
			return this.irHandleState;
		}



		/**
		 * 处理情况信息，描述处理过程中的详细问题
		 */
		private boolean irHandleMessage;

		public void setIrHandleMessage(boolean irHandleMessage){
			this.irHandleMessage=irHandleMessage;
		}

		public boolean getIrHandleMessage(){
			return this.irHandleMessage;
		}


		/**
		 * 处理时间
		 */
		private boolean irHandleTime;

		public void setIrHandleTime(boolean irHandleTime){
			this.irHandleTime=irHandleTime;
		}

		public boolean getIrHandleTime(){
			return this.irHandleTime;
		}



		/**
		 * 任务登记时间
		 */
		private boolean irCreateTime;

		public void setIrCreateTime(boolean irCreateTime){
			this.irCreateTime=irCreateTime;
		}

		public boolean getIrCreateTime(){
			return this.irCreateTime;
		}



		/**
		 * 处理结果截图的json列表
		 */
		private boolean irReportPicPathJson;

		public void setIrReportPicPathJson(boolean irReportPicPathJson){
			this.irReportPicPathJson=irReportPicPathJson;
		}

		public boolean getIrReportPicPathJson(){
			return this.irReportPicPathJson;
		}


		/**
		 * 收款金额，单位：分
		 */
		private boolean irTakeMoney;

		public void setIrTakeMoney(boolean irTakeMoney){
			this.irTakeMoney=irTakeMoney;
		}

		public boolean getIrTakeMoney(){
			return this.irTakeMoney;
		}



		/**
		 * 机器人编号
		 */
		private boolean irRobotId;

		public void setIrRobotId(boolean irRobotId){
			this.irRobotId=irRobotId;
		}

		public boolean getIrRobotId(){
			return this.irRobotId;
		}


		/**
		 * 支付备注
		 */
		private boolean irRemark;

		public void setIrRemark(boolean irRemark){
			this.irRemark=irRemark;
		}

		public boolean getIrRemark(){
			return this.irRemark;
		}


		/**
		 * 推送状态, 0:未推送, 1:推送成功, 2:推送失败
		 */
		private boolean irPushState;

		public void setIrPushState(boolean irPushState){
			this.irPushState=irPushState;
		}

		public boolean getIrPushState(){
			return this.irPushState;
		}



		/**
		 * 推送时间
		 */
		private boolean irPushTime;

		public void setIrPushTime(boolean irPushTime){
			this.irPushTime=irPushTime;
		}

		public boolean getIrPushTime(){
			return this.irPushTime;
		}



		/**
		 * 办事易的ScanPayNotify接口返回结果
		 */
		private boolean irScanPayNotifyRet;

		public void setIrScanPayNotifyRet(boolean irScanPayNotifyRet){
			this.irScanPayNotifyRet=irScanPayNotifyRet;
		}

		public boolean getIrScanPayNotifyRet(){
			return this.irScanPayNotifyRet;
		}


		/**
		 * 该支付订单支付成功时回调。
		 */
		private boolean irScanPayNotifyUrl;

		public void setIrScanPayNotifyUrl(boolean irScanPayNotifyUrl){
			this.irScanPayNotifyUrl=irScanPayNotifyUrl;
		}

		public boolean getIrScanPayNotifyUrl(){
			return this.irScanPayNotifyUrl;
		}


}