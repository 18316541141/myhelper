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
using CommonHelper.Entity;
using CommonHelper.Params;
using CommonHelper.OrderBy;

namespace CommonWeb.Repository
{
    //启用分布式事务
    //[Intercept(typeof(DistributeRepository))]
    public abstract partial class HeartbeatEntityRepository : BaseRepository<HeartbeatEntity, HeartbeatEntityParams, HeartbeatEntitySetNullParams>
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
                if (eqArgs.RobotIp != null)
                {
                    query = query.Where(a => a.RobotIp == eqArgs.RobotIp);
                }

                if (!string.IsNullOrEmpty(eqArgs.RobotIpLike))
                {
                    query = query.Where(a => a.RobotIp.Contains(eqArgs.RobotIpLike));
                }
                if (eqArgs.Remark != null)
                {
                    query = query.Where(a => a.Remark == eqArgs.Remark);
                }

                if (!string.IsNullOrEmpty(eqArgs.RemarkLike))
                {
                    query = query.Where(a => a.Remark.Contains(eqArgs.RemarkLike));
                }
                if (eqArgs.ExtendField != null)
                {
                    query = query.Where(a => a.ExtendField == eqArgs.ExtendField);
                }

                if (!string.IsNullOrEmpty(eqArgs.ExtendFieldLike))
                {
                    query = query.Where(a => a.ExtendField.Contains(eqArgs.ExtendFieldLike));
                }

                if (eqArgs.MonitorServer != null)
                {
                    query = query.Where(a => a.MonitorServer == eqArgs.MonitorServer);
                }

                if (!string.IsNullOrEmpty(eqArgs.MonitorServerLike))
                {
                    query = query.Where(a => a.MonitorServer.Contains(eqArgs.MonitorServerLike));
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

                if (neqArgs.RobotIp != null)
                {
                    query = query.Where(a => a.RobotIp != neqArgs.RobotIp);
                }

                if (!string.IsNullOrEmpty(neqArgs.RobotIpLike))
                {
                    query = query.Where(a => !a.RobotIp.Contains(neqArgs.RobotIpLike));
                }
                if (neqArgs.Remark != null)
                {
                    query = query.Where(a => a.Remark != neqArgs.Remark);
                }

                if (!string.IsNullOrEmpty(neqArgs.RemarkLike))
                {
                    query = query.Where(a => !a.Remark.Contains(neqArgs.RemarkLike));
                }
                if (neqArgs.ExtendField != null)
                {
                    query = query.Where(a => a.ExtendField != neqArgs.ExtendField);
                }

                if (!string.IsNullOrEmpty(neqArgs.ExtendFieldLike))
                {
                    query = query.Where(a => !a.ExtendField.Contains(neqArgs.ExtendFieldLike));
                }

                if (neqArgs.MonitorServer != null)
                {
                    query = query.Where(a => a.MonitorServer != neqArgs.MonitorServer);
                }

                if (!string.IsNullOrEmpty(neqArgs.MonitorServerLike))
                {
                    query = query.Where(a => !a.MonitorServer.Contains(neqArgs.MonitorServerLike));
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
                if (orderBy.RobotIp) { return query = query.OrderBy(a => a.RobotIp); }
                else
                if (orderBy.Remark) { return query = query.OrderBy(a => a.Remark); }
                else
                if (orderBy.ExtendField) { return query = query.OrderBy(a => a.ExtendField); }
                else
                if (orderBy.MonitorServer) { return query = query.OrderBy(a => a.MonitorServer); }
            }
            orderBy = eqArgs.orderByDesc;
            if (orderBy != null)
            {

                if (orderBy.Id) { return query = query.OrderByDescending(a => a.Id); }
                else
                if (orderBy.LastHeartbeatTime) { return query = query.OrderByDescending(a => a.LastHeartbeatTime); }
                else
                if (orderBy.RobotIp) { return query = query.OrderByDescending(a => a.RobotIp); }
                else
                if (orderBy.Remark) { return query = query.OrderByDescending(a => a.Remark); }
                else
                if (orderBy.ExtendField) { return query = query.OrderByDescending(a => a.ExtendField); }
                else
                if (orderBy.MonitorServer) { return query = query.OrderByDescending(a => a.MonitorServer); }
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
            if (entity.RobotIp != null)
            {
                updateBefore.RobotIp = entity.RobotIp;
            }
            if (entity.Remark != null)
            {
                updateBefore.Remark = entity.Remark;
            }
            if (entity.ExtendField != null)
            {
                updateBefore.ExtendField = entity.ExtendField;
            }
            if (entity.MonitorServer != null)
            {
                updateBefore.MonitorServer = entity.MonitorServer;
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
            if (param.RobotIp)
            {
                updateBefore.RobotIp = null;
            }
            if (param.Remark)
            {
                updateBefore.Remark = null;
            }
            if (param.ExtendField)
            {
                updateBefore.ExtendField = null;
            }
            if (param.MonitorServer)
            {
                updateBefore.MonitorServer = null;
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
            disPageEntity.InCondition = (entities) =>
            {
                List<long?> keys = entities.Select(a => a.Id).ToList();
                return a => keys.Contains(a.Id);
            };
            HeartbeatEntityOrderBy orderBy = paramz.orderByAsc;
            if (orderBy != null)
            {
                disPageEntity.OrderType = true;
                if (orderBy.Id)
                {
                    disPageEntity.OrderCol = a => a.Id;
                    disPageEntity.OrderColLazy = a => a.Id;
                    disPageEntity.OrderColAndKeyLazy = a => new HeartbeatEntity { Id = a.Id };
                    disPageEntity.GreatThan = (val) => a => val.CompareTo(a.Id) < 0;
                    disPageEntity.GreatEqThan = (val) => a => val.CompareTo(a.Id) <= 0;
                    disPageEntity.LessThan = (val) => a => val.CompareTo(a.Id) > 0;
                    disPageEntity.LessEqThan = (val) => a => val.CompareTo(a.Id) >= 0;
                    return disPageEntity;
                }
                if (orderBy.LastHeartbeatTime)
                {
                    disPageEntity.OrderCol = a => a.LastHeartbeatTime;
                    disPageEntity.OrderColLazy = a => a.LastHeartbeatTime;
                    disPageEntity.OrderColAndKeyLazy = a => new HeartbeatEntity { Id = a.Id, LastHeartbeatTime = a.LastHeartbeatTime };
                    disPageEntity.GreatThan = (val) => a => val.CompareTo(a.LastHeartbeatTime) < 0;
                    disPageEntity.GreatEqThan = (val) => a => val.CompareTo(a.LastHeartbeatTime) <= 0;
                    disPageEntity.LessThan = (val) => a => val.CompareTo(a.LastHeartbeatTime) > 0;
                    disPageEntity.LessEqThan = (val) => a => val.CompareTo(a.LastHeartbeatTime) >= 0;
                    return disPageEntity;
                }
                if (orderBy.RobotIp)
                {
                    disPageEntity.OrderCol = a => a.RobotIp;
                    disPageEntity.OrderColLazy = a => a.RobotIp;
                    disPageEntity.OrderColAndKeyLazy = a => new HeartbeatEntity { Id = a.Id, RobotIp = a.RobotIp };
                    disPageEntity.GreatThan = (val) => a => val.CompareTo(a.RobotIp) < 0;
                    disPageEntity.GreatEqThan = (val) => a => val.CompareTo(a.RobotIp) <= 0;
                    disPageEntity.LessThan = (val) => a => val.CompareTo(a.RobotIp) > 0;
                    disPageEntity.LessEqThan = (val) => a => val.CompareTo(a.RobotIp) >= 0;
                    return disPageEntity;
                }
                if (orderBy.Remark)
                {
                    disPageEntity.OrderCol = a => a.Remark;
                    disPageEntity.OrderColLazy = a => a.Remark;
                    disPageEntity.OrderColAndKeyLazy = a => new HeartbeatEntity { Id = a.Id, Remark = a.Remark };
                    disPageEntity.GreatThan = (val) => a => val.CompareTo(a.Remark) < 0;
                    disPageEntity.GreatEqThan = (val) => a => val.CompareTo(a.Remark) <= 0;
                    disPageEntity.LessThan = (val) => a => val.CompareTo(a.Remark) > 0;
                    disPageEntity.LessEqThan = (val) => a => val.CompareTo(a.Remark) >= 0;
                    return disPageEntity;
                }
                if (orderBy.ExtendField)
                {
                    disPageEntity.OrderCol = a => a.ExtendField;
                    disPageEntity.OrderColLazy = a => a.ExtendField;
                    disPageEntity.OrderColAndKeyLazy = a => new HeartbeatEntity { Id = a.Id, ExtendField = a.ExtendField };
                    disPageEntity.GreatThan = (val) => a => val.CompareTo(a.ExtendField) < 0;
                    disPageEntity.GreatEqThan = (val) => a => val.CompareTo(a.ExtendField) <= 0;
                    disPageEntity.LessThan = (val) => a => val.CompareTo(a.ExtendField) > 0;
                    disPageEntity.LessEqThan = (val) => a => val.CompareTo(a.ExtendField) >= 0;
                    return disPageEntity;
                }
                if (orderBy.MonitorServer)
                {
                    disPageEntity.OrderCol = a => a.MonitorServer;
                    disPageEntity.OrderColLazy = a => a.MonitorServer;
                    disPageEntity.OrderColAndKeyLazy = a => new HeartbeatEntity { Id = a.Id, MonitorServer = a.MonitorServer };
                    disPageEntity.GreatThan = (val) => a => val.CompareTo(a.MonitorServer) < 0;
                    disPageEntity.GreatEqThan = (val) => a => val.CompareTo(a.MonitorServer) <= 0;
                    disPageEntity.LessThan = (val) => a => val.CompareTo(a.MonitorServer) > 0;
                    disPageEntity.LessEqThan = (val) => a => val.CompareTo(a.MonitorServer) >= 0;
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
                    disPageEntity.OrderColAndKeyLazy = a => new HeartbeatEntity { Id = a.Id };
                    disPageEntity.GreatThan = (val) => a => val.CompareTo(a.Id) < 0;
                    disPageEntity.GreatEqThan = (val) => a => val.CompareTo(a.Id) <= 0;
                    disPageEntity.LessThan = (val) => a => val.CompareTo(a.Id) > 0;
                    disPageEntity.LessEqThan = (val) => a => val.CompareTo(a.Id) >= 0;
                    return disPageEntity;
                }
                else
                if (orderBy.LastHeartbeatTime)
                {
                    disPageEntity.OrderCol = a => a.LastHeartbeatTime;
                    disPageEntity.OrderColLazy = a => a.LastHeartbeatTime;
                    disPageEntity.OrderColAndKeyLazy = a => new HeartbeatEntity { Id = a.Id, LastHeartbeatTime = a.LastHeartbeatTime };
                    disPageEntity.GreatThan = (val) => a => val.CompareTo(a.LastHeartbeatTime) < 0;
                    disPageEntity.GreatEqThan = (val) => a => val.CompareTo(a.LastHeartbeatTime) <= 0;
                    disPageEntity.LessThan = (val) => a => val.CompareTo(a.LastHeartbeatTime) > 0;
                    disPageEntity.LessEqThan = (val) => a => val.CompareTo(a.LastHeartbeatTime) >= 0;
                    return disPageEntity;
                }
                else
                if (orderBy.RobotIp)
                {
                    disPageEntity.OrderCol = a => a.RobotIp;
                    disPageEntity.OrderColLazy = a => a.RobotIp;
                    disPageEntity.OrderColAndKeyLazy = a => new HeartbeatEntity { Id = a.Id, RobotIp = a.RobotIp };
                    disPageEntity.GreatThan = (val) => a => val.CompareTo(a.RobotIp) < 0;
                    disPageEntity.GreatEqThan = (val) => a => val.CompareTo(a.RobotIp) <= 0;
                    disPageEntity.LessThan = (val) => a => val.CompareTo(a.RobotIp) > 0;
                    disPageEntity.LessEqThan = (val) => a => val.CompareTo(a.RobotIp) >= 0;
                    return disPageEntity;
                }
                else
                if (orderBy.Remark)
                {
                    disPageEntity.OrderCol = a => a.Remark;
                    disPageEntity.OrderColLazy = a => a.Remark;
                    disPageEntity.OrderColAndKeyLazy = a => new HeartbeatEntity { Id = a.Id, Remark = a.Remark };
                    disPageEntity.GreatThan = (val) => a => val.CompareTo(a.Remark) < 0;
                    disPageEntity.GreatEqThan = (val) => a => val.CompareTo(a.Remark) <= 0;
                    disPageEntity.LessThan = (val) => a => val.CompareTo(a.Remark) > 0;
                    disPageEntity.LessEqThan = (val) => a => val.CompareTo(a.Remark) >= 0;
                    return disPageEntity;
                }
                else
                if (orderBy.ExtendField)
                {
                    disPageEntity.OrderCol = a => a.ExtendField;
                    disPageEntity.OrderColLazy = a => a.ExtendField;
                    disPageEntity.OrderColAndKeyLazy = a => new HeartbeatEntity { Id = a.Id, ExtendField = a.ExtendField };
                    disPageEntity.GreatThan = (val) => a => val.CompareTo(a.ExtendField) < 0;
                    disPageEntity.GreatEqThan = (val) => a => val.CompareTo(a.ExtendField) <= 0;
                    disPageEntity.LessThan = (val) => a => val.CompareTo(a.ExtendField) > 0;
                    disPageEntity.LessEqThan = (val) => a => val.CompareTo(a.ExtendField) >= 0;
                    return disPageEntity;
                }
                else
                if (orderBy.MonitorServer)
                {
                    disPageEntity.OrderCol = a => a.MonitorServer;
                    disPageEntity.OrderColLazy = a => a.MonitorServer;
                    disPageEntity.OrderColAndKeyLazy = a => new HeartbeatEntity { Id = a.Id, MonitorServer = a.MonitorServer };
                    disPageEntity.GreatThan = (val) => a => val.CompareTo(a.MonitorServer) < 0;
                    disPageEntity.GreatEqThan = (val) => a => val.CompareTo(a.MonitorServer) <= 0;
                    disPageEntity.LessThan = (val) => a => val.CompareTo(a.MonitorServer) > 0;
                    disPageEntity.LessEqThan = (val) => a => val.CompareTo(a.MonitorServer) >= 0;
                    return disPageEntity;
                }
                else
                {
                    disPageEntity.OrderCol = a => a.Id;
                    disPageEntity.OrderColLazy = a => a.Id;
                    disPageEntity.OrderColAndKeyLazy = a => new HeartbeatEntity { Id = a.Id };
                    disPageEntity.GreatThan = (val) => a => val.CompareTo(a.Id) < 0;
                    disPageEntity.GreatEqThan = (val) => a => val.CompareTo(a.Id) <= 0;
                    disPageEntity.LessThan = (val) => a => val.CompareTo(a.Id) > 0;
                    disPageEntity.LessEqThan = (val) => a => val.CompareTo(a.Id) >= 0;
                    return disPageEntity;
                }
            }
            else
            {
                disPageEntity.OrderCol = a => a.Id;
                disPageEntity.OrderColLazy = a => a.Id;
                disPageEntity.OrderColAndKeyLazy = a => new HeartbeatEntity { Id = a.Id };
                disPageEntity.GreatThan = (val) => a => val.CompareTo(a.Id) < 0;
                disPageEntity.GreatEqThan = (val) => a => val.CompareTo(a.Id) <= 0;
                disPageEntity.LessThan = (val) => a => val.CompareTo(a.Id) > 0;
                disPageEntity.LessEqThan = (val) => a => val.CompareTo(a.Id) >= 0;
                return disPageEntity;
            }
        }
    }
}