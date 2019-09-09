using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication1.Intf;

namespace WindowsFormsApplication1.SqlInfo
{
    public sealed class SqliteInfo : ISqlInfo
    {
        SQLiteConnection _conn;
        public SqliteInfo(string path)
        {
            _conn = new SQLiteConnection($@"data source={path};version=3");
            _conn.Open();
        }

        public int ColTypeInfoIndex()
        {
            return 11;
        }

        public DataTable Columns(string tableName)
        {
            return _conn.GetSchema(SqlClientMetaDataCollectionNames.Columns, new string[] { null, null, tableName, null });
        }

        public Dictionary<string, string> CommentMap(string tableName)
        {
            return new Dictionary<string, string>();
        }

        public string KeyCol(string tableName)
        {
            using (SQLiteCommand sqliteCommand = new SQLiteCommand($"pragma table_info ('{tableName}')", _conn))
            {
                using (SQLiteDataReader sqliteDataReader = sqliteCommand.ExecuteReader())
                {
                    while (sqliteDataReader.Read())
                    {
                        int pk=sqliteDataReader.GetInt32(sqliteDataReader.GetOrdinal("pk"));
                        if (pk == 1)
                        {
                            return sqliteDataReader.GetString(sqliteDataReader.GetOrdinal("name"));
                        }
                    }
                }
            }
            return "";
        }

        public string TableNodes(string tableName)
        {
            return "";
        }
    }
}
