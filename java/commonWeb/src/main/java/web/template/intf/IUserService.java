package web.template.intf;
import java.util.List;
import com.txj.common.entity.LeftMenu;
/**
 * 用户操作的业务逻辑接口类，
 * 所有用户操作的业务逻辑必须实现这些方法。
 * @author admin
 *
 */
public interface IUserService {
	/**
	 * 根据签名的signKey获取签名的signSecret
	 * @param signKey 签名的key
	 * @return 返回用户的签名密钥
	 */
    String findSecretByKey(String signKey);

    /**
     * 根据signKey加载用户名
     * @param signKey 签名的key
     * @return 返回登陆的用户名
     */
    String loadUsernameBySignKey(String signKey);
    
    /**
     * 根据用户名加载密码
     * @param username 用户名
     * @return 返回登陆密码
     */
    String loadPasswordByUsername(String username);

    /**
     * 根据用户名，加载用户的树节点
     * @param username 用户名
     * @return 返回用户有权操作的树菜单节点
     */
    List<LeftMenu> loadLeftMenus(String username);
}