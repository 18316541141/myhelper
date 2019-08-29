﻿using Autofac.Extras.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.AopInterceptor;
using WebApplication1.Entity;
using WebApplication1.Entity.Common;
using WebApplication1.Params;
using WebApplication1.Repository;

namespace WebApplication1.Service
{

    /// <summary>
    /// 
    /// </summary>
    [Intercept(typeof(DistributedTransactionScope))]
    public partial class IRobotQrCodePayTaskService
    {
        public IRobotQrCodePayTaskRepository IRobotQrCodePayTaskRepository { set; get; }

        public MyPagedList<IRobotQrCodePayTask> Page(IRobotQrCodePayTaskParams param, int currentPageIndex=1, int pageSize=20)
        {
            if (pageSize > 10000)
            {
                return IRobotQrCodePayTaskRepository.BigPageList(param, currentPageIndex, pageSize);
            }
            else
            {
                return IRobotQrCodePayTaskRepository.PageList(param,currentPageIndex,pageSize);
            }
        }

        public IRobotQrCodePayTask Load(int irTaskID)
        {
            return IRobotQrCodePayTaskRepository.FindEntity(irTaskID);
        }

        
        public void Del(int irTaskID)
        {
            IRobotQrCodePayTaskRepository.Delete(a=>a.irTaskID==irTaskID);
        }

        public void DelBatch(List<int?> irTaskIds)
        {
            IRobotQrCodePayTaskRepository.Delete(a=> irTaskIds.Contains(a.irTaskID));
        }
    }
}