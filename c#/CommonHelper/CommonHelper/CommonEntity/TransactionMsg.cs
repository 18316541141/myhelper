using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CommonHelper.CommonEntity
{
    /// <summary>
    /// 分布式事务在消息队列中传输的数据实体
    /// </summary>
    public class TransactionMsg
    {
        /// <summary>
        /// 事务id
        /// </summary>
        public long Id { set; get; }

        /// <summary>
        /// 事务状态：1（成功）、2（失败）
        /// </summary>
        public sbyte Status { set; get; }

        /// <summary>
        /// 涉及到的数据源
        /// </summary>
        public string[] DataSrcs { set; get; }
    }
}
