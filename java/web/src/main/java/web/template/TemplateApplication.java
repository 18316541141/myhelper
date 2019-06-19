package web.template;
import java.io.File;
import java.io.IOException;
import java.text.SimpleDateFormat;
import java.util.Date;

import org.apache.commons.io.FileUtils;
import org.jsoup.Jsoup;
import org.jsoup.nodes.Document;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.ComponentScan;

@SpringBootApplication
public class TemplateApplication {

	public static void main(String[] args) {
//		refreshVersion();
		SpringApplication.run(TemplateApplication.class, args);
	}
	
	/**
	 * 发布项目前，统一刷新版本号
	 */
	private static void refreshVersion() {
		String s=File.separator;
		String newVersion=new SimpleDateFormat("yyyyMMddHHmmss").format(new Date());
		try {
			FileUtils.writeStringToFile(new File("src"+s+"main"+s+"webapp"+s+"index.jsp"), "<script type='text/javascript'>location.href='/index.html?v="+newVersion+"';</script>","UTF-8");
		} catch (IOException e) {
			e.printStackTrace();
		}
		try {
			File indexFile=new File("src"+s+"main"+s+"webapp"+s+"index.html");
			Document doc=Jsoup.parse(FileUtils.readFileToString(indexFile, "UTF-8"));
			doc.selectFirst(".main-content").attr("ng-include", "{{x.url+'?v="+newVersion+"'}}");
			doc.selectFirst("script[src^='my-js/project.js']").attr("src","my-js/project.js?v="+newVersion);
			FileUtils.writeStringToFile(indexFile, doc.outerHtml(),"UTF-8");
		} catch (IOException e) {
			e.printStackTrace();
		}
	}
}
