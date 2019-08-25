package web.template.shiro;
import java.io.IOException;
import javax.servlet.ServletRequest;
import javax.servlet.ServletResponse;
import org.apache.shiro.web.filter.authz.PermissionsAuthorizationFilter;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import com.fasterxml.jackson.databind.ObjectMapper;

import web.template.entity.common.Result;

@Component("myPermissionsAuthorizationFilter")
public class MyPermissionsAuthorizationFilter extends PermissionsAuthorizationFilter{
	
	@Autowired
	ObjectMapper objectMapper;
	
	protected boolean onAccessDenied(ServletRequest request, ServletResponse response) throws IOException {
		response.setCharacterEncoding("UTF-8");
		response.setContentType("application/json; charset=utf-8");
		response.getWriter().write(objectMapper.writeValueAsString(new Result(-10, "", null)));
        return false;
    }
}
