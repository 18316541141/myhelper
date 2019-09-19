using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CommonHelper.AopInterceptor;
using CommonHelper.EFRepository;
using WebApplication1.Entity.Common;
using CommonHelper.EFMap;
using CommonHelper.CommonEntity;
using System.Threading;
using CommonWeb.Service.Common;
using CommonWeb.Repository;

namespace CommonWeb.Service
{
	/// <summary>
    /// “心跳监测”模块的业务类
    /// </summary>
	//[Intercept(typeof(DistributedTransactionScan))]	//启用分布式事务扫描
	public partial class HeartbeatEntityService : BaseService
    {
		public HeartbeatEntityRepository Repository { set; get; }

        /// <summary>
        /// 分页查询“心跳监测”模块，并返回查询结果
        /// </summary>
        /// <param name="param">查询参数</param>
        /// <param name="currentPageIndex">当前页码</param>
        /// <param name="pageSize">每页显示的数据量</param>
        /// <returns>返回“心跳监测”模块的查询结果</returns>
		public MyPagedList<HeartbeatEntity> Page(HeartbeatEntityParams param, int currentPageIndex = 1, int pageSize = 20)
        {
            return Repository.PageList(param, currentPageIndex, pageSize);
        }

        /// <summary>
        /// 记录心跳信息
        /// </summary>
        /// <param name="robotId">机器人id</param>
        public void RecordHeartbeat(string robotId)
        {
            using (Mutex mutex = new Mutex(false, robotId))
            {
                mutex.WaitOne();
                try
                {

                    HeartbeatEntity heartbeatEntity = Repository.FindEntity(a => a.RobotId == robotId);
                    if (heartbeatEntity==null)
                    {
                        Repository.Insert(new HeartbeatEntity
                        {
                            Id = NextId(),
                            RobotId = robotId,
                            LastHeartbeatTime = DateTime.Now
                        });
                    }
                    else
                    {
                        Repository.UpdateChange(new HeartbeatEntity
                        {
                            Id = heartbeatEntity.Id,
                            LastHeartbeatTime = DateTime.Now
                        });
                    }
                }
                finally
                {
                    mutex.ReleaseMutex();
                }
            }
        }

        /*抄考代码
		[DistributedTransactionScope] //启用分布式事务
----------------------------------------------------------------------------------------------------------------------------
		HeartbeatEntity data = new HeartbeatEntity
		{
	Id = NextId(),LastHeartbeatTime = param.LastHeartbeatTime,RobotId = param.RobotId,
		};
----------------------------------------------------------------------------------------------------------------------------
		HeartbeatEntityParams param = new HeartbeatEntityParams
		{
	Id = param.Id,LastHeartbeatTime = param.LastHeartbeatTime,LastHeartbeatTimeStart = param.LastHeartbeatTimeStart,LastHeartbeatTimeEnd = param.LastHeartbeatTimeEnd,RobotId = param.RobotId,RobotIdLike = param.RobotIdLike,
		};
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 分页查询***模块，并返回查询结果
        /// </summary>
        /// <param name="param">查询参数</param>
        /// <param name="currentPageIndex">当前页码</param>
        /// <param name="pageSize">每页显示的数据量</param>
        /// <returns>返回***模块的查询结果</returns>
		public MyPagedList<HeartbeatEntity> Page(HeartbeatEntityParams param,int currentPageIndex = 1,int pageSize = 20)
		{
			return Repository.PageList(param, currentPageIndex, pageSize);
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 根据主键删除指定数据
        /// </summary>
        /// <param name="Id">删除数据的主键</param>
		public void Del(long Id)
		{
			Repository.Delete(a => a.Id);
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 新增一条数据
        /// </summary>
        /// <param name="data">新增的数据</param>
		public void Add(HeartbeatEntity data)
		{
			Repository.Insert(data);
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 修改***模块的数据
        /// </summary>
        /// <param name="data">修改数据，不为null才会被修改，主键必须不为null</param>
        public void Update(HeartbeatEntity data)
        {
            Repository.UpdateChange(new HeartbeatEntity
            {
                Id = NextId(),LastHeartbeatTime = data.LastHeartbeatTime,RobotId = data.RobotId,
            });
        }
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 新增多条数据
        /// </summary>
        /// <param name="datas">新增的数据</param>
		public void AddBatch(List<HeartbeatEntity> datas)
		{
			foreach(HeartbeatEntity data in datas)
			{
				data.Id = NextId();
				data.CreateDate = DateTime.Now;
				data.Status = 1;
			}
			Repository.Insert(datas);
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 批量修改状态
        /// </summary>
        /// <param name="datas">修改状态的数据</param>
		public int ChangeStatus(List<HeartbeatEntity> datas)
		{
			List<HeartbeatEntity> updates = new List<HeartbeatEntity>();
			for(HeartbeatEntity data in datas)
			{
				updates.Add(new HeartbeatEntity
				{
					Id = data.Id,
					Status = data.Status
				});
			}
			return Repository.UpdateChangeBatch(updates);
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 根据主键id查询***模块的数据实体
        /// </summary>
        /// <param name="Id">主键id</param>
		/// <returns>返回***模块的查询结果</returns>
		public HeartbeatEntity Load(long Id)
		{
			return Repository.FindEntity(Id);
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="datas">批量删除的数据</param>
		public int DelBatch(HeartbeatEntity datas)
		{
			return Repository.Delete(datas);
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 对“****”方法的入参进行校验基础（只校验是否为空，格式，长度，大小，不涉及
	/// 数据库查询、流读取等其他复杂操作），校验不通过时，异常往外抛出
        /// </summary>
        /// <param name="param">待校验参数</param>
        public void Check****(HeartbeatEntity data)
        {
			Regex regex = new Regex("\w");
							if(string.IsNullOrEmpty(data.Id))
							{
								Ex("主键id不能为空");
							}
							else
							{
								if(data.Id.Length<6 || data.Id.Length>20)
								{
									Ex("主键id长度须在6-20个字符之间");
								}
								if(!regex.IsMatch(data.Id))
								{
									Ex("主键id须是字母、数字、下划线组合");
								}
							}				if(string.IsNullOrEmpty(data.LastHeartbeatTime))
							{
								Ex("最近一次的心跳时间不能为空");
							}
							else
							{
								if(data.LastHeartbeatTime.Length<6 || data.LastHeartbeatTime.Length>20)
								{
									Ex("最近一次的心跳时间长度须在6-20个字符之间");
								}
								if(!regex.IsMatch(data.LastHeartbeatTime))
								{
									Ex("最近一次的心跳时间须是字母、数字、下划线组合");
								}
							}				if(string.IsNullOrEmpty(data.RobotId))
							{
								Ex("机器人id不能为空");
							}
							else
							{
								if(data.RobotId.Length<6 || data.RobotId.Length>20)
								{
									Ex("机器人id长度须在6-20个字符之间");
								}
								if(!regex.IsMatch(data.RobotId))
								{
									Ex("机器人id须是字母、数字、下划线组合");
								}
							}
							if(data.Id==null)
							{
								Ex("主键id不能为空");
							}
							else
							{
								if(data.Id<6 || data.Id>20)
								{
									Ex("主键id的值须在6-20之间");
								}

							}				if(data.LastHeartbeatTime==null)
							{
								Ex("最近一次的心跳时间不能为空");
							}
							else
							{
								if(data.LastHeartbeatTime<6 || data.LastHeartbeatTime>20)
								{
									Ex("最近一次的心跳时间的值须在6-20之间");
								}

							}				if(data.RobotId==null)
							{
								Ex("机器人id不能为空");
							}
							else
							{
								if(data.RobotId<6 || data.RobotId>20)
								{
									Ex("机器人id的值须在6-20之间");
								}

							}
        }
		*/
    }
}