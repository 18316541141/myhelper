﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication1.DbTypeTrans;
using WindowsFormsApplication1.entity;
using WindowsFormsApplication1.NameTrans;
using WindowsFormsApplication1.Service;
using WindowsFormsApplication1.SqlInfo;

namespace WindowsFormsApplication1
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Data Source = 183.2.233.235; Initial Catalog = BusinessAssistantDB_Test; User ID = BusinessHeplerTestManager; Password = BusinessHeplerTestManager123; MultipleActiveResultSets = True
            GenEntityService genEntityService = new GenEntityService
            {
                NameTrans = new RobotNameCSharpTrans(),
                //DbTypeTrans = new SqlServerToCSharpTrans(),
                DbTypeTrans = new MySqlToCSharpTrans(),
                //DbTypeTrans = new SqliteToCSharpTrans(),
                //SqlInfo = new SqlServerInfo("183.2.233.235", "BusinessAssistantDB_Test", "BusinessHeplerTestManager", "BusinessHeplerTestManager123"),
                //SqlInfo = new SqliteInfo(@"D:\sqlite\databases\test.db")
                //server=localhost;Database=database01;UID=root;PWD=;SslMode=none
                SqlInfo = new MySqlInfo("localhost", "database01", "root", "", 3306)
            };
            Entity entity = genEntityService.GenTemplateEntity("Fisherycrewsystem");
            EntityTemplateToCode entityTemplateToCode = new EntityTemplateToCode();
            entityTemplateToCode.EntityFrameworkCode(entity);
            //entityTemplateToCode.MyBatisCode(entity);
            //------------------------------下面是内存表操作生成部分------------------------------
            //GenMemoryEntityService genMemoryEntityService = new GenMemoryEntityService();
            //Entity entity = genMemoryEntityService.GenTemplateEntity(new HeartbeatEntity
            //{
            //    Id=1,
            //    LastHeartbeatTime=DateTime.Now,
            //    Username = "asdasd",
            //});
            //EntityTemplateToCode entityTemplateToCode = new EntityTemplateToCode();
            //entityTemplateToCode.CSharpMemoryRepository(entity);
        }
    }
}