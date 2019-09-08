using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper.CommonEntity
{
    /// <summary>
    /// 分布式数据源表，用于记录当前数据库的数据源标识，
    /// 并保存到数据表内，用数据库的表保存该标识的好处是
    /// 当数据库地址迁移时，标识也能保证不变
    /// </summary>
    public class DistributedDataSrc
    {
        /// <summary>
        /// 数据源标识
        /// </summary>
        public virtual string DataSrc { set; get; }
    }
}
