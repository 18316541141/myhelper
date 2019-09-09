using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.Intf
{
    /// <summary>
    /// 命名转换类
    /// </summary>
    public interface INameTrans
    {
        /// <summary>
        /// 数据表名转实体名
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        string TableNameToEntityName(string tableName);

        /// <summary>
        /// 数据列名转属性名
        /// </summary>
        /// <param name="colName"></param>
        /// <returns></returns>
        string ColNameToPropName(string colName);
    }
}