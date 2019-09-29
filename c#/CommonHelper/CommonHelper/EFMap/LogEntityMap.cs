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
            Property(u => u.ProjectName).HasColumnName("Project_Name");
            Property(u => u.TypeName).HasColumnName("Type_Name");
            Property(u => u.FuncName).HasColumnName("Func_Name");
            Property(u => u.Username).HasColumnName("Username");
            /* sql_server版的建表数据
                CREATE TABLE [dbo].[Untitled] (
                  [Id] bigint NOT NULL,
                  [Create_Date] datetime2(7) NULL,
                  [Level] varchar(5) COLLATE Chinese_PRC_CI_AS NULL,
                  [Thread_No] varchar(5) COLLATE Chinese_PRC_CI_AS NULL,
                  [Message] varchar(max) COLLATE Chinese_PRC_CI_AS NULL,
                  [Project_Name] varchar(50) COLLATE Chinese_PRC_CI_AS NULL,
                  [Type_Name] varchar(50) COLLATE Chinese_PRC_CI_AS NULL,
                  [Func_Name] varchar(30) COLLATE Chinese_PRC_CI_AS NULL,
                  [Exception] varchar(max) COLLATE Chinese_PRC_CI_AS NULL,
                  CONSTRAINT [PK__log_enti__3214EC07EFA4021C] PRIMARY KEY CLUSTERED ([Id])
                WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = OFF, ALLOW_PAGE_LOCKS = OFF)  
                ON [PRIMARY]
                )  
                ON [PRIMARY]
                TEXTIMAGE_ON [PRIMARY]
                GO

                ALTER TABLE [dbo].[Untitled] SET (LOCK_ESCALATION = TABLE)
                GO

                EXEC sp_addextendedproperty
                'MS_Description', N'主键id，由分布式雪花id生成',
                'SCHEMA', N'dbo',
                'TABLE', N'Untitled',
                'COLUMN', N'Id'
                GO

                EXEC sp_addextendedproperty
                'MS_Description', N'日志日期',
                'SCHEMA', N'dbo',
                'TABLE', N'Untitled',
                'COLUMN', N'Create_Date'
                GO

                EXEC sp_addextendedproperty
                'MS_Description', N'日志分级',
                'SCHEMA', N'dbo',
                'TABLE', N'Untitled',
                'COLUMN', N'Level'
                GO

                EXEC sp_addextendedproperty
                'MS_Description', N'线程号',
                'SCHEMA', N'dbo',
                'TABLE', N'Untitled',
                'COLUMN', N'Thread_No'
                GO

                EXEC sp_addextendedproperty
                'MS_Description', N'日志内容',
                'SCHEMA', N'dbo',
                'TABLE', N'Untitled',
                'COLUMN', N'Message'
                GO

                EXEC sp_addextendedproperty
                'MS_Description', N'日志发生的项目名',
                'SCHEMA', N'dbo',
                'TABLE', N'Untitled',
                'COLUMN', N'Project_Name'
                GO

                EXEC sp_addextendedproperty
                'MS_Description', N'日志发生的类型',
                'SCHEMA', N'dbo',
                'TABLE', N'Untitled',
                'COLUMN', N'Type_Name'
                GO

                EXEC sp_addextendedproperty
                'MS_Description', N'日志发生的方法名称',
                'SCHEMA', N'dbo',
                'TABLE', N'Untitled',
                'COLUMN', N'Func_Name'
                GO

                EXEC sp_addextendedproperty
                'MS_Description', N'日志的异常堆栈信息',
                'SCHEMA', N'dbo',
                'TABLE', N'Untitled',
                'COLUMN', N'Exception'
             */
        }
    }
}
