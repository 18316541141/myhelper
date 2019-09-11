package web.template.aspect;

import java.lang.reflect.Method;
import java.util.Date;

import org.aspectj.lang.ProceedingJoinPoint;
import org.aspectj.lang.Signature;
import org.aspectj.lang.annotation.After;
import org.aspectj.lang.annotation.AfterThrowing;
import org.aspectj.lang.annotation.Around;
import org.aspectj.lang.annotation.Aspect;
import org.aspectj.lang.annotation.Before;
import org.aspectj.lang.reflect.MethodSignature;

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

	@Around("within(web.template.mapper.*Mapper) && args(joinPoint,params)")
	public void mapperAround(ProceedingJoinPoint joinPoint,Object params) throws Throwable {
		Signature sig = joinPoint.getSignature();
		if (!(sig instanceof MethodSignature)) {
			throw new IllegalArgumentException("该注解只能用于方法");
		}
		MethodSignature msig = (MethodSignature) sig;
		Object target = joinPoint.getTarget();
		Method currentMethod = target.getClass().getMethod(msig.getName(), msig.getParameterTypes());
		String methodName=currentMethod.getName();
		BaseMapper baseMapper=(BaseMapper)joinPoint;
		DistributedMapper distributedMapper =(DistributedMapper)joinPoint;
		SnowFlakeHelper snowFlakeHelper = BeanUtils.getBean(SnowFlakeHelper.class);
		
		DistributedTransactionPart distributedTransactionPart=new DistributedTransactionPart();
		distributedTransactionPart.setCreateDate(new Date());
		distributedTransactionPart.setDistributedTransactionMainId(transId.get());
		distributedTransactionPart.setInverseOper("d");
		distributedTransactionPart.setTransactionStatus((byte)0);
		if(methodName.equals("insert")){
			IEntity entity =(IEntity) params;
			distributedTransactionPart.setId(snowFlakeHelper.nextId());
			distributedTransactionPart.setTransTableName(entity.TableName());
			distributedTransactionPart.setTransPrimaryKeyVal(entity.getKey());
			distributedMapper.insertPart(distributedTransactionPart);
		}else if(methodName.equals("insertList")){
			
		}else if(methodName.equals("delete")){
			
		}else if(methodName.equals("deleteList")){
			
		}else if(methodName.equals("updateAll")){
			
		}else if(methodName.equals("updateChange")){
			
		}
		
		Object ret = joinPoint.proceed();
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