using CommonHelper.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper.EFRepository
{
    /// <summary>
    /// 数据的逆反操作
    /// </summary>
    public interface InverseRepository
    {
        /// <summary>
        /// 分布式插入数据的反操作。当事务失败时调用
        /// </summary>
        /// <param name="distributedTransactionPart">分表对象</param>
        void DistributedInsertInverse(DistributedTransactionPart distributedTransactionPart);

        /// <summary>
        /// 分布式批量插入数据的反向操作。当事务失败时调用
        /// </summary>
        /// <param name="distributedTransactionParts">分表集合</param>
        void DistributedInsertInverse(List<DistributedTransactionPart> distributedTransactionParts);

        /// <summary>
        /// 分布式setNull的逆操作，当事务失败时调用
        /// </summary>
        /// <param name="distributedTransactionPart">分表对象</param>
		void DistributedSetNullInverse(DistributedTransactionPart distributedTransactionPart);

        /// <summary>
        /// 分布式部分更新数据的逆操作，当事务失败时调用
        /// </summary>
        /// <param name="distributedTransactionPart">分表对象</param>
		void DistributedUpdateChangeInverse(DistributedTransactionPart distributedTransactionPart);

        /// <summary>
        /// 分布式全覆盖更新数据的逆操作，当事务失败时调用
        /// </summary>
        /// <param name="distributedTransactionPart">分表对象</param>
		void DistributedUpdateAllInverse(DistributedTransactionPart distributedTransactionPart);

        /// <summary>
        /// 分布式批量全覆盖更新数据的逆操作，当事务失败时调用
        /// </summary>
        /// <param name="distributedTransactionParts">分表集合</param>
		void DistributedUpdateAllBatchInverse(List<DistributedTransactionPart> distributedTransactionParts);

        /// <summary>
        /// 分布式删除数据的逆操作，当事务失败时调用
        /// </summary>
        /// <param name="distributedTransactionPart">分表对象</param>
		void DistributedDeleteInverse(DistributedTransactionPart distributedTransactionPart);

        /// <summary>
        /// 分布式删除批量数据的逆操作，当事务失败时调用
        /// </summary>
        /// <param name="distributedTransactionParts">分表集合</param>
		void DistributedDeleteInverse(List<DistributedTransactionPart> distributedTransactionParts);

        /// <summary>
        /// 根据事务id，查询事务未确定或未取消的分表信息。
        /// </summary>
        /// <param name="transactionId"></param>
        /// <returns></returns>
        List<DistributedTransactionPart> FindTransPartsById(long? transactionId=null);


        /// <summary>
        /// 更新事务分表状态
        /// </summary>
        /// <param name="idList">事务分表的主键</param>
        /// <param name="status">更新状态</param>
        void UpdateTransPartsStatus(List<long> idList, sbyte status);
    }
}
