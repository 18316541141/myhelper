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
    public partial class IRobotErrorMsgRepository : BaseRepository<IRobotErrorMsg>
    {
		/// <summary>
        /// 通用的设置查询参数方法，只有在参数不为null的情况下才会设置，
		/// 满足项目中常用的模糊查询，分页查询等功能
        /// </summary>
        /// <param name="query">待设置参数的query对象</param>
        /// <param name="eqArgs">装载查询参数的实体类</param>
		/// <param name="neqArgs">装载不等于查询参数的实体类</param>
        /// <returns>返回设置好查询参数的query对象</returns>
        IQueryable<IRobotErrorMsg> Query(IQueryable<IRobotErrorMsg> query, IRobotErrorMsgParams eqArgs, IRobotErrorMsgParams neqArgs = null)
        {
            if (eqArgs != null)
            {

							if (eqArgs.IEErrNo != null)
							{
								query = query.Where(a => a.IEErrNo == eqArgs.IEErrNo);
							}

							if (eqArgs.IECreateDate != null)
							{
								query = query.Where(a => a.IECreateDate == eqArgs.IECreateDate);
							}

							if (eqArgs.IECreateDateStart != null)
			                {
			                    query = query.Where(a => a.IECreateDate >= eqArgs.IECreateDateStart);
			                }
							if (eqArgs.IECreateDateEnd != null)
			                {
			                    query = query.Where(a => a.IECreateDate <= eqArgs.IECreateDateEnd);
			                }
							if (eqArgs.IEErrOrderNo != null)
							{
								query = query.Where(a => a.IEErrOrderNo == eqArgs.IEErrOrderNo);
							}

							if (!string.IsNullOrEmpty(eqArgs.IEErrOrderNoLike))
			                {
			                    query = query.Where(a => a.IEErrOrderNo.Contains(eqArgs.IEErrOrderNoLike));
			                }
							if (eqArgs.IEErrRobotId != null)
							{
								query = query.Where(a => a.IEErrRobotId == eqArgs.IEErrRobotId);
							}

							if (!string.IsNullOrEmpty(eqArgs.IEErrRobotIdLike))
			                {
			                    query = query.Where(a => a.IEErrRobotId.Contains(eqArgs.IEErrRobotIdLike));
			                }
							if (eqArgs.IEErrPic != null)
							{
								query = query.Where(a => a.IEErrPic == eqArgs.IEErrPic);
							}

							if (!string.IsNullOrEmpty(eqArgs.IEErrPicLike))
			                {
			                    query = query.Where(a => a.IEErrPic.Contains(eqArgs.IEErrPicLike));
			                }
							if (eqArgs.IEErrContext != null)
							{
								query = query.Where(a => a.IEErrContext == eqArgs.IEErrContext);
							}

							if (!string.IsNullOrEmpty(eqArgs.IEErrContextLike))
			                {
			                    query = query.Where(a => a.IEErrContext.Contains(eqArgs.IEErrContextLike));
			                }
							if (eqArgs.IEHandleStatus != null)
							{
								query = query.Where(a => a.IEHandleStatus == eqArgs.IEHandleStatus);
							}

							if (eqArgs.IEHandleStatusStart != null)
			                {
			                    query = query.Where(a => a.IEHandleStatus >= eqArgs.IEHandleStatusStart);
			                }
							if (eqArgs.IEHandleStatusEnd != null)
			                {
			                    query = query.Where(a => a.IEHandleStatus <= eqArgs.IEHandleStatusEnd);
			                }
            }
			if(neqArgs != null){

							if (neqArgs.IEErrNo != null)
							{
								query = query.Where(a => a.IEErrNo != neqArgs.IEErrNo);
							}

							if (neqArgs.IECreateDate != null)
							{
								query = query.Where(a => a.IECreateDate != neqArgs.IECreateDate);
							}

							if (neqArgs.IEErrOrderNo != null)
							{
								query = query.Where(a => a.IEErrOrderNo != neqArgs.IEErrOrderNo);
							}

							if (!string.IsNullOrEmpty(neqArgs.IEErrOrderNoLike))
			                {
								query = query.Where(a => !a.IEErrOrderNo.Contains(neqArgs.IEErrOrderNoLike));
							}
							if (neqArgs.IEErrRobotId != null)
							{
								query = query.Where(a => a.IEErrRobotId != neqArgs.IEErrRobotId);
							}

							if (!string.IsNullOrEmpty(neqArgs.IEErrRobotIdLike))
			                {
								query = query.Where(a => !a.IEErrRobotId.Contains(neqArgs.IEErrRobotIdLike));
							}
							if (neqArgs.IEErrPic != null)
							{
								query = query.Where(a => a.IEErrPic != neqArgs.IEErrPic);
							}

							if (!string.IsNullOrEmpty(neqArgs.IEErrPicLike))
			                {
								query = query.Where(a => !a.IEErrPic.Contains(neqArgs.IEErrPicLike));
							}
							if (neqArgs.IEErrContext != null)
							{
								query = query.Where(a => a.IEErrContext != neqArgs.IEErrContext);
							}

							if (!string.IsNullOrEmpty(neqArgs.IEErrContextLike))
			                {
								query = query.Where(a => !a.IEErrContext.Contains(neqArgs.IEErrContextLike));
							}
							if (neqArgs.IEHandleStatus != null)
							{
								query = query.Where(a => a.IEHandleStatus != neqArgs.IEHandleStatus);
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
        IQueryable<IRobotErrorMsg> OrderByAsc(IQueryable<IRobotErrorMsg> query, IRobotErrorMsgOrderBy orderBy)
        {
            if (orderBy != null)
            {

							if (orderBy.IEErrNo) { query = query.OrderBy(a => a.IEErrNo); }
							if (orderBy.IECreateDate) { query = query.OrderBy(a => a.IECreateDate); }
							if (orderBy.IEErrOrderNo) { query = query.OrderBy(a => a.IEErrOrderNo); }
							if (orderBy.IEErrRobotId) { query = query.OrderBy(a => a.IEErrRobotId); }
							if (orderBy.IEErrPic) { query = query.OrderBy(a => a.IEErrPic); }
							if (orderBy.IEErrContext) { query = query.OrderBy(a => a.IEErrContext); }
							if (orderBy.IEHandleStatus) { query = query.OrderBy(a => a.IEHandleStatus); }
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
        IQueryable<IRobotErrorMsg> OrderByDesc(IQueryable<IRobotErrorMsg> query, IRobotErrorMsgOrderBy orderBy)
        {
            if (orderBy != null)
            {

							if (orderBy.IEErrNo) { query = query.OrderByDescending(a => a.IEErrNo); }else 
							if (orderBy.IECreateDate) { query = query.OrderByDescending(a => a.IECreateDate); }else 
							if (orderBy.IEErrOrderNo) { query = query.OrderByDescending(a => a.IEErrOrderNo); }else 
							if (orderBy.IEErrRobotId) { query = query.OrderByDescending(a => a.IEErrRobotId); }else 
							if (orderBy.IEErrPic) { query = query.OrderByDescending(a => a.IEErrPic); }else 
							if (orderBy.IEErrContext) { query = query.OrderByDescending(a => a.IEErrContext); }else 
							if (orderBy.IEHandleStatus) { query = query.OrderByDescending(a => a.IEHandleStatus); }
				else
				{
					query = query.OrderByDescending(a => a.IEErrNo);
				}
            }
            else
            {
                query = query.OrderByDescending(a => a.IEErrNo);
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
        public MyPagedList<IRobotErrorMsg> BigPageList(IRobotErrorMsgParams eqArgs, int pageIndex = 1, int pageSize = 10000, IRobotErrorMsgParams neqArgs = null)
        {
			if( pageIndex <= 0 )
			{
				pageIndex = 1;
			}
			if( pageSize > 10000 || pageSize <= 0 )
			{
				pageSize = 10000;
			}
            using (MyDbContext myDbContext = new MyDbContext())
            {
                IQueryable<IRobotErrorMsg> query = Query(myDbContext.IRobotErrorMsgs.AsNoTracking().AsQueryable(), eqArgs, neqArgs);
                query = OrderByAsc(query, eqArgs.orderByAsc);
                query = OrderByDesc(query, eqArgs.orderByDesc);
                int totalItemCount = query.Count();
				int totalPageCount = (totalItemCount - totalItemCount % pageSize) / pageSize + 1;
				if( pageIndex >= totalPageCount)
				{
					pageIndex = Math.Max(totalPageCount - 1,1);
				}
                query = query.Skip((pageIndex-1) * pageSize);
                List<IRobotErrorMsg> pageDataList = new List<IRobotErrorMsg>();
                for (int i=0, partCount = (pageSize - pageSize % 1000) / pageSize + 1; i<partCount ;i++)
                {
                    pageDataList.AddRange(query.Skip(i * 1000).Take(1000).ToList());
                }
                return new MyPagedList<IRobotErrorMsg>
                {
                    CurrentPageIndex = pageIndex,
                    PageDataList = pageDataList,
                    TotalItemCount = totalItemCount,
                    PageSize = pageSize,
                    TotalPageCount = totalPageCount,
                    StartItemIndex = (pageSize - 1) * pageIndex + 1,
                    EndItemIndex = pageSize * pageIndex
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
        public MyPagedList<IRobotErrorMsg> PageList(IRobotErrorMsgParams eqArgs, int pageIndex = 1, int pageSize = 20,IRobotErrorMsgParams neqArgs = null)
        {
			if( pageIndex <= 0 )
			{
				pageIndex = 1;
			}
			if( pageSize > 100)
			{
				pageSize = 100;
			} 
			else if( pageSize <= 0)
			{
				pageSize = 20;
			}
            using (MyDbContext myDbContext = new MyDbContext())
            {
                IQueryable<IRobotErrorMsg> query = Query(myDbContext.IRobotErrorMsgs.AsNoTracking().AsQueryable(), eqArgs, neqArgs);
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
        public int Count(IRobotErrorMsgParams eqArgs = null, IRobotErrorMsgParams neqArgs = null)
        {
            using (MyDbContext myDbContext = new MyDbContext())
            {
                IQueryable<IRobotErrorMsg> query = Query(myDbContext.IRobotErrorMsgs.AsNoTracking().AsQueryable(), eqArgs, neqArgs);
                return query.Count();
            }
        }

		/// <summary>
        /// 查询符合查询条件的数据，数据不宜过大，如果数据过大建议使用分页查询
        /// </summary>
        /// <param name="eqArgs">查询参数，不为null时会作为查询参数</param>
		/// <param name="neqArgs">不等查询参数，不为null时会作为不等查询参数</param>
        /// <returns>返回符合查询条件的数</returns>
        public List<IRobotErrorMsg> FindList(IRobotErrorMsgParams eqArgs = null, IRobotErrorMsgParams neqArgs = null)
        {
            using (MyDbContext myDbContext = new MyDbContext())
            {
                IQueryable<IRobotErrorMsg> query = Query(myDbContext.IRobotErrorMsgs.AsNoTracking().AsQueryable(), eqArgs, neqArgs);
                return query.ToList();
            }
        }

		/// <summary>
        /// 修改变化值，把entity不为null的数据视为变化值
        /// </summary>
        /// <param name="entity">修改实体类，必须要传入主键，其余的参数如果不为null则视为变化值（主键id除外，只做数据标识）</param>
        /// <returns>返回修改的数据行数</returns>
        public int UpdateChange(IRobotErrorMsg entity)
        {
            using (MyDbContext myDbContext = new MyDbContext())
            {
                IRobotErrorMsg  updateBefore = new IRobotErrorMsg  { IEErrNo = entity.IEErrNo};
                myDbContext.IRobotErrorMsgs.Attach(updateBefore);
								if (entity.IECreateDate != null)
								{
									updateBefore.IECreateDate = entity.IECreateDate;
								}					if (entity.IEErrOrderNo != null)
								{
									updateBefore.IEErrOrderNo = entity.IEErrOrderNo;
								}					if (entity.IEErrRobotId != null)
								{
									updateBefore.IEErrRobotId = entity.IEErrRobotId;
								}					if (entity.IEErrPic != null)
								{
									updateBefore.IEErrPic = entity.IEErrPic;
								}					if (entity.IEErrContext != null)
								{
									updateBefore.IEErrContext = entity.IEErrContext;
								}					if (entity.IEHandleStatus != null)
								{
									updateBefore.IEHandleStatus = entity.IEHandleStatus;
								}
                return myDbContext.SaveChanges();
            }
        }

		/// <summary>
        /// 批量修改变化值，把entity不为null的数据视为变化值
        /// </summary>
        /// <param name="entities">修改实体类集合，必须要传入主键，其余的参数如果不为null则视为变化值（主键id除外，只做数据标识）</param>
        /// <returns>返回修改的数据行数</returns>
		public int UpdateChangeBatch(List<IRobotErrorMsg> entities)
		{
			using (MyDbContext myDbContext = new MyDbContext())
            {
				foreach(IRobotErrorMsg entity in entities)
				{
					IRobotErrorMsg  updateBefore = new IRobotErrorMsg  { IEErrNo = entity.IEErrNo};
					myDbContext.IRobotErrorMsgs.Attach(updateBefore);
								if (entity.IECreateDate != null)
								{
									updateBefore.IECreateDate = entity.IECreateDate;
								}					if (entity.IEErrOrderNo != null)
								{
									updateBefore.IEErrOrderNo = entity.IEErrOrderNo;
								}					if (entity.IEErrRobotId != null)
								{
									updateBefore.IEErrRobotId = entity.IEErrRobotId;
								}					if (entity.IEErrPic != null)
								{
									updateBefore.IEErrPic = entity.IEErrPic;
								}					if (entity.IEErrContext != null)
								{
									updateBefore.IEErrContext = entity.IEErrContext;
								}					if (entity.IEHandleStatus != null)
								{
									updateBefore.IEHandleStatus = entity.IEHandleStatus;
								}
				}
				return myDbContext.SaveChanges();
			}
		}
    }
}