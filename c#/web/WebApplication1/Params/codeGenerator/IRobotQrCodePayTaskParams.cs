using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.OrderBy;
using WebApplication1.Params;

namespace WebApplication1.Params
{
    /// <summary>
    /// 微信商务助手二维码支付任务表
    /// </summary>
    public sealed partial class IRobotQrCodePayTaskParams
    {


        /// <summary>
        /// 自增id
        /// </summary>
        public int? IRTaskID { set; get; }


        /// <summary>
        /// 自增id
        /// </summary>
        public int? IRTaskIDStart { set; get; }


        /// <summary>
        /// 自增id
        /// </summary>
        public int? IRTaskIDEnd { set; get; }


        /// <summary>
        /// 自增id
        /// </summary>
        public int? IRTaskIDChange { set; get; }


        /// <summary>
        /// 任务单号(办事易全送过来保证必须唯一)
        /// </summary>
        public string IROrderNo { set; get; }


        /// <summary>
        /// 任务单号(办事易全送过来保证必须唯一)
        /// </summary>
        public string IROrderNoLike { set; get; }


        /// <summary>
        /// 微信昵称
        /// </summary>
        public string IRWeiXinNickName { set; get; }


        /// <summary>
        /// 微信昵称
        /// </summary>
        public string IRWeiXinNickNameLike { set; get; }


        /// <summary>
        /// 微信头像图片相对路径
        /// </summary>
        public string IRWeiXinHeaderImage { set; get; }


        /// <summary>
        /// 微信头像图片相对路径
        /// </summary>
        public string IRWeiXinHeaderImageLike { set; get; }


        /// <summary>
        /// 二维码图片相对路径
        /// </summary>
        public string IRQrCodeImagePath { set; get; }


        /// <summary>
        /// 二维码图片相对路径
        /// </summary>
        public string IRQrCodeImagePathLike { set; get; }


        /// <summary>
        /// 处理状态,0:未处理,1:处理完毕,2:处理失败
        /// </summary>
        public int? IRHandleState { set; get; }


        /// <summary>
        /// 处理状态,0:未处理,1:处理完毕,2:处理失败
        /// </summary>
        public int? IRHandleStateStart { set; get; }


        /// <summary>
        /// 处理状态,0:未处理,1:处理完毕,2:处理失败
        /// </summary>
        public int? IRHandleStateEnd { set; get; }


        /// <summary>
        /// 处理状态,0:未处理,1:处理完毕,2:处理失败
        /// </summary>
        public int? IRHandleStateChange { set; get; }


        /// <summary>
        /// 处理情况信息，描述处理过程中的详细问题
        /// </summary>
        public string IRHandleMessage { set; get; }


        /// <summary>
        /// 处理情况信息，描述处理过程中的详细问题
        /// </summary>
        public string IRHandleMessageLike { set; get; }


        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime? IRHandleTime { set; get; }


        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime? IRHandleTimeStart { set; get; }


        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime? IRHandleTimeEnd { set; get; }


        /// <summary>
        /// 任务登记时间
        /// </summary>
        public DateTime? IRCreateTime { set; get; }


        /// <summary>
        /// 任务登记时间
        /// </summary>
        public DateTime? IRCreateTimeStart { set; get; }


        /// <summary>
        /// 任务登记时间
        /// </summary>
        public DateTime? IRCreateTimeEnd { set; get; }


        /// <summary>
        /// 处理结果截图的json列表
        /// </summary>
        public string IRReportPicPathJson { set; get; }


        /// <summary>
        /// 处理结果截图的json列表
        /// </summary>
        public string IRReportPicPathJsonLike { set; get; }


        /// <summary>
        /// 收款金额，单位：分
        /// </summary>
        public int? IRTakeMoney { set; get; }


        /// <summary>
        /// 收款金额，单位：分
        /// </summary>
        public int? IRTakeMoneyStart { set; get; }


        /// <summary>
        /// 收款金额，单位：分
        /// </summary>
        public int? IRTakeMoneyEnd { set; get; }


        /// <summary>
        /// 收款金额，单位：分
        /// </summary>
        public int? IRTakeMoneyChange { set; get; }


        /// <summary>
        /// 机器人编号
        /// </summary>
        public string IRRobotId { set; get; }


        /// <summary>
        /// 机器人编号
        /// </summary>
        public string IRRobotIdLike { set; get; }


        /// <summary>
        /// 支付备注
        /// </summary>
        public string IRRemark { set; get; }


        /// <summary>
        /// 支付备注
        /// </summary>
        public string IRRemarkLike { set; get; }


        /// <summary>
        /// 推送状态, 0:未推送, 1:推送成功, 2:推送失败
        /// </summary>
        public int? IRPushState { set; get; }


        /// <summary>
        /// 推送状态, 0:未推送, 1:推送成功, 2:推送失败
        /// </summary>
        public int? IRPushStateStart { set; get; }


        /// <summary>
        /// 推送状态, 0:未推送, 1:推送成功, 2:推送失败
        /// </summary>
        public int? IRPushStateEnd { set; get; }


        /// <summary>
        /// 推送状态, 0:未推送, 1:推送成功, 2:推送失败
        /// </summary>
        public int? IRPushStateChange { set; get; }


        /// <summary>
        /// 推送时间
        /// </summary>
        public DateTime? IRPushTime { set; get; }


        /// <summary>
        /// 推送时间
        /// </summary>
        public DateTime? IRPushTimeStart { set; get; }


        /// <summary>
        /// 推送时间
        /// </summary>
        public DateTime? IRPushTimeEnd { set; get; }


        /// <summary>
        /// 办事易的ScanPayNotify接口返回结果
        /// </summary>
        public string IRScanPayNotifyRet { set; get; }


        /// <summary>
        /// 办事易的ScanPayNotify接口返回结果
        /// </summary>
        public string IRScanPayNotifyRetLike { set; get; }


        /// <summary>
        /// 该支付订单支付成功时回调。
        /// </summary>
        public string IRScanPayNotifyUrl { set; get; }


        /// <summary>
        /// 该支付订单支付成功时回调。
        /// </summary>
        public string IRScanPayNotifyUrlLike { set; get; }

        /// <summary>
        /// 升序排序
        /// </summary>
        public IRobotQrCodePayTaskOrderBy orderByAsc { set; get; }

        /// <summary>
        /// 降序排序
        /// </summary>
        public IRobotQrCodePayTaskOrderBy orderByDesc { set; get; }
    }
}