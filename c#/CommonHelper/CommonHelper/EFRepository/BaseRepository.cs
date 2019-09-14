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
using CommonHelper.AopInterceptor;
using CommonHelper.staticVar;

namespace CommonHelper.Helper.EFRepository
{
    /// <summary>
    /// 通用的数据操作基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class BaseRepository<TEntity, TParams, TSetNullParams> where TEntity : class where TParams : class where TSetNullParams :class
    {
        /// <summary>
        /// 分布式雪花id生成器
        /// </summary>
        public IdWorker IdWorker { set; get; }

        static BaseRepository()
        {
            ReadOnlyMainDbContext = new DistributedMainDbContext();
        }

        /// <summary>
        /// 创建DbContext，为解决每张表可能不在同一个数据库内的问题，
        /// 由子类决定创建哪一种DbContext
        /// </summary>
        /// <returns></returns>
        public abstract BaseDbContext CreateDbContext();


        /// <summary>
        /// 创建多个DbContext，返回所有数据源
        /// </summary>
        /// <returns></returns>
        public abstract List<BaseDbContext> CreateAllDbContext();

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
        /// 获取当前数据源的逆操作对象
        /// </summary>
        /// <returns></returns>
        public abstract InverseRepository<TEntity> CurrentInverse();

        /// <summary>
        /// 插入一条数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>返回插入的数据数量</returns>
        public virtual int Insert(TEntity entity)
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
        public virtual int Insert(List<TEntity> entities)
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
        public virtual int Delete(TEntity entity)
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
        public virtual int Delete(List<TEntity> entities)
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
        /// 根据条件删除对象，注意：该方法不支持分布式事务。
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
        public virtual int UpdateAll(TEntity entity)
        {
            using (DbContext dbContext = CreateDbContext())
            {
                dbContext.Entry(entity).State = EntityState.Modified;
                return dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// 批量修改所有字段，注意：为null的也会在数据库上写成null
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual int UpdateAll(List<TEntity> entities)
        {
            using (DbContext dbContext = CreateDbContext())
            {
                foreach (TEntity entity in entities)
                {
                    dbContext.Entry(entity).State = EntityState.Modified;
                }
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
        /// 检查分布式事务是否为成功提交，只有提交成功才能继续操作，提交失败则无法继续操作
        /// </summary>
        /// <param name="primaryKeyVal">主键的值</param>
        /// <param name="transTableName">事务操作的表名称</param>
		public void CheckTransactionFinish(long? primaryKeyVal, string transTableName)
        {
            using (DbContext dbContext = CreateDbContext())
            {
                var query = dbContext.Set<DistributedTransactionPart>().AsNoTracking().AsQueryable().Where(a => a.TransTableName == transTableName && a.TransPrimaryKeyVal == primaryKeyVal && a.TransactionStatus == 0);
                if (query.Any())
                {
                    DistributedTransactionPart distributedTransactionPart = query.FirstOrDefault();
                    var result=ReadOnlyMainDbContext.DistributedTransactionMains.AsNoTracking().AsQueryable().Where(a => a.Id == distributedTransactionPart.DistributedTransactionMainId && a.TransactionStatus==1).Any();
                    List<DistributedTransactionMainDetail> mainDetails = ReadOnlyMainDbContext.DistributedTransactionMainDetails.AsNoTracking().AsQueryable().Where(a => a.DistributedTransactionMainId == distributedTransactionPart.DistributedTransactionMainId).ToList();
                    if (mainDetails.Count == 0)
                    {
                        InverseRepository<TEntity> inverseRepository = CurrentInverse();
                        if (result)
                        {
                            inverseRepository.UpdateTransPartsStatus(distributedTransactionPart.Id, 1);
                        }
                        else
                        {
                            if (distributedTransactionPart.InverseOperType == "D" || distributedTransactionPart.InverseOperType == "d")
                            {
                                inverseRepository.DistributedDeleteInverse(distributedTransactionPart);
                            }
                            else if (distributedTransactionPart.InverseOperType == "I" || distributedTransactionPart.InverseOperType == "i")
                            {
                                inverseRepository.DistributedInsertInverse(distributedTransactionPart);
                            }
                            else if (distributedTransactionPart.InverseOperType == "U" || distributedTransactionPart.InverseOperType == "u")
                            {
                                inverseRepository.DistributedUpdateInverse(distributedTransactionPart);
                            }
                        }
                    }
                    else
                    {
                        foreach (DistributedTransactionMainDetail mainDetail in mainDetails)
                        {
                            var inverseRepository = AllStatic.InverseRepositoryMap[mainDetail.TransactionDataSource][mainDetail.TransactionTable];
                            List<DistributedTransactionPart> distributedTransactionPartList = inverseRepository.FindTransPartsById(distributedTransactionPart.DistributedTransactionMainId);
                            if (result)
                            {
                                inverseRepository.UpdateTransPartsStatus(distributedTransactionPartList.Select(a => a.Id).ToList(), 1);
                            }
                            else
                            {
                                //批量插入的逆操作
                                List<DistributedTransactionPart> dparts = new List<DistributedTransactionPart>();
                                //批量删除的逆操作
                                List<DistributedTransactionPart> iparts = new List<DistributedTransactionPart>();
                                //批量更新的逆操作
                                List<DistributedTransactionPart> uparts = new List<DistributedTransactionPart>();
                                foreach (DistributedTransactionPart part in distributedTransactionPartList)
                                {
                                    if (part.InverseOperType == "D")
                                    {
                                        dparts.Add(part);
                                    }
                                    else if (part.InverseOperType == "d")
                                    {
                                        inverseRepository.DistributedDeleteInverse(part);
                                    }
                                    else if (part.InverseOperType == "I")
                                    {
                                        iparts.Add(part);
                                    }
                                    else if (part.InverseOperType == "i")
                                    {
                                        inverseRepository.DistributedInsertInverse(part);
                                    }
                                    else if (part.InverseOperType == "U")
                                    {
                                        uparts.Add(part);
                                    }
                                    else if (part.InverseOperType == "u")
                                    {
                                        inverseRepository.DistributedUpdateInverse(part);
                                    }
                                    inverseRepository.DistributedDeleteInverse(dparts);
                                    inverseRepository.DistributedInsertInverse(iparts);
                                    inverseRepository.DistributedUpdateInverse(uparts);
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 设置查询时的筛选条件
        /// </summary>
        /// <param name="query">查询对象</param>
        /// <param name="paramz">查询参数</param>
        /// <param name="neqArgs">不等查询参数</param>
        /// <param name="baseVal">基准值，仅在跨库分页查询时使用</param>
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
                    EndItemIndex = Math.Min(pageSize * pageIndex,totalItemCount)
                };
            }
        }

        /// <summary>
        /// 获取跨库查询所需的表达式
        /// </summary>
        /// <returns></returns>
        protected abstract DisPageEntity<TEntity> GetDisPageEntity(TParams paramz);

        /// <summary>
        /// 跨库分页时，基准值查询
        /// </summary>
        /// <param name="paramz">查询参数，判断排序的重要依据</param>
        /// <param name="baseVal">基准值，缩小查询范围</param>
        /// <param name="queryType">查询类型，0：查询基准值跳过的数据量，1：查询比基准值大，并当中含有合适的基准值</param>
        /// <returns></returns>
        protected abstract Expression<Func<TEntity, bool>> BaseValQuery(TParams paramz,IComparable baseVal,int queryType);

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
            List<BaseDbContext> dbContexts = CreateAllDbContext();
            //单个数据源的分页
            if (dbContexts.Count == 1)
            {
                using (DbContext dbContext = dbContexts[0])
                {
                    IQueryable<TEntity> query = Query(dbContext.Set<TEntity>().AsNoTracking().AsQueryable(), eqArgs, neqArgs);
                    return query.ToMyPagedList(pageIndex, pageSize);
                }
            }
            //多个数据源的分页（需改成按比例）
            else if (dbContexts.Count > 1)
            {
                int sumSkipCount = (pageIndex - 1) * pageSize; //期望的总跳过数据量
                int totalCount = 0; //符合条件的总数据量
                List<int> counts = new List<int>(); //各个数据源的符合条件数据
                List<BaseDbContext> baseDbContextList = new List<BaseDbContext>(); //含有符合条件数据的数据库
                for (int i = 0, count, len = dbContexts.Count; i < len; i++)
                {
                    IQueryable<TEntity> query = Query(dbContexts[i].Set<TEntity>().AsNoTracking().AsQueryable(), eqArgs, neqArgs);
                    count = query.Count();
                    if (count > 0)
                    {
                        totalCount += count;
                        counts.Add(count);
                        baseDbContextList.Add(dbContexts[i]);
                    }
                }
                bool orderType = true;
                IComparable comparable; //临时变量
                DisPageEntity<TEntity> disPageEntity = GetDisPageEntity(eqArgs);
                List <IComparable> sortList = new List<IComparable>();
                for (int i = 0, len = dbContexts.Count; i < len; i++)
                {
                    IQueryable<TEntity> query = Query(baseDbContextList[i].Set<TEntity>().AsNoTracking().AsQueryable(), eqArgs, neqArgs);
                    comparable = query.Select(disPageEntity.OrderColLazy).Skip(counts[i] * sumSkipCount / totalCount).Take(1).FirstOrDefault();
                    if (comparable != null)
                    {
                        sortList.Add(comparable);
                    }
                }
                sortList.Sort();
                IComparable baseVal = sortList[orderType ? 0 : sortList.Count - 1]; //得到基准值
                int baseValSkip = 0; //基准值跳过数据量
                for (int i = 0, len = dbContexts.Count; i < len; i++)
                {
                    IQueryable<TEntity> query = Query(baseDbContextList[i].Set<TEntity>().AsNoTracking().AsQueryable(), eqArgs, neqArgs);
                    query = query.Where(BaseValQuery(eqArgs, baseVal, 0));
                    baseValSkip += query.Select(disPageEntity.OrderColLazy).Count();
                    
                }
                int restSkipCount = sumSkipCount - baseValSkip; //剩余需要跳过的数据量
                sortList.Clear();
                for (int i = 0, len = dbContexts.Count; i < len; i++)
                {
                    IQueryable<TEntity> query = Query(baseDbContextList[i].Set<TEntity>().AsNoTracking().AsQueryable(), eqArgs, neqArgs);
                    query = query.Where(BaseValQuery(eqArgs,baseVal,1));
                    sortList.AddRange(query.Select(disPageEntity.OrderColLazy).Take(restSkipCount).ToList());
                }
                sortList.Sort();
                baseVal = sortList[orderType ? restSkipCount : sortList.Count - restSkipCount - 1]; //得到基准值,
                List<TEntity> entities = new List<TEntity>();
                for (int i = 0, len = dbContexts.Count; i < len; i++)
                {
                    IQueryable<TEntity> query = Query(baseDbContextList[i].Set<TEntity>().AsNoTracking().AsQueryable(), eqArgs, neqArgs);
                    entities.AddRange(query.Take(pageSize).ToList());
                }
                if (orderType)
                {
                    entities = entities.OrderBy(disPageEntity.OrderCol).Take(pageSize).ToList();
                }
                else
                {
                    entities = entities.OrderByDescending(disPageEntity.OrderCol).Take(pageSize).ToList();
                }
                return new MyPagedList<TEntity>
                {
                    PageDataList = entities,
                    CurrentPageIndex = pageIndex,
                    EndItemIndex = pageIndex * pageSize + entities.Count,
                    PageSize = pageSize,
                    StartItemIndex = pageIndex * pageSize + 1,
                    TotalItemCount = totalCount,
                    TotalPageCount = (totalCount - totalCount % pageSize) / pageSize + 1
                };
            }
            throw new Exception("没有找到数据源，请确保“CreateAllDbContext”方法正确实现。");
        }

        /// <summary>
        /// 修改变化值，把entity不为null的数据视为变化值
        /// </summary>
        /// <param name="entity">修改实体类，必须要传入主键，其余的参数如果不为null则视为变化值（主键id除外，只做数据标识）</param>
        /// <returns>返回修改的数据行数</returns>
        public virtual int UpdateChange(TEntity entity)
        {
            using (BaseDbContext dbContext = CreateDbContext())
            {
                UpdateChange(dbContext, entity);
                return dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// 批量修改变化值，把entity不为null的数据视为变化值
        /// </summary>
        /// <param name="entities">修改实体类集合，必须要传入主键，其余的参数如果不为null则视为变化值（主键id除外，只做数据标识）</param>
        /// <returns>返回修改的数据行数</returns>
        public virtual int UpdateChange(List<TEntity> entities)
        {
            using (BaseDbContext dbContext = CreateDbContext())
            {
                foreach (TEntity entity in entities)
                {
                    UpdateChange(dbContext, entity);
                }
                return dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// 修改变化值的部分共用代码，这里抽取出来简化结构
        /// </summary>
        /// <param name="updateBefore"></param>
        /// <param name="entity"></param>
        protected abstract void UpdateChange(BaseDbContext dbContext, TEntity entity);


        /// <summary>
        /// 把指定字段值设置为null
        /// </summary>
        /// <param name="param">设为null的字段参数，主键必须要传入，其他字段等于true则表示设置为null</param>
        /// <returns>返回修改的数据行数</returns>
        public virtual void SetNull(TSetNullParams param)
        {
            using (BaseDbContext dbContext = CreateDbContext())
            {
                SetNull(dbContext, param);
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// 把指定字段值设置为null的部分共用代码，这里抽取出来简化结构
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="param"></param>
        protected abstract void SetNull(BaseDbContext dbContext, TSetNullParams param);
    }
}