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
    public sealed class DistributeRepository : IInterceptor
    {
        static HashSet<string> MethodNames { set; get; }
        static DistributeRepository()
        {
            MethodNames = new HashSet<string>();
            MethodNames.Add("Insert");
            MethodNames.Add("Delete");
            MethodNames.Add("UpdateAll");
            MethodNames.Add("UpdateChange");
            MethodNames.Add("SetNull");
        }


        /// <summary>
        /// 记录分布式事务操作时涉及到的表和数据源
        /// </summary>
        /// <param name="dataSrc">记录的数据源</param>
        /// <param name="tableName">记录的表</param>
        void RecordDistribute(string dataSrc, string tableName)
        {
            var dataSrcMap = DistributedTransactionScan.TransactionDataSources.Value;
            if (dataSrcMap.ContainsKey(dataSrc))
            {
                if (!dataSrcMap[dataSrc].Contains(tableName))
                {
                    dataSrcMap[dataSrc].Add(tableName);
                }
            }
            else
            {
                HashSet<string> tableNameSet = new HashSet<string>();
                dataSrcMap.Add(dataSrc, tableNameSet);
                tableNameSet.Add(tableName);
            }
        }

        /// <summary>
        /// 分布式数据操作拦截，会对更新、删除、插入数据进行记录，
        /// 用于异步确定、异步回滚
        /// </summary>
        /// <param name="invocation"></param>
        public void Intercept(IInvocation invocation)
        {
            string MethodName = invocation.Method.Name;
            long? transId = DistributedTransactionScan.TransactionIds.Value;
            if (transId != null && MethodNames.Contains(MethodName))
            {
                dynamic target = invocation.InvocationTarget;
                using (TransactionScope tx = new TransactionScope())
                {
                    using (BaseDbContext baseDbContext = target.CreateDbContext())
                    {
                        if (invocation.Arguments[0] is IEntity)
                        {
                            IEntity entity = (IEntity)invocation.Arguments[0];
                            if (entity != null)
                            {
                                DistributedTransactionPart distributedTransactionPart = new DistributedTransactionPart
                                {
                                    Id = AllStatic.IdWorker.NextId(),
                                    DistributedTransactionMainId = (long)DistributedTransactionScan.TransactionIds.Value,
                                    TransTableName = entity.TableName(),
                                    TransPrimaryKeyVal = entity.Key,
                                    TransactionStatus = 0,
                                    CreateDate = DateTime.Now,
                                };
                                if (MethodName == "Insert")
                                {
                                    RecordDistribute(baseDbContext.Database.Connection.ConnectionString, entity.TableName());
                                    distributedTransactionPart.InverseOperType = "d";
                                    baseDbContext.Entry(distributedTransactionPart).State = EntityState.Added;
                                }
                                else if (MethodName == "Delete")
                                {
                                    var beforeData = target.FindEntity(entity.Key);
                                    if (beforeData != null)
                                    {
                                        RecordDistribute(baseDbContext.Database.Connection.ConnectionString, entity.TableName());
                                        distributedTransactionPart.InverseOper = CompressHelper.GZipCompressString(JsonConvert.SerializeObject(beforeData, AllStatic.TimeConverter));
                                        distributedTransactionPart.InverseOperType = "i";
                                        baseDbContext.Entry(distributedTransactionPart).State = EntityState.Added;
                                    }
                                }
                                else if (MethodName == "UpdateAll" || MethodName == "UpdateChange")
                                {
                                    var beforeData = target.FindEntity(entity.Key);
                                    if (beforeData != null)
                                    {
                                        RecordDistribute(baseDbContext.Database.Connection.ConnectionString, entity.TableName());
                                        distributedTransactionPart.InverseOper = CompressHelper.GZipCompressString(JsonConvert.SerializeObject(beforeData, AllStatic.TimeConverter));
                                        distributedTransactionPart.InverseOperType = "u";
                                        baseDbContext.Entry(distributedTransactionPart).State = EntityState.Added;
                                    }
                                }
                            }
                        }
                        else if (invocation.Arguments[0] is IList)
                        {
                            foreach (IEntity entity in (IList)invocation.Arguments[0])
                            {
                                if (entity != null)
                                {
                                    DistributedTransactionPart distributedTransactionPart = new DistributedTransactionPart
                                    {
                                        Id = AllStatic.IdWorker.NextId(),
                                        DistributedTransactionMainId = (long)DistributedTransactionScan.TransactionIds.Value,
                                        TransTableName = entity.TableName(),
                                        TransPrimaryKeyVal = entity.Key,
                                        TransactionStatus = 0,
                                        CreateDate = DateTime.Now,
                                    };
                                    if (MethodName == "Insert")
                                    {
                                        RecordDistribute(baseDbContext.Database.Connection.ConnectionString, entity.TableName());
                                        distributedTransactionPart.InverseOper = Convert.ToString(entity.Key);
                                        distributedTransactionPart.InverseOperType = "D";
                                        baseDbContext.Entry(distributedTransactionPart).State = EntityState.Added;
                                    }
                                    else if (MethodName == "Delete")
                                    {
                                        var beforeData = target.FindEntity(entity.Key);
                                        if (beforeData != null)
                                        {
                                            RecordDistribute(baseDbContext.Database.Connection.ConnectionString, entity.TableName());
                                            distributedTransactionPart.InverseOper = CompressHelper.GZipCompressString(JsonConvert.SerializeObject(beforeData, AllStatic.TimeConverter));
                                            distributedTransactionPart.InverseOperType = "I";
                                            baseDbContext.Entry(distributedTransactionPart).State = EntityState.Added;
                                        }
                                    }
                                    else if (MethodName == "UpdateAll" || MethodName == "UpdateChange")
                                    {
                                        var beforeData = target.FindEntity(entity.Key);
                                        if (beforeData != null)
                                        {
                                            RecordDistribute(baseDbContext.Database.Connection.ConnectionString, entity.TableName());
                                            distributedTransactionPart.InverseOper = CompressHelper.GZipCompressString(JsonConvert.SerializeObject(beforeData, AllStatic.TimeConverter));
                                            distributedTransactionPart.InverseOperType = "U";
                                            baseDbContext.Entry(distributedTransactionPart).State = EntityState.Added;
                                        }
                                    }
                                }
                            }
                        }
                        baseDbContext.SaveChanges();
                    }
                    invocation.Proceed();
                    tx.Complete();
                }
            }
            else
            {
                invocation.Proceed();
            }
        }
    }
}