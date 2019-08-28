using CommonHelper.CommonEntity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication1.Mapping.Common;

namespace WebApplication1.EFDbContext.Common
{
    /// <summary>
    /// 分布式事务dbContext
    /// </summary>
    public class DistributedPartDbContext : DbContext
    {
        /// <summary>
        /// 分布式分表
        /// </summary>
        public DbSet<DistributedTransactionPart> DistributedTransactionParts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new DistributedTransactionPartMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}