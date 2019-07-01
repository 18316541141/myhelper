using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Entity;
using WebApplication1.Params;
using WebApplication1.Service;

namespace WebApplication1.Controllers
{
    public class IRobotQrCodePayTaskController : Controller
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
            return Json(new Result {code=0,data= irobotQrCodePayTaskService.Page(param, currentPageIndex, pageSize) },JsonRequestBehavior.AllowGet);
        }
    }
}