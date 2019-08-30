using CommonHelper.Helper.CommonExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;
using WebApplication1.Entity.Common;
using Snowflake.Net;
using Newtonsoft.Json.Converters;
using CommonHelper.Helper.EFDbContext;
using CommonHelper.CommonEntity;
using CommonHelper.EFRepository;

namespace CommonHelper.Helper.EFRepository
{
    /// <summary>
    /// 通用的数据操作基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class BaseRepository<TEntity, TParams, TOrderBy>: InverseRepository where TEntity : class where TParams : class where TOrderBy : class
    {
        /// <summary>
        /// 分布式雪花id生成器
        /// </summary>
        public IdWorker IdWorker { set; get; }

        /// <summary>
        /// 默认的时间转换器
        /// </summary>
        protected static IsoDateTimeConverter TimeConverter { set; get; }

        static BaseRepository()
        {
            TimeConverter = new IsoDateTimeConverter();
            TimeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            ReadOnlyMainDbContext = new DistributedMainDbContext();
        }

        /// <summary>
        /// 根据事务id，查询事务未确定或未取消的分表信息。
        /// </summary>
        /// <param name="transactionId">事务id</param>
        /// <returns></returns>
        public List<DistributedTransactionPart> FindTransPartsById(long? transactionId=null)
        {
            using (DbContext dbContext = CreateDbContext())
            {
                var query = dbContext.Set<DistributedTransactionPart>().AsNoTracking().AsQueryable().Where(a => a.TransactionStatus == 0);
                if (transactionId != null)
                {
                    query.Where(a=> a.DistributedTransactionMainId == transactionId);
                }
                return query.Select(a=>new DistributedTransactionPart { Id=a.Id,InverseOper=a.InverseOper,InverseOperType=a.InverseOperType,TransPrimaryKeyVal=a.TransPrimaryKeyVal })
                    .ToList();
            }
        }

        /// <summary>
        /// 更新事务分表状态
        /// </summary>
        /// <param name="idList">事务分表的主键</param>
        /// <param name="status">更新状态</param>
        public void UpdateTransPartsStatus(List<long> idList, sbyte status)
        {
            using (DbContext dbContext = CreateDbContext())
            {
                foreach (long id in idList)
                {
                    DistributedTransactionPart updateBefore = new DistributedTransactionPart {Id=id };
                    dbContext.Set<DistributedTransactionPart>().Attach(updateBefore);
                    updateBefore.TransactionStatus = status;
                    dbContext.SaveChanges();
                }
            }
        }

        /// <summary>
        /// 创建DbContext，为解决每张表可能不在同一个数据库内的问题，
        /// 由子类决定创建哪一种DbContext
        /// </summary>
        /// <returns></returns>
        public abstract DbContext CreateDbContext();

        /// <summary>
        /// 只读数据操作
        /// </summary>
        public ReadOnlyRepository ReadOnly { get; private set; }

        /// <summary>
        /// 只读的分布式主表数据源
        /// </summary>
        public static DistributedMainDbContext ReadOnlyMainDbContext { get; private set; }

        public BaseRepository()
        {
            ReadOnly = new ReadOnlyRepository(CreateDbContext());
        }

        public class ReadOnlyRepository
        {
            /// <summary>
            /// 只读的dbContext，只用于查询只读数据，使用缓存提高效率
            /// </summary>
            DbContext _readonlyDbContext { set; get; }

            public ReadOnlyRepository(DbContext dbContext)
            {
                _readonlyDbContext = dbContext;
            }

            /// <summary>
            /// 根据主键查找一个实体
            /// </summary>
            /// <param name="id">主键</param>
            /// <returns></returns>
            public TEntity FindEntity(object id)
            {
                return _readonlyDbContext.Set<TEntity>().Find(id);
            }

            /// <summary>
            /// 根据条件查找一个实体
            /// </summary>
            /// <param name="predicate">条件</param>
            /// <returns></returns>
            public TEntity FindEntity(Expression<Func<TEntity, bool>> predicate)
            {
                return _readonlyDbContext.Set<TEntity>().FirstOrDefault(predicate);
            }

            /// <summary>
            /// 根据条件查询是否有匹配的数据
            /// </summary>
            /// <param name="predicate">条件</param>
            /// <returns></returns>
            public bool Exist(Expression<Func<TEntity, bool>> predicate)
            {
                return _readonlyDbContext.Set<TEntity>().Where(predicate).Any();
            }

            /// <summary>
            /// 根据条件查询符合条件的数据量
            /// </summary>
            /// <param name="predicate"></param>
            /// <returns></returns>
            public int Count(Expression<Func<TEntity, bool>> predicate)
            {
                return _readonlyDbContext.Set<TEntity>().Where(predicate).Count();
            }

            /// <summary>
            /// 根据sql查询列表
            /// </summary>
            /// <param name="sql">sql</param>
            /// <returns></returns>
            public List<TEntity> FindList(string sql)
            {
                var query = _readonlyDbContext.Database.SqlQuery<TEntity>(sql);
                if (query.Any()) { return query.ToList(); }
                return new List<TEntity>();
            }

            /// <summary>
            /// 根据条件查询列表
            /// </summary>
            /// <param name="predicate"></param>
            /// <returns></returns>
            public List<TEntity> FindList(Expression<Func<TEntity, bool>> predicate = null)
            {
                var query = _readonlyDbContext.Set<TEntity>();
                if (predicate == null)
                {
                    return query.ToList();
                }
                else
                {
                    return query.Where(predicate).ToList();
                }
            }

            /// <summary>
            /// 根据条件分页查询
            /// </summary>
            /// <param name="predicate"></param>
            /// <param name="pageIndex"></param>
            /// <param name="pageSize"></param>
            /// <returns></returns>
            public MyPagedList<TEntity> PageList(Expression<Func<TEntity, bool>> predicate, int pageIndex, int pageSize)
            {
                return _readonlyDbContext.Set<TEntity>().Where(predicate).ToMyPagedList(pageIndex, pageSize);
            }
        }


        /// <summary>
        /// 插入一条数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>返回插入的数据数量</returns>
        public int Insert(TEntity entity)
        {
            using (DbContext dbContext = CreateDbContext())
            {
                dbContext.Entry(entity).State = EntityState.Added;
                return dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// 插入一批数据
        /// </summary>
        /// <param name="entities">表数据列表对象</param>
        /// <returns></returns>
        public int Insert(List<TEntity> entities)
        {
            using (DbContext dbContext = CreateDbContext())
            {
                foreach (var entity in entities)
                {
                    dbContext.Entry(entity).State = EntityState.Added;
                }
                return dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// 删除一个对象
        /// </summary>
        /// <param name="entity">对象实体</param>
        /// <returns></returns>
        public int Delete(TEntity entity)
        {
            using (DbContext dbContext = CreateDbContext())
            {
                dbContext.Entry(entity).State = EntityState.Deleted;
                return dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// 删除一批对象
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public int Delete(List<TEntity> entities)
        {
            using (DbContext dbContext = CreateDbContext())
            {
                foreach (TEntity entity in entities)
                {
                    dbContext.Entry(entity).State = EntityState.Deleted;
                }
                return dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// 根据条件删除对象
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns></returns>
        public int Delete(Expression<Func<TEntity, bool>> predicate)
        {
            using (DbContext dbContext = CreateDbContext())
            {
                var entitys = dbContext.Set<TEntity>().AsNoTracking().Where(predicate).ToList();
                entitys.ForEach(m => dbContext.Entry(m).State = EntityState.Deleted);
                return dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// 根据主键查找一个实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public TEntity FindEntity(object id)
        {
            using (DbContext dbContext = CreateDbContext())
            {
                return dbContext.Set<TEntity>().Find(id);
            }
        }


        /// <summary>
        /// 根据条件查找一个实体
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns></returns>
        public TEntity FindEntity(Expression<Func<TEntity, bool>> predicate)
        {
            using (DbContext dbContext = CreateDbContext())
            {
                var entity = dbContext.Set<TEntity>().AsNoTracking().FirstOrDefault(predicate);
                return entity;
            }
        }

        /// <summary>
        /// 根据条件查询是否有匹配的数据
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns></returns>
        public bool Exist(Expression<Func<TEntity, bool>> predicate)
        {
            using (DbContext dbContext = CreateDbContext())
            {
                return dbContext.Set<TEntity>().AsNoTracking().Where(predicate).Any();
            }
        }

        /// <summary>
        /// 修改所有字段，注意：为null的也会在数据库上写成null
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int UpdateAll(TEntity entity)
        {
            using (DbContext dbContext = CreateDbContext())
            {
                dbContext.Entry(entity).State = EntityState.Modified;
                return dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// 根据条件查询符合条件的数据量
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            using (DbContext dbContext = CreateDbContext())
            {
                return dbContext.Set<TEntity>().AsNoTracking().Where(predicate).Count();
            }
        }

        /// <summary>
        /// 根据sql查询列表
        /// </summary>
        /// <param name="sql">sql</param>
        /// <returns></returns>
        public List<TEntity> FindList(string sql)
        {
            using (DbContext dbContext = CreateDbContext())
            {
                var query = dbContext.Database.SqlQuery<TEntity>(sql);
                if (query.Any()) { return query.ToList(); }
                return new List<TEntity>();
            }
        }

        /// <summary>
        /// 根据条件查询列表
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public List<TEntity> FindList(Expression<Func<TEntity, bool>> predicate = null)
        {
            using (DbContext dbContext = CreateDbContext())
            {
                var query = dbContext.Set<TEntity>().AsNoTracking();
                if (predicate == null)
                {
                    return query.ToList();
                }
                else
                {
                    return query.Where(predicate).ToList();
                }
            }
        }

        /// <summary>
        /// 检查分布式事务是否已完成
        /// </summary>
        /// <param name="primaryKeyVal">主键的值</param>
        /// <param name="transTableName">事务操作的表名称</param>
		public bool CheckTransactionFinish(long primaryKeyVal, string transTableName)
        {
            using (DbContext dbContext = CreateDbContext())
            {
                var query = dbContext.Set<DistributedTransactionPart>().AsNoTracking().AsQueryable().Where(a => a.TransTableName == transTableName && a.TransPrimaryKeyVal == primaryKeyVal && a.TransactionStatus == 0);
                if (query.Any())
                {
                    long distributedTransactionMainId = query.Select(a => a.DistributedTransactionMainId).FirstOrDefault();
                    var result=ReadOnlyMainDbContext.DistributedTransactionMains.AsNoTracking().AsQueryable().Where(a => a.Id == distributedTransactionMainId).Any();
                    if (result)
                    {
                        ///调用消息队列，通知确认事务
                        return true;
                    }
                    else
                    {
                        ///调用消息队列，通知取消事务
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// 设置查询时的筛选条件
        /// </summary>
        /// <param name="query">查询对象</param>
        /// <param name="paramz">筛选条件</param>
        /// <returns></returns>
        protected abstract IQueryable<TEntity> Query(IQueryable<TEntity> query, TParams paramz, TParams neqArgs);


        /// <summary>
        /// 查询符合查询条件的数据量
        /// </summary>
        /// <param name="eqArgs">查询参数，不为null时会作为查询参数</param>
		/// <param name="neqArgs">不等查询参数，不为null时会作为不等查询参数</param>
        /// <returns>返回符合查询条件的数据量</returns>
        public int Count(TParams eqArgs = null, TParams neqArgs = null)
        {
            using (DbContext dbContext = CreateDbContext())
            {
                IQueryable<TEntity> query = Query(dbContext.Set<TEntity>().AsNoTracking().AsQueryable(), eqArgs, neqArgs);
                return query.Count();
            }
        }

        /// <summary>
        /// 查询符合查询条件的数据，数据不宜过大，如果数据过大建议使用分页查询
        /// </summary>
        /// <param name="eqArgs">查询参数，不为null时会作为查询参数</param>
        /// <param name="neqArgs">不等查询参数，不为null时会作为不等查询参数</param>
        /// <returns>返回符合查询条件的数</returns>
        public List<TEntity> FindList(TParams eqArgs = null, TParams neqArgs = null)
        {
            using (DbContext dbContext = CreateDbContext())
            {
                IQueryable<TEntity> query = Query(dbContext.Set<TEntity>().AsNoTracking().AsQueryable(), eqArgs, neqArgs);
                return query.ToList();
            }
        }


        /// <summary>
        /// 查询pageSize超过1000的分页查询，因为pageSize过大会导致查询超时，为避免这种情况，
		/// 必须使用该方法进行分页查询。
        /// </summary>
        /// <param name="eqArgs">查询参数，不为null时会作为查询参数</param>
        /// <param name="pageIndex">查询页码</param>
        /// <param name="pageSize">每页显示数据量</param>
		/// <param name="neqArgs">不等查询参数，不为null时会作为不等查询参数</param>
        /// <returns>返回分页查询结果</returns>
        public MyPagedList<TEntity> BigPageList(TParams eqArgs, int pageIndex = 1, int pageSize = 10000, TParams neqArgs = null)
        {
            if (pageIndex <= 0)
            {
                pageIndex = 1;
            }
            if (pageSize > 10000 || pageSize <= 0)
            {
                pageSize = 10000;
            }
            using (DbContext dbContext = CreateDbContext())
            {
                IQueryable<TEntity> query = Query(dbContext.Set<TEntity>().AsNoTracking().AsQueryable(), eqArgs, neqArgs);
                int totalItemCount = query.Count();
                int totalPageCount = (totalItemCount - totalItemCount % pageSize) / pageSize + 1;
                if (pageIndex >= totalPageCount)
                {
                    pageIndex = Math.Max(totalPageCount, 1);
                }
                query = query.Skip((pageIndex - 1) * pageSize);
                List<TEntity> pageDataList = new List<TEntity>();
                for (int i = 0, partCount = (pageSize - pageSize % 1000) / pageSize + 1; i < partCount; i++)
                {
                    pageDataList.AddRange(query.Skip(i * 1000).Take(1000).ToList());
                }
                return new MyPagedList<TEntity>
                {
                    CurrentPageIndex = pageIndex,
                    PageDataList = pageDataList,
                    TotalItemCount = totalItemCount,
                    PageSize = pageSize,
                    TotalPageCount = totalPageCount,
                    StartItemIndex = (pageSize - 1) * pageIndex + 1,
                    EndItemIndex = pageSize * pageIndex
                };
            }
        }

        /// <summary>
        /// 普通的分页查询功能，pageSize不宜过大，如果pageSize大于1000，使用：BigPageList
        /// </summary>
        /// <param name="eqArgs">查询参数，不为null时会作为查询参数</param>
        /// <param name="pageIndex">查询页码</param>
        /// <param name="pageSize">每页显示数据量</param>
        /// <param name="neqArgs">不等查询参数，不为null时会作为不等查询参数</param>
        /// <returns>返回分页查询结果</returns>
        public MyPagedList<TEntity> PageList(TParams eqArgs, int pageIndex = 1, int pageSize = 20, TParams neqArgs = null)
        {
            if (pageIndex <= 0)
            {
                pageIndex = 1;
            }
            if (pageSize > 100)
            {
                pageSize = 100;
            }
            else if (pageSize <= 0)
            {
                pageSize = 20;
            }
            using (DbContext dbContext = CreateDbContext())
            {
                IQueryable<TEntity> query = Query(dbContext.Set<TEntity>().AsNoTracking().AsQueryable(), eqArgs, neqArgs);
                return query.ToMyPagedList(pageIndex, pageSize);
            }
        }

        public virtual void DistributedInsertInverse(DistributedTransactionPart distributedTransactionPart) { }
        public virtual void DistributedInsertInverse(List<DistributedTransactionPart> distributedTransactionParts) { }
        public virtual void DistributedSetNullInverse(DistributedTransactionPart distributedTransactionPart) { }
        public virtual void DistributedUpdateChangeInverse(DistributedTransactionPart distributedTransactionPart) { }
        public virtual void DistributedUpdateAllInverse(DistributedTransactionPart distributedTransactionPart) { }
        public virtual void DistributedUpdateAllBatchInverse(List<DistributedTransactionPart> distributedTransactionParts) { }
        public virtual void DistributedDeleteInverse(DistributedTransactionPart distributedTransactionPart) { }
        public virtual void DistributedDeleteInverse(List<DistributedTransactionPart> distributedTransactionParts) { }
        
    }
}