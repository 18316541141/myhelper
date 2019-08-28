using CommonHelper.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.EFDbContext.Common;

namespace WebApplication1.Repository.Common
{
    public abstract class DistributedRepository
    {
        public abstract DistributedPartDbContext CreateDbContext();
        
    }
}