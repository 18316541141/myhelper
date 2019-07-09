using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.Service
{
    public class DbTypeTransService
    {
        /// <summary>
        /// sql类型转实体类型
        /// </summary>
        /// <param name="sqlType"></param>
        /// <returns></returns>
        public string SqlType2EntityType(string sqlType)
        {
            if(sqlType == "int")
            {
                return "int?";
            }
            else if(sqlType == "nvarchar" || sqlType == "varchar" || sqlType == "char")
            {
                return "string";
            }
            else if (sqlType == "datetime")
            {
                return "DateTime?";
            }
            return "";
        }

        /// <summary>
        /// sql类型转参数分布类型
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
            else if (sqlType == "datetime")
            {
                return "range";
            }
            return "";
        }
    }
}
