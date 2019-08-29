using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper.CommonEntity
{
    /// <summary>
    /// 事务总表的详细表
    /// </summary>
    public class DistributedTransactionMainDetail
    {
        /// <summary>
        /// 关联事务总表的主键
        /// </summary>
        public virtual long DistributedTransactionMainId { set; get; }

        /// <summary>
        /// 事务总表的数据源
        /// </summary>
        public virtual string TransactionDataSource { set; get; }
    }
}
