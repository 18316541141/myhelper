package web.template.aspect;

import java.lang.reflect.Method;
import java.util.Date;
import java.util.HashSet;
import java.util.List;
import java.util.Set;

import org.aspectj.lang.ProceedingJoinPoint;
import org.aspectj.lang.Signature;
import org.aspectj.lang.annotation.After;
import org.aspectj.lang.annotation.AfterThrowing;
import org.aspectj.lang.annotation.Around;
import org.aspectj.lang.annotation.Aspect;
import org.aspectj.lang.annotation.Before;
import org.aspectj.lang.reflect.MethodSignature;
import org.springframework.jdbc.datasource.DataSourceTransactionManager;
import org.springframework.transaction.TransactionDefinition;
import org.springframework.transaction.TransactionStatus;
import org.springframework.transaction.support.DefaultTransactionDefinition;

import com.fasterxml.jackson.databind.ObjectMapper;
import com.txj.common.SnowFlakeHelper;
import com.txj.common.entity.DistributedTransactionPart;
import com.txj.common.intf.IEntity;

import web.template.mapper.common.BaseMapper;
import web.template.mapper.common.DistributedMapper;
import web.template.utils.BeanUtils;

@Aspect
public class DistributedAspect {

	/**
	 * 每个线程都保存一份的事务id
	 */
	public ThreadLocal<Long> transId;

	/**
	 * 每个线程都保存一份的事务层次标识，用于记录事务是否开启、以及事务 外是否包含事务
	 */
	public ThreadLocal<int[]> layer;

	public DistributedAspect() {
		transSet = new HashSet<String>();
		transSet.add("insert");
		transSet.add("delete");
		transSet.add("updateAll");
		transSet.add("updateChange");
		transSet.add("deleteList");
		transSet.add("insertList");
		transId = new ThreadLocal<Long>();
		layer = new ThreadLocal<int[]>();
		layer.set(new int[1]);
	}

	/**
	 * 分布式事务开启前触发
	 */
	@Before("@annotation(web.template.annotation.DistributedTransactional)")
	public void beginTrans() {
		SnowFlakeHelper snowFlakeHelper = BeanUtils.getBean(SnowFlakeHelper.class);
		int[] currentLayer = layer.get();
		if (currentLayer == null || currentLayer.equals(0)) {
			currentLayer[0] = 1;
			transId.set(snowFlakeHelper.nextId());
		} else {
			currentLayer[0]++;
		}
	}

	/**
	 * 需要记录逆向操作的操作。
	 */
	private Set<String> transSet;

	/**
	 * 当操作insert、delete、updateAll、updateChange、deleteList、insertList方法时，
	 * 记录反向操作数据到分布式 事物表中。其他操作时不记录。
	 * 
	 * @param joinPoint
	 * @param params
	 * @throws Throwable
	 */
	@Around("within(web.template.mapper.*Mapper) && args(joinPoint,params)")
	public void mapperAround(ProceedingJoinPoint joinPoint, Object params) throws Throwable {
		Signature sig = joinPoint.getSignature();
		if (!(sig instanceof MethodSignature)) {
			throw new IllegalArgumentException("该注解只能用于方法");
		}
		MethodSignature msig = (MethodSignature) sig;
		Object target = joinPoint.getTarget();
		Method currentMethod = target.getClass().getMethod(msig.getName(), msig.getParameterTypes());
		String methodName = currentMethod.getName();
		if (transSet.contains(methodName)) {
			BaseMapper baseMapper = (BaseMapper) joinPoint;
			DistributedMapper distributedMapper = (DistributedMapper) joinPoint;
			ObjectMapper objectMapper = BeanUtils.getBean(ObjectMapper.class);
			DefaultTransactionDefinition def = new DefaultTransactionDefinition();
			def.setPropagationBehavior(TransactionDefinition.PROPAGATION_REQUIRED);
			DataSourceTransactionManager tx = BeanUtils.getBean(DataSourceTransactionManager.class);
			TransactionStatus status = tx.getTransaction(def);
			try {
				if (methodName.equals("insert")) {
					IEntity entity = (IEntity) params;
					DistributedTransactionPart distributedTransactionPart = createPart(entity);
					distributedTransactionPart.setInverseOperType("d");
					distributedMapper.insertPart(distributedTransactionPart);
				} else if (methodName.equals("delete")) {
					IEntity entity = (IEntity) params;
					DistributedTransactionPart distributedTransactionPart = createPart(entity);
					distributedTransactionPart.setInverseOperType("i");
					distributedTransactionPart
							.setInverseOper(objectMapper.writeValueAsString(baseMapper.findEntity(entity)));
					distributedMapper.insertPart(distributedTransactionPart);
				} else if (methodName.equals("updateAll") || methodName.equals("updateChange")) {
					IEntity entity = (IEntity) params;
					DistributedTransactionPart distributedTransactionPart = createPart(entity);
					distributedTransactionPart.setInverseOperType("u");
					distributedTransactionPart
							.setInverseOper(objectMapper.writeValueAsString(baseMapper.findEntity(entity)));
					distributedMapper.insertPart(distributedTransactionPart);
				} else if (methodName.equals("deleteList")) {
					List<IEntity> entities = (List<IEntity>) params;
					for (IEntity entity : entities) {
						DistributedTransactionPart distributedTransactionPart = createPart(entity);
						distributedTransactionPart.setInverseOperType("I");
						distributedTransactionPart
								.setInverseOper(objectMapper.writeValueAsString(baseMapper.findEntity(entity)));
						distributedMapper.insertPart(distributedTransactionPart);
					}
				} else if (methodName.equals("insertList")) {
					List<IEntity> entities = (List<IEntity>) params;
					for (IEntity entity : entities) {
						DistributedTransactionPart distributedTransactionPart = createPart(entity);
						distributedTransactionPart.setInverseOperType("D");
						distributedMapper.insertPart(distributedTransactionPart);
					}
				}
				Object ret = joinPoint.proceed();
				tx.commit(status);
			} catch (Throwable e) {
				tx.rollback(status);
			}
		} else {
			Object ret = joinPoint.proceed();
		}
	}

	private DistributedTransactionPart createPart(IEntity entity) {
		SnowFlakeHelper snowFlakeHelper = BeanUtils.getBean(SnowFlakeHelper.class);
		DistributedTransactionPart distributedTransactionPart = new DistributedTransactionPart();
		distributedTransactionPart.setCreateDate(new Date());
		distributedTransactionPart.setDistributedTransactionMainId(transId.get());
		distributedTransactionPart.setTransactionStatus((byte) 0);
		distributedTransactionPart.setId(snowFlakeHelper.nextId());
		distributedTransactionPart.setTransTableName(entity.TableName());
		distributedTransactionPart.setTransPrimaryKeyVal(entity.getKey());
		return distributedTransactionPart;
	}

	/**
	 * 分布式事务结束时触发
	 */
	@After("@annotation(web.template.annotation.DistributedTransactional)")
	public void afterTrans() {
		if (--layer.get()[0] == 0) {
			// TODO 在事务总表中插入一条事务成功数据，并在发送事务确认消息到消息队列
			transId.set(null);
		}
	}

	/**
	 * 程序抛出异常时触发
	 */
	@AfterThrowing("@annotation(web.template.annotation.DistributedTransactional)")
	public void throwTrans() {
		if (--layer.get()[0] == 0) {
			// TODO 在事务总表中插入一条事务失败数据，并在发送回滚消息到消息队列
			transId.set(null);
		}
	}
}