using CommonHelper.Helper.EFRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication1.Entity;
using CommonHelper.Helper.EFDbContext;
using CommonHelper.EFRepository;

namespace WebApplication1.Repository
{
    public class DistrictRepository : BaseRepository<District, District, District, District>
    {
        public override List<BaseDbContext> CreateAllDbContext()
        {
            throw new NotImplementedException();
        }

        public override BaseDbContext CreateDbContext()
        {
            return new MyDbContext();
        }

        public override InverseRepository<District> CurrentInverse()
        {
            throw new NotImplementedException();
        }

        protected override IQueryable<District> GetBetweenAnd(IQueryable<District> query, District paramz, IComparable baseExtremum, IComparable otherExtremum)
        {
            throw new NotImplementedException();
        }

        protected override Func<District, IComparable> GetOrderColAndOrderType(District paramz, out bool orderType)
        {
            throw new NotImplementedException();
        }

        protected override IQueryable<District> Query(IQueryable<District> query, District paramz, District neqArgs)
        {
            throw new NotImplementedException();
        }

        protected override void SetNull(BaseDbContext dbContext, District param)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateChange(BaseDbContext dbContext, District entity)
        {
            throw new NotImplementedException();
        }
    }
}