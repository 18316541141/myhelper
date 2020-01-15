using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CommonHelper.Helper
{
    /// <summary>
    /// 正数限制值，假设池中有a、b、c三个变量，在满足变量和
    /// 等于阈值时，三个变量也必须是正数
    /// </summary>
    public class PositiveDoubleLimitValue : LimitValue<double>
    {
        /// <summary>
        /// 创建正数限制池
        /// </summary>
        /// <param name="threshold">阈值，所有变量的和必须等于阈值</param>
        /// <param name="keys">变量的key值</param>
        public PositiveDoubleLimitValue(double threshold, params string[] keys):base(threshold, keys)
        {
            double rest = threshold % keys.Length;
            double avg = (threshold- rest) / keys.Length;
            foreach (string key in keys)
            {
                _ValuesMap.Add(key, avg);
            }
            _ValuesMap[keys[0]] += rest;
            _ReadOnlyMap = new ReadOnlyDictionary<string, double>(_ValuesMap);
        }

        /// <summary>
        /// 更新值
        /// </summary>
        /// <param name="key">变量名称</param>
        /// <param name="newVal">新值</param>
        public void UpdateVal(string key, double newVal)
        {
            if (newVal > _Threshold)
            {
                newVal = _Threshold;
            }
            else if (newVal < 0)
            {
                newVal = 0;
            }
            lock (this)
            {
                double lossVal = _ValuesMap.Values.Sum() - _Threshold;
                double lossValAvg = lossVal / (_ValuesMap.Count - 1);
                double rest = 0;
                foreach (var keyVal in _ValuesMap)
                {
                    if (keyVal.Key != key)
                    {
                        if (keyVal.Value < lossValAvg)
                        {
                            _ValuesMap[keyVal.Key] = 0;
                            rest += lossValAvg - keyVal.Value;
                        }
                        else
                        {
                            _ValuesMap[keyVal.Key] -= lossValAvg;
                        }
                    }
                }
                foreach (var keyVal in _ValuesMap)
                {
                    if (keyVal.Key != key && keyVal.Value > 0)
                    {
                        if (keyVal.Value < rest)
                        {
                            _ValuesMap[keyVal.Key] = 0;
                            rest -= keyVal.Value;
                        }
                        else
                        {
                            _ValuesMap[keyVal.Key] -= rest;
                            break;
                        }
                    }
                }
                _ReadOnlyIsNewest = false;
            }
        }
    }
}
