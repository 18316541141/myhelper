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
using WebApplication1.MyExtensions;
using WebApplication1.Params;
namespace WebApplication1.Repository
{
    public partial class IRobotRobotManagerInverseRepository : InverseRepository
    {
		/// <summary>
        /// 分布式插入数据的反操作。当事务失败时调用
        /// </summary>
        /// <param name="entity">实体对象</param>
		public override void DistributedInsertInverse(DistributedTransactionPart distributedTransactionPart)
		{
			using (BaseDbContext dbContext = CreateDbContext())
            {
				DistributedTransactionPart updateBefore=new DistributedTransactionPart{ Id = distributedTransactionPart.Id };
				dbContext.Entry(new IRobotRobotManager{ IRMID = distributedTransactionPart.TransPrimaryKeyVal }).State = EntityState.Deleted;
				dbContext.Set<DistributedTransactionPart>().Attach(updateBefore);
				updateBefore.TransactionStatus = 2;
				dbContext.SaveChanges();
			}
		}

		/// <summary>
        /// 分布式批量插入数据的反向操作。当事务失败时调用
        /// </summary>
        /// <param name="entity">实体对象</param>
		public override void DistributedInsertInverse(List<DistributedTransactionPart> distributedTransactionParts)
		{
			using (BaseDbContext dbContext = CreateDbContext())
            {
				foreach(var distributedTransactionPart in distributedTransactionParts)
				{
					DistributedTransactionPart updateBefore=new DistributedTransactionPart{ Id = distributedTransactionPart.Id };
					dbContext.Entry(new IRobotRobotManager{ IRMID = distributedTransactionPart.TransPrimaryKeyVal }).State = EntityState.Deleted;
					dbContext.Set<DistributedTransactionPart>().Attach(updateBefore);
					updateBefore.TransactionStatus = 2;
				}
				dbContext.SaveChanges();
			}
		}

		/// <summary>
        /// 分布式setNull的逆操作，当事务失败时调用
        /// </summary>
        /// <param name="entity">实体对象</param>
		public override void DistributedSetNullInverse(DistributedTransactionPart distributedTransactionPart)
		{
			using (BaseDbContext dbContext = CreateDbContext())
            {
				IRobotRobotManager updateBefore = new IRobotRobotManager { IRMID = distributedTransactionPart.TransPrimaryKeyVal };
				dbContext.Set<IRobotRobotManager>().Attach(updateBefore);
				IRobotRobotManager entity =JsonConvert.DeserializeObject<IRobotRobotManager>(CompressHelper.GZipDecompressString(distributedTransactionPart.InverseOper));
								if (entity.IRMRobotId!=null)
								{
									updateBefore.IRMRobotId = entity.IRMRobotId;
								}				if (entity.IRMRobotState!=null)
								{
									updateBefore.IRMRobotState = entity.IRMRobotState;
								}				if (entity.IRMRuningTime!=null)
								{
									updateBefore.IRMRuningTime = entity.IRMRuningTime;
								}				if (entity.IRMMaxPayTradeCount!=null)
								{
									updateBefore.IRMMaxPayTradeCount = entity.IRMMaxPayTradeCount;
								}				if (entity.IRMCurrentPayCount!=null)
								{
									updateBefore.IRMCurrentPayCount = entity.IRMCurrentPayCount;
								}				if (entity.IRMBalance!=null)
								{
									updateBefore.IRMBalance = entity.IRMBalance;
								}				if (entity.IRMIP!=null)
								{
									updateBefore.IRMIP = entity.IRMIP;
								}				if (entity.IRMCreateTime!=null)
								{
									updateBefore.IRMCreateTime = entity.IRMCreateTime;
								}				if (entity.IRMTeamViewId!=null)
								{
									updateBefore.IRMTeamViewId = entity.IRMTeamViewId;
								}				if (entity.IRMTeamViewPassword!=null)
								{
									updateBefore.IRMTeamViewPassword = entity.IRMTeamViewPassword;
								}				if (entity.IRMSunflowerId!=null)
								{
									updateBefore.IRMSunflowerId = entity.IRMSunflowerId;
								}				if (entity.IRMSunflowerPassword!=null)
								{
									updateBefore.IRMSunflowerPassword = entity.IRMSunflowerPassword;
								}				if (entity.IRMBankCardTailNum!=null)
								{
									updateBefore.IRMBankCardTailNum = entity.IRMBankCardTailNum;
								}				if (entity.IRMBankCardName!=null)
								{
									updateBefore.IRMBankCardName = entity.IRMBankCardName;
								}				if (entity.IRMMinBalance!=null)
								{
									updateBefore.IRMMinBalance = entity.IRMMinBalance;
								}				if (entity.IRMPayPassword!=null)
								{
									updateBefore.IRMPayPassword = entity.IRMPayPassword;
								}				if (entity.IRMScanPayNotifyUrl!=null)
								{
									updateBefore.IRMScanPayNotifyUrl = entity.IRMScanPayNotifyUrl;
								}				if (entity.IRMWxUsername!=null)
								{
									updateBefore.IRMWxUsername = entity.IRMWxUsername;
								}				if (entity.IRMWxPassword!=null)
								{
									updateBefore.IRMWxPassword = entity.IRMWxPassword;
								}				if (entity.IRMRefreshBalance!=null)
								{
									updateBefore.IRMRefreshBalance = entity.IRMRefreshBalance;
								}
				DistributedTransactionPart beforePart = new DistributedTransactionPart{ Id = distributedTransactionPart.Id };
				dbContext.Set<DistributedTransactionPart>().Attach(beforePart);
				beforePart.TransactionStatus = 2;
				dbContext.SaveChanges();
			}
		}

		/// <summary>
        /// 分布式部分更新数据的逆操作，当事务失败时调用
        /// </summary>
        /// <param name="entity">实体对象</param>
		public override void DistributedUpdateChangeInverse(DistributedTransactionPart distributedTransactionPart)
		{
			DistributedSetNullInverse(entity);
		}

		/// <summary>
        /// 分布式全覆盖更新数据的逆操作，当事务失败时调用
        /// </summary>
        /// <param name="entity">实体对象</param>
		public override void DistributedUpdateAllInverse(DistributedTransactionPart distributedTransactionPart)
		{
			DistributedSetNullInverse(entity);
		}

		/// <summary>
        /// 分布式批量全覆盖更新数据的逆操作，当事务失败时调用
        /// </summary>
        /// <param name="entity">实体对象</param>
		public override void DistributedUpdateAllBatchInverse(List<DistributedTransactionPart> distributedTransactionParts)
		{
			using (BaseDbContext dbContext = CreateDbContext())
            {
				foreach(var distributedTransactionPart in distributedTransactionParts)
				{
					IRobotRobotManager updateBefore = new IRobotRobotManager { IRMID = distributedTransactionPart.TransPrimaryKeyVal };
					dbContext.Set<IRobotRobotManager>().Attach(updateBefore);
					IRobotRobotManager entity =JsonConvert.DeserializeObject<IRobotRobotManager>(CompressHelper.GZipDecompressString(distributedTransactionPart.InverseOper));
										if (entity.IRMRobotId!=null)
										{
											updateBefore.IRMRobotId = entity.IRMRobotId;
										}					if (entity.IRMRobotState!=null)
										{
											updateBefore.IRMRobotState = entity.IRMRobotState;
										}					if (entity.IRMRuningTime!=null)
										{
											updateBefore.IRMRuningTime = entity.IRMRuningTime;
										}					if (entity.IRMMaxPayTradeCount!=null)
										{
											updateBefore.IRMMaxPayTradeCount = entity.IRMMaxPayTradeCount;
										}					if (entity.IRMCurrentPayCount!=null)
										{
											updateBefore.IRMCurrentPayCount = entity.IRMCurrentPayCount;
										}					if (entity.IRMBalance!=null)
										{
											updateBefore.IRMBalance = entity.IRMBalance;
										}					if (entity.IRMIP!=null)
										{
											updateBefore.IRMIP = entity.IRMIP;
										}					if (entity.IRMCreateTime!=null)
										{
											updateBefore.IRMCreateTime = entity.IRMCreateTime;
										}					if (entity.IRMTeamViewId!=null)
										{
											updateBefore.IRMTeamViewId = entity.IRMTeamViewId;
										}					if (entity.IRMTeamViewPassword!=null)
										{
											updateBefore.IRMTeamViewPassword = entity.IRMTeamViewPassword;
										}					if (entity.IRMSunflowerId!=null)
										{
											updateBefore.IRMSunflowerId = entity.IRMSunflowerId;
										}					if (entity.IRMSunflowerPassword!=null)
										{
											updateBefore.IRMSunflowerPassword = entity.IRMSunflowerPassword;
										}					if (entity.IRMBankCardTailNum!=null)
										{
											updateBefore.IRMBankCardTailNum = entity.IRMBankCardTailNum;
										}					if (entity.IRMBankCardName!=null)
										{
											updateBefore.IRMBankCardName = entity.IRMBankCardName;
										}					if (entity.IRMMinBalance!=null)
										{
											updateBefore.IRMMinBalance = entity.IRMMinBalance;
										}					if (entity.IRMPayPassword!=null)
										{
											updateBefore.IRMPayPassword = entity.IRMPayPassword;
										}					if (entity.IRMScanPayNotifyUrl!=null)
										{
											updateBefore.IRMScanPayNotifyUrl = entity.IRMScanPayNotifyUrl;
										}					if (entity.IRMWxUsername!=null)
										{
											updateBefore.IRMWxUsername = entity.IRMWxUsername;
										}					if (entity.IRMWxPassword!=null)
										{
											updateBefore.IRMWxPassword = entity.IRMWxPassword;
										}					if (entity.IRMRefreshBalance!=null)
										{
											updateBefore.IRMRefreshBalance = entity.IRMRefreshBalance;
										}
					DistributedTransactionPart beforePart = new DistributedTransactionPart{Id = distributedTransactionPart.Id};
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
		public override void DistributedDeleteInverse(DistributedTransactionPart distributedTransactionPart)
		{
			using (BaseDbContext dbContext = CreateDbContext())
            {
				IRobotRobotManager entity =JsonConvert.DeserializeObject<IRobotRobotManager>(CompressHelper.GZipDecompressString(distributedTransactionPart.InverseOper));
				dbContext.Entry(entity).State = EntityState.Added;
				DistributedTransactionPart beforePart = new DistributedTransactionPart{Id = distributedTransactionPart.Id};
				dbContext.Set<DistributedTransactionPart>().Attach(beforePart);
				beforePart.TransactionStatus = 2;
				dbContext.SaveChanges();
			}
		}

		/// <summary>
        /// 分布式删除批量数据的逆操作，当事务失败时调用
        /// </summary>
        /// <param name="entity">实体对象</param>
		public override void DistributedDeleteInverse(List<DistributedTransactionPart> distributedTransactionParts)
		{
			using (BaseDbContext dbContext = CreateDbContext())
            {
				foreach(DistributedTransactionPart distributedTransactionPart in distributedTransactionParts)
				{
					IRobotRobotManager entity =JsonConvert.DeserializeObject<IRobotRobotManager>(CompressHelper.GZipDecompressString(distributedTransactionPart.InverseOper));
					dbContext.Entry(entity).State = EntityState.Added;
					DistributedTransactionPart beforePart = new DistributedTransactionPart{Id = distributedTransactionPart.Id};
					dbContext.Set<DistributedTransactionPart>().Attach(beforePart);
					beforePart.TransactionStatus = 2;
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