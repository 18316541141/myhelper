package web.template.entity;
import java.util.Date;
/**
 * 最新消息提醒
 * @author admin
 */
public class NewsAlarm {
	/**
	 * 提醒日期
	 */
	public Date createDate;

	/**
	 * 提醒标题
	 */
    public String title;

    /**
     * 菜单id
     */
    public String menuId;

    /**
     * 接收者
     */
    public String receive;

    /**
     * 读取状态
     */
    public Integer readState;

	public Date getCreateDate() {
		return createDate;
	}

	public void setCreateDate(Date createDate) {
		this.createDate = createDate;
	}

	public String getTitle() {
		return title;
	}

	public void setTitle(String title) {
		this.title = title;
	}

	public String getMenuId() {
		return menuId;
	}

	public void setMenuId(String menuId) {
		this.menuId = menuId;
	}

	public String getReceive() {
		return receive;
	}

	public void setReceive(String receive) {
		this.receive = receive;
	}

	public Integer getReadState() {
		return readState;
	}

	public void setReadState(Integer readState) {
		this.readState = readState;
	}
}
