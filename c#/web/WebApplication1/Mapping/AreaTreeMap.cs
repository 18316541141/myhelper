﻿using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using WebApplication1.Entity;

namespace WebApplication1.Mapping
{
    /// <summary>
    /// 省市区镇地区树的映射
    /// </summary>
    public class AreaTreeMap : EntityTypeConfiguration<AreaTree>
    {
        public AreaTreeMap()
        {

        }
    }
}