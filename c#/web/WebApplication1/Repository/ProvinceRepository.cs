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
    public class ProvinceRepository : BaseRepository<Province, Province, Province, Province>
    {
        public override BaseDbContext CreateDbContext()
        {
            return new MyDbContext();
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