package com.txj.common;

import java.awt.Graphics;
import java.awt.Image;
import java.awt.image.BufferedImage;
import java.io.BufferedOutputStream;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.util.UUID;
import javax.imageio.ImageIO;
import org.apache.commons.io.FileUtils;
public final class FileHelper {
	/**
	 * 保存输入流为sha1
	 * @param inputStream
	 * @param basePath
	 * @return
	 */
	public static String SaveInputStreamBySha1(final InputStream inputStream,final String basePath){
		new File(basePath+File.separator).mkdirs();
		try {
			final File uuidFile=new File(basePath+File.separator+UUID.randomUUID().toString());
			FileUtils.copyInputStreamToFile(inputStream, uuidFile);
			final String sha1=EncrypHelper.getSha1FromFile(uuidFile);
			final File sha1File=new File(basePath+File.separator+sha1);
			if(sha1File.exists()){
				FileUtils.deleteQuietly(uuidFile);
			}else{
				FileUtils.moveFile(uuidFile, sha1File);
			}
			return sha1;
		} catch (IOException e) {
			e.printStackTrace();
			throw new RuntimeException(e);
		}
	}
	
	/**
	 * 保存图片，并以sha1命名
	 * @param image	图片
	 * @param basePath	保存的目录位置
	 * @return	返回图片的sha1
	 */
	public static String SaveImageBySha1(final Image image, final String basePath){
		final BufferedImage bufferedImage = new BufferedImage(image.getWidth(null), image.getHeight(null),BufferedImage.TYPE_INT_RGB);
		final Graphics g = bufferedImage.createGraphics();   
        g.drawImage(image, 0, 0, null);   
        g.dispose();
        FileOutputStream fileOutputStream = null;
        BufferedOutputStream bufferedOutputStream=null;
		try {
			final String sha1=EncrypHelper.getSha1FromImage(image);
			new File(basePath+File.separator).mkdirs();
			final String filename=basePath+File.separator+sha1;
			if(!new File(filename).exists()){
				fileOutputStream = new FileOutputStream(filename);
				bufferedOutputStream=new BufferedOutputStream(fileOutputStream);
				ImageIO.write(bufferedImage, "jpeg", bufferedOutputStream);				
			}
			return sha1;
		} catch (FileNotFoundException e) {
			e.printStackTrace();
			throw new RuntimeException(e);
		} catch (IOException e) {
			e.printStackTrace();
			throw new RuntimeException(e);
		}finally {
			try {
				if(bufferedOutputStream!=null){
					bufferedOutputStream.close();
				}
				if(fileOutputStream!=null){
					fileOutputStream.close();
				}
			} catch (Exception e2) {
			}
		}
	}
}
