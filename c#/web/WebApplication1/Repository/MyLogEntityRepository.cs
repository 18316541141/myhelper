using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonHelper.CommonEntity;
using CommonHelper.Helper.EFDbContext;
using CommonWeb.Repository;

namespace WebApplication1.Repository
{
    /// <summary>
    /// 自定义的日志操作类
    /// </summary>
    public class MyLogEntityRepository : LogEntityRepository
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

        protected override void UpdateChangeBatch(BaseDbContext dbContext, List<LogEntity> idList, LogEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}