using Castle.DynamicProxy;
using CommonHelper.CommonEntity;
using CommonHelper.Helper.EFDbContext;
using Snowflake.Net;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.AopInterceptor
{
    /// <summary>
    /// 分布式事务拦截器
    /// </summary>
    public class DistributedTransactionScan : IInterceptor
    {
        /// <summary>
        /// 事务id的值
        /// </summary>
        public static ThreadLocal<long?> TransactionIds { set; get; }

        /// <summary>
        /// 事务的数据源，用于保存和该事务关联的数据源
        /// </summary>
        public static ThreadLocal<HashSet<string>> TransactionDataSources { set; get; }

        /// <summary>
        /// 进入次数，用于确定方法调用方法导致事务嵌套的深度。
        /// </summary>
        public static ThreadLocal<int> EnterTimes { set; get; }

        static DistributedTransactionScan()
        {
            TransactionDataSources = new ThreadLocal<HashSet<string>>(()=> new HashSet<string>());
            TransactionIds = new ThreadLocal<long?>(()=>null);
            EnterTimes = new ThreadLocal<int>(()=>0);
        }

        /// <summary>
        /// 保存事务总表数据
        /// </summary>
        /// <param name="transactionStatus"></param>
        private void SaveDistributedTransaction(sbyte transactionStatus)
        {
            using (DistributedMainDbContext db = new DistributedMainDbContext())
            {
                db.Entry(new DistributedTransactionMain
                {
                    Id = (long)TransactionIds.Value,
                    TransactionStatus = transactionStatus
                }).State = EntityState.Added;
                foreach (string transactionDataSource in TransactionDataSources.Value)
                {
                    db.Entry(new DistributedTransactionMainDetail
                    {
                        DistributedTransactionMainId = (long)TransactionIds.Value,
                        TransactionDataSource = transactionDataSource,
                    }).State = EntityState.Added;
                }
                db.SaveChanges();
            }
        }

        public void Intercept(IInvocation invocation)
        {
            if (invocation.Method.GetCustomAttributes(typeof(DistributedTransactionScope), true).Length > 0)
            {
                if (TransactionIds.Value == null)
                {
                    TransactionIds.Value= DependencyResolver.Current.GetService<IdWorker>().NextId();
                }
                EnterTimes.Value++;
                try
                {
                    invocation.Proceed();
                    if (--EnterTimes.Value == 0)
                    {
                        //保存全局事务id，
                        SaveDistributedTransaction(1);
                        //并通知消息队列确认事务
                        try
                        {

                        }
                        catch
                        {

                        }
                        TransactionDataSources.Value.Clear();
                        TransactionIds.Value = null;
                    }
                }
                catch (Exception ex)
                {
                    if (--EnterTimes.Value == 0)
                    {
                        try
                        {
                            //保存全局事务id，
                            SaveDistributedTransaction(2);
                            //通知消息队列取消事务
                        }
                        catch
                        {

                        }
                        TransactionDataSources.Value.Clear();
                        TransactionIds.Value = null;
                    }
                }
            }
            else
            {
                invocation.Proceed();
            }
        }
    }
}