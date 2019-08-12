using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.Service
{
    public class SqlServerToCSharpTrans: IDbTypeTrans
    {
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
                return "double?";
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
            if (sqlType == "int")
            {
                return "range";
            }
            else if (sqlType == "nvarchar" || sqlType == "varchar" || sqlType == "char")
            {
                return "like";
            }
            else if (sqlType == "datetime" || sqlType == "datetime2" || sqlType == "decimal")
            {
                return "range";
            }
            return "";
        }
    }
}
