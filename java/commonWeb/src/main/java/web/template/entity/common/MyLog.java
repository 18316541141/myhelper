package web.template.entity.common;

import java.io.PrintWriter;

import org.apache.commons.io.IOUtils;
import org.apache.logging.log4j.Level;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import org.apache.logging.log4j.Marker;
import org.apache.logging.log4j.message.EntryMessage;
import org.apache.logging.log4j.message.Message;
import org.apache.logging.log4j.message.MessageFactory;
import org.apache.logging.log4j.util.MessageSupplier;
import org.apache.logging.log4j.util.Supplier;
import org.slf4j.MDC;
import com.txj.common.SnowFlakeHelper;

public class MyLog implements Logger {

	private Logger log;

	private SnowFlakeHelper snowFlakeHelper;

	private String projectName;

	public void setSnowFlakeHelper(SnowFlakeHelper snowFlakeHelper) {
		this.snowFlakeHelper = snowFlakeHelper;
	}

	private MyLog(Class<?> clazz) {
		this.log = LogManager.getLogger(clazz);
	}

	public static MyLog getLogger(Class<?> clazz) {
		return new MyLog(clazz);
	}

	public String getProjectName() {
		return projectName;
	}

	public void setProjectName(String projectName) {
		this.projectName = projectName;
	}

	@Override
	public void catching(Throwable arg0) {
		log.catching(arg0);
	}

	@Override
	public void catching(Level arg0, Throwable arg1) {
		log.catching(arg0, arg1);
	}

	@Override
	public void debug(Message arg0) {
		log.debug(arg0);
	}

	@Override
	public void debug(MessageSupplier arg0) {
		log.debug(arg0);
	}

	@Override
	public void debug(CharSequence arg0) {
		log.debug(arg0);
	}

	@Override
	public void debug(Object arg0) {
		log.debug(arg0);
	}

	@Override
	public void debug(String arg0) {
		log.debug(arg0);
	}

	@Override
	public void debug(Supplier<?> arg0) {
		log.debug(arg0);
	}

	@Override
	public void debug(Marker arg0, Message arg1) {
		log.debug(arg0, arg1);
	}

	@Override
	public void debug(Marker arg0, MessageSupplier arg1) {
		log.debug(arg0, arg1);
	}

	@Override
	public void debug(Marker arg0, CharSequence arg1) {
		log.debug(arg0, arg1);
	}

	@Override
	public void debug(Marker arg0, Object arg1) {
		log.debug(arg0, arg1);
	}

	@Override
	public void debug(Marker arg0, String arg1) {
		log.debug(arg0, arg1);
	}

	@Override
	public void debug(Marker arg0, Supplier<?> arg1) {
		log.debug(arg0, arg1);
	}

	@Override
	public void debug(Message arg0, Throwable arg1) {
		log.debug(arg0, arg1);
	}

	@Override
	public void debug(MessageSupplier arg0, Throwable arg1) {
		log.debug(arg0, arg1);
	}

	@Override
	public void debug(CharSequence arg0, Throwable arg1) {
		log.debug(arg0, arg1);
	}

	@Override
	public void debug(Object arg0, Throwable arg1) {
		log.debug(arg0, arg1);
	}

	@Override
	public void debug(String arg0, Object... arg1) {
		log.debug(arg0, arg1);
	}

	@Override
	public void debug(String arg0, Supplier<?>... arg1) {
		log.debug(arg0, arg1);
	}

	@Override
	public void debug(String arg0, Throwable arg1) {
		log.debug(arg0, arg1);
	}

	@Override
	public void debug(Supplier<?> arg0, Throwable arg1) {
		log.debug(arg0, arg1);
	}

	@Override
	public void debug(String arg0, Object arg1) {
		log.debug(arg0, arg1);
	}

	@Override
	public void debug(Marker arg0, Message arg1, Throwable arg2) {
		log.debug(arg0, arg1, arg2);
	}

	@Override
	public void debug(Marker arg0, MessageSupplier arg1, Throwable arg2) {
		log.debug(arg0, arg1, arg2);
	}

	@Override
	public void debug(Marker arg0, CharSequence arg1, Throwable arg2) {
		log.debug(arg0, arg1, arg2);
	}

	@Override
	public void debug(Marker arg0, Object arg1, Throwable arg2) {
		log.debug(arg0, arg1, arg2);
	}

	@Override
	public void debug(Marker arg0, String arg1, Object... arg2) {
		log.debug(arg0, arg1, arg2);
	}

	@Override
	public void debug(Marker arg0, String arg1, Supplier<?>... arg2) {
		log.debug(arg0, arg1, arg2);
	}

	@Override
	public void debug(Marker arg0, String arg1, Throwable arg2) {
		log.debug(arg0, arg1, arg2);
	}

	@Override
	public void debug(Marker arg0, Supplier<?> arg1, Throwable arg2) {
		log.debug(arg0, arg1, arg2);
	}

	@Override
	public void debug(Marker arg0, String arg1, Object arg2) {
		log.debug(arg0, arg1, arg2);
	}

	@Override
	public void debug(String arg0, Object arg1, Object arg2) {
		log.debug(arg0, arg1, arg2);
	}

	@Override
	public void debug(Marker arg0, String arg1, Object arg2, Object arg3) {
		log.debug(arg0, arg1, arg2, arg3);
	}

	@Override
	public void debug(String arg0, Object arg1, Object arg2, Object arg3) {
		log.debug(arg0, arg1, arg2, arg3);
	}

	@Override
	public void debug(Marker arg0, String arg1, Object arg2, Object arg3, Object arg4) {
		log.debug(arg0, arg1, arg2, arg3, arg4);
	}

	@Override
	public void debug(String arg0, Object arg1, Object arg2, Object arg3, Object arg4) {
		log.debug(arg0, arg1, arg2, arg3, arg4);
	}

	@Override
	public void debug(Marker arg0, String arg1, Object arg2, Object arg3, Object arg4, Object arg5) {
		log.debug(arg0, arg1, arg2, arg3, arg4, arg5);
	}

	@Override
	public void debug(String arg0, Object arg1, Object arg2, Object arg3, Object arg4, Object arg5) {
		log.debug(arg0, arg1, arg2, arg3, arg4, arg5);
	}

	@Override
	public void debug(Marker arg0, String arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6) {
		log.debug(arg0, arg1, arg2, arg3, arg4, arg5, arg6);
	}

	@Override
	public void debug(String arg0, Object arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6) {
		log.debug(arg0, arg1, arg2, arg3, arg4, arg5, arg6);
	}

	@Override
	public void debug(Marker arg0, String arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7) {
		log.debug(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
	}

	@Override
	public void debug(String arg0, Object arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7) {
		log.debug(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
	}

	@Override
	public void debug(Marker arg0, String arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7, Object arg8) {
		log.debug(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
	}

	@Override
	public void debug(String arg0, Object arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7, Object arg8) {
		log.debug(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
	}

	@Override
	public void debug(Marker arg0, String arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7, Object arg8, Object arg9) {
		log.debug(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
	}

	@Override
	public void debug(String arg0, Object arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7, Object arg8, Object arg9) {
		log.debug(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
	}

	@Override
	public void debug(Marker arg0, String arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7, Object arg8, Object arg9, Object arg10) {
		log.debug(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg10);
	}

	@Override
	public void debug(String arg0, Object arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7, Object arg8, Object arg9, Object arg10) {
		log.debug(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg10);
	}

	@Override
	public void debug(Marker arg0, String arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7, Object arg8, Object arg9, Object arg10, Object arg11) {
		log.debug(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg10, arg11);
	}

	@Override
	public void entry() {
		log.entry();
	}

	@Override
	public void entry(Object... arg0) {
		log.entry(arg0);
	}

	private void initSnow() {
		initSnow(null);
	}

	private void initSnow(Throwable t) {
		StackTraceElement[] stackTraces=Thread.currentThread().getStackTrace();
		MDC.put("FUNC_NAME", stackTraces[3].getMethodName());
		if(t==null){
		}else{
			StringBuilder sb=new StringBuilder();
			for(int i=0,len=t.getStackTrace().length;i<len;i++){
				sb.append(t.getStackTrace()[i].toString()).append("\r\n");
			}
			MDC.put("EXCEPTION", sb.toString());			
		}
		MDC.put("PROJECT_NAME", projectName);
		MDC.put("ID", String.valueOf(snowFlakeHelper.nextId()));
	}

	@Override
	public void error(Message arg0) {
		initSnow();
		log.error(arg0);
	}

	@Override
	public void error(MessageSupplier arg0) {
		initSnow();
		log.error(arg0);
	}

	@Override
	public void error(CharSequence arg0) {
		initSnow();
		log.error(arg0);
	}

	@Override
	public void error(Object arg0) {
		initSnow();
		log.error(arg0);
	}

	@Override
	public void error(String arg0) {
		initSnow();
		log.error(arg0);
	}

	@Override
	public void error(Supplier<?> arg0) {
		initSnow();
		log.error(arg0);
	}

	@Override
	public void error(Marker arg0, Message arg1) {
		initSnow();
		log.error(arg0, arg1);
	}

	@Override
	public void error(Marker arg0, MessageSupplier arg1) {
		initSnow();
		log.error(arg0, arg1);
	}

	@Override
	public void error(Marker arg0, CharSequence arg1) {
		initSnow();
		log.error(arg0, arg1);
	}

	@Override
	public void error(Marker arg0, Object arg1) {
		initSnow();
		log.error(arg0, arg1);
	}

	@Override
	public void error(Marker arg0, String arg1) {
		initSnow();
		log.error(arg0, arg1);
	}

	@Override
	public void error(Marker arg0, Supplier<?> arg1) {
		initSnow();
		log.error(arg0, arg1);
	}

	@Override
	public void error(Message arg0, Throwable arg1) {
		initSnow(arg1);
		log.error(arg0, arg1);
	}

	@Override
	public void error(MessageSupplier arg0, Throwable arg1) {
		initSnow(arg1);
		log.error(arg0, arg1);
	}

	@Override
	public void error(CharSequence arg0, Throwable arg1) {
		initSnow(arg1);
		log.error(arg0, arg1);
	}

	@Override
	public void error(Object arg0, Throwable arg1) {
		initSnow(arg1);
		log.error(arg0, arg1);
	}

	@Override
	public void error(String arg0, Object... arg1) {
		initSnow();
		log.error(arg0, arg1);
	}

	@Override
	public void error(String arg0, Supplier<?>... arg1) {
		initSnow();
		log.error(arg0, arg1);
	}

	@Override
	public void error(String arg0, Throwable arg1) {
		initSnow(arg1);
		log.error(arg0, arg1);
	}

	@Override
	public void error(Supplier<?> arg0, Throwable arg1) {
		initSnow(arg1);
		log.error(arg0, arg1);
	}

	@Override
	public void error(String arg0, Object arg1) {
		initSnow();
		log.error(arg0, arg1);
	}

	@Override
	public void error(Marker arg0, Message arg1, Throwable arg2) {
		initSnow(arg2);
		log.error(arg0, arg1, arg2);
	}

	@Override
	public void error(Marker arg0, MessageSupplier arg1, Throwable arg2) {
		initSnow(arg2);
		log.error(arg0, arg1, arg2);
	}

	@Override
	public void error(Marker arg0, CharSequence arg1, Throwable arg2) {
		initSnow(arg2);
		log.error(arg0, arg1, arg2);
	}

	@Override
	public void error(Marker arg0, Object arg1, Throwable arg2) {
		initSnow(arg2);
		log.error(arg0, arg1, arg2);
	}

	@Override
	public void error(Marker arg0, String arg1, Object... arg2) {
		initSnow();
		log.error(arg0, arg1, arg2);
	}

	@Override
	public void error(Marker arg0, String arg1, Supplier<?>... arg2) {
		initSnow();
		log.error(arg0, arg1, arg2);
	}

	@Override
	public void error(Marker arg0, String arg1, Throwable arg2) {
		initSnow(arg2);
		log.error(arg0, arg1, arg2);
	}

	@Override
	public void error(Marker arg0, Supplier<?> arg1, Throwable arg2) {
		initSnow(arg2);
		log.error(arg0, arg1, arg2);
	}

	@Override
	public void error(Marker arg0, String arg1, Object arg2) {
		initSnow();
		log.error(arg0, arg1, arg2);
	}

	@Override
	public void error(String arg0, Object arg1, Object arg2) {
		initSnow();
		log.error(arg0, arg1, arg2);
	}

	@Override
	public void error(Marker arg0, String arg1, Object arg2, Object arg3) {
		initSnow();
		log.error(arg0, arg1, arg2, arg3);
	}

	@Override
	public void error(String arg0, Object arg1, Object arg2, Object arg3) {
		initSnow();
		log.error(arg0, arg1, arg2, arg3);
	}

	@Override
	public void error(Marker arg0, String arg1, Object arg2, Object arg3, Object arg4) {
		initSnow();
		log.error(arg0, arg1, arg2, arg3, arg4);
	}

	@Override
	public void error(String arg0, Object arg1, Object arg2, Object arg3, Object arg4) {
		initSnow();
		log.error(arg0, arg1, arg2, arg3, arg4);
	}

	@Override
	public void error(Marker arg0, String arg1, Object arg2, Object arg3, Object arg4, Object arg5) {
		initSnow();
		log.error(arg0, arg1, arg2, arg3, arg4, arg5);
	}

	@Override
	public void error(String arg0, Object arg1, Object arg2, Object arg3, Object arg4, Object arg5) {
		initSnow();
		log.error(arg0, arg1, arg2, arg3, arg4, arg5);
	}

	@Override
	public void error(Marker arg0, String arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6) {
		initSnow();
		log.error(arg0, arg1, arg2, arg3, arg4, arg5, arg6);
	}

	@Override
	public void error(String arg0, Object arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6) {
		initSnow();
		log.error(arg0, arg1, arg2, arg3, arg4, arg5, arg6);
	}

	@Override
	public void error(Marker arg0, String arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7) {
		initSnow();
		log.error(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
	}

	@Override
	public void error(String arg0, Object arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7) {
		initSnow();
		log.error(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
	}

	@Override
	public void error(Marker arg0, String arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7, Object arg8) {
		initSnow();
		log.error(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
	}

	@Override
	public void error(String arg0, Object arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7, Object arg8) {
		initSnow();
		log.error(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
	}

	@Override
	public void error(Marker arg0, String arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7, Object arg8, Object arg9) {
		initSnow();
		log.error(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
	}

	@Override
	public void error(String arg0, Object arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7, Object arg8, Object arg9) {
		initSnow();
		log.error(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
	}

	@Override
	public void error(Marker arg0, String arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7, Object arg8, Object arg9, Object arg10) {
		initSnow();
		log.error(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
	}

	@Override
	public void error(String arg0, Object arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7, Object arg8, Object arg9, Object arg10) {
		initSnow();
		log.error(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
	}

	@Override
	public void error(Marker arg0, String arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7, Object arg8, Object arg9, Object arg10, Object arg11) {
		initSnow();
		log.error(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
	}

	@Override
	public void exit() {
		log.exit();
	}

	@Override
	public <R> R exit(R arg0) {
		return log.exit(arg0);
	}

	@Override
	public void fatal(Message arg0) {
		initSnow();
		log.fatal(arg0);
	}

	@Override
	public void fatal(MessageSupplier arg0) {
		initSnow();
		log.fatal(arg0);
	}

	@Override
	public void fatal(CharSequence arg0) {
		initSnow();
		log.fatal(arg0);
	}

	@Override
	public void fatal(Object arg0) {
		initSnow();
		log.fatal(arg0);
	}

	@Override
	public void fatal(String arg0) {
		initSnow();
		log.fatal(arg0);
	}

	@Override
	public void fatal(Supplier<?> arg0) {
		initSnow();
		log.fatal(arg0);
	}

	@Override
	public void fatal(Marker arg0, Message arg1) {
		initSnow();
		log.fatal(arg0, arg1);
	}

	@Override
	public void fatal(Marker arg0, MessageSupplier arg1) {
		initSnow();
		log.fatal(arg0, arg1);
	}

	@Override
	public void fatal(Marker arg0, CharSequence arg1) {
		initSnow();
		log.fatal(arg0, arg1);
	}

	@Override
	public void fatal(Marker arg0, Object arg1) {
		initSnow();
		log.fatal(arg0, arg1);
	}

	@Override
	public void fatal(Marker arg0, String arg1) {
		initSnow();
		log.fatal(arg0, arg1);
	}

	@Override
	public void fatal(Marker arg0, Supplier<?> arg1) {
		initSnow();
		log.fatal(arg0, arg1);
	}

	@Override
	public void fatal(Message arg0, Throwable arg1) {
		initSnow();
		log.fatal(arg0, arg1);
	}

	@Override
	public void fatal(MessageSupplier arg0, Throwable arg1) {
		initSnow(arg1);
		log.fatal(arg0, arg1);
	}

	@Override
	public void fatal(CharSequence arg0, Throwable arg1) {
		initSnow(arg1);
		log.fatal(arg0, arg1);
	}

	@Override
	public void fatal(Object arg0, Throwable arg1) {
		initSnow(arg1);
		log.fatal(arg0, arg1);
	}

	@Override
	public void fatal(String arg0, Object... arg1) {
		initSnow();
		log.fatal(arg0, arg1);
	}

	@Override
	public void fatal(String arg0, Supplier<?>... arg1) {
		initSnow();
		log.fatal(arg0, arg1);
	}

	@Override
	public void fatal(String arg0, Throwable arg1) {
		initSnow(arg1);
		log.fatal(arg0, arg1);
	}

	@Override
	public void fatal(Supplier<?> arg0, Throwable arg1) {
		initSnow(arg1);
		log.fatal(arg0, arg1);
	}

	@Override
	public void fatal(String arg0, Object arg1) {
		initSnow();
		log.fatal(arg0, arg1);
	}

	@Override
	public void fatal(Marker arg0, Message arg1, Throwable arg2) {
		initSnow(arg2);
		log.fatal(arg0, arg1, arg2);
	}

	@Override
	public void fatal(Marker arg0, MessageSupplier arg1, Throwable arg2) {
		initSnow(arg2);
		log.fatal(arg0, arg1, arg2);
	}

	@Override
	public void fatal(Marker arg0, CharSequence arg1, Throwable arg2) {
		initSnow(arg2);
		log.fatal(arg0, arg1, arg2);
	}

	@Override
	public void fatal(Marker arg0, Object arg1, Throwable arg2) {
		initSnow(arg2);
		log.fatal(arg0, arg1, arg2);
	}

	@Override
	public void fatal(Marker arg0, String arg1, Object... arg2) {
		initSnow();
		log.fatal(arg0, arg1, arg2);
	}

	@Override
	public void fatal(Marker arg0, String arg1, Supplier<?>... arg2) {
		initSnow();
		log.fatal(arg0, arg1, arg2);
	}

	@Override
	public void fatal(Marker arg0, String arg1, Throwable arg2) {
		initSnow(arg2);
		log.fatal(arg0, arg1, arg2);
	}

	@Override
	public void fatal(Marker arg0, Supplier<?> arg1, Throwable arg2) {
		initSnow(arg2);
		log.fatal(arg0, arg1, arg2);
	}

	@Override
	public void fatal(Marker arg0, String arg1, Object arg2) {
		initSnow();
		log.fatal(arg0, arg1, arg2);
	}

	@Override
	public void fatal(String arg0, Object arg1, Object arg2) {
		initSnow();
		log.fatal(arg0, arg1, arg2);
	}

	@Override
	public void fatal(Marker arg0, String arg1, Object arg2, Object arg3) {
		initSnow();
		log.fatal(arg0, arg1, arg2, arg3);
	}

	@Override
	public void fatal(String arg0, Object arg1, Object arg2, Object arg3) {
		initSnow();
		log.fatal(arg0, arg1, arg2, arg3);
	}

	@Override
	public void fatal(Marker arg0, String arg1, Object arg2, Object arg3, Object arg4) {
		initSnow();
		log.fatal(arg0, arg1, arg2, arg3, arg4);
	}

	@Override
	public void fatal(String arg0, Object arg1, Object arg2, Object arg3, Object arg4) {
		initSnow();
		log.fatal(arg0, arg1, arg2, arg3, arg4);
	}

	@Override
	public void fatal(Marker arg0, String arg1, Object arg2, Object arg3, Object arg4, Object arg5) {
		initSnow();
		log.fatal(arg0, arg1, arg2, arg3, arg4, arg5);
	}

	@Override
	public void fatal(String arg0, Object arg1, Object arg2, Object arg3, Object arg4, Object arg5) {
		initSnow();
		log.fatal(arg0, arg1, arg2, arg3, arg4, arg5);
	}

	@Override
	public void fatal(Marker arg0, String arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6) {
		initSnow();
		log.fatal(arg0, arg1, arg2, arg3, arg4, arg5, arg6);
	}

	@Override
	public void fatal(String arg0, Object arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6) {
		initSnow();
		log.fatal(arg0, arg1, arg2, arg3, arg4, arg5, arg6);
	}

	@Override
	public void fatal(Marker arg0, String arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7) {
		initSnow();
		log.fatal(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
	}

	@Override
	public void fatal(String arg0, Object arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7) {
		initSnow();
		log.fatal(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
	}

	@Override
	public void fatal(Marker arg0, String arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7, Object arg8) {
		initSnow();
		log.fatal(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
	}

	@Override
	public void fatal(String arg0, Object arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7, Object arg8) {
		initSnow();
		log.fatal(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
	}

	@Override
	public void fatal(Marker arg0, String arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7, Object arg8, Object arg9) {
		initSnow();
		log.fatal(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
	}

	@Override
	public void fatal(String arg0, Object arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7, Object arg8, Object arg9) {
		initSnow();
		log.fatal(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
	}

	@Override
	public void fatal(Marker arg0, String arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7, Object arg8, Object arg9, Object arg10) {
		initSnow();
		log.fatal(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
	}

	@Override
	public void fatal(String arg0, Object arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7, Object arg8, Object arg9, Object arg10) {
		initSnow();
		log.fatal(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
	}

	@Override
	public void fatal(Marker arg0, String arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7, Object arg8, Object arg9, Object arg10, Object arg11) {
		initSnow();
		log.fatal(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
	}

	@Override
	public Level getLevel() {
		return log.getLevel();
	}

	@Override
	public <MF extends MessageFactory> MF getMessageFactory() {
		return log.getMessageFactory();
	}

	@Override
	public String getName() {
		return log.getName();
	}

	@Override
	public void info(Message arg0) {
		initSnow();
		log.info(arg0);
	}

	@Override
	public void info(MessageSupplier arg0) {
		initSnow();
		log.info(arg0);
	}

	@Override
	public void info(CharSequence arg0) {
		initSnow();
		log.info(arg0);
	}

	@Override
	public void info(Object arg0) {
		initSnow();
		log.info(arg0);
	}

	@Override
	public void info(String arg0) {
		initSnow();
		log.info(arg0);
	}

	@Override
	public void info(Supplier<?> arg0) {
		initSnow();
		log.info(arg0);
	}

	@Override
	public void info(Marker arg0, Message arg1) {
		initSnow();
		log.info(arg0, arg1);
	}

	@Override
	public void info(Marker arg0, MessageSupplier arg1) {
		initSnow();
		log.info(arg0, arg1);
	}

	@Override
	public void info(Marker arg0, CharSequence arg1) {
		initSnow();
		log.info(arg0, arg1);
	}

	@Override
	public void info(Marker arg0, Object arg1) {
		initSnow();
		log.info(arg0, arg1);
	}

	@Override
	public void info(Marker arg0, String arg1) {
		initSnow();
		log.info(arg0, arg1);
	}

	@Override
	public void info(Marker arg0, Supplier<?> arg1) {
		initSnow();
		log.info(arg0, arg1);
	}

	@Override
	public void info(Message arg0, Throwable arg1) {
		initSnow(arg1);
		log.info(arg0, arg1);
	}

	@Override
	public void info(MessageSupplier arg0, Throwable arg1) {
		initSnow(arg1);
		log.info(arg0, arg1);
	}

	@Override
	public void info(CharSequence arg0, Throwable arg1) {
		initSnow(arg1);
		log.info(arg0, arg1);
	}

	@Override
	public void info(Object arg0, Throwable arg1) {
		initSnow(arg1);
		log.info(arg0, arg1);
	}

	@Override
	public void info(String arg0, Object... arg1) {
		initSnow();
		log.info(arg0, arg1);
	}

	@Override
	public void info(String arg0, Supplier<?>... arg1) {
		initSnow();
		log.info(arg0, arg1);
	}

	@Override
	public void info(String arg0, Throwable arg1) {
		initSnow(arg1);
		log.info(arg0, arg1);
	}

	@Override
	public void info(Supplier<?> arg0, Throwable arg1) {
		initSnow(arg1);
		log.info(arg0, arg1);
	}

	@Override
	public void info(String arg0, Object arg1) {
		initSnow();
		log.info(arg0, arg1);
	}

	@Override
	public void info(Marker arg0, Message arg1, Throwable arg2) {
		initSnow(arg2);
		log.info(arg0, arg1, arg2);
	}

	@Override
	public void info(Marker arg0, MessageSupplier arg1, Throwable arg2) {
		initSnow(arg2);
		log.info(arg0, arg1, arg2);
	}

	@Override
	public void info(Marker arg0, CharSequence arg1, Throwable arg2) {
		initSnow(arg2);
		log.info(arg0, arg1, arg2);
	}

	@Override
	public void info(Marker arg0, Object arg1, Throwable arg2) {
		initSnow(arg2);
		log.info(arg0, arg1, arg2);
	}

	@Override
	public void info(Marker arg0, String arg1, Object... arg2) {
		initSnow();
		log.info(arg0, arg1, arg2);
	}

	@Override
	public void info(Marker arg0, String arg1, Supplier<?>... arg2) {
		initSnow();
		log.info(arg0, arg1, arg2);
	}

	@Override
	public void info(Marker arg0, String arg1, Throwable arg2) {
		initSnow(arg2);
		log.info(arg0, arg1, arg2);
	}

	@Override
	public void info(Marker arg0, Supplier<?> arg1, Throwable arg2) {
		initSnow(arg2);
		log.info(arg0, arg1, arg2);
	}

	@Override
	public void info(Marker arg0, String arg1, Object arg2) {
		initSnow();
		log.info(arg0, arg1, arg2);
	}

	@Override
	public void info(String arg0, Object arg1, Object arg2) {
		initSnow();
		log.info(arg0, arg1, arg2);
	}

	@Override
	public void info(Marker arg0, String arg1, Object arg2, Object arg3) {
		initSnow();
		log.info(arg0, arg1, arg2, arg3);
	}

	@Override
	public void info(String arg0, Object arg1, Object arg2, Object arg3) {
		initSnow();
		log.info(arg0, arg1, arg2, arg3);
	}

	@Override
	public void info(Marker arg0, String arg1, Object arg2, Object arg3, Object arg4) {
		initSnow();
		log.info(arg0, arg1, arg2, arg3, arg4);
	}

	@Override
	public void info(String arg0, Object arg1, Object arg2, Object arg3, Object arg4) {
		initSnow();
		log.info(arg0, arg1, arg2, arg3, arg4);
	}

	@Override
	public void info(Marker arg0, String arg1, Object arg2, Object arg3, Object arg4, Object arg5) {
		initSnow();
		log.info(arg0, arg1, arg2, arg3, arg4, arg5);
	}

	@Override
	public void info(String arg0, Object arg1, Object arg2, Object arg3, Object arg4, Object arg5) {
		initSnow();
		log.info(arg0, arg1, arg2, arg3, arg4, arg5);
	}

	@Override
	public void info(Marker arg0, String arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6) {
		initSnow();
		log.info(arg0, arg1, arg2, arg3, arg4, arg5, arg6);
	}

	@Override
	public void info(String arg0, Object arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6) {
		initSnow();
		log.info(arg0, arg1, arg2, arg3, arg4, arg5, arg6);
	}

	@Override
	public void info(Marker arg0, String arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7) {
		initSnow();
		log.info(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
	}

	@Override
	public void info(String arg0, Object arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7) {
		initSnow();
		log.info(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
	}

	@Override
	public void info(Marker arg0, String arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7, Object arg8) {
		initSnow();
		log.info(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
	}

	@Override
	public void info(String arg0, Object arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7, Object arg8) {
		initSnow();
		log.info(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
	}

	@Override
	public void info(Marker arg0, String arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7, Object arg8, Object arg9) {
		initSnow();
		log.info(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
	}

	@Override
	public void info(String arg0, Object arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7, Object arg8, Object arg9) {
		initSnow();
		log.info(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
	}

	@Override
	public void info(Marker arg0, String arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7, Object arg8, Object arg9, Object arg10) {
		initSnow();
		log.info(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
	}

	@Override
	public void info(String arg0, Object arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7, Object arg8, Object arg9, Object arg10) {
		initSnow();
		log.info(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
	}

	@Override
	public void info(Marker arg0, String arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7, Object arg8, Object arg9, Object arg10, Object arg11) {
		initSnow();
		log.info(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
	}

	@Override
	public boolean isDebugEnabled() {
		return log.isDebugEnabled();
	}

	@Override
	public boolean isDebugEnabled(Marker arg0) {
		return log.isDebugEnabled(arg0);
	}

	@Override
	public boolean isEnabled(Level arg0) {
		return log.isEnabled(arg0);
	}

	@Override
	public boolean isEnabled(Level arg0, Marker arg1) {
		return log.isEnabled(arg0, arg1);
	}

	@Override
	public boolean isErrorEnabled() {
		return log.isErrorEnabled();
	}

	@Override
	public boolean isErrorEnabled(Marker arg0) {
		return log.isErrorEnabled(arg0);
	}

	@Override
	public boolean isFatalEnabled() {
		return isFatalEnabled();
	}

	@Override
	public boolean isFatalEnabled(Marker arg0) {
		return log.isFatalEnabled(arg0);
	}

	@Override
	public boolean isInfoEnabled() {
		return log.isInfoEnabled();
	}

	@Override
	public boolean isInfoEnabled(Marker arg0) {
		return log.isInfoEnabled(arg0);
	}

	@Override
	public boolean isTraceEnabled() {
		return log.isTraceEnabled();
	}

	@Override
	public boolean isTraceEnabled(Marker arg0) {
		return log.isTraceEnabled(arg0);
	}

	@Override
	public boolean isWarnEnabled() {
		return log.isWarnEnabled();
	}

	@Override
	public boolean isWarnEnabled(Marker arg0) {
		return log.isWarnEnabled(arg0);
	}

	@Override
	public void log(Level arg0, Message arg1) {
		log.log(arg0, arg1);
	}

	@Override
	public void log(Level arg0, MessageSupplier arg1) {
		log.log(arg0, arg1);
	}

	@Override
	public void log(Level arg0, CharSequence arg1) {
		log.log(arg0, arg1);
	}

	@Override
	public void log(Level arg0, Object arg1) {
		log.log(arg0, arg1);
	}

	@Override
	public void log(Level arg0, String arg1) {
		log.log(arg0, arg1);
	}

	@Override
	public void log(Level arg0, Supplier<?> arg1) {
		log.log(arg0, arg1);
	}

	@Override
	public void log(Level arg0, Marker arg1, Message arg2) {
		log.log(arg0, arg1, arg2);
	}

	@Override
	public void log(Level arg0, Marker arg1, MessageSupplier arg2) {
		log.log(arg0, arg1, arg2);
	}

	@Override
	public void log(Level arg0, Marker arg1, CharSequence arg2) {
		log.log(arg0, arg1, arg2);
	}

	@Override
	public void log(Level arg0, Marker arg1, Object arg2) {
		log.log(arg0, arg1, arg2);
	}

	@Override
	public void log(Level arg0, Marker arg1, String arg2) {
		log.log(arg0, arg1, arg2);
	}

	@Override
	public void log(Level arg0, Marker arg1, Supplier<?> arg2) {
		log.log(arg0, arg1, arg2);
	}

	@Override
	public void log(Level arg0, Message arg1, Throwable arg2) {
		log.log(arg0, arg1, arg2);
	}

	@Override
	public void log(Level arg0, MessageSupplier arg1, Throwable arg2) {
		log.log(arg0, arg1, arg2);
	}

	@Override
	public void log(Level arg0, CharSequence arg1, Throwable arg2) {
		log.log(arg0, arg1, arg2);
	}

	@Override
	public void log(Level arg0, Object arg1, Throwable arg2) {
		log.log(arg0, arg1, arg2);
	}

	@Override
	public void log(Level arg0, String arg1, Object... arg2) {
		log.log(arg0, arg1, arg2);
	}

	@Override
	public void log(Level arg0, String arg1, Supplier<?>... arg2) {
		log.log(arg0, arg1, arg2);
	}

	@Override
	public void log(Level arg0, String arg1, Throwable arg2) {
		log.log(arg0, arg1, arg2);
	}

	@Override
	public void log(Level arg0, Supplier<?> arg1, Throwable arg2) {
		log.log(arg0, arg1, arg2);
	}

	@Override
	public void log(Level arg0, String arg1, Object arg2) {
		log.log(arg0, arg1, arg2);
	}

	@Override
	public void log(Level arg0, Marker arg1, Message arg2, Throwable arg3) {
		log.log(arg0, arg1, arg2, arg3);
	}

	@Override
	public void log(Level arg0, Marker arg1, MessageSupplier arg2, Throwable arg3) {
		log.log(arg0, arg1, arg2, arg3);
	}

	@Override
	public void log(Level arg0, Marker arg1, CharSequence arg2, Throwable arg3) {
		log.log(arg0, arg1, arg2, arg3);
	}

	@Override
	public void log(Level arg0, Marker arg1, Object arg2, Throwable arg3) {
		log.log(arg0, arg1, arg2, arg3);
	}

	@Override
	public void log(Level arg0, Marker arg1, String arg2, Object... arg3) {
		log.log(arg0, arg1, arg2, arg3);
	}

	@Override
	public void log(Level arg0, Marker arg1, String arg2, Supplier<?>... arg3) {
		log.log(arg0, arg1, arg2, arg3);
	}

	@Override
	public void log(Level arg0, Marker arg1, String arg2, Throwable arg3) {
		log.log(arg0, arg1, arg2, arg3);
	}

	@Override
	public void log(Level arg0, Marker arg1, Supplier<?> arg2, Throwable arg3) {
		log.log(arg0, arg1, arg2, arg3);
	}

	@Override
	public void log(Level arg0, Marker arg1, String arg2, Object arg3) {
		log.log(arg0, arg1, arg2, arg3);
	}

	@Override
	public void log(Level arg0, String arg1, Object arg2, Object arg3) {
		log.log(arg0, arg1, arg2, arg3);
	}

	@Override
	public void log(Level arg0, Marker arg1, String arg2, Object arg3, Object arg4) {
		log.log(arg0, arg1, arg2, arg3, arg4);
	}

	@Override
	public void log(Level arg0, String arg1, Object arg2, Object arg3, Object arg4) {
		log.log(arg0, arg1, arg2, arg3, arg4);
	}

	@Override
	public void log(Level arg0, Marker arg1, String arg2, Object arg3, Object arg4, Object arg5) {
		log.log(arg0, arg1, arg2, arg3, arg4, arg5);
	}

	@Override
	public void log(Level arg0, String arg1, Object arg2, Object arg3, Object arg4, Object arg5) {
		log.log(arg0, arg1, arg2, arg3, arg4, arg5);
	}

	@Override
	public void log(Level arg0, Marker arg1, String arg2, Object arg3, Object arg4, Object arg5, Object arg6) {
		log.log(arg0, arg1, arg2, arg3, arg4, arg5, arg6);
	}

	@Override
	public void log(Level arg0, String arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6) {
		log.log(arg0, arg1, arg2, arg3, arg4, arg5, arg6);
	}

	@Override
	public void log(Level arg0, Marker arg1, String arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7) {
		log.log(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
	}

	@Override
	public void log(Level arg0, String arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7) {
		log.log(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
	}

	@Override
	public void log(Level arg0, Marker arg1, String arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7, Object arg8) {
		log.log(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
	}

	@Override
	public void log(Level arg0, String arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7, Object arg8) {
		log.log(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
	}

	@Override
	public void log(Level arg0, Marker arg1, String arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7, Object arg8, Object arg9) {
		log.log(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
	}

	@Override
	public void log(Level arg0, String arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7, Object arg8, Object arg9) {
		log.log(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);

	}

	@Override
	public void log(Level arg0, Marker arg1, String arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7, Object arg8, Object arg9, Object arg10) {
		log.log(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
	}

	@Override
	public void log(Level arg0, String arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7, Object arg8, Object arg9, Object arg10) {
		log.log(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
	}

	@Override
	public void log(Level arg0, Marker arg1, String arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7, Object arg8, Object arg9, Object arg10, Object arg11) {
		log.log(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
	}

	@Override
	public void log(Level arg0, String arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7, Object arg8, Object arg9, Object arg10, Object arg11) {
		log.log(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
	}

	@Override
	public void log(Level arg0, Marker arg1, String arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7, Object arg8, Object arg9, Object arg10, Object arg11, Object arg12) {
		log.log(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
	}

	@Override
	public void printf(Level arg0, String arg1, Object... arg2) {
		log.printf(arg0, arg1, arg2);
	}

	@Override
	public void printf(Level arg0, Marker arg1, String arg2, Object... arg3) {
		log.printf(arg0, arg1, arg2, arg3);
	}

	@Override
	public <T extends Throwable> T throwing(T arg0) {
		return log.throwing(arg0);
	}

	@Override
	public <T extends Throwable> T throwing(Level arg0, T arg1) {
		return log.throwing(arg0, arg1);
	}

	@Override
	public void trace(Message arg0) {
		initSnow();
		log.trace(arg0);
	}

	@Override
	public void trace(MessageSupplier arg0) {
		initSnow();
		log.trace(arg0);
	}

	@Override
	public void trace(CharSequence arg0) {
		initSnow();
		log.trace(arg0);
	}

	@Override
	public void trace(Object arg0) {
		initSnow();
		log.trace(arg0);
	}

	@Override
	public void trace(String arg0) {
		initSnow();
		log.trace(arg0);
	}

	@Override
	public void trace(Supplier<?> arg0) {
		initSnow();
		log.trace(arg0);
	}

	@Override
	public void trace(Marker arg0, Message arg1) {
		initSnow();
		log.trace(arg0, arg1);
	}

	@Override
	public void trace(Marker arg0, MessageSupplier arg1) {
		initSnow();
		log.trace(arg0, arg1);
	}

	@Override
	public void trace(Marker arg0, CharSequence arg1) {
		initSnow();
		log.trace(arg0, arg1);
	}

	@Override
	public void trace(Marker arg0, Object arg1) {
		initSnow();
		log.trace(arg0, arg1);
	}

	@Override
	public void trace(Marker arg0, String arg1) {
		initSnow();
		log.trace(arg0, arg1);
	}

	@Override
	public void trace(Marker arg0, Supplier<?> arg1) {
		initSnow();
		log.trace(arg0, arg1);
	}

	@Override
	public void trace(Message arg0, Throwable arg1) {
		initSnow(arg1);
		log.trace(arg0, arg1);
	}

	@Override
	public void trace(MessageSupplier arg0, Throwable arg1) {
		initSnow(arg1);
		log.trace(arg0, arg1);
	}

	@Override
	public void trace(CharSequence arg0, Throwable arg1) {
		initSnow(arg1);
		log.trace(arg0, arg1);
	}

	@Override
	public void trace(Object arg0, Throwable arg1) {
		initSnow(arg1);
		log.trace(arg0, arg1);
	}

	@Override
	public void trace(String arg0, Object... arg1) {
		initSnow();
		log.trace(arg0, arg1);
	}

	@Override
	public void trace(String arg0, Supplier<?>... arg1) {
		initSnow();
		log.trace(arg0, arg1);
	}

	@Override
	public void trace(String arg0, Throwable arg1) {
		initSnow(arg1);
		log.trace(arg0, arg1);
	}

	@Override
	public void trace(Supplier<?> arg0, Throwable arg1) {
		initSnow(arg1);
		log.trace(arg0, arg1);
	}

	@Override
	public void trace(String arg0, Object arg1) {
		initSnow();
		log.trace(arg0, arg1);
	}

	@Override
	public void trace(Marker arg0, Message arg1, Throwable arg2) {
		initSnow(arg2);
		log.trace(arg0, arg1, arg2);
	}

	@Override
	public void trace(Marker arg0, MessageSupplier arg1, Throwable arg2) {
		initSnow(arg2);
		log.trace(arg0, arg1, arg2);
	}

	@Override
	public void trace(Marker arg0, CharSequence arg1, Throwable arg2) {
		initSnow(arg2);
		log.trace(arg0, arg1, arg2);
	}

	@Override
	public void trace(Marker arg0, Object arg1, Throwable arg2) {
		initSnow(arg2);
		log.trace(arg0, arg1, arg2);
	}

	@Override
	public void trace(Marker arg0, String arg1, Object... arg2) {
		initSnow();
		log.trace(arg0, arg1, arg2);
	}

	@Override
	public void trace(Marker arg0, String arg1, Supplier<?>... arg2) {
		initSnow();
		log.trace(arg0, arg1, arg2);
	}

	@Override
	public void trace(Marker arg0, String arg1, Throwable arg2) {
		initSnow(arg2);
		log.trace(arg0, arg1, arg2);
	}

	@Override
	public void trace(Marker arg0, Supplier<?> arg1, Throwable arg2) {
		initSnow(arg2);
		log.trace(arg0, arg1, arg2);
	}

	@Override
	public void trace(Marker arg0, String arg1, Object arg2) {
		initSnow();
		log.trace(arg0, arg1, arg2);
	}

	@Override
	public void trace(String arg0, Object arg1, Object arg2) {
		initSnow();
		log.trace(arg0, arg1, arg2);
	}

	@Override
	public void trace(Marker arg0, String arg1, Object arg2, Object arg3) {
		initSnow();
		log.trace(arg0, arg1, arg2, arg3);
	}

	@Override
	public void trace(String arg0, Object arg1, Object arg2, Object arg3) {
		initSnow();
		log.trace(arg0, arg1, arg2, arg3);
	}

	@Override
	public void trace(Marker arg0, String arg1, Object arg2, Object arg3, Object arg4) {
		initSnow();
		log.trace(arg0, arg1, arg2, arg3, arg4);
	}

	@Override
	public void trace(String arg0, Object arg1, Object arg2, Object arg3, Object arg4) {
		initSnow();
		log.trace(arg0, arg1, arg2, arg3, arg4);
	}

	@Override
	public void trace(Marker arg0, String arg1, Object arg2, Object arg3, Object arg4, Object arg5) {
		initSnow();
		log.trace(arg0, arg1, arg2, arg3, arg4, arg5);
	}

	@Override
	public void trace(String arg0, Object arg1, Object arg2, Object arg3, Object arg4, Object arg5) {
		initSnow();
		log.trace(arg0, arg1, arg2, arg3, arg4, arg5);
	}

	@Override
	public void trace(Marker arg0, String arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6) {
		initSnow();
		log.trace(arg0, arg1, arg2, arg3, arg4, arg5, arg6);
	}

	@Override
	public void trace(String arg0, Object arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6) {
		initSnow();
		log.trace(arg0, arg1, arg2, arg3, arg4, arg5, arg6);
	}

	@Override
	public void trace(Marker arg0, String arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7) {
		initSnow();
		log.trace(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
	}

	@Override
	public void trace(String arg0, Object arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7) {
		initSnow();
		log.trace(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
	}

	@Override
	public void trace(Marker arg0, String arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7, Object arg8) {
		initSnow();
		log.trace(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
	}

	@Override
	public void trace(String arg0, Object arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7, Object arg8) {
		initSnow();
		log.trace(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
	}

	@Override
	public void trace(Marker arg0, String arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7, Object arg8, Object arg9) {
		initSnow();
		log.trace(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
	}

	@Override
	public void trace(String arg0, Object arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7, Object arg8, Object arg9) {
		initSnow();
		log.trace(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
	}

	@Override
	public void trace(Marker arg0, String arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7, Object arg8, Object arg9, Object arg10) {
		initSnow();
		log.trace(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
	}

	@Override
	public void trace(String arg0, Object arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7, Object arg8, Object arg9, Object arg10) {
		initSnow();
		log.trace(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
	}

	@Override
	public void trace(Marker arg0, String arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7, Object arg8, Object arg9, Object arg10, Object arg11) {
		initSnow();
		log.trace(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
	}

	@Override
	public EntryMessage traceEntry() {
		return log.traceEntry();
	}

	@Override
	public EntryMessage traceEntry(Supplier<?>... arg0) {
		return log.traceEntry(arg0);
	}

	@Override
	public EntryMessage traceEntry(Message arg0) {
		return log.traceEntry(arg0);
	}

	@Override
	public EntryMessage traceEntry(String arg0, Object... arg1) {
		return log.traceEntry(arg0, arg1);
	}

	@Override
	public EntryMessage traceEntry(String arg0, Supplier<?>... arg1) {
		return log.traceEntry(arg0, arg1);
	}

	@Override
	public void traceExit() {
		log.traceExit();
	}

	@Override
	public <R> R traceExit(R arg0) {
		return log.traceExit(arg0);
	}

	@Override
	public void traceExit(EntryMessage arg0) {
		log.traceExit(arg0);
	}

	@Override
	public <R> R traceExit(String arg0, R arg1) {
		return log.traceExit(arg0, arg1);
	}

	@Override
	public <R> R traceExit(EntryMessage arg0, R arg1) {
		return log.traceExit(arg0, arg1);
	}

	@Override
	public <R> R traceExit(Message arg0, R arg1) {
		return log.traceExit(arg0, arg1);
	}

	@Override
	public void warn(Message arg0) {
		initSnow();
		log.warn(arg0);
	}

	@Override
	public void warn(MessageSupplier arg0) {
		initSnow();
		log.warn(arg0);
	}

	@Override
	public void warn(CharSequence arg0) {
		initSnow();
		log.warn(arg0);
	}

	@Override
	public void warn(Object arg0) {
		initSnow();
		log.warn(arg0);
	}

	@Override
	public void warn(String arg0) {
		initSnow();
		log.warn(arg0);
	}

	@Override
	public void warn(Supplier<?> arg0) {
		initSnow();
		log.warn(arg0);
	}

	@Override
	public void warn(Marker arg0, Message arg1) {
		initSnow();
		log.warn(arg0, arg1);
	}

	@Override
	public void warn(Marker arg0, MessageSupplier arg1) {
		initSnow();
		log.warn(arg0, arg1);
	}

	@Override
	public void warn(Marker arg0, CharSequence arg1) {
		initSnow();
		log.warn(arg0, arg1);
	}

	@Override
	public void warn(Marker arg0, Object arg1) {
		initSnow();
		log.warn(arg0, arg1);
	}

	@Override
	public void warn(Marker arg0, String arg1) {
		initSnow();
		log.warn(arg0, arg1);
	}

	@Override
	public void warn(Marker arg0, Supplier<?> arg1) {
		initSnow();
		log.warn(arg0, arg1);
	}

	@Override
	public void warn(Message arg0, Throwable arg1) {
		initSnow(arg1);
		log.warn(arg0, arg1);
	}

	@Override
	public void warn(MessageSupplier arg0, Throwable arg1) {
		initSnow(arg1);
		log.warn(arg0, arg1);
	}

	@Override
	public void warn(CharSequence arg0, Throwable arg1) {
		initSnow(arg1);
		log.warn(arg0, arg1);
	}

	@Override
	public void warn(Object arg0, Throwable arg1) {
		initSnow(arg1);
		log.warn(arg0, arg1);
	}

	@Override
	public void warn(String arg0, Object... arg1) {
		initSnow();
		log.warn(arg0, arg1);
	}

	@Override
	public void warn(String arg0, Supplier<?>... arg1) {
		initSnow();
		log.warn(arg0, arg1);
	}

	@Override
	public void warn(String arg0, Throwable arg1) {
		initSnow(arg1);
		log.warn(arg0, arg1);
	}

	@Override
	public void warn(Supplier<?> arg0, Throwable arg1) {
		initSnow(arg1);
		log.warn(arg0, arg1);
	}

	@Override
	public void warn(String arg0, Object arg1) {
		initSnow();
		log.warn(arg0, arg1);
	}

	@Override
	public void warn(Marker arg0, Message arg1, Throwable arg2) {
		initSnow(arg2);
		log.warn(arg0, arg1, arg2);
	}

	@Override
	public void warn(Marker arg0, MessageSupplier arg1, Throwable arg2) {
		initSnow(arg2);
		log.warn(arg0, arg1, arg2);
	}

	@Override
	public void warn(Marker arg0, CharSequence arg1, Throwable arg2) {
		initSnow(arg2);
		log.warn(arg0, arg1, arg2);
	}

	@Override
	public void warn(Marker arg0, Object arg1, Throwable arg2) {
		initSnow(arg2);
		log.warn(arg0, arg1, arg2);
	}

	@Override
	public void warn(Marker arg0, String arg1, Object... arg2) {
		initSnow();
		log.warn(arg0, arg1, arg2);
	}

	@Override
	public void warn(Marker arg0, String arg1, Supplier<?>... arg2) {
		initSnow();
		log.warn(arg0, arg1, arg2);
	}

	@Override
	public void warn(Marker arg0, String arg1, Throwable arg2) {
		initSnow(arg2);
		log.warn(arg0, arg1, arg2);
	}

	@Override
	public void warn(Marker arg0, Supplier<?> arg1, Throwable arg2) {
		initSnow(arg2);
		log.warn(arg0, arg1, arg2);
	}

	@Override
	public void warn(Marker arg0, String arg1, Object arg2) {
		initSnow();
		log.warn(arg0, arg1, arg2);
	}

	@Override
	public void warn(String arg0, Object arg1, Object arg2) {
		initSnow();
		log.warn(arg0, arg1, arg2);
	}

	@Override
	public void warn(Marker arg0, String arg1, Object arg2, Object arg3) {
		initSnow();
		log.warn(arg0, arg1, arg2, arg3);
	}

	@Override
	public void warn(String arg0, Object arg1, Object arg2, Object arg3) {
		initSnow();
		log.warn(arg0, arg1, arg2, arg3);
	}

	@Override
	public void warn(Marker arg0, String arg1, Object arg2, Object arg3, Object arg4) {
		initSnow();
		log.warn(arg0, arg1, arg2, arg3, arg4);
	}

	@Override
	public void warn(String arg0, Object arg1, Object arg2, Object arg3, Object arg4) {
		initSnow();
		log.warn(arg0, arg1, arg2, arg3, arg4);
	}

	@Override
	public void warn(Marker arg0, String arg1, Object arg2, Object arg3, Object arg4, Object arg5) {
		initSnow();
		log.warn(arg0, arg1, arg2, arg3, arg4, arg5);
	}

	@Override
	public void warn(String arg0, Object arg1, Object arg2, Object arg3, Object arg4, Object arg5) {
		initSnow();
		log.warn(arg0, arg1, arg2, arg3, arg4, arg5);
	}

	@Override
	public void warn(Marker arg0, String arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6) {
		initSnow();
		log.warn(arg0, arg1, arg2, arg3, arg4, arg5, arg6);
	}

	@Override
	public void warn(String arg0, Object arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6) {
		initSnow();
		log.warn(arg0, arg1, arg2, arg3, arg4, arg5, arg6);
	}

	@Override
	public void warn(Marker arg0, String arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7) {
		initSnow();
		log.warn(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
	}

	@Override
	public void warn(String arg0, Object arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7) {
		initSnow();
		log.warn(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
	}

	@Override
	public void warn(Marker arg0, String arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7, Object arg8) {
		initSnow();
		log.warn(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
	}

	@Override
	public void warn(String arg0, Object arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7, Object arg8) {
		initSnow();
		log.warn(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
	}

	@Override
	public void warn(Marker arg0, String arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7, Object arg8, Object arg9) {
		initSnow();
		log.warn(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
	}

	@Override
	public void warn(String arg0, Object arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7, Object arg8, Object arg9) {
		initSnow();
		log.warn(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
	}

	@Override
	public void warn(Marker arg0, String arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7, Object arg8, Object arg9, Object arg10) {
		initSnow();
		log.warn(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
	}

	@Override
	public void warn(String arg0, Object arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7, Object arg8, Object arg9, Object arg10) {
		initSnow();
		log.warn(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
	}

	@Override
	public void warn(Marker arg0, String arg1, Object arg2, Object arg3, Object arg4, Object arg5, Object arg6,
			Object arg7, Object arg8, Object arg9, Object arg10, Object arg11) {
		initSnow();
		log.warn(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
	}
}