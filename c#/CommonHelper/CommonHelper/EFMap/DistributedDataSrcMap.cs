using CommonHelper.CommonEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper.EFMap
{
    /// <summary>
    /// 数据源标识映射表
    /// </summary>
    public class DistributedDataSrcMap:EntityTypeConfiguration<DistributedDataSrc>
    {
        public DistributedDataSrcMap()
        {
            ToTable("Distributed_Data_Src");
            HasKey(u => u.DataSrc);
            Property(u => u.DataSrc).HasColumnName("Data_Src").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None); ;
        }

    }
}
