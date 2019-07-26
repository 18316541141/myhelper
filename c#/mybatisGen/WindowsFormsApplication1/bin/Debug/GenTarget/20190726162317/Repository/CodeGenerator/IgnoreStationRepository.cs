using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication1.Entity;
using WebApplication1.MyExtensions;
using WebApplication1.Params;
namespace WebApplication1.Repository
{
    public partial class IgnoreStationRepository : BaseRepository<IgnoreStation>
    {
		/// <summary>
        /// 通用的设置查询参数方法，只有在参数不为null的情况下才会设置，
		/// 满足项目中常用的模糊查询，分页查询等功能
        /// </summary>
        /// <param name="query">待设置参数的query对象</param>
        /// <param name="eqArgs">装载查询参数的实体类</param>
		/// <param name="neqArgs">装载不等于查询参数的实体类</param>
        /// <returns>返回设置好查询参数的query对象</returns>
        IQueryable<IgnoreStation> Query(IQueryable<IgnoreStation> query, IgnoreStationParams eqArgs, IgnoreStationParams neqArgs = null)
        {
            if (eqArgs != null)
            {

							if (eqArgs.Id != null)
							{
								query = query.Where(a => a.Id == eqArgs.Id);
							}

							if (eqArgs.Code != null)
							{
								query = query.Where(a => a.Code == eqArgs.Code);
							}

							if (!string.IsNullOrEmpty(eqArgs.CodeLike))
			                {
			                    query = query.Where(a => a.Code.Contains(eqArgs.CodeLike));
			                }
							if (eqArgs.Name != null)
							{
								query = query.Where(a => a.Name == eqArgs.Name);
							}

							if (!string.IsNullOrEmpty(eqArgs.NameLike))
			                {
			                    query = query.Where(a => a.Name.Contains(eqArgs.NameLike));
			                }
							if (eqArgs.NoDataCollectDate != null)
							{
								query = query.Where(a => a.NoDataCollectDate == eqArgs.NoDataCollectDate);
							}

							if (!string.IsNullOrEmpty(eqArgs.NoDataCollectDateLike))
			                {
			                    query = query.Where(a => a.NoDataCollectDate.Contains(eqArgs.NoDataCollectDateLike));
			                }
            }
			if(neqArgs != null){

							if (neqArgs.Id != null)
							{
								query = query.Where(a => a.Id != neqArgs.Id);
							}

							if (neqArgs.Code != null)
							{
								query = query.Where(a => a.Code != neqArgs.Code);
							}

							if (!string.IsNullOrEmpty(neqArgs.CodeLike))
			                {
								query = query.Where(a => !a.Code.Contains(neqArgs.CodeLike));
							}
							if (neqArgs.Name != null)
							{
								query = query.Where(a => a.Name != neqArgs.Name);
							}

							if (!string.IsNullOrEmpty(neqArgs.NameLike))
			                {
								query = query.Where(a => !a.Name.Contains(neqArgs.NameLike));
							}
							if (neqArgs.NoDataCollectDate != null)
							{
								query = query.Where(a => a.NoDataCollectDate != neqArgs.NoDataCollectDate);
							}

							if (!string.IsNullOrEmpty(neqArgs.NoDataCollectDateLike))
			                {
								query = query.Where(a => !a.NoDataCollectDate.Contains(neqArgs.NoDataCollectDateLike));
							}
			}
            return query;
        }

		/// <summary>
        /// 升序排序的查询参数设置，当对应字段的升序设置为true时才会对
		/// 该字段升序。
        /// </summary>
        /// <param name="query">待设置升序参数的query对象</param>
        /// <param name="orderBy">装载升序参数的实体类</param>
        /// <returns>返回设置好升序参数的query对象</returns>
        IQueryable<IgnoreStation> OrderByAsc(IQueryable<IgnoreStation> query, IgnoreStationOrderBy orderBy)
        {
            if (orderBy != null)
            {

							if (orderBy.Id) { query = query.OrderBy(a => a.Id); }
							if (orderBy.Code) { query = query.OrderBy(a => a.Code); }
							if (orderBy.Name) { query = query.OrderBy(a => a.Name); }
							if (orderBy.NoDataCollectDate) { query = query.OrderBy(a => a.NoDataCollectDate); }
            }
            return query;
        }

		/// <summary>
        /// 降序排序的查询参数设置，当对应字段的升序设置为true时才会对
		/// 该字段降序。
        /// </summary>
        /// <param name="query">待设置降序参数的query对象</param>
        /// <param name="orderBy">装载降序参数的实体类</param>
        /// <returns>返回设置好降序参数的query对象</returns>
        IQueryable<IgnoreStation> OrderByDesc(IQueryable<IgnoreStation> query, IgnoreStationOrderBy orderBy)
        {
            if (orderBy != null)
            {

							if (orderBy.Id) { query = query.OrderByDescending(a => a.Id); }
							if (orderBy.Code) { query = query.OrderByDescending(a => a.Code); }
							if (orderBy.Name) { query = query.OrderByDescending(a => a.Name); }
							if (orderBy.NoDataCollectDate) { query = query.OrderByDescending(a => a.NoDataCollectDate); }
            }
            else
            {
                query = query.OrderByDescending(a => a.Id);
            }
            return query;
        }

		/// <summary>
        /// 查询pageSize超过1000的分页查询，因为pageSize过大会导致查询超时，为避免这种情况，
		/// 必须使用该方法进行分页查询。
        /// </summary>
        /// <param name="eqArgs">查询参数，不为null时会作为查询参数</param>
        /// <param name="pageIndex">查询页码</param>
        /// <param name="pageSize">每页显示数据量</param>
		/// <param name="neqArgs">不等查询参数，不为null时会作为不等查询参数</param>
        /// <returns>返回分页查询结果</returns>
        public MyPagedList<IgnoreStation> BigPageList(IgnoreStationParams eqArgs, int pageIndex, int pageSize, IgnoreStationParams neqArgs = null)
        {
            using (MyDbContext myDbContext = new MyDbContext())
            {
                IQueryable<IgnoreStation> query = Query(myDbContext.IgnoreStations.AsNoTracking().AsQueryable(), eqArgs, neqArgs);
                query = OrderByAsc(query, eqArgs.orderByAsc);
                query = OrderByDesc(query, eqArgs.orderByDesc);
                int totalItemCount = query.Count();
                query = query.Skip((pageIndex-1) * pageSize);
                List<IgnoreStation> pageDataList = new List<IgnoreStation>();
                for (int i=0, partCount = (pageSize - pageSize % 1000) / pageSize + 1; i<partCount ;i++)
                {
                    pageDataList.AddRange(query.Skip(i * 1000).Take(1000).ToList());
                }
                return new MyPagedList<IgnoreStation>
                {
                    currentPageIndex = pageIndex,
                    pageDataList = pageDataList,
                    totalItemCount = totalItemCount,
                    pageSize = pageSize,
                    totalPageCount = (totalItemCount - totalItemCount % pageSize) / pageSize + 1,
                    startItemIndex = (pageSize - 1) * pageIndex + 1,
                    endItemIndex = pageSize * pageIndex
                };
            }
        }

		/// <summary>
        /// 普通的分页查询功能，pageSize不宜过大，如果pageSize大于1000，使用：BigPageList
        /// </summary>
        /// <param name="eqArgs">查询参数，不为null时会作为查询参数</param>
        /// <param name="pageIndex">查询页码</param>
        /// <param name="pageSize">每页显示数据量</param>
		/// <param name="neqArgs">不等查询参数，不为null时会作为不等查询参数</param>
        /// <returns>返回分页查询结果</returns>
        public MyPagedList<IgnoreStation> PageList(IgnoreStationParams eqArgs, int pageIndex, int pageSize,IgnoreStationParams neqArgs = null)
        {
            using (MyDbContext myDbContext = new MyDbContext())
            {
                IQueryable<IgnoreStation> query = Query(myDbContext.IgnoreStations.AsNoTracking().AsQueryable(), eqArgs, neqArgs);
                query = OrderByAsc(query, eqArgs.orderByAsc);
                query = OrderByDesc(query, eqArgs.orderByDesc);
                return query.ToMyPagedList(pageIndex, pageSize);
            }
        }

		/// <summary>
        /// 查询符合查询条件的数据量
        /// </summary>
        /// <param name="eqArgs">查询参数，不为null时会作为查询参数</param>
		/// <param name="neqArgs">不等查询参数，不为null时会作为不等查询参数</param>
        /// <returns>返回符合查询条件的数据量</returns>
        public int Count(IgnoreStationParams eqArgs = null, IgnoreStationParams neqArgs = null)
        {
            using (MyDbContext myDbContext = new MyDbContext())
            {
                IQueryable<IgnoreStation> query = Query(myDbContext.IgnoreStations.AsNoTracking().AsQueryable(), eqArgs, neqArgs);
                return query.Count();
            }
        }

		/// <summary>
        /// 查询符合查询条件的数据，数据不宜过大，如果数据过大建议使用分页查询
        /// </summary>
        /// <param name="eqArgs">查询参数，不为null时会作为查询参数</param>
		/// <param name="neqArgs">不等查询参数，不为null时会作为不等查询参数</param>
        /// <returns>返回符合查询条件的数</returns>
        public List<IgnoreStation> FindList(IgnoreStationParams eqArgs = null, IgnoreStationParams neqArgs = null)
        {
            using (MyDbContext myDbContext = new MyDbContext())
            {
                IQueryable<IgnoreStation> query = Query(myDbContext.IgnoreStations.AsNoTracking().AsQueryable(), eqArgs, neqArgs);
                return query.ToList();
            }
        }

		/// <summary>
        /// 修改变化值，把entity不为null的数据视为变化值
        /// </summary>
        /// <param name="entity">修改实体类，必须要传入主键，其余的参数如果不为null则视为变化值（主键id除外，只做数据标识）</param>
        /// <returns></returns>
        public int UpdateChange(IgnoreStation entity)
        {
            using (MyDbContext myDbContext = new MyDbContext())
            {
                IgnoreStation  updateBefore = new IgnoreStation  { Id = entity.Id};
                myDbContext.IgnoreStations.Attach(updateBefore);
								if (entity.Code != null)
								{
									updateBefore.Code = entity.Code;
								}					if (entity.Name != null)
								{
									updateBefore.Name = entity.Name;
								}					if (entity.NoDataCollectDate != null)
								{
									updateBefore.NoDataCollectDate = entity.NoDataCollectDate;
								}
                return myDbContext.SaveChanges();
            }
        }
    }
}