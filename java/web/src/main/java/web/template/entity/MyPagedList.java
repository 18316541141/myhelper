package web.template.entity;

import java.util.List;

/**
 * 自定义的分页列表
 * @author admin
 */
public class MyPagedList<T> {
	/**
	 * 分页查询的数据
	 */
    private List<T> pageDataList;

    /**
     * 当前页码
     */
    private int currentPageIndex;

    /**
     * 当前分页数据中，最后一条数据在所有数据中的索引
     */
    private long endItemIndex;

    /**
     * 每页显示的数据量
     */
    private int pageSize;

    /**
     * 当前分页数据中，第一条数据在所有数据中的索引
     */
    private int startItemIndex;
    
    /**
     * 数据总量
     */
    private long totalItemCount;
    
    /**
     * 总页数
     */
    private long totalPageCount;

	public List<T> getPageDataList() {
		return pageDataList;
	}

	public void setPageDataList(List<T> pageDataList) {
		this.pageDataList = pageDataList;
	}

	public int getCurrentPageIndex() {
		return currentPageIndex;
	}

	public void setCurrentPageIndex(int currentPageIndex) {
		this.currentPageIndex = currentPageIndex;
	}

	public long getEndItemIndex() {
		return endItemIndex;
	}

	public void setEndItemIndex(long endItemIndex) {
		this.endItemIndex = endItemIndex;
	}

	public int getPageSize() {
		return pageSize;
	}

	public void setPageSize(int pageSize) {
		this.pageSize = pageSize;
	}

	public int getStartItemIndex() {
		return startItemIndex;
	}

	public void setStartItemIndex(int startItemIndex) {
		this.startItemIndex = startItemIndex;
	}

	public long getTotalItemCount() {
		return totalItemCount;
	}

	public void setTotalItemCount(long totalItemCount) {
		this.totalItemCount = totalItemCount;
	}

	public long getTotalPageCount() {
		return totalPageCount;
	}

	public void setTotalPageCount(long totalPageCount) {
		this.totalPageCount = totalPageCount;
	}
}
