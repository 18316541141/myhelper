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

            GenEntityService genEntityService = new GenEntityService
            {
                NameTrans = new BsyNameTrans(),
                DbTypeTrans = new SqliteToCSharpTrans(),
                SqlInfo = new SqliteInfo(@"D:\sqlite\databases\test.db")
            };
            Entity entity=genEntityService.GenTemplateEntity("t_ignoreStation");
            EntityTemplateToCode entityTemplateToCode = new EntityTemplateToCode();
            entityTemplateToCode.EntityFrameworkCode(entity);
        }
    }
}
