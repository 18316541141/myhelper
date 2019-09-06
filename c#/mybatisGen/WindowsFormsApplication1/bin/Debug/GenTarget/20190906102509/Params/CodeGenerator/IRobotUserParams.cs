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
    public sealed partial class IRobotUserParams
    {


    	    /// <summary>
    	    /// 主键id
    	    /// </summary>
    	    public long? IUId { set; get; }
        

    	    /// <summary>
    	    /// 用户名
    	    /// </summary>
    	    public string IUUsername { set; get; }
        

    	    /// <summary>
    	    /// 用户名
    	    /// </summary>
    	    public string IUUsernameLike { set; get; }
        

    	    /// <summary>
    	    /// 密码
    	    /// </summary>
    	    public string IUPassword { set; get; }
        

    	    /// <summary>
    	    /// 密码
    	    /// </summary>
    	    public string IUPasswordLike { set; get; }
        

    	    /// <summary>
    	    /// 签名的唯一标识
    	    /// </summary>
    	    public string IUSignKey { set; get; }
        

    	    /// <summary>
    	    /// 签名的唯一标识
    	    /// </summary>
    	    public string IUSignKeyLike { set; get; }
        

    	    /// <summary>
    	    /// 签名密钥
    	    /// </summary>
    	    public string IUSignSecret { set; get; }
        

    	    /// <summary>
    	    /// 签名密钥
    	    /// </summary>
    	    public string IUSignSecretLike { set; get; }
        

    	    /// <summary>
    	    /// 用户创建日期
    	    /// </summary>
    	    public DateTime? IUCreateDate { set; get; }
        

    	    /// <summary>
    	    /// 用户创建日期
    	    /// </summary>
    	    public DateTime? IUCreateDateStart { set; get; }
        

    	    /// <summary>
    	    /// 用户创建日期
    	    /// </summary>
    	    public DateTime? IUCreateDateEnd { set; get; }
        
		/// <summary>
	    /// 升序排序
	    /// </summary>
        public IRobotUserOrderBy orderByAsc { set; get; }

		/// <summary>
	    /// 降序排序
	    /// </summary>
        public IRobotUserOrderBy orderByDesc { set; get; }
    }
}