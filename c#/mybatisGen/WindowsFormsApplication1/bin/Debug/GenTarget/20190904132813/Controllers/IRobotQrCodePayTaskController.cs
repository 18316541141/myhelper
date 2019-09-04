﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication1.Entity;
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
		
		
					IRTakeMoney = param.IRTakeMoney,
		
		
		
		
					IRPushState = param.IRPushState,
		
		
		
		
					IRScanPayNotifyUrl = param.IRScanPayNotifyUrl,
		
		
					IRScanPayNotifyRet = param.IRScanPayNotifyRet,
		
		
					IRHandleState = param.IRHandleState,
		
		
		
		
					IRQrCodeImagePath = param.IRQrCodeImagePath,
		
		
					IRBsyNotifyState = param.IRBsyNotifyState,
		
		
		
		
					IRSendMoneyNotifyState = param.IRSendMoneyNotifyState,
		
		
		
		
					IRReportPicPath = param.IRReportPicPath,
		
		
					IRRemark = param.IRRemark,
		
		
		};
----------------------------------------------------------------------------------------------------------------------------
		IRobotQrCodePayTaskParams param = new IRobotQrCodePayTaskParams
		{

			IRTaskID = param.IRTaskID,
		
			IRTaskIDStart = param.IRTaskIDStart,
		
			IRTaskIDEnd = param.IRTaskIDEnd,
		
			IRTaskIDChange = param.IRTaskIDChange,
		
			IROrderNo = param.IROrderNo,
		
			IROrderNoLike = param.IROrderNoLike,
		
			IRTakeMoney = param.IRTakeMoney,
		
			IRTakeMoneyStart = param.IRTakeMoneyStart,
		
			IRTakeMoneyEnd = param.IRTakeMoneyEnd,
		
			IRTakeMoneyChange = param.IRTakeMoneyChange,
		
			IRPushState = param.IRPushState,
		
			IRPushStateStart = param.IRPushStateStart,
		
			IRPushStateEnd = param.IRPushStateEnd,
		
			IRPushStateChange = param.IRPushStateChange,
		
			IRScanPayNotifyUrl = param.IRScanPayNotifyUrl,
		
			IRScanPayNotifyUrlLike = param.IRScanPayNotifyUrlLike,
		
			IRScanPayNotifyRet = param.IRScanPayNotifyRet,
		
			IRScanPayNotifyRetLike = param.IRScanPayNotifyRetLike,
		
			IRHandleState = param.IRHandleState,
		
			IRHandleStateStart = param.IRHandleStateStart,
		
			IRHandleStateEnd = param.IRHandleStateEnd,
		
			IRHandleStateChange = param.IRHandleStateChange,
		
			IRQrCodeImagePath = param.IRQrCodeImagePath,
		
			IRQrCodeImagePathLike = param.IRQrCodeImagePathLike,
		
			IRBsyNotifyState = param.IRBsyNotifyState,
		
			IRBsyNotifyStateStart = param.IRBsyNotifyStateStart,
		
			IRBsyNotifyStateEnd = param.IRBsyNotifyStateEnd,
		
			IRBsyNotifyStateChange = param.IRBsyNotifyStateChange,
		
			IRSendMoneyNotifyState = param.IRSendMoneyNotifyState,
		
			IRSendMoneyNotifyStateStart = param.IRSendMoneyNotifyStateStart,
		
			IRSendMoneyNotifyStateEnd = param.IRSendMoneyNotifyStateEnd,
		
			IRSendMoneyNotifyStateChange = param.IRSendMoneyNotifyStateChange,
		
			IRReportPicPath = param.IRReportPicPath,
		
			IRReportPicPathLike = param.IRReportPicPathLike,
		
			IRRemark = param.IRRemark,
		
			IRRemarkLike = param.IRRemarkLike,
		
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