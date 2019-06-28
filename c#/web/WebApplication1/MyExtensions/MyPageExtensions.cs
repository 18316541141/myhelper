using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Entity;
using Webdiyer.WebControls.Mvc;

namespace WebApplication1.MyExtensions
{
    /// <summary>
    /// 自定义的page扩展类
    /// </summary>
    public static class MyPageExtensions
    {
        /// <summary>
        /// 转化为自定义的page对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="allItems"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static MyPagedList<T> ToMyPagedList<T>(this IQueryable<T> allItems, int pageIndex, int pageSize)
        {
            PagedList<T> pagedList = allItems.ToPagedList(pageIndex, pageSize);
            return new MyPagedList<T>
            {
                pageDataList = pagedList.ToList(),
                currentPageIndex = pagedList.CurrentPageIndex,
                endItemIndex = pagedList.EndItemIndex,
                pageSize = pagedList.PageSize,
                startItemIndex = pagedList.StartItemIndex,
                totalItemCount = pagedList.TotalItemCount,
                totalPageCount = pagedList.TotalPageCount
            };
        }
    }
}