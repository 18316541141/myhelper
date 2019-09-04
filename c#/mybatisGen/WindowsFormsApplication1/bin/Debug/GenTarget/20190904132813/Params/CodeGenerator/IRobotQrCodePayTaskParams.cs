using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Params;

namespace WebApplication1.Params
{
    /// <summary>
    /// 
    /// </summary>
    public partial class IRobotQrCodePayTaskParams
    {


    	    /// <summary>
    	    /// 
    	    /// </summary>
    	    public long? IRTaskID { set; get; }
        

    	    /// <summary>
    	    /// 
    	    /// </summary>
    	    public long? IRTaskIDStart { set; get; }
        

    	    /// <summary>
    	    /// 
    	    /// </summary>
    	    public long? IRTaskIDEnd { set; get; }
        

    	    /// <summary>
    	    /// 
    	    /// </summary>
    	    public long? IRTaskIDChange { set; get; }
        

    	    /// <summary>
    	    /// 
    	    /// </summary>
    	    public string IROrderNo { set; get; }
        

    	    /// <summary>
    	    /// 
    	    /// </summary>
    	    public string IROrderNoLike { set; get; }
        

    	    /// <summary>
    	    /// 
    	    /// </summary>
    	    public int? IRTakeMoney { set; get; }
        

    	    /// <summary>
    	    /// 
    	    /// </summary>
    	    public int? IRTakeMoneyStart { set; get; }
        

    	    /// <summary>
    	    /// 
    	    /// </summary>
    	    public int? IRTakeMoneyEnd { set; get; }
        

    	    /// <summary>
    	    /// 
    	    /// </summary>
    	    public int? IRTakeMoneyChange { set; get; }
        

    	    /// <summary>
    	    /// 
    	    /// </summary>
    	    public int? IRPushState { set; get; }
        

    	    /// <summary>
    	    /// 
    	    /// </summary>
    	    public int? IRPushStateStart { set; get; }
        

    	    /// <summary>
    	    /// 
    	    /// </summary>
    	    public int? IRPushStateEnd { set; get; }
        

    	    /// <summary>
    	    /// 
    	    /// </summary>
    	    public int? IRPushStateChange { set; get; }
        

    	    /// <summary>
    	    /// 
    	    /// </summary>
    	    public string IRScanPayNotifyUrl { set; get; }
        

    	    /// <summary>
    	    /// 
    	    /// </summary>
    	    public string IRScanPayNotifyUrlLike { set; get; }
        

    	    /// <summary>
    	    /// 
    	    /// </summary>
    	    public string IRScanPayNotifyRet { set; get; }
        

    	    /// <summary>
    	    /// 
    	    /// </summary>
    	    public string IRScanPayNotifyRetLike { set; get; }
        

    	    /// <summary>
    	    /// 
    	    /// </summary>
    	    public int? IRHandleState { set; get; }
        

    	    /// <summary>
    	    /// 
    	    /// </summary>
    	    public int? IRHandleStateStart { set; get; }
        

    	    /// <summary>
    	    /// 
    	    /// </summary>
    	    public int? IRHandleStateEnd { set; get; }
        

    	    /// <summary>
    	    /// 
    	    /// </summary>
    	    public int? IRHandleStateChange { set; get; }
        

    	    /// <summary>
    	    /// 
    	    /// </summary>
    	    public string IRQrCodeImagePath { set; get; }
        

    	    /// <summary>
    	    /// 
    	    /// </summary>
    	    public string IRQrCodeImagePathLike { set; get; }
        

    	    /// <summary>
    	    /// 
    	    /// </summary>
    	    public int? IRBsyNotifyState { set; get; }
        

    	    /// <summary>
    	    /// 
    	    /// </summary>
    	    public int? IRBsyNotifyStateStart { set; get; }
        

    	    /// <summary>
    	    /// 
    	    /// </summary>
    	    public int? IRBsyNotifyStateEnd { set; get; }
        

    	    /// <summary>
    	    /// 
    	    /// </summary>
    	    public int? IRBsyNotifyStateChange { set; get; }
        

    	    /// <summary>
    	    /// 
    	    /// </summary>
    	    public int? IRSendMoneyNotifyState { set; get; }
        

    	    /// <summary>
    	    /// 
    	    /// </summary>
    	    public int? IRSendMoneyNotifyStateStart { set; get; }
        

    	    /// <summary>
    	    /// 
    	    /// </summary>
    	    public int? IRSendMoneyNotifyStateEnd { set; get; }
        

    	    /// <summary>
    	    /// 
    	    /// </summary>
    	    public int? IRSendMoneyNotifyStateChange { set; get; }
        

    	    /// <summary>
    	    /// 
    	    /// </summary>
    	    public string IRReportPicPath { set; get; }
        

    	    /// <summary>
    	    /// 
    	    /// </summary>
    	    public string IRReportPicPathLike { set; get; }
        

    	    /// <summary>
    	    /// 
    	    /// </summary>
    	    public string IRRemark { set; get; }
        

    	    /// <summary>
    	    /// 
    	    /// </summary>
    	    public string IRRemarkLike { set; get; }
        
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