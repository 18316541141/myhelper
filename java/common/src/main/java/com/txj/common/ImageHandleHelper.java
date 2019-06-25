package com.txj.common;

import java.awt.Dimension;
import java.awt.Graphics;
import java.awt.Image;
import java.awt.Rectangle;
import java.awt.Toolkit;
import java.awt.image.BufferedImage;
import java.io.File;
import java.io.IOException;

import javax.imageio.ImageIO;

/**
 * 图像处理帮助类
 * 
 * @author admin
 *
 */
public class ImageHandleHelper {

	/**
	 * 图像缩放
	 * @param image	源图像
	 * @param width	缩放后的宽度
	 * @param height	缩放后的高度
	 * @return	返回缩放后的图片
	 */
	public static Image scale(Image image, int width, int height) {
		BufferedImage bufferedImage = new BufferedImage(width, height, BufferedImage.TYPE_INT_RGB);
		Graphics g = bufferedImage.getGraphics();
		g.drawImage(image, 0, 0, null); // 绘制缩小后的图
		g.dispose();
		return bufferedImage;
	}

	/**
	 * 根据矩形范围，截取部分图片
	 * 
	 * @param image
	 *            原图
	 * @param cutRect
	 *            切割的矩形范围
	 * @param autoDispose
	 *            是否自动回收资源
	 * @return 返回切割后的图片
	 */
	public static Image cutPicByRect(Image image, Rectangle cutRect, boolean autoDispose) {
		BufferedImage tag = new BufferedImage(image.getWidth(null), image.getHeight(null), BufferedImage.TYPE_INT_RGB);
		Graphics g = tag.getGraphics();
		g.getClipBounds(cutRect);
		g.dispose();

		return image;
	}
	
	public static void main(String[] args) {
		try {
			BufferedImage bi=ImageIO.read(new File("C:\\Users\\TEMP.DESKTOP-3UIND36.124\\Desktop\\无标题.png"));
//			ImageIO.geti
		} catch (IOException e) {
			e.printStackTrace();
		}
	}
}