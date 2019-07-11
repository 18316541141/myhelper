using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Entity;
using WebApplication1.Entity.Common;
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
            if (pageSize > 10000)
            {
                return irobotQrCodePayTaskRepository.BigPageList(param, currentPageIndex, pageSize);
            }
            else
            {
                return irobotQrCodePayTaskRepository.PageList(param,currentPageIndex,pageSize);
            }
        }

        public IRobotQrCodePayTask Load(int irTaskID)
        {
            IRobotQrCodePayTaskRepository irobotQrCodePayTaskRepository = new IRobotQrCodePayTaskRepository();
            return irobotQrCodePayTaskRepository.FindEntity(irTaskID);
        }

        public void Del(int irTaskID)
        {
            IRobotQrCodePayTaskRepository irobotQrCodePayTaskRepository = new IRobotQrCodePayTaskRepository();
            irobotQrCodePayTaskRepository.Delete(a=>a.irTaskID==irTaskID);
        }

        public void DelBatch(List<int?> irTaskIds)
        {
            IRobotQrCodePayTaskRepository irobotQrCodePayTaskRepository = new IRobotQrCodePayTaskRepository();
            irobotQrCodePayTaskRepository.Delete(a=> irTaskIds.Contains(a.irTaskID));
        }
    }
}