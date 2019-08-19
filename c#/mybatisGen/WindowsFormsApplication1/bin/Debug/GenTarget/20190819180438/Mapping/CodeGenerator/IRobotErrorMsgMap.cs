using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using WebApplication1.Entity;

namespace WebApplication1.Mapping
{
    public partial class IRobotErrorMsgMap: EntityTypeConfiguration<IRobotErrorMsg>
    {
        public IRobotErrorMsgMap()
        {
            this.ToTable("IRobot_ErrorMsg");
            this.HasKey(u => u.IEErrNo);
            this.Property(u => u.IEErrNo).HasColumnName("IE_ErrNo").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

					this.Property(u => u.IEErrNo).HasColumnName("IE_ErrNo");
					this.Property(u => u.IECreateDate).HasColumnName("IE_CreateDate");
					this.Property(u => u.IEErrOrderNo).HasColumnName("IE_ErrOrderNo");
					this.Property(u => u.IEErrRobotId).HasColumnName("IE_ErrRobotId");
					this.Property(u => u.IEErrPic).HasColumnName("IE_ErrPic");
					this.Property(u => u.IEErrContext).HasColumnName("IE_ErrContext");
					this.Property(u => u.IEHandleStatus).HasColumnName("IE_HandleStatus");
        }
    }
}