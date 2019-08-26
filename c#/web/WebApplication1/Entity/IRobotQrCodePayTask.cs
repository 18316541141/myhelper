using CommonHelper.Helper;
using Newtonsoft.Json;
using System;

namespace WebApplication1.Entity
{
    public partial class IRobotQrCodePayTask
    {
        public IRobotQrCodePayTask() { }

        public virtual int? irTaskID { set; get; }

        public virtual string irOrderNo { set; get; }

        public virtual string irWeiXinNickName { set; get; }

        public virtual string irWeiXinHeaderImage { set; get; }

        public virtual string irQrCodeImagePath { set; get; }

        public virtual int? irHandleState { set; get; }

        public virtual string irHandleMessage { set; get; }

        public virtual DateTime? irHandleTime { set; get; }

        public virtual DateTime? irCreateTime { set; get; }

        public virtual string irReportPicPathJson { set; get; }

        public virtual int? irTakeMoney { set; get; }

        public virtual string irRobotId { set; get; }

        public virtual string irRemark { set; get; }

        public virtual string irPushState { set; get; }

        public virtual DateTime? irPushTime { set; get; }

        public virtual string irScanPayNotifyRet { set; get; }
    }
}