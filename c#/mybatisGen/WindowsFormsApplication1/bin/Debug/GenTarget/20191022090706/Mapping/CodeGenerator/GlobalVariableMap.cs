using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using WebApplication1.Entity;

namespace WebApplication1.Mapping
{
    public sealed partial class GlobalVariableMap: EntityTypeConfiguration<GlobalVariable>
    {
        public GlobalVariableMap()
        {
            ToTable("global_variable");
            HasKey(u => u.Id);
            Property(u => u.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

					Property(u => u.VarSortIndex).HasColumnName("VAR_SORT_INDEX");
					Property(u => u.VarName).HasColumnName("VAR_NAME");
					Property(u => u.VarValue).HasColumnName("VAR_VALUE");
        }
    }
}