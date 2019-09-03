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
            ToTable("Distributed_Transaction_Main");
            HasKey(u => u.Id);
            Property(u => u.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(u => u.TransactionStatus).HasColumnName("Transaction_Status");
            /* 建表sql语句，sql_server版
             CREATE TABLE Distributed_Transaction_Main (
                Id bigint NOT NULL ,
                Transaction_Status tinyint NULL ,
                PRIMARY KEY (Id)
            );
             */
        }
    }
}