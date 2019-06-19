package web.template.shiro;

import org.apache.shiro.authz.Permission;
import org.apache.shiro.authz.permission.PermissionResolver;
import org.springframework.stereotype.Component;
/**
 * 自定义权限适配器
 * @author admin
 */
@Component("myPermissionResolver")
public class MyPermissionResolver implements PermissionResolver{

	/**
	 * 适配指定的权限解析器
	 * @param
	 */
	@Override
	public Permission resolvePermission(String permissionString) {
		return new MyPermission(permissionString);
	}

}
