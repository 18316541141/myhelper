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
    public partial class IRobotRobotManagerRepository : BaseRepository<IRobotRobotManager,IRobotRobotManagerParams,IRobotRobotManagerOrderBy>
    {
		/// <summary>
        /// 通用的设置查询参数方法，只有在参数不为null的情况下才会设置，
		/// 满足项目中常用的模糊查询，分页查询等功能
        /// </summary>
        /// <param name="query">待设置参数的query对象</param>
        /// <param name="eqArgs">装载查询参数的实体类</param>
		/// <param name="neqArgs">装载不等于查询参数的实体类</param>
        /// <returns>返回设置好查询参数的query对象</returns>
        protected override IQueryable<IRobotRobotManager> Query(IQueryable<IRobotRobotManager> query, IRobotRobotManagerParams eqArgs, IRobotRobotManagerParams neqArgs = null)
        {
            if (eqArgs != null)
            {

							if (eqArgs.IRMID != null)
							{
								query = query.Where(a => a.IRMID == eqArgs.IRMID);
							}

							if (eqArgs.IRMRobotId != null)
							{
								query = query.Where(a => a.IRMRobotId == eqArgs.IRMRobotId);
							}

							if (!string.IsNullOrEmpty(eqArgs.IRMRobotIdLike))
			                {
			                    query = query.Where(a => a.IRMRobotId.Contains(eqArgs.IRMRobotIdLike));
			                }
							if (eqArgs.IRMRobotState != null)
							{
								query = query.Where(a => a.IRMRobotState == eqArgs.IRMRobotState);
							}

							if (eqArgs.IRMRobotStateStart != null)
			                {
			                    query = query.Where(a => a.IRMRobotState >= eqArgs.IRMRobotStateStart);
			                }
							if (eqArgs.IRMRobotStateEnd != null)
			                {
			                    query = query.Where(a => a.IRMRobotState <= eqArgs.IRMRobotStateEnd);
			                }
							if (eqArgs.IRMRuningTime != null)
							{
								query = query.Where(a => a.IRMRuningTime == eqArgs.IRMRuningTime);
							}

							if (eqArgs.IRMRuningTimeStart != null)
			                {
			                    query = query.Where(a => a.IRMRuningTime >= eqArgs.IRMRuningTimeStart);
			                }
							if (eqArgs.IRMRuningTimeEnd != null)
			                {
			                    query = query.Where(a => a.IRMRuningTime <= eqArgs.IRMRuningTimeEnd);
			                }
							if (eqArgs.IRMMaxPayTradeCount != null)
							{
								query = query.Where(a => a.IRMMaxPayTradeCount == eqArgs.IRMMaxPayTradeCount);
							}

							if (eqArgs.IRMMaxPayTradeCountStart != null)
			                {
			                    query = query.Where(a => a.IRMMaxPayTradeCount >= eqArgs.IRMMaxPayTradeCountStart);
			                }
							if (eqArgs.IRMMaxPayTradeCountEnd != null)
			                {
			                    query = query.Where(a => a.IRMMaxPayTradeCount <= eqArgs.IRMMaxPayTradeCountEnd);
			                }
							if (eqArgs.IRMCurrentPayCount != null)
							{
								query = query.Where(a => a.IRMCurrentPayCount == eqArgs.IRMCurrentPayCount);
							}

							if (eqArgs.IRMCurrentPayCountStart != null)
			                {
			                    query = query.Where(a => a.IRMCurrentPayCount >= eqArgs.IRMCurrentPayCountStart);
			                }
							if (eqArgs.IRMCurrentPayCountEnd != null)
			                {
			                    query = query.Where(a => a.IRMCurrentPayCount <= eqArgs.IRMCurrentPayCountEnd);
			                }
							if (eqArgs.IRMBalance != null)
							{
								query = query.Where(a => a.IRMBalance == eqArgs.IRMBalance);
							}

							if (eqArgs.IRMBalanceStart != null)
			                {
			                    query = query.Where(a => a.IRMBalance >= eqArgs.IRMBalanceStart);
			                }
							if (eqArgs.IRMBalanceEnd != null)
			                {
			                    query = query.Where(a => a.IRMBalance <= eqArgs.IRMBalanceEnd);
			                }
							if (eqArgs.IRMIP != null)
							{
								query = query.Where(a => a.IRMIP == eqArgs.IRMIP);
							}

							if (!string.IsNullOrEmpty(eqArgs.IRMIPLike))
			                {
			                    query = query.Where(a => a.IRMIP.Contains(eqArgs.IRMIPLike));
			                }
							if (eqArgs.IRMCreateTime != null)
							{
								query = query.Where(a => a.IRMCreateTime == eqArgs.IRMCreateTime);
							}

							if (eqArgs.IRMCreateTimeStart != null)
			                {
			                    query = query.Where(a => a.IRMCreateTime >= eqArgs.IRMCreateTimeStart);
			                }
							if (eqArgs.IRMCreateTimeEnd != null)
			                {
			                    query = query.Where(a => a.IRMCreateTime <= eqArgs.IRMCreateTimeEnd);
			                }
							if (eqArgs.IRMTeamViewId != null)
							{
								query = query.Where(a => a.IRMTeamViewId == eqArgs.IRMTeamViewId);
							}

							if (!string.IsNullOrEmpty(eqArgs.IRMTeamViewIdLike))
			                {
			                    query = query.Where(a => a.IRMTeamViewId.Contains(eqArgs.IRMTeamViewIdLike));
			                }
							if (eqArgs.IRMTeamViewPassword != null)
							{
								query = query.Where(a => a.IRMTeamViewPassword == eqArgs.IRMTeamViewPassword);
							}

							if (!string.IsNullOrEmpty(eqArgs.IRMTeamViewPasswordLike))
			                {
			                    query = query.Where(a => a.IRMTeamViewPassword.Contains(eqArgs.IRMTeamViewPasswordLike));
			                }
							if (eqArgs.IRMSunflowerId != null)
							{
								query = query.Where(a => a.IRMSunflowerId == eqArgs.IRMSunflowerId);
							}

							if (!string.IsNullOrEmpty(eqArgs.IRMSunflowerIdLike))
			                {
			                    query = query.Where(a => a.IRMSunflowerId.Contains(eqArgs.IRMSunflowerIdLike));
			                }
							if (eqArgs.IRMSunflowerPassword != null)
							{
								query = query.Where(a => a.IRMSunflowerPassword == eqArgs.IRMSunflowerPassword);
							}

							if (!string.IsNullOrEmpty(eqArgs.IRMSunflowerPasswordLike))
			                {
			                    query = query.Where(a => a.IRMSunflowerPassword.Contains(eqArgs.IRMSunflowerPasswordLike));
			                }
							if (eqArgs.IRMBankCardTailNum != null)
							{
								query = query.Where(a => a.IRMBankCardTailNum == eqArgs.IRMBankCardTailNum);
							}

							if (!string.IsNullOrEmpty(eqArgs.IRMBankCardTailNumLike))
			                {
			                    query = query.Where(a => a.IRMBankCardTailNum.Contains(eqArgs.IRMBankCardTailNumLike));
			                }
							if (eqArgs.IRMBankCardName != null)
							{
								query = query.Where(a => a.IRMBankCardName == eqArgs.IRMBankCardName);
							}

							if (!string.IsNullOrEmpty(eqArgs.IRMBankCardNameLike))
			                {
			                    query = query.Where(a => a.IRMBankCardName.Contains(eqArgs.IRMBankCardNameLike));
			                }
							if (eqArgs.IRMMinBalance != null)
							{
								query = query.Where(a => a.IRMMinBalance == eqArgs.IRMMinBalance);
							}

							if (eqArgs.IRMMinBalanceStart != null)
			                {
			                    query = query.Where(a => a.IRMMinBalance >= eqArgs.IRMMinBalanceStart);
			                }
							if (eqArgs.IRMMinBalanceEnd != null)
			                {
			                    query = query.Where(a => a.IRMMinBalance <= eqArgs.IRMMinBalanceEnd);
			                }
							if (eqArgs.IRMPayPassword != null)
							{
								query = query.Where(a => a.IRMPayPassword == eqArgs.IRMPayPassword);
							}

							if (!string.IsNullOrEmpty(eqArgs.IRMPayPasswordLike))
			                {
			                    query = query.Where(a => a.IRMPayPassword.Contains(eqArgs.IRMPayPasswordLike));
			                }
							if (eqArgs.IRMScanPayNotifyUrl != null)
							{
								query = query.Where(a => a.IRMScanPayNotifyUrl == eqArgs.IRMScanPayNotifyUrl);
							}

							if (!string.IsNullOrEmpty(eqArgs.IRMScanPayNotifyUrlLike))
			                {
			                    query = query.Where(a => a.IRMScanPayNotifyUrl.Contains(eqArgs.IRMScanPayNotifyUrlLike));
			                }
							if (eqArgs.IRMWxUsername != null)
							{
								query = query.Where(a => a.IRMWxUsername == eqArgs.IRMWxUsername);
							}

							if (!string.IsNullOrEmpty(eqArgs.IRMWxUsernameLike))
			                {
			                    query = query.Where(a => a.IRMWxUsername.Contains(eqArgs.IRMWxUsernameLike));
			                }
							if (eqArgs.IRMWxPassword != null)
							{
								query = query.Where(a => a.IRMWxPassword == eqArgs.IRMWxPassword);
							}

							if (!string.IsNullOrEmpty(eqArgs.IRMWxPasswordLike))
			                {
			                    query = query.Where(a => a.IRMWxPassword.Contains(eqArgs.IRMWxPasswordLike));
			                }
							if (eqArgs.IRMRefreshBalance != null)
							{
								query = query.Where(a => a.IRMRefreshBalance == eqArgs.IRMRefreshBalance);
							}

							if (!string.IsNullOrEmpty(eqArgs.IRMRefreshBalanceLike))
			                {
			                    query = query.Where(a => a.IRMRefreshBalance.Contains(eqArgs.IRMRefreshBalanceLike));
			                }
            }
			if(neqArgs != null){

							if (neqArgs.IRMID != null)
							{
								query = query.Where(a => a.IRMID != neqArgs.IRMID);
							}

							if (neqArgs.IRMRobotId != null)
							{
								query = query.Where(a => a.IRMRobotId != neqArgs.IRMRobotId);
							}

							if (!string.IsNullOrEmpty(neqArgs.IRMRobotIdLike))
			                {
								query = query.Where(a => !a.IRMRobotId.Contains(neqArgs.IRMRobotIdLike));
							}
							if (neqArgs.IRMRobotState != null)
							{
								query = query.Where(a => a.IRMRobotState != neqArgs.IRMRobotState);
							}

							if (neqArgs.IRMRuningTime != null)
							{
								query = query.Where(a => a.IRMRuningTime != neqArgs.IRMRuningTime);
							}

							if (neqArgs.IRMMaxPayTradeCount != null)
							{
								query = query.Where(a => a.IRMMaxPayTradeCount != neqArgs.IRMMaxPayTradeCount);
							}

							if (neqArgs.IRMCurrentPayCount != null)
							{
								query = query.Where(a => a.IRMCurrentPayCount != neqArgs.IRMCurrentPayCount);
							}

							if (neqArgs.IRMBalance != null)
							{
								query = query.Where(a => a.IRMBalance != neqArgs.IRMBalance);
							}

							if (neqArgs.IRMIP != null)
							{
								query = query.Where(a => a.IRMIP != neqArgs.IRMIP);
							}

							if (!string.IsNullOrEmpty(neqArgs.IRMIPLike))
			                {
								query = query.Where(a => !a.IRMIP.Contains(neqArgs.IRMIPLike));
							}
							if (neqArgs.IRMCreateTime != null)
							{
								query = query.Where(a => a.IRMCreateTime != neqArgs.IRMCreateTime);
							}

							if (neqArgs.IRMTeamViewId != null)
							{
								query = query.Where(a => a.IRMTeamViewId != neqArgs.IRMTeamViewId);
							}

							if (!string.IsNullOrEmpty(neqArgs.IRMTeamViewIdLike))
			                {
								query = query.Where(a => !a.IRMTeamViewId.Contains(neqArgs.IRMTeamViewIdLike));
							}
							if (neqArgs.IRMTeamViewPassword != null)
							{
								query = query.Where(a => a.IRMTeamViewPassword != neqArgs.IRMTeamViewPassword);
							}

							if (!string.IsNullOrEmpty(neqArgs.IRMTeamViewPasswordLike))
			                {
								query = query.Where(a => !a.IRMTeamViewPassword.Contains(neqArgs.IRMTeamViewPasswordLike));
							}
							if (neqArgs.IRMSunflowerId != null)
							{
								query = query.Where(a => a.IRMSunflowerId != neqArgs.IRMSunflowerId);
							}

							if (!string.IsNullOrEmpty(neqArgs.IRMSunflowerIdLike))
			                {
								query = query.Where(a => !a.IRMSunflowerId.Contains(neqArgs.IRMSunflowerIdLike));
							}
							if (neqArgs.IRMSunflowerPassword != null)
							{
								query = query.Where(a => a.IRMSunflowerPassword != neqArgs.IRMSunflowerPassword);
							}

							if (!string.IsNullOrEmpty(neqArgs.IRMSunflowerPasswordLike))
			                {
								query = query.Where(a => !a.IRMSunflowerPassword.Contains(neqArgs.IRMSunflowerPasswordLike));
							}
							if (neqArgs.IRMBankCardTailNum != null)
							{
								query = query.Where(a => a.IRMBankCardTailNum != neqArgs.IRMBankCardTailNum);
							}

							if (!string.IsNullOrEmpty(neqArgs.IRMBankCardTailNumLike))
			                {
								query = query.Where(a => !a.IRMBankCardTailNum.Contains(neqArgs.IRMBankCardTailNumLike));
							}
							if (neqArgs.IRMBankCardName != null)
							{
								query = query.Where(a => a.IRMBankCardName != neqArgs.IRMBankCardName);
							}

							if (!string.IsNullOrEmpty(neqArgs.IRMBankCardNameLike))
			                {
								query = query.Where(a => !a.IRMBankCardName.Contains(neqArgs.IRMBankCardNameLike));
							}
							if (neqArgs.IRMMinBalance != null)
							{
								query = query.Where(a => a.IRMMinBalance != neqArgs.IRMMinBalance);
							}

							if (neqArgs.IRMPayPassword != null)
							{
								query = query.Where(a => a.IRMPayPassword != neqArgs.IRMPayPassword);
							}

							if (!string.IsNullOrEmpty(neqArgs.IRMPayPasswordLike))
			                {
								query = query.Where(a => !a.IRMPayPassword.Contains(neqArgs.IRMPayPasswordLike));
							}
							if (neqArgs.IRMScanPayNotifyUrl != null)
							{
								query = query.Where(a => a.IRMScanPayNotifyUrl != neqArgs.IRMScanPayNotifyUrl);
							}

							if (!string.IsNullOrEmpty(neqArgs.IRMScanPayNotifyUrlLike))
			                {
								query = query.Where(a => !a.IRMScanPayNotifyUrl.Contains(neqArgs.IRMScanPayNotifyUrlLike));
							}
							if (neqArgs.IRMWxUsername != null)
							{
								query = query.Where(a => a.IRMWxUsername != neqArgs.IRMWxUsername);
							}

							if (!string.IsNullOrEmpty(neqArgs.IRMWxUsernameLike))
			                {
								query = query.Where(a => !a.IRMWxUsername.Contains(neqArgs.IRMWxUsernameLike));
							}
							if (neqArgs.IRMWxPassword != null)
							{
								query = query.Where(a => a.IRMWxPassword != neqArgs.IRMWxPassword);
							}

							if (!string.IsNullOrEmpty(neqArgs.IRMWxPasswordLike))
			                {
								query = query.Where(a => !a.IRMWxPassword.Contains(neqArgs.IRMWxPasswordLike));
							}
							if (neqArgs.IRMRefreshBalance != null)
							{
								query = query.Where(a => a.IRMRefreshBalance != neqArgs.IRMRefreshBalance);
							}

							if (!string.IsNullOrEmpty(neqArgs.IRMRefreshBalanceLike))
			                {
								query = query.Where(a => !a.IRMRefreshBalance.Contains(neqArgs.IRMRefreshBalanceLike));
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
        IQueryable<IRobotRobotManager> OrderByAsc(IQueryable<IRobotRobotManager> query, IRobotRobotManagerOrderBy orderBy)
        {
            if (orderBy != null)
            {

							if (orderBy.IRMID) { query = query.OrderBy(a => a.IRMID); }
							if (orderBy.IRMRobotId) { query = query.OrderBy(a => a.IRMRobotId); }
							if (orderBy.IRMRobotState) { query = query.OrderBy(a => a.IRMRobotState); }
							if (orderBy.IRMRuningTime) { query = query.OrderBy(a => a.IRMRuningTime); }
							if (orderBy.IRMMaxPayTradeCount) { query = query.OrderBy(a => a.IRMMaxPayTradeCount); }
							if (orderBy.IRMCurrentPayCount) { query = query.OrderBy(a => a.IRMCurrentPayCount); }
							if (orderBy.IRMBalance) { query = query.OrderBy(a => a.IRMBalance); }
							if (orderBy.IRMIP) { query = query.OrderBy(a => a.IRMIP); }
							if (orderBy.IRMCreateTime) { query = query.OrderBy(a => a.IRMCreateTime); }
							if (orderBy.IRMTeamViewId) { query = query.OrderBy(a => a.IRMTeamViewId); }
							if (orderBy.IRMTeamViewPassword) { query = query.OrderBy(a => a.IRMTeamViewPassword); }
							if (orderBy.IRMSunflowerId) { query = query.OrderBy(a => a.IRMSunflowerId); }
							if (orderBy.IRMSunflowerPassword) { query = query.OrderBy(a => a.IRMSunflowerPassword); }
							if (orderBy.IRMBankCardTailNum) { query = query.OrderBy(a => a.IRMBankCardTailNum); }
							if (orderBy.IRMBankCardName) { query = query.OrderBy(a => a.IRMBankCardName); }
							if (orderBy.IRMMinBalance) { query = query.OrderBy(a => a.IRMMinBalance); }
							if (orderBy.IRMPayPassword) { query = query.OrderBy(a => a.IRMPayPassword); }
							if (orderBy.IRMScanPayNotifyUrl) { query = query.OrderBy(a => a.IRMScanPayNotifyUrl); }
							if (orderBy.IRMWxUsername) { query = query.OrderBy(a => a.IRMWxUsername); }
							if (orderBy.IRMWxPassword) { query = query.OrderBy(a => a.IRMWxPassword); }
							if (orderBy.IRMRefreshBalance) { query = query.OrderBy(a => a.IRMRefreshBalance); }
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
        IQueryable<IRobotRobotManager> OrderByDesc(IQueryable<IRobotRobotManager> query, IRobotRobotManagerOrderBy orderBy)
        {
            if (orderBy != null)
            {

							if (orderBy.IRMID) { query = query.OrderByDescending(a => a.IRMID); }else 
							if (orderBy.IRMRobotId) { query = query.OrderByDescending(a => a.IRMRobotId); }else 
							if (orderBy.IRMRobotState) { query = query.OrderByDescending(a => a.IRMRobotState); }else 
							if (orderBy.IRMRuningTime) { query = query.OrderByDescending(a => a.IRMRuningTime); }else 
							if (orderBy.IRMMaxPayTradeCount) { query = query.OrderByDescending(a => a.IRMMaxPayTradeCount); }else 
							if (orderBy.IRMCurrentPayCount) { query = query.OrderByDescending(a => a.IRMCurrentPayCount); }else 
							if (orderBy.IRMBalance) { query = query.OrderByDescending(a => a.IRMBalance); }else 
							if (orderBy.IRMIP) { query = query.OrderByDescending(a => a.IRMIP); }else 
							if (orderBy.IRMCreateTime) { query = query.OrderByDescending(a => a.IRMCreateTime); }else 
							if (orderBy.IRMTeamViewId) { query = query.OrderByDescending(a => a.IRMTeamViewId); }else 
							if (orderBy.IRMTeamViewPassword) { query = query.OrderByDescending(a => a.IRMTeamViewPassword); }else 
							if (orderBy.IRMSunflowerId) { query = query.OrderByDescending(a => a.IRMSunflowerId); }else 
							if (orderBy.IRMSunflowerPassword) { query = query.OrderByDescending(a => a.IRMSunflowerPassword); }else 
							if (orderBy.IRMBankCardTailNum) { query = query.OrderByDescending(a => a.IRMBankCardTailNum); }else 
							if (orderBy.IRMBankCardName) { query = query.OrderByDescending(a => a.IRMBankCardName); }else 
							if (orderBy.IRMMinBalance) { query = query.OrderByDescending(a => a.IRMMinBalance); }else 
							if (orderBy.IRMPayPassword) { query = query.OrderByDescending(a => a.IRMPayPassword); }else 
							if (orderBy.IRMScanPayNotifyUrl) { query = query.OrderByDescending(a => a.IRMScanPayNotifyUrl); }else 
							if (orderBy.IRMWxUsername) { query = query.OrderByDescending(a => a.IRMWxUsername); }else 
							if (orderBy.IRMWxPassword) { query = query.OrderByDescending(a => a.IRMWxPassword); }else 
							if (orderBy.IRMRefreshBalance) { query = query.OrderByDescending(a => a.IRMRefreshBalance); }
				else
				{
					query = query.OrderByDescending(a => a.IRMID);
				}
            }
            else
            {
                query = query.OrderByDescending(a => a.IRMID);
            }
            return query;
        }

		/// <summary>
        /// 修改变化值，把entity不为null的数据视为变化值
        /// </summary>
        /// <param name="entity">修改实体类，必须要传入主键，其余的参数如果不为null则视为变化值（主键id除外，只做数据标识）</param>
        /// <returns>返回修改的数据行数</returns>
        public int UpdateChange(IRobotRobotManager entity)
        {
            using (BaseDbContext dbContext = CreateDbContext())
            {
                IRobotRobotManager  updateBefore = new IRobotRobotManager  { IRMID = entity.IRMID};
                dbContext.Set<IRobotRobotManager>().Attach(updateBefore);
								if (entity.IRMRobotId != null)
								{
									updateBefore.IRMRobotId = entity.IRMRobotId;
								}					if (entity.IRMRobotState != null)
								{
									updateBefore.IRMRobotState = entity.IRMRobotState;
								}					if (entity.IRMRuningTime != null)
								{
									updateBefore.IRMRuningTime = entity.IRMRuningTime;
								}					if (entity.IRMMaxPayTradeCount != null)
								{
									updateBefore.IRMMaxPayTradeCount = entity.IRMMaxPayTradeCount;
								}					if (entity.IRMCurrentPayCount != null)
								{
									updateBefore.IRMCurrentPayCount = entity.IRMCurrentPayCount;
								}					if (entity.IRMBalance != null)
								{
									updateBefore.IRMBalance = entity.IRMBalance;
								}					if (entity.IRMIP != null)
								{
									updateBefore.IRMIP = entity.IRMIP;
								}					if (entity.IRMCreateTime != null)
								{
									updateBefore.IRMCreateTime = entity.IRMCreateTime;
								}					if (entity.IRMTeamViewId != null)
								{
									updateBefore.IRMTeamViewId = entity.IRMTeamViewId;
								}					if (entity.IRMTeamViewPassword != null)
								{
									updateBefore.IRMTeamViewPassword = entity.IRMTeamViewPassword;
								}					if (entity.IRMSunflowerId != null)
								{
									updateBefore.IRMSunflowerId = entity.IRMSunflowerId;
								}					if (entity.IRMSunflowerPassword != null)
								{
									updateBefore.IRMSunflowerPassword = entity.IRMSunflowerPassword;
								}					if (entity.IRMBankCardTailNum != null)
								{
									updateBefore.IRMBankCardTailNum = entity.IRMBankCardTailNum;
								}					if (entity.IRMBankCardName != null)
								{
									updateBefore.IRMBankCardName = entity.IRMBankCardName;
								}					if (entity.IRMMinBalance != null)
								{
									updateBefore.IRMMinBalance = entity.IRMMinBalance;
								}					if (entity.IRMPayPassword != null)
								{
									updateBefore.IRMPayPassword = entity.IRMPayPassword;
								}					if (entity.IRMScanPayNotifyUrl != null)
								{
									updateBefore.IRMScanPayNotifyUrl = entity.IRMScanPayNotifyUrl;
								}					if (entity.IRMWxUsername != null)
								{
									updateBefore.IRMWxUsername = entity.IRMWxUsername;
								}					if (entity.IRMWxPassword != null)
								{
									updateBefore.IRMWxPassword = entity.IRMWxPassword;
								}					if (entity.IRMRefreshBalance != null)
								{
									updateBefore.IRMRefreshBalance = entity.IRMRefreshBalance;
								}
                return dbContext.SaveChanges();
            }
        }

		/// <summary>
        /// 批量修改变化值，把entity不为null的数据视为变化值
        /// </summary>
        /// <param name="entities">修改实体类集合，必须要传入主键，其余的参数如果不为null则视为变化值（主键id除外，只做数据标识）</param>
        /// <returns>返回修改的数据行数</returns>
		public int UpdateChangeBatch(List<IRobotRobotManager> entities)
		{
			using (BaseDbContext dbContext = CreateDbContext())
            {
				foreach(IRobotRobotManager entity in entities)
				{
					IRobotRobotManager  updateBefore = new IRobotRobotManager  { IRMID = entity.IRMID};
					dbContext.Set<IRobotRobotManager>().Attach(updateBefore);
								if (entity.IRMRobotId != null)
								{
									updateBefore.IRMRobotId = entity.IRMRobotId;
								}					if (entity.IRMRobotState != null)
								{
									updateBefore.IRMRobotState = entity.IRMRobotState;
								}					if (entity.IRMRuningTime != null)
								{
									updateBefore.IRMRuningTime = entity.IRMRuningTime;
								}					if (entity.IRMMaxPayTradeCount != null)
								{
									updateBefore.IRMMaxPayTradeCount = entity.IRMMaxPayTradeCount;
								}					if (entity.IRMCurrentPayCount != null)
								{
									updateBefore.IRMCurrentPayCount = entity.IRMCurrentPayCount;
								}					if (entity.IRMBalance != null)
								{
									updateBefore.IRMBalance = entity.IRMBalance;
								}					if (entity.IRMIP != null)
								{
									updateBefore.IRMIP = entity.IRMIP;
								}					if (entity.IRMCreateTime != null)
								{
									updateBefore.IRMCreateTime = entity.IRMCreateTime;
								}					if (entity.IRMTeamViewId != null)
								{
									updateBefore.IRMTeamViewId = entity.IRMTeamViewId;
								}					if (entity.IRMTeamViewPassword != null)
								{
									updateBefore.IRMTeamViewPassword = entity.IRMTeamViewPassword;
								}					if (entity.IRMSunflowerId != null)
								{
									updateBefore.IRMSunflowerId = entity.IRMSunflowerId;
								}					if (entity.IRMSunflowerPassword != null)
								{
									updateBefore.IRMSunflowerPassword = entity.IRMSunflowerPassword;
								}					if (entity.IRMBankCardTailNum != null)
								{
									updateBefore.IRMBankCardTailNum = entity.IRMBankCardTailNum;
								}					if (entity.IRMBankCardName != null)
								{
									updateBefore.IRMBankCardName = entity.IRMBankCardName;
								}					if (entity.IRMMinBalance != null)
								{
									updateBefore.IRMMinBalance = entity.IRMMinBalance;
								}					if (entity.IRMPayPassword != null)
								{
									updateBefore.IRMPayPassword = entity.IRMPayPassword;
								}					if (entity.IRMScanPayNotifyUrl != null)
								{
									updateBefore.IRMScanPayNotifyUrl = entity.IRMScanPayNotifyUrl;
								}					if (entity.IRMWxUsername != null)
								{
									updateBefore.IRMWxUsername = entity.IRMWxUsername;
								}					if (entity.IRMWxPassword != null)
								{
									updateBefore.IRMWxPassword = entity.IRMWxPassword;
								}					if (entity.IRMRefreshBalance != null)
								{
									updateBefore.IRMRefreshBalance = entity.IRMRefreshBalance;
								}
				}
				return dbContext.SaveChanges();
			}
		}

		/// <summary>
        /// 把指定字段值设置为null
        /// </summary>
        /// <param name="param">设为null的字段参数，主键必须要传入，其他字段等于true则表示设置为null</param>
        /// <returns>返回修改的数据行数</returns>
		public void SetNull(IRobotRobotManagerSetNullParams param)
		{
			 IRobotRobotManager updateBefore = FindEntity(param.IRMID);
			 using (BaseDbContext dbContext = CreateDbContext())
             {
				dbContext.Set<IRobotRobotManager>().Attach(updateBefore);
								if (param.IRMRobotId)
								{
									updateBefore.IRMRobotId = null;
								}				if (param.IRMRobotState)
								{
									updateBefore.IRMRobotState = null;
								}				if (param.IRMRuningTime)
								{
									updateBefore.IRMRuningTime = null;
								}				if (param.IRMMaxPayTradeCount)
								{
									updateBefore.IRMMaxPayTradeCount = null;
								}				if (param.IRMCurrentPayCount)
								{
									updateBefore.IRMCurrentPayCount = null;
								}				if (param.IRMBalance)
								{
									updateBefore.IRMBalance = null;
								}				if (param.IRMIP)
								{
									updateBefore.IRMIP = null;
								}				if (param.IRMCreateTime)
								{
									updateBefore.IRMCreateTime = null;
								}				if (param.IRMTeamViewId)
								{
									updateBefore.IRMTeamViewId = null;
								}				if (param.IRMTeamViewPassword)
								{
									updateBefore.IRMTeamViewPassword = null;
								}				if (param.IRMSunflowerId)
								{
									updateBefore.IRMSunflowerId = null;
								}				if (param.IRMSunflowerPassword)
								{
									updateBefore.IRMSunflowerPassword = null;
								}				if (param.IRMBankCardTailNum)
								{
									updateBefore.IRMBankCardTailNum = null;
								}				if (param.IRMBankCardName)
								{
									updateBefore.IRMBankCardName = null;
								}				if (param.IRMMinBalance)
								{
									updateBefore.IRMMinBalance = null;
								}				if (param.IRMPayPassword)
								{
									updateBefore.IRMPayPassword = null;
								}				if (param.IRMScanPayNotifyUrl)
								{
									updateBefore.IRMScanPayNotifyUrl = null;
								}				if (param.IRMWxUsername)
								{
									updateBefore.IRMWxUsername = null;
								}				if (param.IRMWxPassword)
								{
									updateBefore.IRMWxPassword = null;
								}				if (param.IRMRefreshBalance)
								{
									updateBefore.IRMRefreshBalance = null;
								}
				dbContext.SaveChanges();
			 }
		}

		/// <summary>
        /// 检查分布式事务是否已完成
        /// </summary>
        /// <param name="primaryKeyVal">主键的值</param>
		public bool CheckTransactionFinish(long primaryKeyVal)
		{
			return CheckTransactionFinish(primaryKeyVal, "IRobot_RobotManager");
		}

		/// <summary>
        /// 分布式插入一条数据，数据的主键不能为null
        /// </summary>
        /// <param name="entity">实体对象</param>
		public void DistributedInsert(IRobotRobotManager entity)
		{
			if (DistributedTransactionScan.TransactionIds.Value == null)
            {
                throw new Exception("方法调用失败，失败原因：未开启分布式事务。");
            }
			using (BaseDbContext dbContext = CreateDbContext())
            {
				DistributedTransactionScan.TransactionDataSources.Value.Add(dbContext.TransactionDataSource);
				dbContext.Entry(entity).State = EntityState.Added;
				dbContext.Entry(new DistributedTransactionPart
                {
                    Id = IdWorker.NextId(),
                    DistributedTransactionMainId = (long)DistributedTransactionScan.TransactionIds.Value,
                    InverseOper = Convert.ToString(entity.IRMID),
					InverseOperType = 'D',
                    TransTableName = "IRobot_RobotManager",
                    TransPrimaryKey = "IRM_ID",
                    TransPrimaryKeyVal= entity.IRMID,
                    TransactionStatus = 0,
                    CreateDate = DateTime.Now,
                }).State = EntityState.Added;
				dbContext.SaveChanges();
			}
		}

		/// <summary>
        /// 分布式批量插入数据，数据的主键不能为null
        /// </summary>
        /// <param name="entity">实体对象</param>
		public void DistributedInsert(List<IRobotRobotManager> entities)
		{
			if (DistributedTransactionScan.TransactionIds.Value == null)
            {
                throw new Exception("方法调用失败，失败原因：未开启分布式事务。");
            }
			using (BaseDbContext dbContext = CreateDbContext())
            {
				DistributedTransactionScan.TransactionDataSources.Value.Add(dbContext.TransactionDataSource);
				foreach(IRobotRobotManager entity in entities)
				{
					dbContext.Entry(entity).State = EntityState.Added;
					dbContext.Entry(new DistributedTransactionPart
					{
						Id = IdWorker.NextId(),
						DistributedTransactionMainId = (long)DistributedTransactionScan.TransactionIds.Value,
						InverseOper = Convert.ToString(entity.IRMID),
						InverseOperType = 'D',
						TransTableName = "IRobot_RobotManager",
						TransPrimaryKey = "IRM_ID",
						TransPrimaryKeyVal= entity.IRMID,
						TransactionStatus = 0,
						CreateDate = DateTime.Now,
					}).State = EntityState.Added;
				}
				dbContext.SaveChanges();
			}
		}

		/// <summary>
        /// 分布式把指定字段值设置为null，主键不能为null
        /// </summary>
        /// <param name="entity">实体对象</param>
		public void DistributedSetNull(IRobotRobotManagerSetNullParams param)
		{
			if (DistributedTransactionScan.TransactionIds.Value == null)
            {
                throw new Exception("方法调用失败，失败原因：未开启分布式事务。");
            }
			using (BaseDbContext dbContext = CreateDbContext())
            {
				IRobotRobotManager beforeData=dbContext.Set<IRobotRobotManager>().AsNoTracking().AsQueryable().Where(a=>a.IRMID==param.IRMID).FirstOrDefault();
				DistributedTransactionScan.TransactionDataSources.Value.Add(dbContext.TransactionDataSource);
				if(beforeData!=null)
				{
					IRobotRobotManager inverseData = new IRobotRobotManager{ IRMID = param.IRMID};
					DistributedTransactionScan.TransactionDataSources.Value.Add(dbContext.TransactionDataSource);
					dbContext.Set<IRobotRobotManager>().Attach(beforeData);
										if (param.IRMRobotId)
										{
											inverseData.IRMRobotId = beforeData.IRMRobotId;
											beforeData.IRMRobotId = null;
										}					if (param.IRMRobotState)
										{
											inverseData.IRMRobotState = beforeData.IRMRobotState;
											beforeData.IRMRobotState = null;
										}					if (param.IRMRuningTime)
										{
											inverseData.IRMRuningTime = beforeData.IRMRuningTime;
											beforeData.IRMRuningTime = null;
										}					if (param.IRMMaxPayTradeCount)
										{
											inverseData.IRMMaxPayTradeCount = beforeData.IRMMaxPayTradeCount;
											beforeData.IRMMaxPayTradeCount = null;
										}					if (param.IRMCurrentPayCount)
										{
											inverseData.IRMCurrentPayCount = beforeData.IRMCurrentPayCount;
											beforeData.IRMCurrentPayCount = null;
										}					if (param.IRMBalance)
										{
											inverseData.IRMBalance = beforeData.IRMBalance;
											beforeData.IRMBalance = null;
										}					if (param.IRMIP)
										{
											inverseData.IRMIP = beforeData.IRMIP;
											beforeData.IRMIP = null;
										}					if (param.IRMCreateTime)
										{
											inverseData.IRMCreateTime = beforeData.IRMCreateTime;
											beforeData.IRMCreateTime = null;
										}					if (param.IRMTeamViewId)
										{
											inverseData.IRMTeamViewId = beforeData.IRMTeamViewId;
											beforeData.IRMTeamViewId = null;
										}					if (param.IRMTeamViewPassword)
										{
											inverseData.IRMTeamViewPassword = beforeData.IRMTeamViewPassword;
											beforeData.IRMTeamViewPassword = null;
										}					if (param.IRMSunflowerId)
										{
											inverseData.IRMSunflowerId = beforeData.IRMSunflowerId;
											beforeData.IRMSunflowerId = null;
										}					if (param.IRMSunflowerPassword)
										{
											inverseData.IRMSunflowerPassword = beforeData.IRMSunflowerPassword;
											beforeData.IRMSunflowerPassword = null;
										}					if (param.IRMBankCardTailNum)
										{
											inverseData.IRMBankCardTailNum = beforeData.IRMBankCardTailNum;
											beforeData.IRMBankCardTailNum = null;
										}					if (param.IRMBankCardName)
										{
											inverseData.IRMBankCardName = beforeData.IRMBankCardName;
											beforeData.IRMBankCardName = null;
										}					if (param.IRMMinBalance)
										{
											inverseData.IRMMinBalance = beforeData.IRMMinBalance;
											beforeData.IRMMinBalance = null;
										}					if (param.IRMPayPassword)
										{
											inverseData.IRMPayPassword = beforeData.IRMPayPassword;
											beforeData.IRMPayPassword = null;
										}					if (param.IRMScanPayNotifyUrl)
										{
											inverseData.IRMScanPayNotifyUrl = beforeData.IRMScanPayNotifyUrl;
											beforeData.IRMScanPayNotifyUrl = null;
										}					if (param.IRMWxUsername)
										{
											inverseData.IRMWxUsername = beforeData.IRMWxUsername;
											beforeData.IRMWxUsername = null;
										}					if (param.IRMWxPassword)
										{
											inverseData.IRMWxPassword = beforeData.IRMWxPassword;
											beforeData.IRMWxPassword = null;
										}					if (param.IRMRefreshBalance)
										{
											inverseData.IRMRefreshBalance = beforeData.IRMRefreshBalance;
											beforeData.IRMRefreshBalance = null;
										}
					dbContext.Entry(new DistributedTransactionPart
					{
						Id = IdWorker.NextId(),
						DistributedTransactionMainId = (long)DistributedTransactionScan.TransactionIds.Value,
						InverseOper = CompressHelper.GZipCompressString(JsonConvert.SerializeObject(inverseData,TimeConverter)),
						InverseOperType = 'N',
						TransTableName = "IRobot_RobotManager",
						TransPrimaryKey = "IRM_ID",
						TransPrimaryKeyVal= param.IRMID,
						TransactionStatus = 0,
						CreateDate = DateTime.Now,
					}).State = EntityState.Added;
					dbContext.SaveChanges();
				}
			}
		}

		/// <summary>
        /// 分布式部分更新数据，数据的主键不能为null，只更新不为null的值
        /// </summary>
        /// <param name="entity">实体对象</param>
		public void DistributedUpdateChange(IRobotRobotManager entity)
		{
			if (DistributedTransactionScan.TransactionIds.Value == null)
            {
                throw new Exception("方法调用失败，失败原因：未开启分布式事务。");
            }
			using (BaseDbContext dbContext = CreateDbContext())
            {
				IRobotRobotManager beforeData=dbContext.Set<IRobotRobotManager>().AsNoTracking().AsQueryable().Where(a=>a.IRMID==entity.IRMID).FirstOrDefault();
				if(beforeData!=null)
				{
					IRobotRobotManager inverseData = new IRobotRobotManager{ IRMID = entity.IRMID};
					DistributedTransactionScan.TransactionDataSources.Value.Add(dbContext.TransactionDataSource);
					IRobotRobotManager updateBefore = new IRobotRobotManager { IRMID = entity.IRMID};
					dbContext.Set<IRobotRobotManager>().Attach(updateBefore);
											if (entity.IRMRobotId != null)
											{
												updateBefore.IRMRobotId = entity.IRMRobotId;
												inverseData.IRMRobotId = beforeData.IRMRobotId;
											}						if (entity.IRMRobotState != null)
											{
												updateBefore.IRMRobotState = entity.IRMRobotState;
												inverseData.IRMRobotState = beforeData.IRMRobotState;
											}						if (entity.IRMRuningTime != null)
											{
												updateBefore.IRMRuningTime = entity.IRMRuningTime;
												inverseData.IRMRuningTime = beforeData.IRMRuningTime;
											}						if (entity.IRMMaxPayTradeCount != null)
											{
												updateBefore.IRMMaxPayTradeCount = entity.IRMMaxPayTradeCount;
												inverseData.IRMMaxPayTradeCount = beforeData.IRMMaxPayTradeCount;
											}						if (entity.IRMCurrentPayCount != null)
											{
												updateBefore.IRMCurrentPayCount = entity.IRMCurrentPayCount;
												inverseData.IRMCurrentPayCount = beforeData.IRMCurrentPayCount;
											}						if (entity.IRMBalance != null)
											{
												updateBefore.IRMBalance = entity.IRMBalance;
												inverseData.IRMBalance = beforeData.IRMBalance;
											}						if (entity.IRMIP != null)
											{
												updateBefore.IRMIP = entity.IRMIP;
												inverseData.IRMIP = beforeData.IRMIP;
											}						if (entity.IRMCreateTime != null)
											{
												updateBefore.IRMCreateTime = entity.IRMCreateTime;
												inverseData.IRMCreateTime = beforeData.IRMCreateTime;
											}						if (entity.IRMTeamViewId != null)
											{
												updateBefore.IRMTeamViewId = entity.IRMTeamViewId;
												inverseData.IRMTeamViewId = beforeData.IRMTeamViewId;
											}						if (entity.IRMTeamViewPassword != null)
											{
												updateBefore.IRMTeamViewPassword = entity.IRMTeamViewPassword;
												inverseData.IRMTeamViewPassword = beforeData.IRMTeamViewPassword;
											}						if (entity.IRMSunflowerId != null)
											{
												updateBefore.IRMSunflowerId = entity.IRMSunflowerId;
												inverseData.IRMSunflowerId = beforeData.IRMSunflowerId;
											}						if (entity.IRMSunflowerPassword != null)
											{
												updateBefore.IRMSunflowerPassword = entity.IRMSunflowerPassword;
												inverseData.IRMSunflowerPassword = beforeData.IRMSunflowerPassword;
											}						if (entity.IRMBankCardTailNum != null)
											{
												updateBefore.IRMBankCardTailNum = entity.IRMBankCardTailNum;
												inverseData.IRMBankCardTailNum = beforeData.IRMBankCardTailNum;
											}						if (entity.IRMBankCardName != null)
											{
												updateBefore.IRMBankCardName = entity.IRMBankCardName;
												inverseData.IRMBankCardName = beforeData.IRMBankCardName;
											}						if (entity.IRMMinBalance != null)
											{
												updateBefore.IRMMinBalance = entity.IRMMinBalance;
												inverseData.IRMMinBalance = beforeData.IRMMinBalance;
											}						if (entity.IRMPayPassword != null)
											{
												updateBefore.IRMPayPassword = entity.IRMPayPassword;
												inverseData.IRMPayPassword = beforeData.IRMPayPassword;
											}						if (entity.IRMScanPayNotifyUrl != null)
											{
												updateBefore.IRMScanPayNotifyUrl = entity.IRMScanPayNotifyUrl;
												inverseData.IRMScanPayNotifyUrl = beforeData.IRMScanPayNotifyUrl;
											}						if (entity.IRMWxUsername != null)
											{
												updateBefore.IRMWxUsername = entity.IRMWxUsername;
												inverseData.IRMWxUsername = beforeData.IRMWxUsername;
											}						if (entity.IRMWxPassword != null)
											{
												updateBefore.IRMWxPassword = entity.IRMWxPassword;
												inverseData.IRMWxPassword = beforeData.IRMWxPassword;
											}						if (entity.IRMRefreshBalance != null)
											{
												updateBefore.IRMRefreshBalance = entity.IRMRefreshBalance;
												inverseData.IRMRefreshBalance = beforeData.IRMRefreshBalance;
											}
					dbContext.Entry(new DistributedTransactionPart
					{
						Id = IdWorker.NextId(),
						DistributedTransactionMainId = (long)DistributedTransactionScan.TransactionIds.Value,
						InverseOper = CompressHelper.GZipCompressString(JsonConvert.SerializeObject(inverseData,TimeConverter)),
						InverseOperType = 'C',
						TransTableName = "IRobot_RobotManager",
						TransPrimaryKey = "IRM_ID",
						TransPrimaryKeyVal= entity.IRMID,
						TransactionStatus = 0,
						CreateDate = DateTime.Now,
					}).State = EntityState.Added;
					dbContext.SaveChanges();
				}
			}
		}

		/// <summary>
        /// 分布式全覆盖更新数据，数据的主键不能为null，所有属性全部覆盖数据库记录原有的值
        /// </summary>
        /// <param name="entity">实体对象</param>
		public void DistributedUpdateAll(IRobotRobotManager entity)
		{
			if (DistributedTransactionScan.TransactionIds.Value == null)
            {
                throw new Exception("方法调用失败，失败原因：未开启分布式事务。");
            }
			using (BaseDbContext dbContext = CreateDbContext())
            {
				IRobotRobotManager beforeData=dbContext.Set<IRobotRobotManager>().AsNoTracking().AsQueryable().Where(a=>a.IRMID==entity.IRMID).FirstOrDefault();
				if(beforeData!=null)
				{
					DistributedTransactionScan.TransactionDataSources.Value.Add(dbContext.TransactionDataSource);
					dbContext.Entry(entity).State = EntityState.Modified;
					dbContext.Entry(new DistributedTransactionPart
					{
						Id = IdWorker.NextId(),
						DistributedTransactionMainId = (long)DistributedTransactionScan.TransactionIds.Value,
						InverseOper = CompressHelper.GZipCompressString(JsonConvert.SerializeObject(beforeData,TimeConverter)),
						InverseOperType = 'A',
						TransTableName = "IRobot_RobotManager",
						TransPrimaryKey = "IRM_ID",
						TransPrimaryKeyVal= entity.IRMID,
						TransactionStatus = 0,
						CreateDate = DateTime.Now,
					}).State = EntityState.Added;
					dbContext.SaveChanges();
				}
			}
		}

		/// <summary>
        /// 分布式批量全覆盖更新数据，数据的主键不能为null，所有属性全部覆盖数据库记录原有的值
        /// </summary>
        /// <param name="entity">实体对象</param>
		public void DistributedUpdateAllBatch(List<IRobotRobotManager> entities)
		{
			if (DistributedTransactionScan.TransactionIds.Value == null)
            {
                throw new Exception("方法调用失败，失败原因：未开启分布式事务。");
            }
			using (BaseDbContext dbContext = CreateDbContext())
            {
				foreach(IRobotRobotManager entity in entities)
				{
					IRobotRobotManager beforeData=dbContext.Set<IRobotRobotManager>().AsNoTracking().AsQueryable().Where(a=>a.IRMID==entity.IRMID).FirstOrDefault();
					if(beforeData!=null)
					{
						DistributedTransactionScan.TransactionDataSources.Value.Add(dbContext.TransactionDataSource);
						dbContext.Entry(entity).State = EntityState.Modified;
						dbContext.Entry(new DistributedTransactionPart
						{
							Id = IdWorker.NextId(),
							DistributedTransactionMainId = (long)DistributedTransactionScan.TransactionIds.Value,
							InverseOper = CompressHelper.GZipCompressString(JsonConvert.SerializeObject(beforeData,TimeConverter)),
							InverseOperType = 'A',
							TransTableName = "IRobot_RobotManager",
							TransPrimaryKey = "IRM_ID",
							TransPrimaryKeyVal= entity.IRMID,
							TransactionStatus = 0,
							CreateDate = DateTime.Now,
						}).State = EntityState.Added;
					}
				}
				dbContext.SaveChanges();
			}
		}

		/// <summary>
        /// 分布式删除一条数据，数据的主键不能为null
        /// </summary>
        /// <param name="entity">实体对象</param>
		public void DistributedDelete(IRobotRobotManager entity)
		{
			if (DistributedTransactionScan.TransactionIds.Value == null)
            {
                throw new Exception("方法调用失败，失败原因：未开启分布式事务。");
            }
			using (BaseDbContext dbContext = CreateDbContext())
            {
				IRobotRobotManager beforeData=dbContext.Set<IRobotRobotManager>().AsNoTracking().AsQueryable().Where(a=>a.IRMID==entity.IRMID).FirstOrDefault();
				if(beforeData!=null){
					DistributedTransactionScan.TransactionDataSources.Value.Add(dbContext.TransactionDataSource);
					dbContext.Entry(entity).State = EntityState.Deleted;
					dbContext.Entry(new DistributedTransactionPart
					{
						Id = IdWorker.NextId(),
						DistributedTransactionMainId = (long)DistributedTransactionScan.TransactionIds.Value,
						InverseOper = CompressHelper.GZipCompressString(JsonConvert.SerializeObject(beforeData,TimeConverter)),
						InverseOperType = 'I',
						TransTableName = "IRobot_RobotManager",
						TransPrimaryKey = "IRM_ID",
						TransPrimaryKeyVal= entity.IRMID,
						TransactionStatus = 0,
						CreateDate = DateTime.Now,
					}).State = EntityState.Added;
					dbContext.SaveChanges();
				}
			}
		}

		/// <summary>
        /// 分布式删除一条数据，数据的主键不能为null
        /// </summary>
        /// <param name="entity">实体对象</param>
		public void DistributedDelete(List<IRobotRobotManager> entities)
		{
			if (DistributedTransactionScan.TransactionIds.Value == null)
            {
                throw new Exception("方法调用失败，失败原因：未开启分布式事务。");
            }
			using (BaseDbContext dbContext = CreateDbContext())
            {
				foreach(IRobotRobotManager entity in entities)
				{				
					IRobotRobotManager beforeData=dbContext.Set<IRobotRobotManager>().AsNoTracking().AsQueryable().Where(a=>a.IRMID==entity.IRMID).FirstOrDefault();
					if(beforeData!=null){
						DistributedTransactionScan.TransactionDataSources.Value.Add(dbContext.TransactionDataSource);
						dbContext.Entry(entity).State = EntityState.Deleted;
						dbContext.Entry(new DistributedTransactionPart
						{
							Id = IdWorker.NextId(),
							DistributedTransactionMainId = (long)DistributedTransactionScan.TransactionIds.Value,
							InverseOper = CompressHelper.GZipCompressString(JsonConvert.SerializeObject(beforeData,TimeConverter)),
							InverseOperType = 'I',
							TransTableName = "IRobot_RobotManager",
							TransPrimaryKey = "IRM_ID",
							TransPrimaryKeyVal= entity.IRMID,
							TransactionStatus = 0,
							CreateDate = DateTime.Now,
						}).State = EntityState.Added;
					}
				}
				dbContext.SaveChanges();
			}
		}

		public override BaseDbContext CreateDbContext()
        {
            return new MyDbContext2();
        }
    }
}