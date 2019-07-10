using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication1.Entity;
using WebApplication1.MyExtensions;
using WebApplication1.Params;
using WebApplication1.Params.codeGenerator;
namespace WebApplication1.Repository
{
    public partial class IRobotQrCodePayTaskRepository : BaseRepository<IRobotQrCodePayTask>
    {
        IQueryable<IRobotQrCodePayTask> Query(IQueryable<IRobotQrCodePayTask> query, IRobotQrCodePayTaskParams args)
        {
            if (args != null)
            {

							if (args.irTaskID != null)
							{
								query = query.Where(a => a.irTaskID == args.irTaskID);
							}

							if (args.irTaskIDStart != null)
			                {
			                    query = query.Where(a => a.irTaskID >= args.irTaskIDStart);
			                }

							if (args.irTaskIDEnd != null)
			                {
			                    query = query.Where(a => a.irTaskID <= args.irTaskIDEnd);
			                }

							if (args.irWeiXinHeaderImage != null)
							{
								query = query.Where(a => a.irWeiXinHeaderImage == args.irWeiXinHeaderImage);
							}

							if (!string.IsNullOrEmpty(args.irWeiXinHeaderImageLike))
			                {
			                    query = query.Where(a => a.irWeiXinHeaderImage.Contains(args.irWeiXinHeaderImageLike));
			                }
							if (args.irWeiXinNickName != null)
							{
								query = query.Where(a => a.irWeiXinNickName == args.irWeiXinNickName);
							}

							if (!string.IsNullOrEmpty(args.irWeiXinNickNameLike))
			                {
			                    query = query.Where(a => a.irWeiXinNickName.Contains(args.irWeiXinNickNameLike));
			                }
							if (args.irCreateTime != null)
							{
								query = query.Where(a => a.irCreateTime == args.irCreateTime);
							}

							if (args.irCreateTimeStart != null)
			                {
			                    query = query.Where(a => a.irCreateTime >= args.irCreateTimeStart);
			                }

							if (args.irCreateTimeEnd != null)
			                {
			                    query = query.Where(a => a.irCreateTime <= args.irCreateTimeEnd);
			                }

							if (args.irHandleMessage != null)
							{
								query = query.Where(a => a.irHandleMessage == args.irHandleMessage);
							}

							if (!string.IsNullOrEmpty(args.irHandleMessageLike))
			                {
			                    query = query.Where(a => a.irHandleMessage.Contains(args.irHandleMessageLike));
			                }
							if (args.irHandleState != null)
							{
								query = query.Where(a => a.irHandleState == args.irHandleState);
							}

							if (args.irHandleStateStart != null)
			                {
			                    query = query.Where(a => a.irHandleState >= args.irHandleStateStart);
			                }

							if (args.irHandleStateEnd != null)
			                {
			                    query = query.Where(a => a.irHandleState <= args.irHandleStateEnd);
			                }

							if (args.irHandleTime != null)
							{
								query = query.Where(a => a.irHandleTime == args.irHandleTime);
							}

							if (args.irHandleTimeStart != null)
			                {
			                    query = query.Where(a => a.irHandleTime >= args.irHandleTimeStart);
			                }

							if (args.irHandleTimeEnd != null)
			                {
			                    query = query.Where(a => a.irHandleTime <= args.irHandleTimeEnd);
			                }

							if (args.irOrderNo != null)
							{
								query = query.Where(a => a.irOrderNo == args.irOrderNo);
							}

							if (!string.IsNullOrEmpty(args.irOrderNoLike))
			                {
			                    query = query.Where(a => a.irOrderNo.Contains(args.irOrderNoLike));
			                }
							if (args.irPushState != null)
							{
								query = query.Where(a => a.irPushState == args.irPushState);
							}

							if (args.irPushStateStart != null)
			                {
			                    query = query.Where(a => a.irPushState >= args.irPushStateStart);
			                }

							if (args.irPushStateEnd != null)
			                {
			                    query = query.Where(a => a.irPushState <= args.irPushStateEnd);
			                }

							if (args.irPushTime != null)
							{
								query = query.Where(a => a.irPushTime == args.irPushTime);
							}

							if (args.irPushTimeStart != null)
			                {
			                    query = query.Where(a => a.irPushTime >= args.irPushTimeStart);
			                }

							if (args.irPushTimeEnd != null)
			                {
			                    query = query.Where(a => a.irPushTime <= args.irPushTimeEnd);
			                }

							if (args.irQrCodeImagePath != null)
							{
								query = query.Where(a => a.irQrCodeImagePath == args.irQrCodeImagePath);
							}

							if (!string.IsNullOrEmpty(args.irQrCodeImagePathLike))
			                {
			                    query = query.Where(a => a.irQrCodeImagePath.Contains(args.irQrCodeImagePathLike));
			                }
							if (args.irRemark != null)
							{
								query = query.Where(a => a.irRemark == args.irRemark);
							}

							if (!string.IsNullOrEmpty(args.irRemarkLike))
			                {
			                    query = query.Where(a => a.irRemark.Contains(args.irRemarkLike));
			                }
							if (args.irReportPicPathJson != null)
							{
								query = query.Where(a => a.irReportPicPathJson == args.irReportPicPathJson);
							}

							if (!string.IsNullOrEmpty(args.irReportPicPathJsonLike))
			                {
			                    query = query.Where(a => a.irReportPicPathJson.Contains(args.irReportPicPathJsonLike));
			                }
							if (args.irRobotId != null)
							{
								query = query.Where(a => a.irRobotId == args.irRobotId);
							}

							if (!string.IsNullOrEmpty(args.irRobotIdLike))
			                {
			                    query = query.Where(a => a.irRobotId.Contains(args.irRobotIdLike));
			                }
							if (args.irScanPayNotifyRet != null)
							{
								query = query.Where(a => a.irScanPayNotifyRet == args.irScanPayNotifyRet);
							}

							if (!string.IsNullOrEmpty(args.irScanPayNotifyRetLike))
			                {
			                    query = query.Where(a => a.irScanPayNotifyRet.Contains(args.irScanPayNotifyRetLike));
			                }
							if (args.irTakeMoney != null)
							{
								query = query.Where(a => a.irTakeMoney == args.irTakeMoney);
							}

							if (args.irTakeMoneyStart != null)
			                {
			                    query = query.Where(a => a.irTakeMoney >= args.irTakeMoneyStart);
			                }

							if (args.irTakeMoneyEnd != null)
			                {
			                    query = query.Where(a => a.irTakeMoney <= args.irTakeMoneyEnd);
			                }

            }
            return query;
        }

        IQueryable<IRobotQrCodePayTask> OrderByAsc(IQueryable<IRobotQrCodePayTask> query, IRobotQrCodePayTaskOrderBy orderBy)
        {
            if (orderBy != null)
            {

							if (orderBy.irTaskID) { query = query.OrderBy(a => a.irTaskID); }
							if (orderBy.irWeiXinHeaderImage) { query = query.OrderBy(a => a.irWeiXinHeaderImage); }
							if (orderBy.irWeiXinNickName) { query = query.OrderBy(a => a.irWeiXinNickName); }
							if (orderBy.irCreateTime) { query = query.OrderBy(a => a.irCreateTime); }
							if (orderBy.irHandleMessage) { query = query.OrderBy(a => a.irHandleMessage); }
							if (orderBy.irHandleState) { query = query.OrderBy(a => a.irHandleState); }
							if (orderBy.irHandleTime) { query = query.OrderBy(a => a.irHandleTime); }
							if (orderBy.irOrderNo) { query = query.OrderBy(a => a.irOrderNo); }
							if (orderBy.irPushState) { query = query.OrderBy(a => a.irPushState); }
							if (orderBy.irPushTime) { query = query.OrderBy(a => a.irPushTime); }
							if (orderBy.irQrCodeImagePath) { query = query.OrderBy(a => a.irQrCodeImagePath); }
							if (orderBy.irRemark) { query = query.OrderBy(a => a.irRemark); }
							if (orderBy.irReportPicPathJson) { query = query.OrderBy(a => a.irReportPicPathJson); }
							if (orderBy.irRobotId) { query = query.OrderBy(a => a.irRobotId); }
							if (orderBy.irScanPayNotifyRet) { query = query.OrderBy(a => a.irScanPayNotifyRet); }
							if (orderBy.irTakeMoney) { query = query.OrderBy(a => a.irTakeMoney); }
            }
            return query;
        }

        IQueryable<IRobotQrCodePayTask> OrderByDesc(IQueryable<IRobotQrCodePayTask> query, IRobotQrCodePayTaskOrderBy orderBy)
        {
            if (orderBy != null)
            {

							if (orderBy.irTaskID) { query = query.OrderByDescending(a => a.irTaskID); }
							if (orderBy.irWeiXinHeaderImage) { query = query.OrderByDescending(a => a.irWeiXinHeaderImage); }
							if (orderBy.irWeiXinNickName) { query = query.OrderByDescending(a => a.irWeiXinNickName); }
							if (orderBy.irCreateTime) { query = query.OrderByDescending(a => a.irCreateTime); }
							if (orderBy.irHandleMessage) { query = query.OrderByDescending(a => a.irHandleMessage); }
							if (orderBy.irHandleState) { query = query.OrderByDescending(a => a.irHandleState); }
							if (orderBy.irHandleTime) { query = query.OrderByDescending(a => a.irHandleTime); }
							if (orderBy.irOrderNo) { query = query.OrderByDescending(a => a.irOrderNo); }
							if (orderBy.irPushState) { query = query.OrderByDescending(a => a.irPushState); }
							if (orderBy.irPushTime) { query = query.OrderByDescending(a => a.irPushTime); }
							if (orderBy.irQrCodeImagePath) { query = query.OrderByDescending(a => a.irQrCodeImagePath); }
							if (orderBy.irRemark) { query = query.OrderByDescending(a => a.irRemark); }
							if (orderBy.irReportPicPathJson) { query = query.OrderByDescending(a => a.irReportPicPathJson); }
							if (orderBy.irRobotId) { query = query.OrderByDescending(a => a.irRobotId); }
							if (orderBy.irScanPayNotifyRet) { query = query.OrderByDescending(a => a.irScanPayNotifyRet); }
							if (orderBy.irTakeMoney) { query = query.OrderByDescending(a => a.irTakeMoney); }
            }
            else
            {
                query = query.OrderByDescending(a => a.irTaskID);
            }
            return query;
        }

        public MyPagedList<IRobotQrCodePayTask> BigPageList(IRobotQrCodePayTaskParams args, int pageIndex, int pageSize)
        {
            using (MyDbContext myDbContext = new MyDbContext())
            {
                IQueryable<IRobotQrCodePayTask> query = Query(myDbContext.IRobotQrCodePayTasks.AsNoTracking().AsQueryable(), IRobotQrCodePayTaskParams);
                query = OrderByAsc(query, args.orderByAsc);
                query = OrderByDesc(query, args.orderByDesc);
                int totalItemCount = query.Count();
                query = query.Skip((pageIndex-1) * pageSize);
                List<IRobotQrCodePayTask> pageDataList = new List<IRobotQrCodePayTask>();
                for (int i=0, partCount = (pageSize - pageSize % 1000) / pageSize + 1; i<partCount ;i++)
                {
                    pageDataList.AddRange(query.Skip(i * 1000).Take(1000).ToList());
                }
                return new MyPagedList<IRobotQrCodePayTask>
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

        public MyPagedList<IRobotQrCodePayTask> PageList(IRobotQrCodePayTaskParams args, int pageIndex, int pageSize)
        {
            using (MyDbContext myDbContext = new MyDbContext())
            {
                IQueryable<IRobotQrCodePayTask> query = Query(myDbContext.IRobotQrCodePayTasks.AsNoTracking().AsQueryable(), args);
                query = OrderByAsc(query, args.orderByAsc);
                query = OrderByDesc(query, args.orderByDesc);
                return query.ToMyPagedList(pageIndex, pageSize);
            }
        }

        public int Count(IRobotQrCodePayTaskParams args)
        {
            using (MyDbContext myDbContext = new MyDbContext())
            {
                IQueryable<IRobotQrCodePayTask> query = Query(myDbContext.IRobotQrCodePayTasks.AsNoTracking().AsQueryable(), args);
                return query.Count();
            }
        }
        
        public List<IRobotQrCodePayTask> FindList(IRobotQrCodePayTaskParams args = null)
        {
            using (MyDbContext myDbContext = new MyDbContext())
            {
                IQueryable<IRobotQrCodePayTask> query = Query(myDbContext.IRobotQrCodePayTasks.AsNoTracking().AsQueryable(), args);
                return query.ToList();
            }
        }

        public int UpdateChange(IRobotQrCodePayTask entity)
        {
            using (MyDbContext myDbContext = new MyDbContext())
            {
                IRobotQrCodePayTask  updateBefore = new IRobotQrCodePayTask  { irTaskID = entity.irTaskID};
                myDbContext.IRobotQrCodePayTasks.Attach(updateBefore);

							if (entity.irTaskID != null)
							{
								updateBefore.irTaskID = entity.irTaskID;
							}
							if (entity.irWeiXinHeaderImage != null)
							{
								updateBefore.irWeiXinHeaderImage = entity.irWeiXinHeaderImage;
							}
							if (entity.irWeiXinNickName != null)
							{
								updateBefore.irWeiXinNickName = entity.irWeiXinNickName;
							}
							if (entity.irCreateTime != null)
							{
								updateBefore.irCreateTime = entity.irCreateTime;
							}
							if (entity.irHandleMessage != null)
							{
								updateBefore.irHandleMessage = entity.irHandleMessage;
							}
							if (entity.irHandleState != null)
							{
								updateBefore.irHandleState = entity.irHandleState;
							}
							if (entity.irHandleTime != null)
							{
								updateBefore.irHandleTime = entity.irHandleTime;
							}
							if (entity.irOrderNo != null)
							{
								updateBefore.irOrderNo = entity.irOrderNo;
							}
							if (entity.irPushState != null)
							{
								updateBefore.irPushState = entity.irPushState;
							}
							if (entity.irPushTime != null)
							{
								updateBefore.irPushTime = entity.irPushTime;
							}
							if (entity.irQrCodeImagePath != null)
							{
								updateBefore.irQrCodeImagePath = entity.irQrCodeImagePath;
							}
							if (entity.irRemark != null)
							{
								updateBefore.irRemark = entity.irRemark;
							}
							if (entity.irReportPicPathJson != null)
							{
								updateBefore.irReportPicPathJson = entity.irReportPicPathJson;
							}
							if (entity.irRobotId != null)
							{
								updateBefore.irRobotId = entity.irRobotId;
							}
							if (entity.irScanPayNotifyRet != null)
							{
								updateBefore.irScanPayNotifyRet = entity.irScanPayNotifyRet;
							}
							if (entity.irTakeMoney != null)
							{
								updateBefore.irTakeMoney = entity.irTakeMoney;
							}
                return myDbContext.SaveChanges();
            }
        }
    }
}