using Autofac;
using Autofac.Integration.Mvc;
using CommonHelper.Helper;
using log4net;
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
            long ipNum=Convert.ToInt32(ip.Replace(".", ""));
            IdWorker idWorker = new IdWorker((ipNum>>5)&31, ipNum & 31);
            containerBuilder.RegisterInstance(idWorker).As<IdWorker>().SingleInstance().PropertiesAutowired();
            containerBuilder.RegisterAssemblyTypes(typeof(MvcApplication).Assembly).Where(n => n.Name.EndsWith("Repository") || n.Name.EndsWith("Service")).SingleInstance().AsSelf().PropertiesAutowired();
            containerBuilder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired().InstancePerRequest();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(containerBuilder.Build()));
        }
    }
}