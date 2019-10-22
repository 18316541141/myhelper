using CommonHelper.Helper.CommonEntity;
using CommonWeb.Controllers.Common;
using CommonWeb.Filter.Common;
using System.Collections.Generic;
using System.Web.Mvc;
using WebApplication1.Entity;
using WebApplication1.Service;
namespace WebApplication1.Controllers
{
    /// <summary>
    /// “全局变量”模块的控制器类
    /// </summary>
    public partial class GlobalVariableController : BaseController
    {
        public GlobalVariableService Service { set; get; }

        /// <summary>
        /// 获取所有的全局变量
        /// </summary>
        /// <returns></returns>
        public JsonResult ShowAllGlobalVariable()
        {
            return MyJson(new Result
            {
                code = 0,
                data = Service.ShowAllGlobalVariable()
            });
        }

        /// <summary>
        /// 保存所有的全局变量
        /// </summary>
        /// <param name="globalVariableList">全局变量数据</param>
        /// <returns></returns>
        [OperInterval(IntervalMillisecond = 60000)]
        public JsonResult SaveAllGlobalVariable(List<GlobalVariable> globalVariableList)
        {
            Service.SaveAllGlobalVariable(globalVariableList);
            return MyJson(new Result
            {
                code = 0,
                msg = "保存成功"
            });
        }

        /*抄考代码
----------------------------------------------------------------------------------------------------------------------------
		GlobalVariable data = new GlobalVariable
		{

					Id = Next(),
		
					VarName = param.VarName,
		
		
					VarValue = param.VarValue,
		
		
		};
----------------------------------------------------------------------------------------------------------------------------
		GlobalVariableParams param = new GlobalVariableParams
		{

			Id = param.Id,
		
			VarName = param.VarName,
		
			VarNameLike = param.VarNameLike,
		
			VarValue = param.VarValue,
		
			VarValueLike = param.VarValueLike,
		
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
		public JsonResult Page(GlobalVariableParams param,int currentPageIndex = 1,int pageSize = 20)
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
		public ExcelResult<GlobalVariable> Export(GlobalVariableParams param, string excelType, int currentPageIndex = 1, int pageSize = 10000)
		{
			return new ExcelResult<GlobalVariable>
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
			List<GlobalVariable> GlobalVariableList;
            if (fileUpload.FileName.EndsWith("xlsx"))
            {
                GlobalVariableList = ExcelHelper.ExcelXlsxToList<GlobalVariable>(fileUpload.InputStream);
            }
            else if (fileUpload.FileName.EndsWith("xls"))
            {
                GlobalVariableList = ExcelHelper.ExcelXlsToList<GlobalVariable>(fileUpload.InputStream);
            }
			Service.AddBatch(GlobalVariableList);
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
		public JsonResult Add(GlobalVariable data)
		{
			Service.Add(data);
			return MyJson(new Result { code = 0, msg = "保存成功。"});
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 批量修改状态
        /// </summary>
        /// <param name="datas">修改状态的数据</param>
		public JsonResult ChangeStatus(GlobalVariable datas)
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
		public JsonResult DelBatch(List<GlobalVariable> datas)
		{
			return MyJson(new Result { code = 0 ,msg = $"删除成功，共{Service.DelBatch(datas)}条。" });
		}
		*/
    }
}