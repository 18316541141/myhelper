using CommonHelper.CommonEntity;
using CommonHelper.Helper.EFMap;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CommonHelper.Helper.EFDbContext
{
    public sealed class DistributedMainDbContext : DbContext
    {
        /// <summary>
        /// 分布式事务总表
        /// </summary>
        public DbSet<DistributedTransactionMain> DistributedTransactionMains { get; set; }

        /// <summary>
        /// 分布式事务总表关联表
        /// </summary>
        public DbSet<DistributedTransactionMainDetail> DistributedTransactionMainDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new DistributedTransactionMainMap());
            modelBuilder.Configurations.Add(new DistributedTransactionMainDetailMap());
            
            base.OnModelCreating(modelBuilder);
        }
    }
}