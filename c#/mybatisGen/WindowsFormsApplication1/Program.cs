using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication1.entity;
using WindowsFormsApplication1.Service;

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
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            //Data Source = 183.2.233.235; Initial Catalog = BusinessAssistantDB_Test; User ID = BusinessHeplerTestManager; Password = BusinessHeplerTestManager123; MultipleActiveResultSets = True
            GenEntityService genEntityService = new GenEntityService
            {
                NameTrans = new RobotNameCSharpTrans(),
                DbTypeTrans = new SqlServerToCSharpTrans(),
                //DbTypeTrans = new SqliteToCSharpTrans(),
                SqlInfo = new SqlServerInfo("183.2.233.235", "BusinessAssistantDB_Test", "BusinessHeplerTestManager", "BusinessHeplerTestManager123"),
                //SqlInfo =new SqliteInfo(@"D:\sqlite\databases\test.db")
            };
            Entity entity=genEntityService.GenTemplateEntity("IRobot_RobotManager");
            EntityTemplateToCode entityTemplateToCode = new EntityTemplateToCode();
            entityTemplateToCode.EntityFrameworkCode(entity);
        }
    }
}