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
    public class ProvinceRepository : BaseRepository<Province, Province, Province>
    {
        public override List<BaseDbContext> CreateAllDbContext()
        {
            throw new NotImplementedException();
        }

        public override BaseDbContext CreateDbContext()
        {
            throw new NotImplementedException();
        }

        public override InverseRepository<Province> CurrentInverse()
        {
            throw new NotImplementedException();
        }

        public override DisPageEntity<Province> GetDisPageEntity()
        {
            throw new NotImplementedException();
        }

        protected override IQueryable<Province> Query(IQueryable<Province> query, Province paramz, Province neqArgs, IComparable baseVal = null)
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