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
using Autofac.Extras.DynamicProxy;
namespace WebApplication1.Repository
{
	//启用分布式事务
    //[Intercept(typeof(DistributeRepository))]
    public partial class IRobotQrCodePayTaskRepository : BaseRepository<IRobotQrCodePayTask,IRobotQrCodePayTaskParams,IRobotQrCodePayTaskOrderBy,IRobotQrCodePayTaskSetNullParams>
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
							if (eqArgs.IRRemark != null)
							{
								query = query.Where(a => a.IRRemark == eqArgs.IRRemark);
							}

							if (!string.IsNullOrEmpty(eqArgs.IRRemarkLike))
			                {
			                    query = query.Where(a => a.IRRemark.Contains(eqArgs.IRRemarkLike));
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
							if (neqArgs.IRRemark != null)
							{
								query = query.Where(a => a.IRRemark != neqArgs.IRRemark);
							}

							if (!string.IsNullOrEmpty(neqArgs.IRRemarkLike))
			                {
								query = query.Where(a => !a.IRRemark.Contains(neqArgs.IRRemarkLike));
							}
				query = OrderByAsc(query, eqArgs.orderByAsc);
                query = OrderByDesc(query, eqArgs.orderByDesc);
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
							if (orderBy.IRRemark) { query = query.OrderBy(a => a.IRRemark); }
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
							if (orderBy.IRReportPicPath) { query = query.OrderByDescending(a => a.IRReportPicPath); }else 
							if (orderBy.IRRemark) { query = query.OrderByDescending(a => a.IRRemark); }
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
			IRobotQrCodePayTask  updateBefore = new IRobotQrCodePayTask  { IRTaskID = entity.IRTaskID};
			dbContext.Set<IRobotQrCodePayTask>().Attach(updateBefore);
						if (entity.IROrderNo != null)
						{
							updateBefore.IROrderNo = entity.IROrderNo;
						}			if (entity.IRTakeMoney != null)
						{
							updateBefore.IRTakeMoney = entity.IRTakeMoney;
						}			if (entity.IRPushState != null)
						{
							updateBefore.IRPushState = entity.IRPushState;
						}			if (entity.IRScanPayNotifyUrl != null)
						{
							updateBefore.IRScanPayNotifyUrl = entity.IRScanPayNotifyUrl;
						}			if (entity.IRScanPayNotifyRet != null)
						{
							updateBefore.IRScanPayNotifyRet = entity.IRScanPayNotifyRet;
						}			if (entity.IRHandleState != null)
						{
							updateBefore.IRHandleState = entity.IRHandleState;
						}			if (entity.IRQrCodeImagePath != null)
						{
							updateBefore.IRQrCodeImagePath = entity.IRQrCodeImagePath;
						}			if (entity.IRBsyNotifyState != null)
						{
							updateBefore.IRBsyNotifyState = entity.IRBsyNotifyState;
						}			if (entity.IRSendMoneyNotifyState != null)
						{
							updateBefore.IRSendMoneyNotifyState = entity.IRSendMoneyNotifyState;
						}			if (entity.IRReportPicPath != null)
						{
							updateBefore.IRReportPicPath = entity.IRReportPicPath;
						}			if (entity.IRRemark != null)
						{
							updateBefore.IRRemark = entity.IRRemark;
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
						}			if (param.IRTakeMoney)
						{
							updateBefore.IRTakeMoney = null;
						}			if (param.IRPushState)
						{
							updateBefore.IRPushState = null;
						}			if (param.IRScanPayNotifyUrl)
						{
							updateBefore.IRScanPayNotifyUrl = null;
						}			if (param.IRScanPayNotifyRet)
						{
							updateBefore.IRScanPayNotifyRet = null;
						}			if (param.IRHandleState)
						{
							updateBefore.IRHandleState = null;
						}			if (param.IRQrCodeImagePath)
						{
							updateBefore.IRQrCodeImagePath = null;
						}			if (param.IRBsyNotifyState)
						{
							updateBefore.IRBsyNotifyState = null;
						}			if (param.IRSendMoneyNotifyState)
						{
							updateBefore.IRSendMoneyNotifyState = null;
						}			if (param.IRReportPicPath)
						{
							updateBefore.IRReportPicPath = null;
						}			if (param.IRRemark)
						{
							updateBefore.IRRemark = null;
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
			using(BaseDbContext db = CreateDbContext())
			{
				return AllStatic.InverseRepositoryMap[db.Database.Connection.ConnectionString]["IRobot_QrCodePayTask"];
			}
        }
    }
}