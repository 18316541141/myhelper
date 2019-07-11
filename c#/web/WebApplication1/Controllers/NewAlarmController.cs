using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Controllers.Common;
using WebApplication1.Entity;
using WebApplication1.Entity.Common;
using WebApplication1.Filter;
using WebApplication1.Filter.Common;
using WebApplication1.Service;

namespace WebApplication1.Controllers
{
    public class NewAlarmController:BaseController
    {
        [UpdateVersion]
        public JsonResult Add()
        {
            SystemService systemService = new SystemService();
            systemService.AddNewsAlarm(new NewsAlarm
            {
                createDate=DateTime.Now,
                menuId="m11",
                readState=0,
                receive="rfrefsdfsd",
                title="测试001"
            });
            return MyJson(new Result
            {
                code=0
            });
        }
    }
}