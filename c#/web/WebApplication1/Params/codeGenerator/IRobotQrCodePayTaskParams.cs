using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.OrderBy;

namespace WebApplication1.Params
{
    /// <summary>
    /// 
    /// </summary>
    public class IRobotQrCodePayTaskParams
    {
        public int? irTaskID { set; get; }
        public string irOrderNo { set; get; }
        public string irOrderNoLike { set; get; }
        public string irWeiXinNickName { set; get; }
        public string irWeiXinNickNameLike { set; get; }
        public string irWeiXinHeaderImage { set; get; }
        public string irWeiXinHeaderImageLike { set; get; }
        public string irQrCodeImagePath { set; get; }
        public string irQrCodeImagePathLike { set; get; }
        public int? irHandleState { set; get; }
        public string irHandleStateLike { set; get; }
        public string irHandleMessage { set; get; }
        public string irHandleMessageLike { set; get; }
        public DateTime? irHandleTime { set; get; }
        public DateTime? irHandleTimeStart { set; get; }
        public DateTime? irHandleTimeEnd { set; get; }
        public DateTime? irCreateTime { set; get; }
        public DateTime? irCreateTimeStart { set; get; }
        public DateTime? irCreateTimeEnd { set; get; }
        public string irReportPicPathJson { set; get; }
        public string irReportPicPathJsonLike { set; get; }
        public int? irTakeMoney { set; get; }
        public int? irTakeMoneyStart { set; get; }
        public int? irTakeMoneyEnd { set; get; }
        public string irRobotId { set; get; }
        public string irRobotIdLike { set; get; }
        public string irRemark { set; get; }
        public string irRemarkLike { set; get; }
        public string irPushState { set; get; }
        public string irPushStateLike { set; get; }
        public DateTime? irPushTime { set; get; }
        public DateTime? irPushTimeStart { set; get; }
        public DateTime? irPushTimeEnd { set; get; }
        public string irScanPayNotifyRet { set; get; }
        public string irScanPayNotifyRetLike { set; get; }
        public IRobotQrCodePayTaskOrderBy orderByAsc { set; get; }
        public IRobotQrCodePayTaskOrderBy orderByDesc { set; get; }
    }
}