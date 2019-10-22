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
using System.Linq.Expressions;
using WebApplication1.OrderBy;

namespace WebApplication1.Repository
{
    //启用分布式事务
    //[Intercept(typeof(DistributeRepository))]
    public abstract partial class GlobalVariableRepository : BaseRepository<GlobalVariable, GlobalVariableParams, GlobalVariableSetNullParams>
    {
        /// <summary>
        /// 通用的设置查询参数方法，只有在参数不为null的情况下才会设置，
        /// 满足项目中常用的模糊查询，分页查询等功能
        /// </summary>
        /// <param name="query">待设置参数的query对象</param>
        /// <param name="eqArgs">装载查询参数的实体类</param>
        /// <param name="neqArgs">装载不等于查询参数的实体类</param>
        /// <returns>返回设置好查询参数的query对象</returns>
        protected override IQueryable<GlobalVariable> Query(IQueryable<GlobalVariable> query, GlobalVariableParams eqArgs, GlobalVariableParams neqArgs = null)
        {
            if (eqArgs != null)
            {

                if (eqArgs.Id != null)
                {
                    query = query.Where(a => a.Id == eqArgs.Id);
                }

                if (eqArgs.VarSortIndex != null)
                {
                    query = query.Where(a => a.VarSortIndex == eqArgs.VarSortIndex);
                }

                if (eqArgs.VarSortIndexStart != null)
                {
                    query = query.Where(a => a.VarSortIndex >= eqArgs.VarSortIndexStart);
                }
                if (eqArgs.VarSortIndexEnd != null)
                {
                    query = query.Where(a => a.VarSortIndex <= eqArgs.VarSortIndexEnd);
                }
                if (eqArgs.VarName != null)
                {
                    query = query.Where(a => a.VarName == eqArgs.VarName);
                }

                if (!string.IsNullOrEmpty(eqArgs.VarNameLike))
                {
                    query = query.Where(a => a.VarName.Contains(eqArgs.VarNameLike));
                }
                if (eqArgs.VarValue != null)
                {
                    query = query.Where(a => a.VarValue == eqArgs.VarValue);
                }

                if (!string.IsNullOrEmpty(eqArgs.VarValueLike))
                {
                    query = query.Where(a => a.VarValue.Contains(eqArgs.VarValueLike));
                }
                query = OrderBy(query, eqArgs);
            }
            if (neqArgs != null)
            {

                if (neqArgs.Id != null)
                {
                    query = query.Where(a => a.Id != neqArgs.Id);
                }

                if (neqArgs.VarSortIndex != null)
                {
                    query = query.Where(a => a.VarSortIndex != neqArgs.VarSortIndex);
                }

                if (neqArgs.VarName != null)
                {
                    query = query.Where(a => a.VarName != neqArgs.VarName);
                }

                if (!string.IsNullOrEmpty(neqArgs.VarNameLike))
                {
                    query = query.Where(a => !a.VarName.Contains(neqArgs.VarNameLike));
                }
                if (neqArgs.VarValue != null)
                {
                    query = query.Where(a => a.VarValue != neqArgs.VarValue);
                }

                if (!string.IsNullOrEmpty(neqArgs.VarValueLike))
                {
                    query = query.Where(a => !a.VarValue.Contains(neqArgs.VarValueLike));
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
        IQueryable<GlobalVariable> OrderBy(IQueryable<GlobalVariable> query, GlobalVariableParams eqArgs)
        {
            GlobalVariableOrderBy orderBy = eqArgs.orderByAsc;
            if (orderBy != null)
            {

                if (orderBy.Id) { return query = query.OrderBy(a => a.Id); }
                else
                if (orderBy.VarSortIndex) { return query = query.OrderBy(a => a.VarSortIndex); }
                else
                if (orderBy.VarName) { return query = query.OrderBy(a => a.VarName); }
                else
                if (orderBy.VarValue) { return query = query.OrderBy(a => a.VarValue); }
            }
            orderBy = eqArgs.orderByDesc;
            if (orderBy != null)
            {

                if (orderBy.Id) { return query = query.OrderByDescending(a => a.Id); }
                else
                if (orderBy.VarSortIndex) { return query = query.OrderByDescending(a => a.VarSortIndex); }
                else
                if (orderBy.VarName) { return query = query.OrderByDescending(a => a.VarName); }
                else
                if (orderBy.VarValue) { return query = query.OrderByDescending(a => a.VarValue); }
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
        protected override void UpdateChange(BaseDbContext dbContext, GlobalVariable entity)
        {
            GlobalVariable updateBefore = new GlobalVariable { Id = entity.Id };
            dbContext.Set<GlobalVariable>().Attach(updateBefore);
            if (entity.VarSortIndex != null)
            {
                updateBefore.VarSortIndex = entity.VarSortIndex;
            }
            if (entity.VarName != null)
            {
                updateBefore.VarName = entity.VarName;
            }
            if (entity.VarValue != null)
            {
                updateBefore.VarValue = entity.VarValue;
            }
        }

        /// <summary>
        /// 把指定字段值设置为null的部分共用代码，这里抽取出来简化结构
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="param"></param>
        protected override void SetNull(BaseDbContext dbContext, GlobalVariableSetNullParams param)
        {
            GlobalVariable updateBefore = FindEntity(param.Id);
            dbContext.Set<GlobalVariable>().Attach(updateBefore);
            if (param.VarSortIndex)
            {
                updateBefore.VarSortIndex = null;
            }
            if (param.VarName)
            {
                updateBefore.VarName = null;
            }
            if (param.VarValue)
            {
                updateBefore.VarValue = null;
            }
        }

        /// <summary>
        /// 检查分布式事务是否已完成
        /// </summary>
        /// <param name="primaryKeyVal">主键的值</param>
        public void CheckTransactionFinish(long primaryKeyVal)
        {
            CheckTransactionFinish(primaryKeyVal, "global_variable");
        }

        public override InverseRepository<GlobalVariable> CurrentInverse()
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
        protected override DisPageEntity<GlobalVariable> GetDisPageEntity(GlobalVariableParams paramz)
        {
            DisPageEntity<GlobalVariable> disPageEntity = new DisPageEntity<GlobalVariable>();
            disPageEntity.InCondition = (entities) =>
            {
                List<long?> keys = entities.Select(a => a.Id).ToList();
                return a => keys.Contains(a.Id);
            };
            GlobalVariableOrderBy orderBy = paramz.orderByAsc;
            if (orderBy != null)
            {
                disPageEntity.OrderType = true;
                if (orderBy.Id)
                {
                    disPageEntity.OrderCol = a => a.Id;
                    disPageEntity.OrderColLazy = a => a.Id;
                    disPageEntity.OrderColAndKeyLazy = a => new GlobalVariable { Id = a.Id };
                    disPageEntity.GreatThan = (val) => a => val.CompareTo(a.Id) < 0;
                    disPageEntity.GreatEqThan = (val) => a => val.CompareTo(a.Id) <= 0;
                    disPageEntity.LessThan = (val) => a => val.CompareTo(a.Id) > 0;
                    disPageEntity.LessEqThan = (val) => a => val.CompareTo(a.Id) >= 0;
                    return disPageEntity;
                }
                if (orderBy.VarSortIndex)
                {
                    disPageEntity.OrderCol = a => a.VarSortIndex;
                    disPageEntity.OrderColLazy = a => a.VarSortIndex;
                    disPageEntity.OrderColAndKeyLazy = a => new GlobalVariable { Id = a.Id, VarSortIndex = a.VarSortIndex };
                    disPageEntity.GreatThan = (val) => a => val.CompareTo(a.VarSortIndex) < 0;
                    disPageEntity.GreatEqThan = (val) => a => val.CompareTo(a.VarSortIndex) <= 0;
                    disPageEntity.LessThan = (val) => a => val.CompareTo(a.VarSortIndex) > 0;
                    disPageEntity.LessEqThan = (val) => a => val.CompareTo(a.VarSortIndex) >= 0;
                    return disPageEntity;
                }
                if (orderBy.VarName)
                {
                    disPageEntity.OrderCol = a => a.VarName;
                    disPageEntity.OrderColLazy = a => a.VarName;
                    disPageEntity.OrderColAndKeyLazy = a => new GlobalVariable { Id = a.Id, VarName = a.VarName };
                    disPageEntity.GreatThan = (val) => a => val.CompareTo(a.VarName) < 0;
                    disPageEntity.GreatEqThan = (val) => a => val.CompareTo(a.VarName) <= 0;
                    disPageEntity.LessThan = (val) => a => val.CompareTo(a.VarName) > 0;
                    disPageEntity.LessEqThan = (val) => a => val.CompareTo(a.VarName) >= 0;
                    return disPageEntity;
                }
                if (orderBy.VarValue)
                {
                    disPageEntity.OrderCol = a => a.VarValue;
                    disPageEntity.OrderColLazy = a => a.VarValue;
                    disPageEntity.OrderColAndKeyLazy = a => new GlobalVariable { Id = a.Id, VarValue = a.VarValue };
                    disPageEntity.GreatThan = (val) => a => val.CompareTo(a.VarValue) < 0;
                    disPageEntity.GreatEqThan = (val) => a => val.CompareTo(a.VarValue) <= 0;
                    disPageEntity.LessThan = (val) => a => val.CompareTo(a.VarValue) > 0;
                    disPageEntity.LessEqThan = (val) => a => val.CompareTo(a.VarValue) >= 0;
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
                    disPageEntity.OrderColAndKeyLazy = a => new GlobalVariable { Id = a.Id };
                    disPageEntity.GreatThan = (val) => a => val.CompareTo(a.Id) < 0;
                    disPageEntity.GreatEqThan = (val) => a => val.CompareTo(a.Id) <= 0;
                    disPageEntity.LessThan = (val) => a => val.CompareTo(a.Id) > 0;
                    disPageEntity.LessEqThan = (val) => a => val.CompareTo(a.Id) >= 0;
                    return disPageEntity;
                }
                else
                if (orderBy.VarSortIndex)
                {
                    disPageEntity.OrderCol = a => a.VarSortIndex;
                    disPageEntity.OrderColLazy = a => a.VarSortIndex;
                    disPageEntity.OrderColAndKeyLazy = a => new GlobalVariable { Id = a.Id, VarSortIndex = a.VarSortIndex };
                    disPageEntity.GreatThan = (val) => a => val.CompareTo(a.VarSortIndex) < 0;
                    disPageEntity.GreatEqThan = (val) => a => val.CompareTo(a.VarSortIndex) <= 0;
                    disPageEntity.LessThan = (val) => a => val.CompareTo(a.VarSortIndex) > 0;
                    disPageEntity.LessEqThan = (val) => a => val.CompareTo(a.VarSortIndex) >= 0;
                    return disPageEntity;
                }
                else
                if (orderBy.VarName)
                {
                    disPageEntity.OrderCol = a => a.VarName;
                    disPageEntity.OrderColLazy = a => a.VarName;
                    disPageEntity.OrderColAndKeyLazy = a => new GlobalVariable { Id = a.Id, VarName = a.VarName };
                    disPageEntity.GreatThan = (val) => a => val.CompareTo(a.VarName) < 0;
                    disPageEntity.GreatEqThan = (val) => a => val.CompareTo(a.VarName) <= 0;
                    disPageEntity.LessThan = (val) => a => val.CompareTo(a.VarName) > 0;
                    disPageEntity.LessEqThan = (val) => a => val.CompareTo(a.VarName) >= 0;
                    return disPageEntity;
                }
                else
                if (orderBy.VarValue)
                {
                    disPageEntity.OrderCol = a => a.VarValue;
                    disPageEntity.OrderColLazy = a => a.VarValue;
                    disPageEntity.OrderColAndKeyLazy = a => new GlobalVariable { Id = a.Id, VarValue = a.VarValue };
                    disPageEntity.GreatThan = (val) => a => val.CompareTo(a.VarValue) < 0;
                    disPageEntity.GreatEqThan = (val) => a => val.CompareTo(a.VarValue) <= 0;
                    disPageEntity.LessThan = (val) => a => val.CompareTo(a.VarValue) > 0;
                    disPageEntity.LessEqThan = (val) => a => val.CompareTo(a.VarValue) >= 0;
                    return disPageEntity;
                }
                else
                {
                    disPageEntity.OrderCol = a => a.Id;
                    disPageEntity.OrderColLazy = a => a.Id;
                    disPageEntity.OrderColAndKeyLazy = a => new GlobalVariable { Id = a.Id };
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
                disPageEntity.OrderColAndKeyLazy = a => new GlobalVariable { Id = a.Id };
                disPageEntity.GreatThan = (val) => a => val.CompareTo(a.Id) < 0;
                disPageEntity.GreatEqThan = (val) => a => val.CompareTo(a.Id) <= 0;
                disPageEntity.LessThan = (val) => a => val.CompareTo(a.Id) > 0;
                disPageEntity.LessEqThan = (val) => a => val.CompareTo(a.Id) >= 0;
                return disPageEntity;
            }
        }
    }
}