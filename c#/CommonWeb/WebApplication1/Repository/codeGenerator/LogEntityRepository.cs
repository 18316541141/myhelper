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
using CommonHelper.staticVar;
using CommonHelper.EFRepository;
using System.Linq.Expressions;
namespace CommonWeb.Repository
{
    //启用分布式事务
    //[Intercept(typeof(DistributeRepository))]
    public abstract partial class LogEntityRepository : BaseRepository<LogEntity, LogEntityParams, LogEntitySetNullParams>
    {
        /// <summary>
        /// 通用的设置查询参数方法，只有在参数不为null的情况下才会设置，
        /// 满足项目中常用的模糊查询，分页查询等功能
        /// </summary>
        /// <param name="query">待设置参数的query对象</param>
        /// <param name="eqArgs">装载查询参数的实体类</param>
        /// <param name="neqArgs">装载不等于查询参数的实体类</param>
        /// <returns>返回设置好查询参数的query对象</returns>
        protected override IQueryable<LogEntity> Query(IQueryable<LogEntity> query, LogEntityParams eqArgs, LogEntityParams neqArgs = null)
        {
            if (eqArgs != null)
            {

                if (eqArgs.Id != null)
                {
                    query = query.Where(a => a.Id == eqArgs.Id);
                }

                if (eqArgs.CreateDate != null)
                {
                    query = query.Where(a => a.CreateDate == eqArgs.CreateDate);
                }

                if (eqArgs.CreateDateStart != null)
                {
                    query = query.Where(a => a.CreateDate >= eqArgs.CreateDateStart);
                }
                if (eqArgs.CreateDateEnd != null)
                {
                    query = query.Where(a => a.CreateDate <= eqArgs.CreateDateEnd);
                }
                if (eqArgs.Level != null)
                {
                    query = query.Where(a => a.Level == eqArgs.Level);
                }

                if (!string.IsNullOrEmpty(eqArgs.LevelLike))
                {
                    query = query.Where(a => a.Level.Contains(eqArgs.LevelLike));
                }
                if (eqArgs.ThreadNo != null)
                {
                    query = query.Where(a => a.ThreadNo == eqArgs.ThreadNo);
                }

                if (!string.IsNullOrEmpty(eqArgs.ThreadNoLike))
                {
                    query = query.Where(a => a.ThreadNo.Contains(eqArgs.ThreadNoLike));
                }
                if (eqArgs.Message != null)
                {
                    query = query.Where(a => a.Message == eqArgs.Message);
                }

                if (!string.IsNullOrEmpty(eqArgs.MessageLike))
                {
                    query = query.Where(a => a.Message.Contains(eqArgs.MessageLike));
                }
                if (eqArgs.ProjectName != null)
                {
                    query = query.Where(a => a.ProjectName == eqArgs.ProjectName);
                }

                if (!string.IsNullOrEmpty(eqArgs.ProjectNameLike))
                {
                    query = query.Where(a => a.ProjectName.Contains(eqArgs.ProjectNameLike));
                }
                if (eqArgs.TypeName != null)
                {
                    query = query.Where(a => a.TypeName == eqArgs.TypeName);
                }

                if (!string.IsNullOrEmpty(eqArgs.TypeNameLike))
                {
                    query = query.Where(a => a.TypeName.Contains(eqArgs.TypeNameLike));
                }
                if (eqArgs.FuncName != null)
                {
                    query = query.Where(a => a.FuncName == eqArgs.FuncName);
                }

                if (!string.IsNullOrEmpty(eqArgs.FuncNameLike))
                {
                    query = query.Where(a => a.FuncName.Contains(eqArgs.FuncNameLike));
                }
                if (eqArgs.Exception != null)
                {
                    query = query.Where(a => a.Exception == eqArgs.Exception);
                }

                if (!string.IsNullOrEmpty(eqArgs.ExceptionLike))
                {
                    query = query.Where(a => a.Exception.Contains(eqArgs.ExceptionLike));
                }
                query = OrderBy(query, eqArgs);
            }
            if (neqArgs != null)
            {

                if (neqArgs.Id != null)
                {
                    query = query.Where(a => a.Id != neqArgs.Id);
                }

                if (neqArgs.CreateDate != null)
                {
                    query = query.Where(a => a.CreateDate != neqArgs.CreateDate);
                }

                if (neqArgs.Level != null)
                {
                    query = query.Where(a => a.Level != neqArgs.Level);
                }

                if (!string.IsNullOrEmpty(neqArgs.LevelLike))
                {
                    query = query.Where(a => !a.Level.Contains(neqArgs.LevelLike));
                }
                if (neqArgs.ThreadNo != null)
                {
                    query = query.Where(a => a.ThreadNo != neqArgs.ThreadNo);
                }

                if (!string.IsNullOrEmpty(neqArgs.ThreadNoLike))
                {
                    query = query.Where(a => !a.ThreadNo.Contains(neqArgs.ThreadNoLike));
                }
                if (neqArgs.Message != null)
                {
                    query = query.Where(a => a.Message != neqArgs.Message);
                }

                if (!string.IsNullOrEmpty(neqArgs.MessageLike))
                {
                    query = query.Where(a => !a.Message.Contains(neqArgs.MessageLike));
                }
                if (neqArgs.ProjectName != null)
                {
                    query = query.Where(a => a.ProjectName != neqArgs.ProjectName);
                }

                if (!string.IsNullOrEmpty(neqArgs.ProjectNameLike))
                {
                    query = query.Where(a => !a.ProjectName.Contains(neqArgs.ProjectNameLike));
                }
                if (neqArgs.TypeName != null)
                {
                    query = query.Where(a => a.TypeName != neqArgs.TypeName);
                }

                if (!string.IsNullOrEmpty(neqArgs.TypeNameLike))
                {
                    query = query.Where(a => !a.TypeName.Contains(neqArgs.TypeNameLike));
                }
                if (neqArgs.FuncName != null)
                {
                    query = query.Where(a => a.FuncName != neqArgs.FuncName);
                }

                if (!string.IsNullOrEmpty(neqArgs.FuncNameLike))
                {
                    query = query.Where(a => !a.FuncName.Contains(neqArgs.FuncNameLike));
                }
                if (neqArgs.Exception != null)
                {
                    query = query.Where(a => a.Exception != neqArgs.Exception);
                }

                if (!string.IsNullOrEmpty(neqArgs.ExceptionLike))
                {
                    query = query.Where(a => !a.Exception.Contains(neqArgs.ExceptionLike));
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
        IQueryable<LogEntity> OrderBy(IQueryable<LogEntity> query, LogEntityParams eqArgs)
        {
            LogEntityOrderBy orderBy = eqArgs.orderByAsc;
            if (orderBy != null)
            {

                if (orderBy.Id) { return query = query.OrderBy(a => a.Id); }
                else
                if (orderBy.CreateDate) { return query = query.OrderBy(a => a.CreateDate); }
                else
                if (orderBy.Level) { return query = query.OrderBy(a => a.Level); }
                else
                if (orderBy.ThreadNo) { return query = query.OrderBy(a => a.ThreadNo); }
                else
                if (orderBy.Message) { return query = query.OrderBy(a => a.Message); }
                else
                if (orderBy.ProjectName) { return query = query.OrderBy(a => a.ProjectName); }
                else
                if (orderBy.TypeName) { return query = query.OrderBy(a => a.TypeName); }
                else
                if (orderBy.FuncName) { return query = query.OrderBy(a => a.FuncName); }
                else
                if (orderBy.Exception) { return query = query.OrderBy(a => a.Exception); }
            }
            orderBy = eqArgs.orderByDesc;
            if (orderBy != null)
            {

                if (orderBy.Id) { return query = query.OrderByDescending(a => a.Id); }
                else
                if (orderBy.CreateDate) { return query = query.OrderByDescending(a => a.CreateDate); }
                else
                if (orderBy.Level) { return query = query.OrderByDescending(a => a.Level); }
                else
                if (orderBy.ThreadNo) { return query = query.OrderByDescending(a => a.ThreadNo); }
                else
                if (orderBy.Message) { return query = query.OrderByDescending(a => a.Message); }
                else
                if (orderBy.ProjectName) { return query = query.OrderByDescending(a => a.ProjectName); }
                else
                if (orderBy.TypeName) { return query = query.OrderByDescending(a => a.TypeName); }
                else
                if (orderBy.FuncName) { return query = query.OrderByDescending(a => a.FuncName); }
                else
                if (orderBy.Exception) { return query = query.OrderByDescending(a => a.Exception); }
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
        protected override void UpdateChange(BaseDbContext dbContext, LogEntity entity)
        {
            LogEntity updateBefore = new LogEntity { Id = entity.Id };
            dbContext.Set<LogEntity>().Attach(updateBefore);
            if (entity.CreateDate != null)
            {
                updateBefore.CreateDate = entity.CreateDate;
            }
            if (entity.Level != null)
            {
                updateBefore.Level = entity.Level;
            }
            if (entity.ThreadNo != null)
            {
                updateBefore.ThreadNo = entity.ThreadNo;
            }
            if (entity.Message != null)
            {
                updateBefore.Message = entity.Message;
            }
            if (entity.ProjectName != null)
            {
                updateBefore.ProjectName = entity.ProjectName;
            }
            if (entity.TypeName != null)
            {
                updateBefore.TypeName = entity.TypeName;
            }
            if (entity.FuncName != null)
            {
                updateBefore.FuncName = entity.FuncName;
            }
            if (entity.Exception != null)
            {
                updateBefore.Exception = entity.Exception;
            }
        }

        /// <summary>
        /// 把指定字段值设置为null的部分共用代码，这里抽取出来简化结构
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="param"></param>
        protected override void SetNull(BaseDbContext dbContext, LogEntitySetNullParams param)
        {
            LogEntity updateBefore = FindEntity(param.Id);
            dbContext.Set<LogEntity>().Attach(updateBefore);
            if (param.CreateDate)
            {
                updateBefore.CreateDate = null;
            }
            if (param.Level)
            {
                updateBefore.Level = null;
            }
            if (param.ThreadNo)
            {
                updateBefore.ThreadNo = null;
            }
            if (param.Message)
            {
                updateBefore.Message = null;
            }
            if (param.ProjectName)
            {
                updateBefore.ProjectName = null;
            }
            if (param.TypeName)
            {
                updateBefore.TypeName = null;
            }
            if (param.FuncName)
            {
                updateBefore.FuncName = null;
            }
            if (param.Exception)
            {
                updateBefore.Exception = null;
            }
        }

        /// <summary>
        /// 检查分布式事务是否已完成
        /// </summary>
        /// <param name="primaryKeyVal">主键的值</param>
        public void CheckTransactionFinish(long primaryKeyVal)
        {
            CheckTransactionFinish(primaryKeyVal, "Log_Entity");
        }

        public override InverseRepository<LogEntity> CurrentInverse()
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
        protected override DisPageEntity<LogEntity> GetDisPageEntity(LogEntityParams paramz)
        {
            DisPageEntity<LogEntity> disPageEntity = new DisPageEntity<LogEntity>();
            LogEntityOrderBy orderBy = paramz.orderByAsc;
            if (orderBy != null)
            {
                disPageEntity.OrderType = true;
                if (orderBy.Id)
                {
                    disPageEntity.OrderCol = a => a.Id;
                    disPageEntity.OrderColLazy = a => a.Id;
                    return disPageEntity;
                }
                if (orderBy.CreateDate)
                {
                    disPageEntity.OrderCol = a => a.CreateDate;
                    disPageEntity.OrderColLazy = a => a.CreateDate;
                    return disPageEntity;
                }
                if (orderBy.Level)
                {
                    disPageEntity.OrderCol = a => a.Level;
                    disPageEntity.OrderColLazy = a => a.Level;
                    return disPageEntity;
                }
                if (orderBy.ThreadNo)
                {
                    disPageEntity.OrderCol = a => a.ThreadNo;
                    disPageEntity.OrderColLazy = a => a.ThreadNo;
                    return disPageEntity;
                }
                if (orderBy.Message)
                {
                    disPageEntity.OrderCol = a => a.Message;
                    disPageEntity.OrderColLazy = a => a.Message;
                    return disPageEntity;
                }
                if (orderBy.ProjectName)
                {
                    disPageEntity.OrderCol = a => a.ProjectName;
                    disPageEntity.OrderColLazy = a => a.ProjectName;
                    return disPageEntity;
                }
                if (orderBy.TypeName)
                {
                    disPageEntity.OrderCol = a => a.TypeName;
                    disPageEntity.OrderColLazy = a => a.TypeName;
                    return disPageEntity;
                }
                if (orderBy.FuncName)
                {
                    disPageEntity.OrderCol = a => a.FuncName;
                    disPageEntity.OrderColLazy = a => a.FuncName;
                    return disPageEntity;
                }
                if (orderBy.Exception)
                {
                    disPageEntity.OrderCol = a => a.Exception;
                    disPageEntity.OrderColLazy = a => a.Exception;
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
                if (orderBy.CreateDate)
                {
                    disPageEntity.OrderCol = a => a.CreateDate;
                    disPageEntity.OrderColLazy = a => a.CreateDate;
                    return disPageEntity;
                }
                else
                if (orderBy.Level)
                {
                    disPageEntity.OrderCol = a => a.Level;
                    disPageEntity.OrderColLazy = a => a.Level;
                    return disPageEntity;
                }
                else
                if (orderBy.ThreadNo)
                {
                    disPageEntity.OrderCol = a => a.ThreadNo;
                    disPageEntity.OrderColLazy = a => a.ThreadNo;
                    return disPageEntity;
                }
                else
                if (orderBy.Message)
                {
                    disPageEntity.OrderCol = a => a.Message;
                    disPageEntity.OrderColLazy = a => a.Message;
                    return disPageEntity;
                }
                else
                if (orderBy.ProjectName)
                {
                    disPageEntity.OrderCol = a => a.ProjectName;
                    disPageEntity.OrderColLazy = a => a.ProjectName;
                    return disPageEntity;
                }
                else
                if (orderBy.TypeName)
                {
                    disPageEntity.OrderCol = a => a.TypeName;
                    disPageEntity.OrderColLazy = a => a.TypeName;
                    return disPageEntity;
                }
                else
                if (orderBy.FuncName)
                {
                    disPageEntity.OrderCol = a => a.FuncName;
                    disPageEntity.OrderColLazy = a => a.FuncName;
                    return disPageEntity;
                }
                else
                if (orderBy.Exception)
                {
                    disPageEntity.OrderCol = a => a.Exception;
                    disPageEntity.OrderColLazy = a => a.Exception;
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