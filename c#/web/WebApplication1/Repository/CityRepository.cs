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
using CommonHelper.CommonEntity;

namespace WebApplication1.Repository
{
    public class CityRepository : BaseRepository<City, City, City>
    {
        public override List<BaseDbContext> CreateAllDbContext()
        {
            throw new NotImplementedException();
        }

        public override BaseDbContext CreateDbContext()
        {
            throw new NotImplementedException();
        }

        public override InverseRepository<City> CurrentInverse()
        {
            throw new NotImplementedException();
        }

        public override DisPageEntity<City> GetDisPageEntity()
        {
            throw new NotImplementedException();
        }

        protected override IQueryable<City> Query(IQueryable<City> query, City paramz, City neqArgs, IComparable baseVal = null)
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