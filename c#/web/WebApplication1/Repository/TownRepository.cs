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
    public class TownRepository : BaseRepository<Town, Town, Town, Town>
    {
        public override List<BaseDbContext> CreateAllDbContext()
        {
            throw new NotImplementedException();
        }

        public override BaseDbContext CreateDbContext()
        {
            return new MyDbContext();
        }

        public override InverseRepository<Town> CurrentInverse()
        {
            throw new NotImplementedException();
        }

        protected override IQueryable<Town> GetBetweenAnd(IQueryable<Town> query, Town paramz, IComparable baseExtremum, IComparable otherExtremum)
        {
            throw new NotImplementedException();
        }

        protected override Func<Town, IComparable> GetOrderColAndOrderType(Town paramz, out bool orderType)
        {
            throw new NotImplementedException();
        }

        protected override IQueryable<Town> Query(IQueryable<Town> query, Town paramz, Town neqArgs)
        {
            throw new NotImplementedException();
        }

        protected override void SetNull(BaseDbContext dbContext, Town param)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateChange(BaseDbContext dbContext, Town entity)
        {
            throw new NotImplementedException();
        }
    }
}