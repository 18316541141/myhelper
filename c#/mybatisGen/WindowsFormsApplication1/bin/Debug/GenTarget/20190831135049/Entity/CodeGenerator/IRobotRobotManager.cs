using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace WebApplication1.Entity
{
	/// <summary>
	/// 支付机器人管理器
	/// </summary>
    public partial class IRobotRobotManager
    {
        public IRobotRobotManager() { }

			/// <summary>
			/// 自增id
			/// </summary>
			public virtual int? IRMID { set; get; }
			/// <summary>
			/// 机器人id（6位数）
			/// </summary>
			public virtual string IRMRobotId { set; get; }
			/// <summary>
			/// 机器人状态, 1:启用,0:停用
			/// </summary>
			public virtual int? IRMRobotState { set; get; }
			/// <summary>
			/// 机器人当前运行日期
			/// </summary>
			public virtual DateTime? IRMRuningTime { set; get; }
			/// <summary>
			/// 最大支付笔数
			/// </summary>
			public virtual int? IRMMaxPayTradeCount { set; get; }
			/// <summary>
			/// 当前累计支付笔数
			/// </summary>
			public virtual int? IRMCurrentPayCount { set; get; }
			/// <summary>
			/// 当前可支付余额
			/// </summary>
			public virtual decimal? IRMBalance { set; get; }
			/// <summary>
			/// 机器人客户端ip
			/// </summary>
			public virtual string IRMIP { set; get; }
			/// <summary>
			/// 机器人信息登记时间
			/// </summary>
			public virtual DateTime? IRMCreateTime { set; get; }
			/// <summary>
			/// 该机器人的TeamView伙伴ID
			/// </summary>
			public virtual string IRMTeamViewId { set; get; }
			/// <summary>
			/// 该机器人的TeamView密码
			/// </summary>
			public virtual string IRMTeamViewPassword { set; get; }
			/// <summary>
			/// 该机器人的向日葵ID
			/// </summary>
			public virtual string IRMSunflowerId { set; get; }
			/// <summary>
			/// 该机器人的向日葵ID
			/// </summary>
			public virtual string IRMSunflowerPassword { set; get; }
			/// <summary>
			/// 银行卡号最后四位
			/// </summary>
			public virtual string IRMBankCardTailNum { set; get; }
			/// <summary>
			/// 银行名称
			/// </summary>
			public virtual string IRMBankCardName { set; get; }
			/// <summary>
			/// 提示充值的最低余额
			/// </summary>
			public virtual decimal? IRMMinBalance { set; get; }
			/// <summary>
			/// 支付密码
			/// </summary>
			public virtual string IRMPayPassword { set; get; }
			/// <summary>
			/// 支付成功时，通知办事易支付成功的url
			/// </summary>
			public virtual string IRMScanPayNotifyUrl { set; get; }
			/// <summary>
			/// 机器人使用的微信账号
			/// </summary>
			public virtual string IRMWxUsername { set; get; }
			/// <summary>
			/// 机器人使用的微信密码
			/// </summary>
			public virtual string IRMWxPassword { set; get; }
			/// <summary>
			/// 刷新余额（0：不刷新）、（1：马上刷新）；该值设置为1时，机器人会尽可能马上刷新余额。
			/// </summary>
			public virtual string IRMRefreshBalance { set; get; }
    }
}