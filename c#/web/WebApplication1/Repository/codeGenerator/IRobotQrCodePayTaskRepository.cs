using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication1.Entity;
using WebApplication1.Entity.Common;
using WebApplication1.MyExtensions;
using WebApplication1.MyExtensions.Common;
using WebApplication1.Params;
using WebApplication1.OrderBy;
namespace WebApplication1.Repository
{
    public partial class IRobotQrCodePayTaskRepository : BaseRepository<IRobotQrCodePayTask>
    {
        IQueryable<IRobotQrCodePayTask> Query(IQueryable<IRobotQrCodePayTask> query, IRobotQrCodePayTaskParams irobotQrCodePayTaskParams)
        {
            if (irobotQrCodePayTaskParams != null)
            {
                if (irobotQrCodePayTaskParams.irTaskID != null)
                {
                    query = query.Where(a => a.irTaskID == irobotQrCodePayTaskParams.irTaskID);
                }
                if (irobotQrCodePayTaskParams.irOrderNo != null)
                {
                    query = query.Where(a => a.irOrderNo == irobotQrCodePayTaskParams.irOrderNo);
                }
                if (!string.IsNullOrEmpty(irobotQrCodePayTaskParams.irOrderNoLike))
                {
                    query = query.Where(a => a.irOrderNo.Contains(irobotQrCodePayTaskParams.irOrderNoLike));
                }
                if (irobotQrCodePayTaskParams.irWeiXinNickName != null)
                {
                    query = query.Where(a => a.irWeiXinNickName == irobotQrCodePayTaskParams.irWeiXinNickName);
                }
                if (!string.IsNullOrEmpty(irobotQrCodePayTaskParams.irWeiXinNickNameLike))
                {
                    query = query.Where(a => a.irWeiXinNickName.Contains(irobotQrCodePayTaskParams.irWeiXinNickNameLike));
                }
                if (irobotQrCodePayTaskParams.irWeiXinHeaderImage != null)
                {
                    query = query.Where(a => a.irWeiXinHeaderImage == irobotQrCodePayTaskParams.irWeiXinHeaderImage);
                }
                if (!string.IsNullOrEmpty(irobotQrCodePayTaskParams.irWeiXinHeaderImageLike))
                {
                    query = query.Where(a => a.irWeiXinHeaderImage.Contains(irobotQrCodePayTaskParams.irWeiXinHeaderImageLike));
                }
                if (irobotQrCodePayTaskParams.irQrCodeImagePath != null)
                {
                    query = query.Where(a => a.irQrCodeImagePath == irobotQrCodePayTaskParams.irQrCodeImagePath);
                }
                if (!string.IsNullOrEmpty(irobotQrCodePayTaskParams.irQrCodeImagePathLike))
                {
                    query = query.Where(a => a.irQrCodeImagePath.Contains(irobotQrCodePayTaskParams.irQrCodeImagePathLike));
                }
                if (irobotQrCodePayTaskParams.irHandleState != null)
                {
                    query = query.Where(a => a.irHandleState == irobotQrCodePayTaskParams.irHandleState);
                }
                if (irobotQrCodePayTaskParams.irHandleMessage != null)
                {
                    query = query.Where(a => a.irHandleMessage == irobotQrCodePayTaskParams.irHandleMessage);
                }
                if (!string.IsNullOrEmpty(irobotQrCodePayTaskParams.irHandleMessageLike))
                {
                    query = query.Where(a => a.irHandleMessage.Contains(irobotQrCodePayTaskParams.irHandleMessageLike));
                }
                if (irobotQrCodePayTaskParams.irHandleTime != null)
                {
                    query = query.Where(a => a.irHandleTime == irobotQrCodePayTaskParams.irHandleTime);
                }
                if (irobotQrCodePayTaskParams.irHandleTimeStart != null)
                {
                    query = query.Where(a => a.irHandleTime >= irobotQrCodePayTaskParams.irHandleTimeStart);
                }
                if (irobotQrCodePayTaskParams.irHandleTimeEnd != null)
                {
                    query = query.Where(a => a.irHandleTime <= irobotQrCodePayTaskParams.irHandleTimeEnd);
                }
                if (irobotQrCodePayTaskParams.irCreateTime != null)
                {
                    query = query.Where(a => a.irCreateTime == irobotQrCodePayTaskParams.irCreateTime);
                }
                if (irobotQrCodePayTaskParams.irCreateTimeStart != null)
                {
                    query = query.Where(a => a.irCreateTime >= irobotQrCodePayTaskParams.irCreateTimeStart);
                }
                if (irobotQrCodePayTaskParams.irCreateTimeEnd != null)
                {
                    query = query.Where(a => a.irCreateTime <= irobotQrCodePayTaskParams.irCreateTimeEnd);
                }
                if (irobotQrCodePayTaskParams.irReportPicPathJson != null)
                {
                    query = query.Where(a => a.irReportPicPathJson == irobotQrCodePayTaskParams.irReportPicPathJson);
                }
                if (irobotQrCodePayTaskParams.irReportPicPathJsonLike != null)
                {
                    query = query.Where(a => a.irReportPicPathJson.Contains(irobotQrCodePayTaskParams.irReportPicPathJsonLike));
                }
                if (irobotQrCodePayTaskParams.irTakeMoney != null)
                {
                    query = query.Where(a => a.irTakeMoney == irobotQrCodePayTaskParams.irTakeMoney);
                }
                if (irobotQrCodePayTaskParams.irTakeMoneyStart != null)
                {
                    query = query.Where(a => a.irTakeMoney >= irobotQrCodePayTaskParams.irTakeMoneyStart);
                }
                if (irobotQrCodePayTaskParams.irTakeMoneyEnd != null)
                {
                    query = query.Where(a => a.irTakeMoney <= irobotQrCodePayTaskParams.irTakeMoneyEnd);
                }
                if (irobotQrCodePayTaskParams.irRobotId != null)
                {
                    query = query.Where(a => a.irRobotId == irobotQrCodePayTaskParams.irRobotId);
                }
                if (irobotQrCodePayTaskParams.irRobotIdLike != null)
                {
                    query = query.Where(a => a.irRobotId.Contains(irobotQrCodePayTaskParams.irRobotIdLike));
                }
                if (irobotQrCodePayTaskParams.irRemark != null)
                {
                    query = query.Where(a => a.irRemark == irobotQrCodePayTaskParams.irRemark);
                }
                if (irobotQrCodePayTaskParams.irRemarkLike != null)
                {
                    query = query.Where(a => a.irRemark.Contains(irobotQrCodePayTaskParams.irRemarkLike));
                }
                if (irobotQrCodePayTaskParams.irPushState != null)
                {
                    query = query.Where(a => a.irPushState == irobotQrCodePayTaskParams.irPushState);
                }
                if (irobotQrCodePayTaskParams.irPushStateLike != null)
                {
                    query = query.Where(a => a.irPushState.Contains(irobotQrCodePayTaskParams.irPushStateLike));
                }
                if (irobotQrCodePayTaskParams.irPushTime != null)
                {
                    query = query.Where(a => a.irPushTime == irobotQrCodePayTaskParams.irPushTime);
                }
                if (irobotQrCodePayTaskParams.irPushTimeStart != null)
                {
                    query = query.Where(a => a.irPushTime >= irobotQrCodePayTaskParams.irPushTimeStart);
                }
                if (irobotQrCodePayTaskParams.irPushTimeEnd != null)
                {
                    query = query.Where(a => a.irPushTime <= irobotQrCodePayTaskParams.irPushTimeEnd);
                }
                if (irobotQrCodePayTaskParams.irScanPayNotifyRet != null)
                {
                    query = query.Where(a => a.irScanPayNotifyRet == irobotQrCodePayTaskParams.irScanPayNotifyRet);
                }
                if (irobotQrCodePayTaskParams.irScanPayNotifyRetLike != null)
                {
                    query = query.Where(a => a.irScanPayNotifyRet.Contains(irobotQrCodePayTaskParams.irScanPayNotifyRetLike));
                }
            }
            return query;
        }

        IQueryable<IRobotQrCodePayTask> OrderByAsc(IQueryable<IRobotQrCodePayTask> query, IRobotQrCodePayTaskOrderBy orderBy)
        {
            if (orderBy != null)
            {
                if (orderBy.irTaskID) { query = query.OrderBy(a => a.irTaskID); }
                if (orderBy.irOrderNo) { query = query.OrderBy(a => a.irOrderNo); }
                if (orderBy.irWeiXinNickName) { query = query.OrderBy(a => a.irWeiXinNickName); }
                if (orderBy.irWeiXinHeaderImage) { query = query.OrderBy(a => a.irWeiXinHeaderImage); }
                if (orderBy.irQrCodeImagePath) { query = query.OrderBy(a => a.irQrCodeImagePath); }
                if (orderBy.irHandleState) { query = query.OrderBy(a => a.irHandleState); }
                if (orderBy.irHandleMessage) { query = query.OrderBy(a => a.irHandleMessage); }
                if (orderBy.irHandleTime) { query = query.OrderBy(a => a.irHandleTime); }
                if (orderBy.irCreateTime) { query = query.OrderBy(a => a.irCreateTime); }
                if (orderBy.irReportPicPathJson) { query = query.OrderBy(a => a.irReportPicPathJson); }
                if (orderBy.irTakeMoney) { query = query.OrderBy(a => a.irTakeMoney); }
                if (orderBy.irRobotId) { query = query.OrderBy(a => a.irRobotId); }
                if (orderBy.irRemark) { query = query.OrderBy(a => a.irRemark); }
                if (orderBy.irPushState) { query = query.OrderBy(a => a.irPushState); }
                if (orderBy.irPushTime) { query = query.OrderBy(a => a.irPushTime); }
                if (orderBy.irScanPayNotifyRet) { query = query.OrderBy(a => a.irScanPayNotifyRet); }
            }
            return query;
        }

        IQueryable<IRobotQrCodePayTask> OrderByDesc(IQueryable<IRobotQrCodePayTask> query, IRobotQrCodePayTaskOrderBy orderBy)
        {
            if (orderBy != null)
            {
                if (orderBy.irTaskID) { query = query.OrderByDescending(a => a.irTaskID); }
                if (orderBy.irOrderNo) { query = query.OrderByDescending(a => a.irOrderNo); }
                if (orderBy.irWeiXinNickName) { query = query.OrderByDescending(a => a.irWeiXinNickName); }
                if (orderBy.irWeiXinHeaderImage) { query = query.OrderByDescending(a => a.irWeiXinHeaderImage); }
                if (orderBy.irQrCodeImagePath) { query = query.OrderByDescending(a => a.irQrCodeImagePath); }
                if (orderBy.irHandleState) { query = query.OrderByDescending(a => a.irHandleState); }
                if (orderBy.irHandleMessage) { query = query.OrderByDescending(a => a.irHandleMessage); }
                if (orderBy.irHandleTime) { query = query.OrderByDescending(a => a.irHandleTime); }
                if (orderBy.irCreateTime) { query = query.OrderByDescending(a => a.irCreateTime); }
                if (orderBy.irReportPicPathJson) { query = query.OrderByDescending(a => a.irReportPicPathJson); }
                if (orderBy.irTakeMoney) { query = query.OrderByDescending(a => a.irTakeMoney); }
                if (orderBy.irRobotId) { query = query.OrderByDescending(a => a.irRobotId); }
                if (orderBy.irRemark) { query = query.OrderByDescending(a => a.irRemark); }
                if (orderBy.irPushState) { query = query.OrderByDescending(a => a.irPushState); }
                if (orderBy.irPushTime) { query = query.OrderByDescending(a => a.irPushTime); }
                if (orderBy.irScanPayNotifyRet) { query = query.OrderByDescending(a => a.irScanPayNotifyRet); }
            }
            else
            {
                query = query.OrderByDescending(a => a.irTaskID);
            }
            return query;
        }

        public MyPagedList<IRobotQrCodePayTask> BigPageList(IRobotQrCodePayTaskParams irobotQrCodePayTaskParams, int pageIndex, int pageSize)
        {
            using (MyDbContext2 myDbContext2 = new MyDbContext2())
            {
                IQueryable<IRobotQrCodePayTask> query = Query(myDbContext2.IRobotQrCodePayTasks.AsNoTracking().AsQueryable(), irobotQrCodePayTaskParams);
                query = OrderByAsc(query, irobotQrCodePayTaskParams.orderByAsc);
                query = OrderByDesc(query, irobotQrCodePayTaskParams.orderByDesc);
                int totalItemCount = query.Count();
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
                    TotalPageCount = (totalItemCount - totalItemCount % pageSize) / pageSize + 1,
                    StartItemIndex = (pageSize - 1) * pageIndex + 1,
                    EndItemIndex = pageSize * pageIndex
                };
            }
        }

        public MyPagedList<IRobotQrCodePayTask> PageList(IRobotQrCodePayTaskParams irobotQrCodePayTaskParams, int pageIndex, int pageSize)
        {
            using (MyDbContext2 myDbContext2 = new MyDbContext2())
            {
                IQueryable<IRobotQrCodePayTask> query = Query(myDbContext2.IRobotQrCodePayTasks.AsNoTracking().AsQueryable(), irobotQrCodePayTaskParams);
                query = OrderByAsc(query, irobotQrCodePayTaskParams.orderByAsc);
                query = OrderByDesc(query, irobotQrCodePayTaskParams.orderByDesc);
                return query.ToMyPagedList(pageIndex, pageSize);
            }
        }

        public int Count(IRobotQrCodePayTaskParams irobotQrCodePayTaskParams)
        {
            using (MyDbContext2 myDbContext2 = new MyDbContext2())
            {
                IQueryable<IRobotQrCodePayTask> query = Query(myDbContext2.IRobotQrCodePayTasks.AsNoTracking().AsQueryable(), irobotQrCodePayTaskParams);
                return query.Count();
            }
        }
        
        public List<IRobotQrCodePayTask> FindList(IRobotQrCodePayTaskParams irobotQrCodePayTaskParams = null)
        {
            using (MyDbContext2 myDbContext2 = new MyDbContext2())
            {
                IQueryable<IRobotQrCodePayTask> query = Query(myDbContext2.IRobotQrCodePayTasks.AsNoTracking().AsQueryable(), irobotQrCodePayTaskParams);
                return query.ToList();
            }
        }

        public int UpdateChange(IRobotQrCodePayTask irobotQrCodePayTask)
        {
            using (MyDbContext2 myDbContext2 = new MyDbContext2())
            {
                IRobotQrCodePayTask updateBefore = new IRobotQrCodePayTask { irTaskID = irobotQrCodePayTask.irTaskID};
                myDbContext2.IRobotQrCodePayTasks.Attach(updateBefore);
                if (irobotQrCodePayTask.irTaskID != null)
                {
                    updateBefore.irTaskID = irobotQrCodePayTask.irTaskID;
                }
                if (irobotQrCodePayTask.irOrderNo != null)
                {
                    updateBefore.irOrderNo = irobotQrCodePayTask.irOrderNo;
                }
                if (irobotQrCodePayTask.irWeiXinNickName != null)
                {
                    updateBefore.irWeiXinNickName = irobotQrCodePayTask.irWeiXinNickName;
                }
                if (irobotQrCodePayTask.irWeiXinHeaderImage != null)
                {
                    updateBefore.irWeiXinHeaderImage = irobotQrCodePayTask.irWeiXinHeaderImage;
                }
                if (irobotQrCodePayTask.irQrCodeImagePath != null)
                {
                    updateBefore.irQrCodeImagePath = irobotQrCodePayTask.irQrCodeImagePath;
                }
                if (irobotQrCodePayTask.irHandleState != null)
                {
                    updateBefore.irHandleState = irobotQrCodePayTask.irHandleState;
                }
                if (irobotQrCodePayTask.irHandleMessage != null)
                {
                    updateBefore.irHandleMessage = irobotQrCodePayTask.irHandleMessage;
                }
                if (irobotQrCodePayTask.irHandleTime != null)
                {
                    updateBefore.irHandleTime = irobotQrCodePayTask.irHandleTime;
                }
                if (irobotQrCodePayTask.irCreateTime != null)
                {
                    updateBefore.irCreateTime = irobotQrCodePayTask.irCreateTime;
                }
                if (irobotQrCodePayTask.irReportPicPathJson != null)
                {
                    updateBefore.irReportPicPathJson = irobotQrCodePayTask.irReportPicPathJson;
                }
                if (irobotQrCodePayTask.irTakeMoney != null)
                {
                    updateBefore.irTakeMoney = irobotQrCodePayTask.irTakeMoney;
                }
                if (irobotQrCodePayTask.irRobotId != null)
                {
                    updateBefore.irRobotId = irobotQrCodePayTask.irRobotId;
                }
                if (irobotQrCodePayTask.irRemark != null)
                {
                    updateBefore.irRemark = irobotQrCodePayTask.irRemark;
                }
                if (irobotQrCodePayTask.irPushState != null)
                {
                    updateBefore.irPushState = irobotQrCodePayTask.irPushState;
                }
                if (irobotQrCodePayTask.irPushTime != null)
                {
                    updateBefore.irPushTime = irobotQrCodePayTask.irPushTime;
                }
                if (irobotQrCodePayTask.irScanPayNotifyRet != null)
                {
                    updateBefore.irScanPayNotifyRet = irobotQrCodePayTask.irScanPayNotifyRet;
                }
                return myDbContext2.SaveChanges();
            }
        }
    }
}