package com.txj.common;

import java.util.Calendar;
import java.util.HashMap;
import java.util.Map;
import java.util.UUID;

public class ThreadHelper {
	static Map<String, String> controllerVersionMap;
	static Map<String, UserEditLockVal> editLockMap;

	static {
		controllerVersionMap = new HashMap<String, String>();
		editLockMap = new HashMap<String, UserEditLockVal>();
	}

	/**
	 * 当修改数据时，如果涉及到控制器版本号更改，则使用该方法修改
	 * 
	 * @param controllerName
	 *            控制器名称
	 * @return 返回最新版本号
	 */
	public static String editControllerVersion(String controllerName) {
		String newestVersion;
		controllerName = controllerName + "Version";
		synchronized (controllerName.intern()) {
			controllerVersionMap.put(controllerName, newestVersion = UUID.randomUUID().toString());
		}
		return newestVersion;
	}

	/**
	 * 比较控制器的版本号，如果相同的则返回true，否则返回false，并返回最新版本号
	 * 
	 * @param controllerName
	 *            控制器名称
	 * @param userVersion
	 *            用户的版本号
	 * @param newestVersion
	 *            这是一个返回值，不论版本号有没有变化，都返回最新版本号
	 * @return
	 */
	public static boolean compareControllerVersion(String controllerName, String userVersion,final String[] newestVersion) {
		controllerName = controllerName + "Version";
		synchronized (controllerName.intern()) {
			if (controllerVersionMap.containsKey(controllerName)) {
				newestVersion[0] = controllerVersionMap.get(controllerName);
			} else {
				controllerVersionMap.put(controllerName, newestVersion[0] = UUID.randomUUID().toString());
			}
			return newestVersion[0].equals(userVersion);
		}
	}

	/**
	 * 批量等待，可以多次调用，并且可以被BatchSet统一唤醒
	 * 
	 * @param controllerName
	 *            等待的控制器名称
	 * @param millisecondsTimeout
	 *            等待秒数
	 */
	public static void batchWait(String controllerName, long millisecondsTimeout) {
		controllerName = controllerName + "Wait";
		Object waitLock=controllerName.intern();
		synchronized (waitLock) {
			try {
				waitLock.wait(millisecondsTimeout);
			} catch (Exception ex) {
				System.out.println(ex);
			}
		}
	}

	/**
	 * 批量唤醒
	 * 
	 * @param controllerName
	 *            等待的控制器名称
	 */
	public static void batchSet(String controllerName) {
		controllerName = controllerName + "Wait";
		Object waitLock=controllerName.intern();
		synchronized (waitLock) {
			waitLock.notifyAll();
		}
	}

	/**
	 * 延长修改锁定时间
	 * 
	 * @param key
	 *            延长的key值
	 * @param username
	 *            延长的用户
	 */
	public static void addEditLockLimitDate(String key, String username) {
		if (editLockMap.containsKey(key)) {
			synchronized (key.intern()) {
				if (editLockMap.containsKey(key) && username.equals(editLockMap.get(key).username)) {
					editLockMap.get(key).getLimitDate().add(Calendar.SECOND, 5);
				}
			}
		}
	}

	/**
	 * 对于多个用户可同时修改的界面需要使用锁。
	 * @param key	数据主键
	 * @param editUsername	想要修改该数据的用户
	 * @param username	输出占用的用户名
	 * @return 抢夺成功时返回true，否则返回false
	 */
	public static boolean UserEditLock(String key,String editUsername, String username){
		Calendar calendar=Calendar.getInstance();
		calendar.add(Calendar.SECOND, 10);
		UserEditLockVal val = new UserEditLockVal(editUsername,calendar);
        synchronized (key.intern()) {
        		IteratorHelper.delEleFromMap(editLockMap, (value)-> value.limitDate.compareTo(Calendar.getInstance())>0);
        		if (editLockMap.containsKey(key))
        			if (!editLockMap.get(key).username.equals(editUsername) && editLockMap.get(key).limitDate.compareTo(Calendar.getInstance()) !=-1)
        			{
        				username = editLockMap.get(key).username;
        				return false;
        			}
        			else
        			{
        				username = null;
        				editLockMap.put(key, val);
        				return true;
        			}
        		else
        		{
        			username = null;
        			editLockMap.put(key, val);
        			return true;
        		}
		}
	}
	private static class UserEditLockVal {
		public UserEditLockVal(){
		}
		
		public UserEditLockVal(String username,Calendar limitDate){
			this.username=username;
			this.limitDate=limitDate;
		}
		
		public String username;
		public Calendar limitDate;

		public String getUsername() {
			return username;
		}

		public void setUsername(String username) {
			this.username = username;
		}

		public Calendar getLimitDate() {
			return limitDate;
		}

		public void setLimitDate(Calendar limitDate) {
			this.limitDate = limitDate;
		}

	}
}