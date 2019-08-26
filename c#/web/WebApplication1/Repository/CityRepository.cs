using CommonHelper.Helper.EFRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication1.Entity;

namespace WebApplication1.Repository
{
    public class CityRepository : BaseRepository<City>
    {
        public override DbContext CreateDbContext()
        {
            return new MyDbContext();
        }
    }
}