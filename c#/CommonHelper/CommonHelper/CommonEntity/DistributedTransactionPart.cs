using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper.CommonEntity
{
    /// <summary>
    /// 分布式事务分表
    /// </summary>
    public class DistributedTransactionPart
    {
        /// <summary>
        /// 主键id
        /// </summary>
        public virtual long Id { set; get; }

        /// <summary>
        /// 事务id
        /// </summary>
        public virtual long DistributedTransactionMainId { set; get; }

        /// <summary>
        /// 逆向操作sql
        /// </summary>
        public virtual string InverseOper { set; get; }

        /// <summary>
        /// 涉及事务的表
        /// </summary>
        public virtual string TransTableName { set; get; }

        /// <summary>
        /// 涉及事务的主键
        /// </summary>
        public virtual string TransPrimaryKey { set; get; }

        /// <summary>
        /// 事务提交状态：0（已保存，未完成）、1（已保存、已完成）、2（事务失败）
        /// </summary>
        public virtual sbyte TransactionStatus { set; get; }
    }
}
