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
    public class TownRepository : BaseRepository<Town, Town, Town>
    {
        public override List<BaseDbContext> CreateAllDbContext()
        {
            throw new NotImplementedException();
        }

        public override BaseDbContext CreateDbContext()
        {
            throw new NotImplementedException();
        }

        public override InverseRepository<Town> CurrentInverse()
        {
            throw new NotImplementedException();
        }

        public override DisPageEntity<Town> GetDisPageEntity()
        {
            throw new NotImplementedException();
        }

        protected override IQueryable<Town> Query(IQueryable<Town> query, Town paramz, Town neqArgs, IComparable baseVal = null)
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