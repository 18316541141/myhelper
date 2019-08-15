using CommonHelper.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Entity.Common
{
    /// <summary>
    /// 下载excel的返回结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ExcelResult<T> : ActionResult
    {
        /// <summary>
        /// 导出的数据
        /// </summary>
        public List<T> DataList { set; get; }

        /// <summary>
        /// 导出excel的名称
        /// </summary>
        public string FileName { set; get; }

        /// <summary>
        /// 分组名
        /// </summary>
        public string GroupName { set; get; }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            HttpResponseBase response = context.HttpContext.Response;
            response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            response.ContentType = "application/vnd.ms-excel";
            if (FileName.EndsWith("xls"))
            {
                ExcelHelper.ListToExcelXls(DataList, response.OutputStream,GroupName);
            }
            else if (FileName.EndsWith("xlsx"))
            {
                ExcelHelper.ListToExcelXlsx(DataList, response.OutputStream, GroupName);
            }
            else
            {
                throw new Exception("使用该返回结果时，fileName必须以xls或xlsx、csv结尾");
            }
        }
    }
}