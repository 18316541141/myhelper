package web.template.mapper.common;
import java.util.ArrayList;
import java.util.List;
import org.apache.ibatis.annotations.Param;
import com.txj.common.entity.MyPagedList;
/**
 * 操作内存数据表的mapper
 * 
 * @author admin
 *
 * @param <T>
 *            实体类型
 * @param
 * 			<P>
 *            参数类型
 * @param <SNP>
 *            setNull的参数类型
 */
public abstract class BaseMemoryMapper<T, P, SNP> {

	private List<T> dataSrcList;

	public BaseMemoryMapper() {
		dataSrcList = new ArrayList<T>();
	}

	/**
	 * 把指定字段的值设置为null，
	 * 
	 * @param setNullParams
	 * @return
	 */
	public void setNull(T entity, SNP setNullParams) {
		synchronized (dataSrcList) {
			int index = dataSrcList.indexOf(entity);
			if (index > -1) {
				setNullOper(dataSrcList.get(index), setNullParams);
			}
		}
	}

	/**
	 * 具体需要设置为null的字段
	 * 
	 * @param entity
	 * @param setNullParams
	 */
	protected abstract void setNullOper(T entity, SNP setNullParams);

	/**
	 * 插入一条记录，返回主键
	 * 
	 * @param t
	 *            插入记录的实体类
	 * @return
	 */
	public void insert(T entity) {
		synchronized (dataSrcList) {
			int index = dataSrcList.indexOf(entity);
			if (index == -1) {
				dataSrcList.add(entity);
			} else {
				throw new RuntimeException("已有相同主键的记录，操作失败！");
			}
		}
	}

	/**
	 * 插入一批数据
	 * 
	 * @param entities
	 *            插入数据
	 */
	public void insertList(List<T> entities) {
		synchronized (dataSrcList) {
			for (T entity : entities) {
				if (dataSrcList.indexOf(entity) > -1) {
					throw new RuntimeException("已有相同主键的记录，操作失败！");
				}
			}
			dataSrcList.addAll(entities);
		}
	}

	/**
	 * 根据主键删除数据
	 * 
	 * @param entity
	 */
	public void delete(T entity) {
		synchronized (dataSrcList) {
			int index = dataSrcList.indexOf(entity);
			if (index > -1) {
				dataSrcList.remove(index);
			}
		}
	}

	/**
	 * 根据主键删除一批对象，仅在数量不多的情况下使用 该方法删除，如果数量过多需要使用deleteBigList删除
	 * 
	 * @param keys
	 */
	public void deleteList(List<T> entities) {
		for (T entity : entities) {
			delete(entity);
		}
	}

	/**
	 * 根据条件删除对象，
	 * 
	 * @param params
	 *            删除条件
	 * @param nparams
	 *            不等删除条件
	 */
	public void deleteByParams(@Param("params") P params, @Param("nparams") P nparams) {
		synchronized (dataSrcList) {
			for (int i = dataSrcList.size() - 1; i >= 0; i--) {
				if (isTarget(dataSrcList.get(i),params, nparams)) {
					dataSrcList.remove(i);
				}
			}
		}
	}

	/**
	 * 根据主键查找一个实体
	 * 
	 * @return
	 */
	public T findEntity(T entity) {
		int index = dataSrcList.indexOf(entity);
		synchronized (dataSrcList) {
			if (index == -1) {
				return null;
			} else {
				return dataSrcList.get(index);
			}
		}
	}

	/**
	 * 根据in(主键...)的方式查询实体
	 * 
	 * @param keys
	 *            只填写了主键的查询参数集合
	 */
	public List<T> findEntitiesByIn(List<T> entities) {
		List<T> retList = new ArrayList<T>();
		synchronized (dataSrcList) {
			for (T entity : entities) {
				retList.add(findEntity(entity));
			}
			return retList;
		}
	}

	/**
	 * 根据条件查找一个实体
	 * 
	 * @param params
	 *            查询条件
	 * @param nparams
	 *            不等查询条件
	 * @return
	 */
	public T findEntityByParams(P params, P nparams) {
		synchronized (dataSrcList) {
			for (T t : dataSrcList) {
				if (isTarget(t, params, nparams)) {
					return t;
				}
			}
		}
		return null;
	}

	/**
	 * 检查实体类是否符合参数的条件，
	 * 符合条件说明就是查询的目标
	 * @param entity
	 * @param params
	 * @param nparams
	 * @return
	 */
	protected abstract boolean isTarget(T entity, P params, P nparams);

	/**
	 * 根据条件查询符合条件的数据量
	 * 
	 * @param params
	 *            查询条件
	 * @param nparams
	 *            不等查询条件
	 * @return
	 */
	public int count(@Param("params") P params, @Param("nparams") P nparams) {
		int i = 0;
		synchronized (dataSrcList) {
			for (T t : dataSrcList) {
				if (isTarget(t, params, nparams)) {
					i++;
				}
			}
			return i;
		}
	}

	/**
	 * 根据条件查询所有符合条件的数据
	 * 
	 * @param params
	 *            查询条件
	 * @param nparams
	 *            不等查询条件
	 * @return
	 */
	public List<T> findAllByParams(@Param("params") P params, @Param("nparams") P nparams) {
		List<T> retList = new ArrayList<T>();
		synchronized (dataSrcList) {
			for (T t : dataSrcList) {
				if (isTarget(t, params, nparams)) {
					retList.add(t);
				}
			}
			return retList;
		}
	}

	/**
	 * 根据条件查询列表，分页查询
	 * 
	 * @param params
	 *            查询条件
	 * @param currentPageIndex
	 *            当前页码
	 * @param pageSize
	 *            每页显示数据量
	 * @param nparams
	 *            不等查询条件
	 * @return
	 */
	public List<T> findListByParams(P params, P nparams) {
		List<T> retList = new ArrayList<T>();
		synchronized (dataSrcList) {
			for (T entity : dataSrcList) {
				if (isTarget(entity, params, nparams)) {
					retList.add(entity);
				}
			}
			return retList;
		}
	}

	/**
	 * 修改所有字段
	 * 
	 * @param entity
	 */
	public void updateAll(T entity) {
		synchronized (dataSrcList) {
			int index = dataSrcList.indexOf(entity);
			if (index > -1) {
				dataSrcList.set(index, entity);
			}
		}
	}

	/**
	 * 修改所有字段
	 * 
	 * @param entity
	 */
	public void updateAllList(List<T> entities) {
		synchronized (dataSrcList) {
			for (T t : entities) {
				updateAll(t);
			}
		}
	}

	/**
	 * 只修改变化的字段
	 * 
	 * @param entity
	 */
	public void updateChange(T entity) {
		synchronized (dataSrcList) {
			int index = dataSrcList.indexOf(entity);
			if (index > -1) {
				updateChangeOper(dataSrcList.get(index), entity);
			}
		}
	}

	/**
	 * 修改变化的字段
	 * 
	 * @param src
	 * @param entity
	 */
	protected abstract void updateChangeOper(T src, T entity);

	/**
	 * 只修改变化的字段
	 * 
	 * @param entity
	 */
	public void updateChangeList(List<T> entities) {
		synchronized (dataSrcList) {
			for (T t : entities) {
				updateChange(t);
			}
		}
	}

	/**
	 * 普通分页查询结果
	 * 
	 * @param params
	 *            分页查询参数
	 * @param currentPageIndex
	 *            当前查询页码
	 * @param pageSize
	 *            每页显示数据量，当大于100时自动修正成100
	 * @param nparams
	 *            不等查询产生
	 * @return
	 */
	public MyPagedList<T> pageList(P params, int currentPageIndex, int pageSize, P nparams) {
		if (currentPageIndex <= 0) {
			currentPageIndex = 1;
		}
		if (pageSize > 100) {
			pageSize = 100;
		} else if (pageSize <= 0) {
			pageSize = 20;
		}
		synchronized (dataSrcList) {
			List<T> tList = findListByParams(params, nparams);
			int totalCount = tList.size();
			int totalPageCount = ((totalCount - totalCount % pageSize) / pageSize) + 1;
			if (currentPageIndex >= totalPageCount) {
				currentPageIndex = Math.max(totalPageCount, 1);
			}
			int startIndex = (currentPageIndex - 1) * pageSize;
			tList = tList.subList(startIndex, Math.min(startIndex + pageSize, totalCount));
			MyPagedList<T> myPagedList = new MyPagedList<>();
			myPagedList.setPageDataList(tList);
			myPagedList.setPageSize(pageSize);
			myPagedList.setTotalItemCount(totalCount);
			myPagedList.setTotalPageCount(totalPageCount);
			myPagedList.setCurrentPageIndex(currentPageIndex);
			myPagedList.setStartItemIndex((currentPageIndex - 1) * pageSize + 1);
			myPagedList.setEndItemIndex(Math.min(totalCount, currentPageIndex * pageSize));
			return myPagedList;
		}
	}

	/**
	 * 普通分页查询结果
	 * 
	 * @param params
	 *            分页查询参数
	 * @param currentPageIndex
	 *            当前查询页码
	 * @param pageSize
	 *            每页显示数据量，当大于100时自动修正成100
	 * @return
	 */
	public MyPagedList<T> pageList(P params, int currentPageIndex, int pageSize) {
		return pageList(params, currentPageIndex, pageSize, null);
	}
}