using CommonHelper.CommonEntity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace CommonHelper.Helper.EFMap
{
    public class DistributedTransactionMainDetailMap : EntityTypeConfiguration<DistributedTransactionMainDetail>
    {
        public DistributedTransactionMainDetailMap()
        {
            this.ToTable("Distributed_Transaction_Main_Detail");
            this.Property(u => u.DistributedTransactionMainId).HasColumnName("DistributedTransactionMainId");
            this.Property(u => u.TransactionDataSource).HasColumnName("TransactionDataSource");
            this.Property(u => u.TransactionTable).HasColumnName("TransactionTable");
        }
    }
}