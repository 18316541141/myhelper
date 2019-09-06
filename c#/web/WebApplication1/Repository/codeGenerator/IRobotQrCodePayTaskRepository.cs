using CommonHelper.Helper.EFDbContext;
using CommonHelper.Helper.EFRepository;
using System.Linq;
using WebApplication1.Entity;
using WebApplication1.Params;
using WebApplication1.OrderBy;
using CommonHelper.AopInterceptor;
using Autofac.Extras.DynamicProxy;
using CommonHelper.EFRepository;
using System;
using System.Collections.Generic;
using CommonHelper.CommonEntity;

namespace WebApplication1.Repository
{
    //启用分布式事务
    //[Intercept(typeof(DistributeRepository))]
    public partial class IRobotQrCodePayTaskRepository : BaseRepository<IRobotQrCodePayTask, IRobotQrCodePayTaskParams, IRobotQrCodePayTaskSetNullParams>
    {
        public override List<BaseDbContext> CreateAllDbContext()
        {
            throw new NotImplementedException();
        }

        public override BaseDbContext CreateDbContext()
        {
            throw new NotImplementedException();
        }

        public override InverseRepository<IRobotQrCodePayTask> CurrentInverse()
        {
            throw new NotImplementedException();
        }

        public override DisPageEntity<IRobotQrCodePayTask> GetDisPageEntity()
        {
            throw new NotImplementedException();
        }

        protected override IQueryable<IRobotQrCodePayTask> Query(IQueryable<IRobotQrCodePayTask> query, IRobotQrCodePayTaskParams paramz, IRobotQrCodePayTaskParams neqArgs, IComparable baseVal = null)
        {
            throw new NotImplementedException();
        }

        protected override void SetNull(BaseDbContext dbContext, IRobotQrCodePayTaskSetNullParams param)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateChange(BaseDbContext dbContext, IRobotQrCodePayTask entity)
        {
            throw new NotImplementedException();
        }
    }
}