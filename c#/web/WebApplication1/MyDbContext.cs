using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication1.Entity;
using WebApplication1.Mapping;

namespace WebApplication1
{
    /// <summary>
    /// 数据库访问器
    /// </summary>
    public class MyDbContext: DbContext
    {
        public MyDbContext() : base(new OracleConnection("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=183.2.233.12)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=ORCL)));User ID=ELBTEST;PassWord=ELBTEST;"), true){ }

        /// <summary>
        /// 省表
        /// </summary>
        public DbSet<Province> Provinces { get; set; }

        /// <summary>
        /// 市表
        /// </summary>
        public DbSet<City> Cities { get; set; }

        /// <summary>
        /// 区表
        /// </summary>
        public DbSet<District> Districts { get; set; }

        /// <summary>
        /// 镇表
        /// </summary>
        public DbSet<Town> Towns { get; set; }

        /// <summary>
        /// 左侧菜单树数据
        /// </summary>
        //public DbSet<LeftMenu> LeftMenus { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Configurations.Add(new LeftMenuMap());
            modelBuilder.Configurations.Add(new ProvinceMap());
            modelBuilder.Configurations.Add(new CityMap());
            modelBuilder.Configurations.Add(new DistrictMap());
            modelBuilder.Configurations.Add(new TownMap());

            modelBuilder.HasDefaultSchema("ELBTEST");
            base.OnModelCreating(modelBuilder);
        }
    }
}