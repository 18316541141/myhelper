using CommonHelper.EFMap;
using log4net;
using log4net.Config;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebApplication1.App_Start;
using WebApplication1.Repository;
using Webdiyer.WebControls.Mvc;
namespace WebApplication1
{

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            XmlConfigurator.Configure(new FileInfo($"{AppDomain.CurrentDomain.SetupInformation.ApplicationBase}{Path.DirectorySeparatorChar}Log4net.config"));
            Database.SetInitializer<DbContext>(null);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutofacConfig.Register();

            //每隔10分钟检查心跳监测表，如果发现有超过10分钟没有心跳的，则马上报警
            new Thread(()=> {
                HeartbeatEntityRepository heartbeatEntityRepository = DependencyResolver.Current.GetService<HeartbeatEntityRepository>();
                while (true)
                {
                    DateTime temp = DateTime.Now.AddMinutes(-10);
                    try
                    {
                        foreach (HeartbeatEntity heartbeatEntity in heartbeatEntityRepository.FindList(a => a.LastHeartbeatTime < temp))
                        {

                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    Thread.Sleep(600000);
                }
            }).Start();
        }
    }
}
