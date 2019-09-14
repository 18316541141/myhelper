using CommonHelper.Helper.EFDbContext;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication1.Entity;
using WebApplication1.Mapping;

namespace WebApplication1
{
    /// <summary>
    /// 数据库访问器
    /// </summary>
    public sealed class MyDbContext2: BaseDbContext
    {
        public MyDbContext2() : base(new SqlConnection("Data Source=183.2.233.235;Initial Catalog=BusinessAssistantDB_Test;User ID=BusinessHeplerTestManager;Password=BusinessHeplerTestManager123;MultipleActiveResultSets=True;"), true){ }

        /// <summary>
        /// 省表
        /// </summary>
        public DbSet<IRobotQrCodePayTask> IRobotQrCodePayTasks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Configurations.Add(new LeftMenuMap());
            modelBuilder.Configurations.Add(new IRobotQrCodePayTaskMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}