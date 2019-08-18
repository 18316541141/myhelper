using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using WebApplication1.Entity;
using WebApplication1.Entity.Common;
using WebApplication1.MyExtensions;
using WebApplication1.MyExtensions.Common;
using Webdiyer.WebControls.Mvc;

namespace WebApplication1.Repository
{
    /// <summary>
    /// 通用的数据操作基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class BaseRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// 只读数据操作
        /// </summary>
        public ReadOnlyRepository ReadOnly { get; private set; }

        static ReadOnlyRepository _readOnly { set; get; }

        public BaseRepository()
        {
            if (_readOnly == null)
            {
                lock (this)
                {
                    if (_readOnly == null)
                    {
                        _readOnly = ReadOnly = new ReadOnlyRepository();
                    }
                    else
                    {
                        ReadOnly = _readOnly;
                    }
                }
            }
            else
            {
                ReadOnly=_readOnly;
            }
        }

        public class ReadOnlyRepository
        {
            /// <summary>
            /// 只读的dbContext，只用于查询只读数据，使用缓存提高效率
            /// </summary>
            MyDbContext _readonlyDbContext { set; get; }

            public ReadOnlyRepository()
            {
                _readonlyDbContext = new MyDbContext();
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
            using (MyDbContext dbContext=new MyDbContext())
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
            using (MyDbContext dbContext = new MyDbContext())
            {
                foreach (var entity in entities)
                {
                    dbContext.Entry(entity).State=EntityState.Added;
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
            using (MyDbContext dbContext = new MyDbContext())
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
            using (MyDbContext dbContext = new MyDbContext())
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
            using (MyDbContext2 dbContext = new MyDbContext2())
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
            using (MyDbContext2 dbContext = new MyDbContext2())
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
            using (MyDbContext dbContext = new MyDbContext())
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
            using (MyDbContext dbContext = new MyDbContext())
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
            using (MyDbContext dbContext = new MyDbContext())
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
            using (MyDbContext dbContext = new MyDbContext())
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
            using (MyDbContext dbContext = new MyDbContext())
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
            using (MyDbContext dbContext = new MyDbContext())
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
    }
}