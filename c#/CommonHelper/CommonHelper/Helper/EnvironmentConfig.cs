using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.App_Start
{
    /// <summary>
    /// 环境配置，根据开发环境、生产环境，编译不同的代码
    /// </summary>
    public static class EnvironmentConfig
    {
        public static string EnvironmentType { set; get; }

        static EnvironmentConfig()
        {
#if debugger
            EnvironmentType = "Debug";
#else
            EnvironmentType = "Release";
#endif
        }
    }
}