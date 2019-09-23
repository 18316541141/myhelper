package web.template.intf;

/**
 * 实时刷新的首次刷新业务处理
 * @author admin
 *
 */
public interface IRealTimeInitService {
	
	/**
	 * 首次刷新时调用该方法
	 * @param realTimePool	实时等待池名称
	 * @param username	用户名
	 * @return	返回true，需进入等待池。返回false则马上刷新
	 */
	boolean init(String realTimePool, String username);
}
