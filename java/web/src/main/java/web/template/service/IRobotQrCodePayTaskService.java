package web.template.service;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import web.template.entity.IRobotQrCodePayTask;
import web.template.entity.MyPagedList;
import web.template.entity.param.IRobotQrCodePayTaskParam;
import web.template.mapper.IRobotQrCodePayTaskMapper;

@Service
public class IRobotQrCodePayTaskService {
	
	@Autowired
	private IRobotQrCodePayTaskMapper irobotQrCodePayTaskMapper;
	
	/**
	 * 
	 * @param param
	 * @param pageIndex
	 * @param pageSize
	 * @return
	 */
	public MyPagedList<IRobotQrCodePayTask> page(IRobotQrCodePayTaskParam param,int currentPageIndex,int pageSize){
		return irobotQrCodePayTaskMapper.pageList(param, currentPageIndex, pageSize);
	}
}
