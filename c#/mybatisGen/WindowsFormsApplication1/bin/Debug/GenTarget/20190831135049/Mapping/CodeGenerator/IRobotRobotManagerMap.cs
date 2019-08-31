using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using WebApplication1.Entity;

namespace WebApplication1.Mapping
{
    public partial class IRobotRobotManagerMap: EntityTypeConfiguration<IRobotRobotManager>
    {
        public IRobotRobotManagerMap()
        {
            this.ToTable("IRobot_RobotManager");
            this.HasKey(u => u.IRMID);
            this.Property(u => u.IRMID).HasColumnName("IRM_ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

					this.Property(u => u.IRMID).HasColumnName("IRM_ID");
					this.Property(u => u.IRMRobotId).HasColumnName("IRM_RobotId");
					this.Property(u => u.IRMRobotState).HasColumnName("IRM_RobotState");
					this.Property(u => u.IRMRuningTime).HasColumnName("IRM_RuningTime");
					this.Property(u => u.IRMMaxPayTradeCount).HasColumnName("IRM_MaxPayTradeCount");
					this.Property(u => u.IRMCurrentPayCount).HasColumnName("IRM_CurrentPayCount");
					this.Property(u => u.IRMBalance).HasColumnName("IRM_Balance");
					this.Property(u => u.IRMIP).HasColumnName("IRM_IP");
					this.Property(u => u.IRMCreateTime).HasColumnName("IRM_CreateTime");
					this.Property(u => u.IRMTeamViewId).HasColumnName("IRM_TeamViewId");
					this.Property(u => u.IRMTeamViewPassword).HasColumnName("IRM_TeamViewPassword");
					this.Property(u => u.IRMSunflowerId).HasColumnName("IRM_SunflowerId");
					this.Property(u => u.IRMSunflowerPassword).HasColumnName("IRM_SunflowerPassword");
					this.Property(u => u.IRMBankCardTailNum).HasColumnName("IRM_BankCardTailNum");
					this.Property(u => u.IRMBankCardName).HasColumnName("IRM_BankCardName");
					this.Property(u => u.IRMMinBalance).HasColumnName("IRM_MinBalance");
					this.Property(u => u.IRMPayPassword).HasColumnName("IRM_PayPassword");
					this.Property(u => u.IRMScanPayNotifyUrl).HasColumnName("IRM_ScanPayNotifyUrl");
					this.Property(u => u.IRMWxUsername).HasColumnName("IRM_WxUsername");
					this.Property(u => u.IRMWxPassword).HasColumnName("IRM_WxPassword");
					this.Property(u => u.IRMRefreshBalance).HasColumnName("IRM_RefreshBalance");
        }
    }
}