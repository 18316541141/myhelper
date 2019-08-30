using CommonHelper.Helper.EFRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication1.Entity;

namespace WebApplication1.Repository
{
    public class TownRepository : BaseRepository<Town, Town, Town>
    {
        public override DbContext CreateDbContext()
        {
            return new MyDbContext();
        }

        protected override IQueryable<Town> Query(IQueryable<Town> query, Town paramz, Town neqArgs)
        {
            throw new NotImplementedException();
        }
    }
}