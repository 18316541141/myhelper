using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using WebApplication1.Entity;

namespace WebApplication1.Mapping
{
    public sealed class CityMap : EntityTypeConfiguration<City>
    {
        public CityMap()
        {
            this.ToTable("S_City");
            this.HasKey(u => u.CityID);
            this.Property(u => u.CityID).HasColumnName("CityID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            this.Property(u => u.CityName).HasColumnName("CityName");
            this.Property(u => u.ProvinceID).HasColumnName("ProvinceID");
        }
    }
}