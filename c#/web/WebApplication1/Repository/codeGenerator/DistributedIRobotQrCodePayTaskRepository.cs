using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication1.Entity;

namespace WebApplication1.Repository.codeGenerator
{
    /// <summary>
    /// 分布式的
    /// </summary>
    public class DistributedIRobotQrCodePayTaskRepository
    {
        /// <summary>
        /// 插入一条数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>返回插入的数据数量</returns>
        public int Insert(IRobotQrCodePayTask irobotQrCodePayTask)
        {
            using (DbContext dbContext = new MyDbContext2())
            {
                dbContext.Entry(irobotQrCodePayTask).State = EntityState.Added;

                return dbContext.SaveChanges();
            }
        }
    }
}