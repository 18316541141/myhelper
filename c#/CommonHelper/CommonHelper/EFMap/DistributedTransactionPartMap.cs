using CommonHelper.CommonEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace CommonHelper.Helper.EFMap
{
    public class DistributedTransactionPartMap : EntityTypeConfiguration<DistributedTransactionPart>
    {
        public DistributedTransactionPartMap()
        {
            this.ToTable("Distributed_Transaction_Part");
            this.HasKey(u => u.Id);
            this.Property(u => u.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            this.Property(u => u.DistributedTransactionMainId).HasColumnName("DistributedTransactionMainId");
            this.Property(u => u.InverseOper).HasColumnName("InverseOper");
            this.Property(u => u.TransactionStatus).HasColumnName("TransactionStatus");
            this.Property(u => u.TransPrimaryKey).HasColumnName("TransPrimaryKey");
            this.Property(u => u.TransTableName).HasColumnName("TransTableName");
            this.Property(u => u.CreateDate).HasColumnName("CreateDate");
            this.Property(u => u.InverseOperType).HasColumnName("InverseOperType");

        }
    }
}