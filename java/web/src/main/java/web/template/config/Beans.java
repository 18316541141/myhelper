package web.template.config;
import java.util.Map;
import java.util.Properties;

import javax.servlet.Filter;

import org.apache.shiro.authz.ModularRealmAuthorizer;
import org.apache.shiro.mgt.SecurityManager;
import org.apache.shiro.spring.web.ShiroFilterFactoryBean;
import org.apache.shiro.web.mgt.DefaultWebSecurityManager;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.web.servlet.FilterRegistrationBean;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.web.filter.DelegatingFilterProxy;

import com.fasterxml.jackson.databind.ObjectMapper;
import com.google.code.kaptcha.impl.DefaultKaptcha;
import com.google.code.kaptcha.util.Config;

import web.template.shiro.MyFormAuthenticationFilter;
import web.template.shiro.MyPermissionResolver;
import web.template.shiro.MyPermissionsAuthorizationFilter;
import web.template.shiro.UserRealm;
@Configuration
public class Beans {
	@Bean(name="defaultKaptcha")
	public DefaultKaptcha defaultKaptcha(){
		Properties properties=new Properties();
		properties.put("kaptcha.border", "no");  
		properties.put("kaptcha.border.color", "105,179,90");  
		properties.put("kaptcha.textproducer.font.color", "red");  
		properties.put("kaptcha.image.width", "250");
		properties.put("kaptcha.textproducer.font.size", "80");  
		properties.put("kaptcha.image.height", "90");
		properties.put("kaptcha.session.key", "code");
		properties.put("kaptcha.textproducer.char.length", "4");  
		properties.put("kaptcha.textproducer.char.string", "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ");
		properties.put("kaptcha.textproducer.font.names", "宋体,楷体,微软雅黑");		
		DefaultKaptcha defaultKaptcha=new DefaultKaptcha();
		defaultKaptcha.setConfig(new Config(properties));
		return defaultKaptcha;
	}
	
	@Bean(name="modularRealmAuthorizer")
	public ModularRealmAuthorizer modularRealmAuthorizer(){
		return new ModularRealmAuthorizer();
	}
	
	@Bean(name="objectMapper")
	public ObjectMapper objectMapper(){
		return new ObjectMapper();
	}
	
	@Bean(name="securityManager")
	public SecurityManager securityManager(@Autowired UserRealm userRealm,@Autowired MyPermissionResolver myPermissionResolver,@Autowired ModularRealmAuthorizer modularRealmAuthorizer){
		DefaultWebSecurityManager defaultWebSecurityManager=new DefaultWebSecurityManager();
		defaultWebSecurityManager.setRealm(userRealm);
		defaultWebSecurityManager.setAuthorizer(modularRealmAuthorizer);
		return defaultWebSecurityManager;
	}
	
	/**
	 * 创建一个自定义的登陆拦截器，如果有登陆，则执行拦截器责任链的下一环。
	 * 如果没有登陆，则返回信息告知用户没有登陆
	 * @return
	 */
	@Bean(name="myFormAuthenticationFilter")
	public MyFormAuthenticationFilter myFormAuthenticationFilter(){
		MyFormAuthenticationFilter myFormAuthenticationFilter=new MyFormAuthenticationFilter();
		myFormAuthenticationFilter.setLoginUrl("/index/login");
		myFormAuthenticationFilter.setUsernameParam("username");
		myFormAuthenticationFilter.setPasswordParam("password");
		return myFormAuthenticationFilter;
	}
	
	@Bean
	public FilterRegistrationBean<DelegatingFilterProxy> registerFilter() {
		FilterRegistrationBean<DelegatingFilterProxy> filterRegistrationBean = new FilterRegistrationBean<>();
	    DelegatingFilterProxy proxy = new DelegatingFilterProxy();
	    proxy.setTargetFilterLifecycle(true);
	    proxy.setTargetBeanName("shiroFilter");
	    filterRegistrationBean.setFilter(proxy);
	    return filterRegistrationBean;
	}
	
	@Bean(name="shiroFilter")
	public ShiroFilterFactoryBean shiroFilter(@Autowired SecurityManager securityManager,@Autowired MyFormAuthenticationFilter myFormAuthenticationFilter,@Autowired MyPermissionsAuthorizationFilter myPermissionsAuthorizationFilter){
		ShiroFilterFactoryBean shiroFilterFactoryBean=new ShiroFilterFactoryBean();
		shiroFilterFactoryBean.setSecurityManager(securityManager);
		Map<String,Filter> filterMap=shiroFilterFactoryBean.getFilters();
		filterMap.put("authc",myFormAuthenticationFilter);
		filterMap.put("perms", myPermissionsAuthorizationFilter);
		
		filterMap=shiroFilterFactoryBean.getFilters();
		
		Map<String, String> filterChainDefinitionMap=shiroFilterFactoryBean.getFilterChainDefinitionMap();
		filterChainDefinitionMap.put("/index/verificationCode", "anon");
//		filterChainDefinitionMap.put("/index/logout", "logout");
		filterChainDefinitionMap.put("/index/login", "authc");
		filterChainDefinitionMap.put("/index/loadLeftMenus", "authc");
//		filterChainDefinitionMap.put("/user/teacher", "authc,perms[\"/user/teacher\"]");
		return shiroFilterFactoryBean;
	}
}