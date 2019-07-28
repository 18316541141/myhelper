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
    public partial class IgnoreStationParams
    {


    	    /// <summary>
    	    /// 
    	    /// </summary>
    	    public int? Id { set; get; }
        

    	    /// <summary>
    	    /// 
    	    /// </summary>
    	    public int? IdStart { set; get; }
        

    	    /// <summary>
    	    /// 
    	    /// </summary>
    	    public int? IdEnd { set; get; }
        

    	    /// <summary>
    	    /// 
    	    /// </summary>
    	    public string Code { set; get; }
        

    	    /// <summary>
    	    /// 
    	    /// </summary>
    	    public string CodeLike { set; get; }
        

    	    /// <summary>
    	    /// 
    	    /// </summary>
    	    public string Name { set; get; }
        

    	    /// <summary>
    	    /// 
    	    /// </summary>
    	    public string NameLike { set; get; }
        

    	    /// <summary>
    	    /// 
    	    /// </summary>
    	    public string NoDataCollectDate { set; get; }
        

    	    /// <summary>
    	    /// 
    	    /// </summary>
    	    public string NoDataCollectDateLike { set; get; }
        
		/// <summary>
	    /// 升序排序
	    /// </summary>
        public IgnoreStationOrderBy orderByAsc { set; get; }

		/// <summary>
	    /// 降序排序
	    /// </summary>
        public IgnoreStationOrderBy orderByDesc { set; get; }
    }
}