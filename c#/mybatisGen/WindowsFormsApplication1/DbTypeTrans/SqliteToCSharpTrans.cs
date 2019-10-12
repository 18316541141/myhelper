using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication1.Intf;

namespace WindowsFormsApplication1.Service
{
    public sealed class SqliteToCSharpTrans : IDbTypeTrans
    {
        public string SqlType2EntityType(string sqlType)
        {
            if (sqlType == "integer")
            {
                return "int?";
            }
            else if (sqlType == "text")
            {
                return "string";
            }
            else if(sqlType == "long")
            {
                return "long?";
            }
            return "string";
        }

        public string SqlType2ParamsType(string sqlType)
        {
            if (sqlType == "integer" || sqlType == "long")
            {
                return "range";
            }
            else if (sqlType == "text")
            {
                return "like";
            }
            return "like";
        }

        public bool SqlTypeIsChangeType(string sqlType)
        {
            return sqlType == "integer" || sqlType == "long";
        }
    }
}
