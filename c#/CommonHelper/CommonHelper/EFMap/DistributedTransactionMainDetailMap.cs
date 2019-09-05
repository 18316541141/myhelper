using CommonHelper.CommonEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace CommonHelper.Helper.EFMap
{
    public sealed class DistributedTransactionMainDetailMap : EntityTypeConfiguration<DistributedTransactionMainDetail>
    {
        public DistributedTransactionMainDetailMap()
        {
            ToTable("Distributed_Transaction_Main_Detail");
            HasKey(u => u.Id);
            Property(u => u.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(u => u.DistributedTransactionMainId).HasColumnName("Distributed_Transaction_Main_Id");
            Property(u => u.TransactionDataSource).HasColumnName("Transaction_Data_Source");
            Property(u => u.TransactionTable).HasColumnName("Transaction_Table");
            /* 建表sql语句，sql_server版
             CREATE TABLE Distributed_Transaction_Main_Detail (
                Id bigint NOT NULL ,
                Distributed_Transaction_Main_Id bigint NOT NULL ,
                Transaction_Data_Source varchar(300) NULL ,
                Transaction_Table varchar(30) NULL ,
                PRIMARY KEY (Id)
            );
             */
        }
    }
}