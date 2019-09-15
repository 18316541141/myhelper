using System;
using CommonHelper.CommonEntity;
using CommonHelper.EFRepository;
using CommonHelper.Helper;
using CommonHelper.Helper.EFDbContext;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication1.Entity;
using WebApplication1.Params;
namespace WebApplication1.Repository
{
    public partial class HeartbeatEntityMemoryRepository : BaseMemoryRepository<HeartbeatEntity, HeartbeatEntityParams, HeartbeatEntitySetNullParams>
    {
        public override List<IEquatable<HeartbeatEntity>> Sort(HeartbeatEntityParams eqArgs, List<HeartbeatEntity> list)
        {
            IOrderedEnumerable<HeartbeatEntity> orderEnum = null;
            HeartbeatEntityOrderBy orderBy = eqArgs.orderByAsc;
            if (orderBy != null)
            {

                if (orderBy.Id) { orderEnum = list.OrderBy(a => a.Id); }
                else
                if (orderBy.Username) { orderEnum = list.OrderBy(a => a.Username); }
                else
                if (orderBy.LastHeartbeatTime) { orderEnum = list.OrderBy(a => a.LastHeartbeatTime); }
            }
            if (orderEnum == null)
            {
                orderBy = eqArgs.orderByDesc;
                if (orderBy != null)
                {

                    if (orderBy.Id) { orderEnum = list.OrderByDescending(a => a.Id); }
                    else
                    if (orderBy.Username) { orderEnum = list.OrderByDescending(a => a.Username); }
                    else
                    if (orderBy.LastHeartbeatTime) { orderEnum = list.OrderByDescending(a => a.LastHeartbeatTime); }
                    else
                    {
                        orderEnum = list.OrderByDescending(a => a.Id);
                    }
                }
                else
                {
                    orderEnum = list.OrderByDescending(a => a.Id);
                }
            }
            List<IEquatable<HeartbeatEntity>> ret = new List<IEquatable<HeartbeatEntity>>();
            foreach (HeartbeatEntity entity in orderEnum.ToList())
            {
                ret.Add(entity);
            }
            return ret;
        }

        protected override bool Query(HeartbeatEntity entity, HeartbeatEntityParams paramz, HeartbeatEntityParams neqArgs)
        {
            if (paramz != null)
            {

                if (paramz.Id != null && entity.Id != paramz.Id)
                {
                    return false;
                }

                if (paramz.Username != null && entity.Username != paramz.Username)
                {
                    return false;
                }

                if (!string.IsNullOrEmpty(paramz.UsernameLike) && entity.Username.IndexOf(paramz.UsernameLike) == -1)
                {
                    return false;
                }
                if (paramz.LastHeartbeatTime != null && entity.LastHeartbeatTime != paramz.LastHeartbeatTime)
                {
                    return false;
                }

            }
            return true;
        }

        protected override void SetNullOper(HeartbeatEntity entity, HeartbeatEntitySetNullParams param)
        {
            if (param.Username)
            {
                entity.Username = null;
            }
            if (param.LastHeartbeatTime)
            {
                entity.LastHeartbeatTime = null;
            }
        }

        protected override void UpdateChange(HeartbeatEntity entity, HeartbeatEntity change)
        {
            if (change.Username != null)
            {
                entity.Username = change.Username;
            }
            if (change.LastHeartbeatTime != null)
            {
                entity.LastHeartbeatTime = change.LastHeartbeatTime;
            }
        }
    }
}