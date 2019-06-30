package web.template.mapper;
import java.util.List;

import org.apache.ibatis.annotations.Param;

import web.template.entity.MyPagedList;
public interface BaseMapper<T,P> {
	/**
	 * 插入一条记录，返回主键
	 * @param t	插入记录的实体类
	 * @return
	 */
	public int insert(T entity);
	
	/**
	 * 插入一批数据
	 * @param entities	插入数据
	 */
	public void insertList(List<T> entities);
	
	/**
	 * 根据主键删除数据
	 * @param entity
	 */
	public void delete(int key);
	
	/**
	 * 根据主键删除一批对象
	 * @param keys
	 */
	public void deleteList(List<P> keys);
	
	/**
	 * 根据条件删除对象
	 * @param params
	 */
	public void deleteByParams(P params);
	
	/**
	 * 根据主键查找一个实体
	 * @return
	 */
	public T findEntity(int key);
	
	/**
	 * 根据条件查找一个实体
	 * @param params
	 * @return
	 */
	public T findEntityByParams(P params);
	
	/**
	 * 根据条件查询符合条件的数据量
	 * @param params
	 * @return
	 */
	public long count(P params);
	
	/**
	 * 根据条件查询所有符合条件的数据
	 * @param params
	 * @return
	 */
	public List<T> findAllByParams(P params);
	
	/**
	 * 根据条件查询列表，分页查询
	 * @param params
	 * @return
	 */
	public List<T> findListByParams(@Param("params")P params,@Param("currentPageIndex")int currentPageIndex,@Param("pageSize")int pageSize);
	
	/**
	 * 分页查询结果
	 * @param params
	 * @param pageIndex
	 * @param pageSize
	 * @return
	 */
	default  MyPagedList<T> pageList(P params,int currentPageIndex,int pageSize){
		long totalCount=count(params);
		List<T> tList=findListByParams(params,currentPageIndex,pageSize);
		MyPagedList<T> myPagedList=new MyPagedList<>();
		myPagedList.setPageDataList(tList);
		myPagedList.setPageSize(pageSize);
		myPagedList.setTotalItemCount(totalCount);
		myPagedList.setTotalPageCount(((totalCount-totalCount%pageSize)/pageSize)+1);
		myPagedList.setCurrentPageIndex(currentPageIndex);
		myPagedList.setStartItemIndex((currentPageIndex-1)*pageSize+1);
		myPagedList.setEndItemIndex(Math.min(totalCount, currentPageIndex*pageSize));
		return myPagedList;
	}
}