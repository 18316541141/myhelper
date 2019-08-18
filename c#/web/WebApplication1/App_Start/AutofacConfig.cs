using Autofac;
using Autofac.Integration.Mvc;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
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
            containerBuilder.RegisterAssemblyTypes(typeof(MvcApplication).Assembly).Where(n => n.Name.EndsWith("Repository") || n.Name.EndsWith("Service")).SingleInstance().AsSelf().PropertiesAutowired();
            containerBuilder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired().InstancePerRequest();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(containerBuilder.Build()));
        }
    }
}