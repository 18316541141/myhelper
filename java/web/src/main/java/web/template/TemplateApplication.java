package web.template;
import java.awt.Graphics;
import java.awt.Image;
import java.awt.image.BufferedImage;
import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;

import javax.imageio.ImageIO;

import org.apache.commons.io.FileUtils;
import org.jsoup.Jsoup;
import org.jsoup.nodes.Document;
import org.mybatis.spring.annotation.MapperScan;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;

import com.txj.common.ImageHandleHelper;
@MapperScan(value="web.template.mapper")
@SpringBootApplication
public class TemplateApplication {

	public static void main(String[] args) throws IOException {
		SpringApplication.run(TemplateApplication.class, args);
//		refreshVersion();
	}
	
	public static void adasdas(List<Integer> list){
		list=new ArrayList<Integer>();
		list.add(1);
	}
	
	/**
	 * 发布项目前，统一刷新版本号
	 */
	private static void refreshVersion() {
		String s=File.separator;
		String newVersion=new SimpleDateFormat("yyyyMMddHHmmss").format(new Date());
		try {
			FileUtils.writeStringToFile(new File("src"+s+"main"+s+"webapp"+s+"index.jsp"), "<!doctype html>\r\n"+
"<html>\r\n"+
	"<head></head>\r\n"+
	"<body>\r\n"+
		"<script type='text/javascript'>\r\n"+
			"setTimeout(function(){\r\n"+
				"location.href='index.html?v="+newVersion+"';\r\n"+
			"});\r\n"+
		"</script>\r\n"+
	"</body>\r\n"+
"</html>","UTF-8");
		} catch (IOException e) {
			e.printStackTrace();
		}
		try {
			File indexFile=new File("src"+s+"main"+s+"webapp"+s+"index.html");
			Document doc=Jsoup.parse(indexFile, "UTF-8");
			doc.selectFirst("[ng-include]").attr("ng-include", "x.url+'?v="+newVersion+"'");
			doc.selectFirst("script[src^='my-js/project.js']").attr("src","my-js/project.js?v="+newVersion);
			FileUtils.writeStringToFile(indexFile, doc.outerHtml(),"UTF-8");
		} catch (IOException e) {
			e.printStackTrace();
		}
	}
}
