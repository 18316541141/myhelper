using CommonHelper.CommonEntity;
using CommonHelper.EFRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Repository.codeGenerator
{
    public class HeartbeatEntityMemoryRepository : BaseMemoryRepository<HeartbeatEntity, HeartbeatEntityParams, HeartbeatEntitySetNullParams>
    {
        public override List<IEquatable<HeartbeatEntity>> Sort(HeartbeatEntityParams eqArgs, List<HeartbeatEntity> list)
        {
            IOrderedEnumerable<HeartbeatEntity> orderEnum =list.OrderBy(a=>a.Id);
            List<IEquatable<HeartbeatEntity>> ret = new List<IEquatable<HeartbeatEntity>>();
            foreach (HeartbeatEntity heartbeatEntity in orderEnum.ToList())
            {
                ret.Add(heartbeatEntity);
            }
            return ret;
        }

        protected override bool Query(HeartbeatEntity entity, HeartbeatEntityParams paramz, HeartbeatEntityParams neqArgs)
        {
            throw new NotImplementedException();
        }

        protected override void SetNullOper(HeartbeatEntity entity, HeartbeatEntitySetNullParams param)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateChange(HeartbeatEntity src, HeartbeatEntity change)
        {
            throw new NotImplementedException();
        }
    }
}