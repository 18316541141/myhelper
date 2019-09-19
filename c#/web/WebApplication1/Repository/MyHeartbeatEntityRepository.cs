using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonHelper.Helper.EFDbContext;
using CommonWeb.Repository;

namespace WebApplication1.Repository
{
    /// <summary>
    /// 自定义心跳监测的数据操作类。
    /// </summary>
    public class MyHeartbeatEntityRepository : HeartbeatEntityRepository
    {
        public override List<BaseDbContext> CreateAllDbContext()
        {
            List<BaseDbContext> baseDbContextList = new List<BaseDbContext>();
            baseDbContextList.Add(new MyDbContext());
            return baseDbContextList;
        }

        public override BaseDbContext CreateDbContext()
        {
            return new MyDbContext();
        }
    }
}