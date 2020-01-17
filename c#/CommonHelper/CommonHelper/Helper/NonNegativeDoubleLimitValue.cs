using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        /// 内置非负数整数限制值
        /// </summary>
        NonNegativeIntLimitValue _NonNegativeIntLimitValue { set; get; }

        /// <summary>
        /// 阈值
        /// </summary>
        public double Threshold { private set; get; }

        /// <summary>
        /// 小数位数
        /// </summary>
        int _DecimalCount { set; get; }

        /// <summary>
        /// 只读变量表
        /// </summary>
        ReadOnlyDictionary<string, long> _ReadOnlyMap { set; get; }

        /// <summary>
        /// 只读浮点变量表
        /// </summary>
        ReadOnlyDictionary<string, double> _ReadOnlyDoubleMap { set; get; }

        /// <summary>
        /// 创建非负整数限制
        /// </summary>
        /// <param name="threshold">阈值，所有变量的和必须等于阈值</param>
        /// <param name="decimalCoumt">小数位数</param>
        /// <param name="limitValueRule">限制规则</param>
        /// <param name="keys">变量的key值</param>
        public NonNegativeDoubleLimitValue(double threshold, int decimalCount, LimitValueRule limitValueRule, params string[] keys)
        {
            NonNegativeIntLimitValue.CloseDebugOutput();
            _DecimalCount = decimalCount;
            Threshold = threshold;
            _NonNegativeIntLimitValue = new NonNegativeIntLimitValue(Convert.ToInt64(threshold * Math.Pow(10, _DecimalCount)), limitValueRule, keys);
            _ReadOnlyMap = _NonNegativeIntLimitValue.GetDictionary();
            Dictionary<string, double> retMap = new Dictionary<string, double>();
            foreach (var keyVal in _ReadOnlyMap)
            {
                retMap.Add(keyVal.Key, keyVal.Value / Math.Pow(10, _DecimalCount));
            }
            _ReadOnlyDoubleMap = new ReadOnlyDictionary<string, double>(retMap);
#if DEBUG
            Console.WriteLine(OutputDebug());
#endif
        }

        /// <summary>
        /// 更新阈值，当阈值相对之前的阈值增加时
        ///     在“全等限制”下，所有变量都增加，直到符合全等限制为止、
        ///     在“等于或小于限制（自动、手动）”下，所有变量不变
        /// 当阈值相对之前的阈值减少时
        ///     在“全等限制”下，所有变量都减少，直到符合全等限制为止、
        ///     在“等于或小于限制（自动、手动）”下，所有变量和不超过阈值时变量不变；
        ///     超过阈值时所有变量都减少，直到符合全等限制为止
        /// </summary>
        /// <param name="newThreadHold">更新的阈值</param>
        public void UpdateThreadHold(double newThreadHold)
        {
            Threshold = newThreadHold < 0 ? 0 : newThreadHold;
            _NonNegativeIntLimitValue.UpdateThreadHold(Convert.ToInt64(newThreadHold * Math.Pow(10, _DecimalCount)));
#if DEBUG
            Console.WriteLine($"update newThreadHold={newThreadHold}");
            Console.WriteLine(OutputDebug());
#endif
        }


#if DEBUG
        /// <summary>
        /// 输出调试信息
        /// </summary>
        string OutputDebug()
        {
            StringBuilder outPut = new StringBuilder("Threshold=");
            outPut.Append(Threshold).Append("，");
            foreach (var keyVal in GetDictionary())
            {
                outPut.Append(keyVal.Key).Append("=").Append(keyVal.Value).Append("，");
            }
            string connChar = "";
            double sum = 0;
            foreach (var keyVal in GetDictionary())
            {
                outPut.Append(connChar).Append(keyVal.Key);
                connChar = "+";
                sum += keyVal.Value;
            }
            outPut.Append("=").Append(sum);
            return outPut.ToString();
        }
#endif

        /// <summary>
        /// 获取变量值
        /// </summary>
        /// <param name="key">变量名</param>
        /// <returns>返回变量值</returns>
        public double GetVal(string key)
        {
            return _NonNegativeIntLimitValue.GetVal(key) / Math.Pow(10, _DecimalCount);
        }

        /// <summary>
        /// 追加新变量
        /// 在“全等限制”下，所有变量
        /// </summary>
        /// <param name="key">变量名</param>
        /// <param name="val">变量值</param>
        public void AddVal(string key, double val = 0)
        {
            _NonNegativeIntLimitValue.AddVal(key, Convert.ToInt64(val * Math.Pow(10, _DecimalCount)));
#if DEBUG
            Console.WriteLine($"add {key}={val}");
            Console.WriteLine(OutputDebug());
#endif
        }

        /// <summary>
        /// 更新值
        /// </summary>
        /// <param name="key">变量名称</param>
        /// <param name="newVal">新值</param>
        public void UpdateVal(string key, double newVal)
        {
            _NonNegativeIntLimitValue.UpdateVal(key, Convert.ToInt64(newVal * Math.Pow(10, _DecimalCount)));
#if DEBUG
            Console.WriteLine($"update {key}={newVal}");
            Console.WriteLine(OutputDebug());
#endif
        }

        /// <summary>
        /// 获取只读的map，用于遍历。
        /// </summary>
        /// <returns></returns>
        public ReadOnlyDictionary<string, double> GetDictionary()
        {
            ReadOnlyDictionary<string, long> tempMap = _NonNegativeIntLimitValue.GetDictionary();
            lock (this)
            {
                if(_ReadOnlyMap == tempMap)
                {
                    return _ReadOnlyDoubleMap;
                }
                else
                {
                    Dictionary<string, double> retMap = new Dictionary<string, double>();
                    foreach (var keyVal in tempMap)
                    {
                        retMap.Add(keyVal.Key, keyVal.Value / Math.Pow(10, _DecimalCount));
                    }
                    return _ReadOnlyDoubleMap = new ReadOnlyDictionary<string, double>(retMap);
                }
            }
        }
    }
}