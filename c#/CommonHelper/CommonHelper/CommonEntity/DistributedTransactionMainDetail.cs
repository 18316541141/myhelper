using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper.CommonEntity
{
    /// <summary>
    /// 分布式事务总表的子表
    /// </summary>
    public class DistributedTransactionMainDetail
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
        /// 分表所在的数据库标识
        /// </summary>
        public virtual string PartDatabaseTag { set; get; }
    }
}
