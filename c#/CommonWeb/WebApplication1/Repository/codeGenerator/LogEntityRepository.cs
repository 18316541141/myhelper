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

                if (eqArgs.id != null)
                {
                    query = query.Where(a => a.Id == eqArgs.id);
                }

                if (eqArgs.createDate != null)
                {
                    query = query.Where(a => a.CreateDate == eqArgs.createDate);
                }

                if (eqArgs.createDateStart != null)
                {
                    query = query.Where(a => a.CreateDate >= eqArgs.createDateStart);
                }
                if (eqArgs.createDateEnd != null)
                {
                    query = query.Where(a => a.CreateDate <= eqArgs.createDateEnd);
                }
                if (eqArgs.level != null)
                {
                    query = query.Where(a => a.Level == eqArgs.level);
                }

                if (!string.IsNullOrEmpty(eqArgs.levelLike))
                {
                    query = query.Where(a => a.Level.Contains(eqArgs.levelLike));
                }
                if (eqArgs.threadNo != null)
                {
                    query = query.Where(a => a.ThreadNo == eqArgs.threadNo);
                }

                if (!string.IsNullOrEmpty(eqArgs.threadNoLike))
                {
                    query = query.Where(a => a.ThreadNo.Contains(eqArgs.threadNoLike));
                }
                if (eqArgs.message != null)
                {
                    query = query.Where(a => a.Message == eqArgs.message);
                }

                if (!string.IsNullOrEmpty(eqArgs.messageLike))
                {
                    query = query.Where(a => a.Message.Contains(eqArgs.messageLike));
                }
                if (eqArgs.projectName != null)
                {
                    query = query.Where(a => a.ProjectName == eqArgs.projectName);
                }

                if (!string.IsNullOrEmpty(eqArgs.projectNameLike))
                {
                    query = query.Where(a => a.ProjectName.Contains(eqArgs.projectNameLike));
                }
                if (eqArgs.typeName != null)
                {
                    query = query.Where(a => a.TypeName == eqArgs.typeName);
                }

                if (!string.IsNullOrEmpty(eqArgs.typeNameLike))
                {
                    query = query.Where(a => a.TypeName.Contains(eqArgs.typeNameLike));
                }
                if (eqArgs.funcName != null)
                {
                    query = query.Where(a => a.FuncName == eqArgs.funcName);
                }

                if (!string.IsNullOrEmpty(eqArgs.funcNameLike))
                {
                    query = query.Where(a => a.FuncName.Contains(eqArgs.funcNameLike));
                }
                if (eqArgs.exception != null)
                {
                    query = query.Where(a => a.Exception == eqArgs.exception);
                }

                if (!string.IsNullOrEmpty(eqArgs.exceptionLike))
                {
                    query = query.Where(a => a.Exception.Contains(eqArgs.exceptionLike));
                }

                if (eqArgs.username != null)
                {
                    query = query.Where(a => a.Username == eqArgs.username);
                }
                if (!string.IsNullOrEmpty(eqArgs.usernameLike))
                {
                    query = query.Where(a => a.Username.Contains(eqArgs.usernameLike));
                }
                query = OrderBy(query, eqArgs);
            }
            if (neqArgs != null)
            {

                if (neqArgs.id != null)
                {
                    query = query.Where(a => a.Id != neqArgs.id);
                }

                if (neqArgs.createDate != null)
                {
                    query = query.Where(a => a.CreateDate != neqArgs.createDate);
                }

                if (neqArgs.level != null)
                {
                    query = query.Where(a => a.Level != neqArgs.level);
                }

                if (!string.IsNullOrEmpty(neqArgs.levelLike))
                {
                    query = query.Where(a => !a.Level.Contains(neqArgs.levelLike));
                }
                if (neqArgs.threadNo != null)
                {
                    query = query.Where(a => a.ThreadNo != neqArgs.threadNo);
                }

                if (!string.IsNullOrEmpty(neqArgs.threadNoLike))
                {
                    query = query.Where(a => !a.ThreadNo.Contains(neqArgs.threadNoLike));
                }
                if (neqArgs.message != null)
                {
                    query = query.Where(a => a.Message != neqArgs.message);
                }

                if (!string.IsNullOrEmpty(neqArgs.messageLike))
                {
                    query = query.Where(a => !a.Message.Contains(neqArgs.messageLike));
                }
                if (neqArgs.projectName != null)
                {
                    query = query.Where(a => a.ProjectName != neqArgs.projectName);
                }

                if (!string.IsNullOrEmpty(neqArgs.projectNameLike))
                {
                    query = query.Where(a => !a.ProjectName.Contains(neqArgs.projectNameLike));
                }
                if (neqArgs.typeName != null)
                {
                    query = query.Where(a => a.TypeName != neqArgs.typeName);
                }

                if (!string.IsNullOrEmpty(neqArgs.typeNameLike))
                {
                    query = query.Where(a => !a.TypeName.Contains(neqArgs.typeNameLike));
                }
                if (neqArgs.funcName != null)
                {
                    query = query.Where(a => a.FuncName != neqArgs.funcName);
                }

                if (!string.IsNullOrEmpty(neqArgs.funcNameLike))
                {
                    query = query.Where(a => !a.FuncName.Contains(neqArgs.funcNameLike));
                }
                if (neqArgs.exception != null)
                {
                    query = query.Where(a => a.Exception != neqArgs.exception);
                }
                if (!string.IsNullOrEmpty(neqArgs.exceptionLike))
                {
                    query = query.Where(a => !a.Exception.Contains(neqArgs.exceptionLike));
                }

                if (neqArgs.username != null)
                {
                    query = query.Where(a => a.Username != neqArgs.username);
                }
                if (!string.IsNullOrEmpty(neqArgs.usernameLike))
                {
                    query = query.Where(a => !a.Username.Contains(neqArgs.usernameLike));
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

                if (orderBy.id) { return query = query.OrderBy(a => a.Id); }
                else
                if (orderBy.createDate) { return query = query.OrderBy(a => a.CreateDate); }
                else
                if (orderBy.level) { return query = query.OrderBy(a => a.Level); }
                else
                if (orderBy.threadNo) { return query = query.OrderBy(a => a.ThreadNo); }
                else
                if (orderBy.message) { return query = query.OrderBy(a => a.Message); }
                else
                if (orderBy.projectName) { return query = query.OrderBy(a => a.ProjectName); }
                else
                if (orderBy.typeName) { return query = query.OrderBy(a => a.TypeName); }
                else
                if (orderBy.funcName) { return query = query.OrderBy(a => a.FuncName); }
                else
                if (orderBy.exception) { return query = query.OrderBy(a => a.Exception); }
                else
                if (orderBy.username) { return query = query.OrderBy(a => a.Username); }
            }
            orderBy = eqArgs.orderByDesc;
            if (orderBy != null)
            {

                if (orderBy.id) { return query = query.OrderByDescending(a => a.Id); }
                else
                if (orderBy.createDate) { return query = query.OrderByDescending(a => a.CreateDate); }
                else
                if (orderBy.level) { return query = query.OrderByDescending(a => a.Level); }
                else
                if (orderBy.threadNo) { return query = query.OrderByDescending(a => a.ThreadNo); }
                else
                if (orderBy.message) { return query = query.OrderByDescending(a => a.Message); }
                else
                if (orderBy.projectName) { return query = query.OrderByDescending(a => a.ProjectName); }
                else
                if (orderBy.typeName) { return query = query.OrderByDescending(a => a.TypeName); }
                else
                if (orderBy.funcName) { return query = query.OrderByDescending(a => a.FuncName); }
                else
                if (orderBy.exception) { return query = query.OrderByDescending(a => a.Exception); }
                else
                if (orderBy.username) { return query = query.OrderByDescending(a => a.Username); }
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
            if (entity.Username != null)
            {
                updateBefore.Username = entity.Username;
            }
        }

        /// <summary>
        /// 把指定字段值设置为null的部分共用代码，这里抽取出来简化结构
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="param"></param>
        protected override void SetNull(BaseDbContext dbContext, LogEntitySetNullParams param)
        {
            LogEntity updateBefore = FindEntity(param.id);
            dbContext.Set<LogEntity>().Attach(updateBefore);
            if (param.createDate)
            {
                updateBefore.CreateDate = null;
            }
            if (param.level)
            {
                updateBefore.Level = null;
            }
            if (param.threadNo)
            {
                updateBefore.ThreadNo = null;
            }
            if (param.message)
            {
                updateBefore.Message = null;
            }
            if (param.projectName)
            {
                updateBefore.ProjectName = null;
            }
            if (param.typeName)
            {
                updateBefore.TypeName = null;
            }
            if (param.funcName)
            {
                updateBefore.FuncName = null;
            }
            if (param.exception)
            {
                updateBefore.Exception = null;
            }
            if (param.username)
            {
                updateBefore.Username = null;
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
                if (orderBy.id)
                {
                    disPageEntity.OrderCol = a => a.Id;
                    disPageEntity.OrderColLazy = a => a.Id;
                    return disPageEntity;
                }
                if (orderBy.createDate)
                {
                    disPageEntity.OrderCol = a => a.CreateDate;
                    disPageEntity.OrderColLazy = a => a.CreateDate;
                    return disPageEntity;
                }
                if (orderBy.level)
                {
                    disPageEntity.OrderCol = a => a.Level;
                    disPageEntity.OrderColLazy = a => a.Level;
                    return disPageEntity;
                }
                if (orderBy.threadNo)
                {
                    disPageEntity.OrderCol = a => a.ThreadNo;
                    disPageEntity.OrderColLazy = a => a.ThreadNo;
                    return disPageEntity;
                }
                if (orderBy.message)
                {
                    disPageEntity.OrderCol = a => a.Message;
                    disPageEntity.OrderColLazy = a => a.Message;
                    return disPageEntity;
                }
                if (orderBy.projectName)
                {
                    disPageEntity.OrderCol = a => a.ProjectName;
                    disPageEntity.OrderColLazy = a => a.ProjectName;
                    return disPageEntity;
                }
                if (orderBy.typeName)
                {
                    disPageEntity.OrderCol = a => a.TypeName;
                    disPageEntity.OrderColLazy = a => a.TypeName;
                    return disPageEntity;
                }
                if (orderBy.funcName)
                {
                    disPageEntity.OrderCol = a => a.FuncName;
                    disPageEntity.OrderColLazy = a => a.FuncName;
                    return disPageEntity;
                }
                if (orderBy.exception)
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

                if (orderBy.id)
                {
                    disPageEntity.OrderCol = a => a.Id;
                    disPageEntity.OrderColLazy = a => a.Id;
                    return disPageEntity;
                }
                else
                if (orderBy.createDate)
                {
                    disPageEntity.OrderCol = a => a.CreateDate;
                    disPageEntity.OrderColLazy = a => a.CreateDate;
                    return disPageEntity;
                }
                else
                if (orderBy.level)
                {
                    disPageEntity.OrderCol = a => a.Level;
                    disPageEntity.OrderColLazy = a => a.Level;
                    return disPageEntity;
                }
                else
                if (orderBy.threadNo)
                {
                    disPageEntity.OrderCol = a => a.ThreadNo;
                    disPageEntity.OrderColLazy = a => a.ThreadNo;
                    return disPageEntity;
                }
                else
                if (orderBy.message)
                {
                    disPageEntity.OrderCol = a => a.Message;
                    disPageEntity.OrderColLazy = a => a.Message;
                    return disPageEntity;
                }
                else
                if (orderBy.projectName)
                {
                    disPageEntity.OrderCol = a => a.ProjectName;
                    disPageEntity.OrderColLazy = a => a.ProjectName;
                    return disPageEntity;
                }
                else
                if (orderBy.typeName)
                {
                    disPageEntity.OrderCol = a => a.TypeName;
                    disPageEntity.OrderColLazy = a => a.TypeName;
                    return disPageEntity;
                }
                else
                if (orderBy.funcName)
                {
                    disPageEntity.OrderCol = a => a.FuncName;
                    disPageEntity.OrderColLazy = a => a.FuncName;
                    return disPageEntity;
                }
                else
                if (orderBy.exception)
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