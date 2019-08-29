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
        /// 逆向操作的数据，当InverseOperType=D时，InverseOper记录逆操作的数据主键，
        /// 当InverseOperType=I时，InverseOper记录逆操作的数据主键，
        /// 当InverseOperType=A时，InverseOper记录更新前的json数据，
		/// 当InverseOperType=C时，InverseOper记录更新前的部分json数据，
        /// </summary>
        public virtual string InverseOper { set; get; }

        /// <summary>
        /// 逆向操作类型，当本次事务为插入操作时，逆向操作类型是：D（Delete）、
        /// 当本次事务为删除操作时，逆向操作类型是：I（Insert）、
        /// 当本次事务为全更新操作时，逆向操作类型是：A（UpdateAll）、
		/// 当本次事务为部分更新操作时，逆向操作类型是：C（UpdateChange）、
        /// </summary>
        public virtual char InverseOperType { set; get; }

        /// <summary>
        /// 涉及事务的表
        /// </summary>
        public virtual string TransTableName { set; get; }

        /// <summary>
        /// 涉及事务的主键
        /// </summary>
        public virtual string TransPrimaryKey { set; get; }

        /// <summary>
        /// 涉及事务的主键值
        /// </summary>
        public virtual long TransPrimaryKeyVal { set; get; }

        /// <summary>
        /// 事务提交状态：0（已保存，未完成）、1（已保存、已完成）、2（事务失败）
        /// </summary>
        public virtual sbyte TransactionStatus { set; get; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public virtual DateTime CreateDate { set; get; }
    }
}
