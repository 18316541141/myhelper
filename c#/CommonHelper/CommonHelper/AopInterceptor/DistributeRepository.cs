using Castle.DynamicProxy;
using CommonHelper.CommonEntity;
using CommonHelper.Helper;
using CommonHelper.Helper.EFDbContext;
using CommonHelper.Helper.EFRepository;
using CommonHelper.staticVar;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CommonHelper.AopInterceptor
{
    /// <summary>
    /// 分布式数据管理类的属性
    /// </summary>
    public class DistributeRepository : IInterceptor
    {

        public void Intercept(IInvocation invocation)
        {
            string MethodName = invocation.Method.Name;
            dynamic target = invocation.InvocationTarget;
            if(invocation.Arguments[0] is IDistributedEntity)
            {
                if (MethodName == "Insert" && invocation.Arguments[0] is IDistributedEntity)
                {
                    IDistributedEntity entity = (IDistributedEntity)invocation.Arguments[0];
                    if (entity != null)
                    {
                        using (TransactionScope tx = new TransactionScope())
                        {
                            using (BaseDbContext baseDbContext = target.CreateDbContext())
                            {
                                baseDbContext.Entry(new DistributedTransactionPart
                                {
                                    Id = AllStatic.IdWorker.NextId(),
                                    DistributedTransactionMainId = (long)DistributedTransactionScan.TransactionIds.Value,
                                    InverseOper = Convert.ToString(entity.Key()),
                                    InverseOperType = 'd',
                                    TransTableName = entity.TableName(),
                                    TransPrimaryKeyVal = entity.Key(),
                                    TransactionStatus = 0,
                                    CreateDate = DateTime.Now,
                                }).State = EntityState.Added;
                                baseDbContext.SaveChanges();
                            }
                            invocation.Proceed();
                            tx.Complete();
                        }
                    }
                }
                else if (MethodName == "Insert" && invocation.Arguments[0] is IList)
                {
                    using (TransactionScope tx = new TransactionScope())
                    {
                        using (BaseDbContext baseDbContext = target.CreateDbContext())
                        {
                            foreach (IDistributedEntity entity in (IList)invocation.Arguments[0])
                            {
                                baseDbContext.Entry(new DistributedTransactionPart
                                {
                                    Id = AllStatic.IdWorker.NextId(),
                                    DistributedTransactionMainId = (long)DistributedTransactionScan.TransactionIds.Value,
                                    InverseOper = Convert.ToString(entity.Key()),
                                    InverseOperType = 'D',
                                    TransTableName = entity.TableName(),
                                    TransPrimaryKeyVal = entity.Key(),
                                    TransactionStatus = 0,
                                    CreateDate = DateTime.Now,
                                }).State = EntityState.Added;
                            }
                            baseDbContext.SaveChanges();
                        }
                        invocation.Proceed();
                        tx.Complete();
                    }
                }
                else if (MethodName == "Delete" && invocation.Arguments[0] is IDistributedEntity)
                {
                    using (TransactionScope tx = new TransactionScope())
                    {
                        using (BaseDbContext baseDbContext = target.CreateDbContext())
                        {
                            IDistributedEntity entity = (IDistributedEntity)invocation.Arguments[0];
                            var beforeData = target.FindEntity(entity.Key());
                            baseDbContext.Entry(new DistributedTransactionPart
                            {
                                Id = AllStatic.IdWorker.NextId(),
                                DistributedTransactionMainId = (long)DistributedTransactionScan.TransactionIds.Value,
                                InverseOper = CompressHelper.GZipCompressString(JsonConvert.SerializeObject(beforeData, AllStatic.TimeConverter)),
                                InverseOperType = 'i',
                                TransTableName = entity.TableName(),
                                TransPrimaryKeyVal = entity.Key(),
                                TransactionStatus = 0,
                                CreateDate = DateTime.Now,
                            }).State = EntityState.Added;
                            baseDbContext.SaveChanges();
                        }
                        invocation.Proceed();
                        tx.Complete();
                    }
                }
                else if (MethodName == "Delete" && invocation.Arguments[0] is IList)
                {
                    using (TransactionScope tx = new TransactionScope())
                    {
                        using (BaseDbContext baseDbContext = target.CreateDbContext())
                        {
                            foreach (IDistributedEntity entity in (IList)invocation.Arguments[0])
                            {
                                var beforeData = target.FindEntity(entity.Key());
                                baseDbContext.Entry(new DistributedTransactionPart
                                {
                                    Id = AllStatic.IdWorker.NextId(),
                                    DistributedTransactionMainId = (long)DistributedTransactionScan.TransactionIds.Value,
                                    InverseOper = CompressHelper.GZipCompressString(JsonConvert.SerializeObject(beforeData, AllStatic.TimeConverter)),
                                    InverseOperType = 'D',
                                    TransTableName = entity.TableName(),
                                    TransPrimaryKeyVal = entity.Key(),
                                    TransactionStatus = 0,
                                    CreateDate = DateTime.Now,
                                }).State = EntityState.Added;
                            }
                            baseDbContext.SaveChanges();
                        }
                        invocation.Proceed();
                        tx.Complete();
                    }
                }
                else if(MethodName == "UpdateAll" && invocation.Arguments[0] is IDistributedEntity)
                {
                    IDistributedEntity entity = (IDistributedEntity)invocation.Arguments[0];
                    if (entity != null)
                    {
                        using (TransactionScope tx = new TransactionScope())
                        {
                            using (BaseDbContext baseDbContext = target.CreateDbContext())
                            {
                                var beforeData = target.FindEntity(entity.Key());
                                if (beforeData == null)
                                {
                                    return;
                                }
                                baseDbContext.Entry(new DistributedTransactionPart
                                {
                                    Id = AllStatic.IdWorker.NextId(),
                                    DistributedTransactionMainId = (long)DistributedTransactionScan.TransactionIds.Value,
                                    InverseOper = CompressHelper.GZipCompressString(JsonConvert.SerializeObject(beforeData, AllStatic.TimeConverter)),
                                    InverseOperType = 'u',
                                    TransTableName = entity.TableName(),
                                    TransPrimaryKeyVal = entity.Key(),
                                    TransactionStatus = 0,
                                    CreateDate = DateTime.Now,
                                }).State = EntityState.Added;
                                baseDbContext.SaveChanges();
                            }
                            invocation.Proceed();
                            tx.Complete();
                        }
                    }
                }
                else if (MethodName == "UpdateAll" && invocation.Arguments[0] is IList)
                {
                    using (TransactionScope tx = new TransactionScope())
                    {
                        using (BaseDbContext baseDbContext = target.CreateDbContext())
                        {
                            foreach (IDistributedEntity entity in (IList)invocation.Arguments[0])
                            {
                                var beforeData = target.FindEntity(entity.Key());
                                if (beforeData == null)
                                {
                                    continue;
                                }
                                baseDbContext.Entry(new DistributedTransactionPart
                                {
                                    Id = AllStatic.IdWorker.NextId(),
                                    DistributedTransactionMainId = (long)DistributedTransactionScan.TransactionIds.Value,
                                    InverseOper = CompressHelper.GZipCompressString(JsonConvert.SerializeObject(beforeData, AllStatic.TimeConverter)),
                                    InverseOperType = 'U',
                                    TransTableName = entity.TableName(),
                                    TransPrimaryKeyVal = entity.Key(),
                                    TransactionStatus = 0,
                                    CreateDate = DateTime.Now,
                                }).State = EntityState.Added;
                            }
                            baseDbContext.SaveChanges();
                        }
                        invocation.Proceed();
                        tx.Complete();
                    }
                }
                else if (MethodName == "UpdateChange" && invocation.Arguments[0] is IDistributedEntity)
                {
                    IDistributedEntity entity = (IDistributedEntity)invocation.Arguments[0];
                    var beforeData = target.FindEntity(entity.Key());
                    if (beforeData != null)
                    {
                        using (TransactionScope tx = new TransactionScope())
                        {
                            using (BaseDbContext baseDbContext = target.CreateDbContext())
                            {

                                baseDbContext.SaveChanges();
                            }
                            invocation.Proceed();
                            tx.Complete();
                        }
                    }
                }
            }
        }
    }
}
