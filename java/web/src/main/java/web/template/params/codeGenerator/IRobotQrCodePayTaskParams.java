package web.template.params.codeGenerator;
import java.util.Date;

import web.template.orderBy.codeGenerator.IRobotQrCodePayTaskOrderBy;
/**
 * 微信商务助手二维码支付任务表
 */
public class IRobotQrCodePayTaskParams {

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
		* 自增id
		*/
	private Integer irTaskIDStart;

	public void setIrTaskIDStart(Integer irTaskIDStart){
		this.irTaskIDStart=irTaskIDStart;
	}

	public Integer getIrTaskIDStart(){
		return this.irTaskIDStart;
	}

	/**
		* 自增id
		*/
	private Integer irTaskIDEnd;

	public void setIrTaskIDEnd(Integer irTaskIDEnd){
		this.irTaskIDEnd=irTaskIDEnd;
	}

	public Integer getIrTaskIDEnd(){
		return this.irTaskIDEnd;
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
		* 任务单号(办事易全送过来保证必须唯一)
		*/
	private String irOrderNoLike;

	public void setIrOrderNoLike(String irOrderNoLike){
		this.irOrderNoLike=irOrderNoLike;
	}

	public String getIrOrderNoLike(){
		return this.irOrderNoLike;
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
		* 微信昵称
		*/
	private String irWeiXinNickNameLike;

	public void setIrWeiXinNickNameLike(String irWeiXinNickNameLike){
		this.irWeiXinNickNameLike=irWeiXinNickNameLike;
	}

	public String getIrWeiXinNickNameLike(){
		return this.irWeiXinNickNameLike;
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
		* 微信头像图片相对路径
		*/
	private String irWeiXinHeaderImageLike;

	public void setIrWeiXinHeaderImageLike(String irWeiXinHeaderImageLike){
		this.irWeiXinHeaderImageLike=irWeiXinHeaderImageLike;
	}

	public String getIrWeiXinHeaderImageLike(){
		return this.irWeiXinHeaderImageLike;
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
		* 二维码图片相对路径
		*/
	private String irQrCodeImagePathLike;

	public void setIrQrCodeImagePathLike(String irQrCodeImagePathLike){
		this.irQrCodeImagePathLike=irQrCodeImagePathLike;
	}

	public String getIrQrCodeImagePathLike(){
		return this.irQrCodeImagePathLike;
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
		* 处理状态,0:未处理,1:处理完毕,2:处理失败
		*/
	private Integer irHandleStateStart;

	public void setIrHandleStateStart(Integer irHandleStateStart){
		this.irHandleStateStart=irHandleStateStart;
	}

	public Integer getIrHandleStateStart(){
		return this.irHandleStateStart;
	}

	/**
		* 处理状态,0:未处理,1:处理完毕,2:处理失败
		*/
	private Integer irHandleStateEnd;

	public void setIrHandleStateEnd(Integer irHandleStateEnd){
		this.irHandleStateEnd=irHandleStateEnd;
	}

	public Integer getIrHandleStateEnd(){
		return this.irHandleStateEnd;
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
		* 处理情况信息，描述处理过程中的详细问题
		*/
	private String irHandleMessageLike;

	public void setIrHandleMessageLike(String irHandleMessageLike){
		this.irHandleMessageLike=irHandleMessageLike;
	}

	public String getIrHandleMessageLike(){
		return this.irHandleMessageLike;
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
		* 处理时间
		*/
	private Date irHandleTimeStart;

	public void setIrHandleTimeStart(Date irHandleTimeStart){
		this.irHandleTimeStart=irHandleTimeStart;
	}

	public Date getIrHandleTimeStart(){
		return this.irHandleTimeStart;
	}

	/**
		* 处理时间
		*/
	private Date irHandleTimeEnd;

	public void setIrHandleTimeEnd(Date irHandleTimeEnd){
		this.irHandleTimeEnd=irHandleTimeEnd;
	}

	public Date getIrHandleTimeEnd(){
		return this.irHandleTimeEnd;
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
		* 任务登记时间
		*/
	private Date irCreateTimeStart;

	public void setIrCreateTimeStart(Date irCreateTimeStart){
		this.irCreateTimeStart=irCreateTimeStart;
	}

	public Date getIrCreateTimeStart(){
		return this.irCreateTimeStart;
	}

	/**
		* 任务登记时间
		*/
	private Date irCreateTimeEnd;

	public void setIrCreateTimeEnd(Date irCreateTimeEnd){
		this.irCreateTimeEnd=irCreateTimeEnd;
	}

	public Date getIrCreateTimeEnd(){
		return this.irCreateTimeEnd;
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
		* 处理结果截图的json列表
		*/
	private String irReportPicPathJsonLike;

	public void setIrReportPicPathJsonLike(String irReportPicPathJsonLike){
		this.irReportPicPathJsonLike=irReportPicPathJsonLike;
	}

	public String getIrReportPicPathJsonLike(){
		return this.irReportPicPathJsonLike;
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
		* 收款金额，单位：分
		*/
	private Integer irTakeMoneyStart;

	public void setIrTakeMoneyStart(Integer irTakeMoneyStart){
		this.irTakeMoneyStart=irTakeMoneyStart;
	}

	public Integer getIrTakeMoneyStart(){
		return this.irTakeMoneyStart;
	}

	/**
		* 收款金额，单位：分
		*/
	private Integer irTakeMoneyEnd;

	public void setIrTakeMoneyEnd(Integer irTakeMoneyEnd){
		this.irTakeMoneyEnd=irTakeMoneyEnd;
	}

	public Integer getIrTakeMoneyEnd(){
		return this.irTakeMoneyEnd;
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
		* 机器人编号
		*/
	private String irRobotIdLike;

	public void setIrRobotIdLike(String irRobotIdLike){
		this.irRobotIdLike=irRobotIdLike;
	}

	public String getIrRobotIdLike(){
		return this.irRobotIdLike;
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
		* 支付备注
		*/
	private String irRemarkLike;

	public void setIrRemarkLike(String irRemarkLike){
		this.irRemarkLike=irRemarkLike;
	}

	public String getIrRemarkLike(){
		return this.irRemarkLike;
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
		* 推送状态, 0:未推送, 1:推送成功, 2:推送失败
		*/
	private Integer irPushStateStart;

	public void setIrPushStateStart(Integer irPushStateStart){
		this.irPushStateStart=irPushStateStart;
	}

	public Integer getIrPushStateStart(){
		return this.irPushStateStart;
	}

	/**
		* 推送状态, 0:未推送, 1:推送成功, 2:推送失败
		*/
	private Integer irPushStateEnd;

	public void setIrPushStateEnd(Integer irPushStateEnd){
		this.irPushStateEnd=irPushStateEnd;
	}

	public Integer getIrPushStateEnd(){
		return this.irPushStateEnd;
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
		* 推送时间
		*/
	private Date irPushTimeStart;

	public void setIrPushTimeStart(Date irPushTimeStart){
		this.irPushTimeStart=irPushTimeStart;
	}

	public Date getIrPushTimeStart(){
		return this.irPushTimeStart;
	}

	/**
		* 推送时间
		*/
	private Date irPushTimeEnd;

	public void setIrPushTimeEnd(Date irPushTimeEnd){
		this.irPushTimeEnd=irPushTimeEnd;
	}

	public Date getIrPushTimeEnd(){
		return this.irPushTimeEnd;
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
		* 办事易的ScanPayNotify接口返回结果
		*/
	private String irScanPayNotifyRetLike;

	public void setIrScanPayNotifyRetLike(String irScanPayNotifyRetLike){
		this.irScanPayNotifyRetLike=irScanPayNotifyRetLike;
	}

	public String getIrScanPayNotifyRetLike(){
		return this.irScanPayNotifyRetLike;
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

	/**
		* 该支付订单支付成功时回调。
		*/
	private String irScanPayNotifyUrlLike;

	public void setIrScanPayNotifyUrlLike(String irScanPayNotifyUrlLike){
		this.irScanPayNotifyUrlLike=irScanPayNotifyUrlLike;
	}

	public String getIrScanPayNotifyUrlLike(){
		return this.irScanPayNotifyUrlLike;
	}

	/**
	 * 升序排列
	 */
	private IRobotQrCodePayTaskOrderBy orderByAsc;

	public void setOrderByAsc(IRobotQrCodePayTaskOrderBy orderByAsc){
		this.orderByAsc=orderByAsc;
	}

	public IRobotQrCodePayTaskOrderBy getOrderByAsc(){
		return this.orderByAsc;
	}

	/**
	 * 降序排列
	 */
	private IRobotQrCodePayTaskOrderBy orderByDesc;

	public void setOrderByDesc(IRobotQrCodePayTaskOrderBy orderByDesc){
		this.orderByDesc=orderByDesc;
	}

	public IRobotQrCodePayTaskOrderBy getOrderByDesc(){
		return this.orderByDesc;
	}
}