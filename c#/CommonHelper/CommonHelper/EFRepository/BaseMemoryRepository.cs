using Snowflake.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Entity.Common;

namespace CommonHelper.EFRepository
{
    /// <summary>
    /// 通用的内存数据操作基类。
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TParams"></typeparam>
    /// <typeparam name="TSetNullParams"></typeparam>
    public abstract class BaseMemoryRepository<TEntity, TParams, TSetNullParams> where TEntity : class where TParams : class where TSetNullParams : class
    {
        /// <summary>
        /// 单例检测，确保每种类型的内存数据操作类只有一个
        /// </summary>
        static HashSet<string> SingleCheck;

        static BaseMemoryRepository()
        {
            SingleCheck = new HashSet<string>();
        }

        /// <summary>
        /// 内存保存的数据，注意：实体类必须是实现了IEquatable，并使用id作为等值判断的依据
        /// </summary>
        protected List<IEquatable<TEntity>> DataList { set; get; }

        public BaseMemoryRepository()
        {
            string name = typeof(TEntity).FullName;
            lock (SingleCheck)
            {
                if (SingleCheck.Contains(name))
                {
                    throw new Exception($"{name} 类型的内存操作类已创建过，请勿重复创建！");
                }
                else
                {
                    SingleCheck.Add(name);
                    DataList = new List<IEquatable<TEntity>>();
                }
            }
        }

        /// <summary>
        /// 插入一条数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>返回插入的数据数量</returns>
        public void Insert(IEquatable<TEntity> entity)
        {
            lock (DataList)
            {
                if (DataList.Contains(entity))
                {
                    throw new Exception($"{entity.GetType().Name}内存表中已存在相同数据，插入失败！");
                }
                DataList.Add(entity);
            }
        }

        /// <summary>
        /// 根据主键删除一个对象
        /// </summary>
        /// <param name="entity">对象实体</param>
        /// <returns></returns>
        public void Delete(IEquatable<TEntity> entity)
        {
            lock (DataList)
            {
                DataList.Remove(entity);
            }
        }

        /// <summary>
        /// 删除一批对象
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public void Delete(List<IEquatable<TEntity>> entities)
        {
            lock (DataList)
            {
                foreach (IEquatable<TEntity> entity in entities)
                {
                    DataList.Remove(entity);
                }
            }
        }

        /// <summary>
        /// 根据条件删除对象
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns></returns>
        public void Delete(Func<IEquatable<TEntity>, bool> predicate)
        {
            lock (DataList)
            {
                Delete(DataList.Where(predicate).ToList());
            }
        }

        /// <summary>
        /// 根据主键查找一个实体
        /// </summary>
        /// <param name="entity">含有主键的实体</param>
        /// <returns></returns>
        public IEquatable<TEntity> FindEntity(IEquatable<TEntity> entity)
        {
            lock (DataList)
            {
                int tempIndex = DataList.IndexOf(entity);
                return tempIndex == -1 ? null : DataList[tempIndex];
            }
        }

        /// <summary>
        /// 根据条件查找一个实体
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns></returns>
        public IEquatable<TEntity> FindEntity(Func<IEquatable<TEntity>, bool> predicate)
        {
            lock (DataList)
            {
                List<IEquatable<TEntity>> list = DataList.Where(predicate).ToList();
                return list.Count == 0 ? null : list[0];
            }
        }

        /// <summary>
        /// 根据条件查询是否有匹配的数据
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns></returns>
        public bool Exist(Func<IEquatable<TEntity>, bool> predicate)
        {
            lock (DataList)
            {
                return DataList.Where(predicate).Count() > 0;
            }
        }

        /// <summary>
        /// 修改所有字段，注意：为null的也会在数据库上写成null
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual void UpdateAll(IEquatable<TEntity> entity)
        {
            lock (DataList)
            {
                int tempIndex = DataList.IndexOf(entity);
                if (tempIndex > -1)
                {
                    DataList[tempIndex] = entity;
                }
            }
        }

        /// <summary>
        /// 批量修改所有字段，注意：为null的也会在数据库上写成null
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual void UpdateAll(List<IEquatable<TEntity>> entities)
        {
            lock (DataList)
            {
                foreach (IEquatable<TEntity> entity in entities)
                {
                    UpdateAll(entity);
                }
            }
        }

        /// <summary>
        /// 根据条件查询符合条件的数据量
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public int Count(Func<IEquatable<TEntity>, bool> predicate)
        {
            lock (DataList)
            {
                return DataList.Where(predicate).Count();
            }
        }

        /// <summary>
        /// 根据条件查询列表
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public List<IEquatable<TEntity>> FindList(Func<IEquatable<TEntity>, bool> predicate = null)
        {
            lock (DataList)
            {
                return DataList.Where(predicate).ToList();
            }
        }

        /// <summary>
        /// 设置查询时的筛选条件
        /// </summary>
        /// <param name="entity">查询对象</param>
        /// <param name="paramz">查询参数</param>
        /// <param name="neqArgs">不等查询参数</param>
        /// <param name="baseVal">基准值，仅在跨库分页查询时使用</param>
        /// <returns>返回true表示符合条件，返回false，表示不符合条件</returns>
        protected abstract bool Query(IEquatable<TEntity> entity, TParams paramz, TParams neqArgs);

        /// <summary>
        /// 查询符合查询条件的数据量
        /// </summary>
        /// <param name="eqArgs">查询参数，不为null时会作为查询参数</param>
        /// <param name="neqArgs">不等查询参数，不为null时会作为不等查询参数</param>
        /// <returns>返回符合查询条件的数据量</returns>
        public int Count(TParams eqArgs = null, TParams neqArgs = null)
        {
            int count = 0;
            lock (DataList)
            {
                foreach (IEquatable<TEntity> entity in DataList)
                {
                    if (Query(entity, eqArgs, neqArgs))
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        /// <summary>
        /// 查询符合查询条件的数据，数据不宜过大，如果数据过大建议使用分页查询
        /// </summary>
        /// <param name="eqArgs">查询参数，不为null时会作为查询参数</param>
        /// <param name="neqArgs">不等查询参数，不为null时会作为不等查询参数</param>
        /// <returns>返回符合查询条件的数</returns>
        public List<IEquatable<TEntity>> FindList(TParams eqArgs = null, TParams neqArgs = null)
        {
            List<IEquatable<TEntity>> ret = new List<IEquatable<TEntity>>();
            lock (DataList)
            {
                foreach (IEquatable<TEntity> entity in DataList)
                {
                    if (Query(entity, eqArgs, neqArgs))
                    {
                        ret.Add(entity);
                    }
                }
            }
            return ret;
        }

        /// 普通的分页查询功能，pageSize不宜过大，如果pageSize大于1000，使用：BigPageList
        /// </summary>
        /// <param name="eqArgs">查询参数，不为null时会作为查询参数</param>
        /// <param name="pageIndex">查询页码</param>
        /// <param name="pageSize">每页显示数据量</param>
        /// <param name="neqArgs">不等查询参数，不为null时会作为不等查询参数</param>
        /// <returns>返回分页查询结果</returns>
        public MyPagedList<IEquatable<TEntity>> PageList(TParams eqArgs, int pageIndex = 1, int pageSize = 20, TParams neqArgs = null)
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
            List<IEquatable<TEntity>> ret = new List<IEquatable<TEntity>>(20);
            int totalCount = 0;
            lock (DataList)
            {
                for (int i = 0, totalSkip = (pageIndex - 1) * pageSize, skipCount = 0, takeCount = 0, len = DataList.Count; i < len; i++)
                {
                    if (Query(DataList[i], eqArgs, neqArgs) && totalSkip == skipCount && takeCount < pageSize)
                    {
                        ret.Add(DataList[i]);
                        takeCount++;
                    }
                    else
                    {
                        skipCount++;
                    }
                    totalCount++;
                }
            }
            return new MyPagedList<IEquatable<TEntity>>
            {
                PageDataList = ret,
                TotalItemCount = totalCount,
                TotalPageCount = (totalCount - totalCount % pageSize) / pageSize + 1,
                PageSize = pageSize,
                CurrentPageIndex = pageIndex,
                StartItemIndex = (pageSize - 1) * pageIndex + 1,
                EndItemIndex = Math.Min(pageSize * pageIndex,totalCount)
            };
        }

        /// <summary>
        /// 修改变化值，把entity不为null的数据视为变化值
        /// </summary>
        /// <param name="entity">修改实体类，必须要传入主键，其余的参数如果不为null则视为变化值（主键id除外，只做数据标识）</param>
        /// <returns>返回修改的数据行数</returns>
        public void UpdateChange(IEquatable<TEntity> entity)
        {
            lock (DataList)
            {
                int tempIndex = DataList.IndexOf(entity);
                if (tempIndex > -1)
                {
                    UpdateChange(DataList[tempIndex], entity);
                }
            }
        }

        /// <summary>
        /// 修改变化值的部分共用代码，这里抽取出来简化结构
        /// </summary>
        /// <param name="src">修改前的数据</param>
        /// <param name="entity">需要修改的数据</param>
        protected abstract void UpdateChange(IEquatable<TEntity> src, IEquatable<TEntity> change);

        /// <summary>
        /// 把指定字段值设置为null
        /// </summary>
        /// <param name="equal">等值依据</param>
        /// <param name="param">设为null的字段参数，主键必须要传入，其他字段等于true则表示设置为null</param>
        /// <returns>返回修改的数据行数</returns>
        public void SetNull(IEquatable<TEntity> equal, TSetNullParams param)
        {
            lock (DataList)
            {
                int tempIndex = DataList.IndexOf(equal);
                if (tempIndex > -1)
                {
                    SetNullOper(DataList[tempIndex], param);
                }
            }
        }

        /// <summary>
        /// 把指定字段值设置为null的部分共用代码，这里抽取出来简化结构
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="param"></param>
        protected abstract void SetNullOper(IEquatable<TEntity> entity, TSetNullParams param);
    }
}
