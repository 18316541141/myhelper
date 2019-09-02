using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.OrderBy
{
    public partial class IRobotQrCodePayTaskOrderBy
    {


        /// <summary>
        /// 自增id
        /// </summary>
        public bool IRTaskID { set; get; }

        /// <summary>
        /// 任务单号(办事易全送过来保证必须唯一)
        /// </summary>
        public bool IROrderNo { set; get; }

        /// <summary>
        /// 微信昵称
        /// </summary>
        public bool IRWeiXinNickName { set; get; }

        /// <summary>
        /// 微信头像图片相对路径
        /// </summary>
        public bool IRWeiXinHeaderImage { set; get; }

        /// <summary>
        /// 二维码图片相对路径
        /// </summary>
        public bool IRQrCodeImagePath { set; get; }

        /// <summary>
        /// 处理状态,0:未处理,1:处理完毕,2:处理失败
        /// </summary>
        public bool IRHandleState { set; get; }

        /// <summary>
        /// 处理情况信息，描述处理过程中的详细问题
        /// </summary>
        public bool IRHandleMessage { set; get; }

        /// <summary>
        /// 处理时间
        /// </summary>
        public bool IRHandleTime { set; get; }

        /// <summary>
        /// 任务登记时间
        /// </summary>
        public bool IRCreateTime { set; get; }

        /// <summary>
        /// 处理结果截图的json列表
        /// </summary>
        public bool IRReportPicPathJson { set; get; }

        /// <summary>
        /// 收款金额，单位：分
        /// </summary>
        public bool IRTakeMoney { set; get; }

        /// <summary>
        /// 机器人编号
        /// </summary>
        public bool IRRobotId { set; get; }

        /// <summary>
        /// 支付备注
        /// </summary>
        public bool IRRemark { set; get; }

        /// <summary>
        /// 推送状态, 0:未推送, 1:推送成功, 2:推送失败
        /// </summary>
        public bool IRPushState { set; get; }

        /// <summary>
        /// 推送时间
        /// </summary>
        public bool IRPushTime { set; get; }

        /// <summary>
        /// 办事易的ScanPayNotify接口返回结果
        /// </summary>
        public bool IRScanPayNotifyRet { set; get; }

        /// <summary>
        /// 该支付订单支付成功时回调。
        /// </summary>
        public bool IRScanPayNotifyUrl { set; get; }
    }
}