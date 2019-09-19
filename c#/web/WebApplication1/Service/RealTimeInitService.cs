using CommonWeb.Intf;

namespace WebApplication1.Service
{
    /// <summary>
    /// 实时刷新的首次调用
    /// </summary>
    public class RealTimeInitService : IRealTimeInitService
    {
        public bool Init(string realTimePool, string username)
        {
            return false;
        }
    }
}