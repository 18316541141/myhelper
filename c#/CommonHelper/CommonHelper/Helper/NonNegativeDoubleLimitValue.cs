using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper.Helper
{
    /// <summary>
    /// 非负浮点数整数限制值，假设池中有a、b、c三个变量，在满足变量和
    /// 等于阈值时，三个变量也必须是非负浮点数
    /// </summary>
    public class NonNegativeDoubleLimitValue
    {
         /// <summary>
        /// 创建非负整数限制
        /// </summary>
        /// <param name="threshold">阈值，所有变量的和必须等于阈值</param>
        /// <param name="keys">变量的key值</param>
        public NonNegativeDoubleLimitValue(double threshold, LimitValueRule limitValueRule, params string[] keys)
        {

        }
    }
}
