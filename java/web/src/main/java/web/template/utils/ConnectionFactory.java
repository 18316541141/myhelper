package web.template.utils;
import java.sql.Connection;
import java.util.Properties;
import com.alibaba.druid.pool.DruidDataSourceFactory;

/**
 * log4j2获取链接的方法
 * @author admin
 *
 */
public class ConnectionFactory {

	public static Connection getDataSourceConnection() throws Exception {
		Properties result = new Properties();
        result.put("driverClassName", "com.microsoft.sqlserver.jdbc.SQLServerDriver");
        result.put("url","jdbc:sqlserver://183.2.233.235:1433;DatabaseName=BusinessAssistantDB_Test");
        result.put("username", "BusinessHeplerTestManager");
        result.put("password", "BusinessHeplerTestManager123");
		return DruidDataSourceFactory.createDataSource(result).getConnection();
	}
}
