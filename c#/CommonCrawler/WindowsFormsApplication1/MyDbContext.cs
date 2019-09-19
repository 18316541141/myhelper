using CommonHelper.CommonEntity;
using CommonHelper.Helper.EFDbContext;
using CommonHelper.Helper.EFMap;
using CommonHelper.staticVar;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using WebApplication1.Entity;

namespace WebApplication1
{
    /// <summary>
    /// 数据库访问器
    /// </summary>
    public class MyDbContext : BaseDbContext
    {
        public MyDbContext() :
            base(new SQLiteConnection(ConfigurationManager.AppSettings[$"{AllStatic.EnvironmentType}.Sqlite"]), true)
        {

        }

        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }
    }
}