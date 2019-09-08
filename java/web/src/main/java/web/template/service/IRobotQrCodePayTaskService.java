package web.template.service;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import com.txj.common.entity.MyPagedList;

import web.template.entity.db1.codeGenerator.IRobotQrCodePayTask;
import web.template.mapper.db1.IRobotQrCodePayTaskMapper;
import web.template.params.db1.codeGenerator.IRobotQrCodePayTaskParams;
import web.template.service.common.BaseService;

/**
 * ***模块的业务类
 * 
 * @author admin
 */
@Service
public class IRobotQrCodePayTaskService extends BaseService {

	@Autowired
	private IRobotQrCodePayTaskMapper mapper;

	/**
	 * 分页查询***模块，并返回查询结果
	 * 
	 * @param param
	 *            查询参数
	 * @param currentPageIndex
	 *            当前页码
	 * @param pageSize
	 *            每页显示的数据量
	 */
	public MyPagedList<IRobotQrCodePayTask> page(IRobotQrCodePayTaskParams param, int currentPageIndex, int pageSize) {
		return mapper.pageList(param, currentPageIndex, pageSize);
	}

	/**
	 * 分页查询***模块，并返回查询结果
	 * 
	 * @param param
	 *            查询参数
	 * @param currentPageIndex
	 *            当前页码
	 * @param pageSize
	 *            每页显示的数据量
	 */
	public MyPagedList<IRobotQrCodePayTask> export(IRobotQrCodePayTaskParams param, int currentPageIndex,
			int pageSize) {
		return mapper.bigPageList(param, currentPageIndex, pageSize);
	}

	/*
	 * 抄考代码
	 * -------------------------------------------------------------------------
	 * --------------------------------------------------- IRobotQrCodePayTask
	 * data = new IRobotQrCodePayTask();
	 * data.setIrTaskID(nextId());data.setIrOrderNo(param.getIrOrderNo());data.
	 * setIrWeiXinNickName(param.getIrWeiXinNickName());data.
	 * setIrWeiXinHeaderImage(param.getIrWeiXinHeaderImage());data.
	 * setIrQrCodeImagePath(param.getIrQrCodeImagePath());data.setIrHandleState(
	 * param.getIrHandleState());data.setIrHandleMessage(param.
	 * getIrHandleMessage());data.setIrHandleTime(param.getIrHandleTime());data.
	 * setIrCreateTime(param.getIrCreateTime());data.setIrReportPicPathJson(
	 * param.getIrReportPicPathJson());data.setIrTakeMoney(param.getIrTakeMoney(
	 * ));data.setIrRobotId(param.getIrRobotId());data.setIrRemark(param.
	 * getIrRemark());data.setIrPushState(param.getIrPushState());data.
	 * setIrPushTime(param.getIrPushTime());data.setIrScanPayNotifyRet(param.
	 * getIrScanPayNotifyRet());data.setIrScanPayNotifyUrl(param.
	 * getIrScanPayNotifyUrl());
	 * -------------------------------------------------------------------------
	 * ---------------------------------------------------
	 * IRobotQrCodePayTaskParams param = new IRobotQrCodePayTaskParams();
	 * param.setIrTaskID(param.getIrTaskID());param.setIrTaskIDStart(param.
	 * getIrTaskIDStart());param.setIrTaskIDEnd(param.getIrTaskIDEnd());param.
	 * setIrOrderNo(param.getIrOrderNo());param.setIrOrderNoLike(param.
	 * getIrOrderNoLike());param.setIrWeiXinNickName(param.getIrWeiXinNickName()
	 * );param.setIrWeiXinNickNameLike(param.getIrWeiXinNickNameLike());param.
	 * setIrWeiXinHeaderImage(param.getIrWeiXinHeaderImage());param.
	 * setIrWeiXinHeaderImageLike(param.getIrWeiXinHeaderImageLike());param.
	 * setIrQrCodeImagePath(param.getIrQrCodeImagePath());param.
	 * setIrQrCodeImagePathLike(param.getIrQrCodeImagePathLike());param.
	 * setIrHandleState(param.getIrHandleState());param.setIrHandleStateStart(
	 * param.getIrHandleStateStart());param.setIrHandleStateEnd(param.
	 * getIrHandleStateEnd());param.setIrHandleMessage(param.getIrHandleMessage(
	 * ));param.setIrHandleMessageLike(param.getIrHandleMessageLike());param.
	 * setIrHandleTime(param.getIrHandleTime());param.setIrHandleTimeStart(param
	 * .getIrHandleTimeStart());param.setIrHandleTimeEnd(param.
	 * getIrHandleTimeEnd());param.setIrCreateTime(param.getIrCreateTime());
	 * param.setIrCreateTimeStart(param.getIrCreateTimeStart());param.
	 * setIrCreateTimeEnd(param.getIrCreateTimeEnd());param.
	 * setIrReportPicPathJson(param.getIrReportPicPathJson());param.
	 * setIrReportPicPathJsonLike(param.getIrReportPicPathJsonLike());param.
	 * setIrTakeMoney(param.getIrTakeMoney());param.setIrTakeMoneyStart(param.
	 * getIrTakeMoneyStart());param.setIrTakeMoneyEnd(param.getIrTakeMoneyEnd())
	 * ;param.setIrRobotId(param.getIrRobotId());param.setIrRobotIdLike(param.
	 * getIrRobotIdLike());param.setIrRemark(param.getIrRemark());param.
	 * setIrRemarkLike(param.getIrRemarkLike());param.setIrPushState(param.
	 * getIrPushState());param.setIrPushStateStart(param.getIrPushStateStart());
	 * param.setIrPushStateEnd(param.getIrPushStateEnd());param.setIrPushTime(
	 * param.getIrPushTime());param.setIrPushTimeStart(param.getIrPushTimeStart(
	 * ));param.setIrPushTimeEnd(param.getIrPushTimeEnd());param.
	 * setIrScanPayNotifyRet(param.getIrScanPayNotifyRet());param.
	 * setIrScanPayNotifyRetLike(param.getIrScanPayNotifyRetLike());param.
	 * setIrScanPayNotifyUrl(param.getIrScanPayNotifyUrl());param.
	 * setIrScanPayNotifyUrlLike(param.getIrScanPayNotifyUrlLike());
	 * -------------------------------------------------------------------------
	 * --------------------------------------------------- 分页查询***模块，并返回查询结果
	 * 
	 * @param param 查询参数
	 * 
	 * @param currentPageIndex 当前页码
	 * 
	 * @param pageSize 每页显示的数据量 public MyPagedList<IRobotQrCodePayTask>
	 * page(IRobotQrCodePayTaskParams param,int currentPageIndex,int pageSize){
	 * return mapper.pageList(param, currentPageIndex, pageSize); }
	 * -------------------------------------------------------------------------
	 * --------------------------------------------------- 根据主键删除指定数据
	 * 
	 * @param irTaskID 删除数据的主键 public void del(long irTaskID){
	 * mapper.delete(irTaskID); }
	 * -------------------------------------------------------------------------
	 * --------------------------------------------------- 新增一条数据
	 * 
	 * @param data 新增的数据 public void add(IRobotQrCodePayTask data){
	 * mapper.insert(data); }
	 * -------------------------------------------------------------------------
	 * --------------------------------------------------- 修改***模块的数据
	 * 
	 * @param data 修改数据，不为null才会被修改，主键必须不为null public void
	 * update(IRobotQrCodePayTask data){ IRobotQrCodePayTask param=new
	 * IRobotQrCodePayTask();
	 * param.setIrTaskID(nextId());param.setIrOrderNo(data.getIrOrderNo());param
	 * .setIrWeiXinNickName(data.getIrWeiXinNickName());param.
	 * setIrWeiXinHeaderImage(data.getIrWeiXinHeaderImage());param.
	 * setIrQrCodeImagePath(data.getIrQrCodeImagePath());param.setIrHandleState(
	 * data.getIrHandleState());param.setIrHandleMessage(data.getIrHandleMessage
	 * ());param.setIrHandleTime(data.getIrHandleTime());param.setIrCreateTime(
	 * data.getIrCreateTime());param.setIrReportPicPathJson(data.
	 * getIrReportPicPathJson());param.setIrTakeMoney(data.getIrTakeMoney());
	 * param.setIrRobotId(data.getIrRobotId());param.setIrRemark(data.
	 * getIrRemark());param.setIrPushState(data.getIrPushState());param.
	 * setIrPushTime(data.getIrPushTime());param.setIrScanPayNotifyRet(data.
	 * getIrScanPayNotifyRet());param.setIrScanPayNotifyUrl(data.
	 * getIrScanPayNotifyUrl()); mapper.updateChange(param); }
	 * -------------------------------------------------------------------------
	 * --------------------------------------------------- 新增多条数据
	 * 
	 * @param datas 新增的数据 public void addBatch(List<IRobotQrCodePayTask> datas){
	 * List<IRobotQrCodePayTask> insertDatas = new
	 * ArrayList<IRobotQrCodePayTask>(); for(IRobotQrCodePayTask data : datas){
	 * 
	 * insertDatas.add(); } mapper.insert(datas); }
	 * -------------------------------------------------------------------------
	 * --------------------------------------------------- 批量修改状态
	 * 
	 * @param datas 修改状态的数据 public int changeStatus(List<IRobotQrCodePayTask>
	 * datas){ List<IRobotQrCodePayTask> updates = new
	 * List<IRobotQrCodePayTask>(); for(IRobotQrCodePayTask data : datas){
	 * IRobotQrCodePayTask update=new IRobotQrCodePayTask();
	 * update.setirTaskID(data.getirTaskID());
	 * update.setStatus(data.getStatus()); updates.add(update); } return
	 * mapper.updateChangeBatch(updates); }
	 * -------------------------------------------------------------------------
	 * --------------------------------------------------- 根据主键id查询***模块的数据实体
	 * 
	 * @param irTaskID 主键id
	 * 
	 * @return 返回***模块的查询结果 public IRobotQrCodePayTask load(long irTaskID){
	 * return mapper.findEntity(irTaskID); }
	 * -------------------------------------------------------------------------
	 * --------------------------------------------------- 批量删除数据
	 * 
	 * @param datas 批量删除的数据 public int delBatch(List<IRobotQrCodePayTask>
	 * datas){ return mapper.deleteList(datas); }
	 * -------------------------------------------------------------------------
	 * ---------------------------------------------------
	 * 对“****”方法的入参进行校验基础（只校验是否为空，格式，长度，大小，不涉及 数据库查询、流读取等其他复杂操作），校验不通过时，异常往外抛出
	 * 
	 * @param param 待校验参数 public void check****(IRobotQrCodePayTaskParams
	 * param){ if(...){ Ex(...,...); } }
	 */
}