package com.txj.common;
import java.awt.Graphics;
import java.awt.Image;
import java.awt.Rectangle;
import java.awt.geom.AffineTransform;
import java.awt.image.BufferedImage;
import java.awt.image.ColorModel;
import java.awt.image.ImageObserver;
import java.awt.image.WritableRaster;

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
	 * @return 返回切割后的图片
	 */
	public static Image cutPicByRect(Image image, Rectangle cutRect) {
		BufferedImage bufferedImage = new BufferedImage(cutRect.width, cutRect.height, BufferedImage.TYPE_INT_RGB);
		Graphics g = bufferedImage.getGraphics();
		g.drawImage(image, 0, 0, cutRect.width, cutRect.height, cutRect.x, cutRect.y, cutRect.x+cutRect.width, cutRect.y+cutRect.height, null);
		g.dispose();
		return image;
	}
}