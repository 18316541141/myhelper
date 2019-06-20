package web.template.service;
import java.util.ArrayList;
import java.util.List;

import org.springframework.stereotype.Service;

import web.template.entity.LeftMenu;
/**
 * 项目内置的业务逻辑类
 * @author admin
 */
@Service("systemService")
public class SystemService {
	/**
	 * 根据用户名，加载左侧的菜单。
	 * @param username 当前用户的用户名称
	 * @return 返回左侧菜单列表
	 */
	public List<LeftMenu> loadLeftMenus(String username){
		List<LeftMenu>  leftMenuList=new ArrayList<LeftMenu>();
		LeftMenu leftMenu1=new LeftMenu();
		leftMenu1.setId("m1");
		leftMenu1.setTitle("测试1");
		List<LeftMenu>  leftMenuList11=new ArrayList<LeftMenu>();			
		LeftMenu leftMenu111=new LeftMenu();{
			leftMenu111.setId("m11");
			leftMenu111.setTitle("测试1-1");
			leftMenu111.setUrl("menus/testMenus1/treeForm.html");			
		}
		leftMenuList11.add(leftMenu111);
		
		LeftMenu leftMenu112=new LeftMenu();{
			leftMenu112.setId("m12");
			leftMenu112.setTitle("测试1-2");
			leftMenu112.setUrl("menus/testMenus1/uploadImage.html");			
		}
		leftMenuList11.add(leftMenu112);
		leftMenu1.setLeftMenus(leftMenuList11);
		leftMenuList.add(leftMenu1);
		LeftMenu leftMenu2=new LeftMenu();
		leftMenu2.setId("m2");
		leftMenu2.setTitle("测试2");
		
		leftMenuList.add(leftMenu2);
		LeftMenu leftMenu3=new LeftMenu();
		leftMenu3.setId("m3");
		leftMenu3.setTitle("测试3");
		
		leftMenuList.add(leftMenu3);
		return leftMenuList;
	}
}
