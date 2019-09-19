
using WebApplication1.Entity.Common;
using CommonHelper.CommonEntity;
using CommonWeb.Service.Common;
using CommonWeb.Repository;

namespace CommonWeb.Service
{
	/// <summary>
    /// “日志管理”模块的业务类
    /// </summary>
	//[Intercept(typeof(DistributedTransactionScan))]	//启用分布式事务扫描
	public partial class LogEntityService : BaseService
    {
		public LogEntityRepository Repository { set; get; }

        /// <summary>
        /// 分页查询“日志管理”模块，并返回查询结果
        /// </summary>
        /// <param name="param">查询参数</param>
        /// <param name="currentPageIndex">当前页码</param>
        /// <param name="pageSize">每页显示的数据量</param>
        /// <returns>返回“日志管理”模块的查询结果</returns>
        public MyPagedList<LogEntity> Page(LogEntityParams param, int currentPageIndex = 1, int pageSize = 20)
        {
            return Repository.PageList(param, currentPageIndex, pageSize);
        }

        /// <summary>
        /// 根据主键id查询“日志管理”模块的数据实体
        /// </summary>
        /// <param name="ID">主键id</param>
		/// <returns>返回“日志管理”模块的查询结果</returns>
		public LogEntity Load(long ID)
        {
            return Repository.FindEntity(ID);
        }

        /*抄考代码
		[DistributedTransactionScope] //启用分布式事务
----------------------------------------------------------------------------------------------------------------------------
		LogEntity data = new LogEntity
		{
	ID = NextId(),CreateDate = param.CreateDate,Level = param.Level,ThreadNo = param.ThreadNo,Message = param.Message,Namespace = param.Namespace,TypeName = param.TypeName,MethodName = param.MethodName,USERNAME = param.USERNAME,
		};
----------------------------------------------------------------------------------------------------------------------------
		LogEntityParams param = new LogEntityParams
		{
	ID = param.ID,CreateDate = param.CreateDate,CreateDateStart = param.CreateDateStart,CreateDateEnd = param.CreateDateEnd,Level = param.Level,LevelLike = param.LevelLike,ThreadNo = param.ThreadNo,ThreadNoLike = param.ThreadNoLike,Message = param.Message,MessageLike = param.MessageLike,Namespace = param.Namespace,NamespaceLike = param.NamespaceLike,TypeName = param.TypeName,TypeNameLike = param.TypeNameLike,MethodName = param.MethodName,MethodNameLike = param.MethodNameLike,USERNAME = param.USERNAME,USERNAMELike = param.USERNAMELike,
		};
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 分页查询***模块，并返回查询结果
        /// </summary>
        /// <param name="param">查询参数</param>
        /// <param name="currentPageIndex">当前页码</param>
        /// <param name="pageSize">每页显示的数据量</param>
        /// <returns>返回***模块的查询结果</returns>
		public MyPagedList<LogEntity> Page(LogEntityParams param,int currentPageIndex = 1,int pageSize = 20)
		{
			return Repository.PageList(param, currentPageIndex, pageSize);
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 根据主键删除指定数据
        /// </summary>
        /// <param name="ID">删除数据的主键</param>
		public void Del(long ID)
		{
			Repository.Delete(a => a.ID);
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 新增一条数据
        /// </summary>
        /// <param name="data">新增的数据</param>
		public void Add(LogEntity data)
		{
			Repository.Insert(data);
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 修改***模块的数据
        /// </summary>
        /// <param name="data">修改数据，不为null才会被修改，主键必须不为null</param>
        public void Update(LogEntity data)
        {
            Repository.UpdateChange(new LogEntity
            {
                ID = NextId(),CreateDate = data.CreateDate,Level = data.Level,ThreadNo = data.ThreadNo,Message = data.Message,Namespace = data.Namespace,TypeName = data.TypeName,MethodName = data.MethodName,USERNAME = data.USERNAME,
            });
        }
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 新增多条数据
        /// </summary>
        /// <param name="datas">新增的数据</param>
		public void AddBatch(List<LogEntity> datas)
		{
			foreach(LogEntity data in datas)
			{
				data.ID = NextId();
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
		public int ChangeStatus(List<LogEntity> datas)
		{
			List<LogEntity> updates = new List<LogEntity>();
			for(LogEntity data in datas)
			{
				updates.Add(new LogEntity
				{
					ID = data.ID,
					Status = data.Status
				});
			}
			return Repository.UpdateChangeBatch(updates);
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 根据主键id查询***模块的数据实体
        /// </summary>
        /// <param name="ID">主键id</param>
		/// <returns>返回***模块的查询结果</returns>
		public LogEntity Load(long ID)
		{
			return Repository.FindEntity(ID);
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="datas">批量删除的数据</param>
		public int DelBatch(LogEntity datas)
		{
			return Repository.Delete(datas);
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 对“****”方法的入参进行校验基础（只校验是否为空，格式，长度，大小，不涉及
	/// 数据库查询、流读取等其他复杂操作），校验不通过时，异常往外抛出
        /// </summary>
        /// <param name="param">待校验参数</param>
        public void Check****(LogEntity data)
        {
			Regex regex = new Regex("\w");
							if(string.IsNullOrEmpty(data.ID))
							{
								Ex("主键id，由分布式雪花id生成不能为空");
							}
							else
							{
								if(data.ID.Length<6 || data.ID.Length>20)
								{
									Ex("主键id，由分布式雪花id生成长度须在6-20个字符之间");
								}
								if(!regex.IsMatch(data.ID))
								{
									Ex("主键id，由分布式雪花id生成须是字母、数字、下划线组合");
								}
							}				if(string.IsNullOrEmpty(data.CreateDate))
							{
								Ex("日志日期不能为空");
							}
							else
							{
								if(data.CreateDate.Length<6 || data.CreateDate.Length>20)
								{
									Ex("日志日期长度须在6-20个字符之间");
								}
								if(!regex.IsMatch(data.CreateDate))
								{
									Ex("日志日期须是字母、数字、下划线组合");
								}
							}				if(string.IsNullOrEmpty(data.Level))
							{
								Ex("日志分级不能为空");
							}
							else
							{
								if(data.Level.Length<6 || data.Level.Length>20)
								{
									Ex("日志分级长度须在6-20个字符之间");
								}
								if(!regex.IsMatch(data.Level))
								{
									Ex("日志分级须是字母、数字、下划线组合");
								}
							}				if(string.IsNullOrEmpty(data.ThreadNo))
							{
								Ex("线程号不能为空");
							}
							else
							{
								if(data.ThreadNo.Length<6 || data.ThreadNo.Length>20)
								{
									Ex("线程号长度须在6-20个字符之间");
								}
								if(!regex.IsMatch(data.ThreadNo))
								{
									Ex("线程号须是字母、数字、下划线组合");
								}
							}				if(string.IsNullOrEmpty(data.Message))
							{
								Ex("日志内容不能为空");
							}
							else
							{
								if(data.Message.Length<6 || data.Message.Length>20)
								{
									Ex("日志内容长度须在6-20个字符之间");
								}
								if(!regex.IsMatch(data.Message))
								{
									Ex("日志内容须是字母、数字、下划线组合");
								}
							}				if(string.IsNullOrEmpty(data.Namespace))
							{
								Ex("日志发生的命名空间不能为空");
							}
							else
							{
								if(data.Namespace.Length<6 || data.Namespace.Length>20)
								{
									Ex("日志发生的命名空间长度须在6-20个字符之间");
								}
								if(!regex.IsMatch(data.Namespace))
								{
									Ex("日志发生的命名空间须是字母、数字、下划线组合");
								}
							}				if(string.IsNullOrEmpty(data.TypeName))
							{
								Ex("日志发生的类型不能为空");
							}
							else
							{
								if(data.TypeName.Length<6 || data.TypeName.Length>20)
								{
									Ex("日志发生的类型长度须在6-20个字符之间");
								}
								if(!regex.IsMatch(data.TypeName))
								{
									Ex("日志发生的类型须是字母、数字、下划线组合");
								}
							}				if(string.IsNullOrEmpty(data.MethodName))
							{
								Ex("日志发生的方法名称不能为空");
							}
							else
							{
								if(data.MethodName.Length<6 || data.MethodName.Length>20)
								{
									Ex("日志发生的方法名称长度须在6-20个字符之间");
								}
								if(!regex.IsMatch(data.MethodName))
								{
									Ex("日志发生的方法名称须是字母、数字、下划线组合");
								}
							}				if(string.IsNullOrEmpty(data.USERNAME))
							{
								Ex("导致该日志的用户不能为空");
							}
							else
							{
								if(data.USERNAME.Length<6 || data.USERNAME.Length>20)
								{
									Ex("导致该日志的用户长度须在6-20个字符之间");
								}
								if(!regex.IsMatch(data.USERNAME))
								{
									Ex("导致该日志的用户须是字母、数字、下划线组合");
								}
							}
							if(data.ID==null)
							{
								Ex("主键id，由分布式雪花id生成不能为空");
							}
							else
							{
								if(data.ID<6 || data.ID>20)
								{
									Ex("主键id，由分布式雪花id生成的值须在6-20之间");
								}

							}				if(data.CreateDate==null)
							{
								Ex("日志日期不能为空");
							}
							else
							{
								if(data.CreateDate<6 || data.CreateDate>20)
								{
									Ex("日志日期的值须在6-20之间");
								}

							}				if(data.Level==null)
							{
								Ex("日志分级不能为空");
							}
							else
							{
								if(data.Level<6 || data.Level>20)
								{
									Ex("日志分级的值须在6-20之间");
								}

							}				if(data.ThreadNo==null)
							{
								Ex("线程号不能为空");
							}
							else
							{
								if(data.ThreadNo<6 || data.ThreadNo>20)
								{
									Ex("线程号的值须在6-20之间");
								}

							}				if(data.Message==null)
							{
								Ex("日志内容不能为空");
							}
							else
							{
								if(data.Message<6 || data.Message>20)
								{
									Ex("日志内容的值须在6-20之间");
								}

							}				if(data.Namespace==null)
							{
								Ex("日志发生的命名空间不能为空");
							}
							else
							{
								if(data.Namespace<6 || data.Namespace>20)
								{
									Ex("日志发生的命名空间的值须在6-20之间");
								}

							}				if(data.TypeName==null)
							{
								Ex("日志发生的类型不能为空");
							}
							else
							{
								if(data.TypeName<6 || data.TypeName>20)
								{
									Ex("日志发生的类型的值须在6-20之间");
								}

							}				if(data.MethodName==null)
							{
								Ex("日志发生的方法名称不能为空");
							}
							else
							{
								if(data.MethodName<6 || data.MethodName>20)
								{
									Ex("日志发生的方法名称的值须在6-20之间");
								}

							}				if(data.USERNAME==null)
							{
								Ex("导致该日志的用户不能为空");
							}
							else
							{
								if(data.USERNAME<6 || data.USERNAME>20)
								{
									Ex("导致该日志的用户的值须在6-20之间");
								}

							}
        }
		*/
    }
}