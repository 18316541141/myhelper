using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using WebApplication1.Entity;

namespace WebApplication1.Mapping
{
    public sealed class ProvinceMap : EntityTypeConfiguration<Province>
    {
        public ProvinceMap()
        {
            this.ToTable("S_Province");
            this.HasKey(u => u.provinceID);
            this.Property(u => u.provinceID).HasColumnName("ProvinceID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            this.Property(u => u.provinceName).HasColumnName("ProvinceName");
        }
    }
}