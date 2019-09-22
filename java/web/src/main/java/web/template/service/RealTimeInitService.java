package web.template.service;
import org.springframework.stereotype.Service;
import web.template.intf.IRealTimeInitService;

@Service
public class RealTimeInitService implements IRealTimeInitService{
	@Override
	public boolean init(String realTimePool, String username) {
		return false;
	}
}