using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Controllers
{
    /// <summary>
    /// 跨域控制器，当前后台管理系统默认对含有Jsonp开头的控制器都进行
    /// 签名校验
    /// </summary>
    public class JsonpController:FastController
    {
    }
}