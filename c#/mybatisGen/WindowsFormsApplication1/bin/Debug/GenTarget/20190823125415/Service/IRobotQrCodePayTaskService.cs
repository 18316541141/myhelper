using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication1.Entity;
using WebApplication1.MyExtensions;
using WebApplication1.Params;
namespace WebApplication1.Service
{
	/// <summary>
        /// ***模块的业务类
        /// </summary>
    public partial class IRobotQrCodePayTaskService : BaseService
    {
		public IRobotQrCodePayTaskRepository Repository { set; get; }

		/*抄考代码
----------------------------------------------------------------------------------------------------------------------------
		IRobotQrCodePayTask data = new IRobotQrCodePayTask
		{
	IRTaskID = NextId(),IROrderNo = param.IROrderNo,IRWeiXinNickName = param.IRWeiXinNickName,IRWeiXinHeaderImage = param.IRWeiXinHeaderImage,IRQrCodeImagePath = param.IRQrCodeImagePath,IRHandleState = param.IRHandleState,IRHandleMessage = param.IRHandleMessage,IRHandleTime = param.IRHandleTime,IRCreateTime = param.IRCreateTime,IRReportPicPathJson = param.IRReportPicPathJson,IRTakeMoney = param.IRTakeMoney,IRRobotId = param.IRRobotId,IRRemark = param.IRRemark,IRPushState = param.IRPushState,IRPushTime = param.IRPushTime,IRScanPayNotifyRet = param.IRScanPayNotifyRet,IRScanPayNotifyUrl = param.IRScanPayNotifyUrl,
		};
----------------------------------------------------------------------------------------------------------------------------
		IRobotQrCodePayTaskParams param = new IRobotQrCodePayTaskParams
		{
	IRTaskID = param.IRTaskID,IRTaskIDStart = param.IRTaskIDStart,IRTaskIDEnd = param.IRTaskIDEnd,IROrderNo = param.IROrderNo,IROrderNoLike = param.IROrderNoLike,IRWeiXinNickName = param.IRWeiXinNickName,IRWeiXinNickNameLike = param.IRWeiXinNickNameLike,IRWeiXinHeaderImage = param.IRWeiXinHeaderImage,IRWeiXinHeaderImageLike = param.IRWeiXinHeaderImageLike,IRQrCodeImagePath = param.IRQrCodeImagePath,IRQrCodeImagePathLike = param.IRQrCodeImagePathLike,IRHandleState = param.IRHandleState,IRHandleStateStart = param.IRHandleStateStart,IRHandleStateEnd = param.IRHandleStateEnd,IRHandleMessage = param.IRHandleMessage,IRHandleMessageLike = param.IRHandleMessageLike,IRHandleTime = param.IRHandleTime,IRHandleTimeStart = param.IRHandleTimeStart,IRHandleTimeEnd = param.IRHandleTimeEnd,IRCreateTime = param.IRCreateTime,IRCreateTimeStart = param.IRCreateTimeStart,IRCreateTimeEnd = param.IRCreateTimeEnd,IRReportPicPathJson = param.IRReportPicPathJson,IRReportPicPathJsonLike = param.IRReportPicPathJsonLike,IRTakeMoney = param.IRTakeMoney,IRTakeMoneyStart = param.IRTakeMoneyStart,IRTakeMoneyEnd = param.IRTakeMoneyEnd,IRRobotId = param.IRRobotId,IRRobotIdLike = param.IRRobotIdLike,IRRemark = param.IRRemark,IRRemarkLike = param.IRRemarkLike,IRPushState = param.IRPushState,IRPushStateStart = param.IRPushStateStart,IRPushStateEnd = param.IRPushStateEnd,IRPushTime = param.IRPushTime,IRPushTimeStart = param.IRPushTimeStart,IRPushTimeEnd = param.IRPushTimeEnd,IRScanPayNotifyRet = param.IRScanPayNotifyRet,IRScanPayNotifyRetLike = param.IRScanPayNotifyRetLike,IRScanPayNotifyUrl = param.IRScanPayNotifyUrl,IRScanPayNotifyUrlLike = param.IRScanPayNotifyUrlLike,
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
                IRTaskID = NextId(),IROrderNo = param.IROrderNo,IRWeiXinNickName = param.IRWeiXinNickName,IRWeiXinHeaderImage = param.IRWeiXinHeaderImage,IRQrCodeImagePath = param.IRQrCodeImagePath,IRHandleState = param.IRHandleState,IRHandleMessage = param.IRHandleMessage,IRHandleTime = param.IRHandleTime,IRCreateTime = param.IRCreateTime,IRReportPicPathJson = param.IRReportPicPathJson,IRTakeMoney = param.IRTakeMoney,IRRobotId = param.IRRobotId,IRRemark = param.IRRemark,IRPushState = param.IRPushState,IRPushTime = param.IRPushTime,IRScanPayNotifyRet = param.IRScanPayNotifyRet,IRScanPayNotifyUrl = param.IRScanPayNotifyUrl,
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
		public int ChangeStatus(IRobotQrCodePayTask datas)
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