using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication1.Intf;

namespace WindowsFormsApplication1.SqlInfo
{
    /// <summary>
    /// mysql数据库信息类
    /// </summary>
    public class MySqlInfo : ISqlInfo
    {
        public MySqlConnection _conn;

        public string Server { set; get; }


        public string Database { set; get; }


        public string Uid { set; get; }


        public string Pwd { set; get; }


        public short Port { set; get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="server">服务端ip地址</param>
        /// <param name="database">数据库名称</param>
        /// <param name="uid">用户名</param>
        /// <param name="pwd">密码</param>
        /// <param name="port">端口号</param>
        public MySqlInfo(string server,string database,string uid,string pwd,short port=3307)
        {
            Server = server;
            Database = database;
            Uid = uid;
            Pwd = pwd;
            Port = port;
            _conn = new MySqlConnection($"server={server};Database={database};UID={uid};PWD={pwd};SslMode=none;Port={port}");
            _conn.Open();
        }

        //server=localhost;Database=sdny;UID=root;PWD=root;SslMode=none;Port=3307

        /// <summary>
        /// 该数据库原型表中，描述列类型的所在列下标
        /// </summary>
        /// <returns></returns>
        public int ColTypeInfoIndex()
        {
            return 7;
        }

        /// <summary>
        /// 根据表名，返回列信息
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public DataTable Columns(string tableName)
        {
            return _conn.GetSchema("Columns", new string[] { null, null, tableName, null });
        }

        /// <summary>
        /// 根据表名，获取“列名-列注释”的键值对。
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns>返回“列名-列注释”的键值对</returns>
        public Dictionary<string, string> CommentMap(string tableName)
        {
            Dictionary<string, string> commentMap = new Dictionary<string, string>();
            using (MySqlCommand command = new MySqlCommand("SELECT column_name,column_comment FROM information_schema.columns WHERE table_schema = @table_schema AND table_name = @table_name", _conn))
            {
                command.Parameters.Add(new MySqlParameter("@table_schema", MySqlDbType.VarChar)).Value = Database;
                command.Parameters.Add(new MySqlParameter("@table_name", MySqlDbType.VarChar)).Value = tableName;
                using (MySqlDataReader mySqlDataReader = command.ExecuteReader())
                {
                    while (mySqlDataReader.Read())
                    {
                        commentMap.Add(
                            mySqlDataReader.GetString(mySqlDataReader.GetOrdinal("column_name")),
                            mySqlDataReader.GetString(mySqlDataReader.GetOrdinal("column_comment"))
                        );
                    }
                }
            }
            return commentMap;
        }

        /// <summary>
        /// 根据表名，获取主键的字段名称。
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public string KeyCol(string tableName)
        {
            using (MySqlCommand command = new MySqlCommand("SELECT column_name FROM INFORMATION_SCHEMA.`KEY_COLUMN_USAGE` WHERE table_schema = @table_schema AND table_name=@table_name AND constraint_name='PRIMARY'", _conn))
            {
                command.Parameters.Add(new MySqlParameter("@table_schema", MySqlDbType.VarChar)).Value = Database;
                command.Parameters.Add(new MySqlParameter("@table_name", MySqlDbType.VarChar)).Value = tableName;
                return Convert.ToString(command.ExecuteScalar());
            }
        }

        /// <summary>
        /// 根据表名，获取表注释
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public string TableNodes(string tableName)
        {
            using (MySqlCommand command = new MySqlCommand("SELECT TABLE_COMMENT FROM information_schema.TABLES WHERE table_schema = @table_schema AND table_name=@table_name", _conn))
            {
                command.Parameters.Add(new MySqlParameter("@table_schema", MySqlDbType.VarChar)).Value = Database;
                command.Parameters.Add(new MySqlParameter("@table_name", MySqlDbType.VarChar)).Value = tableName;
                return Convert.ToString(command.ExecuteScalar());
            }
        }
    }
}