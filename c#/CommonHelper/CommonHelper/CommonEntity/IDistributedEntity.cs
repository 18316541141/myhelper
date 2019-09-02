using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper.CommonEntity
{
    /// <summary>
    /// 分布式实体类接口，所有分布式实体类必须继承该接口
    /// </summary>
    public interface IDistributedEntity
    {
        /// <summary>
        /// 获取该实体类的主键值
        /// </summary>
        /// <returns></returns>
        long Key();

        /// <summary>
        /// 获取该实体类的表名
        /// </summary>
        /// <returns></returns>
        string TableName();
    }
}
