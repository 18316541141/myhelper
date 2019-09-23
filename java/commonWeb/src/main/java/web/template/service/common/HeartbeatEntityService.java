package web.template.service.common;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import com.txj.common.entity.MyPagedList;
import web.template.entity.common.HeartbeatEntity;
import web.template.mapper.common.HeartbeatEntityMapper;
import web.template.params.HeartbeatEntityParams;
import web.template.service.common.BaseService;

/**
 * “心跳监测”模块的业务类
 * 
 * @author admin
 */
@Service
public class HeartbeatEntityService extends BaseService {

	@Autowired
	private HeartbeatEntityMapper mapper;

	/**
	 * 分页查询***模块，并返回查询结果
	 * 
	 * @param param
	 *            查询参数
	 * 
	 * @param currentPageIndex
	 *            当前页码
	 * 
	 * @param pageSize
	 *            每页显示的数据量
	 */
	public MyPagedList<HeartbeatEntity> page(HeartbeatEntityParams param, int currentPageIndex, int pageSize) {
		return mapper.pageList(param, currentPageIndex, pageSize);
	}

	/*
	 * 抄考代码
	 * -------------------------------------------------------------------------
	 * --------------------------------------------------- HeartbeatEntity data
	 * = new HeartbeatEntity();
	 * data.setId(nextId());data.setLastHeartbeat(param.getLastHeartbeat());data
	 * .setRobotId(param.getRobotId());
	 * -------------------------------------------------------------------------
	 * --------------------------------------------------- HeartbeatEntityParams
	 * param = new HeartbeatEntityParams();
	 * param.setId(param.getId());param.setLastHeartbeat(param.getLastHeartbeat(
	 * ));param.setLastHeartbeatStart(param.getLastHeartbeatStart());param.
	 * setLastHeartbeatEnd(param.getLastHeartbeatEnd());param.setRobotId(param.
	 * getRobotId());param.setRobotIdLike(param.getRobotIdLike());
	 * -------------------------------------------------------------------------
	 * --------------------------------------------------- 分页查询***模块，并返回查询结果
	 * 
	 * @param param 查询参数
	 * 
	 * @param currentPageIndex 当前页码
	 * 
	 * @param pageSize 每页显示的数据量 public MyPagedList<HeartbeatEntity>
	 * export(HeartbeatEntityParams param, int currentPageIndex,int pageSize) {
	 * return mapper.bigPageList(param, currentPageIndex, pageSize); }
	 * -------------------------------------------------------------------------
	 * --------------------------------------------------- 分页查询***模块，并返回查询结果
	 * 
	 * @param param 查询参数
	 * 
	 * @param currentPageIndex 当前页码
	 * 
	 * @param pageSize 每页显示的数据量 public MyPagedList<HeartbeatEntity>
	 * page(HeartbeatEntityParams param,int currentPageIndex,int pageSize){
	 * return mapper.pageList(param, currentPageIndex, pageSize); }
	 * -------------------------------------------------------------------------
	 * --------------------------------------------------- 分页查询***模块，并返回查询结果
	 * 
	 * @param param 查询参数
	 * 
	 * @param currentPageIndex 当前页码
	 * 
	 * @param pageSize 每页显示的数据量 public MyPagedList<HeartbeatEntity>
	 * export(HeartbeatEntityParams param,int currentPageIndex,int pageSize){
	 * return mapper.bigPageList(param, currentPageIndex, pageSize); }
	 * -------------------------------------------------------------------------
	 * --------------------------------------------------- 根据主键删除指定数据
	 * 
	 * @param id 删除数据的主键 public void del(long id){ mapper.delete(id); }
	 * -------------------------------------------------------------------------
	 * --------------------------------------------------- 新增一条数据
	 * 
	 * @param data 新增的数据 public void add(HeartbeatEntity data){
	 * mapper.insert(data); }
	 * -------------------------------------------------------------------------
	 * --------------------------------------------------- 修改***模块的数据
	 * 
	 * @param data 修改数据，不为null才会被修改，主键必须不为null public void
	 * update(HeartbeatEntity data){ HeartbeatEntity param=new
	 * HeartbeatEntity();
	 * param.setId(nextId());param.setLastHeartbeat(data.getLastHeartbeat());
	 * param.setRobotId(data.getRobotId()); mapper.updateChange(param); }
	 * -------------------------------------------------------------------------
	 * --------------------------------------------------- 新增多条数据
	 * 
	 * @param datas 新增的数据 public void addBatch(List<HeartbeatEntity> datas){
	 * List<HeartbeatEntity> insertDatas = new ArrayList<HeartbeatEntity>();
	 * for(HeartbeatEntity data : datas){
	 * 
	 * insertDatas.add(); } mapper.insert(datas); }
	 * -------------------------------------------------------------------------
	 * --------------------------------------------------- 批量修改状态
	 * 
	 * @param datas 修改状态的数据 public int changeStatus(List<HeartbeatEntity>
	 * datas){ List<HeartbeatEntity> updates = new List<HeartbeatEntity>();
	 * for(HeartbeatEntity data : datas){ HeartbeatEntity update=new
	 * HeartbeatEntity(); update.setid(data.getid());
	 * update.setStatus(data.getStatus()); updates.add(update); } return
	 * mapper.updateChangeBatch(updates); }
	 * -------------------------------------------------------------------------
	 * --------------------------------------------------- 根据主键id查询***模块的数据实体
	 * 
	 * @param id 主键id
	 * 
	 * @return 返回***模块的查询结果 public HeartbeatEntity load(long id){ return
	 * mapper.findEntity(id); }
	 * -------------------------------------------------------------------------
	 * --------------------------------------------------- 批量删除数据
	 * 
	 * @param datas 批量删除的数据 public int delBatch(List<HeartbeatEntity> datas){
	 * return mapper.deleteList(datas); }
	 * -------------------------------------------------------------------------
	 * ---------------------------------------------------
	 * 对“****”方法的入参进行校验基础（只校验是否为空，格式，长度，大小，不涉及 数据库查询、流读取等其他复杂操作），校验不通过时，异常往外抛出
	 * 
	 * @param param 待校验参数 public void check****(HeartbeatEntityParams param){
	 * if(...){ Ex(...,...); } }
	 */
}