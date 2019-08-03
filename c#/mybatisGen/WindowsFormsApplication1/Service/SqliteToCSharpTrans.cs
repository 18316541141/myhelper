﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.Service
{
    public class SqliteToCSharpTrans : IDbTypeTrans
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
            return "string";
        }

        public string SqlType2ParamsType(string sqlType)
        {
            if (sqlType == "integer")
            {
                return "range";
            }
            else if (sqlType == "text")
            {
                return "like";
            }
            return "like";
        }
    }
}