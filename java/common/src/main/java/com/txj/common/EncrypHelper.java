package com.txj.common;
import java.io.BufferedInputStream;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.InputStream;
import java.util.Arrays;
import java.util.Base64;
import java.util.Base64.Encoder;

import javax.imageio.ImageIO;

import org.apache.commons.io.IOUtils;

/**
 * 加密帮助类
 * @author admin
 */
public class EncrypHelper {
	
	/**
	 * 读取输入流，并转化为base64返回
	 * @param	inputStream	输入流
	 * @return	返回图片的base64
	 */
	public static String inputStreamToBase64(InputStream inputStream){
		Encoder encoder = Base64.getEncoder();
		byte[] buffer=new byte[Integer.MAX_VALUE];
		try {
			byte[]  fixedBuffer = Arrays.copyOf(buffer, IOUtils.read(inputStream, buffer));
			return new String(encoder.encode(fixedBuffer));
		} catch (IOException e) {
			e.printStackTrace();
			throw new RuntimeException(e);
		}
	}
	
	/**
	 * 读取指定路径的文件内容，并转化为base64返回
	 * @param filePath	文件路径
	 * @return
	 */
	public static String fileToBase64(String filePath){
		try(InputStream inputStream =new BufferedInputStream(new FileInputStream(filePath))){
			return inputStreamToBase64(inputStream);
		} catch (FileNotFoundException e) {
			e.printStackTrace();
			throw new RuntimeException(e);
		} catch (IOException e) {
			e.printStackTrace();
			throw new RuntimeException(e);
		}
	}
	
	
}