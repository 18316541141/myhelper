using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Entity;
using WebApplication1.Params;
using WebApplication1.Repository;

namespace WebApplication1.Service
{
    /// <summary>
    /// 
    /// </summary>
    public partial class IRobotQrCodePayTaskService
    {
        public MyPagedList<IRobotQrCodePayTask> Page(IRobotQrCodePayTaskParams param, int currentPageIndex=1, int pageSize=20)
        {
            IRobotQrCodePayTaskRepository irobotQrCodePayTaskRepository = new IRobotQrCodePayTaskRepository();
            return irobotQrCodePayTaskRepository.PageList(param,currentPageIndex,pageSize);
        }
    }
}