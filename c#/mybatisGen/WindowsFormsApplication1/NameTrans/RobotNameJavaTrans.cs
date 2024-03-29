﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication1.Intf;

namespace WindowsFormsApplication1.NameTrans
{
    /// <summary>
    /// 机器人项目组的java项目转换规则
    /// </summary>
    public sealed class RobotNameJavaTrans : INameTrans
    {
        /// <summary>
        /// 列名称转属性名称
        /// </summary>
        /// <param name="colName">表列名称</param>
        /// <returns>返回属性名称</returns>
        public string ColNameToPropName(string colName)
        {
            if (colName.IndexOf('_') > -1)
            {
                string[] names=colName.Split('_');
                StringBuilder sb = new StringBuilder(names[0].ToLower());
                for (int i=1,len=names.Length;i<len ;i++)
                {
                    sb.Append(names[1]);
                }
                return sb.ToString();
            }
            else
            {
                return colName.ToLower();
            }
        }

        /// <summary>
        /// 表名称转实体名称
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <returns>返回实体名称</returns>
        public string TableNameToEntityName(string tableName)
        {
            return tableName.Replace("_", "");
        }
    }
}
