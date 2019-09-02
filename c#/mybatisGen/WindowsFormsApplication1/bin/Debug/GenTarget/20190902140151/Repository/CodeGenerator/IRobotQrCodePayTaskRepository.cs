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
namespace WebApplication1.Repository
{
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
						}			if (entity.IRWeiXinNickName != null)
						{
							updateBefore.IRWeiXinNickName = entity.IRWeiXinNickName;
						}			if (entity.IRWeiXinHeaderImage != null)
						{
							updateBefore.IRWeiXinHeaderImage = entity.IRWeiXinHeaderImage;
						}			if (entity.IRQrCodeImagePath != null)
						{
							updateBefore.IRQrCodeImagePath = entity.IRQrCodeImagePath;
						}			if (entity.IRHandleState != null)
						{
							updateBefore.IRHandleState = entity.IRHandleState;
						}			if (entity.IRHandleMessage != null)
						{
							updateBefore.IRHandleMessage = entity.IRHandleMessage;
						}			if (entity.IRHandleTime != null)
						{
							updateBefore.IRHandleTime = entity.IRHandleTime;
						}			if (entity.IRCreateTime != null)
						{
							updateBefore.IRCreateTime = entity.IRCreateTime;
						}			if (entity.IRReportPicPathJson != null)
						{
							updateBefore.IRReportPicPathJson = entity.IRReportPicPathJson;
						}			if (entity.IRTakeMoney != null)
						{
							updateBefore.IRTakeMoney = entity.IRTakeMoney;
						}			if (entity.IRRobotId != null)
						{
							updateBefore.IRRobotId = entity.IRRobotId;
						}			if (entity.IRRemark != null)
						{
							updateBefore.IRRemark = entity.IRRemark;
						}			if (entity.IRPushState != null)
						{
							updateBefore.IRPushState = entity.IRPushState;
						}			if (entity.IRPushTime != null)
						{
							updateBefore.IRPushTime = entity.IRPushTime;
						}			if (entity.IRScanPayNotifyRet != null)
						{
							updateBefore.IRScanPayNotifyRet = entity.IRScanPayNotifyRet;
						}			if (entity.IRScanPayNotifyUrl != null)
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
						}			if (param.IRWeiXinNickName)
						{
							updateBefore.IRWeiXinNickName = null;
						}			if (param.IRWeiXinHeaderImage)
						{
							updateBefore.IRWeiXinHeaderImage = null;
						}			if (param.IRQrCodeImagePath)
						{
							updateBefore.IRQrCodeImagePath = null;
						}			if (param.IRHandleState)
						{
							updateBefore.IRHandleState = null;
						}			if (param.IRHandleMessage)
						{
							updateBefore.IRHandleMessage = null;
						}			if (param.IRHandleTime)
						{
							updateBefore.IRHandleTime = null;
						}			if (param.IRCreateTime)
						{
							updateBefore.IRCreateTime = null;
						}			if (param.IRReportPicPathJson)
						{
							updateBefore.IRReportPicPathJson = null;
						}			if (param.IRTakeMoney)
						{
							updateBefore.IRTakeMoney = null;
						}			if (param.IRRobotId)
						{
							updateBefore.IRRobotId = null;
						}			if (param.IRRemark)
						{
							updateBefore.IRRemark = null;
						}			if (param.IRPushState)
						{
							updateBefore.IRPushState = null;
						}			if (param.IRPushTime)
						{
							updateBefore.IRPushTime = null;
						}			if (param.IRScanPayNotifyRet)
						{
							updateBefore.IRScanPayNotifyRet = null;
						}			if (param.IRScanPayNotifyUrl)
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

		/// <summary>
        /// 分布式插入一条数据，数据的主键不能为null
        /// </summary>
        /// <param name="entity">实体对象</param>
		public void DistributedInsert(IRobotQrCodePayTask entity)
		{
			if (DistributedTransactionScan.TransactionIds.Value == null)
            {
                throw new Exception("方法调用失败，失败原因：未开启分布式事务。");
            }
			using (BaseDbContext dbContext = CreateDbContext())
            {
				dbContext.Entry(entity).State = EntityState.Added;
				dbContext.Entry(new DistributedTransactionPart
                {
                    Id = IdWorker.NextId(),
                    DistributedTransactionMainId = (long)DistributedTransactionScan.TransactionIds.Value,
                    InverseOper = Convert.ToString(entity.IRTaskID),
					InverseOperType = 'd',
                    TransTableName = "IRobot_QrCodePayTask",
					TransPrimaryKeyVal = entity.IRTaskID,
                    TransactionStatus = 0,
                    CreateDate = DateTime.Now,
                }).State = EntityState.Added;
				if(dbContext.SaveChanges()>0)
				{
					RecordDistribute(dbContext.TransactionDataSource,"IRobot_QrCodePayTask");
				}
			}
		}

		/// <summary>
        /// 分布式批量插入数据，数据的主键不能为null
        /// </summary>
        /// <param name="entity">实体对象</param>
		public void DistributedInsert(List<IRobotQrCodePayTask> entities)
		{
			if (DistributedTransactionScan.TransactionIds.Value == null)
            {
                throw new Exception("方法调用失败，失败原因：未开启分布式事务。");
            }
			using (BaseDbContext dbContext = CreateDbContext())
            {
				foreach(IRobotQrCodePayTask entity in entities)
				{
					dbContext.Entry(entity).State = EntityState.Added;
					dbContext.Entry(new DistributedTransactionPart
					{
						Id = IdWorker.NextId(),
						DistributedTransactionMainId = (long)DistributedTransactionScan.TransactionIds.Value,
						InverseOper = Convert.ToString(entity.IRTaskID),
						InverseOperType = 'D',
						TransTableName = "IRobot_QrCodePayTask",
						TransPrimaryKeyVal = entity.IRTaskID,
						TransactionStatus = 0,
						CreateDate = DateTime.Now,
					}).State = EntityState.Added;
				}
				if(dbContext.SaveChanges()>0)
				{
					RecordDistribute(dbContext.TransactionDataSource,"IRobot_QrCodePayTask");
				}
			}
		}

		/// <summary>
        /// 分布式把指定字段值设置为null，主键不能为null
        /// </summary>
        /// <param name="entity">实体对象</param>
		public void DistributedSetNull(IRobotQrCodePayTaskSetNullParams param)
		{
			if (DistributedTransactionScan.TransactionIds.Value == null)
            {
                throw new Exception("方法调用失败，失败原因：未开启分布式事务。");
            }
			using (BaseDbContext dbContext = CreateDbContext())
            {
				IRobotQrCodePayTask beforeData= FindEntity(param.IRTaskID);
				if(beforeData!=null)
				{
					IRobotQrCodePayTask inverseData = new IRobotQrCodePayTask{ IRTaskID = param.IRTaskID};
					dbContext.Set<IRobotQrCodePayTask>().Attach(beforeData);
										if (param.IROrderNo)
										{
											inverseData.IROrderNo = beforeData.IROrderNo;
											beforeData.IROrderNo = null;
										}					if (param.IRWeiXinNickName)
										{
											inverseData.IRWeiXinNickName = beforeData.IRWeiXinNickName;
											beforeData.IRWeiXinNickName = null;
										}					if (param.IRWeiXinHeaderImage)
										{
											inverseData.IRWeiXinHeaderImage = beforeData.IRWeiXinHeaderImage;
											beforeData.IRWeiXinHeaderImage = null;
										}					if (param.IRQrCodeImagePath)
										{
											inverseData.IRQrCodeImagePath = beforeData.IRQrCodeImagePath;
											beforeData.IRQrCodeImagePath = null;
										}					if (param.IRHandleState)
										{
											inverseData.IRHandleState = beforeData.IRHandleState;
											beforeData.IRHandleState = null;
										}					if (param.IRHandleMessage)
										{
											inverseData.IRHandleMessage = beforeData.IRHandleMessage;
											beforeData.IRHandleMessage = null;
										}					if (param.IRHandleTime)
										{
											inverseData.IRHandleTime = beforeData.IRHandleTime;
											beforeData.IRHandleTime = null;
										}					if (param.IRCreateTime)
										{
											inverseData.IRCreateTime = beforeData.IRCreateTime;
											beforeData.IRCreateTime = null;
										}					if (param.IRReportPicPathJson)
										{
											inverseData.IRReportPicPathJson = beforeData.IRReportPicPathJson;
											beforeData.IRReportPicPathJson = null;
										}					if (param.IRTakeMoney)
										{
											inverseData.IRTakeMoney = beforeData.IRTakeMoney;
											beforeData.IRTakeMoney = null;
										}					if (param.IRRobotId)
										{
											inverseData.IRRobotId = beforeData.IRRobotId;
											beforeData.IRRobotId = null;
										}					if (param.IRRemark)
										{
											inverseData.IRRemark = beforeData.IRRemark;
											beforeData.IRRemark = null;
										}					if (param.IRPushState)
										{
											inverseData.IRPushState = beforeData.IRPushState;
											beforeData.IRPushState = null;
										}					if (param.IRPushTime)
										{
											inverseData.IRPushTime = beforeData.IRPushTime;
											beforeData.IRPushTime = null;
										}					if (param.IRScanPayNotifyRet)
										{
											inverseData.IRScanPayNotifyRet = beforeData.IRScanPayNotifyRet;
											beforeData.IRScanPayNotifyRet = null;
										}					if (param.IRScanPayNotifyUrl)
										{
											inverseData.IRScanPayNotifyUrl = beforeData.IRScanPayNotifyUrl;
											beforeData.IRScanPayNotifyUrl = null;
										}
					dbContext.Entry(new DistributedTransactionPart
					{
						Id = IdWorker.NextId(),
						DistributedTransactionMainId = (long)DistributedTransactionScan.TransactionIds.Value,
						InverseOper = CompressHelper.GZipCompressString(JsonConvert.SerializeObject(inverseData,TimeConverter)),
						InverseOperType = 'u',
						TransTableName = "IRobot_QrCodePayTask",
						TransPrimaryKeyVal = param.IRTaskID,
						TransactionStatus = 0,
						CreateDate = DateTime.Now,
					}).State = EntityState.Added;
					if(dbContext.SaveChanges()>0)
					{
						RecordDistribute(dbContext.TransactionDataSource,"IRobot_QrCodePayTask");
					}
				}
			}
		}

		/// <summary>
        /// 分布式部分更新数据，数据的主键不能为null，只更新不为null的值
        /// </summary>
        /// <param name="entity">实体对象</param>
		public void DistributedUpdateChange(IRobotQrCodePayTask entity)
		{
			if (DistributedTransactionScan.TransactionIds.Value == null)
            {
                throw new Exception("方法调用失败，失败原因：未开启分布式事务。");
            }
			using (BaseDbContext dbContext = CreateDbContext())
            {
				IRobotQrCodePayTask beforeData = FindEntity(entity.IRTaskID);
				if(beforeData!=null)
				{
					IRobotQrCodePayTask inverseData = new IRobotQrCodePayTask{ IRTaskID = entity.IRTaskID};
					DistributedUpdateChange(beforeData, inverseData, entity, dbContext);
					dbContext.Entry(new DistributedTransactionPart
					{
						Id = IdWorker.NextId(),
						DistributedTransactionMainId = (long)DistributedTransactionScan.TransactionIds.Value,
						InverseOper = CompressHelper.GZipCompressString(JsonConvert.SerializeObject(inverseData,TimeConverter)),
						InverseOperType = 'u',
						TransTableName = "IRobot_QrCodePayTask",
						TransPrimaryKeyVal = entity.IRTaskID,
						TransactionStatus = 0,
						CreateDate = DateTime.Now,
					}).State = EntityState.Added;
					if(dbContext.SaveChanges()>0)
					{
						RecordDistribute(dbContext.TransactionDataSource,"IRobot_QrCodePayTask");
					}
				}
			}
		}

		/// <summary>
        /// 分布式更新部分数据的共用代码，
        /// </summary>
        /// <param name="beforeData"></param>
        /// <param name="inverseData"></param>
        /// <param name="entity"></param>
        /// <param name="dbContext"></param>
        void DistributedUpdateChange(IRobotQrCodePayTask beforeData, IRobotQrCodePayTask inverseData, IRobotQrCodePayTask entity, BaseDbContext dbContext)
        {
			IRobotQrCodePayTask updateBefore = new IRobotQrCodePayTask { IRTaskID = entity.IRTaskID};
			dbContext.Set<IRobotQrCodePayTask>().Attach(updateBefore);
							if (entity.IROrderNo != null)
							{
								updateBefore.IROrderNo = entity.IROrderNo;
								inverseData.IROrderNo = beforeData.IROrderNo;
							}				if (entity.IRWeiXinNickName != null)
							{
								updateBefore.IRWeiXinNickName = entity.IRWeiXinNickName;
								inverseData.IRWeiXinNickName = beforeData.IRWeiXinNickName;
							}				if (entity.IRWeiXinHeaderImage != null)
							{
								updateBefore.IRWeiXinHeaderImage = entity.IRWeiXinHeaderImage;
								inverseData.IRWeiXinHeaderImage = beforeData.IRWeiXinHeaderImage;
							}				if (entity.IRQrCodeImagePath != null)
							{
								updateBefore.IRQrCodeImagePath = entity.IRQrCodeImagePath;
								inverseData.IRQrCodeImagePath = beforeData.IRQrCodeImagePath;
							}				if (entity.IRHandleState != null)
							{
								updateBefore.IRHandleState = entity.IRHandleState;
								inverseData.IRHandleState = beforeData.IRHandleState;
							}				if (entity.IRHandleMessage != null)
							{
								updateBefore.IRHandleMessage = entity.IRHandleMessage;
								inverseData.IRHandleMessage = beforeData.IRHandleMessage;
							}				if (entity.IRHandleTime != null)
							{
								updateBefore.IRHandleTime = entity.IRHandleTime;
								inverseData.IRHandleTime = beforeData.IRHandleTime;
							}				if (entity.IRCreateTime != null)
							{
								updateBefore.IRCreateTime = entity.IRCreateTime;
								inverseData.IRCreateTime = beforeData.IRCreateTime;
							}				if (entity.IRReportPicPathJson != null)
							{
								updateBefore.IRReportPicPathJson = entity.IRReportPicPathJson;
								inverseData.IRReportPicPathJson = beforeData.IRReportPicPathJson;
							}				if (entity.IRTakeMoney != null)
							{
								updateBefore.IRTakeMoney = entity.IRTakeMoney;
								inverseData.IRTakeMoney = beforeData.IRTakeMoney;
							}				if (entity.IRRobotId != null)
							{
								updateBefore.IRRobotId = entity.IRRobotId;
								inverseData.IRRobotId = beforeData.IRRobotId;
							}				if (entity.IRRemark != null)
							{
								updateBefore.IRRemark = entity.IRRemark;
								inverseData.IRRemark = beforeData.IRRemark;
							}				if (entity.IRPushState != null)
							{
								updateBefore.IRPushState = entity.IRPushState;
								inverseData.IRPushState = beforeData.IRPushState;
							}				if (entity.IRPushTime != null)
							{
								updateBefore.IRPushTime = entity.IRPushTime;
								inverseData.IRPushTime = beforeData.IRPushTime;
							}				if (entity.IRScanPayNotifyRet != null)
							{
								updateBefore.IRScanPayNotifyRet = entity.IRScanPayNotifyRet;
								inverseData.IRScanPayNotifyRet = beforeData.IRScanPayNotifyRet;
							}				if (entity.IRScanPayNotifyUrl != null)
							{
								updateBefore.IRScanPayNotifyUrl = entity.IRScanPayNotifyUrl;
								inverseData.IRScanPayNotifyUrl = beforeData.IRScanPayNotifyUrl;
							}
		}

		/// <summary>
        /// 分布式批量的部分更新数据，数据的主键不能为null，只更新不为null的值
        /// </summary>
        /// <param name="entity">实体对象</param>
		public void DistributedUpdateChange(List<IRobotQrCodePayTask> entities)
		{
			if (DistributedTransactionScan.TransactionIds.Value == null)
            {
                throw new Exception("方法调用失败，失败原因：未开启分布式事务。");
            }
			using (BaseDbContext dbContext = CreateDbContext())
            {
				foreach(IRobotQrCodePayTask entity in entities)
				{
					IRobotQrCodePayTask beforeData = FindEntity(entity.IRTaskID);
					if(beforeData!=null)
					{
						IRobotQrCodePayTask inverseData = new IRobotQrCodePayTask{ IRTaskID = entity.IRTaskID};
						DistributedUpdateChange(beforeData, inverseData, entity, dbContext);
						dbContext.Entry(new DistributedTransactionPart
						{
							Id = IdWorker.NextId(),
							DistributedTransactionMainId = (long)DistributedTransactionScan.TransactionIds.Value,
							InverseOper = CompressHelper.GZipCompressString(JsonConvert.SerializeObject(inverseData,TimeConverter)),
							InverseOperType = 'U',
							TransTableName = "IRobot_QrCodePayTask",
							TransPrimaryKeyVal = entity.IRTaskID,
							TransactionStatus = 0,
							CreateDate = DateTime.Now,
						}).State = EntityState.Added;
					}
				}
				if(dbContext.SaveChanges()>0)
				{
					RecordDistribute(dbContext.TransactionDataSource,"IRobot_QrCodePayTask");
				}
			}
		}

		/// <summary>
        /// 分布式全覆盖更新数据，数据的主键不能为null，所有属性全部覆盖数据库记录原有的值
        /// </summary>
        /// <param name="entity">实体对象</param>
		public void DistributedUpdateAll(IRobotQrCodePayTask entity)
		{
			if (DistributedTransactionScan.TransactionIds.Value == null)
            {
                throw new Exception("方法调用失败，失败原因：未开启分布式事务。");
            }
			using (BaseDbContext dbContext = CreateDbContext())
            {
				IRobotQrCodePayTask beforeData = FindEntity(entity.IRTaskID);
				if(beforeData!=null)
				{
					dbContext.Entry(entity).State = EntityState.Modified;
					dbContext.Entry(new DistributedTransactionPart
					{
						Id = IdWorker.NextId(),
						DistributedTransactionMainId = (long)DistributedTransactionScan.TransactionIds.Value,
						InverseOper = CompressHelper.GZipCompressString(JsonConvert.SerializeObject(beforeData,TimeConverter)),
						InverseOperType = 'u',
						TransTableName = "IRobot_QrCodePayTask",
						TransPrimaryKeyVal = entity.IRTaskID,
						TransactionStatus = 0,
						CreateDate = DateTime.Now,
					}).State = EntityState.Added;
					if(dbContext.SaveChanges()>0)
					{
						RecordDistribute(dbContext.TransactionDataSource,"IRobot_QrCodePayTask");
					}
				}
			}
		}

		/// <summary>
        /// 分布式批量全覆盖更新数据，数据的主键不能为null，所有属性全部覆盖数据库记录原有的值
        /// </summary>
        /// <param name="entity">实体对象</param>
		public void DistributedUpdateAllBatch(List<IRobotQrCodePayTask> entities)
		{
			if (DistributedTransactionScan.TransactionIds.Value == null)
            {
                throw new Exception("方法调用失败，失败原因：未开启分布式事务。");
            }
			using (BaseDbContext dbContext = CreateDbContext())
            {
				foreach(IRobotQrCodePayTask entity in entities)
				{
					IRobotQrCodePayTask beforeData = FindEntity(entity.IRTaskID);
					if(beforeData!=null)
					{
						dbContext.Entry(entity).State = EntityState.Modified;
						dbContext.Entry(new DistributedTransactionPart
						{
							Id = IdWorker.NextId(),
							DistributedTransactionMainId = (long)DistributedTransactionScan.TransactionIds.Value,
							InverseOper = CompressHelper.GZipCompressString(JsonConvert.SerializeObject(beforeData,TimeConverter)),
							InverseOperType = 'U',
							TransTableName = "IRobot_QrCodePayTask",
							TransPrimaryKeyVal = entity.IRTaskID,
							TransactionStatus = 0,
							CreateDate = DateTime.Now,
						}).State = EntityState.Added;
					}
				}
				if(dbContext.SaveChanges()>0)
				{
					RecordDistribute(dbContext.TransactionDataSource,"IRobot_QrCodePayTask");
				}
			}
		}

		/// <summary>
        /// 分布式删除一条数据，数据的主键不能为null
        /// </summary>
        /// <param name="entity">实体对象</param>
		public void DistributedDelete(IRobotQrCodePayTask entity)
		{
			if (DistributedTransactionScan.TransactionIds.Value == null)
            {
                throw new Exception("方法调用失败，失败原因：未开启分布式事务。");
            }
			using (BaseDbContext dbContext = CreateDbContext())
            {
				IRobotQrCodePayTask beforeData = FindEntity(entity.IRTaskID);
				if(beforeData!=null){
					dbContext.Entry(entity).State = EntityState.Deleted;
					dbContext.Entry(new DistributedTransactionPart
					{
						Id = IdWorker.NextId(),
						DistributedTransactionMainId = (long)DistributedTransactionScan.TransactionIds.Value,
						InverseOper = CompressHelper.GZipCompressString(JsonConvert.SerializeObject(beforeData,TimeConverter)),
						InverseOperType = 'i',
						TransTableName = "IRobot_QrCodePayTask",
						TransPrimaryKeyVal = entity.IRTaskID,
						TransactionStatus = 0,
						CreateDate = DateTime.Now,
					}).State = EntityState.Added;
					if(dbContext.SaveChanges()>0)
					{
						RecordDistribute(dbContext.TransactionDataSource,"IRobot_QrCodePayTask");
					}
				}
			}
		}


		/// <summary>
        /// 分布式删除一条数据，数据的主键不能为null
        /// </summary>
        /// <param name="entity">实体对象</param>
		public void DistributedDelete(List<IRobotQrCodePayTask> entities)
		{
			if (DistributedTransactionScan.TransactionIds.Value == null)
            {
                throw new Exception("方法调用失败，失败原因：未开启分布式事务。");
            }
			using (BaseDbContext dbContext = CreateDbContext())
            {
				foreach(IRobotQrCodePayTask entity in entities)
				{
					IRobotQrCodePayTask beforeData = FindEntity(entity.IRTaskID);
					if(beforeData!=null){
						dbContext.Entry(entity).State = EntityState.Deleted;
						dbContext.Entry(new DistributedTransactionPart
						{
							Id = IdWorker.NextId(),
							DistributedTransactionMainId = (long)DistributedTransactionScan.TransactionIds.Value,
							InverseOper = CompressHelper.GZipCompressString(JsonConvert.SerializeObject(beforeData,TimeConverter)),
							InverseOperType = 'I',
							TransTableName = "IRobot_QrCodePayTask",
							TransPrimaryKeyVal = entity.IRTaskID,
							TransactionStatus = 0,
							CreateDate = DateTime.Now,
						}).State = EntityState.Added;
					}
				}
				if(dbContext.SaveChanges()>0)
				{
					RecordDistribute(dbContext.TransactionDataSource,"IRobot_QrCodePayTask");
				}
			}
		}

		public override BaseDbContext CreateDbContext()
        {
            return new MyDbContext2();
        }
    }
}