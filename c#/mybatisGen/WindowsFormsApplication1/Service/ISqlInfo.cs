
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace WindowsFormsApplication1.Service
{
    /// <summary>
    /// 获取sql的信息接口
    /// </summary>
    public interface ISqlInfo
    {
        /// <summary>
        /// 根据表名，获取表注释
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns>返回表注释</returns>
        string TableNodes(string tableName);

        /// <summary>
        /// 根据表名，获取主键列名
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns>返回主键列名</returns>
        string KeyCol(string tableName);

        /// <summary>
        /// 根据表名，获取“列名-列注释”的键值对。
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns>返回“列名-列注释”的键值对</returns>
        Dictionary<string, string> CommentMap(string tableName);

        /// <summary>
        /// 根据表名，返回列信息
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        DataTable Columns(string tableName);
    }
}