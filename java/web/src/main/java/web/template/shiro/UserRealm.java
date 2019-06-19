package web.template.shiro;
import org.apache.shiro.authc.AuthenticationException;
import org.apache.shiro.authc.AuthenticationInfo;
import org.apache.shiro.authc.AuthenticationToken;
import org.apache.shiro.authc.SimpleAuthenticationInfo;
import org.apache.shiro.authc.credential.CredentialsMatcher;
import org.apache.shiro.authz.AuthorizationInfo;
import org.apache.shiro.authz.SimpleAuthorizationInfo;
import org.apache.shiro.realm.AuthorizingRealm;
import org.apache.shiro.subject.PrincipalCollection;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;
/**
 * 用户信息和权限信息的数据源
 * @author admin
 */
@Component("userRealm")
public class UserRealm extends AuthorizingRealm {
	
	/**
	 * 创建一个权限校验器，并把权限的数据源提供给权限校验器
	 */
	@Override
	protected AuthorizationInfo doGetAuthorizationInfo(PrincipalCollection principals) {
		SimpleAuthorizationInfo info=new SimpleAuthorizationInfo();
		info.addRole("admin");
		info.addStringPermission("/aaaa");
		return info;
	}

	/**
	 * 创建一个登陆校验器
	 */
	@Override
	protected AuthenticationInfo doGetAuthenticationInfo(AuthenticationToken token) throws AuthenticationException {
		//zhang
		String username="60b69332fb3d57a5c6537a57d45d6e790768b6b6";
		//123
		String password="40bd001563085fc35165329ea1ff5c5ecbdbbeef";
		return new SimpleAuthenticationInfo(username, password, getName());
	}

}
