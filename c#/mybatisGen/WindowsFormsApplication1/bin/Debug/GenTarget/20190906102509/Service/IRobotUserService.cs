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
	public partial class IRobotUserService : BaseService
    {
		public IRobotUserRepository Repository { set; get; }

		/*抄考代码
		[DistributedTransactionScope] //启用分布式事务
----------------------------------------------------------------------------------------------------------------------------
		IRobotUser data = new IRobotUser
		{
	IUId = NextId(),IUUsername = param.IUUsername,IUPassword = param.IUPassword,IUSignKey = param.IUSignKey,IUSignSecret = param.IUSignSecret,IUCreateDate = param.IUCreateDate,
		};
----------------------------------------------------------------------------------------------------------------------------
		IRobotUserParams param = new IRobotUserParams
		{
	IUId = param.IUId,IUUsername = param.IUUsername,IUUsernameLike = param.IUUsernameLike,IUPassword = param.IUPassword,IUPasswordLike = param.IUPasswordLike,IUSignKey = param.IUSignKey,IUSignKeyLike = param.IUSignKeyLike,IUSignSecret = param.IUSignSecret,IUSignSecretLike = param.IUSignSecretLike,IUCreateDate = param.IUCreateDate,IUCreateDateStart = param.IUCreateDateStart,IUCreateDateEnd = param.IUCreateDateEnd,
		};
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 分页查询***模块，并返回查询结果
        /// </summary>
        /// <param name="param">查询参数</param>
        /// <param name="currentPageIndex">当前页码</param>
        /// <param name="pageSize">每页显示的数据量</param>
        /// <returns>返回***模块的查询结果</returns>
		public MyPagedList<IRobotUser> Page(IRobotUserParams param,int currentPageIndex = 1,int pageSize = 20)
		{
			return Repository.PageList(param, currentPageIndex, pageSize);
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 根据主键删除指定数据
        /// </summary>
        /// <param name="IUId">删除数据的主键</param>
		public void Del(long IUId)
		{
			Repository.Delete(a => a.IUId);
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 新增一条数据
        /// </summary>
        /// <param name="data">新增的数据</param>
		public void Add(IRobotUser data)
		{
			Repository.Insert(data);
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 修改***模块的数据
        /// </summary>
        /// <param name="data">修改数据，不为null才会被修改，主键必须不为null</param>
        public void Update(IRobotUser data)
        {
            Repository.UpdateChange(new IRobotUser
            {
                IUId = NextId(),IUUsername = data.IUUsername,IUPassword = data.IUPassword,IUSignKey = data.IUSignKey,IUSignSecret = data.IUSignSecret,IUCreateDate = data.IUCreateDate,
            });
        }
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 新增多条数据
        /// </summary>
        /// <param name="datas">新增的数据</param>
		public void AddBatch(List<IRobotUser> datas)
		{
			foreach(IRobotUser data in datas)
			{
				data.IUId = NextId();
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
		public int ChangeStatus(List<IRobotUser> datas)
		{
			List<IRobotUser> updates = new List<IRobotUser>();
			for(IRobotUser data in datas)
			{
				updates.Add(new IRobotUser
				{
					IUId = data.IUId,
					Status = data.Status
				});
			}
			return Repository.UpdateChangeBatch(updates);
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 根据主键id查询***模块的数据实体
        /// </summary>
        /// <param name="IUId">主键id</param>
		/// <returns>返回***模块的查询结果</returns>
		public IRobotUser Load(long IUId)
		{
			return Repository.FindEntity(IUId);
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="datas">批量删除的数据</param>
		public int DelBatch(IRobotUser datas)
		{
			return Repository.Delete(datas);
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 对“****”方法的入参进行校验基础（只校验是否为空，格式，长度，大小，不涉及
	/// 数据库查询、流读取等其他复杂操作），校验不通过时，异常往外抛出
        /// </summary>
        /// <param name="param">待校验参数</param>
        public void Check****(IRobotUserParams param)
        {
            if(...){
		Ex(...,...);
	    }
        }
		*/
    }
}