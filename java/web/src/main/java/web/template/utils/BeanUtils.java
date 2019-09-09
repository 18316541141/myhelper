package web.template.utils;

import org.springframework.context.ApplicationContext;
import org.springframework.context.ApplicationContextAware;
import org.springframework.stereotype.Component;

@Component
public class BeanUtils implements ApplicationContextAware {
    private static ApplicationContext applicationContext;
 
    /**
     * Spring容器会在创建该Bean之后，自动调用该Bean的setApplicationContext方法
     * @param applicationContext
     */
    @Override
    public void setApplicationContext(ApplicationContext applicationContext) {
        if(BeanUtils.applicationContext == null) {
            BeanUtils.applicationContext = applicationContext;
        }
    }
    
    public static ApplicationContext getApplicationContext() {
        return applicationContext;
    }
 
    /**
     * 只使用名称获取bean
     * @param name
     * @return
     */
    public static Object getBean(String name){
        return getApplicationContext().getBean(name);
    }
 
    /**
     * 通过clazz类型获取bean
     * @param clazz class类型
     * @return
     */
    public static <T> T getBean(Class<T> clazz){
        return getApplicationContext().getBean(clazz);
    }
 
    /**
     * 通过bean名称获取bean
     * @param name bean名称
     * @param clazz 返回的bean类型
     * @return 返回容器内的bean
     */
    public static <T> T getBean(String name,Class<T> clazz){
        return getApplicationContext().getBean(name, clazz);
    }
}