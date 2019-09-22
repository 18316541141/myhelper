package web.template.service.common;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.Map;

import org.springframework.stereotype.Service;

import com.txj.common.entity.LeftMenu;
import com.txj.common.entity.MyPagedList;

import web.template.entity.common.AreaTree;
import web.template.entity.common.NewsAlarm;

/**
 * 项目内置的业务逻辑类
 * 
 * @author admin
 */
@Service("systemService")
public class SystemService {

	static List<NewsAlarm> _newsAlarmList;

	public SystemService() {
		_newsAlarmList = new ArrayList<NewsAlarm>();
		NewsAlarm newsAlarm = new NewsAlarm();
		newsAlarm.setCreateDate(new Date());
		newsAlarm.setMenuId("n11");
		newsAlarm.setReadState(0);
		newsAlarm.setReceive("asdasd");
		newsAlarm.setTitle("测试asdasd");
		_newsAlarmList.add(newsAlarm);
		NewsAlarm newsAlarm2 = new NewsAlarm();
		newsAlarm2.setCreateDate(new Date());
		newsAlarm2.setMenuId("m11");
		newsAlarm2.setReadState(0);
		newsAlarm2.setReceive("asddasd");
		newsAlarm2.setTitle("测试vvss");
		_newsAlarmList.add(newsAlarm2);
		NewsAlarm newsAlarm3 = new NewsAlarm();
		newsAlarm3.setCreateDate(new Date());
		newsAlarm3.setMenuId("m12");
		newsAlarm3.setReadState(0);
		newsAlarm3.setReceive("assdffsdd");
		newsAlarm3.setTitle("测e4as试");
		_newsAlarmList.add(newsAlarm3);
	}

	/**
	 * 加载最新消息提醒
	 * @return
	 */
	public MyPagedList<NewsAlarm> loadNewsAlarm() {
		MyPagedList<NewsAlarm> myPagedList = new MyPagedList<NewsAlarm>();
		myPagedList.setCurrentPageIndex(1);
		myPagedList.setStartItemIndex(0);
		myPagedList.setEndItemIndex(2);
		myPagedList.setPageDataList(_newsAlarmList);
		myPagedList.setPageSize(20);
		myPagedList.setTotalItemCount(3);
		myPagedList.setTotalPageCount(1);
		return myPagedList;
	}

	public Map<String, List<AreaTree>> loadAreaTree() {

		return null;
	}

	/**
	 * 根据用户名，加载左侧的菜单。
	 * 
	 * @param username
	 *            当前用户的用户名称
	 * @return 返回左侧菜单列表
	 */
	public List<LeftMenu> loadLeftMenus(String username) {
		List<LeftMenu> leftMenuList = new ArrayList<LeftMenu>();
        List<LeftMenu> leftMenuList11 = new ArrayList<LeftMenu>();
        LeftMenu leftMenuCharts = new LeftMenu();
    	leftMenuCharts.setId("m312");
    	leftMenuCharts.setTitle("统计图");
    	leftMenuCharts.setUrl("menus/testMenus1/charts.html");
    	leftMenuCharts.setSortIndex(3);
        	
        leftMenuList11.add(leftMenuCharts);
        LeftMenu leftMenuNewAlarm = new LeftMenu();
        leftMenuNewAlarm.setId("n11");
        leftMenuNewAlarm.setTitle("实时刷最新消息");
        leftMenuNewAlarm.setUrl("menus/testMenus1/testNewAlarm.html");
        leftMenuNewAlarm.setSortIndex(4);
        
        leftMenuList11.add(leftMenuNewAlarm);
        LeftMenu leftMenu111 = new LeftMenu();
    	leftMenu111.setId("m101");
    	leftMenu111.setTitle("测试1-1");
    	leftMenu111.setUrl("menus/testMenus1/treeForm.html");
    	leftMenu111.setSortIndex(5);
        
        leftMenuList11.add(leftMenu111);

        LeftMenu leftMenu112 = new LeftMenu();
    	leftMenu112.setId("m12");
    	leftMenu112.setTitle("测试1-2");
    	leftMenu112.setUrl("menus/testMenus1/uploadImage.html");
    	leftMenu112.setSortIndex(6);
        
        leftMenuList11.add(leftMenu112);

        LeftMenu leftMenu13 = new LeftMenu();
    	leftMenu13.setId("m13");
    	leftMenu13.setTitle("测试1-3");
    	leftMenu13.setUrl("menus/testMenus1/bigImg.html");
    	leftMenu13.setSortIndex(7);
        
        leftMenuList11.add(leftMenu13);

        LeftMenu leftMenu14 = new LeftMenu();
    	leftMenu14.setId("m14");
    	leftMenu14.setTitle("测试1-4");
    	leftMenu14.setUrl("menus/testMenus1/uploadFiles.html");
    	leftMenu14.setSortIndex(8);

        leftMenuList11.add(leftMenu14);

        LeftMenu leftMenu15 = new LeftMenu();
    	leftMenu15.setId("m15");
    	leftMenu15.setTitle("测试1-5");
    	leftMenu15.setUrl("menus/testMenus1/areaSelect.html");
    	leftMenu15.setSortIndex(9);
        
        leftMenuList11.add(leftMenu15);

        LeftMenu leftMenu17 = new LeftMenu();
        leftMenu17.setId("m17");
        leftMenu17.setTitle("测试1-7");
        leftMenu17.setUrl("menus/testMenus1/pageTable2.html");
        leftMenu17.setSortIndex(10);
        
        leftMenuList11.add(leftMenu17);

        LeftMenu leftMenu18 = new LeftMenu();
    	leftMenu18.setId("m18");
    	leftMenu18.setTitle("测试1-8");
    	leftMenu18.setUrl("menus/testMenus1/uploadExcel.html");
    	leftMenu18.setSortIndex(11);
        
        leftMenuList11.add(leftMenu18);
        LeftMenu leftMenu1 = new LeftMenu();
    	leftMenu1.setId("m1");
    	leftMenu1.setTitle("测试1");
    	leftMenu1.setSortIndex(0);
        
        leftMenu1.setLeftMenus(leftMenuList11);
        leftMenuList.add(leftMenu1);
        LeftMenu leftMenu2 = new LeftMenu();
    	leftMenu2.setId("m2");
    	leftMenu2.setTitle("测试2");
    	leftMenu2.setSortIndex(1);

        leftMenuList.add(leftMenu2);
        LeftMenu leftMenu3 = new LeftMenu();
    	leftMenu3.setId("m3");
    	leftMenu3.setTitle("测试3");
    	leftMenu3.setSortIndex(2);

        leftMenuList.add(leftMenu3);
        return leftMenuList;
	}
	
	/**
	 * 追加最新消息
	 * @param newsAlarm 最新消息实体类
	 */
	public void addNewsAlarm(NewsAlarm newsAlarm)
    {
        _newsAlarmList.add(newsAlarm);
    }
	
	/**
	 * 批量追加最新消息
	 * @param newsAlarmList
	 */
    public void addNewsAlarm(List<NewsAlarm> newsAlarmList)
    {
        _newsAlarmList.addAll(newsAlarmList);
    }
}