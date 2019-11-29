using CommonHelper.CommonEntity;
using CommonHelper.Helper.CommonEntity;
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
    /// “日志管理”模块的控制器类
    /// </summary>
    public sealed partial class LogEntityController : BaseController
    {
        public LogEntityService Service { set; get; }

        /// <summary>
        /// 分页查询“日志管理”模块，并返回查询结果
        /// </summary>
        /// <param name="param">查询参数</param>
        /// <param name="currentPageIndex">当前页码</param>
        /// <param name="pageSize">每页显示的数据量</param>
        /// <returns>返回“日志管理”模块的查询结果</returns>
		[Compress]
        public JsonResult Page(LogEntityParams param, int currentPageIndex = 1, int pageSize = 20)
        {
            return MyJson(new Result { code = 0, data = Service.Page(currentPageIndex, pageSize, param) });
        }

        /// <summary>
        /// 根据主键id查询“日志管理”模块的数据实体
        /// </summary>
        /// <param name="id">主键id</param>
		/// <returns>返回“日志管理”模块的查询结果</returns>
		public JsonResult Load(long id)
        {
            return MyJson(new Result { code = 0, data = Service.Load(id) });
        }

        /*抄考代码
----------------------------------------------------------------------------------------------------------------------------
		LogEntity data = new LogEntity
		{

					ID = Next(),
		
					CreateDate = param.CreateDate,
		
		
		
					Level = param.Level,
		
		
					ThreadNo = param.ThreadNo,
		
		
					Message = param.Message,
		
		
					Namespace = param.Namespace,
		
		
					TypeName = param.TypeName,
		
		
					MethodName = param.MethodName,
		
		
					USERNAME = param.USERNAME,
		
		
		};
----------------------------------------------------------------------------------------------------------------------------
		LogEntityParams param = new LogEntityParams
		{

			ID = param.ID,
		
			CreateDate = param.CreateDate,
		
			CreateDateStart = param.CreateDateStart,
		
			CreateDateEnd = param.CreateDateEnd,
		
			Level = param.Level,
		
			LevelLike = param.LevelLike,
		
			ThreadNo = param.ThreadNo,
		
			ThreadNoLike = param.ThreadNoLike,
		
			Message = param.Message,
		
			MessageLike = param.MessageLike,
		
			Namespace = param.Namespace,
		
			NamespaceLike = param.NamespaceLike,
		
			TypeName = param.TypeName,
		
			TypeNameLike = param.TypeNameLike,
		
			MethodName = param.MethodName,
		
			MethodNameLike = param.MethodNameLike,
		
			USERNAME = param.USERNAME,
		
			USERNAMELike = param.USERNAMELike,
		
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
		public JsonResult Page(LogEntityParams param,int currentPageIndex = 1,int pageSize = 20)
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
		public ExcelResult<LogEntity> Export(LogEntityParams param, string excelType, int currentPageIndex = 1, int pageSize = 10000)
		{
			return new ExcelResult<LogEntity>
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
			List<LogEntity> LogEntityList;
            if (fileUpload.FileName.EndsWith("xlsx"))
            {
                LogEntityList = ExcelHelper.ExcelXlsxToList<LogEntity>(fileUpload.InputStream);
            }
            else if (fileUpload.FileName.EndsWith("xls"))
            {
                LogEntityList = ExcelHelper.ExcelXlsToList<LogEntity>(fileUpload.InputStream);
            }
			Service.AddBatch(LogEntityList);
			return MyJson(new Result { code = 0, msg = "导入成功。"});
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 根据主键删除指定数据
        /// </summary>
        /// <param name="ID">删除数据的主键</param>
		public JsonResult Del(long ID)
		{
            Service.Del(ID);
			return MyJson(new Result { code = 0, msg = "数据已删除。"});
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 新增一条数据
        /// </summary>
        /// <param name="data">新增的数据</param>
		public JsonResult Add(LogEntity data)
		{
			Service.Add(data);
			return MyJson(new Result { code = 0, msg = "保存成功。"});
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 批量修改状态
        /// </summary>
        /// <param name="datas">修改状态的数据</param>
		public JsonResult ChangeStatus(LogEntity datas)
		{
			return MyJson(new Result { code = 0,msg=$"修改成功，共{Service.ChangeStatus(datas)}条。"});
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 根据主键id查询***模块的数据实体
        /// </summary>
        /// <param name="ID">主键id</param>
		/// <returns>返回***模块的查询结果</returns>
		public JsonResult Load(long ID)
		{
			return MyJson(new Result { code = 0, data = Service.Load(ID) });
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="datas">批量删除的数据</param>
		public JsonResult DelBatch(List<LogEntity> datas)
		{
			return MyJson(new Result { code = 0 ,msg = $"删除成功，共{Service.DelBatch(datas)}条。" });
		}
		*/
    }
}