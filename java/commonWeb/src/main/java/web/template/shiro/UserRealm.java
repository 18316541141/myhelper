package web.template.shiro;

import org.apache.commons.lang3.StringUtils;
import org.apache.shiro.authc.AuthenticationException;
import org.apache.shiro.authc.AuthenticationInfo;
import org.apache.shiro.authc.AuthenticationToken;
import org.apache.shiro.authc.SimpleAuthenticationInfo;
import org.apache.shiro.authc.UnknownAccountException;
import org.apache.shiro.authz.AuthorizationInfo;
import org.apache.shiro.authz.SimpleAuthorizationInfo;
import org.apache.shiro.realm.AuthorizingRealm;
import org.apache.shiro.subject.PrincipalCollection;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import web.template.intf.IUserService;

/**
 * 用户信息和权限信息的数据源
 * 
 * @author admin
 */
@Component("userRealm")
public class UserRealm extends AuthorizingRealm {

	@Autowired
	private IUserService userService;

	/**
	 * 创建一个权限校验器，并把权限的数据源提供给权限校验器
	 */
	@Override
	protected AuthorizationInfo doGetAuthorizationInfo(PrincipalCollection principals) {
		SimpleAuthorizationInfo info = new SimpleAuthorizationInfo();
		info.addRole("admin");
		info.addStringPermission("/aaaa");
		return info;
	}

	/**
	 * 创建一个登陆校验器
	 */
	@Override
	protected AuthenticationInfo doGetAuthenticationInfo(AuthenticationToken token) throws AuthenticationException {
		String username = (String) token.getPrincipal();
		if (StringUtils.isEmpty(username)) {
			throw new RuntimeException("用户名不能为空！");
		}
		username = username.trim();
		if (username.length()<5 || username.length()>20) {
			throw new RuntimeException("用户名长度只能在5-20个字符之间！");
		}
		String password = userService.loadPasswordByUsername(username);
		if (password == null) {
			throw new UnknownAccountException("用户名或密码错误！");
		}
		return new SimpleAuthenticationInfo(username, password, getName());
	}

}
