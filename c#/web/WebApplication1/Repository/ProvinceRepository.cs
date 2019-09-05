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
    public class ProvinceRepository : BaseRepository<Province, Province, Province, Province>
    {
        public override List<BaseDbContext> CreateAllDbContext()
        {
            throw new NotImplementedException();
        }

        public override BaseDbContext CreateDbContext()
        {
            return new MyDbContext();
        }

        public override InverseRepository<Province> CurrentInverse()
        {
            throw new NotImplementedException();
        }

        protected override IQueryable<Province> GetBetweenAnd(IQueryable<Province> query, Province paramz, IComparable baseExtremum, IComparable otherExtremum)
        {
            throw new NotImplementedException();
        }

        protected override Func<Province, IComparable> GetOrderColAndOrderType(Province paramz, out bool orderType)
        {
            throw new NotImplementedException();
        }

        protected override IQueryable<Province> Query(IQueryable<Province> query, Province paramz, Province neqArgs)
        {
            throw new NotImplementedException();
        }

        protected override void SetNull(BaseDbContext dbContext, Province param)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateChange(BaseDbContext dbContext, Province entity)
        {
            throw new NotImplementedException();
        }
    }
}