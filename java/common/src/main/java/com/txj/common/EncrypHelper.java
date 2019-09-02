package com.txj.common;
import java.awt.Graphics;
import java.awt.Image;
import java.awt.image.BufferedImage;
import java.io.BufferedInputStream;
import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.InputStream;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.util.Arrays;
import java.util.Base64;
import java.util.Base64.Encoder;

import javax.imageio.ImageIO;

import org.apache.commons.io.IOUtils;

/**
 * 加密帮助类
 * @author admin
 */
public final class EncrypHelper {
	
	/**
	 * 读取输入流，并转化为base64返回
	 * @param	inputStream	输入流
	 * @return	返回文件的base64
	 */
	public static String inputStreamToBase64(final InputStream inputStream){
		final Encoder encoder = Base64.getEncoder();
		final byte[] buffer=new byte[Integer.MAX_VALUE];
		try {
			final byte[]  fixedBuffer = Arrays.copyOf(buffer, IOUtils.read(inputStream, buffer));
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
	public static String imageToBase64(final Image image){
		final BufferedImage bufferedImage = new BufferedImage(image.getWidth(null), image.getHeight(null),BufferedImage.TYPE_INT_RGB);
		final Graphics g = bufferedImage.createGraphics();   
        g.drawImage(image, 0, 0, null);   
        g.dispose();
		try (final ByteArrayOutputStream output=new ByteArrayOutputStream()){
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
	public static String fileToBase64(final String filePath){
		try(final InputStream inputStream =new BufferedInputStream(new FileInputStream(filePath))){
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
	 * 把文件转化为sha1
	 * @param file 文件
	 * @return 返回sha1
	 */
	public static String getSha1FromFile(final File file){
		try (final FileInputStream fileInputStream=new FileInputStream(file)){
			return getSha1FromInputStream(fileInputStream);
		} catch (FileNotFoundException e) {
			e.printStackTrace();
			return null;
		} catch (IOException e1) {
			e1.printStackTrace();
			return null;
		}
	}
	
	/**
	 * 把字符串转化为sha1加密串
	 * @param text
	 * @return
	 */
	public static String getSha1FromString(final String text){
		final char[] hexDigits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9','a', 'b', 'c', 'd', 'e', 'f'};
	    try {
	    	final MessageDigest mdTemp = MessageDigest.getInstance("SHA1");
	    	final byte[] buff;
	        try {
	        	buff=text.getBytes("UTF-8");
				mdTemp.update(buff, 0, buff.length);
			} catch (IOException e) {
				e.printStackTrace();
			}
	        final byte[] md = mdTemp.digest();
	        final int j = md.length;
	        final char[] buf = new char[j * 2];
	        int k = 0;
	        for (int i = 0; i < j; i++) {
	        	final byte byte0 = md[i];
	            buf[k++] = hexDigits[byte0 >>> 4 & 0xf];
	            buf[k++] = hexDigits[byte0 & 0xf];
	        }
	        return new String(buf);
	    } catch (NoSuchAlgorithmException e) {
	        e.printStackTrace();
	        return null;
	    }
	}
	
	/**
	 * 把输入流转化为sha1，并关闭流
	 * @param inputStream 输入流
	 * @return
	 */
	public static String getSha1FromInputStream(final InputStream inputStream){
		final char[] hexDigits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9','a', 'b', 'c', 'd', 'e', 'f'};
	    try {
	    	final MessageDigest mdTemp = MessageDigest.getInstance("SHA1");
	    	final byte[] buff=new byte[2048];
	        try {
				for(int len;(len=inputStream.read(buff))>-1;){	        	
					mdTemp.update(buff, 0, len);
				}
			} catch (IOException e) {
				e.printStackTrace();
			}finally{
				try {
					inputStream.close();
				} catch (IOException e) {
					e.printStackTrace();
				}
			}
	        final byte[] md = mdTemp.digest();
	        final int j = md.length;
	        final char[] buf = new char[j * 2];
	        int k = 0;
	        for (int i = 0; i < j; i++) {
	            byte byte0 = md[i];
	            buf[k++] = hexDigits[byte0 >>> 4 & 0xf];
	            buf[k++] = hexDigits[byte0 & 0xf];
	        }
	        return new String(buf);
	    } catch (NoSuchAlgorithmException e) {
	        e.printStackTrace();
	        return null;
	    }
	}
	
	/**
	 * 把文件转化为MD5
	 * @param file
	 * @return
	 */
	public static String getMD5FromFile(final File file){
		try (final FileInputStream fileInputStream=new FileInputStream(file)){
			return getMD5FromInputStream(fileInputStream);
		} catch (FileNotFoundException e) {
			e.printStackTrace();
			return null;
		} catch (IOException e1) {
			e1.printStackTrace();
			return null;
		}
	}
	
	/**
	 * 把输入流转化为MD5
	 * @param inputStream
	 * @return
	 */
	public static String getMD5FromInputStream(final InputStream inputStream){
		try {
			final MessageDigest m = MessageDigest.getInstance("MD5");
			final byte[] buff=new byte[2048];
	        try {
				for(int len;(len=inputStream.read(buff))>-1;){	        	
					m.update(buff, 0, len);
				}
			} catch (IOException e) {
				e.printStackTrace();
			}
	        final byte s[] = m.digest();
			String result = "";
			for (int i = 0; i < s.length; i++) {
				result += Integer.toHexString((0x000000FF & s[i]) | 0xFFFFFF00).substring(6);
			}
			return result;
		} catch (Exception e) {
			e.printStackTrace();
			return "";
		}
	}
	
	/**
	 * 把图片转化为sha1
	 * @param image	图片
	 * @return	返回sha1
	 */
	public static String getSha1FromImage(final Image image){
		final BufferedImage bufferedImage = new BufferedImage(image.getWidth(null), image.getHeight(null),BufferedImage.TYPE_INT_RGB);
		final Graphics g = bufferedImage.createGraphics();   
        g.drawImage(image, 0, 0, null);   
        g.dispose();
		try (final ByteArrayOutputStream output=new ByteArrayOutputStream()){
			ImageIO.write(bufferedImage, "jpeg", output);
			ByteArrayInputStream input=new ByteArrayInputStream(output.toByteArray());
			return getSha1FromInputStream(input);
		} catch (IOException e) {
			e.printStackTrace();
			throw new RuntimeException(e);
		}
	}
	
	/**
	 * 获取图片的md5（base64格式）
	 * @param image	
	 * @return
	 */
	public static String getMD5FromImage(final Image image){
		final BufferedImage bufferedImage = new BufferedImage(image.getWidth(null), image.getHeight(null),BufferedImage.TYPE_INT_RGB);
		final Graphics g = bufferedImage.createGraphics();   
        g.drawImage(image, 0, 0, null);   
        g.dispose();
		try(final ByteArrayOutputStream output=new ByteArrayOutputStream()) {
			ImageIO.write(bufferedImage, "jpeg", output);
			ByteArrayInputStream input=new ByteArrayInputStream(output.toByteArray());
			return getMD5FromInputStream(input);
		} catch (IOException e) {
			e.printStackTrace();
			throw new RuntimeException(e);
		}
	}
}