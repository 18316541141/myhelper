package web.template.config;
import java.util.Map;
import java.util.Properties;

import javax.servlet.Filter;

import org.apache.shiro.authz.ModularRealmAuthorizer;
import org.apache.shiro.mgt.SecurityManager;
import org.apache.shiro.spring.web.ShiroFilterFactoryBean;
import org.apache.shiro.web.mgt.DefaultWebSecurityManager;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.autoconfigure.condition.ConditionalOnBean;
import org.springframework.boot.autoconfigure.condition.ConditionalOnMissingBean;
import org.springframework.boot.autoconfigure.web.servlet.WebMvcProperties.View;
import org.springframework.boot.web.servlet.FilterRegistrationBean;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.core.Ordered;
import org.springframework.stereotype.Component;
import org.springframework.web.filter.DelegatingFilterProxy;
import org.springframework.web.servlet.config.annotation.InterceptorRegistry;
import org.springframework.web.servlet.config.annotation.WebMvcConfigurer;
import org.springframework.web.servlet.view.BeanNameViewResolver;

import com.fasterxml.jackson.databind.ObjectMapper;
import com.google.code.kaptcha.impl.DefaultKaptcha;
import com.google.code.kaptcha.util.Config;
import com.txj.common.IpHelper;
import com.txj.common.SnowFlakeHelper;

import web.template.filter.UpdateVersionInterceptor;
import web.template.service.common.SystemService;
import web.template.shiro.MyFormAuthenticationFilter;
import web.template.shiro.MyPermissionResolver;
import web.template.shiro.MyPermissionsAuthorizationFilter;
import web.template.shiro.UserRealm;
import web.template.view.MyExcelView;
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
	
	@Bean(name="snowFlakeHelper")
	public SnowFlakeHelper snowFlakeHelper(){
		int ipInt=IpHelper.ipToInt(IpHelper.getOuterNetIP());
		return new SnowFlakeHelper(ipInt>>>27,ipInt & 31);
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
	public ShiroFilterFactoryBean shiroFilter(@Autowired SecurityManager securityManager,@Autowired MyPermissionsAuthorizationFilter myPermissionsAuthorizationFilter,@Autowired ObjectMapper objectMapper,@Autowired SystemService systemService){
		ShiroFilterFactoryBean shiroFilterFactoryBean=new ShiroFilterFactoryBean();
		shiroFilterFactoryBean.setSecurityManager(securityManager);
		
		Map<String, String> filterChainDefinitionMap=shiroFilterFactoryBean.getFilterChainDefinitionMap();
		filterChainDefinitionMap.put("/session/verificationCode", "anon");
		filterChainDefinitionMap.put("/index/anonymousRealTime", "anon");
		
		filterChainDefinitionMap.put("/**", "authc");
		
		Map<String,Filter> filterMap=shiroFilterFactoryBean.getFilters();
		
		MyFormAuthenticationFilter myFormAuthenticationFilter=new MyFormAuthenticationFilter();
		myFormAuthenticationFilter.setLoginUrl("/session/login");
		myFormAuthenticationFilter.setUsernameParam("username");
		myFormAuthenticationFilter.setPasswordParam("password");
		myFormAuthenticationFilter.setObjectMapper(objectMapper);
		myFormAuthenticationFilter.setSystemService(systemService);
		filterMap.put("authc",myFormAuthenticationFilter);
		filterMap.put("perms", myPermissionsAuthorizationFilter);
		return shiroFilterFactoryBean;
	}
	
	/**
	 * 这是整个web项目的拦截器配置
	 * @return
	 */
	@Bean
    public WebMvcConfigurer webMvcConfigurerAdapter(){
		return new WebMvcConfigurer(){
			@Override
            public void addInterceptors(InterceptorRegistry registry) {
                registry.addInterceptor(new UpdateVersionInterceptor())
                        .addPathPatterns(
                        		"/newAlarm/add"
                        );
            }
		};
	}
}