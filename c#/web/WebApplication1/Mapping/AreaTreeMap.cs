using CommonHelper.Helper.CommonEntity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using WebApplication1.Entity;
using WebApplication1.Entity.Common;

namespace WebApplication1.Mapping
{
    /// <summary>
    /// 省市区镇地区树的映射
    /// </summary>
    public sealed class AreaTreeMap : EntityTypeConfiguration<AreaTree>
    {
        public AreaTreeMap()
        {

        }
    }
}