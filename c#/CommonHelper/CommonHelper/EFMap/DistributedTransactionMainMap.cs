using CommonHelper.CommonEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace CommonHelper.Helper.EFMap
{
    /// <summary>
    /// 事务总表
    /// </summary>
    public class DistributedTransactionMainMap: EntityTypeConfiguration<DistributedTransactionMain>
    {
        public DistributedTransactionMainMap()
        {
            this.ToTable("Distributed_Transaction_Main");
            this.HasKey(u => u.Id);
            this.Property(u => u.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            this.Property(u => u.TransactionStatus).HasColumnName("TransactionStatus");
        }
    }
}