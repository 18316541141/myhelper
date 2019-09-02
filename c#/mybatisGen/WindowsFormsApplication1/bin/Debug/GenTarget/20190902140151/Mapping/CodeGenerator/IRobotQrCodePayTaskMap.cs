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
					this.Property(u => u.IRWeiXinNickName).HasColumnName("IR_WeiXinNickName");
					this.Property(u => u.IRWeiXinHeaderImage).HasColumnName("IR_WeiXinHeaderImage");
					this.Property(u => u.IRQrCodeImagePath).HasColumnName("IR_QrCodeImagePath");
					this.Property(u => u.IRHandleState).HasColumnName("IR_HandleState");
					this.Property(u => u.IRHandleMessage).HasColumnName("IR_HandleMessage");
					this.Property(u => u.IRHandleTime).HasColumnName("IR_HandleTime");
					this.Property(u => u.IRCreateTime).HasColumnName("IR_CreateTime");
					this.Property(u => u.IRReportPicPathJson).HasColumnName("IR_ReportPicPathJson");
					this.Property(u => u.IRTakeMoney).HasColumnName("IR_TakeMoney");
					this.Property(u => u.IRRobotId).HasColumnName("IR_RobotId");
					this.Property(u => u.IRRemark).HasColumnName("IR_Remark");
					this.Property(u => u.IRPushState).HasColumnName("IR_PushState");
					this.Property(u => u.IRPushTime).HasColumnName("IR_PushTime");
					this.Property(u => u.IRScanPayNotifyRet).HasColumnName("IR_ScanPayNotifyRet");
					this.Property(u => u.IRScanPayNotifyUrl).HasColumnName("IR_ScanPayNotifyUrl");
        }
    }
}