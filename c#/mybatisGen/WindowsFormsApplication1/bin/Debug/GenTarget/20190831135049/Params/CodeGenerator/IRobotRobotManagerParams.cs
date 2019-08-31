using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Params;

namespace WebApplication1.Params
{
    /// <summary>
    /// 支付机器人管理器
    /// </summary>
    public partial class IRobotRobotManagerParams
    {


    	    /// <summary>
    	    /// 自增id
    	    /// </summary>
    	    public int? IRMID { set; get; }
        

    	    /// <summary>
    	    /// 自增id
    	    /// </summary>
    	    public int? IRMIDStart { set; get; }
        

    	    /// <summary>
    	    /// 自增id
    	    /// </summary>
    	    public int? IRMIDEnd { set; get; }
        

    	    /// <summary>
    	    /// 自增id
    	    /// </summary>
    	    public int? IRMIDChange { set; get; }
        

    	    /// <summary>
    	    /// 机器人id（6位数）
    	    /// </summary>
    	    public string IRMRobotId { set; get; }
        

    	    /// <summary>
    	    /// 机器人id（6位数）
    	    /// </summary>
    	    public string IRMRobotIdLike { set; get; }
        

    	    /// <summary>
    	    /// 机器人状态, 1:启用,0:停用
    	    /// </summary>
    	    public int? IRMRobotState { set; get; }
        

    	    /// <summary>
    	    /// 机器人状态, 1:启用,0:停用
    	    /// </summary>
    	    public int? IRMRobotStateStart { set; get; }
        

    	    /// <summary>
    	    /// 机器人状态, 1:启用,0:停用
    	    /// </summary>
    	    public int? IRMRobotStateEnd { set; get; }
        

    	    /// <summary>
    	    /// 机器人状态, 1:启用,0:停用
    	    /// </summary>
    	    public int? IRMRobotStateChange { set; get; }
        

    	    /// <summary>
    	    /// 机器人当前运行日期
    	    /// </summary>
    	    public DateTime? IRMRuningTime { set; get; }
        

    	    /// <summary>
    	    /// 机器人当前运行日期
    	    /// </summary>
    	    public DateTime? IRMRuningTimeStart { set; get; }
        

    	    /// <summary>
    	    /// 机器人当前运行日期
    	    /// </summary>
    	    public DateTime? IRMRuningTimeEnd { set; get; }
        

    	    /// <summary>
    	    /// 最大支付笔数
    	    /// </summary>
    	    public int? IRMMaxPayTradeCount { set; get; }
        

    	    /// <summary>
    	    /// 最大支付笔数
    	    /// </summary>
    	    public int? IRMMaxPayTradeCountStart { set; get; }
        

    	    /// <summary>
    	    /// 最大支付笔数
    	    /// </summary>
    	    public int? IRMMaxPayTradeCountEnd { set; get; }
        

    	    /// <summary>
    	    /// 最大支付笔数
    	    /// </summary>
    	    public int? IRMMaxPayTradeCountChange { set; get; }
        

    	    /// <summary>
    	    /// 当前累计支付笔数
    	    /// </summary>
    	    public int? IRMCurrentPayCount { set; get; }
        

    	    /// <summary>
    	    /// 当前累计支付笔数
    	    /// </summary>
    	    public int? IRMCurrentPayCountStart { set; get; }
        

    	    /// <summary>
    	    /// 当前累计支付笔数
    	    /// </summary>
    	    public int? IRMCurrentPayCountEnd { set; get; }
        

    	    /// <summary>
    	    /// 当前累计支付笔数
    	    /// </summary>
    	    public int? IRMCurrentPayCountChange { set; get; }
        

    	    /// <summary>
    	    /// 当前可支付余额
    	    /// </summary>
    	    public decimal? IRMBalance { set; get; }
        

    	    /// <summary>
    	    /// 当前可支付余额
    	    /// </summary>
    	    public decimal? IRMBalanceStart { set; get; }
        

    	    /// <summary>
    	    /// 当前可支付余额
    	    /// </summary>
    	    public decimal? IRMBalanceEnd { set; get; }
        

    	    /// <summary>
    	    /// 当前可支付余额
    	    /// </summary>
    	    public decimal? IRMBalanceChange { set; get; }
        

    	    /// <summary>
    	    /// 机器人客户端ip
    	    /// </summary>
    	    public string IRMIP { set; get; }
        

    	    /// <summary>
    	    /// 机器人客户端ip
    	    /// </summary>
    	    public string IRMIPLike { set; get; }
        

    	    /// <summary>
    	    /// 机器人信息登记时间
    	    /// </summary>
    	    public DateTime? IRMCreateTime { set; get; }
        

    	    /// <summary>
    	    /// 机器人信息登记时间
    	    /// </summary>
    	    public DateTime? IRMCreateTimeStart { set; get; }
        

    	    /// <summary>
    	    /// 机器人信息登记时间
    	    /// </summary>
    	    public DateTime? IRMCreateTimeEnd { set; get; }
        

    	    /// <summary>
    	    /// 该机器人的TeamView伙伴ID
    	    /// </summary>
    	    public string IRMTeamViewId { set; get; }
        

    	    /// <summary>
    	    /// 该机器人的TeamView伙伴ID
    	    /// </summary>
    	    public string IRMTeamViewIdLike { set; get; }
        

    	    /// <summary>
    	    /// 该机器人的TeamView密码
    	    /// </summary>
    	    public string IRMTeamViewPassword { set; get; }
        

    	    /// <summary>
    	    /// 该机器人的TeamView密码
    	    /// </summary>
    	    public string IRMTeamViewPasswordLike { set; get; }
        

    	    /// <summary>
    	    /// 该机器人的向日葵ID
    	    /// </summary>
    	    public string IRMSunflowerId { set; get; }
        

    	    /// <summary>
    	    /// 该机器人的向日葵ID
    	    /// </summary>
    	    public string IRMSunflowerIdLike { set; get; }
        

    	    /// <summary>
    	    /// 该机器人的向日葵ID
    	    /// </summary>
    	    public string IRMSunflowerPassword { set; get; }
        

    	    /// <summary>
    	    /// 该机器人的向日葵ID
    	    /// </summary>
    	    public string IRMSunflowerPasswordLike { set; get; }
        

    	    /// <summary>
    	    /// 银行卡号最后四位
    	    /// </summary>
    	    public string IRMBankCardTailNum { set; get; }
        

    	    /// <summary>
    	    /// 银行卡号最后四位
    	    /// </summary>
    	    public string IRMBankCardTailNumLike { set; get; }
        

    	    /// <summary>
    	    /// 银行名称
    	    /// </summary>
    	    public string IRMBankCardName { set; get; }
        

    	    /// <summary>
    	    /// 银行名称
    	    /// </summary>
    	    public string IRMBankCardNameLike { set; get; }
        

    	    /// <summary>
    	    /// 提示充值的最低余额
    	    /// </summary>
    	    public decimal? IRMMinBalance { set; get; }
        

    	    /// <summary>
    	    /// 提示充值的最低余额
    	    /// </summary>
    	    public decimal? IRMMinBalanceStart { set; get; }
        

    	    /// <summary>
    	    /// 提示充值的最低余额
    	    /// </summary>
    	    public decimal? IRMMinBalanceEnd { set; get; }
        

    	    /// <summary>
    	    /// 提示充值的最低余额
    	    /// </summary>
    	    public decimal? IRMMinBalanceChange { set; get; }
        

    	    /// <summary>
    	    /// 支付密码
    	    /// </summary>
    	    public string IRMPayPassword { set; get; }
        

    	    /// <summary>
    	    /// 支付密码
    	    /// </summary>
    	    public string IRMPayPasswordLike { set; get; }
        

    	    /// <summary>
    	    /// 支付成功时，通知办事易支付成功的url
    	    /// </summary>
    	    public string IRMScanPayNotifyUrl { set; get; }
        

    	    /// <summary>
    	    /// 支付成功时，通知办事易支付成功的url
    	    /// </summary>
    	    public string IRMScanPayNotifyUrlLike { set; get; }
        

    	    /// <summary>
    	    /// 机器人使用的微信账号
    	    /// </summary>
    	    public string IRMWxUsername { set; get; }
        

    	    /// <summary>
    	    /// 机器人使用的微信账号
    	    /// </summary>
    	    public string IRMWxUsernameLike { set; get; }
        

    	    /// <summary>
    	    /// 机器人使用的微信密码
    	    /// </summary>
    	    public string IRMWxPassword { set; get; }
        

    	    /// <summary>
    	    /// 机器人使用的微信密码
    	    /// </summary>
    	    public string IRMWxPasswordLike { set; get; }
        

    	    /// <summary>
    	    /// 刷新余额（0：不刷新）、（1：马上刷新）；该值设置为1时，机器人会尽可能马上刷新余额。
    	    /// </summary>
    	    public string IRMRefreshBalance { set; get; }
        

    	    /// <summary>
    	    /// 刷新余额（0：不刷新）、（1：马上刷新）；该值设置为1时，机器人会尽可能马上刷新余额。
    	    /// </summary>
    	    public string IRMRefreshBalanceLike { set; get; }
        
		/// <summary>
	    /// 升序排序
	    /// </summary>
        public IRobotRobotManagerOrderBy orderByAsc { set; get; }

		/// <summary>
	    /// 降序排序
	    /// </summary>
        public IRobotRobotManagerOrderBy orderByDesc { set; get; }
    }
}