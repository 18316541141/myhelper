using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.Service
{
    public class SqlServerInfo : ISqlInfo
    {

        public string DataSource { set; get; }
        public string InitialCatalog { set; get; }
        public string UserId { set; get; }
        public string Password { set; get; }

        public SqlConnection _conn;
        public SqlServerInfo(string dataSource,string initialCatalog ,string userId,string password)
        {
            DataSource=dataSource;
            InitialCatalog = InitialCatalog;
            UserId = UserId;
            Password = password;
            _conn= new SqlConnection($"Data Source={dataSource};Initial Catalog={initialCatalog};User ID={userId};Password={password};MultipleActiveResultSets=True;");
            _conn.Open();
        }

        public DataTable Columns(string tableName)
        {
            return _conn.GetSchema(SqlClientMetaDataCollectionNames.Columns, new string[] { InitialCatalog, null, tableName, null });
        }

        public Dictionary<string, string> CommentMap(string tableName)
        {
            Dictionary<string, string> commentMap = new Dictionary<string, string>();
            using (SqlCommand sqlCommand = new SqlCommand(@"SELECT c.[name] AS COL_NAME,cast(ep.[value] 
                as varchar(100)) AS COL_COMMENT
                FROM sys.tables AS t
                INNER JOIN sys.columns
                AS c ON t.object_id = c.object_id
                LEFT JOIN sys.extended_properties AS ep
                ON ep.major_id = c.object_id AND ep.minor_id = c.column_id WHERE ep.class =1 
                AND t.name=@TABLE_NAME", _conn))
            {
                sqlCommand.Parameters.Add(new SqlParameter("@TABLE_NAME", SqlDbType.NVarChar)).Value = tableName;
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        commentMap.Add(
                            sqlDataReader.GetString(sqlDataReader.GetOrdinal("COL_NAME")),
                            sqlDataReader.GetString(sqlDataReader.GetOrdinal("COL_COMMENT"))
                        );
                    }
                }
            }
            return commentMap;
        }

        public string KeyCol(string tableName)
        {
            using (SqlCommand sqlCommand = new SqlCommand(@"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE TABLE_NAME=@TABLE_NAME
                and CONSTRAINT_NAME like 'PK_Tb_%'", _conn))
            {
                sqlCommand.Parameters.Add(new SqlParameter("@TABLE_NAME", SqlDbType.NVarChar)).Value = tableName;
                return Convert.ToString(sqlCommand.ExecuteScalar());
            }
        }

        public string TableNodes(string tableName)
        {
            using (SqlCommand sqlCommand = new SqlCommand(@"SELECT C.value
                FROM sys.tables A
                LEFT JOIN sys.extended_properties C ON C.major_id = A.object_id
                WHERE C.minor_id = 0 and A.name = @TABLE_NAME
                group by A.name, C.value", _conn))
            {
                sqlCommand.Parameters.Add(new SqlParameter("@TABLE_NAME", SqlDbType.NVarChar)).Value = tableName;
                return Convert.ToString(sqlCommand.ExecuteScalar());
            }
        }

        public int ColTypeInfoIndex()
        {
            return 7;
        }
    }
}
