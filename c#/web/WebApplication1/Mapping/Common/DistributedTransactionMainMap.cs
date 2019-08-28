using CommonHelper.CommonEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace WebApplication1.Mapping.Common
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
        }
    }
}