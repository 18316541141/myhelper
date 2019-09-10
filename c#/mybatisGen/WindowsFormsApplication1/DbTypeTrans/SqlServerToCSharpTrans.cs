using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication1.Intf;

namespace WindowsFormsApplication1.Service
{
    /// <summary>
    /// sql Server字段类型转c#数据类型
    /// </summary>
    public sealed class SqlServerToCSharpTrans: IDbTypeTrans
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
                return "long?";
            }
            else if(sqlType == "int")
            {
                return "int?";
            }
            else if(sqlType == "nvarchar" || sqlType == "varchar" || sqlType == "char")
            {
                return "string";
            }
            else if (sqlType == "datetime" || sqlType == "datetime2")
            {
                return "DateTime?";
            }
            else if (sqlType == "decimal")
            {
                return "decimal?";
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

        public bool SqlTypeIsChangeType(string sqlType)
        {
            return sqlType == "int" || sqlType == "decimal";
        }
    }
}
