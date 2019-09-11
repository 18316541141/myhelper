package com.txj.common.intf;

/**
 * 使用分布式事务的实体类必须继承该接口
 * @author admin
 */
public interface IEntity {
	
	/**
	 * 返回该实体类的主键值
	 * @return
	 */
	Long getKey();
	
	/**
	 * 设置该实体类的主键值
	 * @param key
	 */
	void setKey(Long key);
	
	/**
	 * 获取该实体类的表名
	 * @return
	 */
    String TableName();
}
