using CommonHelper.Helper.EFRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication1.Entity;
using CommonHelper.Helper.EFDbContext;
using CommonHelper.EFRepository;
using CommonHelper.CommonEntity;

namespace WebApplication1.Repository
{
    public class DistrictRepository : BaseRepository<District, District, District>
    {
        public override List<BaseDbContext> CreateAllDbContext()
        {
            throw new NotImplementedException();
        }

        public override BaseDbContext CreateDbContext()
        {
            throw new NotImplementedException();
        }

        public override InverseRepository<District> CurrentInverse()
        {
            throw new NotImplementedException();
        }

        public override DisPageEntity<District> GetDisPageEntity()
        {
            throw new NotImplementedException();
        }

        protected override IQueryable<District> Query(IQueryable<District> query, District paramz, District neqArgs, IComparable baseVal = null)
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