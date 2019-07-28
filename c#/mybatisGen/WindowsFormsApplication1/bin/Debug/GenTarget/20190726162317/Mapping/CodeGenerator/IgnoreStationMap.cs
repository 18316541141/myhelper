using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using WebApplication1.Entity;

namespace WebApplication1.Mapping
{
    public partial class IgnoreStationMap: EntityTypeConfiguration<IgnoreStation>
    {
        public IgnoreStationMap()
        {
            this.ToTable("t_ignoreStation");
            this.HasKey(u => u.Id);
            this.Property(u => u.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

					this.Property(u => u.Id).HasColumnName("Id");
					this.Property(u => u.Code).HasColumnName("Code");
					this.Property(u => u.Name).HasColumnName("Name");
					this.Property(u => u.NoDataCollectDate).HasColumnName("NoDataCollectDate");
        }
    }
}