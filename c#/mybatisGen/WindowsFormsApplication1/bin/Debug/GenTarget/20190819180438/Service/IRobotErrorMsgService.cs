﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication1.Entity;
using WebApplication1.MyExtensions;
using WebApplication1.Params;
namespace WebApplication1.Service
{
	/// <summary>
        /// ***模块的业务类
        /// </summary>
    public partial class IRobotErrorMsgService : BaseService
    {
		public IRobotErrorMsgRepository Repository { set; get; }

		/*抄考代码
----------------------------------------------------------------------------------------------------------------------------
		IRobotErrorMsg data = new IRobotErrorMsg
		{
	IEErrNo = NextId(),IECreateDate = param.IECreateDate,IEErrOrderNo = param.IEErrOrderNo,IEErrRobotId = param.IEErrRobotId,IEErrPic = param.IEErrPic,IEErrContext = param.IEErrContext,IEHandleStatus = param.IEHandleStatus,
		};
----------------------------------------------------------------------------------------------------------------------------
		IRobotErrorMsgParams param = new IRobotErrorMsgParams
		{
	IEErrNo = param.IEErrNo,IEErrNoLike = param.IEErrNoLike,IECreateDate = param.IECreateDate,IECreateDateStart = param.IECreateDateStart,IECreateDateEnd = param.IECreateDateEnd,IEErrOrderNo = param.IEErrOrderNo,IEErrOrderNoLike = param.IEErrOrderNoLike,IEErrRobotId = param.IEErrRobotId,IEErrRobotIdLike = param.IEErrRobotIdLike,IEErrPic = param.IEErrPic,IEErrPicLike = param.IEErrPicLike,IEErrContext = param.IEErrContext,IEErrContextLike = param.IEErrContextLike,IEHandleStatus = param.IEHandleStatus,IEHandleStatusStart = param.IEHandleStatusStart,IEHandleStatusEnd = param.IEHandleStatusEnd,
		};
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 分页查询***模块，并返回查询结果
        /// </summary>
        /// <param name="param">查询参数</param>
        /// <param name="currentPageIndex">当前页码</param>
        /// <param name="pageSize">每页显示的数据量</param>
        /// <returns>返回***模块的查询结果</returns>
		public MyPagedList<IRobotErrorMsg> Page(IRobotErrorMsgParams param,int currentPageIndex = 1,int pageSize = 20)
		{
			return Repository.PageList(param, currentPageIndex, pageSize);
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 根据主键删除指定数据
        /// </summary>
        /// <param name="IEErrNo">删除数据的主键</param>
		public void Del(long IEErrNo)
		{
			Repository.Delete(a => a.IEErrNo);
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 新增一条数据
        /// </summary>
        /// <param name="data">新增的数据</param>
		public void Add(IRobotErrorMsg data)
		{
			Repository.Insert(data);
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 新增多条数据
        /// </summary>
        /// <param name="datas">新增的数据</param>
		public void AddBatch(List<IRobotErrorMsg> datas)
		{
			foreach(IRobotErrorMsg data in datas)
			{
				data.IEErrNo = NextId();
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
		public int ChangeStatus(IRobotErrorMsg datas)
		{
			List<IRobotErrorMsg> updates = new List<IRobotErrorMsg>();
			for(IRobotErrorMsg data in datas)
			{
				updates.Add(new IRobotErrorMsg
				{
					IEErrNo = data.IEErrNo,
					Status = data.Status
				});
			}
			return Repository.UpdateChangeBatch(updates);
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 根据主键id查询***模块的数据实体
        /// </summary>
        /// <param name="IEErrNo">主键id</param>
		/// <returns>返回***模块的查询结果</returns>
		public IRobotErrorMsg Load(long IEErrNo)
		{
			return Repository.FindEntity(IEErrNo);
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="datas">批量删除的数据</param>
		public int DelBatch(IRobotErrorMsg datas)
		{
			return Repository.Delete(datas);
		}
----------------------------------------------------------------------------------------------------------------------------
		/// <summary>
        /// 对“****”方法的入参进行校验基础（只校验是否为空，格式，长度，大小，不涉及
	/// 数据库查询、流读取等其他复杂操作），校验不通过时，异常往外抛出
        /// </summary>
        /// <param name="param">待校验参数</param>
        public void Check****(IRobotErrorMsgParams param)
        {
            if(...){
		Ex(...,...);
	    }
        }
		*/
    }
}