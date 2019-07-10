using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using WebApplication1.Entity;

namespace WebApplication1.Mapping
{
    public class IRobotQrCodePayTaskMap: EntityTypeConfiguration<IRobotQrCodePayTask>
    {
        public IRobotQrCodePayTaskMap()
        {
            this.ToTable("IRobotQrCodePayTask");
            this.HasKey(u => u.irTaskID);
            this.Property(u => u.irTaskID).HasColumnName("IR_TaskID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

					this.Property(u => u.irTaskID).HasColumnName("IR_TaskID");
					this.Property(u => u.irWeiXinHeaderImage).HasColumnName("IR_WeiXinHeaderImage");
					this.Property(u => u.irWeiXinNickName).HasColumnName("IR_WeiXinNickName");
					this.Property(u => u.irCreateTime).HasColumnName("IR_CreateTime");
					this.Property(u => u.irHandleMessage).HasColumnName("IR_HandleMessage");
					this.Property(u => u.irHandleState).HasColumnName("IR_HandleState");
					this.Property(u => u.irHandleTime).HasColumnName("IR_HandleTime");
					this.Property(u => u.irOrderNo).HasColumnName("IR_OrderNo");
					this.Property(u => u.irPushState).HasColumnName("IR_PushState");
					this.Property(u => u.irPushTime).HasColumnName("IR_PushTime");
					this.Property(u => u.irQrCodeImagePath).HasColumnName("IR_QrCodeImagePath");
					this.Property(u => u.irRemark).HasColumnName("IR_Remark");
					this.Property(u => u.irReportPicPathJson).HasColumnName("IR_ReportPicPathJson");
					this.Property(u => u.irRobotId).HasColumnName("IR_RobotId");
					this.Property(u => u.irScanPayNotifyRet).HasColumnName("IR_ScanPayNotifyRet");
					this.Property(u => u.irTakeMoney).HasColumnName("IR_TakeMoney");
        }
    }
}