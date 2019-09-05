using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper.CommonEntity
{
    /// <summary>
    /// 实时监听池的webSocket实体类
    /// </summary>
    public sealed class RealTimeWSEntity
    {
        /// <summary>
        /// 实时等待池名称
        /// </summary>
        public string RealTimePool { set; get; }

        /// <summary>
        /// 实时版本号
        /// </summary>
        public string RealTimeVersion { set; get; }
    }
}
