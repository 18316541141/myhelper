using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.Service
{
    /// <summary>
    /// sql Server字段类型转java数据类型
    /// </summary>
    public class SqlServerToJavaTrans : IDbTypeTrans
    {
        public bool ColIsDeleteProp(string colName)
        {
            return colName.ToLower().EndsWith("isdelete");
        }

        /// <summary>
        /// sql类型转实体类型
        /// </summary>
        /// <param name="sqlType"></param>
        /// <returns></returns>
        public string SqlType2EntityType(string sqlType)
        {
            if (sqlType == "bigint")
            {
                return "Long";
            }
            else if (sqlType == "int")
            {
                return "Integer";
            }
            else if (sqlType == "nvarchar" || sqlType == "varchar" || sqlType == "char")
            {
                return "String";
            }
            else if (sqlType == "datetime" || sqlType == "datetime2")
            {
                return "Date";
            }
            else if (sqlType == "decimal")
            {
                return "Double";
            }
            return "";
        }

        /// <summary>
        /// 根据sql类型判断传参的类型：range（模糊搜索时可以传入范围条件）、like（模糊搜索时可以传入like条件）
        /// </summary>
        /// <param name="sqlType"></param>
        /// <returns></returns>
        public string SqlType2ParamsType(string sqlType)
        {
            if (sqlType == "int" || sqlType == "datetime" || sqlType == "datetime2" || sqlType == "decimal")
            {
                return "range";
            }
            else if (sqlType == "nvarchar" || sqlType == "varchar" || sqlType == "char")
            {
                return "like";
            }
            return "";
        }

        /// <summary>
        /// 根据sql类型判断是否为可通过+、-进行update的字段
        /// </summary>
        /// <param name="sqlType"></param>
        /// <returns></returns>
        public bool SqlTypeIsChangeType(string sqlType)
        {
            return sqlType == "int" || sqlType == "decimal";
        }
    }
}
