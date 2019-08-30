using CommonHelper.Helper.EFRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication1.Entity;

namespace WebApplication1.Repository
{
    public class CityRepository : BaseRepository<City, City, City>
    {
        public override DbContext CreateDbContext()
        {
            return new MyDbContext();
        }

        protected override IQueryable<City> Query(IQueryable<City> query, City paramz, City neqArgs)
        {
            throw new NotImplementedException();
        }
    }
}