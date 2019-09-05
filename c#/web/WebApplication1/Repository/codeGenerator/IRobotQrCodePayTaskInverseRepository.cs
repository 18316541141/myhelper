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
    public partial class IRobotQrCodePayTaskInverseRepository : InverseRepository<IRobotQrCodePayTask>
    {

        public override BaseDbContext CreateDbContext()
        {
            return new MyDbContext2();
        }

        public override void DistributedDeleteInverse(List<DistributedTransactionPart> distributedTransactionParts)
        {
            throw new NotImplementedException();
        }

        public override void DistributedDeleteInverse(DistributedTransactionPart distributedTransactionPart)
        {
            throw new NotImplementedException();
        }
    }
}