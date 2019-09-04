using CommonHelper.Helper.EFRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication1.Entity;
using CommonHelper.Helper.EFDbContext;
using CommonHelper.EFRepository;
using System.Linq.Expressions;

namespace WebApplication1.Repository
{
    public class CityRepository : BaseRepository<City, City, City, City>
    {
        public override List<BaseDbContext> CreateAllDbContext()
        {
            throw new NotImplementedException();
        }

        public override BaseDbContext CreateDbContext()
        {
            return new MyDbContext();
        }

        public override InverseRepository<City> CurrentInverse()
        {
            throw new NotImplementedException();
        }

        protected override Expression<Func<City, IComparable>> GetOrderColAndOrderType(City paramz, out bool orderType)
        {
            throw new NotImplementedException();
        }

        protected override IQueryable<City> Query(IQueryable<City> query, City paramz, City neqArgs)
        {
            throw new NotImplementedException();
        }

        protected override void SetNull(BaseDbContext dbContext, City param)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateChange(BaseDbContext dbContext, City entity)
        {
            throw new NotImplementedException();
        }
    }
}