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
using CommonHelper.staticVar;
using CommonHelper.EFRepository;
using System.Linq.Expressions;
using CommonHelper.EFMap;

namespace CommonWeb.Repository
{
    //启用分布式事务
    //[Intercept(typeof(DistributeRepository))]
    public abstract class HeartbeatEntityRepository : BaseRepository<HeartbeatEntity, HeartbeatEntityParams, HeartbeatEntitySetNullParams>
    {
        /// <summary>
        /// 通用的设置查询参数方法，只有在参数不为null的情况下才会设置，
        /// 满足项目中常用的模糊查询，分页查询等功能
        /// </summary>
        /// <param name="query">待设置参数的query对象</param>
        /// <param name="eqArgs">装载查询参数的实体类</param>
        /// <param name="neqArgs">装载不等于查询参数的实体类</param>
        /// <returns>返回设置好查询参数的query对象</returns>
        protected override IQueryable<HeartbeatEntity> Query(IQueryable<HeartbeatEntity> query, HeartbeatEntityParams eqArgs, HeartbeatEntityParams neqArgs = null)
        {
            if (eqArgs != null)
            {

                if (eqArgs.id != null)
                {
                    query = query.Where(a => a.Id == eqArgs.id);
                }

                if (eqArgs.lastHeartbeatTime != null)
                {
                    query = query.Where(a => a.LastHeartbeatTime == eqArgs.lastHeartbeatTime);
                }

                if (eqArgs.lastHeartbeatTimeStart != null)
                {
                    query = query.Where(a => a.LastHeartbeatTime >= eqArgs.lastHeartbeatTimeStart);
                }
                if (eqArgs.lastHeartbeatTimeEnd != null)
                {
                    query = query.Where(a => a.LastHeartbeatTime <= eqArgs.lastHeartbeatTimeEnd);
                }
                if (eqArgs.robotId != null)
                {
                    query = query.Where(a => a.RobotId == eqArgs.robotId);
                }

                if (!string.IsNullOrEmpty(eqArgs.robotIdLike))
                {
                    query = query.Where(a => a.RobotId.Contains(eqArgs.robotIdLike));
                }
                query = OrderBy(query, eqArgs);
            }
            if (neqArgs != null)
            {

                if (neqArgs.id != null)
                {
                    query = query.Where(a => a.Id != neqArgs.id);
                }

                if (neqArgs.lastHeartbeatTime != null)
                {
                    query = query.Where(a => a.LastHeartbeatTime != neqArgs.lastHeartbeatTime);
                }

                if (neqArgs.robotId != null)
                {
                    query = query.Where(a => a.RobotId != neqArgs.robotId);
                }

                if (!string.IsNullOrEmpty(neqArgs.robotIdLike))
                {
                    query = query.Where(a => !a.RobotId.Contains(neqArgs.robotIdLike));
                }
            }
            return query;
        }

        /// <summary>
        /// 升序、降序排序的查询参数设置，当对应字段的升序设置为true时才会对
        /// 该字段降序。
        /// </summary>
        /// <param name="query">待设置降序参数的query对象</param>
        /// <param name="orderBy">装载降序参数的实体类</param>
        /// <returns>返回设置好降序参数的query对象</returns>
        IQueryable<HeartbeatEntity> OrderBy(IQueryable<HeartbeatEntity> query, HeartbeatEntityParams eqArgs)
        {
            HeartbeatEntityOrderBy orderBy = eqArgs.orderByAsc;
            if (orderBy != null)
            {

                if (orderBy.id) { return query = query.OrderBy(a => a.Id); }
                else
                if (orderBy.lastHeartbeatTime) { return query = query.OrderBy(a => a.LastHeartbeatTime); }
                else
                if (orderBy.robotId) { return query = query.OrderBy(a => a.RobotId); }
            }
            orderBy = eqArgs.orderByDesc;
            if (orderBy != null)
            {

                if (orderBy.id) { return query = query.OrderByDescending(a => a.Id); }
                else
                if (orderBy.lastHeartbeatTime) { return query = query.OrderByDescending(a => a.LastHeartbeatTime); }
                else
                if (orderBy.robotId) { return query = query.OrderByDescending(a => a.RobotId); }
                else
                {
                    return query = query.OrderByDescending(a => a.Id);
                }
            }
            else
            {
                return query = query.OrderByDescending(a => a.Id);
            }
        }

        /// <summary>
        /// 修改变化值的部分共用代码，这里抽取出来简化结构
        /// </summary>
        /// <param name="updateBefore"></param>
        /// <param name="entity"></param>
        protected override void UpdateChange(BaseDbContext dbContext, HeartbeatEntity entity)
        {
            HeartbeatEntity updateBefore = new HeartbeatEntity { Id = entity.Id };
            dbContext.Set<HeartbeatEntity>().Attach(updateBefore);
            if (entity.LastHeartbeatTime != null)
            {
                updateBefore.LastHeartbeatTime = entity.LastHeartbeatTime;
            }
            if (entity.RobotId != null)
            {
                updateBefore.RobotId = entity.RobotId;
            }
        }

        /// <summary>
        /// 把指定字段值设置为null的部分共用代码，这里抽取出来简化结构
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="param"></param>
        protected override void SetNull(BaseDbContext dbContext, HeartbeatEntitySetNullParams param)
        {
            HeartbeatEntity updateBefore = FindEntity(param.id);
            dbContext.Set<HeartbeatEntity>().Attach(updateBefore);
            if (param.lastHeartbeatTime)
            {
                updateBefore.LastHeartbeatTime = null;
            }
            if (param.robotId)
            {
                updateBefore.RobotId = null;
            }
        }

        /// <summary>
        /// 检查分布式事务是否已完成
        /// </summary>
        /// <param name="primaryKeyVal">主键的值</param>
        public void CheckTransactionFinish(long primaryKeyVal)
        {
            CheckTransactionFinish(primaryKeyVal, "Heartbeat_Entity");
        }

        public override InverseRepository<HeartbeatEntity> CurrentInverse()
        {
            using (BaseDbContext db = CreateDbContext())
            {
                return AllStatic.InverseRepositoryMap[db.Database.Connection.ConnectionString]["IRobot_QrCodePayTask"];
            }
        }

        /// <summary>
        /// 获取跨库查询所需的表达式
        /// </summary>
        /// <returns></returns>
        protected override DisPageEntity<HeartbeatEntity> GetDisPageEntity(HeartbeatEntityParams paramz)
        {
            DisPageEntity<HeartbeatEntity> disPageEntity = new DisPageEntity<HeartbeatEntity>();
            HeartbeatEntityOrderBy orderBy = paramz.orderByAsc;
            if (orderBy != null)
            {
                disPageEntity.OrderType = true;
                if (orderBy.id)
                {
                    disPageEntity.OrderCol = a => a.Id;
                    disPageEntity.OrderColLazy = a => a.Id;
                    return disPageEntity;
                }
                if (orderBy.lastHeartbeatTime)
                {
                    disPageEntity.OrderCol = a => a.LastHeartbeatTime;
                    disPageEntity.OrderColLazy = a => a.LastHeartbeatTime;
                    return disPageEntity;
                }
                if (orderBy.robotId)
                {
                    disPageEntity.OrderCol = a => a.RobotId;
                    disPageEntity.OrderColLazy = a => a.RobotId;
                    return disPageEntity;
                }
            }
            disPageEntity.OrderType = false;
            orderBy = paramz.orderByDesc;
            if (orderBy != null)
            {

                if (orderBy.id)
                {
                    disPageEntity.OrderCol = a => a.Id;
                    disPageEntity.OrderColLazy = a => a.Id;
                    return disPageEntity;
                }
                else
                if (orderBy.lastHeartbeatTime)
                {
                    disPageEntity.OrderCol = a => a.LastHeartbeatTime;
                    disPageEntity.OrderColLazy = a => a.LastHeartbeatTime;
                    return disPageEntity;
                }
                else
                if (orderBy.robotId)
                {
                    disPageEntity.OrderCol = a => a.RobotId;
                    disPageEntity.OrderColLazy = a => a.RobotId;
                    return disPageEntity;
                }
                else
                {
                    disPageEntity.OrderCol = a => a.Id;
                    disPageEntity.OrderColLazy = a => a.Id;
                    return disPageEntity;
                }
            }
            else
            {
                disPageEntity.OrderCol = a => a.Id;
                disPageEntity.OrderColLazy = a => a.Id;
                return disPageEntity;
            }
        }
    }
}