package com.txj.common.entity;
import java.util.ArrayList;
import java.util.List;
/**
 * 后台的左侧菜单栏
 * @author admin
 */
public class LeftMenu {
	
	/**
	 * 菜单标题
	 */
	private String title;
	
	/**
	 * 菜单名称，系统内唯一名称，用于匹配菜单组件
	 */
	private String name;
	
	/**
	 * 菜单点击时，打开的页面所在的url
	 */
	private String url; 
	
	/**
	 * 子菜单
	 */
	private List<LeftMenu> leftMenus;
	
	/**
	 * 菜单序号
	 */
	private Integer sortIndex;
	
	public LeftMenu(){
		leftMenus=new ArrayList<LeftMenu>();
	}

	public Integer getSortIndex() {
		return sortIndex;
	}

	public void setSortIndex(Integer sortIndex) {
		this.sortIndex = sortIndex;
	}

	public String getTitle() {
		return title;
	}

	public void setTitle(String title) {
		this.title = title;
	}
	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

	public String getUrl() {
		return url;
	}

	public void setUrl(String url) {
		this.url = url;
	}

	public List<LeftMenu> getLeftMenus() {
		return leftMenus;
	}

	public void setLeftMenus(List<LeftMenu> leftMenus) {
		this.leftMenus = leftMenus;
	}
}
