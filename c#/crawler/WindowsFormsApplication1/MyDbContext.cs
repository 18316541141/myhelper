using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using WebApplication1.App_Start;

namespace WebApplication1
{
    /// <summary>
    /// 数据库访问器
    /// </summary>
    public class MyDbContext : DbContext
    {
        public MyDbContext() : base(new SQLiteConnection(ConfigurationManager.AppSettings[$"{EnvironmentConfig.EnvironmentType}.Sqlite"]), true)
        {

        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }
    }
}