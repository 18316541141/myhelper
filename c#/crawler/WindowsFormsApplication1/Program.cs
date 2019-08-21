using Autofac;
using CX_Task_Center.Code.Message;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication1.ServiceReference1;

namespace WindowsFormsApplication1
{
    static class Program
    {
        public static IContainer Container { private set; get; }

        [DllImport("kernel32.dll")]
        public static extern Boolean AllocConsole();

        static Program()
        {
            AllocConsole();
            XmlConfigurator.Configure(new FileInfo($"{AppDomain.CurrentDomain.SetupInformation.ApplicationBase}{Path.DirectorySeparatorChar}Log4net.config"));
            Database.SetInitializer<DbContext>(null);
            ContainerBuilder containerBuilder = new ContainerBuilder();
            ILog log = LogManager.GetLogger("Log4net.config");
            containerBuilder.RegisterType<MainForm>().As<MainForm>().SingleInstance();
            BsyWarningHelper bsyWarningHelper = new BsyWarningHelper();
            containerBuilder.RegisterInstance(bsyWarningHelper).As<BsyWarningHelper>().SingleInstance().PropertiesAutowired();
            containerBuilder.RegisterInstance(log).As<ILog>().SingleInstance().PropertiesAutowired();
            containerBuilder.RegisterAssemblyTypes(typeof(Program).Assembly).Where(n => n.Name.EndsWith("Repository") || n.Name.EndsWith("Service") || n.Name.EndsWith("Controller")).SingleInstance().AsSelf().PropertiesAutowired();
            Container = containerBuilder.Build();
        }

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(Container.Resolve<MainForm>());
        }
    }
}
