package web.template.service;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import web.template.entity.MyPagedList;
import web.template.entity.codeGenerator.IRobotQrCodePayTask;
import web.template.entity.params.codeGenerator.IRobotQrCodePayTaskParams;
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
	public MyPagedList<IRobotQrCodePayTask> page(IRobotQrCodePayTaskParams params,Integer currentPageIndex,Integer pageSize){
		return irobotQrCodePayTaskMapper.pageList(params, currentPageIndex, pageSize);
	}
}
