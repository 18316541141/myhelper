using CommonHelper.CommonEntity;
using CommonHelper.Helper;
using CommonHelper.Helper.EFDbContext;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CommonHelper.EFRepository
{
    /// <summary>
    /// 数据的逆反操作
    /// </summary>
    public abstract class InverseRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// 分布式插入数据的反操作。当事务失败时调用
        /// </summary>
        /// <param name="distributedTransactionPart">分表对象</param>
        public abstract void DistributedDeleteInverse(DistributedTransactionPart distributedTransactionPart);

        /// <summary>
        /// 分布式批量插入数据的反向操作。当事务失败时调用
        /// </summary>
        /// <param name="distributedTransactionParts">分表集合</param>
        public abstract void DistributedDeleteInverse(List<DistributedTransactionPart> distributedTransactionParts);

        /// <summary>
        /// 分布式更新的逆操作，当事务失败时调用
        /// </summary>
        /// <param name="entity">实体对象</param>
        public void DistributedUpdateInverse(DistributedTransactionPart distributedTransactionPart)
        {
            using (BaseDbContext dbContext = CreateDbContext())
            {
                TEntity entity = JsonConvert.DeserializeObject<TEntity>(CompressHelper.GZipDecompressString(distributedTransactionPart.InverseOper));
                dbContext.Entry(entity).State = EntityState.Modified;
                DistributedTransactionPart beforePart = new DistributedTransactionPart { Id = distributedTransactionPart.Id };
                dbContext.Set<DistributedTransactionPart>().Attach(beforePart);
                beforePart.TransactionStatus = 2;
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// 分布式批量全覆盖更新数据的逆操作，当事务失败时调用
        /// </summary>
        /// <param name="entity">实体对象</param>
        public void DistributedUpdateInverse(List<DistributedTransactionPart> distributedTransactionParts)
        {
            using (BaseDbContext dbContext = CreateDbContext())
            {
                foreach (var distributedTransactionPart in distributedTransactionParts)
                {
                    TEntity entity = JsonConvert.DeserializeObject<TEntity>(CompressHelper.GZipDecompressString(distributedTransactionPart.InverseOper));
                    dbContext.Entry(entity).State = EntityState.Modified;
                    DistributedTransactionPart beforePart = new DistributedTransactionPart { Id = distributedTransactionPart.Id };
                    dbContext.Set<DistributedTransactionPart>().Attach(beforePart);
                    beforePart.TransactionStatus = 2;
                }
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// 分布式删除数据的逆操作，当事务失败时调用
        /// </summary>
        /// <param name="entity">实体对象</param>
        public void DistributedInsertInverse(DistributedTransactionPart distributedTransactionPart)
        {
            using (BaseDbContext dbContext = CreateDbContext())
            {
                TEntity entity = JsonConvert.DeserializeObject<TEntity>(CompressHelper.GZipDecompressString(distributedTransactionPart.InverseOper));
                dbContext.Entry(entity).State = EntityState.Added;
                DistributedTransactionPart beforePart = new DistributedTransactionPart { Id = distributedTransactionPart.Id };
                dbContext.Set<DistributedTransactionPart>().Attach(beforePart);
                beforePart.TransactionStatus = 2;
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// 分布式删除批量数据的逆操作，当事务失败时调用
        /// </summary>
        /// <param name="entity">实体对象</param>
        public void DistributedInsertInverse(List<DistributedTransactionPart> distributedTransactionParts)
        {
            using (BaseDbContext dbContext = CreateDbContext())
            {
                foreach (DistributedTransactionPart distributedTransactionPart in distributedTransactionParts)
                {
                    TEntity entity = JsonConvert.DeserializeObject<TEntity>(CompressHelper.GZipDecompressString(distributedTransactionPart.InverseOper));
                    dbContext.Entry(entity).State = EntityState.Added;
                    DistributedTransactionPart beforePart = new DistributedTransactionPart { Id = distributedTransactionPart.Id };
                    dbContext.Set<DistributedTransactionPart>().Attach(beforePart);
                    beforePart.TransactionStatus = 2;
                }
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// 根据事务id，查询事务未确定或未取消的分表信息。
        /// </summary>
        /// <param name="transactionId">事务id</param>
        /// <returns></returns>
        public List<DistributedTransactionPart> FindTransPartsById(long? transactionId = null)
        {
            using (DbContext dbContext = CreateDbContext())
            {
                var query = dbContext.Set<DistributedTransactionPart>().AsNoTracking().AsQueryable().Where(a => a.TransactionStatus == 0);
                if (transactionId != null)
                {
                    query.Where(a => a.DistributedTransactionMainId == transactionId);
                }
                return query.Select(a => new DistributedTransactionPart { Id = a.Id, InverseOper = a.InverseOper, InverseOperType = a.InverseOperType, TransPrimaryKeyVal = a.TransPrimaryKeyVal })
                    .ToList();
            }
        }

        /// <summary>
        /// 更新事务分表状态
        /// </summary>
        /// <param name="id">事务分表的主键</param>
        /// <param name="status">更新状态</param>
        public void UpdateTransPartsStatus(long? id, byte status)
        {
            using (DbContext dbContext = CreateDbContext())
            {
                DistributedTransactionPart updateBefore = new DistributedTransactionPart { Id = id };
                dbContext.Set<DistributedTransactionPart>().Attach(updateBefore);
                updateBefore.TransactionStatus = status;
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// 更新事务分表状态
        /// </summary>
        /// <param name="idList">事务分表的主键列表</param>
        /// <param name="status">更新状态</param>
        public void UpdateTransPartsStatus(List<long?> idList, byte status)
        {
            using (DbContext dbContext = CreateDbContext())
            {
                foreach (long? id in idList)
                {
                    DistributedTransactionPart updateBefore = new DistributedTransactionPart { Id = id };
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
        public abstract BaseDbContext CreateDbContext();
    }
}
