using CommonHelper.CommonEntity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication1.Mapping.Common;

namespace WebApplication1.EFDbContext.Common
{
    public class DistributedMainDbContext : DbContext
    {
        /// <summary>
        /// 分布式总表
        /// </summary>
        public DbSet<DistributedTransactionMain> DistributedTransactionMains { get; set; }

        /// <summary>
        /// 分布式总表
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