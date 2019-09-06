using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using WebApplication1.Entity;

namespace WebApplication1.Mapping
{
    public sealed partial class IRobotUserMap: EntityTypeConfiguration<IRobotUser>
    {
        public IRobotUserMap()
        {
            this.ToTable("IRobot_User");
            this.HasKey(u => u.IUId);
            this.Property(u => u.IUId).HasColumnName("IU_Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

					this.Property(u => u.IUId).HasColumnName("IU_Id");
					this.Property(u => u.IUUsername).HasColumnName("IU_Username");
					this.Property(u => u.IUPassword).HasColumnName("IU_Password");
					this.Property(u => u.IUSignKey).HasColumnName("IU_SignKey");
					this.Property(u => u.IUSignSecret).HasColumnName("IU_SignSecret");
					this.Property(u => u.IUCreateDate).HasColumnName("IU_CreateDate");
        }
    }
}