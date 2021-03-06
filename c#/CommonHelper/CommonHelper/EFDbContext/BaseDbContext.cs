﻿using CommonHelper.CommonEntity;
using CommonHelper.EFMap;
using CommonHelper.Entity;
using CommonHelper.Helper.EFMap;
using CommonHelper.Mapping;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using WebApplication1.Entity;
using WebApplication1.Mapping;

namespace CommonHelper.Helper.EFDbContext
{
    /// <summary>
    /// 通用的数据源基类，所有数据源必须继承
    /// </summary>
    public abstract class BaseDbContext : DbContext
    {
        public BaseDbContext():base()
        {

        }
        public BaseDbContext(string nameOrConnectionString):base(nameOrConnectionString)
        {

        }
        public BaseDbContext(string nameOrConnectionString, DbCompiledModel model) : base(nameOrConnectionString,model)
        {

        }
        public BaseDbContext(DbConnection existingConnection, bool contextOwnsConnection) : base(existingConnection, contextOwnsConnection)
        {

        }
        public BaseDbContext(ObjectContext objectContext, bool dbContextOwnsObjectContext) : base(objectContext, dbContextOwnsObjectContext)
        {

        }
        public BaseDbContext(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection) : base(existingConnection, model, contextOwnsConnection)
        {

        }

        /// <summary>
        /// 数据源标识表
        /// </summary>
        public readonly DbSet<DistributedDataSrc> DistributedDataSrcs;

        /// <summary>
        /// 分布式分表
        /// </summary>
        public readonly DbSet<DistributedTransactionPart> DistributedTransactionParts;

        /// <summary>
        /// 全局变量表
        /// </summary>
        public readonly DbSet<GlobalVariable> GlobalVariables;

        /// <summary>
        /// 日志表
        /// </summary>
        public readonly DbSet<LogEntity> LogEntities;

        /// <summary>
        /// 心跳监测表
        /// </summary>
        public readonly DbSet<HeartbeatEntity> HeartbeatEntities;

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new DistributedTransactionPartMap());
            modelBuilder.Configurations.Add(new DistributedDataSrcMap());
            modelBuilder.Configurations.Add(new LogEntityMap());
            modelBuilder.Configurations.Add(new HeartbeatEntityMap());
            modelBuilder.Configurations.Add(new GlobalVariableMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}