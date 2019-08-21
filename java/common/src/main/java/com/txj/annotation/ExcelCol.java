package com.txj.annotation;

import java.lang.annotation.Documented;
import java.lang.annotation.ElementType;
import java.lang.annotation.Retention;
import java.lang.annotation.RetentionPolicy;
import java.lang.annotation.Target;

/**
 * excel表格的列名称属性
 * @author admin
 */
@Target(ElementType.FIELD)
@Retention(RetentionPolicy.RUNTIME)
@Documented
public @interface ExcelCol {
	/**
	 * 列名称
	 * @return
	 */
	String[] colNames();

	/**
	 * 列索引，从0开始
	 * @return
	 */
    int[] colIndexs();

    /**
     * 组名称
     * @return
     */
    String[] groupNames();
}
