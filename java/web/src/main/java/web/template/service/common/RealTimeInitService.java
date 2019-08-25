package web.template.service.common;

import org.springframework.stereotype.Service;

/**
 * 实时消息初始化，因为实时消息是通过版本号机制减少数据库的查询，
 * 但首次访问时，浏览器并不会存有任何版本号，这种情况下，默认是认为浏览器的
 * 信息不是最新的，但实际上需要先从数据库查询决定是否获取最新消息
 * @author admin
 */
@Service
public class RealTimeInitService {
	
	/**
	 * 实时信息的初始化器，返回true：不需要刷新，返回false：需要刷新
	 * @param poolName 等待池名称
	 * @param username 当前用户名，用于区分用户组
	 * @return
	 */
    public boolean init(String poolName,String username)
    {
        if (poolName == "newsAlarm")
        {
            return initNewsAlarm(username);
        }
        return false;
    } 

    /**
     * 最新消息框的初始化器
     * @param username 当前用户名，用于区分用户组
     * @return
     */
    boolean initNewsAlarm(String username)
    {
        return false;
    }
}
