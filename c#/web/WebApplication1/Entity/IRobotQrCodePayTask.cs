using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Entity
{
    public class IRobotQrCodePayTask
    {
        public int? irTaskID { set; get; }
        public string irOrderNo { set; get; }
        public string irWeiXinNickName { set; get; }
        public string irWeiXinHeaderImage { set; get; }
        public string irQrCodeImagePath { set; get; }
        public int? irHandleState { set; get; }
        public string irHandleMessage { set; get; }
        public DateTime? irHandleTime { set; get; }
        public DateTime? irCreateTime { set; get; }
        public string irReportPicPathJson { set; get; }
        public int? irTakeMoney { set; get; }
        public string irRobotId { set; get; }
        public string irRemark { set; get; }
        public string irPushState { set; get; }
        public DateTime? irPushTime { set; get; }
        public string irScanPayNotifyRet { set; get; }
    }
}