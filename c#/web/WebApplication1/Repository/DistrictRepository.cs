using CommonHelper.Helper.EFRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication1.Entity;
using CommonHelper.Helper.EFDbContext;

namespace WebApplication1.Repository
{
    public class DistrictRepository : BaseRepository<District, District, District>
    {
        public override BaseDbContext CreateDbContext()
        {
            return new MyDbContext();
        }

        protected override IQueryable<District> Query(IQueryable<District> query, District paramz, District neqArgs)
        {
            throw new NotImplementedException();
        }
    }
}