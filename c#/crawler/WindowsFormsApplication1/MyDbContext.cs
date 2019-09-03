using CommonHelper.CommonEntity;
using CommonHelper.Helper.EFDbContext;
using CommonHelper.Helper.EFMap;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using WebApplication1.App_Start;
using WebApplication1.Entity;

namespace WebApplication1
{
    /// <summary>
    /// 数据库访问器
    /// </summary>
    public class MyDbContext : BaseDbContext
    {
        public MyDbContext() :
            base(new SQLiteConnection(ConfigurationManager.AppSettings[$"{EnvironmentConfig.EnvironmentType}.Sqlite"]), true)
        {

        }

        /// <summary>
        /// 微信商务助手二维码支付任务表
        /// </summary>
        public DbSet<DistributedTransactionPart> DistributedTransactionParts { set; get; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new DistributedTransactionPartMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}