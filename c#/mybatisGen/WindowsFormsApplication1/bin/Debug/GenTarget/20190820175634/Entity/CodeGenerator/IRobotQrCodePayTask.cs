﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace WebApplication1.Entity
{
	/// <summary>
	/// 
	/// </summary>
    public partial class IRobotQrCodePayTask
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
    }
}