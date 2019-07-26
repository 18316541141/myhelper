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
	 * @param params 删除条件
	 * @param nparams 不等删除条件
	 */
	public void deleteByParams(@Param("params")P params,@Param("nparams")P nparams);
	
	/**
	 * 根据主键查找一个实体
	 * @return
	 */
	public T findEntity(int key);
	
	/**
	 * 根据条件查找一个实体
	 * @param params 查询条件
	 * @param nparams 不等查询条件
	 * @return
	 */
	public T findEntityByParams(@Param("params")P params, @Param("nparams")P nparams);
	
	/**
	 * 根据条件查询符合条件的数据量
	 * @param params 查询条件
	 * @param nparams 不等查询条件
	 * @return
	 */
	public long count(@Param("params")P params, @Param("nparams")P nparams);
	
	/**
	 * 根据条件查询所有符合条件的数据
	 * @param params 查询条件
	 * @param nparams 不等查询条件
	 * @return
	 */
	public List<T> findAllByParams(@Param("params")P params, @Param("nparams")P nparams);
	
	/**
	 * 根据条件查询列表，分页查询
	 * @param params 查询条件
	 * @param currentPageIndex 当前页码
	 * @param pageSize 每页显示数据量
	 * @param nparams 不等查询条件
	 * @return
	 */
	public List<T> findListByParams(@Param("params")P params,@Param("currentPageIndex")int currentPageIndex,@Param("pageSize")int pageSize, @Param("nparams")P nparams);
	
	/**
	 * 修改所有字段
	 * @param entity
	 */
	public void updateAll(T entity);
	
	/**
	 * 只修改变化的字段
	 * @param entity
	 */
	public void updateChange(T entity);
	
	/**
	 * 分页查询结果
	 * @param params 分页查询参数
	 * @param currentPageIndex 当前查询页码
	 * @param pageSize 每页显示数据量
	 * @param nparams 不等查询产生
	 * @return
	 */
	default  MyPagedList<T> pageList(P params,int currentPageIndex,int pageSize,P nparams){
		long totalCount=count(params,nparams);
		List<T> tList=findListByParams(params,currentPageIndex,pageSize,nparams);
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