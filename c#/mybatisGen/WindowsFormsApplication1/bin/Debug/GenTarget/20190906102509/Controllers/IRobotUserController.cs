using System;
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
    public sealed partial class IRobotUserController : BaseController
    {
		public IRobotUserService Service { set; get; }

		/*抄考代码
----------------------------------------------------------------------------------------------------------------------------
		IRobotUser data = new IRobotUser
		{

					IUId = Next(),
		
					IUUsername = param.IUUsername,
		
		
					IUPassword = param.IUPassword,
		
		
					IUSignKey = param.IUSignKey,
		
		
					IUSignSecret = param.IUSignSecret,
		
		
					IUCreateDate = param.IUCreateDate,
		
		
		
		};
----------------------------------------------------------------------------------------------------------------------------
		IRobotUserParams param = new IRobotUserParams
		{

			IUId = param.IUId,
		
			IUUsername = param.IUUsername,
		
			IUUsernameLike = param.IUUsernameLike,
		
			IUPassword = param.IUPassword,
		
			IUPasswordLike = param.IUPasswordLike,
		
			IUSignKey = param.IUSignKey,
		
			IUSignKeyLike = param.IUSignKeyLike,
		
			IUSignSecret = param.IUSignSecret,
		
			IUSignSecretLike = param.IUSignSecretLike,
		
			IUCreateDate = param.IUCreateDate,
		
			IUCreateDateStart = param.IUCreateDateStart,
		
			IUCreateDateEnd = param.IUCreateDateEnd,
		
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
		public JsonResult Page(IRobotUserParams param,int currentPageIndex = 1,int pageSize = 20)
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
		public ExcelResult<IRobotUser> Export(IRobotUserParams param, string excelType, int currentPageIndex = 1, int pageSize = 10000)
		{
			return new ExcelResult<IRobotUser>
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
			List<IRobotUser> IRobotUserList;
            if (fileUpload.FileName.EndsWith("xlsx"))
            {
                IRobotUserList = ExcelHelper.ExcelXlsxToList<IRobotUser>(fileUpload.InputStream);
            }
            else if (fileUpload.FileName.EndsWith("xls"))
            {
                IRobotUserList = ExcelHelper.ExcelXlsToList<IRobotUser>(fileUpload.InputStream);
            }
			Service.AddBatch(IRobotUserList);
			return MyJson(new Result { code = 0, msg = "导入成功。"});
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 根据主键删除指定数据
        /// </summary>
        /// <param name="IUId">删除数据的主键</param>
		public JsonResult Del(long IUId)
		{
            Service.Del(IUId);
			return MyJson(new Result { code = 0, msg = "数据已删除。"});
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 新增一条数据
        /// </summary>
        /// <param name="data">新增的数据</param>
		public JsonResult Add(IRobotUser data)
		{
			Service.Add(data);
			return MyJson(new Result { code = 0, msg = "保存成功。"});
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 批量修改状态
        /// </summary>
        /// <param name="datas">修改状态的数据</param>
		public JsonResult ChangeStatus(IRobotUser datas)
		{
			return MyJson(new Result { code = 0,msg=$"修改成功，共{Service.ChangeStatus(datas)}条。"});
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 根据主键id查询***模块的数据实体
        /// </summary>
        /// <param name="IUId">主键id</param>
		/// <returns>返回***模块的查询结果</returns>
		public JsonResult Load(long IUId)
		{
			return MyJson(new Result { code = 0, data = Service.Load(IUId) });
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="datas">批量删除的数据</param>
		public JsonResult DelBatch(List<IRobotUser> datas)
		{
			return MyJson(new Result { code = 0 ,msg = $"删除成功，共{Service.DelBatch(datas)}条。" });
		}
		*/
    }
}