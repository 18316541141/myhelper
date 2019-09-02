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
            this.Property(u => u.DistributedTransactionMainId).HasColumnName("Distributed_Transaction_Main_Id");
            this.Property(u => u.TransactionDataSource).HasColumnName("Transaction_Data_Source");
            this.Property(u => u.TransactionTable).HasColumnName("Transaction_Table");
            /* 建表sql语句，sql_server版
             CREATE TABLE Distributed_Transaction_Main_Detail (
                Distributed_Transaction_Main_Id bigint NOT NULL ,
                Transaction_Data_Source varchar(300) NULL ,
                Transaction_Table varchar(30) NULL 
            );
             */
        }
    }
}