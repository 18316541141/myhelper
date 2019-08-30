using CommonHelper.Helper.EFRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication1.Entity;

namespace WebApplication1.Repository
{
    public class ProvinceRepository:BaseRepository<Province, Province, Province>
    {
        public override DbContext CreateDbContext()
        {
            return new MyDbContext();
        }

        protected override IQueryable<Province> Query(IQueryable<Province> query, Province paramz, Province neqArgs)
        {
            throw new NotImplementedException();
        }
    }
}