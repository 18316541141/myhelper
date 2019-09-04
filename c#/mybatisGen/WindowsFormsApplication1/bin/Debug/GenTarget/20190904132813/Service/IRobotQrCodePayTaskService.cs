using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication1.Entity;
using WebApplication1.MyExtensions;
using WebApplication1.Params;
using CommonHelper.AopInterceptor;
namespace WebApplication1.Service
{
	/// <summary>
    /// ***模块的业务类
    /// </summary>
	//[Intercept(typeof(DistributedTransactionScan))]	//启用分布式事务扫描
	public partial class IRobotQrCodePayTaskService : BaseService
    {
		public IRobotQrCodePayTaskRepository Repository { set; get; }

		/*抄考代码
		[DistributedTransactionScope] //启用分布式事务
----------------------------------------------------------------------------------------------------------------------------
		IRobotQrCodePayTask data = new IRobotQrCodePayTask
		{
	IRTaskID = NextId(),IROrderNo = param.IROrderNo,IRTakeMoney = param.IRTakeMoney,IRPushState = param.IRPushState,IRScanPayNotifyUrl = param.IRScanPayNotifyUrl,IRScanPayNotifyRet = param.IRScanPayNotifyRet,IRHandleState = param.IRHandleState,IRQrCodeImagePath = param.IRQrCodeImagePath,IRBsyNotifyState = param.IRBsyNotifyState,IRSendMoneyNotifyState = param.IRSendMoneyNotifyState,IRReportPicPath = param.IRReportPicPath,IRRemark = param.IRRemark,
		};
----------------------------------------------------------------------------------------------------------------------------
		IRobotQrCodePayTaskParams param = new IRobotQrCodePayTaskParams
		{
	IRTaskID = param.IRTaskID,IRTaskIDStart = param.IRTaskIDStart,IRTaskIDEnd = param.IRTaskIDEnd,IRTaskIDChange = param.IRTaskIDChange,IROrderNo = param.IROrderNo,IROrderNoLike = param.IROrderNoLike,IRTakeMoney = param.IRTakeMoney,IRTakeMoneyStart = param.IRTakeMoneyStart,IRTakeMoneyEnd = param.IRTakeMoneyEnd,IRTakeMoneyChange = param.IRTakeMoneyChange,IRPushState = param.IRPushState,IRPushStateStart = param.IRPushStateStart,IRPushStateEnd = param.IRPushStateEnd,IRPushStateChange = param.IRPushStateChange,IRScanPayNotifyUrl = param.IRScanPayNotifyUrl,IRScanPayNotifyUrlLike = param.IRScanPayNotifyUrlLike,IRScanPayNotifyRet = param.IRScanPayNotifyRet,IRScanPayNotifyRetLike = param.IRScanPayNotifyRetLike,IRHandleState = param.IRHandleState,IRHandleStateStart = param.IRHandleStateStart,IRHandleStateEnd = param.IRHandleStateEnd,IRHandleStateChange = param.IRHandleStateChange,IRQrCodeImagePath = param.IRQrCodeImagePath,IRQrCodeImagePathLike = param.IRQrCodeImagePathLike,IRBsyNotifyState = param.IRBsyNotifyState,IRBsyNotifyStateStart = param.IRBsyNotifyStateStart,IRBsyNotifyStateEnd = param.IRBsyNotifyStateEnd,IRBsyNotifyStateChange = param.IRBsyNotifyStateChange,IRSendMoneyNotifyState = param.IRSendMoneyNotifyState,IRSendMoneyNotifyStateStart = param.IRSendMoneyNotifyStateStart,IRSendMoneyNotifyStateEnd = param.IRSendMoneyNotifyStateEnd,IRSendMoneyNotifyStateChange = param.IRSendMoneyNotifyStateChange,IRReportPicPath = param.IRReportPicPath,IRReportPicPathLike = param.IRReportPicPathLike,IRRemark = param.IRRemark,IRRemarkLike = param.IRRemarkLike,
		};
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 分页查询***模块，并返回查询结果
        /// </summary>
        /// <param name="param">查询参数</param>
        /// <param name="currentPageIndex">当前页码</param>
        /// <param name="pageSize">每页显示的数据量</param>
        /// <returns>返回***模块的查询结果</returns>
		public MyPagedList<IRobotQrCodePayTask> Page(IRobotQrCodePayTaskParams param,int currentPageIndex = 1,int pageSize = 20)
		{
			return Repository.PageList(param, currentPageIndex, pageSize);
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 根据主键删除指定数据
        /// </summary>
        /// <param name="IRTaskID">删除数据的主键</param>
		public void Del(long IRTaskID)
		{
			Repository.Delete(a => a.IRTaskID);
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 新增一条数据
        /// </summary>
        /// <param name="data">新增的数据</param>
		public void Add(IRobotQrCodePayTask data)
		{
			Repository.Insert(data);
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 修改***模块的数据
        /// </summary>
        /// <param name="data">修改数据，不为null才会被修改，主键必须不为null</param>
        public void Update(IRobotQrCodePayTask data)
        {
            Repository.UpdateChange(new IRobotQrCodePayTask
            {
                IRTaskID = NextId(),IROrderNo = data.IROrderNo,IRTakeMoney = data.IRTakeMoney,IRPushState = data.IRPushState,IRScanPayNotifyUrl = data.IRScanPayNotifyUrl,IRScanPayNotifyRet = data.IRScanPayNotifyRet,IRHandleState = data.IRHandleState,IRQrCodeImagePath = data.IRQrCodeImagePath,IRBsyNotifyState = data.IRBsyNotifyState,IRSendMoneyNotifyState = data.IRSendMoneyNotifyState,IRReportPicPath = data.IRReportPicPath,IRRemark = data.IRRemark,
            });
        }
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 新增多条数据
        /// </summary>
        /// <param name="datas">新增的数据</param>
		public void AddBatch(List<IRobotQrCodePayTask> datas)
		{
			foreach(IRobotQrCodePayTask data in datas)
			{
				data.IRTaskID = NextId();
				data.CreateDate = DateTime.Now;
				data.Status = 1;
			}
			Repository.Insert(datas);
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 批量修改状态
        /// </summary>
        /// <param name="datas">修改状态的数据</param>
		public int ChangeStatus(List<IRobotQrCodePayTask> datas)
		{
			List<IRobotQrCodePayTask> updates = new List<IRobotQrCodePayTask>();
			for(IRobotQrCodePayTask data in datas)
			{
				updates.Add(new IRobotQrCodePayTask
				{
					IRTaskID = data.IRTaskID,
					Status = data.Status
				});
			}
			return Repository.UpdateChangeBatch(updates);
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 根据主键id查询***模块的数据实体
        /// </summary>
        /// <param name="IRTaskID">主键id</param>
		/// <returns>返回***模块的查询结果</returns>
		public IRobotQrCodePayTask Load(long IRTaskID)
		{
			return Repository.FindEntity(IRTaskID);
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="datas">批量删除的数据</param>
		public int DelBatch(IRobotQrCodePayTask datas)
		{
			return Repository.Delete(datas);
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 对“****”方法的入参进行校验基础（只校验是否为空，格式，长度，大小，不涉及
	/// 数据库查询、流读取等其他复杂操作），校验不通过时，异常往外抛出
        /// </summary>
        /// <param name="param">待校验参数</param>
        public void Check****(IRobotQrCodePayTaskParams param)
        {
            if(...){
		Ex(...,...);
	    }
        }
		*/
    }
}