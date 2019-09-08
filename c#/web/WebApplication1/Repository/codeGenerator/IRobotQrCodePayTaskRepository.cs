using System;
using CommonHelper.CommonEntity;
using CommonHelper.Helper;
using CommonHelper.Helper.EFDbContext;
using CommonHelper.Helper.EFRepository;
using CommonHelper.AopInterceptor;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication1.Entity;
using WebApplication1.Params;
using CommonHelper.staticVar;
using CommonHelper.EFRepository;
using System.Linq.Expressions;
using WebApplication1.OrderBy;

namespace WebApplication1.Repository
{
    //启用分布式事务
    //[Intercept(typeof(DistributeRepository))]
    public partial class IRobotQrCodePayTaskRepository : BaseRepository<IRobotQrCodePayTask, IRobotQrCodePayTaskParams, IRobotQrCodePayTaskSetNullParams>
    {
        /// <summary>
        /// 通用的设置查询参数方法，只有在参数不为null的情况下才会设置，
        /// 满足项目中常用的模糊查询，分页查询等功能
        /// </summary>
        /// <param name="query">待设置参数的query对象</param>
        /// <param name="eqArgs">装载查询参数的实体类</param>
        /// <param name="neqArgs">装载不等于查询参数的实体类</param>
        /// <returns>返回设置好查询参数的query对象</returns>
        protected override IQueryable<IRobotQrCodePayTask> Query(IQueryable<IRobotQrCodePayTask> query, IRobotQrCodePayTaskParams eqArgs, IRobotQrCodePayTaskParams neqArgs = null)
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
                query = OrderByAsc(query, eqArgs.orderByAsc);
                query = OrderByDesc(query, eqArgs.orderByDesc);
            }
            if (neqArgs != null)
            {

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
                else
                if (orderBy.IROrderNo) { query = query.OrderBy(a => a.IROrderNo); }
                else
                if (orderBy.IRWeiXinNickName) { query = query.OrderBy(a => a.IRWeiXinNickName); }
                else
                if (orderBy.IRWeiXinHeaderImage) { query = query.OrderBy(a => a.IRWeiXinHeaderImage); }
                else
                if (orderBy.IRQrCodeImagePath) { query = query.OrderBy(a => a.IRQrCodeImagePath); }
                else
                if (orderBy.IRHandleState) { query = query.OrderBy(a => a.IRHandleState); }
                else
                if (orderBy.IRHandleMessage) { query = query.OrderBy(a => a.IRHandleMessage); }
                else
                if (orderBy.IRHandleTime) { query = query.OrderBy(a => a.IRHandleTime); }
                else
                if (orderBy.IRCreateTime) { query = query.OrderBy(a => a.IRCreateTime); }
                else
                if (orderBy.IRReportPicPathJson) { query = query.OrderBy(a => a.IRReportPicPathJson); }
                else
                if (orderBy.IRTakeMoney) { query = query.OrderBy(a => a.IRTakeMoney); }
                else
                if (orderBy.IRRobotId) { query = query.OrderBy(a => a.IRRobotId); }
                else
                if (orderBy.IRRemark) { query = query.OrderBy(a => a.IRRemark); }
                else
                if (orderBy.IRPushState) { query = query.OrderBy(a => a.IRPushState); }
                else
                if (orderBy.IRPushTime) { query = query.OrderBy(a => a.IRPushTime); }
                else
                if (orderBy.IRScanPayNotifyRet) { query = query.OrderBy(a => a.IRScanPayNotifyRet); }
                else
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

                if (orderBy.IRTaskID) { query = query.OrderByDescending(a => a.IRTaskID); }
                else
                if (orderBy.IROrderNo) { query = query.OrderByDescending(a => a.IROrderNo); }
                else
                if (orderBy.IRWeiXinNickName) { query = query.OrderByDescending(a => a.IRWeiXinNickName); }
                else
                if (orderBy.IRWeiXinHeaderImage) { query = query.OrderByDescending(a => a.IRWeiXinHeaderImage); }
                else
                if (orderBy.IRQrCodeImagePath) { query = query.OrderByDescending(a => a.IRQrCodeImagePath); }
                else
                if (orderBy.IRHandleState) { query = query.OrderByDescending(a => a.IRHandleState); }
                else
                if (orderBy.IRHandleMessage) { query = query.OrderByDescending(a => a.IRHandleMessage); }
                else
                if (orderBy.IRHandleTime) { query = query.OrderByDescending(a => a.IRHandleTime); }
                else
                if (orderBy.IRCreateTime) { query = query.OrderByDescending(a => a.IRCreateTime); }
                else
                if (orderBy.IRReportPicPathJson) { query = query.OrderByDescending(a => a.IRReportPicPathJson); }
                else
                if (orderBy.IRTakeMoney) { query = query.OrderByDescending(a => a.IRTakeMoney); }
                else
                if (orderBy.IRRobotId) { query = query.OrderByDescending(a => a.IRRobotId); }
                else
                if (orderBy.IRRemark) { query = query.OrderByDescending(a => a.IRRemark); }
                else
                if (orderBy.IRPushState) { query = query.OrderByDescending(a => a.IRPushState); }
                else
                if (orderBy.IRPushTime) { query = query.OrderByDescending(a => a.IRPushTime); }
                else
                if (orderBy.IRScanPayNotifyRet) { query = query.OrderByDescending(a => a.IRScanPayNotifyRet); }
                else
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
        /// 修改变化值的部分共用代码，这里抽取出来简化结构
        /// </summary>
        /// <param name="updateBefore"></param>
        /// <param name="entity"></param>
        protected override void UpdateChange(BaseDbContext dbContext, IRobotQrCodePayTask entity)
        {
            IRobotQrCodePayTask updateBefore = new IRobotQrCodePayTask { IRTaskID = entity.IRTaskID };
            dbContext.Set<IRobotQrCodePayTask>().Attach(updateBefore);
            if (entity.IROrderNo != null)
            {
                updateBefore.IROrderNo = entity.IROrderNo;
            }
            if (entity.IRWeiXinNickName != null)
            {
                updateBefore.IRWeiXinNickName = entity.IRWeiXinNickName;
            }
            if (entity.IRWeiXinHeaderImage != null)
            {
                updateBefore.IRWeiXinHeaderImage = entity.IRWeiXinHeaderImage;
            }
            if (entity.IRQrCodeImagePath != null)
            {
                updateBefore.IRQrCodeImagePath = entity.IRQrCodeImagePath;
            }
            if (entity.IRHandleState != null)
            {
                updateBefore.IRHandleState = entity.IRHandleState;
            }
            if (entity.IRHandleMessage != null)
            {
                updateBefore.IRHandleMessage = entity.IRHandleMessage;
            }
            if (entity.IRHandleTime != null)
            {
                updateBefore.IRHandleTime = entity.IRHandleTime;
            }
            if (entity.IRCreateTime != null)
            {
                updateBefore.IRCreateTime = entity.IRCreateTime;
            }
            if (entity.IRReportPicPathJson != null)
            {
                updateBefore.IRReportPicPathJson = entity.IRReportPicPathJson;
            }
            if (entity.IRTakeMoney != null)
            {
                updateBefore.IRTakeMoney = entity.IRTakeMoney;
            }
            if (entity.IRRobotId != null)
            {
                updateBefore.IRRobotId = entity.IRRobotId;
            }
            if (entity.IRRemark != null)
            {
                updateBefore.IRRemark = entity.IRRemark;
            }
            if (entity.IRPushState != null)
            {
                updateBefore.IRPushState = entity.IRPushState;
            }
            if (entity.IRPushTime != null)
            {
                updateBefore.IRPushTime = entity.IRPushTime;
            }
            if (entity.IRScanPayNotifyRet != null)
            {
                updateBefore.IRScanPayNotifyRet = entity.IRScanPayNotifyRet;
            }
            if (entity.IRScanPayNotifyUrl != null)
            {
                updateBefore.IRScanPayNotifyUrl = entity.IRScanPayNotifyUrl;
            }
        }

        /// <summary>
        /// 把指定字段值设置为null的部分共用代码，这里抽取出来简化结构
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="param"></param>
        protected override void SetNull(BaseDbContext dbContext, IRobotQrCodePayTaskSetNullParams param)
        {
            IRobotQrCodePayTask updateBefore = FindEntity(param.IRTaskID);
            dbContext.Set<IRobotQrCodePayTask>().Attach(updateBefore);
            if (param.IROrderNo)
            {
                updateBefore.IROrderNo = null;
            }
            if (param.IRWeiXinNickName)
            {
                updateBefore.IRWeiXinNickName = null;
            }
            if (param.IRWeiXinHeaderImage)
            {
                updateBefore.IRWeiXinHeaderImage = null;
            }
            if (param.IRQrCodeImagePath)
            {
                updateBefore.IRQrCodeImagePath = null;
            }
            if (param.IRHandleState)
            {
                updateBefore.IRHandleState = null;
            }
            if (param.IRHandleMessage)
            {
                updateBefore.IRHandleMessage = null;
            }
            if (param.IRHandleTime)
            {
                updateBefore.IRHandleTime = null;
            }
            if (param.IRCreateTime)
            {
                updateBefore.IRCreateTime = null;
            }
            if (param.IRReportPicPathJson)
            {
                updateBefore.IRReportPicPathJson = null;
            }
            if (param.IRTakeMoney)
            {
                updateBefore.IRTakeMoney = null;
            }
            if (param.IRRobotId)
            {
                updateBefore.IRRobotId = null;
            }
            if (param.IRRemark)
            {
                updateBefore.IRRemark = null;
            }
            if (param.IRPushState)
            {
                updateBefore.IRPushState = null;
            }
            if (param.IRPushTime)
            {
                updateBefore.IRPushTime = null;
            }
            if (param.IRScanPayNotifyRet)
            {
                updateBefore.IRScanPayNotifyRet = null;
            }
            if (param.IRScanPayNotifyUrl)
            {
                updateBefore.IRScanPayNotifyUrl = null;
            }
        }

        /// <summary>
        /// 检查分布式事务是否已完成
        /// </summary>
        /// <param name="primaryKeyVal">主键的值</param>
        public void CheckTransactionFinish(long primaryKeyVal)
        {
            CheckTransactionFinish(primaryKeyVal, "IRobot_QrCodePayTask");
        }

        public override BaseDbContext CreateDbContext()
        {
            return new MyDbContext2();
        }

        public override InverseRepository<IRobotQrCodePayTask> CurrentInverse()
        {
            using (BaseDbContext db = CreateDbContext())
            {
                return AllStatic.InverseRepositoryMap[db.Database.Connection.ConnectionString]["IRobot_QrCodePayTask"];
            }
        }

        public override List<BaseDbContext> CreateAllDbContext()
        {
            List<BaseDbContext> baseDbContextList = new List<BaseDbContext>();
            baseDbContextList.Add(new MyDbContext2());
            return baseDbContextList;
        }

        /// <summary>
        /// 获取跨库查询所需的表达式
        /// </summary>
        /// <returns></returns>
        protected override DisPageEntity<IRobotQrCodePayTask> GetDisPageEntity(IRobotQrCodePayTaskParams paramz)
        {
            DisPageEntity<IRobotQrCodePayTask> disPageEntity = new DisPageEntity<IRobotQrCodePayTask>();
            IRobotQrCodePayTaskOrderBy orderBy = paramz.orderByAsc;
            if (orderBy != null)
            {
                if (orderBy.IRTaskID)
                {
                    disPageEntity.OrderCol = a => a.IRTaskID;
                    disPageEntity.OrderColLazy = a => a.IRTaskID;
                    disPageEntity.OrderType = true;
                }
                if (orderBy.IROrderNo)
                {
                    disPageEntity.OrderCol = a => a.IROrderNo;
                    disPageEntity.OrderColLazy = a => a.IROrderNo;
                    disPageEntity.OrderType = true;
                }
                if (orderBy.IRWeiXinNickName)
                {
                    disPageEntity.OrderCol = a => a.IRWeiXinNickName;
                    disPageEntity.OrderColLazy = a => a.IRWeiXinNickName;
                    disPageEntity.OrderType = true;
                }
                if (orderBy.IRWeiXinHeaderImage)
                {
                    disPageEntity.OrderCol = a => a.IRWeiXinHeaderImage;
                    disPageEntity.OrderColLazy = a => a.IRWeiXinHeaderImage;
                    disPageEntity.OrderType = true;
                }
                if (orderBy.IRQrCodeImagePath)
                {
                    disPageEntity.OrderCol = a => a.IRQrCodeImagePath;
                    disPageEntity.OrderColLazy = a => a.IRQrCodeImagePath;
                    disPageEntity.OrderType = true;
                }
                if (orderBy.IRHandleState)
                {
                    disPageEntity.OrderCol = a => a.IRHandleState;
                    disPageEntity.OrderColLazy = a => a.IRHandleState;
                    disPageEntity.OrderType = true;
                }
                if (orderBy.IRHandleMessage)
                {
                    disPageEntity.OrderCol = a => a.IRHandleMessage;
                    disPageEntity.OrderColLazy = a => a.IRHandleMessage;
                    disPageEntity.OrderType = true;
                }
                if (orderBy.IRHandleTime)
                {
                    disPageEntity.OrderCol = a => a.IRHandleTime;
                    disPageEntity.OrderColLazy = a => a.IRHandleTime;
                    disPageEntity.OrderType = true;
                }
                if (orderBy.IRCreateTime)
                {
                    disPageEntity.OrderCol = a => a.IRCreateTime;
                    disPageEntity.OrderColLazy = a => a.IRCreateTime;
                    disPageEntity.OrderType = true;
                }
                if (orderBy.IRReportPicPathJson)
                {
                    disPageEntity.OrderCol = a => a.IRReportPicPathJson;
                    disPageEntity.OrderColLazy = a => a.IRReportPicPathJson;
                    disPageEntity.OrderType = true;
                }
                if (orderBy.IRTakeMoney)
                {
                    disPageEntity.OrderCol = a => a.IRTakeMoney;
                    disPageEntity.OrderColLazy = a => a.IRTakeMoney;
                    disPageEntity.OrderType = true;
                }
                if (orderBy.IRRobotId)
                {
                    disPageEntity.OrderCol = a => a.IRRobotId;
                    disPageEntity.OrderColLazy = a => a.IRRobotId;
                    disPageEntity.OrderType = true;
                }
                if (orderBy.IRRemark)
                {
                    disPageEntity.OrderCol = a => a.IRRemark;
                    disPageEntity.OrderColLazy = a => a.IRRemark;
                    disPageEntity.OrderType = true;
                }
                if (orderBy.IRPushState)
                {
                    disPageEntity.OrderCol = a => a.IRPushState;
                    disPageEntity.OrderColLazy = a => a.IRPushState;
                    disPageEntity.OrderType = true;
                }
                if (orderBy.IRPushTime)
                {
                    disPageEntity.OrderCol = a => a.IRPushTime;
                    disPageEntity.OrderColLazy = a => a.IRPushTime;
                    disPageEntity.OrderType = true;
                }
                if (orderBy.IRScanPayNotifyRet)
                {
                    disPageEntity.OrderCol = a => a.IRScanPayNotifyRet;
                    disPageEntity.OrderColLazy = a => a.IRScanPayNotifyRet;
                    disPageEntity.OrderType = true;
                }
                if (orderBy.IRScanPayNotifyUrl)
                {
                    disPageEntity.OrderCol = a => a.IRScanPayNotifyUrl;
                    disPageEntity.OrderColLazy = a => a.IRScanPayNotifyUrl;
                    disPageEntity.OrderType = true;
                }
            }
            else
            {
                orderBy = paramz.orderByDesc;
                if (orderBy != null)
                {

                    if (orderBy.IRTaskID)
                    {
                        disPageEntity.OrderCol = a => a.IRTaskID;
                        disPageEntity.OrderColLazy = a => a.IRTaskID;
                        disPageEntity.OrderType = false;
                    }
                    else
                    if (orderBy.IROrderNo)
                    {
                        disPageEntity.OrderCol = a => a.IROrderNo;
                        disPageEntity.OrderColLazy = a => a.IROrderNo;
                        disPageEntity.OrderType = false;
                    }
                    else
                    if (orderBy.IRWeiXinNickName)
                    {
                        disPageEntity.OrderCol = a => a.IRWeiXinNickName;
                        disPageEntity.OrderColLazy = a => a.IRWeiXinNickName;
                        disPageEntity.OrderType = false;
                    }
                    else
                    if (orderBy.IRWeiXinHeaderImage)
                    {
                        disPageEntity.OrderCol = a => a.IRWeiXinHeaderImage;
                        disPageEntity.OrderColLazy = a => a.IRWeiXinHeaderImage;
                        disPageEntity.OrderType = false;
                    }
                    else
                    if (orderBy.IRQrCodeImagePath)
                    {
                        disPageEntity.OrderCol = a => a.IRQrCodeImagePath;
                        disPageEntity.OrderColLazy = a => a.IRQrCodeImagePath;
                        disPageEntity.OrderType = false;
                    }
                    else
                    if (orderBy.IRHandleState)
                    {
                        disPageEntity.OrderCol = a => a.IRHandleState;
                        disPageEntity.OrderColLazy = a => a.IRHandleState;
                        disPageEntity.OrderType = false;
                    }
                    else
                    if (orderBy.IRHandleMessage)
                    {
                        disPageEntity.OrderCol = a => a.IRHandleMessage;
                        disPageEntity.OrderColLazy = a => a.IRHandleMessage;
                        disPageEntity.OrderType = false;
                    }
                    else
                    if (orderBy.IRHandleTime)
                    {
                        disPageEntity.OrderCol = a => a.IRHandleTime;
                        disPageEntity.OrderColLazy = a => a.IRHandleTime;
                        disPageEntity.OrderType = false;
                    }
                    else
                    if (orderBy.IRCreateTime)
                    {
                        disPageEntity.OrderCol = a => a.IRCreateTime;
                        disPageEntity.OrderColLazy = a => a.IRCreateTime;
                        disPageEntity.OrderType = false;
                    }
                    else
                    if (orderBy.IRReportPicPathJson)
                    {
                        disPageEntity.OrderCol = a => a.IRReportPicPathJson;
                        disPageEntity.OrderColLazy = a => a.IRReportPicPathJson;
                        disPageEntity.OrderType = false;
                    }
                    else
                    if (orderBy.IRTakeMoney)
                    {
                        disPageEntity.OrderCol = a => a.IRTakeMoney;
                        disPageEntity.OrderColLazy = a => a.IRTakeMoney;
                        disPageEntity.OrderType = false;
                    }
                    else
                    if (orderBy.IRRobotId)
                    {
                        disPageEntity.OrderCol = a => a.IRRobotId;
                        disPageEntity.OrderColLazy = a => a.IRRobotId;
                        disPageEntity.OrderType = false;
                    }
                    else
                    if (orderBy.IRRemark)
                    {
                        disPageEntity.OrderCol = a => a.IRRemark;
                        disPageEntity.OrderColLazy = a => a.IRRemark;
                        disPageEntity.OrderType = false;
                    }
                    else
                    if (orderBy.IRPushState)
                    {
                        disPageEntity.OrderCol = a => a.IRPushState;
                        disPageEntity.OrderColLazy = a => a.IRPushState;
                        disPageEntity.OrderType = false;
                    }
                    else
                    if (orderBy.IRPushTime)
                    {
                        disPageEntity.OrderCol = a => a.IRPushTime;
                        disPageEntity.OrderColLazy = a => a.IRPushTime;
                        disPageEntity.OrderType = false;
                    }
                    else
                    if (orderBy.IRScanPayNotifyRet)
                    {
                        disPageEntity.OrderCol = a => a.IRScanPayNotifyRet;
                        disPageEntity.OrderColLazy = a => a.IRScanPayNotifyRet;
                        disPageEntity.OrderType = false;
                    }
                    else
                    if (orderBy.IRScanPayNotifyUrl)
                    {
                        disPageEntity.OrderCol = a => a.IRScanPayNotifyUrl;
                        disPageEntity.OrderColLazy = a => a.IRScanPayNotifyUrl;
                        disPageEntity.OrderType = false;
                    }
                    else
                    {
                        disPageEntity.OrderCol = a => a.IRTaskID;
                        disPageEntity.OrderColLazy = a => a.IRTaskID;
                        disPageEntity.OrderType = false;
                    }
                }
                else
                {
                    disPageEntity.OrderCol = a => a.IRTaskID;
                    disPageEntity.OrderColLazy = a => a.IRTaskID;
                    disPageEntity.OrderType = false;
                }
            }
            return disPageEntity;
        }

        /// <summary>
        /// 跨库分页时，基准值查询
        /// </summary>
        /// <param name="paramz">查询参数，判断排序的重要依据</param>
        /// <param name="baseVal">基准值，缩小查询范围</param>
        /// <param name="queryType">查询类型，0：查询基准值跳过的数据量，1：查询比基准值大，并当中含有合适的基准值</param>
        /// <returns></returns>
        protected override Expression<Func<IRobotQrCodePayTask, bool>> BaseValQuery(IRobotQrCodePayTaskParams paramz, IComparable baseVal, int queryType)
        {
            IRobotQrCodePayTaskOrderBy orderBy = paramz.orderByAsc;
            if (orderBy != null)
            {
                if (orderBy.IRTaskID)
                {
                    if (queryType == 0)
                    {
                        return a => baseVal.CompareTo(a.IRTaskID) > 0;
                    }
                    else if (queryType == 1)
                    {
                        return a => baseVal.CompareTo(a.IRTaskID) < 0;
                    }
                }
                if (orderBy.IROrderNo)
                {
                    if (queryType == 0)
                    {
                        return a => baseVal.CompareTo(a.IROrderNo) > 0;
                    }
                    else if (queryType == 1)
                    {
                        return a => baseVal.CompareTo(a.IROrderNo) < 0;
                    }
                }
                if (orderBy.IRWeiXinNickName)
                {
                    if (queryType == 0)
                    {
                        return a => baseVal.CompareTo(a.IRWeiXinNickName) > 0;
                    }
                    else if (queryType == 1)
                    {
                        return a => baseVal.CompareTo(a.IRWeiXinNickName) < 0;
                    }
                }
                if (orderBy.IRWeiXinHeaderImage)
                {
                    if (queryType == 0)
                    {
                        return a => baseVal.CompareTo(a.IRWeiXinHeaderImage) > 0;
                    }
                    else if (queryType == 1)
                    {
                        return a => baseVal.CompareTo(a.IRWeiXinHeaderImage) < 0;
                    }
                }
                if (orderBy.IRQrCodeImagePath)
                {
                    if (queryType == 0)
                    {
                        return a => baseVal.CompareTo(a.IRQrCodeImagePath) > 0;
                    }
                    else if (queryType == 1)
                    {
                        return a => baseVal.CompareTo(a.IRQrCodeImagePath) < 0;
                    }
                }
                if (orderBy.IRHandleState)
                {
                    if (queryType == 0)
                    {
                        return a => baseVal.CompareTo(a.IRHandleState) > 0;
                    }
                    else if (queryType == 1)
                    {
                        return a => baseVal.CompareTo(a.IRHandleState) < 0;
                    }
                }
                if (orderBy.IRHandleMessage)
                {
                    if (queryType == 0)
                    {
                        return a => baseVal.CompareTo(a.IRHandleMessage) > 0;
                    }
                    else if (queryType == 1)
                    {
                        return a => baseVal.CompareTo(a.IRHandleMessage) < 0;
                    }
                }
                if (orderBy.IRHandleTime)
                {
                    if (queryType == 0)
                    {
                        return a => baseVal.CompareTo(a.IRHandleTime) > 0;
                    }
                    else if (queryType == 1)
                    {
                        return a => baseVal.CompareTo(a.IRHandleTime) < 0;
                    }
                }
                if (orderBy.IRCreateTime)
                {
                    if (queryType == 0)
                    {
                        return a => baseVal.CompareTo(a.IRCreateTime) > 0;
                    }
                    else if (queryType == 1)
                    {
                        return a => baseVal.CompareTo(a.IRCreateTime) < 0;
                    }
                }
                if (orderBy.IRReportPicPathJson)
                {
                    if (queryType == 0)
                    {
                        return a => baseVal.CompareTo(a.IRReportPicPathJson) > 0;
                    }
                    else if (queryType == 1)
                    {
                        return a => baseVal.CompareTo(a.IRReportPicPathJson) < 0;
                    }
                }
                if (orderBy.IRTakeMoney)
                {
                    if (queryType == 0)
                    {
                        return a => baseVal.CompareTo(a.IRTakeMoney) > 0;
                    }
                    else if (queryType == 1)
                    {
                        return a => baseVal.CompareTo(a.IRTakeMoney) < 0;
                    }
                }
                if (orderBy.IRRobotId)
                {
                    if (queryType == 0)
                    {
                        return a => baseVal.CompareTo(a.IRRobotId) > 0;
                    }
                    else if (queryType == 1)
                    {
                        return a => baseVal.CompareTo(a.IRRobotId) < 0;
                    }
                }
                if (orderBy.IRRemark)
                {
                    if (queryType == 0)
                    {
                        return a => baseVal.CompareTo(a.IRRemark) > 0;
                    }
                    else if (queryType == 1)
                    {
                        return a => baseVal.CompareTo(a.IRRemark) < 0;
                    }
                }
                if (orderBy.IRPushState)
                {
                    if (queryType == 0)
                    {
                        return a => baseVal.CompareTo(a.IRPushState) > 0;
                    }
                    else if (queryType == 1)
                    {
                        return a => baseVal.CompareTo(a.IRPushState) < 0;
                    }
                }
                if (orderBy.IRPushTime)
                {
                    if (queryType == 0)
                    {
                        return a => baseVal.CompareTo(a.IRPushTime) > 0;
                    }
                    else if (queryType == 1)
                    {
                        return a => baseVal.CompareTo(a.IRPushTime) < 0;
                    }
                }
                if (orderBy.IRScanPayNotifyRet)
                {
                    if (queryType == 0)
                    {
                        return a => baseVal.CompareTo(a.IRScanPayNotifyRet) > 0;
                    }
                    else if (queryType == 1)
                    {
                        return a => baseVal.CompareTo(a.IRScanPayNotifyRet) < 0;
                    }
                }
                if (orderBy.IRScanPayNotifyUrl)
                {
                    if (queryType == 0)
                    {
                        return a => baseVal.CompareTo(a.IRScanPayNotifyUrl) > 0;
                    }
                    else if (queryType == 1)
                    {
                        return a => baseVal.CompareTo(a.IRScanPayNotifyUrl) < 0;
                    }
                }
            }
            else
            {
                orderBy = paramz.orderByDesc;
                if (orderBy != null)
                {
                    if (orderBy.IRTaskID)
                    {
                        if (queryType == 0)
                        {
                            return a => baseVal.CompareTo(a.IRTaskID) < 0;
                        }
                        else if (queryType == 1)
                        {
                            return a => baseVal.CompareTo(a.IRTaskID) > 0;
                        }
                    }
                    else if (orderBy.IROrderNo)
                    {
                        if (queryType == 0)
                        {
                            return a => baseVal.CompareTo(a.IROrderNo) < 0;
                        }
                        else if (queryType == 1)
                        {
                            return a => baseVal.CompareTo(a.IROrderNo) > 0;
                        }
                    }
                    else if (orderBy.IRWeiXinNickName)
                    {
                        if (queryType == 0)
                        {
                            return a => baseVal.CompareTo(a.IRWeiXinNickName) < 0;
                        }
                        else if (queryType == 1)
                        {
                            return a => baseVal.CompareTo(a.IRWeiXinNickName) > 0;
                        }
                    }
                    else if (orderBy.IRWeiXinHeaderImage)
                    {
                        if (queryType == 0)
                        {
                            return a => baseVal.CompareTo(a.IRWeiXinHeaderImage) < 0;
                        }
                        else if (queryType == 1)
                        {
                            return a => baseVal.CompareTo(a.IRWeiXinHeaderImage) > 0;
                        }
                    }
                    else if (orderBy.IRQrCodeImagePath)
                    {
                        if (queryType == 0)
                        {
                            return a => baseVal.CompareTo(a.IRQrCodeImagePath) < 0;
                        }
                        else if (queryType == 1)
                        {
                            return a => baseVal.CompareTo(a.IRQrCodeImagePath) > 0;
                        }
                    }
                    else if (orderBy.IRHandleState)
                    {
                        if (queryType == 0)
                        {
                            return a => baseVal.CompareTo(a.IRHandleState) < 0;
                        }
                        else if (queryType == 1)
                        {
                            return a => baseVal.CompareTo(a.IRHandleState) > 0;
                        }
                    }
                    else if (orderBy.IRHandleMessage)
                    {
                        if (queryType == 0)
                        {
                            return a => baseVal.CompareTo(a.IRHandleMessage) < 0;
                        }
                        else if (queryType == 1)
                        {
                            return a => baseVal.CompareTo(a.IRHandleMessage) > 0;
                        }
                    }
                    else if (orderBy.IRHandleTime)
                    {
                        if (queryType == 0)
                        {
                            return a => baseVal.CompareTo(a.IRHandleTime) < 0;
                        }
                        else if (queryType == 1)
                        {
                            return a => baseVal.CompareTo(a.IRHandleTime) > 0;
                        }
                    }
                    else if (orderBy.IRCreateTime)
                    {
                        if (queryType == 0)
                        {
                            return a => baseVal.CompareTo(a.IRCreateTime) < 0;
                        }
                        else if (queryType == 1)
                        {
                            return a => baseVal.CompareTo(a.IRCreateTime) > 0;
                        }
                    }
                    else if (orderBy.IRReportPicPathJson)
                    {
                        if (queryType == 0)
                        {
                            return a => baseVal.CompareTo(a.IRReportPicPathJson) < 0;
                        }
                        else if (queryType == 1)
                        {
                            return a => baseVal.CompareTo(a.IRReportPicPathJson) > 0;
                        }
                    }
                    else if (orderBy.IRTakeMoney)
                    {
                        if (queryType == 0)
                        {
                            return a => baseVal.CompareTo(a.IRTakeMoney) < 0;
                        }
                        else if (queryType == 1)
                        {
                            return a => baseVal.CompareTo(a.IRTakeMoney) > 0;
                        }
                    }
                    else if (orderBy.IRRobotId)
                    {
                        if (queryType == 0)
                        {
                            return a => baseVal.CompareTo(a.IRRobotId) < 0;
                        }
                        else if (queryType == 1)
                        {
                            return a => baseVal.CompareTo(a.IRRobotId) > 0;
                        }
                    }
                    else if (orderBy.IRRemark)
                    {
                        if (queryType == 0)
                        {
                            return a => baseVal.CompareTo(a.IRRemark) < 0;
                        }
                        else if (queryType == 1)
                        {
                            return a => baseVal.CompareTo(a.IRRemark) > 0;
                        }
                    }
                    else if (orderBy.IRPushState)
                    {
                        if (queryType == 0)
                        {
                            return a => baseVal.CompareTo(a.IRPushState) < 0;
                        }
                        else if (queryType == 1)
                        {
                            return a => baseVal.CompareTo(a.IRPushState) > 0;
                        }
                    }
                    else if (orderBy.IRPushTime)
                    {
                        if (queryType == 0)
                        {
                            return a => baseVal.CompareTo(a.IRPushTime) < 0;
                        }
                        else if (queryType == 1)
                        {
                            return a => baseVal.CompareTo(a.IRPushTime) > 0;
                        }
                    }
                    else if (orderBy.IRScanPayNotifyRet)
                    {
                        if (queryType == 0)
                        {
                            return a => baseVal.CompareTo(a.IRScanPayNotifyRet) < 0;
                        }
                        else if (queryType == 1)
                        {
                            return a => baseVal.CompareTo(a.IRScanPayNotifyRet) > 0;
                        }
                    }
                    else if (orderBy.IRScanPayNotifyUrl)
                    {
                        if (queryType == 0)
                        {
                            return a => baseVal.CompareTo(a.IRScanPayNotifyUrl) < 0;
                        }
                        else if (queryType == 1)
                        {
                            return a => baseVal.CompareTo(a.IRScanPayNotifyUrl) > 0;
                        }
                    }
                    else
                    {
                        if (queryType == 0)
                        {
                            return a => baseVal.CompareTo(a.IRTaskID) < 0;
                        }
                        else if (queryType == 1)
                        {
                            return a => baseVal.CompareTo(a.IRTaskID) > 0;
                        }
                    }
                }
                else
                {
                    if (queryType == 0)
                    {
                        return a => baseVal.CompareTo(a.IRTaskID) < 0;
                    }
                    else if (queryType == 1)
                    {
                        return a => baseVal.CompareTo(a.IRTaskID) > 0;
                    }
                }
            }
            return null;
        }
    }
}