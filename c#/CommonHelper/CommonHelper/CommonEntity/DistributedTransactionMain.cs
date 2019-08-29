using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper.CommonEntity
{
    /// <summary>
    /// 分布式事务总表
    /// </summary>
    public class DistributedTransactionMain
    {
        /// <summary>
        /// 事务id，由消息队列记录完成、或未完成事务的数据源，
        /// </summary>
        public virtual long Id { set; get; }

        /// <summary>
        /// 事务状态，1（事务成功）、2（事务失败）
        /// </summary>
        public virtual sbyte TransactionStatus { set; get; }
    }
}
