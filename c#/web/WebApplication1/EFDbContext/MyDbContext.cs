using CommonHelper.Helper.EFDbContext;
using CommonHelper.staticVar;
using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using WebApplication1.App_Start;
using WebApplication1.Entity;
using WebApplication1.Mapping;

namespace WebApplication1
{
    /// <summary>
    /// 数据库访问器
    /// </summary>
    public class MyDbContext: BaseDbContext
    {
        public MyDbContext() : base(new OracleConnection(WebConfigurationManager.ConnectionStrings[$"{AllStatic.EnvironmentType}.oracleConn"].ConnectionString),true){
        }

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