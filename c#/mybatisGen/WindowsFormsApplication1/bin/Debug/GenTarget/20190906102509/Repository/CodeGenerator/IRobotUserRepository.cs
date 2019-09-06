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
namespace WebApplication1.Repository
{
	//启用分布式事务
    //[Intercept(typeof(DistributeRepository))]
    public partial class IRobotUserRepository : BaseRepository<IRobotUser,IRobotUserParams,IRobotUserOrderBy,IRobotUserSetNullParams>
    {
		/// <summary>
        /// 通用的设置查询参数方法，只有在参数不为null的情况下才会设置，
		/// 满足项目中常用的模糊查询，分页查询等功能
        /// </summary>
        /// <param name="query">待设置参数的query对象</param>
        /// <param name="eqArgs">装载查询参数的实体类</param>
		/// <param name="neqArgs">装载不等于查询参数的实体类</param>
        /// <returns>返回设置好查询参数的query对象</returns>
        protected override IQueryable<IRobotUser> Query(IQueryable<IRobotUser> query, IRobotUserParams eqArgs, IRobotUserParams neqArgs = null)
        {
            if (eqArgs != null)
            {

							if (eqArgs.IUId != null)
							{
								query = query.Where(a => a.IUId == eqArgs.IUId);
							}

							if (eqArgs.IUUsername != null)
							{
								query = query.Where(a => a.IUUsername == eqArgs.IUUsername);
							}

							if (!string.IsNullOrEmpty(eqArgs.IUUsernameLike))
			                {
			                    query = query.Where(a => a.IUUsername.Contains(eqArgs.IUUsernameLike));
			                }
							if (eqArgs.IUPassword != null)
							{
								query = query.Where(a => a.IUPassword == eqArgs.IUPassword);
							}

							if (!string.IsNullOrEmpty(eqArgs.IUPasswordLike))
			                {
			                    query = query.Where(a => a.IUPassword.Contains(eqArgs.IUPasswordLike));
			                }
							if (eqArgs.IUSignKey != null)
							{
								query = query.Where(a => a.IUSignKey == eqArgs.IUSignKey);
							}

							if (!string.IsNullOrEmpty(eqArgs.IUSignKeyLike))
			                {
			                    query = query.Where(a => a.IUSignKey.Contains(eqArgs.IUSignKeyLike));
			                }
							if (eqArgs.IUSignSecret != null)
							{
								query = query.Where(a => a.IUSignSecret == eqArgs.IUSignSecret);
							}

							if (!string.IsNullOrEmpty(eqArgs.IUSignSecretLike))
			                {
			                    query = query.Where(a => a.IUSignSecret.Contains(eqArgs.IUSignSecretLike));
			                }
							if (eqArgs.IUCreateDate != null)
							{
								query = query.Where(a => a.IUCreateDate == eqArgs.IUCreateDate);
							}

							if (eqArgs.IUCreateDateStart != null)
			                {
			                    query = query.Where(a => a.IUCreateDate >= eqArgs.IUCreateDateStart);
			                }
							if (eqArgs.IUCreateDateEnd != null)
			                {
			                    query = query.Where(a => a.IUCreateDate <= eqArgs.IUCreateDateEnd);
			                }
            }
			if(neqArgs != null){

							if (neqArgs.IUId != null)
							{
								query = query.Where(a => a.IUId != neqArgs.IUId);
							}

							if (neqArgs.IUUsername != null)
							{
								query = query.Where(a => a.IUUsername != neqArgs.IUUsername);
							}

							if (!string.IsNullOrEmpty(neqArgs.IUUsernameLike))
			                {
								query = query.Where(a => !a.IUUsername.Contains(neqArgs.IUUsernameLike));
							}
							if (neqArgs.IUPassword != null)
							{
								query = query.Where(a => a.IUPassword != neqArgs.IUPassword);
							}

							if (!string.IsNullOrEmpty(neqArgs.IUPasswordLike))
			                {
								query = query.Where(a => !a.IUPassword.Contains(neqArgs.IUPasswordLike));
							}
							if (neqArgs.IUSignKey != null)
							{
								query = query.Where(a => a.IUSignKey != neqArgs.IUSignKey);
							}

							if (!string.IsNullOrEmpty(neqArgs.IUSignKeyLike))
			                {
								query = query.Where(a => !a.IUSignKey.Contains(neqArgs.IUSignKeyLike));
							}
							if (neqArgs.IUSignSecret != null)
							{
								query = query.Where(a => a.IUSignSecret != neqArgs.IUSignSecret);
							}

							if (!string.IsNullOrEmpty(neqArgs.IUSignSecretLike))
			                {
								query = query.Where(a => !a.IUSignSecret.Contains(neqArgs.IUSignSecretLike));
							}
							if (neqArgs.IUCreateDate != null)
							{
								query = query.Where(a => a.IUCreateDate != neqArgs.IUCreateDate);
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
        IQueryable<IRobotUser> OrderByAsc(IQueryable<IRobotUser> query, IRobotUserOrderBy orderBy)
        {
            if (orderBy != null)
            {

							if (orderBy.IUId) { query = query.OrderBy(a => a.IUId); }
							if (orderBy.IUUsername) { query = query.OrderBy(a => a.IUUsername); }
							if (orderBy.IUPassword) { query = query.OrderBy(a => a.IUPassword); }
							if (orderBy.IUSignKey) { query = query.OrderBy(a => a.IUSignKey); }
							if (orderBy.IUSignSecret) { query = query.OrderBy(a => a.IUSignSecret); }
							if (orderBy.IUCreateDate) { query = query.OrderBy(a => a.IUCreateDate); }
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
        IQueryable<IRobotUser> OrderByDesc(IQueryable<IRobotUser> query, IRobotUserOrderBy orderBy)
        {
            if (orderBy != null)
            {

							if (orderBy.IUId) { query = query.OrderByDescending(a => a.IUId); }else 
							if (orderBy.IUUsername) { query = query.OrderByDescending(a => a.IUUsername); }else 
							if (orderBy.IUPassword) { query = query.OrderByDescending(a => a.IUPassword); }else 
							if (orderBy.IUSignKey) { query = query.OrderByDescending(a => a.IUSignKey); }else 
							if (orderBy.IUSignSecret) { query = query.OrderByDescending(a => a.IUSignSecret); }else 
							if (orderBy.IUCreateDate) { query = query.OrderByDescending(a => a.IUCreateDate); }
				else
				{
					query = query.OrderByDescending(a => a.IUId);
				}
            }
            else
            {
                query = query.OrderByDescending(a => a.IUId);
            }
            return query;
        }

		/// <summary>
        /// 修改变化值的部分共用代码，这里抽取出来简化结构
        /// </summary>
        /// <param name="updateBefore"></param>
        /// <param name="entity"></param>
        protected override void UpdateChange(BaseDbContext dbContext, IRobotUser entity)
		{
			IRobotUser  updateBefore = new IRobotUser  { IUId = entity.IUId};
			dbContext.Set<IRobotUser>().Attach(updateBefore);
						if (entity.IUUsername != null)
						{
							updateBefore.IUUsername = entity.IUUsername;
						}			if (entity.IUPassword != null)
						{
							updateBefore.IUPassword = entity.IUPassword;
						}			if (entity.IUSignKey != null)
						{
							updateBefore.IUSignKey = entity.IUSignKey;
						}			if (entity.IUSignSecret != null)
						{
							updateBefore.IUSignSecret = entity.IUSignSecret;
						}			if (entity.IUCreateDate != null)
						{
							updateBefore.IUCreateDate = entity.IUCreateDate;
						}
		}

		/// <summary>
        /// 把指定字段值设置为null的部分共用代码，这里抽取出来简化结构
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="param"></param>
        protected override void SetNull(BaseDbContext dbContext, IRobotUserSetNullParams param)
		{
			IRobotUser updateBefore = FindEntity(param.IUId);
			dbContext.Set<IRobotUser>().Attach(updateBefore);
						if (param.IUUsername)
						{
							updateBefore.IUUsername = null;
						}			if (param.IUPassword)
						{
							updateBefore.IUPassword = null;
						}			if (param.IUSignKey)
						{
							updateBefore.IUSignKey = null;
						}			if (param.IUSignSecret)
						{
							updateBefore.IUSignSecret = null;
						}			if (param.IUCreateDate)
						{
							updateBefore.IUCreateDate = null;
						}
		}

		/// <summary>
        /// 检查分布式事务是否已完成
        /// </summary>
        /// <param name="primaryKeyVal">主键的值</param>
		public void CheckTransactionFinish(long primaryKeyVal)
		{
			CheckTransactionFinish(primaryKeyVal, "IRobot_User");
		}

		public override BaseDbContext CreateDbContext()
        {
            return new MyDbContext2();
        }

		public override InverseRepository<IRobotUser> CurrentInverse()
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

        protected override Func<IRobotUser, IComparable> GetOrderColAndOrderType(IRobotUserParams paramz, out bool orderType)
        {
            throw new NotImplementedException();
        }

        protected override IQueryable<IRobotUser> GetBetweenAnd(IQueryable<IRobotUser> query, IRobotUserParams paramz, IComparable baseExtremum, IComparable otherExtremum)
        {
            throw new NotImplementedException();
        }
    }
}