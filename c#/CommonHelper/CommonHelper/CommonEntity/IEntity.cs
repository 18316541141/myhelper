using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper.CommonEntity
{
    /// <summary>
    /// 实体类接口，实体类继承该接口可以方便采集数据，打印日志
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// 读写该实体类的主键值
        /// </summary>
        /// <returns></returns>
        long? Key { set; get; }

        /// <summary>
        /// 获取该实体类的表名
        /// </summary>
        /// <returns></returns>
        string TableName();
    }
}
