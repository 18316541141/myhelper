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
        /// 事务id
        /// </summary>
        public virtual long Id { set; get; }

        /// <summary>
        /// 事务提交状态：0（已保存，未完成）、1（已保存、已完成）、2（事务失败）
        /// </summary>
        public virtual sbyte TransactionStatus { set; get; }

        /// <summary>
        /// 发起该事务的服务标识。
        /// </summary>
        public virtual string TransactionServiceTag { set; get; }
    }
}
