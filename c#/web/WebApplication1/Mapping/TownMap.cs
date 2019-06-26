using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using WebApplication1.Entity;

namespace WebApplication1.Mapping
{
    public class TownMap : EntityTypeConfiguration<Town>
    {
        public TownMap()
        {
            this.ToTable("S_Town");
            this.HasKey(u => u.TownID);
            this.Property(u => u.TownID).HasColumnName("TownID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            this.Property(u => u.TownName).HasColumnName("TownName");
            this.Property(u => u.DistrictID).HasColumnName("DistrictID");
        }
    }
}