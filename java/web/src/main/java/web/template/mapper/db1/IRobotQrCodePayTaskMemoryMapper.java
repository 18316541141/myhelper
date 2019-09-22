package web.template.mapper.db1;
import web.template.entity.db1.codeGenerator.IRobotQrCodePayTask;
import web.template.mapper.common.BaseMemoryMapper;
import web.template.params.db1.codeGenerator.IRobotQrCodePayTaskParams;
import web.template.setNullParams.db1.codeGenerator.IRobotQrCodePayTaskSetNullParams;
public class IRobotQrCodePayTaskMemoryMapper extends BaseMemoryMapper<IRobotQrCodePayTask, IRobotQrCodePayTaskParams, IRobotQrCodePayTaskSetNullParams>{

	@Override
	protected void setNullOper(IRobotQrCodePayTask entity, IRobotQrCodePayTaskSetNullParams setNullParams) {
		// TODO Auto-generated method stub
		
	}

	@Override
	protected boolean isTarget(IRobotQrCodePayTask entity, IRobotQrCodePayTaskParams params,
			IRobotQrCodePayTaskParams nparams) {
		// TODO Auto-generated method stub
		return false;
	}

	@Override
	protected void updateChangeOper(IRobotQrCodePayTask src, IRobotQrCodePayTask entity) {
		// TODO Auto-generated method stub
		
	}

}
