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
    public partial sealed class IRobotErrorMsgParams
    {


    	    /// <summary>
    	    /// 错误编号，错误信息的主键
    	    /// </summary>
    	    public string IEErrNo { set; get; }
        

    	    /// <summary>
    	    /// 错误编号，错误信息的主键
    	    /// </summary>
    	    public string IEErrNoLike { set; get; }
        

    	    /// <summary>
    	    /// 错误记录创建日期
    	    /// </summary>
    	    public DateTime? IECreateDate { set; get; }
        

    	    /// <summary>
    	    /// 错误记录创建日期
    	    /// </summary>
    	    public DateTime? IECreateDateStart { set; get; }
        

    	    /// <summary>
    	    /// 错误记录创建日期
    	    /// </summary>
    	    public DateTime? IECreateDateEnd { set; get; }
        

    	    /// <summary>
    	    /// 发生错误的订单
    	    /// </summary>
    	    public string IEErrOrderNo { set; get; }
        

    	    /// <summary>
    	    /// 发生错误的订单
    	    /// </summary>
    	    public string IEErrOrderNoLike { set; get; }
        

    	    /// <summary>
    	    /// 发生错误的机器人ID
    	    /// </summary>
    	    public string IEErrRobotId { set; get; }
        

    	    /// <summary>
    	    /// 发生错误的机器人ID
    	    /// </summary>
    	    public string IEErrRobotIdLike { set; get; }
        

    	    /// <summary>
    	    /// 错误截图路径的json字符串
    	    /// </summary>
    	    public string IEErrPic { set; get; }
        

    	    /// <summary>
    	    /// 错误截图路径的json字符串
    	    /// </summary>
    	    public string IEErrPicLike { set; get; }
        

    	    /// <summary>
    	    /// 错误信息的具体内容
    	    /// </summary>
    	    public string IEErrContext { set; get; }
        

    	    /// <summary>
    	    /// 错误信息的具体内容
    	    /// </summary>
    	    public string IEErrContextLike { set; get; }
        

    	    /// <summary>
    	    /// 处理状态：0 未处理、1 已处理
    	    /// </summary>
    	    public int? IEHandleStatus { set; get; }
        

    	    /// <summary>
    	    /// 处理状态：0 未处理、1 已处理
    	    /// </summary>
    	    public int? IEHandleStatusStart { set; get; }
        

    	    /// <summary>
    	    /// 处理状态：0 未处理、1 已处理
    	    /// </summary>
    	    public int? IEHandleStatusEnd { set; get; }
        

    	    /// <summary>
    	    /// 处理状态：0 未处理、1 已处理
    	    /// </summary>
    	    public int? IEHandleStatusChange { set; get; }
        
		/// <summary>
	    /// 升序排序
	    /// </summary>
        public IRobotErrorMsgOrderBy orderByAsc { set; get; }

		/// <summary>
	    /// 降序排序
	    /// </summary>
        public IRobotErrorMsgOrderBy orderByDesc { set; get; }
    }
}