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
namespace WebApplication1.Repository
{
    public partial class IRobotQrCodePayTaskInverseRepository : InverseRepository
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
				dbContext.Entry(new IRobotQrCodePayTask{ IRTaskID = (int?)distributedTransactionPart.TransPrimaryKeyVal }).State = EntityState.Deleted;
				dbContext.Set<DistributedTransactionPart>().Attach(updateBefore);
				updateBefore.TransactionStatus = 2;
				dbContext.SaveChanges();
			}
		}

		/// <summary>
        /// 分布式批量更新数据的反向操作。当事务失败时调用
        /// </summary>
        /// <param name="entity">实体对象</param>
		public override void DistributedInsertInverse(List<DistributedTransactionPart> distributedTransactionParts)
		{
			using (BaseDbContext dbContext = CreateDbContext())
            {
				foreach(var distributedTransactionPart in distributedTransactionParts)
				{
					DistributedTransactionPart updateBefore=new DistributedTransactionPart{ Id = distributedTransactionPart.Id };
					dbContext.Entry(new IRobotQrCodePayTask{ IRTaskID = (int?)distributedTransactionPart.TransPrimaryKeyVal }).State = EntityState.Deleted;
					dbContext.Set<DistributedTransactionPart>().Attach(updateBefore);
					updateBefore.TransactionStatus = 2;
				}
				dbContext.SaveChanges();
			}
		}

		/// <summary>
        /// 分布式更新的逆操作，当事务失败时调用
        /// </summary>
        /// <param name="entity">实体对象</param>
		public override void DistributedUpdateInverse(DistributedTransactionPart distributedTransactionPart)
		{
			using (BaseDbContext dbContext = CreateDbContext())
            {
				IRobotQrCodePayTask updateBefore = new IRobotQrCodePayTask { IRTaskID = (int?)distributedTransactionPart.TransPrimaryKeyVal };
				dbContext.Set<IRobotQrCodePayTask>().Attach(updateBefore);
				IRobotQrCodePayTask entity =JsonConvert.DeserializeObject<IRobotQrCodePayTask>(CompressHelper.GZipDecompressString(distributedTransactionPart.InverseOper));
								if (entity.IROrderNo!=null)
								{
									updateBefore.IROrderNo = entity.IROrderNo;
								}				if (entity.IRWeiXinNickName!=null)
								{
									updateBefore.IRWeiXinNickName = entity.IRWeiXinNickName;
								}				if (entity.IRWeiXinHeaderImage!=null)
								{
									updateBefore.IRWeiXinHeaderImage = entity.IRWeiXinHeaderImage;
								}				if (entity.IRQrCodeImagePath!=null)
								{
									updateBefore.IRQrCodeImagePath = entity.IRQrCodeImagePath;
								}				if (entity.IRHandleState!=null)
								{
									updateBefore.IRHandleState = entity.IRHandleState;
								}				if (entity.IRHandleMessage!=null)
								{
									updateBefore.IRHandleMessage = entity.IRHandleMessage;
								}				if (entity.IRHandleTime!=null)
								{
									updateBefore.IRHandleTime = entity.IRHandleTime;
								}				if (entity.IRCreateTime!=null)
								{
									updateBefore.IRCreateTime = entity.IRCreateTime;
								}				if (entity.IRReportPicPathJson!=null)
								{
									updateBefore.IRReportPicPathJson = entity.IRReportPicPathJson;
								}				if (entity.IRTakeMoney!=null)
								{
									updateBefore.IRTakeMoney = entity.IRTakeMoney;
								}				if (entity.IRRobotId!=null)
								{
									updateBefore.IRRobotId = entity.IRRobotId;
								}				if (entity.IRRemark!=null)
								{
									updateBefore.IRRemark = entity.IRRemark;
								}				if (entity.IRPushState!=null)
								{
									updateBefore.IRPushState = entity.IRPushState;
								}				if (entity.IRPushTime!=null)
								{
									updateBefore.IRPushTime = entity.IRPushTime;
								}				if (entity.IRScanPayNotifyRet!=null)
								{
									updateBefore.IRScanPayNotifyRet = entity.IRScanPayNotifyRet;
								}				if (entity.IRScanPayNotifyUrl!=null)
								{
									updateBefore.IRScanPayNotifyUrl = entity.IRScanPayNotifyUrl;
								}
				DistributedTransactionPart beforePart = new DistributedTransactionPart{ Id = distributedTransactionPart.Id };
				dbContext.Set<DistributedTransactionPart>().Attach(beforePart);
				beforePart.TransactionStatus = 2;
				dbContext.SaveChanges();
			}
		}

		/// <summary>
        /// 分布式批量全覆盖更新数据的逆操作，当事务失败时调用
        /// </summary>
        /// <param name="entity">实体对象</param>
		public override void DistributedUpdateInverse(List<DistributedTransactionPart> distributedTransactionParts)
		{
			using (BaseDbContext dbContext = CreateDbContext())
            {
				foreach(var distributedTransactionPart in distributedTransactionParts)
				{
					IRobotQrCodePayTask updateBefore = new IRobotQrCodePayTask { IRTaskID = (int?)distributedTransactionPart.TransPrimaryKeyVal };
					dbContext.Set<IRobotQrCodePayTask>().Attach(updateBefore);
					IRobotQrCodePayTask entity =JsonConvert.DeserializeObject<IRobotQrCodePayTask>(CompressHelper.GZipDecompressString(distributedTransactionPart.InverseOper));
										if (entity.IROrderNo!=null)
										{
											updateBefore.IROrderNo = entity.IROrderNo;
										}					if (entity.IRWeiXinNickName!=null)
										{
											updateBefore.IRWeiXinNickName = entity.IRWeiXinNickName;
										}					if (entity.IRWeiXinHeaderImage!=null)
										{
											updateBefore.IRWeiXinHeaderImage = entity.IRWeiXinHeaderImage;
										}					if (entity.IRQrCodeImagePath!=null)
										{
											updateBefore.IRQrCodeImagePath = entity.IRQrCodeImagePath;
										}					if (entity.IRHandleState!=null)
										{
											updateBefore.IRHandleState = entity.IRHandleState;
										}					if (entity.IRHandleMessage!=null)
										{
											updateBefore.IRHandleMessage = entity.IRHandleMessage;
										}					if (entity.IRHandleTime!=null)
										{
											updateBefore.IRHandleTime = entity.IRHandleTime;
										}					if (entity.IRCreateTime!=null)
										{
											updateBefore.IRCreateTime = entity.IRCreateTime;
										}					if (entity.IRReportPicPathJson!=null)
										{
											updateBefore.IRReportPicPathJson = entity.IRReportPicPathJson;
										}					if (entity.IRTakeMoney!=null)
										{
											updateBefore.IRTakeMoney = entity.IRTakeMoney;
										}					if (entity.IRRobotId!=null)
										{
											updateBefore.IRRobotId = entity.IRRobotId;
										}					if (entity.IRRemark!=null)
										{
											updateBefore.IRRemark = entity.IRRemark;
										}					if (entity.IRPushState!=null)
										{
											updateBefore.IRPushState = entity.IRPushState;
										}					if (entity.IRPushTime!=null)
										{
											updateBefore.IRPushTime = entity.IRPushTime;
										}					if (entity.IRScanPayNotifyRet!=null)
										{
											updateBefore.IRScanPayNotifyRet = entity.IRScanPayNotifyRet;
										}					if (entity.IRScanPayNotifyUrl!=null)
										{
											updateBefore.IRScanPayNotifyUrl = entity.IRScanPayNotifyUrl;
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
				IRobotQrCodePayTask entity =JsonConvert.DeserializeObject<IRobotQrCodePayTask>(CompressHelper.GZipDecompressString(distributedTransactionPart.InverseOper));
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
					IRobotQrCodePayTask entity =JsonConvert.DeserializeObject<IRobotQrCodePayTask>(CompressHelper.GZipDecompressString(distributedTransactionPart.InverseOper));
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
            return new MyDbContext();
        }
    }
}