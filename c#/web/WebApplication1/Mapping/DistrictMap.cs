using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using WebApplication1.Entity;

namespace WebApplication1.Mapping
{
    /// <summary>
    /// 
    /// </summary>
    public class DistrictMap : EntityTypeConfiguration<District>
    {
        public DistrictMap()
        {
            this.ToTable("S_District");
            this.HasKey(u => u.DistrictID);
            this.Property(u => u.DistrictID).HasColumnName("DistrictID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            this.Property(u => u.DistrictName).HasColumnName("DistrictName");
            this.Property(u => u.CityID).HasColumnName("CityID");
        }
    }
}