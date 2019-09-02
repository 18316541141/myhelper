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
            this.Property(u => u.DistributedTransactionMainId).HasColumnName("Distributed_Transaction_Main_Id");
            this.Property(u => u.InverseOper).HasColumnName("Inverse_Oper");
            this.Property(u => u.TransactionStatus).HasColumnName("Transaction_Status");
            this.Property(u => u.TransPrimaryKeyVal).HasColumnName("Trans_Primary_Key_Val");
            this.Property(u => u.TransTableName).HasColumnName("Trans_Table_Name");
            this.Property(u => u.CreateDate).HasColumnName("Create_Date");
            this.Property(u => u.InverseOperType).HasColumnName("Inverse_Oper_Type");
            /* 建表sql语句，sql_server版
             CREATE TABLE Distributed_Transaction_Part (
                Id bigint NOT NULL ,
                Distributed_Transaction_Main_Id bigint NULL ,
                Inverse_Oper varchar(300) NULL ,
                Transaction_Status tinyint NULL ,
                Trans_Primary_Key_Val bigint NULL ,
                Trans_Table_Name varchar(30) NULL ,
                Create_Date datetime2 NULL ,
                Inverse_Oper_Type char(1) NULL ,
                PRIMARY KEY (Id)
            );
             */

        }
    }
}