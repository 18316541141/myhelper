using System;
using CommonHelper.CommonEntity;
using CommonHelper.EFRepository;
using CommonHelper.Helper;
using CommonHelper.Helper.EFDbContext;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication1.Entity;
using WebApplication1.Params;
namespace WebApplication1.Repository
{
    public sealed partial class LogEntityInverseRepository : InverseRepository<LogEntity>
    {
		/// <summary>
        /// 分布式插入数据的反操作。当事务失败时调用
        /// </summary>
        /// <param name="entity">实体对象</param>
		public override void DistributedDeleteInverse(DistributedTransactionPart distributedTransactionPart)
		{
			using (BaseDbContext dbContext = CreateDbContext())
            {
				DistributedTransactionPart updateBefore=new DistributedTransactionPart{ Id = distributedTransactionPart.Id };
				dbContext.Entry(new LogEntity{ Id = distributedTransactionPart.TransPrimaryKeyVal }).State = EntityState.Deleted;
				dbContext.Set<DistributedTransactionPart>().Attach(updateBefore);
				updateBefore.TransactionStatus = 2;
				dbContext.SaveChanges();
			}
		}

		/// <summary>
        /// 分布式批量更新数据的反向操作。当事务失败时调用
        /// </summary>
        /// <param name="entity">实体对象</param>
		public override void DistributedDeleteInverse(List<DistributedTransactionPart> distributedTransactionParts)
		{
			using (BaseDbContext dbContext = CreateDbContext())
            {
				foreach(var distributedTransactionPart in distributedTransactionParts)
				{
					DistributedTransactionPart updateBefore=new DistributedTransactionPart{ Id = distributedTransactionPart.Id };
					dbContext.Entry(new LogEntity{ Id = distributedTransactionPart.TransPrimaryKeyVal }).State = EntityState.Deleted;
					dbContext.Set<DistributedTransactionPart>().Attach(updateBefore);
					updateBefore.TransactionStatus = 2;
				}
				dbContext.SaveChanges();
			}
		}

		public override BaseDbContext CreateDbContext()
        {
            return new MyDbContext2();
        }
    }
}