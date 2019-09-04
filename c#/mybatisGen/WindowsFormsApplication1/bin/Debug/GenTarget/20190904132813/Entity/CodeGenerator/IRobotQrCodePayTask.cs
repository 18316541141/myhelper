using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebApplication1.Entity
{
	/// <summary>
	/// 
	/// </summary>
    public partial class IRobotQrCodePayTask //: IEntity
    {
        public IRobotQrCodePayTask() { }

			/// <summary>
			/// 
			/// </summary>
			public virtual long? IRTaskID { set; get; }
			/// <summary>
			/// 
			/// </summary>
			public virtual string IROrderNo { set; get; }
			/// <summary>
			/// 
			/// </summary>
			public virtual int? IRTakeMoney { set; get; }
			/// <summary>
			/// 
			/// </summary>
			public virtual int? IRPushState { set; get; }
			/// <summary>
			/// 
			/// </summary>
			public virtual string IRScanPayNotifyUrl { set; get; }
			/// <summary>
			/// 
			/// </summary>
			public virtual string IRScanPayNotifyRet { set; get; }
			/// <summary>
			/// 
			/// </summary>
			public virtual int? IRHandleState { set; get; }
			/// <summary>
			/// 
			/// </summary>
			public virtual string IRQrCodeImagePath { set; get; }
			/// <summary>
			/// 
			/// </summary>
			public virtual int? IRBsyNotifyState { set; get; }
			/// <summary>
			/// 
			/// </summary>
			public virtual int? IRSendMoneyNotifyState { set; get; }
			/// <summary>
			/// 
			/// </summary>
			public virtual string IRReportPicPath { set; get; }
			/// <summary>
			/// 
			/// </summary>
			public virtual string IRRemark { set; get; }
	/*
		[NotMapped]
		public long? Key
		{
			set { IRTaskID = value; }
			get { return IRTaskID; }
		}

		public string TableName()
		{
			return "IRobot_QrCodePayTask";
		}
	*/
    }
}