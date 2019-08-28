using CommonHelper.CommonEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace WebApplication1.Mapping.Common
{
    public class DistributedTransactionMainDetailMap : EntityTypeConfiguration<DistributedTransactionMainDetail>
    {
        public DistributedTransactionMainDetailMap()
        {
            this.ToTable("Distributed_Transaction_Main_Detail");
            this.HasKey(u => u.Id);
            this.Property(u => u.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            this.Property(u => u.DistributedTransactionMainId).HasColumnName("DistributedTransactionMainId");
            this.Property(u => u.PartDatabaseTag).HasColumnName("PartDatabaseTag");
        }
    }
}