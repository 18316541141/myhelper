using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication1.Entity;
using WebApplication1.MyExtensions;
using WebApplication1.Params;
namespace WebApplication1.Controllers
{
	/// <summary>
	/// ***模块的控制器类
	/// </summary>
    public partial class IRobotRobotManagerController : BaseController
    {
		public IRobotRobotManagerService Service { set; get; }

		/*抄考代码
----------------------------------------------------------------------------------------------------------------------------
		IRobotRobotManager data = new IRobotRobotManager
		{

					IRMID = Next(),
		
		
		
		
					IRMRobotId = param.IRMRobotId,
		
		
					IRMRobotState = param.IRMRobotState,
		
		
		
		
					IRMRuningTime = param.IRMRuningTime,
		
		
		
					IRMMaxPayTradeCount = param.IRMMaxPayTradeCount,
		
		
		
		
					IRMCurrentPayCount = param.IRMCurrentPayCount,
		
		
		
		
					IRMBalance = param.IRMBalance,
		
		
		
		
					IRMIP = param.IRMIP,
		
		
					IRMCreateTime = param.IRMCreateTime,
		
		
		
					IRMTeamViewId = param.IRMTeamViewId,
		
		
					IRMTeamViewPassword = param.IRMTeamViewPassword,
		
		
					IRMSunflowerId = param.IRMSunflowerId,
		
		
					IRMSunflowerPassword = param.IRMSunflowerPassword,
		
		
					IRMBankCardTailNum = param.IRMBankCardTailNum,
		
		
					IRMBankCardName = param.IRMBankCardName,
		
		
					IRMMinBalance = param.IRMMinBalance,
		
		
		
		
					IRMPayPassword = param.IRMPayPassword,
		
		
					IRMScanPayNotifyUrl = param.IRMScanPayNotifyUrl,
		
		
					IRMWxUsername = param.IRMWxUsername,
		
		
					IRMWxPassword = param.IRMWxPassword,
		
		
					IRMRefreshBalance = param.IRMRefreshBalance,
		
		
		};
----------------------------------------------------------------------------------------------------------------------------
		IRobotRobotManagerParams param = new IRobotRobotManagerParams
		{

			IRMID = param.IRMID,
		
			IRMIDStart = param.IRMIDStart,
		
			IRMIDEnd = param.IRMIDEnd,
		
			IRMIDChange = param.IRMIDChange,
		
			IRMRobotId = param.IRMRobotId,
		
			IRMRobotIdLike = param.IRMRobotIdLike,
		
			IRMRobotState = param.IRMRobotState,
		
			IRMRobotStateStart = param.IRMRobotStateStart,
		
			IRMRobotStateEnd = param.IRMRobotStateEnd,
		
			IRMRobotStateChange = param.IRMRobotStateChange,
		
			IRMRuningTime = param.IRMRuningTime,
		
			IRMRuningTimeStart = param.IRMRuningTimeStart,
		
			IRMRuningTimeEnd = param.IRMRuningTimeEnd,
		
			IRMMaxPayTradeCount = param.IRMMaxPayTradeCount,
		
			IRMMaxPayTradeCountStart = param.IRMMaxPayTradeCountStart,
		
			IRMMaxPayTradeCountEnd = param.IRMMaxPayTradeCountEnd,
		
			IRMMaxPayTradeCountChange = param.IRMMaxPayTradeCountChange,
		
			IRMCurrentPayCount = param.IRMCurrentPayCount,
		
			IRMCurrentPayCountStart = param.IRMCurrentPayCountStart,
		
			IRMCurrentPayCountEnd = param.IRMCurrentPayCountEnd,
		
			IRMCurrentPayCountChange = param.IRMCurrentPayCountChange,
		
			IRMBalance = param.IRMBalance,
		
			IRMBalanceStart = param.IRMBalanceStart,
		
			IRMBalanceEnd = param.IRMBalanceEnd,
		
			IRMBalanceChange = param.IRMBalanceChange,
		
			IRMIP = param.IRMIP,
		
			IRMIPLike = param.IRMIPLike,
		
			IRMCreateTime = param.IRMCreateTime,
		
			IRMCreateTimeStart = param.IRMCreateTimeStart,
		
			IRMCreateTimeEnd = param.IRMCreateTimeEnd,
		
			IRMTeamViewId = param.IRMTeamViewId,
		
			IRMTeamViewIdLike = param.IRMTeamViewIdLike,
		
			IRMTeamViewPassword = param.IRMTeamViewPassword,
		
			IRMTeamViewPasswordLike = param.IRMTeamViewPasswordLike,
		
			IRMSunflowerId = param.IRMSunflowerId,
		
			IRMSunflowerIdLike = param.IRMSunflowerIdLike,
		
			IRMSunflowerPassword = param.IRMSunflowerPassword,
		
			IRMSunflowerPasswordLike = param.IRMSunflowerPasswordLike,
		
			IRMBankCardTailNum = param.IRMBankCardTailNum,
		
			IRMBankCardTailNumLike = param.IRMBankCardTailNumLike,
		
			IRMBankCardName = param.IRMBankCardName,
		
			IRMBankCardNameLike = param.IRMBankCardNameLike,
		
			IRMMinBalance = param.IRMMinBalance,
		
			IRMMinBalanceStart = param.IRMMinBalanceStart,
		
			IRMMinBalanceEnd = param.IRMMinBalanceEnd,
		
			IRMMinBalanceChange = param.IRMMinBalanceChange,
		
			IRMPayPassword = param.IRMPayPassword,
		
			IRMPayPasswordLike = param.IRMPayPasswordLike,
		
			IRMScanPayNotifyUrl = param.IRMScanPayNotifyUrl,
		
			IRMScanPayNotifyUrlLike = param.IRMScanPayNotifyUrlLike,
		
			IRMWxUsername = param.IRMWxUsername,
		
			IRMWxUsernameLike = param.IRMWxUsernameLike,
		
			IRMWxPassword = param.IRMWxPassword,
		
			IRMWxPasswordLike = param.IRMWxPasswordLike,
		
			IRMRefreshBalance = param.IRMRefreshBalance,
		
			IRMRefreshBalanceLike = param.IRMRefreshBalanceLike,
		
		};
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 分页查询***模块，并返回查询结果
        /// </summary>
        /// <param name="param">查询参数</param>
        /// <param name="currentPageIndex">当前页码</param>
        /// <param name="pageSize">每页显示的数据量</param>
        /// <returns>返回***模块的查询结果</returns>
		public JsonResult Page(IRobotRobotManagerParams param,int currentPageIndex = 1,int pageSize = 20)
		{
			return MyJson(new Result{code = 0, data = Service.Page(param, currentPageIndex, pageSize)});
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 导出***模块，使用excel表保存
        /// </summary>
        /// <param name="param">查询参数</param>
		/// <param name="excelType">excel类型</param>
        /// <param name="currentPageIndex">当前页码</param>
        /// <param name="pageSize">每页显示的数据量</param>
        /// <returns>返回***模块的导出结果</returns>
		[OperInterval(IntervalMillisecond=10000)]
		public ExcelResult<IRobotRobotManager> Export(IRobotRobotManagerParams param, string excelType, int currentPageIndex = 1, int pageSize = 10000)
		{
			return new ExcelResult<IRobotRobotManager>
			{
				DataList = Service.Page(param, currentPageIndex, pageSize).pageDataList,
				FileName = "测试excel."+excelType
			};
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 导入***模块数据
        /// </summary>
        /// <param name="param">查询参数</param>
        /// <param name="currentPageIndex">当前页码</param>
        /// <param name="pageSize">每页显示的数据量</param>
        /// <returns>返回***模块的导出结果</returns>
		public JsonResult Import(HttpPostedFileBase fileUpload, 其他参数...)
		{
			List<IRobotRobotManager> IRobotRobotManagerList;
            if (fileUpload.FileName.EndsWith("xlsx"))
            {
                IRobotRobotManagerList = ExcelHelper.ExcelXlsxToList<IRobotRobotManager>(fileUpload.InputStream);
            }
            else if (fileUpload.FileName.EndsWith("xls"))
            {
                IRobotRobotManagerList = ExcelHelper.ExcelXlsToList<IRobotRobotManager>(fileUpload.InputStream);
            }
			Service.AddBatch(IRobotRobotManagerList);
			return MyJson(new Result { code = 0, msg = "导入成功。"});
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 根据主键删除指定数据
        /// </summary>
        /// <param name="IRMID">删除数据的主键</param>
		public JsonResult Del(long IRMID)
		{
            Service.Del(IRMID);
			return MyJson(new Result { code = 0, msg = "数据已删除。"});
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 新增一条数据
        /// </summary>
        /// <param name="data">新增的数据</param>
		public JsonResult Add(IRobotRobotManager data)
		{
			Service.Add(data);
			return MyJson(new Result { code = 0, msg = "保存成功。"});
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 批量修改状态
        /// </summary>
        /// <param name="datas">修改状态的数据</param>
		public JsonResult ChangeStatus(IRobotRobotManager datas)
		{
			return MyJson(new Result { code = 0,msg=$"修改成功，共{Service.ChangeStatus(datas)}条。"});
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 根据主键id查询***模块的数据实体
        /// </summary>
        /// <param name="IRMID">主键id</param>
		/// <returns>返回***模块的查询结果</returns>
		public JsonResult Load(long IRMID)
		{
			return MyJson(new Result { code = 0, data = Service.Load(IRMID) });
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="datas">批量删除的数据</param>
		public JsonResult DelBatch(List<IRobotRobotManager> datas)
		{
			return MyJson(new Result { code = 0 ,msg = $"删除成功，共{Service.DelBatch(datas)}条。" });
		}
		*/
    }
}