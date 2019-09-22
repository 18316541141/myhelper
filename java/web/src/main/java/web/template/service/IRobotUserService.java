package web.template.service;

import java.util.ArrayList;
import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.txj.common.entity.LeftMenu;

import web.template.entity.common.LogEntity;
import web.template.entity.db1.codeGenerator.IRobotUser;
import web.template.intf.IUserService;
import web.template.mapper.db1.IRobotUserMapper;
import web.template.params.db1.codeGenerator.IRobotUserParams;
import web.template.service.common.BaseService;

/**
 * “用户管理模块”模块的业务类
 * 
 * @author admin
 */
@Service
public class IRobotUserService extends BaseService implements IUserService {

	@Autowired
	private IRobotUserMapper mapper;

	@Override
	public String findSecretByKey(String signKey) {
		IRobotUserParams irobotUserParams = new IRobotUserParams();
		irobotUserParams.setIuSignKey(signKey);
		IRobotUser irobotUser = mapper.findEntityByParams(irobotUserParams);
		return irobotUser == null ? null : irobotUser.getIuSignSecret();
	}

	@Override
	public String loadUsernameBySignKey(String signKey) {
		IRobotUserParams irobotUserParams = new IRobotUserParams();
		irobotUserParams.setIuSignKey(signKey);
		IRobotUser irobotUser = mapper.findEntityByParams(irobotUserParams);
		return irobotUser == null ? null : irobotUser.getIuUsername();
	}

	@Override
	public String loadPasswordByUsername(String username) {
		IRobotUserParams irobotUserParams = new IRobotUserParams();
		irobotUserParams.setIuUsername(username);
		IRobotUser irobotUser = mapper.findEntityByParams(irobotUserParams);
		return irobotUser == null ? null : irobotUser.getIuPassword();
	}

	@Override
	public List<LeftMenu> loadLeftMenus(String username) {
		List<LeftMenu> leftMenuList = new ArrayList<LeftMenu>();
		LeftMenu sysMenu = new LeftMenu();
		sysMenu.setId("system");
		sysMenu.setSortIndex(0);
		sysMenu.setTitle("系统管理");
		LeftMenu heartbeatMenu = new LeftMenu();
		heartbeatMenu.setId("heartbeatEntity");
		heartbeatMenu.setSortIndex(0);
		heartbeatMenu.setTitle("心跳监测");
		sysMenu.getLeftMenus().add(heartbeatMenu);
		LeftMenu logEntityMenu = new LeftMenu();
		logEntityMenu.setId("logEntity");
		logEntityMenu.setSortIndex(1);
		logEntityMenu.setTitle("系统日志");
		sysMenu.getLeftMenus().add(logEntityMenu);
		leftMenuList.add(sysMenu);
		return leftMenuList;
	}

	/*
	 * 抄考代码
	 * -------------------------------------------------------------------------
	 * --------------------------------------------------- IRobotUser data = new
	 * IRobotUser();
	 * data.setIuId(nextId());data.setIuUsername(param.getIuUsername());data.
	 * setIuPassword(param.getIuPassword());data.setIuSignKey(param.getIuSignKey
	 * ());data.setIuSignSecret(param.getIuSignSecret());data.setIuCreateDate(
	 * param.getIuCreateDate());
	 * -------------------------------------------------------------------------
	 * --------------------------------------------------- IRobotUserParams
	 * param = new IRobotUserParams();
	 * param.setIuId(param.getIuId());param.setIuUsername(param.getIuUsername())
	 * ;param.setIuUsernameLike(param.getIuUsernameLike());param.setIuPassword(
	 * param.getIuPassword());param.setIuPasswordLike(param.getIuPasswordLike())
	 * ;param.setIuSignKey(param.getIuSignKey());param.setIuSignKeyLike(param.
	 * getIuSignKeyLike());param.setIuSignSecret(param.getIuSignSecret());param.
	 * setIuSignSecretLike(param.getIuSignSecretLike());param.setIuCreateDate(
	 * param.getIuCreateDate());param.setIuCreateDateStart(param.
	 * getIuCreateDateStart());param.setIuCreateDateEnd(param.getIuCreateDateEnd
	 * ());
	 * -------------------------------------------------------------------------
	 * --------------------------------------------------- 分页查询***模块，并返回查询结果
	 * 
	 * @param param 查询参数
	 * 
	 * @param currentPageIndex 当前页码
	 * 
	 * @param pageSize 每页显示的数据量 public MyPagedList<IRobotUser>
	 * export(IRobotUserParams param, int currentPageIndex,int pageSize) {
	 * return mapper.bigPageList(param, currentPageIndex, pageSize); }
	 * -------------------------------------------------------------------------
	 * --------------------------------------------------- 分页查询***模块，并返回查询结果
	 * 
	 * @param param 查询参数
	 * 
	 * @param currentPageIndex 当前页码
	 * 
	 * @param pageSize 每页显示的数据量 public MyPagedList<IRobotUser>
	 * page(IRobotUserParams param,int currentPageIndex,int pageSize){ return
	 * mapper.pageList(param, currentPageIndex, pageSize); }
	 * -------------------------------------------------------------------------
	 * --------------------------------------------------- 分页查询***模块，并返回查询结果
	 * 
	 * @param param 查询参数
	 * 
	 * @param currentPageIndex 当前页码
	 * 
	 * @param pageSize 每页显示的数据量 public MyPagedList<IRobotUser>
	 * export(IRobotUserParams param,int currentPageIndex,int pageSize){ return
	 * mapper.bigPageList(param, currentPageIndex, pageSize); }
	 * -------------------------------------------------------------------------
	 * --------------------------------------------------- 根据主键删除指定数据
	 * 
	 * @param iuId 删除数据的主键 public void del(long iuId){ mapper.delete(iuId); }
	 * -------------------------------------------------------------------------
	 * --------------------------------------------------- 新增一条数据
	 * 
	 * @param data 新增的数据 public void add(IRobotUser data){ mapper.insert(data);
	 * }
	 * -------------------------------------------------------------------------
	 * --------------------------------------------------- 修改***模块的数据
	 * 
	 * @param data 修改数据，不为null才会被修改，主键必须不为null public void update(IRobotUser
	 * data){ IRobotUser param=new IRobotUser();
	 * param.setIuId(nextId());param.setIuUsername(data.getIuUsername());param.
	 * setIuPassword(data.getIuPassword());param.setIuSignKey(data.getIuSignKey(
	 * ));param.setIuSignSecret(data.getIuSignSecret());param.setIuCreateDate(
	 * data.getIuCreateDate()); mapper.updateChange(param); }
	 * -------------------------------------------------------------------------
	 * --------------------------------------------------- 新增多条数据
	 * 
	 * @param datas 新增的数据 public void addBatch(List<IRobotUser> datas){
	 * List<IRobotUser> insertDatas = new ArrayList<IRobotUser>();
	 * for(IRobotUser data : datas){
	 * 
	 * insertDatas.add(); } mapper.insert(datas); }
	 * -------------------------------------------------------------------------
	 * --------------------------------------------------- 批量修改状态
	 * 
	 * @param datas 修改状态的数据 public int changeStatus(List<IRobotUser> datas){
	 * List<IRobotUser> updates = new List<IRobotUser>(); for(IRobotUser data :
	 * datas){ IRobotUser update=new IRobotUser();
	 * update.setiuId(data.getiuId()); update.setStatus(data.getStatus());
	 * updates.add(update); } return mapper.updateChangeBatch(updates); }
	 * -------------------------------------------------------------------------
	 * --------------------------------------------------- 根据主键id查询***模块的数据实体
	 * 
	 * @param iuId 主键id
	 * 
	 * @return 返回***模块的查询结果 public IRobotUser load(long iuId){ return
	 * mapper.findEntity(iuId); }
	 * -------------------------------------------------------------------------
	 * --------------------------------------------------- 批量删除数据
	 * 
	 * @param datas 批量删除的数据 public int delBatch(List<IRobotUser> datas){ return
	 * mapper.deleteList(datas); }
	 * -------------------------------------------------------------------------
	 * ---------------------------------------------------
	 * 对“****”方法的入参进行校验基础（只校验是否为空，格式，长度，大小，不涉及 数据库查询、流读取等其他复杂操作），校验不通过时，异常往外抛出
	 * 
	 * @param param 待校验参数 public void check****(IRobotUserParams param){
	 * if(...){ Ex(...,...); } }
	 */
}