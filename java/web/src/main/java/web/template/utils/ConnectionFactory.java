package web.template.utils;

import java.sql.Connection;
import java.sql.SQLException;

import javax.sql.DataSource;

/**
 * log4j2获取链接的方法
 * @author admin
 *
 */
public class ConnectionFactory {

	public static Connection getDataSourceConnection() throws SQLException {
		DataSource dataSource =BeanUtils.getBean("db1DataSource",DataSource.class);
		return dataSource.getConnection();
	}
}
