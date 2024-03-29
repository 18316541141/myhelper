package web.template.mapper.common;
import java.util.ArrayList;
import java.util.List;
import org.apache.ibatis.annotations.Param;
import com.txj.common.entity.MyPagedList;
/**
 * 操作数据库的mapper
 * @author admin
 *
 * @param <T> 实体类型
 * @param <P> 参数类型
 * @param <SNP> setNull的参数类型
 */
public interface BaseMapper<T,P,SNP> {
	/**
	 * 把指定字段的值设置为null，注：该方法在分布式事务中，事务失败时不会还原。
	 * @param setNullParams
	 * @return
	 */
	public int setNull(SNP setNullParams);
	
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
	public void delete(T t);
	
	/**
	 * 根据主键删除一批对象，仅在数量不多的情况下使用
	 * 该方法删除，如果数量过多需要使用deleteBigList删除
	 * @param keys
	 */
	public void deleteList(List<T> keys);
	
	/**
	 * 根据条件删除对象，注：该方法在分布式事务中，事务失败时不会还原。
	 * @param params 删除条件
	 * @param nparams 不等删除条件
	 */
	public void deleteByParams(@Param("params")P params,@Param("nparams")P nparams);
	
	/**
	 * 根据主键查找一个实体
	 * @return
	 */
	public T findEntity(T entity);

	/**
	 * 根据in(主键...)的方式查询实体
	 * @param keys 只填写了主键的查询参数集合
	 */
	public List<T> findEntitiesByIn(List<P> keys);
	
	/**
	 * 根据条件查找一个实体
	 * @param params 查询条件
	 * @return 返回第一个符合条件的实体
	 */
	default T findEntityByParams(P params){
		return findEntityByParams(params, null);
	}
	
	/**
	 * 根据条件查找一个实体
	 * @param params 查询条件
	 * @param nparams 不等查询条件
	 * @return 返回第一个符合条件的实体
	 */
	public T findEntityByParams(@Param("params")P params, @Param("nparams")P nparams);
	
	/**
	 * 根据条件查询符合条件的数据量
	 * @param params 查询条件
	 * @param nparams 不等查询条件
	 * @return
	 */
	public int count(@Param("params")P params, @Param("nparams")P nparams);
	
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
	 * 修改所有字段
	 * @param entity
	 */
	default void updateAllList(List<T> entities){
		for(T t:entities){
			updateAll(t);
		}
	}
	
	/**
	 * 只修改变化的字段
	 * @param entity
	 */
	public void updateChange(T entity);
	
	/**
	 * 只修改变化的字段
	 * @param entity
	 */
	default void updateChangeList(List<T> entities){
		for(T t:entities){
			updateChange(t);
		}
	}
	
	/**
	 * 普通分页查询结果
	 * @param params 分页查询参数
	 * @param currentPageIndex 当前查询页码
	 * @param pageSize 每页显示数据量，当大于100时自动修正成100
	 * @param nparams 不等查询产生
	 * @return
	 */
	default MyPagedList<T> pageList(P params,int currentPageIndex,int pageSize,P nparams){
		if(currentPageIndex<=0){
			currentPageIndex=1;
		}
		if(pageSize>100){
			pageSize=100;
		} else if(pageSize<=0){
			pageSize=20;
		}
		int totalCount=count(params,nparams);
		int totalPageCount=((totalCount-totalCount%pageSize)/pageSize)+1;
		if(currentPageIndex>=totalPageCount){
			currentPageIndex=Math.max(totalPageCount,1);
		}
		List<T> tList=findListByParams(params,currentPageIndex,pageSize,nparams);
		MyPagedList<T> myPagedList=new MyPagedList<>();
		myPagedList.setPageDataList(tList);
		myPagedList.setPageSize(pageSize);
		myPagedList.setTotalItemCount(totalCount);
		myPagedList.setTotalPageCount(totalPageCount);
		myPagedList.setCurrentPageIndex(currentPageIndex);
		myPagedList.setStartItemIndex((currentPageIndex-1)*pageSize+1);
		myPagedList.setEndItemIndex(Math.min(totalCount, currentPageIndex*pageSize));
		return myPagedList;
	}

	/**
	 * 普通分页查询结果
	 * @param params 分页查询参数
	 * @param currentPageIndex 当前查询页码
	 * @param pageSize 每页显示数据量，当大于100时自动修正成100
	 * @return
	 */
	default MyPagedList<T> pageList(P params,int currentPageIndex,int pageSize){
		return pageList(params,currentPageIndex,pageSize,null);
	}

	/**
	 * 大分页查询结果，该查询方法是用于导出功能或一些需要查询出大量数据的功能
	 * @param params 分页查询参数
	 * @param currentPageIndex 当前查询页码
	 * @param pageSize 每页显示数据量，当大于10000时自动修正成10000
	 * @param nparams 不等查询产生
	 * @return
	 */
	default MyPagedList<T> bigPageList(P params,int currentPageIndex,int pageSize,P nparams){
		if(currentPageIndex<=0){
			currentPageIndex=1;
		}
		if(pageSize>10000 || pageSize<=0){
			pageSize=10000;
		}
		int totalCount=count(params,nparams);
		int totalPageCount=((totalCount-totalCount%pageSize)/pageSize)+1;
		if(currentPageIndex >= totalPageCount){
			currentPageIndex = Math.max(totalPageCount,1);
		}
		List<T> tList=findListByParams(params,currentPageIndex,pageSize,nparams);
		MyPagedList<T> myPagedList=new MyPagedList<>();
		myPagedList.setPageDataList(tList);
		myPagedList.setPageSize(pageSize);
		myPagedList.setTotalItemCount(totalCount);
		myPagedList.setTotalPageCount(totalPageCount);
		myPagedList.setCurrentPageIndex(currentPageIndex);
		myPagedList.setStartItemIndex((currentPageIndex-1)*pageSize+1);
		myPagedList.setEndItemIndex(Math.min(totalCount, currentPageIndex*pageSize));
		return myPagedList;
	}

	/**
	 * 大分页查询结果，该查询方法是用于导出功能或一些需要查询出大量数据的功能
	 * @param params 分页查询参数
	 * @param currentPageIndex 当前查询页码
	 * @param pageSize 每页显示数据量，当大于10000时自动修正成10000
	 * @return
	 */
	default MyPagedList<T> bigPageList(P params,int currentPageIndex,int pageSize){
		return pageList(params,currentPageIndex,pageSize,null);
	}
}