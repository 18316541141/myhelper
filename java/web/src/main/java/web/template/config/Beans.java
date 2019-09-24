package web.template.config;
import java.util.ArrayList;
import java.util.Date;
import java.util.HashMap;
import java.util.List;
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
import org.springframework.core.annotation.Order;
import org.springframework.core.convert.converter.Converter;
import org.springframework.web.filter.DelegatingFilterProxy;
import org.springframework.web.servlet.config.annotation.InterceptorRegistry;
import org.springframework.web.servlet.config.annotation.WebMvcConfigurer;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.google.code.kaptcha.impl.DefaultKaptcha;
import com.google.code.kaptcha.util.Config;
import com.txj.common.IpHelper;
import com.txj.common.SnowFlakeHelper;

import web.template.entity.common.MyLog;
import web.template.filter.CompressFilter;
import web.template.formDataConverter.DateConverter;
import web.template.interceptor.SignInterceptor;
import web.template.interceptor.SizeFilterInterceptor;
import web.template.interceptor.SizeFilterInterceptor.SizeInfo;
import web.template.interceptor.UpdateVersionInterceptor;
import web.template.intf.IUserService;
import web.template.shiro.MyFormAuthenticationFilter;
import web.template.shiro.MyPermissionResolver;
import web.template.shiro.MyPermissionsAuthorizationFilter;
import web.template.shiro.UserRealm;
@Configuration
public class Beans {
	
	@Bean
    public Converter<String, Date> dateConverter() {
        return new DateConverter();
    }
	
	@Bean
	public MyLog myLog(@Value("projectName") final String projectName,@Autowired final SnowFlakeHelper snowFlakeHelper){
		final MyLog myLog = new MyLog();
		myLog.setProjectName(projectName);
		myLog.setSnowFlakeHelper(snowFlakeHelper);
		return myLog;
	}
	
	@Bean(name="defaultKaptcha")
	public DefaultKaptcha defaultKaptcha(){
		final Properties properties=new Properties();
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
		final DefaultKaptcha defaultKaptcha=new DefaultKaptcha();
		defaultKaptcha.setConfig(new Config(properties));
		return defaultKaptcha;
	}
	
	/**
	 * 注册一个报文压缩的拦截器
	 * @return
	 * @throws JsonProcessingException 
	 */
	@Bean
    public FilterRegistrationBean<CompressFilter> compressFilter(@Autowired final ObjectMapper objectMapper) throws JsonProcessingException {
		FilterRegistrationBean<CompressFilter> filterRegistrationBean = new FilterRegistrationBean<>();
		CompressFilter compressFilter = new CompressFilter();
		compressFilter.setObjectMapper(objectMapper);
		filterRegistrationBean.setFilter(compressFilter);
		//需要进行压缩的报文
		filterRegistrationBean.addUrlPatterns("/index/loadLeftMenus");
		filterRegistrationBean.addUrlPatterns("/index/loadLoginData");
		//这里一般是指分页查询数据，为确保分页查询得到压缩，控制器名称尽量以page为前缀。
		filterRegistrationBean.addUrlPatterns("/*/page*");
		//这里指代导出功能
		filterRegistrationBean.addUrlPatterns("/*/export*");
		List<String> exclude=new ArrayList<String>();
		//被匹配到但又不需要压缩的请求
//		exclude.add("//");
		filterRegistrationBean.addInitParameter(CompressFilter.EXCLUDE, objectMapper.writeValueAsString(exclude));
        return filterRegistrationBean;
    }
	
	@Bean(name="snowFlakeHelper")
	public SnowFlakeHelper snowFlakeHelper(){
		final int ipInt=IpHelper.ipToInt(IpHelper.getOuterNetIP());
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
	public SecurityManager securityManager(@Autowired final UserRealm userRealm,@Autowired final MyPermissionResolver myPermissionResolver,@Autowired final ModularRealmAuthorizer modularRealmAuthorizer){
		final DefaultWebSecurityManager defaultWebSecurityManager=new DefaultWebSecurityManager();
		defaultWebSecurityManager.setRealm(userRealm);
		defaultWebSecurityManager.setAuthorizer(modularRealmAuthorizer);
		return defaultWebSecurityManager;
	}
	
	@Bean
	public FilterRegistrationBean<DelegatingFilterProxy> registerFilter() {
		final FilterRegistrationBean<DelegatingFilterProxy> filterRegistrationBean = new FilterRegistrationBean<>();
		final DelegatingFilterProxy proxy = new DelegatingFilterProxy();
	    proxy.setTargetFilterLifecycle(true);
	    proxy.setTargetBeanName("shiroFilter");
	    filterRegistrationBean.setFilter(proxy);
	    return filterRegistrationBean;
	}
	
	@Bean(name="shiroFilter")
	public ShiroFilterFactoryBean shiroFilter(@Autowired final SecurityManager securityManager,@Autowired final MyPermissionsAuthorizationFilter myPermissionsAuthorizationFilter,@Autowired final ObjectMapper objectMapper,@Autowired final IUserService userService){
		final ShiroFilterFactoryBean shiroFilterFactoryBean=new ShiroFilterFactoryBean();
		shiroFilterFactoryBean.setSecurityManager(securityManager);
		
		final Map<String, String> filterChainDefinitionMap=shiroFilterFactoryBean.getFilterChainDefinitionMap();
		filterChainDefinitionMap.put("/session/verificationCode", "anon");
		filterChainDefinitionMap.put("/index/anonymousRealTime", "anon");
		
		filterChainDefinitionMap.put("/**", "authc");
		
		final Map<String,Filter> filterMap=shiroFilterFactoryBean.getFilters();
		
		final MyFormAuthenticationFilter myFormAuthenticationFilter=new MyFormAuthenticationFilter();
		myFormAuthenticationFilter.setLoginUrl("/session/login");
		myFormAuthenticationFilter.setUsernameParam("username");
		myFormAuthenticationFilter.setPasswordParam("password");
		myFormAuthenticationFilter.setObjectMapper(objectMapper);
		myFormAuthenticationFilter.setUserService(userService);
		filterMap.put("authc",myFormAuthenticationFilter);
		filterMap.put("perms", myPermissionsAuthorizationFilter);
		return shiroFilterFactoryBean;
	}
	
	/**
	 * 这是整个web项目的拦截器配置
	 * @return
	 */
	@Bean
    public WebMvcConfigurer webMvcConfigurerAdapter(@Autowired final ObjectMapper objectMapper){
		return new WebMvcConfigurer(){
			@Override
            public final void addInterceptors(InterceptorRegistry registry) {
				//请求报文长度拦截器，分为全局拦截、局部拦截
				Map<String,SizeInfo> sizeInfoMap=new HashMap<String, SizeInfo>();
				SizeFilterInterceptor sizeFilterInterceptor=new SizeFilterInterceptor(new SizeInfo(10240,"参数过大，请求失败！"),sizeInfoMap);
				sizeFilterInterceptor.setObjectMapper(objectMapper);
				registry.addInterceptor(sizeFilterInterceptor);
				
				//请求间隔拦截器，对于每次请求之间间隔时间太短时，进行拦截
//				Map<String, Long> intervalMillisMap = new HashMap<String, Long>();
//				intervalMillisMap.put("/session/verificationCode", 10000l);
//				OperIntervalInterceptor operIntervalInterceptor=new OperIntervalInterceptor(intervalMillisMap);
//				operIntervalInterceptor.setObjectMapper(objectMapper);
//				registry.addInterceptor(operIntervalInterceptor).addPathPatterns("/session/verificationCode");
				
				//请求次数拦截器，会记录请求的次数，当次数达到阈值时禁止访问，需要等待一段时间才能继续访问
//				Map<String, ClearInfo> clearInfoMap=new HashMap<String, ClearInfo>();
//				clearInfoMap.put("/session/verificationCode", new ClearInfo(1, 10000));
//				OperCountInterceptor operCountInterceptor=new OperCountInterceptor(clearInfoMap);
//				operCountInterceptor.setObjectMapper(objectMapper);
//				registry.addInterceptor(operCountInterceptor).addPathPatterns("/session/verificationCode");
				
				//版本更新拦截器，当调用该请求时，会更新等待池版本号
                registry.addInterceptor(new UpdateVersionInterceptor())
                        .addPathPatterns("/newAlarm/add");
                
				//签名拦截器，当该请求是一个需要签名认证的接口时，使用该过滤器
                final SignInterceptor signInterceptor=new SignInterceptor();
                signInterceptor.setObjectMapper(objectMapper);
                registry.addInterceptor(signInterceptor).addPathPatterns("/index/anonymousRealTime");
            }
		};
	}
}