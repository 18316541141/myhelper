using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Entity;
using WebApplication1.Entity.Common;
using Webdiyer.WebControls.Mvc;
namespace CommonHelper.Helper.CommonExtensions
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
                PageDataList = pagedList.ToList(),
                CurrentPageIndex = pagedList.CurrentPageIndex,
                EndItemIndex = pagedList.EndItemIndex,
                PageSize = pagedList.PageSize,
                StartItemIndex = pagedList.StartItemIndex,
                TotalItemCount = pagedList.TotalItemCount,
                TotalPageCount = pagedList.TotalPageCount
            };
        }
    }
}