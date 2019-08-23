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
							if (eqArgs.IRWeiXinNickName != null)
							{
								query = query.Where(a => a.IRWeiXinNickName == eqArgs.IRWeiXinNickName);
							}

							if (!string.IsNullOrEmpty(eqArgs.IRWeiXinNickNameLike))
			                {
			                    query = query.Where(a => a.IRWeiXinNickName.Contains(eqArgs.IRWeiXinNickNameLike));
			                }
							if (eqArgs.IRWeiXinHeaderImage != null)
							{
								query = query.Where(a => a.IRWeiXinHeaderImage == eqArgs.IRWeiXinHeaderImage);
							}

							if (!string.IsNullOrEmpty(eqArgs.IRWeiXinHeaderImageLike))
			                {
			                    query = query.Where(a => a.IRWeiXinHeaderImage.Contains(eqArgs.IRWeiXinHeaderImageLike));
			                }
							if (eqArgs.IRQrCodeImagePath != null)
							{
								query = query.Where(a => a.IRQrCodeImagePath == eqArgs.IRQrCodeImagePath);
							}

							if (!string.IsNullOrEmpty(eqArgs.IRQrCodeImagePathLike))
			                {
			                    query = query.Where(a => a.IRQrCodeImagePath.Contains(eqArgs.IRQrCodeImagePathLike));
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
							if (eqArgs.IRHandleMessage != null)
							{
								query = query.Where(a => a.IRHandleMessage == eqArgs.IRHandleMessage);
							}

							if (!string.IsNullOrEmpty(eqArgs.IRHandleMessageLike))
			                {
			                    query = query.Where(a => a.IRHandleMessage.Contains(eqArgs.IRHandleMessageLike));
			                }
							if (eqArgs.IRHandleTime != null)
							{
								query = query.Where(a => a.IRHandleTime == eqArgs.IRHandleTime);
							}

							if (eqArgs.IRHandleTimeStart != null)
			                {
			                    query = query.Where(a => a.IRHandleTime >= eqArgs.IRHandleTimeStart);
			                }
							if (eqArgs.IRHandleTimeEnd != null)
			                {
			                    query = query.Where(a => a.IRHandleTime <= eqArgs.IRHandleTimeEnd);
			                }
							if (eqArgs.IRCreateTime != null)
							{
								query = query.Where(a => a.IRCreateTime == eqArgs.IRCreateTime);
							}

							if (eqArgs.IRCreateTimeStart != null)
			                {
			                    query = query.Where(a => a.IRCreateTime >= eqArgs.IRCreateTimeStart);
			                }
							if (eqArgs.IRCreateTimeEnd != null)
			                {
			                    query = query.Where(a => a.IRCreateTime <= eqArgs.IRCreateTimeEnd);
			                }
							if (eqArgs.IRReportPicPathJson != null)
							{
								query = query.Where(a => a.IRReportPicPathJson == eqArgs.IRReportPicPathJson);
							}

							if (!string.IsNullOrEmpty(eqArgs.IRReportPicPathJsonLike))
			                {
			                    query = query.Where(a => a.IRReportPicPathJson.Contains(eqArgs.IRReportPicPathJsonLike));
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
							if (eqArgs.IRRobotId != null)
							{
								query = query.Where(a => a.IRRobotId == eqArgs.IRRobotId);
							}

							if (!string.IsNullOrEmpty(eqArgs.IRRobotIdLike))
			                {
			                    query = query.Where(a => a.IRRobotId.Contains(eqArgs.IRRobotIdLike));
			                }
							if (eqArgs.IRRemark != null)
							{
								query = query.Where(a => a.IRRemark == eqArgs.IRRemark);
							}

							if (!string.IsNullOrEmpty(eqArgs.IRRemarkLike))
			                {
			                    query = query.Where(a => a.IRRemark.Contains(eqArgs.IRRemarkLike));
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
							if (eqArgs.IRPushTime != null)
							{
								query = query.Where(a => a.IRPushTime == eqArgs.IRPushTime);
							}

							if (eqArgs.IRPushTimeStart != null)
			                {
			                    query = query.Where(a => a.IRPushTime >= eqArgs.IRPushTimeStart);
			                }
							if (eqArgs.IRPushTimeEnd != null)
			                {
			                    query = query.Where(a => a.IRPushTime <= eqArgs.IRPushTimeEnd);
			                }
							if (eqArgs.IRScanPayNotifyRet != null)
							{
								query = query.Where(a => a.IRScanPayNotifyRet == eqArgs.IRScanPayNotifyRet);
							}

							if (!string.IsNullOrEmpty(eqArgs.IRScanPayNotifyRetLike))
			                {
			                    query = query.Where(a => a.IRScanPayNotifyRet.Contains(eqArgs.IRScanPayNotifyRetLike));
			                }
							if (eqArgs.IRScanPayNotifyUrl != null)
							{
								query = query.Where(a => a.IRScanPayNotifyUrl == eqArgs.IRScanPayNotifyUrl);
							}

							if (!string.IsNullOrEmpty(eqArgs.IRScanPayNotifyUrlLike))
			                {
			                    query = query.Where(a => a.IRScanPayNotifyUrl.Contains(eqArgs.IRScanPayNotifyUrlLike));
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
							if (neqArgs.IRWeiXinNickName != null)
							{
								query = query.Where(a => a.IRWeiXinNickName != neqArgs.IRWeiXinNickName);
							}

							if (!string.IsNullOrEmpty(neqArgs.IRWeiXinNickNameLike))
			                {
								query = query.Where(a => !a.IRWeiXinNickName.Contains(neqArgs.IRWeiXinNickNameLike));
							}
							if (neqArgs.IRWeiXinHeaderImage != null)
							{
								query = query.Where(a => a.IRWeiXinHeaderImage != neqArgs.IRWeiXinHeaderImage);
							}

							if (!string.IsNullOrEmpty(neqArgs.IRWeiXinHeaderImageLike))
			                {
								query = query.Where(a => !a.IRWeiXinHeaderImage.Contains(neqArgs.IRWeiXinHeaderImageLike));
							}
							if (neqArgs.IRQrCodeImagePath != null)
							{
								query = query.Where(a => a.IRQrCodeImagePath != neqArgs.IRQrCodeImagePath);
							}

							if (!string.IsNullOrEmpty(neqArgs.IRQrCodeImagePathLike))
			                {
								query = query.Where(a => !a.IRQrCodeImagePath.Contains(neqArgs.IRQrCodeImagePathLike));
							}
							if (neqArgs.IRHandleState != null)
							{
								query = query.Where(a => a.IRHandleState != neqArgs.IRHandleState);
							}

							if (neqArgs.IRHandleMessage != null)
							{
								query = query.Where(a => a.IRHandleMessage != neqArgs.IRHandleMessage);
							}

							if (!string.IsNullOrEmpty(neqArgs.IRHandleMessageLike))
			                {
								query = query.Where(a => !a.IRHandleMessage.Contains(neqArgs.IRHandleMessageLike));
							}
							if (neqArgs.IRHandleTime != null)
							{
								query = query.Where(a => a.IRHandleTime != neqArgs.IRHandleTime);
							}

							if (neqArgs.IRCreateTime != null)
							{
								query = query.Where(a => a.IRCreateTime != neqArgs.IRCreateTime);
							}

							if (neqArgs.IRReportPicPathJson != null)
							{
								query = query.Where(a => a.IRReportPicPathJson != neqArgs.IRReportPicPathJson);
							}

							if (!string.IsNullOrEmpty(neqArgs.IRReportPicPathJsonLike))
			                {
								query = query.Where(a => !a.IRReportPicPathJson.Contains(neqArgs.IRReportPicPathJsonLike));
							}
							if (neqArgs.IRTakeMoney != null)
							{
								query = query.Where(a => a.IRTakeMoney != neqArgs.IRTakeMoney);
							}

							if (neqArgs.IRRobotId != null)
							{
								query = query.Where(a => a.IRRobotId != neqArgs.IRRobotId);
							}

							if (!string.IsNullOrEmpty(neqArgs.IRRobotIdLike))
			                {
								query = query.Where(a => !a.IRRobotId.Contains(neqArgs.IRRobotIdLike));
							}
							if (neqArgs.IRRemark != null)
							{
								query = query.Where(a => a.IRRemark != neqArgs.IRRemark);
							}

							if (!string.IsNullOrEmpty(neqArgs.IRRemarkLike))
			                {
								query = query.Where(a => !a.IRRemark.Contains(neqArgs.IRRemarkLike));
							}
							if (neqArgs.IRPushState != null)
							{
								query = query.Where(a => a.IRPushState != neqArgs.IRPushState);
							}

							if (neqArgs.IRPushTime != null)
							{
								query = query.Where(a => a.IRPushTime != neqArgs.IRPushTime);
							}

							if (neqArgs.IRScanPayNotifyRet != null)
							{
								query = query.Where(a => a.IRScanPayNotifyRet != neqArgs.IRScanPayNotifyRet);
							}

							if (!string.IsNullOrEmpty(neqArgs.IRScanPayNotifyRetLike))
			                {
								query = query.Where(a => !a.IRScanPayNotifyRet.Contains(neqArgs.IRScanPayNotifyRetLike));
							}
							if (neqArgs.IRScanPayNotifyUrl != null)
							{
								query = query.Where(a => a.IRScanPayNotifyUrl != neqArgs.IRScanPayNotifyUrl);
							}

							if (!string.IsNullOrEmpty(neqArgs.IRScanPayNotifyUrlLike))
			                {
								query = query.Where(a => !a.IRScanPayNotifyUrl.Contains(neqArgs.IRScanPayNotifyUrlLike));
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
							if (orderBy.IRWeiXinNickName) { query = query.OrderBy(a => a.IRWeiXinNickName); }
							if (orderBy.IRWeiXinHeaderImage) { query = query.OrderBy(a => a.IRWeiXinHeaderImage); }
							if (orderBy.IRQrCodeImagePath) { query = query.OrderBy(a => a.IRQrCodeImagePath); }
							if (orderBy.IRHandleState) { query = query.OrderBy(a => a.IRHandleState); }
							if (orderBy.IRHandleMessage) { query = query.OrderBy(a => a.IRHandleMessage); }
							if (orderBy.IRHandleTime) { query = query.OrderBy(a => a.IRHandleTime); }
							if (orderBy.IRCreateTime) { query = query.OrderBy(a => a.IRCreateTime); }
							if (orderBy.IRReportPicPathJson) { query = query.OrderBy(a => a.IRReportPicPathJson); }
							if (orderBy.IRTakeMoney) { query = query.OrderBy(a => a.IRTakeMoney); }
							if (orderBy.IRRobotId) { query = query.OrderBy(a => a.IRRobotId); }
							if (orderBy.IRRemark) { query = query.OrderBy(a => a.IRRemark); }
							if (orderBy.IRPushState) { query = query.OrderBy(a => a.IRPushState); }
							if (orderBy.IRPushTime) { query = query.OrderBy(a => a.IRPushTime); }
							if (orderBy.IRScanPayNotifyRet) { query = query.OrderBy(a => a.IRScanPayNotifyRet); }
							if (orderBy.IRScanPayNotifyUrl) { query = query.OrderBy(a => a.IRScanPayNotifyUrl); }
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
							if (orderBy.IRWeiXinNickName) { query = query.OrderByDescending(a => a.IRWeiXinNickName); }else 
							if (orderBy.IRWeiXinHeaderImage) { query = query.OrderByDescending(a => a.IRWeiXinHeaderImage); }else 
							if (orderBy.IRQrCodeImagePath) { query = query.OrderByDescending(a => a.IRQrCodeImagePath); }else 
							if (orderBy.IRHandleState) { query = query.OrderByDescending(a => a.IRHandleState); }else 
							if (orderBy.IRHandleMessage) { query = query.OrderByDescending(a => a.IRHandleMessage); }else 
							if (orderBy.IRHandleTime) { query = query.OrderByDescending(a => a.IRHandleTime); }else 
							if (orderBy.IRCreateTime) { query = query.OrderByDescending(a => a.IRCreateTime); }else 
							if (orderBy.IRReportPicPathJson) { query = query.OrderByDescending(a => a.IRReportPicPathJson); }else 
							if (orderBy.IRTakeMoney) { query = query.OrderByDescending(a => a.IRTakeMoney); }else 
							if (orderBy.IRRobotId) { query = query.OrderByDescending(a => a.IRRobotId); }else 
							if (orderBy.IRRemark) { query = query.OrderByDescending(a => a.IRRemark); }else 
							if (orderBy.IRPushState) { query = query.OrderByDescending(a => a.IRPushState); }else 
							if (orderBy.IRPushTime) { query = query.OrderByDescending(a => a.IRPushTime); }else 
							if (orderBy.IRScanPayNotifyRet) { query = query.OrderByDescending(a => a.IRScanPayNotifyRet); }else 
							if (orderBy.IRScanPayNotifyUrl) { query = query.OrderByDescending(a => a.IRScanPayNotifyUrl); }
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
								}					if (entity.IRWeiXinNickName != null)
								{
									updateBefore.IRWeiXinNickName = entity.IRWeiXinNickName;
								}					if (entity.IRWeiXinHeaderImage != null)
								{
									updateBefore.IRWeiXinHeaderImage = entity.IRWeiXinHeaderImage;
								}					if (entity.IRQrCodeImagePath != null)
								{
									updateBefore.IRQrCodeImagePath = entity.IRQrCodeImagePath;
								}					if (entity.IRHandleState != null)
								{
									updateBefore.IRHandleState = entity.IRHandleState;
								}					if (entity.IRHandleMessage != null)
								{
									updateBefore.IRHandleMessage = entity.IRHandleMessage;
								}					if (entity.IRHandleTime != null)
								{
									updateBefore.IRHandleTime = entity.IRHandleTime;
								}					if (entity.IRCreateTime != null)
								{
									updateBefore.IRCreateTime = entity.IRCreateTime;
								}					if (entity.IRReportPicPathJson != null)
								{
									updateBefore.IRReportPicPathJson = entity.IRReportPicPathJson;
								}					if (entity.IRTakeMoney != null)
								{
									updateBefore.IRTakeMoney = entity.IRTakeMoney;
								}					if (entity.IRRobotId != null)
								{
									updateBefore.IRRobotId = entity.IRRobotId;
								}					if (entity.IRRemark != null)
								{
									updateBefore.IRRemark = entity.IRRemark;
								}					if (entity.IRPushState != null)
								{
									updateBefore.IRPushState = entity.IRPushState;
								}					if (entity.IRPushTime != null)
								{
									updateBefore.IRPushTime = entity.IRPushTime;
								}					if (entity.IRScanPayNotifyRet != null)
								{
									updateBefore.IRScanPayNotifyRet = entity.IRScanPayNotifyRet;
								}					if (entity.IRScanPayNotifyUrl != null)
								{
									updateBefore.IRScanPayNotifyUrl = entity.IRScanPayNotifyUrl;
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
								}					if (entity.IRWeiXinNickName != null)
								{
									updateBefore.IRWeiXinNickName = entity.IRWeiXinNickName;
								}					if (entity.IRWeiXinHeaderImage != null)
								{
									updateBefore.IRWeiXinHeaderImage = entity.IRWeiXinHeaderImage;
								}					if (entity.IRQrCodeImagePath != null)
								{
									updateBefore.IRQrCodeImagePath = entity.IRQrCodeImagePath;
								}					if (entity.IRHandleState != null)
								{
									updateBefore.IRHandleState = entity.IRHandleState;
								}					if (entity.IRHandleMessage != null)
								{
									updateBefore.IRHandleMessage = entity.IRHandleMessage;
								}					if (entity.IRHandleTime != null)
								{
									updateBefore.IRHandleTime = entity.IRHandleTime;
								}					if (entity.IRCreateTime != null)
								{
									updateBefore.IRCreateTime = entity.IRCreateTime;
								}					if (entity.IRReportPicPathJson != null)
								{
									updateBefore.IRReportPicPathJson = entity.IRReportPicPathJson;
								}					if (entity.IRTakeMoney != null)
								{
									updateBefore.IRTakeMoney = entity.IRTakeMoney;
								}					if (entity.IRRobotId != null)
								{
									updateBefore.IRRobotId = entity.IRRobotId;
								}					if (entity.IRRemark != null)
								{
									updateBefore.IRRemark = entity.IRRemark;
								}					if (entity.IRPushState != null)
								{
									updateBefore.IRPushState = entity.IRPushState;
								}					if (entity.IRPushTime != null)
								{
									updateBefore.IRPushTime = entity.IRPushTime;
								}					if (entity.IRScanPayNotifyRet != null)
								{
									updateBefore.IRScanPayNotifyRet = entity.IRScanPayNotifyRet;
								}					if (entity.IRScanPayNotifyUrl != null)
								{
									updateBefore.IRScanPayNotifyUrl = entity.IRScanPayNotifyUrl;
								}
				}
				return myDbContext.SaveChanges();
			}
		}

		/// <summary>
        /// 把指定字段值设置为null
        /// </summary>
        /// <param name="param">字段值</param>
        /// <returns>返回修改的数据行数</returns>
		public void SetNull(IRobotQrCodePayTaskSetNullParams param)
		{
			 IRobotQrCodePayTask updateBefore = FindEntity(param.IRTaskID);
			 using (MyDbContext myDbContext = new MyDbContext())
             {
				myDbContext.IRobotQrCodePayTasks.Attach(updateBefore);
								if (param.IROrderNo)
								{
									updateBefore.IROrderNo = null;
								}				if (param.IRWeiXinNickName)
								{
									updateBefore.IRWeiXinNickName = null;
								}				if (param.IRWeiXinHeaderImage)
								{
									updateBefore.IRWeiXinHeaderImage = null;
								}				if (param.IRQrCodeImagePath)
								{
									updateBefore.IRQrCodeImagePath = null;
								}				if (param.IRHandleState)
								{
									updateBefore.IRHandleState = null;
								}				if (param.IRHandleMessage)
								{
									updateBefore.IRHandleMessage = null;
								}				if (param.IRHandleTime)
								{
									updateBefore.IRHandleTime = null;
								}				if (param.IRCreateTime)
								{
									updateBefore.IRCreateTime = null;
								}				if (param.IRReportPicPathJson)
								{
									updateBefore.IRReportPicPathJson = null;
								}				if (param.IRTakeMoney)
								{
									updateBefore.IRTakeMoney = null;
								}				if (param.IRRobotId)
								{
									updateBefore.IRRobotId = null;
								}				if (param.IRRemark)
								{
									updateBefore.IRRemark = null;
								}				if (param.IRPushState)
								{
									updateBefore.IRPushState = null;
								}				if (param.IRPushTime)
								{
									updateBefore.IRPushTime = null;
								}				if (param.IRScanPayNotifyRet)
								{
									updateBefore.IRScanPayNotifyRet = null;
								}				if (param.IRScanPayNotifyUrl)
								{
									updateBefore.IRScanPayNotifyUrl = null;
								}
				myDbContext.SaveChanges();
			 }
		}
    }
}