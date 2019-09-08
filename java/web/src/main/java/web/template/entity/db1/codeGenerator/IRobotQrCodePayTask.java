package web.template.entity.db1.codeGenerator;

import java.util.Date;

import com.txj.annotation.ExcelCol;
import com.txj.annotation.ExcelSheet;

/**
 * 微信商务助手二维码支付任务表
 */
@ExcelSheet(sheetNames = { "sheet1" }, groupNames = { "groupName1" })
public class IRobotQrCodePayTask {

	public IRobotQrCodePayTask() {
	}

	/**
	 * 自增id
	 */
	@ExcelCol(colIndexs={0},colNames={"自增id"},groupNames={"groupName1"})
	private Integer irTaskID;

	public void setIrTaskID(Integer irTaskID) {
		this.irTaskID = irTaskID;
	}

	public Integer getIrTaskID() {
		return this.irTaskID;
	}

	/**
	 * 任务单号(办事易全送过来保证必须唯一)
	 */
	@ExcelCol(colIndexs={1},colNames={"自增任务"},groupNames={"groupName1"})
	private String irOrderNo;

	public void setIrOrderNo(String irOrderNo) {
		this.irOrderNo = irOrderNo;
	}

	public String getIrOrderNo() {
		return this.irOrderNo;
	}

	/**
	 * 微信昵称
	 */
	@ExcelCol(colIndexs={2},colNames={"自增昵称"},groupNames={"groupName1"})
	private String irWeiXinNickName;

	public void setIrWeiXinNickName(String irWeiXinNickName) {
		this.irWeiXinNickName = irWeiXinNickName;
	}

	public String getIrWeiXinNickName() {
		return this.irWeiXinNickName;
	}

	/**
	 * 微信头像图片相对路径
	 */
	@ExcelCol(colIndexs={3},colNames={"自增相对"},groupNames={"groupName1"})
	private String irWeiXinHeaderImage;

	public void setIrWeiXinHeaderImage(String irWeiXinHeaderImage) {
		this.irWeiXinHeaderImage = irWeiXinHeaderImage;
	}

	public String getIrWeiXinHeaderImage() {
		return this.irWeiXinHeaderImage;
	}

	/**
	 * 二维码图片相对路径
	 */
	@ExcelCol(colIndexs={4},colNames={"自增维id"},groupNames={"groupName1"})
	private String irQrCodeImagePath;

	public void setIrQrCodeImagePath(String irQrCodeImagePath) {
		this.irQrCodeImagePath = irQrCodeImagePath;
	}

	public String getIrQrCodeImagePath() {
		return this.irQrCodeImagePath;
	}

	/**
	 * 处理状态,0:未处理,1:处理完毕,2:处理失败
	 */
	@ExcelCol(colIndexs={5},colNames={"自增处理"},groupNames={"groupName1"})
	private Integer irHandleState;

	public void setIrHandleState(Integer irHandleState) {
		this.irHandleState = irHandleState;
	}

	public Integer getIrHandleState() {
		return this.irHandleState;
	}

	/**
	 * 处理情况信息，描述处理过程中的详细问题
	 */
	@ExcelCol(colIndexs={6},colNames={"信息"},groupNames={"groupName1"})
	private String irHandleMessage;

	public void setIrHandleMessage(String irHandleMessage) {
		this.irHandleMessage = irHandleMessage;
	}

	public String getIrHandleMessage() {
		return this.irHandleMessage;
	}

	/**
	 * 处理时间
	 */
	@ExcelCol(colIndexs={7},colNames={"时间增id"},groupNames={"groupName1"})
	private Date irHandleTime;

	public void setIrHandleTime(Date irHandleTime) {
		this.irHandleTime = irHandleTime;
	}

	public Date getIrHandleTime() {
		return this.irHandleTime;
	}

	/**
	 * 任务登记时间
	 */
	@ExcelCol(colIndexs={8},colNames={"登记增id"},groupNames={"groupName1"})
	private Date irCreateTime;

	public void setIrCreateTime(Date irCreateTime) {
		this.irCreateTime = irCreateTime;
	}

	public Date getIrCreateTime() {
		return this.irCreateTime;
	}

	/**
	 * 处理结果截图的json列表
	 */
	@ExcelCol(colIndexs={9},colNames={"自截增id"},groupNames={"groupName1"})
	private String irReportPicPathJson;

	public void setIrReportPicPathJson(String irReportPicPathJson) {
		this.irReportPicPathJson = irReportPicPathJson;
	}

	public String getIrReportPicPathJson() {
		return this.irReportPicPathJson;
	}

	/**
	 * 收款金额，单位：分
	 */
	@ExcelCol(colIndexs={10},colNames={"自增金额"},groupNames={"groupName1"})
	private Integer irTakeMoney;

	public void setIrTakeMoney(Integer irTakeMoney) {
		this.irTakeMoney = irTakeMoney;
	}

	public Integer getIrTakeMoney() {
		return this.irTakeMoney;
	}

	/**
	 * 机器人编号
	 */
	@ExcelCol(colIndexs={11},colNames={"自增编号"},groupNames={"groupName1"})
	private String irRobotId;

	public void setIrRobotId(String irRobotId) {
		this.irRobotId = irRobotId;
	}

	public String getIrRobotId() {
		return this.irRobotId;
	}

	/**
	 * 支付备注
	 */
	@ExcelCol(colIndexs={12},colNames={"自增支付"},groupNames={"groupName1"})
	private String irRemark;

	public void setIrRemark(String irRemark) {
		this.irRemark = irRemark;
	}

	public String getIrRemark() {
		return this.irRemark;
	}

	/**
	 * 推送状态, 0:未推送, 1:推送成功, 2:推送失败
	 */
	@ExcelCol(colIndexs={13},colNames={"自增推送"},groupNames={"groupName1"})
	private Integer irPushState;

	public void setIrPushState(Integer irPushState) {
		this.irPushState = irPushState;
	}

	public Integer getIrPushState() {
		return this.irPushState;
	}

	/**
	 * 推送时间
	 */
	@ExcelCol(colIndexs={14},colNames={"推送增id"},groupNames={"groupName1"})
	private Date irPushTime;

	public void setIrPushTime(Date irPushTime) {
		this.irPushTime = irPushTime;
	}

	public Date getIrPushTime() {
		return this.irPushTime;
	}

	/**
	 * 办事易的ScanPayNotify接口返回结果
	 */
	@ExcelCol(colIndexs={15},colNames={"结果增id"},groupNames={"groupName1"})
	private String irScanPayNotifyRet;

	public void setIrScanPayNotifyRet(String irScanPayNotifyRet) {
		this.irScanPayNotifyRet = irScanPayNotifyRet;
	}

	public String getIrScanPayNotifyRet() {
		return this.irScanPayNotifyRet;
	}

	/**
	 * 该支付订单支付成功时回调。
	 */
	@ExcelCol(colIndexs={16},colNames={"自增回调"},groupNames={"groupName1"})
	private String irScanPayNotifyUrl;

	public void setIrScanPayNotifyUrl(String irScanPayNotifyUrl) {
		this.irScanPayNotifyUrl = irScanPayNotifyUrl;
	}

	public String getIrScanPayNotifyUrl() {
		return this.irScanPayNotifyUrl;
	}

}