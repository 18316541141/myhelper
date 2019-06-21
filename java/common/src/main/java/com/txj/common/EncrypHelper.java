package com.txj.common;
import java.awt.Graphics;
import java.awt.Image;
import java.awt.image.BufferedImage;
import java.io.BufferedInputStream;
import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.InputStream;
import java.util.Arrays;
import java.util.Base64;
import java.util.Base64.Encoder;

import javax.imageio.ImageIO;

import org.apache.commons.io.IOUtils;
import org.apache.shiro.crypto.hash.Md5Hash;
import org.apache.shiro.crypto.hash.Sha1Hash;

/**
 * 加密帮助类
 * @author admin
 */
public class EncrypHelper {
	
	/**
	 * 读取输入流，并转化为base64返回
	 * @param	inputStream	输入流
	 * @return	返回文件的base64
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
	 * 图片转base64
	 * @param image	图片对象
	 * @return	返回图片的base64
	 */
	public static String imageToBase64(Image image){
		BufferedImage bufferedImage = new BufferedImage(image.getWidth(null), image.getHeight(null),BufferedImage.TYPE_INT_RGB);
		Graphics g = bufferedImage.createGraphics();   
        g.drawImage(image, 0, 0, null);   
        g.dispose();
		ByteArrayOutputStream output=new ByteArrayOutputStream();
		try {
			ImageIO.write(bufferedImage, "jpeg", output);
			ByteArrayInputStream input=new ByteArrayInputStream(output.toByteArray());
			return inputStreamToBase64(input);
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
	
	/**
	 * 把输入流转化为base64（注意：该方法不能用于处理超大文件，否则内存不足）
	 * @param inputStream
	 * @return
	 */
	public static String getSha1FromInputStream(InputStream inputStream){
		ByteArrayOutputStream outputStream=new ByteArrayOutputStream();
		try {
			IOUtils.copy(inputStream, outputStream);
			Sha1Hash sha1=new Sha1Hash();
			sha1.setBytes(outputStream.toByteArray());
			return sha1.toHex();
		} catch (IOException e) {
			e.printStackTrace();
			throw new RuntimeException(e);
		}
	}
	
	/**
	 * 把输入流转化为MD5（注意：该方法不能用于处理超大文件，否则内存不足）
	 * @param inputStream
	 * @return
	 */
	public static String getMD5FromInputStream(InputStream inputStream){
		ByteArrayOutputStream outputStream=new ByteArrayOutputStream();
		try {
			IOUtils.copy(inputStream, outputStream);
			Md5Hash md5=new Md5Hash();
			md5.setBytes(outputStream.toByteArray());
			return md5.toHex();
		} catch (IOException e) {
			e.printStackTrace();
			throw new RuntimeException(e);
		}
	}
	
	/**
	 * 把图片转化为sha1
	 * @param image	图片
	 * @return	返回sha1
	 */
	public static String getSha1FromImage(Image image){
		BufferedImage bufferedImage = new BufferedImage(image.getWidth(null), image.getHeight(null),BufferedImage.TYPE_INT_RGB);
		Graphics g = bufferedImage.createGraphics();   
        g.drawImage(image, 0, 0, null);   
        g.dispose();
        ByteArrayOutputStream output=new ByteArrayOutputStream();
		try {
			ImageIO.write(bufferedImage, "jpeg", output);
			Sha1Hash sha1=new Sha1Hash();
			sha1.setBytes(output.toByteArray());
			return sha1.toHex();
		} catch (IOException e) {
			e.printStackTrace();
			throw new RuntimeException(e);
		}
	}
	
	/**
	 * 获取图片的md5（注意：该方法不能用于处理超大文件，否则内存不足）
	 * @param image	
	 * @return
	 */
	public static String getMD5FromImage(Image image){
		BufferedImage bufferedImage = new BufferedImage(image.getWidth(null), image.getHeight(null),BufferedImage.TYPE_INT_RGB);
		Graphics g = bufferedImage.createGraphics();   
        g.drawImage(image, 0, 0, null);   
        g.dispose();
        ByteArrayOutputStream output=new ByteArrayOutputStream();
		try {
			ImageIO.write(bufferedImage, "jpeg", output);
			Md5Hash md5=new Md5Hash();
			md5.setBytes(output.toByteArray());
			return md5.toHex();
		} catch (IOException e) {
			e.printStackTrace();
			throw new RuntimeException(e);
		}
	}
}