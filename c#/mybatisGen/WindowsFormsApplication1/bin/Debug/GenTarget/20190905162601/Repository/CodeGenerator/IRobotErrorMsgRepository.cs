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
namespace WebApplication1.Repository
{
	//启用分布式事务
    //[Intercept(typeof(DistributeRepository))]
    public partial class IRobotErrorMsgRepository : BaseRepository<IRobotErrorMsg,IRobotErrorMsgParams,IRobotErrorMsgOrderBy,IRobotErrorMsgSetNullParams>
    {
		/// <summary>
        /// 通用的设置查询参数方法，只有在参数不为null的情况下才会设置，
		/// 满足项目中常用的模糊查询，分页查询等功能
        /// </summary>
        /// <param name="query">待设置参数的query对象</param>
        /// <param name="eqArgs">装载查询参数的实体类</param>
		/// <param name="neqArgs">装载不等于查询参数的实体类</param>
        /// <returns>返回设置好查询参数的query对象</returns>
        protected override IQueryable<IRobotErrorMsg> Query(IQueryable<IRobotErrorMsg> query, IRobotErrorMsgParams eqArgs, IRobotErrorMsgParams neqArgs = null)
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
        /// 修改变化值的部分共用代码，这里抽取出来简化结构
        /// </summary>
        /// <param name="updateBefore"></param>
        /// <param name="entity"></param>
        protected override void UpdateChange(BaseDbContext dbContext, IRobotErrorMsg entity)
		{
			IRobotErrorMsg  updateBefore = new IRobotErrorMsg  { IEErrNo = entity.IEErrNo};
			dbContext.Set<IRobotErrorMsg>().Attach(updateBefore);
						if (entity.IECreateDate != null)
						{
							updateBefore.IECreateDate = entity.IECreateDate;
						}			if (entity.IEErrOrderNo != null)
						{
							updateBefore.IEErrOrderNo = entity.IEErrOrderNo;
						}			if (entity.IEErrRobotId != null)
						{
							updateBefore.IEErrRobotId = entity.IEErrRobotId;
						}			if (entity.IEErrPic != null)
						{
							updateBefore.IEErrPic = entity.IEErrPic;
						}			if (entity.IEErrContext != null)
						{
							updateBefore.IEErrContext = entity.IEErrContext;
						}			if (entity.IEHandleStatus != null)
						{
							updateBefore.IEHandleStatus = entity.IEHandleStatus;
						}
		}

		/// <summary>
        /// 把指定字段值设置为null的部分共用代码，这里抽取出来简化结构
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="param"></param>
        protected override void SetNull(BaseDbContext dbContext, IRobotErrorMsgSetNullParams param)
		{
			IRobotErrorMsg updateBefore = FindEntity(param.IEErrNo);
			dbContext.Set<IRobotErrorMsg>().Attach(updateBefore);
						if (param.IECreateDate)
						{
							updateBefore.IECreateDate = null;
						}			if (param.IEErrOrderNo)
						{
							updateBefore.IEErrOrderNo = null;
						}			if (param.IEErrRobotId)
						{
							updateBefore.IEErrRobotId = null;
						}			if (param.IEErrPic)
						{
							updateBefore.IEErrPic = null;
						}			if (param.IEErrContext)
						{
							updateBefore.IEErrContext = null;
						}			if (param.IEHandleStatus)
						{
							updateBefore.IEHandleStatus = null;
						}
		}

		/// <summary>
        /// 检查分布式事务是否已完成
        /// </summary>
        /// <param name="primaryKeyVal">主键的值</param>
		public void CheckTransactionFinish(long primaryKeyVal)
		{
			CheckTransactionFinish(primaryKeyVal, "IRobot_ErrorMsg");
		}

		public override BaseDbContext CreateDbContext()
        {
            return new MyDbContext2();
        }

		public override InverseRepository<IRobotErrorMsg> CurrentInverse()
        {
			using(BaseDbContext db = CreateDbContext())
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

        protected override Func<IRobotErrorMsg, IComparable> GetOrderColAndOrderType(IRobotErrorMsgParams paramz, out bool orderType)
        {
            throw new NotImplementedException();
        }

        protected override IQueryable<IRobotErrorMsg> GetBetweenAnd(IQueryable<IRobotErrorMsg> query, IRobotErrorMsgParams paramz, IComparable baseExtremum, IComparable otherExtremum)
        {
            throw new NotImplementedException();
        }
    }
}