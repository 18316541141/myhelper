package web.template.shiro;

import org.apache.shiro.authz.Permission;

/**
 * 自定义的权限解析器
 * 
 * @author admin
 */
public class MyPermission implements Permission {

	/**
	 * 创建来自外部的请求，该请求所需的权限信息
	 * @param permissionStr	外部请求的权限信息
	 */
	public MyPermission(String permissionStr) {
		
	}

	/**
	 * 从权限校验器中获得的权限信息作为参数传入内部
	 * @param 权限校验器的校验信息。
	 */
	@Override
	public boolean implies(Permission permission) {
		// TODO Auto-generated method stub
		return false;
	}

}
