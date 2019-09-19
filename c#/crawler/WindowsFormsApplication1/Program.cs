using Autofac;
using Autofac.Extras.DynamicProxy;
using CommonHelper.AopInterceptor;
using CommonHelper.Helper;
using CommonHelper.staticVar;
using CX_Task_Center.Code.Message;
using log4net;
using log4net.Config;
using Snowflake.Net;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebApplication1.Entity;

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
            string ip;
            try
            {
                ip = IpHelper.GetOuterNetIP();
            }
            catch (Exception)
            {
                IPHostEntry ipe = Dns.GetHostEntry(Dns.GetHostName());
                ip = ipe.AddressList[4].ToString();
            }
            int ipNum = 0;
            string[] parts = ip.Split('.');
            for (int i = 0, len = parts.Length; i < len; i++)
            {
                ipNum += Convert.ToInt32(parts[0]) << ((3 - i) * 8);
            }
            IdWorker idWorker = new IdWorker((ipNum >> 27) & 31, ipNum & 31);
            AllStatic.IdWorker = idWorker;
            containerBuilder.Register(c => new DistributedTransactionScan());
            containerBuilder.Register(c => new DistributeRepository());
            containerBuilder.RegisterInstance(idWorker).As<IdWorker>().SingleInstance().PropertiesAutowired();
            containerBuilder.RegisterInstance(bsyWarningHelper).As<BsyWarningHelper>().SingleInstance().PropertiesAutowired();
            containerBuilder.RegisterInstance(log).As<ILog>().SingleInstance().PropertiesAutowired();
            containerBuilder.RegisterAssemblyTypes(typeof(Program).Assembly).Where(n => n.Name.EndsWith("Repository") || n.Name.EndsWith("Service") || n.Name.EndsWith("Controller"))
                .SingleInstance().AsSelf().PropertiesAutowired().EnableClassInterceptors();
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
