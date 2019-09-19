using CommonHelper.Helper.EFDbContext;
using CommonHelper.staticVar;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using WebApplication1.Entity;

namespace WebApplication1
{
    /// <summary>
    /// 数据库访问器
    /// </summary>
    public sealed class MyDbContext: BaseDbContext
    {
        public MyDbContext() : base(new SqlConnection(WebConfigurationManager.ConnectionStrings[$"{AllStatic.EnvironmentType}.sqlConn"].ConnectionString), true){ }
        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);
        }
    }
}