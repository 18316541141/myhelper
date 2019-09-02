using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace WebApplication1.Entity
{
    /// <summary>
    /// 微信商务助手二维码支付任务表
    /// </summary>
    public partial class IRobotQrCodePayTask
    {
        public IRobotQrCodePayTask() { }

        /// <summary>
        /// 自增id
        /// </summary>
        public virtual int? IRTaskID { set; get; }
        /// <summary>
        /// 任务单号(办事易全送过来保证必须唯一)
        /// </summary>
        public virtual string IROrderNo { set; get; }
        /// <summary>
        /// 微信昵称
        /// </summary>
        public virtual string IRWeiXinNickName { set; get; }
        /// <summary>
        /// 微信头像图片相对路径
        /// </summary>
        public virtual string IRWeiXinHeaderImage { set; get; }
        /// <summary>
        /// 二维码图片相对路径
        /// </summary>
        public virtual string IRQrCodeImagePath { set; get; }
        /// <summary>
        /// 处理状态,0:未处理,1:处理完毕,2:处理失败
        /// </summary>
        public virtual int? IRHandleState { set; get; }
        /// <summary>
        /// 处理情况信息，描述处理过程中的详细问题
        /// </summary>
        public virtual string IRHandleMessage { set; get; }
        /// <summary>
        /// 处理时间
        /// </summary>
        public virtual DateTime? IRHandleTime { set; get; }
        /// <summary>
        /// 任务登记时间
        /// </summary>
        public virtual DateTime? IRCreateTime { set; get; }
        /// <summary>
        /// 处理结果截图的json列表
        /// </summary>
        public virtual string IRReportPicPathJson { set; get; }
        /// <summary>
        /// 收款金额，单位：分
        /// </summary>
        public virtual int? IRTakeMoney { set; get; }
        /// <summary>
        /// 机器人编号
        /// </summary>
        public virtual string IRRobotId { set; get; }
        /// <summary>
        /// 支付备注
        /// </summary>
        public virtual string IRRemark { set; get; }
        /// <summary>
        /// 推送状态, 0:未推送, 1:推送成功, 2:推送失败
        /// </summary>
        public virtual int? IRPushState { set; get; }
        /// <summary>
        /// 推送时间
        /// </summary>
        public virtual DateTime? IRPushTime { set; get; }
        /// <summary>
        /// 办事易的ScanPayNotify接口返回结果
        /// </summary>
        public virtual string IRScanPayNotifyRet { set; get; }
        /// <summary>
        /// 该支付订单支付成功时回调。
        /// </summary>
        public virtual string IRScanPayNotifyUrl { set; get; }
    }
}