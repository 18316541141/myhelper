using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication1.Entity;
using WebApplication1.MyExtensions;
using WebApplication1.Params;
using CommonHelper.AopInterceptor;
namespace WebApplication1.Service
{
	/// <summary>
    /// ***模块的业务类
    /// </summary>
	//[Intercept(typeof(DistributedTransactionScan))]	//启用分布式事务扫描
	public partial class GlobalVariableService : BaseService
    {
		public GlobalVariableRepository Repository { set; get; }

		/*抄考代码
		[DistributedTransactionScope] //启用分布式事务
----------------------------------------------------------------------------------------------------------------------------
		GlobalVariable data = new GlobalVariable
		{
	Id = NextId(),VarSortIndex = param.VarSortIndex,VarName = param.VarName,VarValue = param.VarValue,
		};
----------------------------------------------------------------------------------------------------------------------------
		GlobalVariableParams param = new GlobalVariableParams
		{
	Id = param.Id,VarSortIndex = param.VarSortIndex,VarSortIndexStart = param.VarSortIndexStart,VarSortIndexEnd = param.VarSortIndexEnd,VarSortIndexChange = param.VarSortIndexChange,VarName = param.VarName,VarNameLike = param.VarNameLike,VarValue = param.VarValue,VarValueLike = param.VarValueLike,
		};
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 分页查询***模块，并返回查询结果
        /// </summary>
        /// <param name="param">查询参数</param>
        /// <param name="currentPageIndex">当前页码</param>
        /// <param name="pageSize">每页显示的数据量</param>
        /// <returns>返回***模块的查询结果</returns>
		public MyPagedList<GlobalVariable> Page(GlobalVariableParams param,int currentPageIndex = 1,int pageSize = 20)
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
		public void Add(GlobalVariable data)
		{
			Repository.Insert(data);
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 修改***模块的数据
        /// </summary>
        /// <param name="data">修改数据，不为null才会被修改，主键必须不为null</param>
        public void Update(GlobalVariable data)
        {
            Repository.UpdateChange(new GlobalVariable
            {
                Id = NextId(),VarSortIndex = data.VarSortIndex,VarName = data.VarName,VarValue = data.VarValue,
            });
        }
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 新增多条数据
        /// </summary>
        /// <param name="datas">新增的数据</param>
		public void AddBatch(List<GlobalVariable> datas)
		{
			foreach(GlobalVariable data in datas)
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
		public int ChangeStatus(List<GlobalVariable> datas)
		{
			List<GlobalVariable> updates = new List<GlobalVariable>();
			for(GlobalVariable data in datas)
			{
				updates.Add(new GlobalVariable
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
		public GlobalVariable Load(long Id)
		{
			return Repository.FindEntity(Id);
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="datas">批量删除的数据</param>
		public int DelBatch(GlobalVariable datas)
		{
			return Repository.Delete(datas);
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 对“****”方法的入参进行校验基础（只校验是否为空，格式，长度，大小，不涉及
	/// 数据库查询、流读取等其他复杂操作），校验不通过时，异常往外抛出
        /// </summary>
        /// <param name="param">待校验参数</param>
        public void Check****(GlobalVariable data)
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
							}				if(string.IsNullOrEmpty(data.VarSortIndex))
							{
								Ex("变量排序号不能为空");
							}
							else
							{
								if(data.VarSortIndex.Length<6 || data.VarSortIndex.Length>20)
								{
									Ex("变量排序号长度须在6-20个字符之间");
								}
								if(!regex.IsMatch(data.VarSortIndex))
								{
									Ex("变量排序号须是字母、数字、下划线组合");
								}
							}				if(string.IsNullOrEmpty(data.VarName))
							{
								Ex("变量名称不能为空");
							}
							else
							{
								if(data.VarName.Length<6 || data.VarName.Length>20)
								{
									Ex("变量名称长度须在6-20个字符之间");
								}
								if(!regex.IsMatch(data.VarName))
								{
									Ex("变量名称须是字母、数字、下划线组合");
								}
							}				if(string.IsNullOrEmpty(data.VarValue))
							{
								Ex("变量值不能为空");
							}
							else
							{
								if(data.VarValue.Length<6 || data.VarValue.Length>20)
								{
									Ex("变量值长度须在6-20个字符之间");
								}
								if(!regex.IsMatch(data.VarValue))
								{
									Ex("变量值须是字母、数字、下划线组合");
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

							}				if(data.VarSortIndex==null)
							{
								Ex("变量排序号不能为空");
							}
							else
							{
								if(data.VarSortIndex<6 || data.VarSortIndex>20)
								{
									Ex("变量排序号的值须在6-20之间");
								}

							}				if(data.VarName==null)
							{
								Ex("变量名称不能为空");
							}
							else
							{
								if(data.VarName<6 || data.VarName>20)
								{
									Ex("变量名称的值须在6-20之间");
								}

							}				if(data.VarValue==null)
							{
								Ex("变量值不能为空");
							}
							else
							{
								if(data.VarValue<6 || data.VarValue>20)
								{
									Ex("变量值的值须在6-20之间");
								}

							}
        }
		*/
    }
}