using CommonHelper.CommonEntity;
using CommonHelper.Helper.EFMap;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CommonHelper.Helper.EFDbContext
{
    /// <summary>
    /// 分布式事务dbContext
    /// </summary>
    public class DistributedPartDbContext : DbContext
    {
        /// <summary>
        /// 数据源标识
        /// </summary>
        public string TransactionDataSource { set; get; }

        /// <summary>
        /// 分布式分表
        /// </summary>
        public DbSet<DistributedTransactionPart> DistributedTransactionParts { get; set; }

        protected new virtual void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new DistributedTransactionPartMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}