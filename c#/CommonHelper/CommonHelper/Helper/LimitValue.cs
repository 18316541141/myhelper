using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper.Helper
{
    /// <summary>
    /// 限制值类.假设有a、b、c三个变量，
    /// 并规定三个变量的和必须等于100，当改变a、b、c任意一个数的值时，
    /// 另外两个数也必须要改变才能保证三个变量的值等于100.
    /// </summary>
    public abstract class LimitValue<NumType>
    {
        public LimitValue(NumType threshold, params string[] keys)
        {
            _Threshold = threshold;
            _ReadOnlyIsNewest = true;
        }

        /// <summary>
        /// 获取只读的map，用于遍历。
        /// </summary>
        /// <returns></returns>
        public ReadOnlyDictionary<string, NumType> GetDictionary()
        {
            if (!_ReadOnlyIsNewest)
            {
                lock (this)
                {
                    if (!_ReadOnlyIsNewest)
                    {
                        _ReadOnlyMap = new ReadOnlyDictionary<string, NumType>(_ValuesMap);
                        _ReadOnlyIsNewest = true;

                    }
                }
            }
            return _ReadOnlyMap;
        }

        /// <summary>
        /// 阈值
        /// </summary>
        protected NumType _Threshold { set; get; }

        /// <summary>
        /// 变量表
        /// </summary>
        protected Dictionary<string, NumType> _ValuesMap { set; get; }

        /// <summary>
        /// 只读变量表
        /// </summary>
        protected ReadOnlyDictionary<string, NumType> _ReadOnlyMap { set; get; }

        /// <summary>
        /// 只读表是否为最新版
        /// </summary>
        protected bool _ReadOnlyIsNewest { set; get; }
    }
}
