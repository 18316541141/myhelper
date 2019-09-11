using CommonHelper.CommonEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper.EFMap
{
    /// <summary>
    /// 这是日志实体类的映射信息
    /// </summary>
    public class LogEntityMap: EntityTypeConfiguration<LogEntity>
    {
        public LogEntityMap()
        {
            ToTable("log_entity");
            HasKey(u => u.Id);
            Property(u => u.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(u => u.CreateDate).HasColumnName("Create_Date");
            Property(u => u.Level).HasColumnName("Level");
            Property(u => u.ThreadNo).HasColumnName("Thread_No");
            Property(u => u.Message).HasColumnName("Message");
            Property(u => u.Namespace).HasColumnName("Namespace");
            Property(u => u.TypeName).HasColumnName("Type_Name");
            Property(u => u.MethodName).HasColumnName("Method_Name");
            Property(u => u.Username).HasColumnName("USERNAME");
            /* sql_server版的建表数据
                CREATE TABLE [dbo].[NewTable] (
                [ID] bigint NOT NULL ,
                [Create_Date] datetime2 NULL ,
                [Level] varchar(5) NULL ,
                [Thread_No] varchar(5) NULL ,
                [Message] varchar(MAX) NULL ,
                [Namespace] varchar(50) NULL ,
                [Type_Name] varchar(50) NULL ,
                [Method_Name] varchar(30) NULL ,
                [USERNAME] varchar(20) NULL ,
                PRIMARY KEY ([ID])
                )

                GO

                IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
                'SCHEMA', N'dbo', 
                'TABLE', N'NewTable', 
                'COLUMN', N'ID')) > 0) 
                EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'主键id，由分布式雪花id生成'
                , @level0type = 'SCHEMA', @level0name = N'dbo'
                , @level1type = 'TABLE', @level1name = N'NewTable'
                , @level2type = 'COLUMN', @level2name = N'ID'
                ELSE
                EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'主键id，由分布式雪花id生成'
                , @level0type = 'SCHEMA', @level0name = N'dbo'
                , @level1type = 'TABLE', @level1name = N'NewTable'
                , @level2type = 'COLUMN', @level2name = N'ID'
                GO

                IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
                'SCHEMA', N'dbo', 
                'TABLE', N'NewTable', 
                'COLUMN', N'Create_Date')) > 0) 
                EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'日志日期'
                , @level0type = 'SCHEMA', @level0name = N'dbo'
                , @level1type = 'TABLE', @level1name = N'NewTable'
                , @level2type = 'COLUMN', @level2name = N'Create_Date'
                ELSE
                EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'日志日期'
                , @level0type = 'SCHEMA', @level0name = N'dbo'
                , @level1type = 'TABLE', @level1name = N'NewTable'
                , @level2type = 'COLUMN', @level2name = N'Create_Date'
                GO

                IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
                'SCHEMA', N'dbo', 
                'TABLE', N'NewTable', 
                'COLUMN', N'Level')) > 0) 
                EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'日志分级'
                , @level0type = 'SCHEMA', @level0name = N'dbo'
                , @level1type = 'TABLE', @level1name = N'NewTable'
                , @level2type = 'COLUMN', @level2name = N'Level'
                ELSE
                EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'日志分级'
                , @level0type = 'SCHEMA', @level0name = N'dbo'
                , @level1type = 'TABLE', @level1name = N'NewTable'
                , @level2type = 'COLUMN', @level2name = N'Level'
                GO

                IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
                'SCHEMA', N'dbo', 
                'TABLE', N'NewTable', 
                'COLUMN', N'Thread_No')) > 0) 
                EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'线程号'
                , @level0type = 'SCHEMA', @level0name = N'dbo'
                , @level1type = 'TABLE', @level1name = N'NewTable'
                , @level2type = 'COLUMN', @level2name = N'Thread_No'
                ELSE
                EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'线程号'
                , @level0type = 'SCHEMA', @level0name = N'dbo'
                , @level1type = 'TABLE', @level1name = N'NewTable'
                , @level2type = 'COLUMN', @level2name = N'Thread_No'
                GO

                IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
                'SCHEMA', N'dbo', 
                'TABLE', N'NewTable', 
                'COLUMN', N'Message')) > 0) 
                EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'日志内容'
                , @level0type = 'SCHEMA', @level0name = N'dbo'
                , @level1type = 'TABLE', @level1name = N'NewTable'
                , @level2type = 'COLUMN', @level2name = N'Message'
                ELSE
                EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'日志内容'
                , @level0type = 'SCHEMA', @level0name = N'dbo'
                , @level1type = 'TABLE', @level1name = N'NewTable'
                , @level2type = 'COLUMN', @level2name = N'Message'
                GO

                IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
                'SCHEMA', N'dbo', 
                'TABLE', N'NewTable', 
                'COLUMN', N'Namespace')) > 0) 
                EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'日志发生的命名空间'
                , @level0type = 'SCHEMA', @level0name = N'dbo'
                , @level1type = 'TABLE', @level1name = N'NewTable'
                , @level2type = 'COLUMN', @level2name = N'Namespace'
                ELSE
                EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'日志发生的命名空间'
                , @level0type = 'SCHEMA', @level0name = N'dbo'
                , @level1type = 'TABLE', @level1name = N'NewTable'
                , @level2type = 'COLUMN', @level2name = N'Namespace'
                GO

                IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
                'SCHEMA', N'dbo', 
                'TABLE', N'NewTable', 
                'COLUMN', N'Type_Name')) > 0) 
                EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'日志发生的类型'
                , @level0type = 'SCHEMA', @level0name = N'dbo'
                , @level1type = 'TABLE', @level1name = N'NewTable'
                , @level2type = 'COLUMN', @level2name = N'Type_Name'
                ELSE
                EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'日志发生的类型'
                , @level0type = 'SCHEMA', @level0name = N'dbo'
                , @level1type = 'TABLE', @level1name = N'NewTable'
                , @level2type = 'COLUMN', @level2name = N'Type_Name'
                GO

                IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
                'SCHEMA', N'dbo', 
                'TABLE', N'NewTable', 
                'COLUMN', N'Method_Name')) > 0) 
                EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'日志发生的方法名称'
                , @level0type = 'SCHEMA', @level0name = N'dbo'
                , @level1type = 'TABLE', @level1name = N'NewTable'
                , @level2type = 'COLUMN', @level2name = N'Method_Name'
                ELSE
                EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'日志发生的方法名称'
                , @level0type = 'SCHEMA', @level0name = N'dbo'
                , @level1type = 'TABLE', @level1name = N'NewTable'
                , @level2type = 'COLUMN', @level2name = N'Method_Name'
                GO

                IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
                'SCHEMA', N'dbo', 
                'TABLE', N'NewTable', 
                'COLUMN', N'USERNAME')) > 0) 
                EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'导致该日志的用户'
                , @level0type = 'SCHEMA', @level0name = N'dbo'
                , @level1type = 'TABLE', @level1name = N'NewTable'
                , @level2type = 'COLUMN', @level2name = N'USERNAME'
                ELSE
                EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'导致该日志的用户'
                , @level0type = 'SCHEMA', @level0name = N'dbo'
                , @level1type = 'TABLE', @level1name = N'NewTable'
                , @level2type = 'COLUMN', @level2name = N'USERNAME'
                GO
             */
        }
    }
}
