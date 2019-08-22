using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using WebApplication1.Entity;

namespace WebApplication1.Mapping
{
    public partial class IRobotQrCodePayTaskMap: EntityTypeConfiguration<IRobotQrCodePayTask>
    {
        public IRobotQrCodePayTaskMap()
        {
            this.ToTable("IRobot_QrCodePayTask");
            this.HasKey(u => u.IRTaskID);
            this.Property(u => u.IRTaskID).HasColumnName("IR_TaskID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

					this.Property(u => u.IRTaskID).HasColumnName("IR_TaskID");
					this.Property(u => u.IROrderNo).HasColumnName("IR_OrderNo");
					this.Property(u => u.IRTakeMoney).HasColumnName("IR_TakeMoney");
					this.Property(u => u.IRPushState).HasColumnName("IR_PushState");
					this.Property(u => u.IRScanPayNotifyUrl).HasColumnName("IR_ScanPayNotifyUrl");
					this.Property(u => u.IRScanPayNotifyRet).HasColumnName("IR_ScanPayNotifyRet");
					this.Property(u => u.IRHandleState).HasColumnName("IR_HandleState");
					this.Property(u => u.IRQrCodeImagePath).HasColumnName("IR_QrCodeImagePath");
					this.Property(u => u.IRBsyNotifyState).HasColumnName("IR_BsyNotifyState");
					this.Property(u => u.IRSendMoneyNotifyState).HasColumnName("IR_SendMoneyNotifyState");
					this.Property(u => u.IRReportPicPath).HasColumnName("IR_ReportPicPath");
        }
    }
}