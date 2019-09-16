using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using WebApplication1.Entity;

namespace CommonHelper.EFMap
{
    public sealed partial class HeartbeatEntityMap : EntityTypeConfiguration<HeartbeatEntity>
    {
        public HeartbeatEntityMap()
        {
            ToTable("Heartbeat_Entity");
            HasKey(u => u.Id);
            Property(u => u.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(u => u.Id).HasColumnName("Id");
            Property(u => u.LastHeartbeatTime).HasColumnName("Last_Heartbeat_Time");
            Property(u => u.RobotId).HasColumnName("Robot_Id");
            /* sqlServer版建表脚本
             CREATE TABLE [dbo].[NewTable] (
            [Id] bigint NOT NULL ,
            [Last_Heartbeat_Time] datetime2(7) NULL ,
            [Robot_Id] char(6) COLLATE Chinese_PRC_CI_AS NULL ,
            CONSTRAINT [PK__Heartbea__3214EC07811C7B65] PRIMARY KEY ([Id])
            )
            ON [PRIMARY]
            GO

            IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
            'SCHEMA', N'dbo', 
            'TABLE', N'NewTable', 
            NULL, NULL)) > 0) 
            EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'心跳监测表'
            , @level0type = 'SCHEMA', @level0name = N'dbo'
            , @level1type = 'TABLE', @level1name = N'NewTable'
            ELSE
            EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'心跳监测表'
            , @level0type = 'SCHEMA', @level0name = N'dbo'
            , @level1type = 'TABLE', @level1name = N'NewTable'
            GO

            IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
            'SCHEMA', N'dbo', 
            'TABLE', N'NewTable', 
            'COLUMN', N'Id')) > 0) 
            EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'主键id'
            , @level0type = 'SCHEMA', @level0name = N'dbo'
            , @level1type = 'TABLE', @level1name = N'NewTable'
            , @level2type = 'COLUMN', @level2name = N'Id'
            ELSE
            EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'主键id'
            , @level0type = 'SCHEMA', @level0name = N'dbo'
            , @level1type = 'TABLE', @level1name = N'NewTable'
            , @level2type = 'COLUMN', @level2name = N'Id'
            GO

            IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
            'SCHEMA', N'dbo', 
            'TABLE', N'NewTable', 
            'COLUMN', N'Last_Heartbeat_Time')) > 0) 
            EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'最近一次的心跳时间'
            , @level0type = 'SCHEMA', @level0name = N'dbo'
            , @level1type = 'TABLE', @level1name = N'NewTable'
            , @level2type = 'COLUMN', @level2name = N'Last_Heartbeat_Time'
            ELSE
            EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'最近一次的心跳时间'
            , @level0type = 'SCHEMA', @level0name = N'dbo'
            , @level1type = 'TABLE', @level1name = N'NewTable'
            , @level2type = 'COLUMN', @level2name = N'Last_Heartbeat_Time'
            GO

            IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 
            'SCHEMA', N'dbo', 
            'TABLE', N'NewTable', 
            'COLUMN', N'Robot_Id')) > 0) 
            EXEC sp_updateextendedproperty @name = N'MS_Description', @value = N'机器人id'
            , @level0type = 'SCHEMA', @level0name = N'dbo'
            , @level1type = 'TABLE', @level1name = N'NewTable'
            , @level2type = 'COLUMN', @level2name = N'Robot_Id'
            ELSE
            EXEC sp_addextendedproperty @name = N'MS_Description', @value = N'机器人id'
            , @level0type = 'SCHEMA', @level0name = N'dbo'
            , @level1type = 'TABLE', @level1name = N'NewTable'
            , @level2type = 'COLUMN', @level2name = N'Robot_Id'
            GO
             */
        }
    }
}