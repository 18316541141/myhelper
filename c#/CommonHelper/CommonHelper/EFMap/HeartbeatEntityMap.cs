using CommonHelper.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using WebApplication1.Entity;

namespace CommonHelper.Mapping
{
    public sealed partial class HeartbeatEntityMap : EntityTypeConfiguration<HeartbeatEntity>
    {
        public HeartbeatEntityMap()
        {
            this.ToTable("Heartbeat_Entity");
            this.HasKey(u => u.Id);
            this.Property(u => u.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            this.Property(u => u.Id).HasColumnName("Id");
            this.Property(u => u.LastHeartbeatTime).HasColumnName("Last_Heartbeat_Time");
            this.Property(u => u.RobotIp).HasColumnName("Robot_Ip");
            this.Property(u => u.Remark).HasColumnName("Remark");
            this.Property(u => u.ExtendField).HasColumnName("Extend_Field");
            this.Property(u => u.MonitorServer).HasColumnName("Monitor_Server");
        }
    }
}