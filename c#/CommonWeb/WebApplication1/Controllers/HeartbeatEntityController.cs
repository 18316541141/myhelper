using CommonHelper.CommonEntity;
using CommonHelper.Entity;
using CommonHelper.Helper.CommonEntity;
using CommonHelper.Params;
using CommonWeb.Controllers.Common;
using CommonWeb.Filter.Common;
using CommonWeb.Service;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CommonWeb.Controllers
{
	/// <summary>
	/// “心跳监测”模块的控制器类
	/// </summary>
    public sealed partial class HeartbeatEntityController : BaseController
    {
		public HeartbeatEntityService Service { set; get; }


        /// <summary>
        /// 分页查询“心跳监测”模块，并返回查询结果
        /// </summary>
        /// <param name="param">查询参数</param>
        /// <param name="currentPageIndex">当前页码</param>
        /// <param name="pageSize">每页显示的数据量</param>
        /// <returns>返回“心跳监测”模块的查询结果</returns>
		[Compress]
        public JsonResult Page(int currentPageIndex = 1, int pageSize = 20, HeartbeatEntityParams param = null)
        {
            return MyJson(new Result { code = 0, data = Service.Page(currentPageIndex, pageSize, param) });
        }

        /// <summary>
        /// 由机器人向服务器发送心跳信息
        /// </summary>
        /// <param name="robotId"></param>
        /// <returns></returns>
        [OperInterval(IntervalMillisecond= 60000)]
        [Sign(new string[] { "createDate", "r" })]
        [AllowAnonymous]
        public JsonResult Send(HeartbeatEntity heartbeatEntity)
        {
            Service.RecordHeartbeat(heartbeatEntity);
            return MyJson(new Result { code = 0, msg = "心跳发送成功！" });
        }

        /// <summary>
        /// 根据主键删除指定数据
        /// </summary>
        /// <param name="id">删除数据的主键</param>
		public JsonResult Del(long? id)
        {
            Service.Del(id);
            return MyJson(new Result { code = 0, msg = "数据已删除。" });
        }

        /*抄考代码
----------------------------------------------------------------------------------------------------------------------------
		HeartbeatEntity data = new HeartbeatEntity
		{

					Id = Next(),
		
					LastHeartbeatTime = param.LastHeartbeatTime,
		
		
		
					RobotId = param.RobotId,
		
		
		};
----------------------------------------------------------------------------------------------------------------------------
		HeartbeatEntityParams param = new HeartbeatEntityParams
		{

			Id = param.Id,
		
			LastHeartbeatTime = param.LastHeartbeatTime,
		
			LastHeartbeatTimeStart = param.LastHeartbeatTimeStart,
		
			LastHeartbeatTimeEnd = param.LastHeartbeatTimeEnd,
		
			RobotId = param.RobotId,
		
			RobotIdLike = param.RobotIdLike,
		
		};
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 分页查询***模块，并返回查询结果
        /// </summary>
        /// <param name="param">查询参数</param>
        /// <param name="currentPageIndex">当前页码</param>
        /// <param name="pageSize">每页显示的数据量</param>
        /// <returns>返回***模块的查询结果</returns>
		[Compress]
		public JsonResult Page(HeartbeatEntityParams param,int currentPageIndex = 1,int pageSize = 20)
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
		[Compress]
		public ExcelResult<HeartbeatEntity> Export(HeartbeatEntityParams param, string excelType, int currentPageIndex = 1, int pageSize = 10000)
		{
			return new ExcelResult<HeartbeatEntity>
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
			List<HeartbeatEntity> HeartbeatEntityList;
            if (fileUpload.FileName.EndsWith("xlsx"))
            {
                HeartbeatEntityList = ExcelHelper.ExcelXlsxToList<HeartbeatEntity>(fileUpload.InputStream);
            }
            else if (fileUpload.FileName.EndsWith("xls"))
            {
                HeartbeatEntityList = ExcelHelper.ExcelXlsToList<HeartbeatEntity>(fileUpload.InputStream);
            }
			Service.AddBatch(HeartbeatEntityList);
			return MyJson(new Result { code = 0, msg = "导入成功。"});
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 根据主键删除指定数据
        /// </summary>
        /// <param name="Id">删除数据的主键</param>
		public JsonResult Del(long Id)
		{
            Service.Del(Id);
			return MyJson(new Result { code = 0, msg = "数据已删除。"});
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 新增一条数据
        /// </summary>
        /// <param name="data">新增的数据</param>
		public JsonResult Add(HeartbeatEntity data)
		{
			Service.Add(data);
			return MyJson(new Result { code = 0, msg = "保存成功。"});
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 批量修改状态
        /// </summary>
        /// <param name="datas">修改状态的数据</param>
		public JsonResult ChangeStatus(HeartbeatEntity datas)
		{
			return MyJson(new Result { code = 0,msg=$"修改成功，共{Service.ChangeStatus(datas)}条。"});
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 根据主键id查询***模块的数据实体
        /// </summary>
        /// <param name="Id">主键id</param>
		/// <returns>返回***模块的查询结果</returns>
		public JsonResult Load(long Id)
		{
			return MyJson(new Result { code = 0, data = Service.Load(Id) });
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="datas">批量删除的数据</param>
		public JsonResult DelBatch(List<HeartbeatEntity> datas)
		{
			return MyJson(new Result { code = 0 ,msg = $"删除成功，共{Service.DelBatch(datas)}条。" });
		}
		*/
    }
}