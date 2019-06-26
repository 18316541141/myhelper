using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using WebApplication1.Entity;

namespace WebApplication1.Mapping
{
    public class ProvinceMap : EntityTypeConfiguration<Province>
    {
        public ProvinceMap()
        {
            this.ToTable("S_Province");
            this.HasKey(u => u.ProvinceID);
            this.Property(u => u.ProvinceID).HasColumnName("ProvinceID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            this.Property(u => u.ProvinceName).HasColumnName("ProvinceName");
        }
    }
}