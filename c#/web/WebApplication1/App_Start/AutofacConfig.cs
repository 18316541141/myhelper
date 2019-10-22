using Autofac;
using Autofac.Integration.Mvc;
using CommonHelper.AopInterceptor;
using CommonHelper.Helper;
using CommonHelper.staticVar;
using CommonWeb.AutoThread;
using CommonWeb.Controllers.Common;
using CommonWeb.Entity.Common;
using CommonWeb.Intf;
using CommonWeb.Repository;
using log4net;
using RabbitMQ.Client;
using Snowflake.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Repository;
using WebApplication1.Service;

namespace WebApplication1.App_Start
{
    /// <summary>
    /// autofac专用的配置类
    /// </summary>
    public static class AutofacConfig
    {
        /// <summary>
        /// 注册autofac配置
        /// </summary>
        public static void Register()
        {
            //autofac容器工厂
            ContainerBuilder containerBuilder = new ContainerBuilder();
            string ip;
            try
            {
                ip = NetworkHelper.GetOuterNetIP();
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
            containerBuilder.RegisterInstance(idWorker).As<IdWorker>().SingleInstance().PropertiesAutowired();
            containerBuilder.RegisterInstance(new MyLog()).As<ILog>().SingleInstance().PropertiesAutowired();
            containerBuilder.RegisterType<RealTimeInitService>().As<IRealTimeInitService>().SingleInstance().PropertiesAutowired();
            containerBuilder.RegisterType<UserService>().As<IUserService>().SingleInstance().PropertiesAutowired();
            containerBuilder.RegisterType<MyHeartbeatEntityRepository>().AsSelf().As<HeartbeatEntityRepository>().SingleInstance().PropertiesAutowired();
            containerBuilder.RegisterType<MyLogEntityRepository>().AsSelf().As<LogEntityRepository>().SingleInstance().PropertiesAutowired();
            containerBuilder.RegisterType<MyGlobalVariableRepository>().AsSelf().As<GlobalVariableRepository>().SingleInstance().PropertiesAutowired();
            containerBuilder.RegisterAssemblyTypes(
                typeof(MvcApplication).Assembly,
                typeof(BaseController).Assembly).
            Where(n => (n.Name.EndsWith("Repository") || n.Name.EndsWith("Service")) 
            && n.Name!= "RealTimeInitService" && n.Name!= "UserService"
            && n.Name!= "MyHeartbeatEntityRepository" && n.Name!= "MyLogEntityRepository").SingleInstance().AsSelf().PropertiesAutowired();
            containerBuilder.RegisterControllers(
                typeof(MvcApplication).Assembly,
                typeof(BaseController).Assembly
            ).PropertiesAutowired().InstancePerRequest();
            IContainer container = containerBuilder.Build();
            AllAutoThread allAutoThread = new AllAutoThread(container.Resolve<MyHeartbeatEntityRepository>());
            allAutoThread.log = container.Resolve<ILog>();
            allAutoThread.Start();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}