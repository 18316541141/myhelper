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
    public partial class IRobotQrCodePayTaskController : BaseController
    {
		public IRobotQrCodePayTaskService Service { set; get; }

		/*抄考代码
----------------------------------------------------------------------------------------------------------------------------
		IRobotQrCodePayTask data = new IRobotQrCodePayTask
		{

					IRTaskID = Next(),
		
		
		
					IROrderNo = param.IROrderNo,
		
		
					IRWeiXinNickName = param.IRWeiXinNickName,
		
		
					IRWeiXinHeaderImage = param.IRWeiXinHeaderImage,
		
		
					IRQrCodeImagePath = param.IRQrCodeImagePath,
		
		
					IRHandleState = param.IRHandleState,
		
		
		
					IRHandleMessage = param.IRHandleMessage,
		
		
					IRHandleTime = param.IRHandleTime,
		
		
		
					IRCreateTime = param.IRCreateTime,
		
		
		
					IRReportPicPathJson = param.IRReportPicPathJson,
		
		
					IRTakeMoney = param.IRTakeMoney,
		
		
		
					IRRobotId = param.IRRobotId,
		
		
					IRRemark = param.IRRemark,
		
		
					IRPushState = param.IRPushState,
		
		
		
					IRPushTime = param.IRPushTime,
		
		
		
					IRScanPayNotifyRet = param.IRScanPayNotifyRet,
		
		
					IRScanPayNotifyUrl = param.IRScanPayNotifyUrl,
		
		
		};
----------------------------------------------------------------------------------------------------------------------------
		IRobotQrCodePayTaskParams param = new IRobotQrCodePayTaskParams
		{

			IRTaskID = param.IRTaskID,
		
			IRTaskIDStart = param.IRTaskIDStart,
		
			IRTaskIDEnd = param.IRTaskIDEnd,
		
			IROrderNo = param.IROrderNo,
		
			IROrderNoLike = param.IROrderNoLike,
		
			IRWeiXinNickName = param.IRWeiXinNickName,
		
			IRWeiXinNickNameLike = param.IRWeiXinNickNameLike,
		
			IRWeiXinHeaderImage = param.IRWeiXinHeaderImage,
		
			IRWeiXinHeaderImageLike = param.IRWeiXinHeaderImageLike,
		
			IRQrCodeImagePath = param.IRQrCodeImagePath,
		
			IRQrCodeImagePathLike = param.IRQrCodeImagePathLike,
		
			IRHandleState = param.IRHandleState,
		
			IRHandleStateStart = param.IRHandleStateStart,
		
			IRHandleStateEnd = param.IRHandleStateEnd,
		
			IRHandleMessage = param.IRHandleMessage,
		
			IRHandleMessageLike = param.IRHandleMessageLike,
		
			IRHandleTime = param.IRHandleTime,
		
			IRHandleTimeStart = param.IRHandleTimeStart,
		
			IRHandleTimeEnd = param.IRHandleTimeEnd,
		
			IRCreateTime = param.IRCreateTime,
		
			IRCreateTimeStart = param.IRCreateTimeStart,
		
			IRCreateTimeEnd = param.IRCreateTimeEnd,
		
			IRReportPicPathJson = param.IRReportPicPathJson,
		
			IRReportPicPathJsonLike = param.IRReportPicPathJsonLike,
		
			IRTakeMoney = param.IRTakeMoney,
		
			IRTakeMoneyStart = param.IRTakeMoneyStart,
		
			IRTakeMoneyEnd = param.IRTakeMoneyEnd,
		
			IRRobotId = param.IRRobotId,
		
			IRRobotIdLike = param.IRRobotIdLike,
		
			IRRemark = param.IRRemark,
		
			IRRemarkLike = param.IRRemarkLike,
		
			IRPushState = param.IRPushState,
		
			IRPushStateStart = param.IRPushStateStart,
		
			IRPushStateEnd = param.IRPushStateEnd,
		
			IRPushTime = param.IRPushTime,
		
			IRPushTimeStart = param.IRPushTimeStart,
		
			IRPushTimeEnd = param.IRPushTimeEnd,
		
			IRScanPayNotifyRet = param.IRScanPayNotifyRet,
		
			IRScanPayNotifyRetLike = param.IRScanPayNotifyRetLike,
		
			IRScanPayNotifyUrl = param.IRScanPayNotifyUrl,
		
			IRScanPayNotifyUrlLike = param.IRScanPayNotifyUrlLike,
		
		};
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 分页查询***模块，并返回查询结果
        /// </summary>
        /// <param name="param">查询参数</param>
        /// <param name="currentPageIndex">当前页码</param>
        /// <param name="pageSize">每页显示的数据量</param>
        /// <returns>返回***模块的查询结果</returns>
		public JsonResult Page(IRobotQrCodePayTaskParams param,int currentPageIndex = 1,int pageSize = 20)
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
		public ExcelResult<IRobotQrCodePayTask> Export(IRobotQrCodePayTaskParams param, string excelType, int currentPageIndex = 1, int pageSize = 10000)
		{
			return new ExcelResult<IRobotQrCodePayTask>
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
			List<IRobotQrCodePayTask> IRobotQrCodePayTaskList;
            if (fileUpload.FileName.EndsWith("xlsx"))
            {
                IRobotQrCodePayTaskList = ExcelHelper.ExcelXlsxToList<IRobotQrCodePayTask>(fileUpload.InputStream);
            }
            else if (fileUpload.FileName.EndsWith("xls"))
            {
                IRobotQrCodePayTaskList = ExcelHelper.ExcelXlsToList<IRobotQrCodePayTask>(fileUpload.InputStream);
            }
			Service.AddBatch(IRobotQrCodePayTaskList);
			return MyJson(new Result { code = 0, msg = "导入成功。"});
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 根据主键删除指定数据
        /// </summary>
        /// <param name="IRTaskID">删除数据的主键</param>
		public JsonResult Del(long IRTaskID)
		{
            Service.Del(IRTaskID);
			return MyJson(new Result { code = 0, msg = "数据已删除。"});
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 新增一条数据
        /// </summary>
        /// <param name="data">新增的数据</param>
		public JsonResult Add(IRobotQrCodePayTask data)
		{
			Service.Add(data);
			return MyJson(new Result { code = 0, msg = "保存成功。"});
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 批量修改状态
        /// </summary>
        /// <param name="datas">修改状态的数据</param>
		public JsonResult ChangeStatus(IRobotQrCodePayTask datas)
		{
			return MyJson(new Result { code = 0,msg=$"修改成功，共{Service.ChangeStatus(datas)}条。"});
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 根据主键id查询***模块的数据实体
        /// </summary>
        /// <param name="IRTaskID">主键id</param>
		/// <returns>返回***模块的查询结果</returns>
		public JsonResult Load(long IRTaskID)
		{
			return MyJson(new Result { code = 0, data = Service.Load(IRTaskID) });
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="datas">批量删除的数据</param>
		public JsonResult DelBatch(List<IRobotQrCodePayTask> datas)
		{
			return MyJson(new Result { code = 0 ,msg = $"删除成功，共{Service.DelBatch(datas)}条。" });
		}
		*/
    }
}