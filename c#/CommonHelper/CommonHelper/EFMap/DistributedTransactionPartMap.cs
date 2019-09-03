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
            ToTable("Distributed_Transaction_Part");
            HasKey(u => u.Id);
            Property(u => u.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(u => u.DistributedTransactionMainId).HasColumnName("Distributed_Transaction_Main_Id");
            Property(u => u.InverseOper).HasColumnName("Inverse_Oper");
            Property(u => u.TransactionStatus).HasColumnName("Transaction_Status");
            Property(u => u.TransPrimaryKeyVal).HasColumnName("Trans_Primary_Key_Val");
            Property(u => u.TransTableName).HasColumnName("Trans_Table_Name");
            Property(u => u.CreateDate).HasColumnName("Create_Date");
            Property(u => u.InverseOperType).HasColumnName("Inverse_Oper_Type");
            /* 建表sql语句，sql_server版
             CREATE TABLE Distributed_Transaction_Part (
                Id bigint NOT NULL ,
                Distributed_Transaction_Main_Id bigint NULL ,
                Inverse_Oper varchar(2000) NULL ,
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