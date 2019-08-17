using CommonHelper.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Controllers.Common;
using WebApplication1.Entity;
using WebApplication1.Entity.Common;
using WebApplication1.ExcelEntity;
using WebApplication1.Filter.Common;
using WebApplication1.Params;
using WebApplication1.Service;

namespace WebApplication1.Controllers
{
    public class IRobotQrCodePayTaskController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <param name="currentPageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public JsonResult Page(IRobotQrCodePayTaskParams param, int currentPageIndex = 1, int pageSize = 20)
        {
            IRobotQrCodePayTaskService irobotQrCodePayTaskService = new IRobotQrCodePayTaskService();
            return MyJson(new Result {code=0,data= irobotQrCodePayTaskService.Page(param, currentPageIndex, pageSize) });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <param name="currentPageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ExcelResult<IRobotQrCodePayTaskExcel> Export(IRobotQrCodePayTaskParams param, int currentPageIndex = 1, int pageSize = 10000)
        {
            IRobotQrCodePayTaskService irobotQrCodePayTaskService = new IRobotQrCodePayTaskService();
            return new ExcelResult<IRobotQrCodePayTaskExcel>
            {
                DataList= IRobotQrCodePayTaskExcel.NewExcelList(irobotQrCodePayTaskService.Page(param, currentPageIndex, pageSize).PageDataList),
                FileName = "测试excel.xlsx"
            };
        }


        public JsonResult Import(HttpPostedFileBase fileUpload,string otherData)
        {
            List<IRobotQrCodePayTaskExcel> irobotQrCodePayTaskExcelList;
            if (fileUpload.FileName.EndsWith("xlsx"))
            {
                irobotQrCodePayTaskExcelList = ExcelHelper.ExcelXlsxToList<IRobotQrCodePayTaskExcel>(fileUpload.InputStream);
            }
            else if (fileUpload.FileName.EndsWith("xls"))
            {
                irobotQrCodePayTaskExcelList = ExcelHelper.ExcelXlsToList<IRobotQrCodePayTaskExcel>(fileUpload.InputStream);
            }
            return MyJson(new Result { code = 0});
        }

        public JsonResult Del(int irTaskID)
        {
            IRobotQrCodePayTaskService irobotQrCodePayTaskService = new IRobotQrCodePayTaskService();
            irobotQrCodePayTaskService.Del(irTaskID);
            return MyJson(new Result { code = 0});
        }

        public JsonResult DelBatch(List<int?> irTaskIds)
        {
            IRobotQrCodePayTaskService irobotQrCodePayTaskService = new IRobotQrCodePayTaskService();
            irobotQrCodePayTaskService.DelBatch(irTaskIds);
            return MyJson(new Result { code = 0 });
        }

        public JsonResult Load(int irTaskID)
        {
            IRobotQrCodePayTaskService irobotQrCodePayTaskService = new IRobotQrCodePayTaskService();
            return MyJson(new Result { code = 0, data = irobotQrCodePayTaskService.Load(irTaskID) });
        }
    }
}