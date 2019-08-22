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
    public partial class IRobotQrCodePayTaskRepository : BaseRepository<IRobotQrCodePayTask>
    {
		/// <summary>
        /// 通用的设置查询参数方法，只有在参数不为null的情况下才会设置，
		/// 满足项目中常用的模糊查询，分页查询等功能
        /// </summary>
        /// <param name="query">待设置参数的query对象</param>
        /// <param name="eqArgs">装载查询参数的实体类</param>
		/// <param name="neqArgs">装载不等于查询参数的实体类</param>
        /// <returns>返回设置好查询参数的query对象</returns>
        IQueryable<IRobotQrCodePayTask> Query(IQueryable<IRobotQrCodePayTask> query, IRobotQrCodePayTaskParams eqArgs, IRobotQrCodePayTaskParams neqArgs = null)
        {
            if (eqArgs != null)
            {

							if (eqArgs.IRTaskID != null)
							{
								query = query.Where(a => a.IRTaskID == eqArgs.IRTaskID);
							}

							if (eqArgs.IROrderNo != null)
							{
								query = query.Where(a => a.IROrderNo == eqArgs.IROrderNo);
							}

							if (!string.IsNullOrEmpty(eqArgs.IROrderNoLike))
			                {
			                    query = query.Where(a => a.IROrderNo.Contains(eqArgs.IROrderNoLike));
			                }
							if (eqArgs.IRTakeMoney != null)
							{
								query = query.Where(a => a.IRTakeMoney == eqArgs.IRTakeMoney);
							}

							if (eqArgs.IRTakeMoneyStart != null)
			                {
			                    query = query.Where(a => a.IRTakeMoney >= eqArgs.IRTakeMoneyStart);
			                }
							if (eqArgs.IRTakeMoneyEnd != null)
			                {
			                    query = query.Where(a => a.IRTakeMoney <= eqArgs.IRTakeMoneyEnd);
			                }
							if (eqArgs.IRPushState != null)
							{
								query = query.Where(a => a.IRPushState == eqArgs.IRPushState);
							}

							if (eqArgs.IRPushStateStart != null)
			                {
			                    query = query.Where(a => a.IRPushState >= eqArgs.IRPushStateStart);
			                }
							if (eqArgs.IRPushStateEnd != null)
			                {
			                    query = query.Where(a => a.IRPushState <= eqArgs.IRPushStateEnd);
			                }
							if (eqArgs.IRScanPayNotifyUrl != null)
							{
								query = query.Where(a => a.IRScanPayNotifyUrl == eqArgs.IRScanPayNotifyUrl);
							}

							if (!string.IsNullOrEmpty(eqArgs.IRScanPayNotifyUrlLike))
			                {
			                    query = query.Where(a => a.IRScanPayNotifyUrl.Contains(eqArgs.IRScanPayNotifyUrlLike));
			                }
							if (eqArgs.IRScanPayNotifyRet != null)
							{
								query = query.Where(a => a.IRScanPayNotifyRet == eqArgs.IRScanPayNotifyRet);
							}

							if (!string.IsNullOrEmpty(eqArgs.IRScanPayNotifyRetLike))
			                {
			                    query = query.Where(a => a.IRScanPayNotifyRet.Contains(eqArgs.IRScanPayNotifyRetLike));
			                }
							if (eqArgs.IRHandleState != null)
							{
								query = query.Where(a => a.IRHandleState == eqArgs.IRHandleState);
							}

							if (eqArgs.IRHandleStateStart != null)
			                {
			                    query = query.Where(a => a.IRHandleState >= eqArgs.IRHandleStateStart);
			                }
							if (eqArgs.IRHandleStateEnd != null)
			                {
			                    query = query.Where(a => a.IRHandleState <= eqArgs.IRHandleStateEnd);
			                }
							if (eqArgs.IRQrCodeImagePath != null)
							{
								query = query.Where(a => a.IRQrCodeImagePath == eqArgs.IRQrCodeImagePath);
							}

							if (!string.IsNullOrEmpty(eqArgs.IRQrCodeImagePathLike))
			                {
			                    query = query.Where(a => a.IRQrCodeImagePath.Contains(eqArgs.IRQrCodeImagePathLike));
			                }
							if (eqArgs.IRBsyNotifyState != null)
							{
								query = query.Where(a => a.IRBsyNotifyState == eqArgs.IRBsyNotifyState);
							}

							if (eqArgs.IRBsyNotifyStateStart != null)
			                {
			                    query = query.Where(a => a.IRBsyNotifyState >= eqArgs.IRBsyNotifyStateStart);
			                }
							if (eqArgs.IRBsyNotifyStateEnd != null)
			                {
			                    query = query.Where(a => a.IRBsyNotifyState <= eqArgs.IRBsyNotifyStateEnd);
			                }
							if (eqArgs.IRSendMoneyNotifyState != null)
							{
								query = query.Where(a => a.IRSendMoneyNotifyState == eqArgs.IRSendMoneyNotifyState);
							}

							if (eqArgs.IRSendMoneyNotifyStateStart != null)
			                {
			                    query = query.Where(a => a.IRSendMoneyNotifyState >= eqArgs.IRSendMoneyNotifyStateStart);
			                }
							if (eqArgs.IRSendMoneyNotifyStateEnd != null)
			                {
			                    query = query.Where(a => a.IRSendMoneyNotifyState <= eqArgs.IRSendMoneyNotifyStateEnd);
			                }
							if (eqArgs.IRReportPicPath != null)
							{
								query = query.Where(a => a.IRReportPicPath == eqArgs.IRReportPicPath);
							}

							if (!string.IsNullOrEmpty(eqArgs.IRReportPicPathLike))
			                {
			                    query = query.Where(a => a.IRReportPicPath.Contains(eqArgs.IRReportPicPathLike));
			                }
            }
			if(neqArgs != null){

							if (neqArgs.IRTaskID != null)
							{
								query = query.Where(a => a.IRTaskID != neqArgs.IRTaskID);
							}

							if (neqArgs.IROrderNo != null)
							{
								query = query.Where(a => a.IROrderNo != neqArgs.IROrderNo);
							}

							if (!string.IsNullOrEmpty(neqArgs.IROrderNoLike))
			                {
								query = query.Where(a => !a.IROrderNo.Contains(neqArgs.IROrderNoLike));
							}
							if (neqArgs.IRTakeMoney != null)
							{
								query = query.Where(a => a.IRTakeMoney != neqArgs.IRTakeMoney);
							}

							if (neqArgs.IRPushState != null)
							{
								query = query.Where(a => a.IRPushState != neqArgs.IRPushState);
							}

							if (neqArgs.IRScanPayNotifyUrl != null)
							{
								query = query.Where(a => a.IRScanPayNotifyUrl != neqArgs.IRScanPayNotifyUrl);
							}

							if (!string.IsNullOrEmpty(neqArgs.IRScanPayNotifyUrlLike))
			                {
								query = query.Where(a => !a.IRScanPayNotifyUrl.Contains(neqArgs.IRScanPayNotifyUrlLike));
							}
							if (neqArgs.IRScanPayNotifyRet != null)
							{
								query = query.Where(a => a.IRScanPayNotifyRet != neqArgs.IRScanPayNotifyRet);
							}

							if (!string.IsNullOrEmpty(neqArgs.IRScanPayNotifyRetLike))
			                {
								query = query.Where(a => !a.IRScanPayNotifyRet.Contains(neqArgs.IRScanPayNotifyRetLike));
							}
							if (neqArgs.IRHandleState != null)
							{
								query = query.Where(a => a.IRHandleState != neqArgs.IRHandleState);
							}

							if (neqArgs.IRQrCodeImagePath != null)
							{
								query = query.Where(a => a.IRQrCodeImagePath != neqArgs.IRQrCodeImagePath);
							}

							if (!string.IsNullOrEmpty(neqArgs.IRQrCodeImagePathLike))
			                {
								query = query.Where(a => !a.IRQrCodeImagePath.Contains(neqArgs.IRQrCodeImagePathLike));
							}
							if (neqArgs.IRBsyNotifyState != null)
							{
								query = query.Where(a => a.IRBsyNotifyState != neqArgs.IRBsyNotifyState);
							}

							if (neqArgs.IRSendMoneyNotifyState != null)
							{
								query = query.Where(a => a.IRSendMoneyNotifyState != neqArgs.IRSendMoneyNotifyState);
							}

							if (neqArgs.IRReportPicPath != null)
							{
								query = query.Where(a => a.IRReportPicPath != neqArgs.IRReportPicPath);
							}

							if (!string.IsNullOrEmpty(neqArgs.IRReportPicPathLike))
			                {
								query = query.Where(a => !a.IRReportPicPath.Contains(neqArgs.IRReportPicPathLike));
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
        IQueryable<IRobotQrCodePayTask> OrderByAsc(IQueryable<IRobotQrCodePayTask> query, IRobotQrCodePayTaskOrderBy orderBy)
        {
            if (orderBy != null)
            {

							if (orderBy.IRTaskID) { query = query.OrderBy(a => a.IRTaskID); }
							if (orderBy.IROrderNo) { query = query.OrderBy(a => a.IROrderNo); }
							if (orderBy.IRTakeMoney) { query = query.OrderBy(a => a.IRTakeMoney); }
							if (orderBy.IRPushState) { query = query.OrderBy(a => a.IRPushState); }
							if (orderBy.IRScanPayNotifyUrl) { query = query.OrderBy(a => a.IRScanPayNotifyUrl); }
							if (orderBy.IRScanPayNotifyRet) { query = query.OrderBy(a => a.IRScanPayNotifyRet); }
							if (orderBy.IRHandleState) { query = query.OrderBy(a => a.IRHandleState); }
							if (orderBy.IRQrCodeImagePath) { query = query.OrderBy(a => a.IRQrCodeImagePath); }
							if (orderBy.IRBsyNotifyState) { query = query.OrderBy(a => a.IRBsyNotifyState); }
							if (orderBy.IRSendMoneyNotifyState) { query = query.OrderBy(a => a.IRSendMoneyNotifyState); }
							if (orderBy.IRReportPicPath) { query = query.OrderBy(a => a.IRReportPicPath); }
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
        IQueryable<IRobotQrCodePayTask> OrderByDesc(IQueryable<IRobotQrCodePayTask> query, IRobotQrCodePayTaskOrderBy orderBy)
        {
            if (orderBy != null)
            {

							if (orderBy.IRTaskID) { query = query.OrderByDescending(a => a.IRTaskID); }else 
							if (orderBy.IROrderNo) { query = query.OrderByDescending(a => a.IROrderNo); }else 
							if (orderBy.IRTakeMoney) { query = query.OrderByDescending(a => a.IRTakeMoney); }else 
							if (orderBy.IRPushState) { query = query.OrderByDescending(a => a.IRPushState); }else 
							if (orderBy.IRScanPayNotifyUrl) { query = query.OrderByDescending(a => a.IRScanPayNotifyUrl); }else 
							if (orderBy.IRScanPayNotifyRet) { query = query.OrderByDescending(a => a.IRScanPayNotifyRet); }else 
							if (orderBy.IRHandleState) { query = query.OrderByDescending(a => a.IRHandleState); }else 
							if (orderBy.IRQrCodeImagePath) { query = query.OrderByDescending(a => a.IRQrCodeImagePath); }else 
							if (orderBy.IRBsyNotifyState) { query = query.OrderByDescending(a => a.IRBsyNotifyState); }else 
							if (orderBy.IRSendMoneyNotifyState) { query = query.OrderByDescending(a => a.IRSendMoneyNotifyState); }else 
							if (orderBy.IRReportPicPath) { query = query.OrderByDescending(a => a.IRReportPicPath); }
				else
				{
					query = query.OrderByDescending(a => a.IRTaskID);
				}
            }
            else
            {
                query = query.OrderByDescending(a => a.IRTaskID);
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
        public MyPagedList<IRobotQrCodePayTask> BigPageList(IRobotQrCodePayTaskParams eqArgs, int pageIndex = 1, int pageSize = 10000, IRobotQrCodePayTaskParams neqArgs = null)
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
                IQueryable<IRobotQrCodePayTask> query = Query(myDbContext.IRobotQrCodePayTasks.AsNoTracking().AsQueryable(), eqArgs, neqArgs);
                query = OrderByAsc(query, eqArgs.orderByAsc);
                query = OrderByDesc(query, eqArgs.orderByDesc);
                int totalItemCount = query.Count();
				int totalPageCount = (totalItemCount - totalItemCount % pageSize) / pageSize + 1;
				if( pageIndex >= totalPageCount)
				{
					pageIndex = Math.Max(totalPageCount - 1,1);
				}
                query = query.Skip((pageIndex-1) * pageSize);
                List<IRobotQrCodePayTask> pageDataList = new List<IRobotQrCodePayTask>();
                for (int i=0, partCount = (pageSize - pageSize % 1000) / pageSize + 1; i<partCount ;i++)
                {
                    pageDataList.AddRange(query.Skip(i * 1000).Take(1000).ToList());
                }
                return new MyPagedList<IRobotQrCodePayTask>
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
        public MyPagedList<IRobotQrCodePayTask> PageList(IRobotQrCodePayTaskParams eqArgs, int pageIndex = 1, int pageSize = 20,IRobotQrCodePayTaskParams neqArgs = null)
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
                IQueryable<IRobotQrCodePayTask> query = Query(myDbContext.IRobotQrCodePayTasks.AsNoTracking().AsQueryable(), eqArgs, neqArgs);
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
        public int Count(IRobotQrCodePayTaskParams eqArgs = null, IRobotQrCodePayTaskParams neqArgs = null)
        {
            using (MyDbContext myDbContext = new MyDbContext())
            {
                IQueryable<IRobotQrCodePayTask> query = Query(myDbContext.IRobotQrCodePayTasks.AsNoTracking().AsQueryable(), eqArgs, neqArgs);
                return query.Count();
            }
        }

		/// <summary>
        /// 查询符合查询条件的数据，数据不宜过大，如果数据过大建议使用分页查询
        /// </summary>
        /// <param name="eqArgs">查询参数，不为null时会作为查询参数</param>
		/// <param name="neqArgs">不等查询参数，不为null时会作为不等查询参数</param>
        /// <returns>返回符合查询条件的数</returns>
        public List<IRobotQrCodePayTask> FindList(IRobotQrCodePayTaskParams eqArgs = null, IRobotQrCodePayTaskParams neqArgs = null)
        {
            using (MyDbContext myDbContext = new MyDbContext())
            {
                IQueryable<IRobotQrCodePayTask> query = Query(myDbContext.IRobotQrCodePayTasks.AsNoTracking().AsQueryable(), eqArgs, neqArgs);
                return query.ToList();
            }
        }

		/// <summary>
        /// 修改变化值，把entity不为null的数据视为变化值
        /// </summary>
        /// <param name="entity">修改实体类，必须要传入主键，其余的参数如果不为null则视为变化值（主键id除外，只做数据标识）</param>
        /// <returns>返回修改的数据行数</returns>
        public int UpdateChange(IRobotQrCodePayTask entity)
        {
            using (MyDbContext myDbContext = new MyDbContext())
            {
                IRobotQrCodePayTask  updateBefore = new IRobotQrCodePayTask  { IRTaskID = entity.IRTaskID};
                myDbContext.IRobotQrCodePayTasks.Attach(updateBefore);
								if (entity.IROrderNo != null)
								{
									updateBefore.IROrderNo = entity.IROrderNo;
								}					if (entity.IRTakeMoney != null)
								{
									updateBefore.IRTakeMoney = entity.IRTakeMoney;
								}					if (entity.IRPushState != null)
								{
									updateBefore.IRPushState = entity.IRPushState;
								}					if (entity.IRScanPayNotifyUrl != null)
								{
									updateBefore.IRScanPayNotifyUrl = entity.IRScanPayNotifyUrl;
								}					if (entity.IRScanPayNotifyRet != null)
								{
									updateBefore.IRScanPayNotifyRet = entity.IRScanPayNotifyRet;
								}					if (entity.IRHandleState != null)
								{
									updateBefore.IRHandleState = entity.IRHandleState;
								}					if (entity.IRQrCodeImagePath != null)
								{
									updateBefore.IRQrCodeImagePath = entity.IRQrCodeImagePath;
								}					if (entity.IRBsyNotifyState != null)
								{
									updateBefore.IRBsyNotifyState = entity.IRBsyNotifyState;
								}					if (entity.IRSendMoneyNotifyState != null)
								{
									updateBefore.IRSendMoneyNotifyState = entity.IRSendMoneyNotifyState;
								}					if (entity.IRReportPicPath != null)
								{
									updateBefore.IRReportPicPath = entity.IRReportPicPath;
								}
                return myDbContext.SaveChanges();
            }
        }

		/// <summary>
        /// 批量修改变化值，把entity不为null的数据视为变化值
        /// </summary>
        /// <param name="entities">修改实体类集合，必须要传入主键，其余的参数如果不为null则视为变化值（主键id除外，只做数据标识）</param>
        /// <returns>返回修改的数据行数</returns>
		public int UpdateChangeBatch(List<IRobotQrCodePayTask> entities)
		{
			using (MyDbContext myDbContext = new MyDbContext())
            {
				foreach(IRobotQrCodePayTask entity in entities)
				{
					IRobotQrCodePayTask  updateBefore = new IRobotQrCodePayTask  { IRTaskID = entity.IRTaskID};
					myDbContext.IRobotQrCodePayTasks.Attach(updateBefore);
								if (entity.IROrderNo != null)
								{
									updateBefore.IROrderNo = entity.IROrderNo;
								}					if (entity.IRTakeMoney != null)
								{
									updateBefore.IRTakeMoney = entity.IRTakeMoney;
								}					if (entity.IRPushState != null)
								{
									updateBefore.IRPushState = entity.IRPushState;
								}					if (entity.IRScanPayNotifyUrl != null)
								{
									updateBefore.IRScanPayNotifyUrl = entity.IRScanPayNotifyUrl;
								}					if (entity.IRScanPayNotifyRet != null)
								{
									updateBefore.IRScanPayNotifyRet = entity.IRScanPayNotifyRet;
								}					if (entity.IRHandleState != null)
								{
									updateBefore.IRHandleState = entity.IRHandleState;
								}					if (entity.IRQrCodeImagePath != null)
								{
									updateBefore.IRQrCodeImagePath = entity.IRQrCodeImagePath;
								}					if (entity.IRBsyNotifyState != null)
								{
									updateBefore.IRBsyNotifyState = entity.IRBsyNotifyState;
								}					if (entity.IRSendMoneyNotifyState != null)
								{
									updateBefore.IRSendMoneyNotifyState = entity.IRSendMoneyNotifyState;
								}					if (entity.IRReportPicPath != null)
								{
									updateBefore.IRReportPicPath = entity.IRReportPicPath;
								}
				}
				return myDbContext.SaveChanges();
			}
		}
    }
}