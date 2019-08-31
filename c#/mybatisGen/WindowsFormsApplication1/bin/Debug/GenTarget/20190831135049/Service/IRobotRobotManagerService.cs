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
    public partial class IRobotRobotManagerService : BaseService
    {
		public IRobotRobotManagerRepository Repository { set; get; }

		/*抄考代码
----------------------------------------------------------------------------------------------------------------------------
		IRobotRobotManager data = new IRobotRobotManager
		{
	IRMID = NextId(),IRMRobotId = param.IRMRobotId,IRMRobotState = param.IRMRobotState,IRMRuningTime = param.IRMRuningTime,IRMMaxPayTradeCount = param.IRMMaxPayTradeCount,IRMCurrentPayCount = param.IRMCurrentPayCount,IRMBalance = param.IRMBalance,IRMIP = param.IRMIP,IRMCreateTime = param.IRMCreateTime,IRMTeamViewId = param.IRMTeamViewId,IRMTeamViewPassword = param.IRMTeamViewPassword,IRMSunflowerId = param.IRMSunflowerId,IRMSunflowerPassword = param.IRMSunflowerPassword,IRMBankCardTailNum = param.IRMBankCardTailNum,IRMBankCardName = param.IRMBankCardName,IRMMinBalance = param.IRMMinBalance,IRMPayPassword = param.IRMPayPassword,IRMScanPayNotifyUrl = param.IRMScanPayNotifyUrl,IRMWxUsername = param.IRMWxUsername,IRMWxPassword = param.IRMWxPassword,IRMRefreshBalance = param.IRMRefreshBalance,
		};
----------------------------------------------------------------------------------------------------------------------------
		IRobotRobotManagerParams param = new IRobotRobotManagerParams
		{
	IRMID = param.IRMID,IRMIDStart = param.IRMIDStart,IRMIDEnd = param.IRMIDEnd,IRMIDChange = param.IRMIDChange,IRMRobotId = param.IRMRobotId,IRMRobotIdLike = param.IRMRobotIdLike,IRMRobotState = param.IRMRobotState,IRMRobotStateStart = param.IRMRobotStateStart,IRMRobotStateEnd = param.IRMRobotStateEnd,IRMRobotStateChange = param.IRMRobotStateChange,IRMRuningTime = param.IRMRuningTime,IRMRuningTimeStart = param.IRMRuningTimeStart,IRMRuningTimeEnd = param.IRMRuningTimeEnd,IRMMaxPayTradeCount = param.IRMMaxPayTradeCount,IRMMaxPayTradeCountStart = param.IRMMaxPayTradeCountStart,IRMMaxPayTradeCountEnd = param.IRMMaxPayTradeCountEnd,IRMMaxPayTradeCountChange = param.IRMMaxPayTradeCountChange,IRMCurrentPayCount = param.IRMCurrentPayCount,IRMCurrentPayCountStart = param.IRMCurrentPayCountStart,IRMCurrentPayCountEnd = param.IRMCurrentPayCountEnd,IRMCurrentPayCountChange = param.IRMCurrentPayCountChange,IRMBalance = param.IRMBalance,IRMBalanceStart = param.IRMBalanceStart,IRMBalanceEnd = param.IRMBalanceEnd,IRMBalanceChange = param.IRMBalanceChange,IRMIP = param.IRMIP,IRMIPLike = param.IRMIPLike,IRMCreateTime = param.IRMCreateTime,IRMCreateTimeStart = param.IRMCreateTimeStart,IRMCreateTimeEnd = param.IRMCreateTimeEnd,IRMTeamViewId = param.IRMTeamViewId,IRMTeamViewIdLike = param.IRMTeamViewIdLike,IRMTeamViewPassword = param.IRMTeamViewPassword,IRMTeamViewPasswordLike = param.IRMTeamViewPasswordLike,IRMSunflowerId = param.IRMSunflowerId,IRMSunflowerIdLike = param.IRMSunflowerIdLike,IRMSunflowerPassword = param.IRMSunflowerPassword,IRMSunflowerPasswordLike = param.IRMSunflowerPasswordLike,IRMBankCardTailNum = param.IRMBankCardTailNum,IRMBankCardTailNumLike = param.IRMBankCardTailNumLike,IRMBankCardName = param.IRMBankCardName,IRMBankCardNameLike = param.IRMBankCardNameLike,IRMMinBalance = param.IRMMinBalance,IRMMinBalanceStart = param.IRMMinBalanceStart,IRMMinBalanceEnd = param.IRMMinBalanceEnd,IRMMinBalanceChange = param.IRMMinBalanceChange,IRMPayPassword = param.IRMPayPassword,IRMPayPasswordLike = param.IRMPayPasswordLike,IRMScanPayNotifyUrl = param.IRMScanPayNotifyUrl,IRMScanPayNotifyUrlLike = param.IRMScanPayNotifyUrlLike,IRMWxUsername = param.IRMWxUsername,IRMWxUsernameLike = param.IRMWxUsernameLike,IRMWxPassword = param.IRMWxPassword,IRMWxPasswordLike = param.IRMWxPasswordLike,IRMRefreshBalance = param.IRMRefreshBalance,IRMRefreshBalanceLike = param.IRMRefreshBalanceLike,
		};
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 分页查询***模块，并返回查询结果
        /// </summary>
        /// <param name="param">查询参数</param>
        /// <param name="currentPageIndex">当前页码</param>
        /// <param name="pageSize">每页显示的数据量</param>
        /// <returns>返回***模块的查询结果</returns>
		public MyPagedList<IRobotRobotManager> Page(IRobotRobotManagerParams param,int currentPageIndex = 1,int pageSize = 20)
		{
			return Repository.PageList(param, currentPageIndex, pageSize);
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 根据主键删除指定数据
        /// </summary>
        /// <param name="IRMID">删除数据的主键</param>
		public void Del(long IRMID)
		{
			Repository.Delete(a => a.IRMID);
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 新增一条数据
        /// </summary>
        /// <param name="data">新增的数据</param>
		public void Add(IRobotRobotManager data)
		{
			Repository.Insert(data);
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 修改***模块的数据
        /// </summary>
        /// <param name="data">修改数据，不为null才会被修改，主键必须不为null</param>
        public void Update(IRobotRobotManager data)
        {
            Repository.UpdateChange(new IRobotRobotManager
            {
                IRMID = NextId(),IRMRobotId = data.IRMRobotId,IRMRobotState = data.IRMRobotState,IRMRuningTime = data.IRMRuningTime,IRMMaxPayTradeCount = data.IRMMaxPayTradeCount,IRMCurrentPayCount = data.IRMCurrentPayCount,IRMBalance = data.IRMBalance,IRMIP = data.IRMIP,IRMCreateTime = data.IRMCreateTime,IRMTeamViewId = data.IRMTeamViewId,IRMTeamViewPassword = data.IRMTeamViewPassword,IRMSunflowerId = data.IRMSunflowerId,IRMSunflowerPassword = data.IRMSunflowerPassword,IRMBankCardTailNum = data.IRMBankCardTailNum,IRMBankCardName = data.IRMBankCardName,IRMMinBalance = data.IRMMinBalance,IRMPayPassword = data.IRMPayPassword,IRMScanPayNotifyUrl = data.IRMScanPayNotifyUrl,IRMWxUsername = data.IRMWxUsername,IRMWxPassword = data.IRMWxPassword,IRMRefreshBalance = data.IRMRefreshBalance,
            });
        }
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 新增多条数据
        /// </summary>
        /// <param name="datas">新增的数据</param>
		public void AddBatch(List<IRobotRobotManager> datas)
		{
			foreach(IRobotRobotManager data in datas)
			{
				data.IRMID = NextId();
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
		public int ChangeStatus(List<IRobotRobotManager> datas)
		{
			List<IRobotRobotManager> updates = new List<IRobotRobotManager>();
			for(IRobotRobotManager data in datas)
			{
				updates.Add(new IRobotRobotManager
				{
					IRMID = data.IRMID,
					Status = data.Status
				});
			}
			return Repository.UpdateChangeBatch(updates);
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 根据主键id查询***模块的数据实体
        /// </summary>
        /// <param name="IRMID">主键id</param>
		/// <returns>返回***模块的查询结果</returns>
		public IRobotRobotManager Load(long IRMID)
		{
			return Repository.FindEntity(IRMID);
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="datas">批量删除的数据</param>
		public int DelBatch(IRobotRobotManager datas)
		{
			return Repository.Delete(datas);
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 对“****”方法的入参进行校验基础（只校验是否为空，格式，长度，大小，不涉及
	/// 数据库查询、流读取等其他复杂操作），校验不通过时，异常往外抛出
        /// </summary>
        /// <param name="param">待校验参数</param>
        public void Check****(IRobotRobotManagerParams param)
        {
            if(...){
		Ex(...,...);
	    }
        }
		*/
    }
}