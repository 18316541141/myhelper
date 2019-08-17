using Autofac;
using Autofac.Integration.Mvc;
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
            containerBuilder.RegisterAssemblyTypes(typeof(MvcApplication).Assembly).Where(n => n.Name.EndsWith("Repository") && n.Name.EndsWith("Service")).AsSelf().PropertiesAutowired().SingleInstance();
            containerBuilder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired().SingleInstance();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(containerBuilder.Build()));
        }
    }
}