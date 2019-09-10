package com.txj.common.entity;

import java.util.Date;

/**
 * 分布式事务分表
 * 
 * @author admin
 */
public class DistributedTransactionPart {
	/**
	 * 主键id
	 */
	public Long id;

	/**
	 * 事务id
	 */
	public Long distributedTransactionMainId;

	/**
	 * 逆向操作的数据，当InverseOperType=D时，InverseOper记录逆操作的数据主键，
	 * 当InverseOperType=I时，InverseOper记录逆操作的数据主键，
	 * 当InverseOperType=A时，InverseOper记录更新前的json数据，
	 * 当InverseOperType=C时，InverseOper记录更新前的部分json数据，
	 */
	public String inverseOper;

	/**
	 * 逆向操作类型，当本次事务为插入操作时，逆向操作类型是：d（Delete）、 当本次事务为批量插入操作时，逆向操作类型是：D（Delete）、
	 * 当本次事务为删除操作时，逆向操作类型是：i（Insert）、 当本次事务为批量删除操作时，逆向操作类型是：I（Insert）、
	 * 当本次事务为更新操作时，逆向操作类型是：u（UpdateAll）、 当本次事务为批量更新操作时，逆向操作类型是：U（UpdateAll）、
	 */
	public String inverseOperType;

	/**
	 * 涉及事务的表
	 */
	public String transTableName;

	/**
	 * 涉及事务的主键值
	 */
	public Long transPrimaryKeyVal;

	/**
	 * 事务提交状态：0（已保存，未完成）、1（已保存、已完成）、2（事务失败）
	 */
	public Byte transactionStatus;

	/**
	 * 创建日期
	 */
	public Date createDate;

	public Long getId() {
		return id;
	}

	public void setId(Long id) {
		this.id = id;
	}

	public Long getDistributedTransactionMainId() {
		return distributedTransactionMainId;
	}

	public void setDistributedTransactionMainId(Long distributedTransactionMainId) {
		this.distributedTransactionMainId = distributedTransactionMainId;
	}

	public String getInverseOper() {
		return inverseOper;
	}

	public void setInverseOper(String inverseOper) {
		this.inverseOper = inverseOper;
	}

	public String getInverseOperType() {
		return inverseOperType;
	}

	public void setInverseOperType(String inverseOperType) {
		this.inverseOperType = inverseOperType;
	}

	public String getTransTableName() {
		return transTableName;
	}

	public void setTransTableName(String transTableName) {
		this.transTableName = transTableName;
	}

	public Long getTransPrimaryKeyVal() {
		return transPrimaryKeyVal;
	}

	public void setTransPrimaryKeyVal(Long transPrimaryKeyVal) {
		this.transPrimaryKeyVal = transPrimaryKeyVal;
	}

	public Byte getTransactionStatus() {
		return transactionStatus;
	}

	public void setTransactionStatus(Byte transactionStatus) {
		this.transactionStatus = transactionStatus;
	}

	public Date getCreateDate() {
		return createDate;
	}

	public void setCreateDate(Date createDate) {
		this.createDate = createDate;
	}
}