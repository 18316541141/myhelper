package web.template.formDataConverter;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Date;
import org.apache.commons.lang3.StringUtils;
import org.apache.logging.log4j.Logger;
import org.springframework.core.convert.converter.Converter;

import web.template.entity.common.MyLog;

/**
 * form表单请求日期格式转换器
 * @author admin
 *
 */
public class DateConverter implements Converter<String, Date> {

	private Logger log;
	
	public DateConverter(){
		log=MyLog.getLogger(this.getClass());
	}
	
	private static final SimpleDateFormat[] DATE_FORMATS;

	static {
		DATE_FORMATS = new SimpleDateFormat[] { new SimpleDateFormat("yyyy-MM-dd HH:mm:ss"),
				new SimpleDateFormat("yyyy-MM-dd HH:mm"), new SimpleDateFormat("yyyy-MM-dd"),
				new SimpleDateFormat("yyyy/MM/dd HH:mm:ss"), new SimpleDateFormat("yyyy/MM/dd HH:mm"),
				new SimpleDateFormat("yyyy/MM/dd"), };
	}

	@Override
	public Date convert(String source) {
		if (!StringUtils.isEmpty(source) && !StringUtils.isEmpty(source=source.trim())) {
			for (int i = 0, len = DATE_FORMATS.length; i < len; i++) {
				try {
					return DATE_FORMATS[i].parse(source);
				} catch (ParseException e) {
					log.debug(e.getMessage());
				}
			}
		}
		return null;
	}
}