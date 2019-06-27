package web.template.mapper;

import java.util.List;

public interface BaseMapper<T> {
	/**
	 * 插入一条记录，返回主键
	 * @param t	插入记录的实体类
	 * @return
	 */
	public void insert(T entity);
	
	/**
	 * 插入一批数据
	 * @param entities	插入数据
	 */
	public void insertList(List<T> entities);
	
	/**
	 * 根据主键删除数据
	 * @param entity
	 */
	public void delete(T key);
	
	/**
	 * 根据主键删除一批对象
	 * @param keys
	 */
	public void deleteList(List<T> keys);
	
	/**
	 * 根据条件删除对象
	 * @param params
	 */
	public void deleteByParams(Object params);
	
	/**
	 * 根据主键查找一个实体
	 * @return
	 */
	public T findEntity(Object key);
	
	/**
	 * 根据条件查找一个实体
	 * @param params
	 * @return
	 */
	public T findEntityByParams(Object params);
	
	/**
	 * 根据条件查询符合条件的数据量
	 * @param params
	 * @return
	 */
	public long count(Object params);
	
	/**
	 * 根据条件查询列表
	 * @param params
	 * @return
	 */
	public List<T> findListByParams(Object params);
	
	
}