using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.Service
{
    public class SqlServerService
    {
        public string DataSource { set; get; }
        public string InitialCatalog { set; get; }
        public string UserId { set; get; }
        public string Password { set; get; }

        public string ConnStr { set; get; }

        NameTransService _nameTransService;

        DbTypeTransService _dbTypeTransService;

        public SqlServerService(string dataSource,string initialCatalog,string userId,string password)
        {
            DataSource = dataSource;
            InitialCatalog = initialCatalog;
            UserId = userId;
            Password = password;
            ConnStr = $"Data Source={dataSource};Initial Catalog={initialCatalog};User ID={userId};Password={password};MultipleActiveResultSets=True;";
            _nameTransService = new NameTransService();
            _dbTypeTransService = new DbTypeTransService();
        }

        public SqlConnection Open()
        {
            SqlConnection sqlConnection = new SqlConnection($"Data Source={DataSource};Initial Catalog={InitialCatalog};User ID={UserId};Password={Password};MultipleActiveResultSets=True;");
            sqlConnection.Open();
            return sqlConnection;
        }

        public void sssa(string tableName)
        {
            Dictionary<string, string> commentMap = new Dictionary<string, string>();
            SqlConnection sqlConnection = Open();
            using (SqlCommand sqlCommand = new SqlCommand(@"SELECT C.value TABLE_NAME
                FROM sys.tables A
                LEFT JOIN sys.extended_properties C ON C.major_id = A.object_id
                WHERE C.minor_id = 0 and A.name = @TABLE_NAME
                group by A.name, C.value"))
            {
                sqlCommand.ExecuteScalar();
            }
            using (SqlCommand sqlCommand = new SqlCommand(@"SELECT c.[name] AS COL_NAME,cast(ep.[value] 
                as varchar(100)) AS COL_COMMENT
                FROM sys.tables AS t
                INNER JOIN sys.columns
                AS c ON t.object_id = c.object_id
                LEFT JOIN sys.extended_properties AS ep
                ON ep.major_id = c.object_id AND ep.minor_id = c.column_id WHERE ep.class =1 
                AND t.name=@tableName", sqlConnection))
            {
                sqlCommand.Parameters.Add(new SqlParameter("@tableName", SqlDbType.NVarChar)).Value = tableName;
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
            DataTable dataTable = sqlConnection.GetSchema(SqlClientMetaDataCollectionNames.Columns, new string[] { InitialCatalog, null, tableName, null });
            if(dataTable.Rows.Count == 0)
            {
                throw new Exception("找不到该数据表！");
            }
            foreach (DataRow cl in dataTable.Rows)
            {
                string colName=(string)cl.ItemArray[3];
                _nameTransService.UnderlineToHump(colName);
                string dbType = (string)cl.ItemArray[7];
                string s=_dbTypeTransService.SqlType2EntityType(dbType);
            }
            Console.WriteLine();
        }
    }
}
