using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

namespace WebApplication1.Controllers
{
    /// <summary>
    /// 快速控制器，比其他控制器有更快的响应请求的速度，但不支持session的修改
    /// </summary>
    [SessionState(SessionStateBehavior.ReadOnly)]
    public class FastController:BaseController
    {
    }
}