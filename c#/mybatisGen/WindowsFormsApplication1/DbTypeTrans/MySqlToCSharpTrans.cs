using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication1.Intf;

namespace WindowsFormsApplication1.DbTypeTrans
{
    /// <summary>
    /// mysql数据库的字段转为C井字段的转化类
    /// </summary>
    public class MySqlToCSharpTrans : IDbTypeTrans
    {

        public string SqlType2EntityType(string sqlType)
        {
            if (sqlType == "bigint")
            {
                return "long?";
            }
            else if (sqlType == "int")
            {
                return "int?";
            }
            else if (sqlType == "nvarchar" || sqlType == "varchar" || sqlType == "char")
            {
                return "string";
            }
            else if (sqlType == "datetime" || sqlType == "datetime2" || sqlType == "timestamp")
            {
                return "DateTime?";
            }
            else if (sqlType == "decimal")
            {
                return "decimal?";
            }
            return "";
        }

        public string SqlType2ParamsType(string sqlType)
        {
            if (sqlType == "int" || sqlType == "datetime" || sqlType == "datetime2" || sqlType == "decimal" || sqlType == "timestamp")
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
