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
        /// 获取排序列和主键
        /// </summary>
        public Expression<Func<TEntity, TEntity>> OrderColAndKeyLazy { set; get; }

        /// <summary>
        /// in查询条件
        /// </summary>
        public InCondition<TEntity> InCondition { set; get; }

        /// <summary>
        /// 大于某个值的where条件
        /// </summary>
        public WhereCompare<TEntity> GreatThan { set; get; }

        /// <summary>
        /// 大于或等于某个值的where条件
        /// </summary>
        public WhereCompare<TEntity> GreatEqThan { set; get; }

        /// <summary>
        /// 小于某个值的where条件
        /// </summary>
        public WhereCompare<TEntity> LessThan { set; get; }

        /// <summary>
        /// 小于或等于某个值的where条件
        /// </summary>
        public WhereCompare<TEntity> LessEqThan { set; get; }

        /// <summary>
        /// 排序方式，升序：true、降序:false
        /// </summary>
        public bool OrderType { set; get; }
    }

    /// <summary>
    /// 动态返回大于或小于某个值的where条件表达式
    /// </summary>
    /// <typeparam name="TEntity">实体类</typeparam>
    /// <param name="val">对比值</param>
    /// <returns></returns>
    public delegate Expression<Func<TEntity, bool>> WhereCompare<TEntity>(IComparable val);

    /// <summary>
    /// in条件，这里使用主键作为in的查询条件
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="entities"></param>
    /// <returns></returns>
    public delegate Expression<Func<TEntity, bool>> InCondition<TEntity>(List<TEntity> entities);
}
