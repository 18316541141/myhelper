using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper.CommonEntity
{
    /// <summary>
    /// 跨库分页的实体类，跨库分页需要根据排序条件，让用户提供以下几个表达式：
    ///     (1) 排序列，Func类型
    ///     (2) 排序列，Expression类型
    ///     (3) 排序方式，升序、降序
    /// </summary>
    public class DisPageEntity<TEntity>
    {
        /// <summary>
        /// 获取排序列
        /// </summary>
        public Func<TEntity, IComparable> OrderCol { set; get; }

        /// <summary>
        /// 获取排序列
        /// </summary>
        public Expression<Func<TEntity, IComparable>> OrderColLazy { set; get; }

        /// <summary>
        /// 排序方式，升序：true、降序:false
        /// </summary>
        public bool OrderType { set; get; }
    }
}
