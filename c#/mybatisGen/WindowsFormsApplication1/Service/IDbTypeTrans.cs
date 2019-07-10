using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.Service
{
    /// <summary>
    /// 数据库类型转编程语言类型
    /// </summary>
    public interface IDbTypeTrans
    {
        /// <summary>
        /// sql类型转实体类型
        /// </summary>
        /// <param name="sqlType">数据库类型</param>
        /// <returns></returns>
        string SqlType2EntityType(string sqlType);

        /// <summary>
        /// 根据sql类型判断传参的类型：range（模糊搜索时可以传入范围条件）、like（模糊搜索时可以传入like条件）
        /// </summary>
        /// <param name="sqlType">数据库类型</param>
        /// <returns></returns>
        string SqlType2ParamsType(string sqlType);
    }
}
