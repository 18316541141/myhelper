﻿using System;
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
using CommonHelper.EFMap;

namespace WebApplication1.Repository
{
    //启用分布式事务
    //[Intercept(typeof(DistributeRepository))]
    public partial class HeartbeatEntityRepository : BaseRepository<HeartbeatEntity, HeartbeatEntityParams, HeartbeatEntitySetNullParams>
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

                if (eqArgs.Id != null)
                {
                    query = query.Where(a => a.Id == eqArgs.Id);
                }

                if (eqArgs.LastHeartbeatTime != null)
                {
                    query = query.Where(a => a.LastHeartbeatTime == eqArgs.LastHeartbeatTime);
                }

                if (eqArgs.LastHeartbeatTimeStart != null)
                {
                    query = query.Where(a => a.LastHeartbeatTime >= eqArgs.LastHeartbeatTimeStart);
                }
                if (eqArgs.LastHeartbeatTimeEnd != null)
                {
                    query = query.Where(a => a.LastHeartbeatTime <= eqArgs.LastHeartbeatTimeEnd);
                }
                if (eqArgs.RobotId != null)
                {
                    query = query.Where(a => a.RobotId == eqArgs.RobotId);
                }

                if (!string.IsNullOrEmpty(eqArgs.RobotIdLike))
                {
                    query = query.Where(a => a.RobotId.Contains(eqArgs.RobotIdLike));
                }
                query = OrderBy(query, eqArgs);
            }
            if (neqArgs != null)
            {

                if (neqArgs.Id != null)
                {
                    query = query.Where(a => a.Id != neqArgs.Id);
                }

                if (neqArgs.LastHeartbeatTime != null)
                {
                    query = query.Where(a => a.LastHeartbeatTime != neqArgs.LastHeartbeatTime);
                }

                if (neqArgs.RobotId != null)
                {
                    query = query.Where(a => a.RobotId != neqArgs.RobotId);
                }

                if (!string.IsNullOrEmpty(neqArgs.RobotIdLike))
                {
                    query = query.Where(a => !a.RobotId.Contains(neqArgs.RobotIdLike));
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

                if (orderBy.Id) { return query = query.OrderBy(a => a.Id); }
                else
                if (orderBy.LastHeartbeatTime) { return query = query.OrderBy(a => a.LastHeartbeatTime); }
                else
                if (orderBy.RobotId) { return query = query.OrderBy(a => a.RobotId); }
            }
            orderBy = eqArgs.orderByDesc;
            if (orderBy != null)
            {

                if (orderBy.Id) { return query = query.OrderByDescending(a => a.Id); }
                else
                if (orderBy.LastHeartbeatTime) { return query = query.OrderByDescending(a => a.LastHeartbeatTime); }
                else
                if (orderBy.RobotId) { return query = query.OrderByDescending(a => a.RobotId); }
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
            HeartbeatEntity updateBefore = FindEntity(param.Id);
            dbContext.Set<HeartbeatEntity>().Attach(updateBefore);
            if (param.LastHeartbeatTime)
            {
                updateBefore.LastHeartbeatTime = null;
            }
            if (param.RobotId)
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

        public override BaseDbContext CreateDbContext()
        {
            return new MyDbContext2();
        }

        public override InverseRepository<HeartbeatEntity> CurrentInverse()
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
        protected override DisPageEntity<HeartbeatEntity> GetDisPageEntity(HeartbeatEntityParams paramz)
        {
            DisPageEntity<HeartbeatEntity> disPageEntity = new DisPageEntity<HeartbeatEntity>();
            HeartbeatEntityOrderBy orderBy = paramz.orderByAsc;
            if (orderBy != null)
            {
                disPageEntity.OrderType = true;
                if (orderBy.Id)
                {
                    disPageEntity.OrderCol = a => a.Id;
                    disPageEntity.OrderColLazy = a => a.Id;
                    return disPageEntity;
                }
                if (orderBy.LastHeartbeatTime)
                {
                    disPageEntity.OrderCol = a => a.LastHeartbeatTime;
                    disPageEntity.OrderColLazy = a => a.LastHeartbeatTime;
                    return disPageEntity;
                }
                if (orderBy.RobotId)
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

                if (orderBy.Id)
                {
                    disPageEntity.OrderCol = a => a.Id;
                    disPageEntity.OrderColLazy = a => a.Id;
                    return disPageEntity;
                }
                else
                if (orderBy.LastHeartbeatTime)
                {
                    disPageEntity.OrderCol = a => a.LastHeartbeatTime;
                    disPageEntity.OrderColLazy = a => a.LastHeartbeatTime;
                    return disPageEntity;
                }
                else
                if (orderBy.RobotId)
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

        /// <summary>
        /// 跨库分页时，基准值查询
        /// </summary>
        /// <param name="paramz">查询参数，判断排序的重要依据</param>
        /// <param name="baseVal">基准值，缩小查询范围</param>
        /// <param name="queryType">查询类型，0：查询基准值跳过的数据量，1：查询比基准值大，并当中含有合适的基准值</param>
        /// <returns></returns>
        protected override Expression<Func<HeartbeatEntity, bool>> BaseValQuery(HeartbeatEntityParams paramz, IComparable baseVal, int queryType)
        {
            HeartbeatEntityOrderBy orderBy = paramz.orderByAsc;
            if (orderBy != null)
            {
                if (orderBy.Id)
                {
                    if (queryType == 0)
                    {
                        return a => baseVal.CompareTo(a.Id) > 0;
                    }
                    else if (queryType == 1)
                    {
                        return a => baseVal.CompareTo(a.Id) < 0;
                    }
                }
                if (orderBy.LastHeartbeatTime)
                {
                    if (queryType == 0)
                    {
                        return a => baseVal.CompareTo(a.LastHeartbeatTime) > 0;
                    }
                    else if (queryType == 1)
                    {
                        return a => baseVal.CompareTo(a.LastHeartbeatTime) < 0;
                    }
                }
                if (orderBy.RobotId)
                {
                    if (queryType == 0)
                    {
                        return a => baseVal.CompareTo(a.RobotId) > 0;
                    }
                    else if (queryType == 1)
                    {
                        return a => baseVal.CompareTo(a.RobotId) < 0;
                    }
                }
            }
            orderBy = paramz.orderByDesc;
            if (orderBy != null)
            {
                if (orderBy.Id)
                {
                    if (queryType == 0)
                    {
                        return a => baseVal.CompareTo(a.Id) < 0;
                    }
                    else if (queryType == 1)
                    {
                        return a => baseVal.CompareTo(a.Id) > 0;
                    }
                }
                else if (orderBy.LastHeartbeatTime)
                {
                    if (queryType == 0)
                    {
                        return a => baseVal.CompareTo(a.LastHeartbeatTime) < 0;
                    }
                    else if (queryType == 1)
                    {
                        return a => baseVal.CompareTo(a.LastHeartbeatTime) > 0;
                    }
                }
                else if (orderBy.RobotId)
                {
                    if (queryType == 0)
                    {
                        return a => baseVal.CompareTo(a.RobotId) < 0;
                    }
                    else if (queryType == 1)
                    {
                        return a => baseVal.CompareTo(a.RobotId) > 0;
                    }
                }
                else
                {
                    if (queryType == 0)
                    {
                        return a => baseVal.CompareTo(a.Id) < 0;
                    }
                    else if (queryType == 1)
                    {
                        return a => baseVal.CompareTo(a.Id) > 0;
                    }
                }
            }
            else
            {
                if (queryType == 0)
                {
                    return a => baseVal.CompareTo(a.Id) < 0;
                }
                else if (queryType == 1)
                {
                    return a => baseVal.CompareTo(a.Id) > 0;
                }
            }
            return null;
        }
    }
}