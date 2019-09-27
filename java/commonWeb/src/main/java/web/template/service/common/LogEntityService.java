package web.template.service.common;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.txj.common.entity.MyPagedList;

import web.template.entity.common.LogEntity;
import web.template.mapper.common.LogEntityMapper;
import web.template.params.LogEntityParams;
import web.template.service.common.BaseService;

/**
 * “日志管理”模块的业务类
 * 
 * @author admin
 */
@Service
public class LogEntityService extends BaseService {

	@Autowired
	private LogEntityMapper mapper;

	/**
	 * 分页查询***模块，并返回查询结果
	 * 
	 * @param param
	 *            查询参数
	 * @param currentPageIndex
	 *            当前页码
	 * @param pageSize
	 *            每页显示的数据量
	 */
	public MyPagedList<LogEntity> page(LogEntityParams param, int currentPageIndex, int pageSize) {
		if("".equals(param.getLevel())){
			param.setLevel("");
		}
		return mapper.pageList(param, currentPageIndex, pageSize);
	}

	/**
	 * 根据主键id查询***模块的数据实体
	 * @param id
	 * @return
	 */
	public LogEntity load(long id) {
		LogEntity logEntity=new LogEntity();
		logEntity.setId(id);
		return mapper.findEntity(logEntity);
	}

	/*
	 * 抄考代码
	 * -------------------------------------------------------------------------
	 * --------------------------------------------------- LogEntity data = new
	 * LogEntity();
	 * data.setId(nextId());data.setCreateDate(param.getCreateDate());data.
	 * setLevel(param.getLevel());data.setThreadNo(param.getThreadNo());data.
	 * setMessage(param.getMessage());data.setProjectName(param.getProjectName()
	 * );data.setTypeName(param.getTypeName());data.setFuncName(param.
	 * getFuncName());data.setException(param.getException());
	 * -------------------------------------------------------------------------
	 * --------------------------------------------------- LogEntityParams param
	 * = new LogEntityParams();
	 * param.setId(param.getId());param.setCreateDate(param.getCreateDate());
	 * param.setCreateDateStart(param.getCreateDateStart());param.
	 * setCreateDateEnd(param.getCreateDateEnd());param.setLevel(param.getLevel(
	 * ));param.setLevelLike(param.getLevelLike());param.setThreadNo(param.
	 * getThreadNo());param.setThreadNoLike(param.getThreadNoLike());param.
	 * setMessage(param.getMessage());param.setMessageLike(param.getMessageLike(
	 * ));param.setProjectName(param.getProjectName());param.setProjectNameLike(
	 * param.getProjectNameLike());param.setTypeName(param.getTypeName());param.
	 * setTypeNameLike(param.getTypeNameLike());param.setFuncName(param.
	 * getFuncName());param.setFuncNameLike(param.getFuncNameLike());param.
	 * setException(param.getException());param.setExceptionLike(param.
	 * getExceptionLike());
	 * -------------------------------------------------------------------------
	 * --------------------------------------------------- 分页查询***模块，并返回查询结果
	 * 
	 * @param param 查询参数
	 * 
	 * @param currentPageIndex 当前页码
	 * 
	 * @param pageSize 每页显示的数据量 public MyPagedList<LogEntity>
	 * export(LogEntityParams param, int currentPageIndex,int pageSize) { return
	 * mapper.bigPageList(param, currentPageIndex, pageSize); }
	 * -------------------------------------------------------------------------
	 * --------------------------------------------------- 分页查询***模块，并返回查询结果
	 * 
	 * @param param 查询参数
	 * 
	 * @param currentPageIndex 当前页码
	 * 
	 * @param pageSize 每页显示的数据量 public MyPagedList<LogEntity>
	 * page(LogEntityParams param,int currentPageIndex,int pageSize){ return
	 * mapper.pageList(param, currentPageIndex, pageSize); }
	 * -------------------------------------------------------------------------
	 * --------------------------------------------------- 分页查询***模块，并返回查询结果
	 * 
	 * @param param 查询参数
	 * 
	 * @param currentPageIndex 当前页码
	 * 
	 * @param pageSize 每页显示的数据量 public MyPagedList<LogEntity>
	 * export(LogEntityParams param,int currentPageIndex,int pageSize){ return
	 * mapper.bigPageList(param, currentPageIndex, pageSize); }
	 * -------------------------------------------------------------------------
	 * --------------------------------------------------- 根据主键删除指定数据
	 * 
	 * @param id 删除数据的主键 public void del(long id){ mapper.delete(id); }
	 * -------------------------------------------------------------------------
	 * --------------------------------------------------- 新增一条数据
	 * 
	 * @param data 新增的数据 public void add(LogEntity data){ mapper.insert(data); }
	 * -------------------------------------------------------------------------
	 * --------------------------------------------------- 修改***模块的数据
	 * 
	 * @param data 修改数据，不为null才会被修改，主键必须不为null public void update(LogEntity
	 * data){ LogEntity param=new LogEntity();
	 * param.setId(nextId());param.setCreateDate(data.getCreateDate());param.
	 * setLevel(data.getLevel());param.setThreadNo(data.getThreadNo());param.
	 * setMessage(data.getMessage());param.setProjectName(data.getProjectName())
	 * ;param.setTypeName(data.getTypeName());param.setFuncName(data.getFuncName
	 * ());param.setException(data.getException()); mapper.updateChange(param);
	 * }
	 * -------------------------------------------------------------------------
	 * --------------------------------------------------- 新增多条数据
	 * 
	 * @param datas 新增的数据 public void addBatch(List<LogEntity> datas){
	 * List<LogEntity> insertDatas = new ArrayList<LogEntity>(); for(LogEntity
	 * data : datas){
	 * 
	 * insertDatas.add(); } mapper.insert(datas); }
	 * -------------------------------------------------------------------------
	 * --------------------------------------------------- 批量修改状态
	 * 
	 * @param datas 修改状态的数据 public int changeStatus(List<LogEntity> datas){
	 * List<LogEntity> updates = new List<LogEntity>(); for(LogEntity data :
	 * datas){ LogEntity update=new LogEntity(); update.setid(data.getid());
	 * update.setStatus(data.getStatus()); updates.add(update); } return
	 * mapper.updateChangeBatch(updates); }
	 * -------------------------------------------------------------------------
	 * --------------------------------------------------- 根据主键id查询***模块的数据实体
	 * 
	 * @param id 主键id
	 * 
	 * @return 返回***模块的查询结果 public LogEntity load(long id){ return
	 * mapper.findEntity(id); }
	 * -------------------------------------------------------------------------
	 * --------------------------------------------------- 批量删除数据
	 * 
	 * @param datas 批量删除的数据 public int delBatch(List<LogEntity> datas){ return
	 * mapper.deleteList(datas); }
	 * -------------------------------------------------------------------------
	 * ---------------------------------------------------
	 * 对“****”方法的入参进行校验基础（只校验是否为空，格式，长度，大小，不涉及 数据库查询、流读取等其他复杂操作），校验不通过时，异常往外抛出
	 * 
	 * @param param 待校验参数 public void check****(LogEntityParams param){ if(...){
	 * Ex(...,...); } }
	 */
}