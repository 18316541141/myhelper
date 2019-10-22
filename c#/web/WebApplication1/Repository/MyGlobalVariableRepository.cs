using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonHelper.Helper.EFDbContext;

namespace WebApplication1.Repository
{
    /// <summary>
    /// 自定义数据源的全局变量数据操作类
    /// </summary>
    public class MyGlobalVariableRepository : GlobalVariableRepository
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