using CommonHelper.Helper.EFRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication1.Entity;
using CommonHelper.Helper.EFDbContext;
using WebApplication1.AopInterceptor;
using CommonHelper.CommonEntity;
using Snowflake.Net;

namespace WebApplication1.Repository.codeGenerator
{
    /// <summary>
    /// 分布式的
    /// </summary>
    public class DistributedIRobotQrCodePayTaskRepository
    {
        /// <summary>
        /// 分布式雪花id生成器
        /// </summary>
        public IdWorker IdWorker { set; get; }

        /// <summary>
        /// 插入一条数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>返回插入的数据数量</returns>
        public int Insert(IRobotQrCodePayTask irobotQrCodePayTask)
        {
            if (DistributedTransactionScan.TransactionIds.Value == null)
            {
                throw new Exception("方法调用失败，失败原因：未开启分布式事务。");
            }
            using (DistributedPartDbContext dbContext = new DistributedPartDbContext())
            {
                DistributedTransactionScan.TransactionDataSources.Value.Add(dbContext.TransactionDataSource);
                dbContext.Entry(irobotQrCodePayTask).State = EntityState.Added;
                dbContext.Entry(new DistributedTransactionPart
                {
                    Id = IdWorker.NextId(),
                    DistributedTransactionMainId = (long)DistributedTransactionScan.TransactionIds.Value,
                    InverseOper = "DELETE FROM TABLE_NAME WHERE PRIMARY_KEY=PRIMARY_KEY_VALUE",
                    TransTableName = "TABLE_NAME",
                    TransPrimaryKey = "PRIMARY_KEY",
                    TransPrimaryKeyVal= 10086,
                    TransactionStatus = 0,
                    CreateDate = DateTime.Now,
                }).State = EntityState.Added;
                return dbContext.SaveChanges();
            }
        }

        public void Delete(long key)
        {
            if (DistributedTransactionScan.TransactionIds.Value == null)
            {
                throw new Exception("方法调用失败，失败原因：未开启分布式事务。");
            }
            using (DistributedPartDbContext dbContext = new DistributedPartDbContext())
            {
                DistributedTransactionScan.TransactionDataSources.Value.Add(dbContext.TransactionDataSource);

                //dbContext.Entry();
            }
        }
    }
}