using Autofac;
using Autofac.Integration.Mvc;
using CommonHelper.AopInterceptor;
using CommonHelper.Helper;
using CommonHelper.staticVar;
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
            ILog log = LogManager.GetLogger("Log4net.config");
            containerBuilder.RegisterInstance(log).As<ILog>().SingleInstance().PropertiesAutowired();
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
            //ConnectionFactory factory = new ConnectionFactory { HostName = "hostname", UserName = "root", Password = "root001", VirtualHost = "hostserver" };
            //containerBuilder.RegisterInstance(factory.CreateConnection()).As<IConnection>().SingleInstance().PropertiesAutowired();
            AllStatic.IdWorker = idWorker;
            containerBuilder.RegisterInstance(idWorker).As<IdWorker>().SingleInstance().PropertiesAutowired();
            containerBuilder.RegisterAssemblyTypes(typeof(MvcApplication).Assembly).Where(n => n.Name.EndsWith("Repository") || n.Name.EndsWith("Service")).SingleInstance().AsSelf().PropertiesAutowired();
            containerBuilder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired().InstancePerRequest();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(containerBuilder.Build()));
        }
    }
}