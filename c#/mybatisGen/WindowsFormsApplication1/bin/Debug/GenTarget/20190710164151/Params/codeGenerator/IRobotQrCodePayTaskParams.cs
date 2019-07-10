using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Params.codeGenerator;

namespace WebApplication1.Params
{
    /// <summary>
    /// 微信商务助手二维码支付任务表
    /// </summary>
    public class IRobotQrCodePayTaskParams
    {


    	    /// <summary>
    	    /// 自增id
    	    /// </summary>
    	    public int? irTaskID { set; get; }
        

    	    /// <summary>
    	    /// 自增id
    	    /// </summary>
    	    public int? irTaskIDStart { set; get; }
        

    	    /// <summary>
    	    /// 自增id
    	    /// </summary>
    	    public int? irTaskIDEnd { set; get; }
        

    	    /// <summary>
    	    /// 微信头像图片相对路径
    	    /// </summary>
    	    public string irWeiXinHeaderImage { set; get; }
        

    	    /// <summary>
    	    /// 微信头像图片相对路径
    	    /// </summary>
    	    public string irWeiXinHeaderImageLike { set; get; }
        

    	    /// <summary>
    	    /// 微信昵称
    	    /// </summary>
    	    public string irWeiXinNickName { set; get; }
        

    	    /// <summary>
    	    /// 微信昵称
    	    /// </summary>
    	    public string irWeiXinNickNameLike { set; get; }
        

    	    /// <summary>
    	    /// 任务登记时间
    	    /// </summary>
    	    public DateTime? irCreateTime { set; get; }
        

    	    /// <summary>
    	    /// 任务登记时间
    	    /// </summary>
    	    public DateTime? irCreateTimeStart { set; get; }
        

    	    /// <summary>
    	    /// 任务登记时间
    	    /// </summary>
    	    public DateTime? irCreateTimeEnd { set; get; }
        

    	    /// <summary>
    	    /// 处理情况信息，描述处理过程中的详细问题
    	    /// </summary>
    	    public string irHandleMessage { set; get; }
        

    	    /// <summary>
    	    /// 处理情况信息，描述处理过程中的详细问题
    	    /// </summary>
    	    public string irHandleMessageLike { set; get; }
        

    	    /// <summary>
    	    /// 处理状态,0:未处理,1:处理完毕,2:处理失败
    	    /// </summary>
    	    public int? irHandleState { set; get; }
        

    	    /// <summary>
    	    /// 处理状态,0:未处理,1:处理完毕,2:处理失败
    	    /// </summary>
    	    public int? irHandleStateStart { set; get; }
        

    	    /// <summary>
    	    /// 处理状态,0:未处理,1:处理完毕,2:处理失败
    	    /// </summary>
    	    public int? irHandleStateEnd { set; get; }
        

    	    /// <summary>
    	    /// 处理时间
    	    /// </summary>
    	    public DateTime? irHandleTime { set; get; }
        

    	    /// <summary>
    	    /// 处理时间
    	    /// </summary>
    	    public DateTime? irHandleTimeStart { set; get; }
        

    	    /// <summary>
    	    /// 处理时间
    	    /// </summary>
    	    public DateTime? irHandleTimeEnd { set; get; }
        

    	    /// <summary>
    	    /// 任务单号(办事易全送过来保证必须唯一)
    	    /// </summary>
    	    public string irOrderNo { set; get; }
        

    	    /// <summary>
    	    /// 任务单号(办事易全送过来保证必须唯一)
    	    /// </summary>
    	    public string irOrderNoLike { set; get; }
        

    	    /// <summary>
    	    /// 推送状态, 0:未推送, 1:推送成功, 2:推送失败
    	    /// </summary>
    	    public int? irPushState { set; get; }
        

    	    /// <summary>
    	    /// 推送状态, 0:未推送, 1:推送成功, 2:推送失败
    	    /// </summary>
    	    public int? irPushStateStart { set; get; }
        

    	    /// <summary>
    	    /// 推送状态, 0:未推送, 1:推送成功, 2:推送失败
    	    /// </summary>
    	    public int? irPushStateEnd { set; get; }
        

    	    /// <summary>
    	    /// 推送时间
    	    /// </summary>
    	    public DateTime? irPushTime { set; get; }
        

    	    /// <summary>
    	    /// 推送时间
    	    /// </summary>
    	    public DateTime? irPushTimeStart { set; get; }
        

    	    /// <summary>
    	    /// 推送时间
    	    /// </summary>
    	    public DateTime? irPushTimeEnd { set; get; }
        

    	    /// <summary>
    	    /// 二维码图片相对路径
    	    /// </summary>
    	    public string irQrCodeImagePath { set; get; }
        

    	    /// <summary>
    	    /// 二维码图片相对路径
    	    /// </summary>
    	    public string irQrCodeImagePathLike { set; get; }
        

    	    /// <summary>
    	    /// 支付备注
    	    /// </summary>
    	    public string irRemark { set; get; }
        

    	    /// <summary>
    	    /// 支付备注
    	    /// </summary>
    	    public string irRemarkLike { set; get; }
        

    	    /// <summary>
    	    /// 处理结果截图的json列表
    	    /// </summary>
    	    public string irReportPicPathJson { set; get; }
        

    	    /// <summary>
    	    /// 处理结果截图的json列表
    	    /// </summary>
    	    public string irReportPicPathJsonLike { set; get; }
        

    	    /// <summary>
    	    /// 机器人编号
    	    /// </summary>
    	    public string irRobotId { set; get; }
        

    	    /// <summary>
    	    /// 机器人编号
    	    /// </summary>
    	    public string irRobotIdLike { set; get; }
        

    	    /// <summary>
    	    /// 办事易的ScanPayNotify接口返回结果
    	    /// </summary>
    	    public string irScanPayNotifyRet { set; get; }
        

    	    /// <summary>
    	    /// 办事易的ScanPayNotify接口返回结果
    	    /// </summary>
    	    public string irScanPayNotifyRetLike { set; get; }
        

    	    /// <summary>
    	    /// 收款金额，单位：分
    	    /// </summary>
    	    public int? irTakeMoney { set; get; }
        

    	    /// <summary>
    	    /// 收款金额，单位：分
    	    /// </summary>
    	    public int? irTakeMoneyStart { set; get; }
        

    	    /// <summary>
    	    /// 收款金额，单位：分
    	    /// </summary>
    	    public int? irTakeMoneyEnd { set; get; }
        
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