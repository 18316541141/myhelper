package web.template.mapper.common;

import java.util.List;

import com.txj.common.entity.DistributedTransactionPart;

/**
 * 分布式分表的数据操作
 * @author admin
 */
public interface DistributedMapper {
	
	/**
	 * 插入一条记录到分布式分表中
	 * @param distributedTransactionPart
	 */
	void insertPart(DistributedTransactionPart distributedTransactionPart);
	
	/**
	 * 查询符合条件的分布式分表数据
	 * @param params
	 * @return
	 */
	List<DistributedTransactionPart> selectPart(DistributedTransactionPart params);
	
	/**
	 * 查询第一个符合条件的分布式分表数据
	 * @param params
	 * @return
	 */
	default DistributedTransactionPart selectFirstPart(DistributedTransactionPart params){
		List<DistributedTransactionPart> distributedTransactionPartList = selectPart(params);
		if(distributedTransactionPartList.size()>0){
			return distributedTransactionPartList.get(0);
		}else{
			return null;
		}
	}
}