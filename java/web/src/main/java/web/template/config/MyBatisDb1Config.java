package web.template.config;
import javax.sql.DataSource;
import org.apache.ibatis.session.SqlSessionFactory;
import org.mybatis.spring.SqlSessionFactoryBean;
import org.mybatis.spring.SqlSessionTemplate;
import org.mybatis.spring.annotation.MapperScan;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.boot.autoconfigure.jdbc.DataSourceProperties;
import org.springframework.boot.context.properties.ConfigurationProperties;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.context.annotation.Primary;
import org.springframework.core.io.Resource;
import org.springframework.core.io.support.PathMatchingResourcePatternResolver;
import org.springframework.jdbc.datasource.DataSourceTransactionManager;

import com.alibaba.druid.pool.DruidDataSource;

/**
 * 基于mybatis的db1配置类
 * @author admin
 */
@Configuration
@MapperScan(
        basePackages  = {"web.template.mapper.db1","web.template.mapper.common"},
        sqlSessionTemplateRef = "db1SqlSessionTemplate")
public class MyBatisDb1Config {
	
	@Bean
    @Primary
    public SqlSessionTemplate db1SqlSessionTemplate(@Autowired @Qualifier("db1SqlSessionFactory") SqlSessionFactory db1SqlSessionFactory) {
        return new SqlSessionTemplate(db1SqlSessionFactory);
    }

    @Bean
    @Primary
    public DataSourceTransactionManager db1TransactionManager(@Autowired @Qualifier("db1DataSource") DataSource db1DataSource){
        return new DataSourceTransactionManager(db1DataSource);
    }

    @Bean
    @Primary
    public SqlSessionFactory db1SqlSessionFactory(@Autowired @Qualifier("db1DataSource") DataSource db1DataSource) throws Exception {
        SqlSessionFactoryBean factoryBean = new SqlSessionFactoryBean();
        factoryBean.setDataSource(db1DataSource);
        PathMatchingResourcePatternResolver resolver=new PathMatchingResourcePatternResolver();
        factoryBean.setMapperLocations(resolver.getResources("classpath:mapper/db1/codeGenerator/*.xml"));
        factoryBean.setTypeAliasesPackage("web.template.entity.db1,web.template.params.db1,web.template.orderBy.db1,web.template.setNullParams.db1");
        return factoryBean.getObject();
    }

    @Bean
    @Primary
    @ConfigurationProperties("spring.datasource.db1")
    public DataSourceProperties db1DataSourceProperties() {
        return new DataSourceProperties();
    }
    
    @Bean
    @Primary
    public DataSource db1DataSource(@Autowired @Qualifier("db1DataSourceProperties") DataSourceProperties db1DataSourceProperties) {
    	DruidDataSource dataSource=new DruidDataSource();
    	dataSource.setDriverClassName(db1DataSourceProperties.getDriverClassName());
        dataSource.setUrl(db1DataSourceProperties.getUrl());
        dataSource.setUsername(db1DataSourceProperties.getUsername());
        dataSource.setPassword(db1DataSourceProperties.getPassword());
        return dataSource;
    }
}