using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CommonHelper.Helper
{
    /// <summary>
    /// 限制值规则
    /// </summary>
    public enum LimitValueRule
    {
        /// <summary>
        /// 全等限制，变量和必须等于阈值
        /// </summary>
        EQUALS,

        /// <summary>
        /// 等于或小于限制，变量和必须小于等于阈值
        /// </summary>
        LESS_OR_EQUALS,
    }

    /// <summary>
    /// 非负数整数限制值，假设池中有a、b、c三个变量，在满足变量和
    /// 等于阈值时，三个变量也必须是非负数
    /// </summary>
    public class NonNegativeIntLimitValue
    {
        /// <summary>
        /// 阈值
        /// </summary>
        public int Threshold { private set; get; }

        /// <summary>
        /// 变量表
        /// </summary>
        Dictionary<string, int> _ValuesMap { set; get; }

        /// <summary>
        /// 只读变量表
        /// </summary>
        ReadOnlyDictionary<string, int> _ReadOnlyMap { set; get; }

        /// <summary>
        /// 只读表是否为最新版
        /// </summary>
        bool _ReadOnlyIsNewest { set; get; }

        /// <summary>
        /// 限制规则
        /// </summary>
        LimitValueRule _LimitValueRule { set; get; }

        /// <summary>
        /// 创建非负数数限制池
        /// </summary>
        /// <param name="threshold">阈值，所有变量的和必须等于阈值</param>
        /// <param name="keys">变量的key值</param>
        public NonNegativeIntLimitValue(int threshold, LimitValueRule limitValueRule, params string[] keys)
        {
            _LimitValueRule = limitValueRule;
            Threshold = threshold;
            _ReadOnlyIsNewest = true;
            int rest = threshold % keys.Length;
            int avg = (threshold - rest) / keys.Length;
            _ValuesMap = new Dictionary<string, int>();
            foreach (string key in keys)
            {
                _ValuesMap.Add(key, avg);
            }
            _ValuesMap[keys[0]] += rest;
            _ReadOnlyMap = new ReadOnlyDictionary<string, int>(_ValuesMap);
#if DEBUG
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
            foreach (var keyVal in _ValuesMap)
            {
                outPut.Append(keyVal.Key).Append("=").Append(keyVal.Value).Append("，");
            }
            string connChar = "";
            int sum = 0;
            foreach (var keyVal in _ValuesMap)
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
        /// 获取只读的map，用于遍历。
        /// </summary>
        /// <returns></returns>
        public ReadOnlyDictionary<string, int> GetDictionary()
        {
            if (!_ReadOnlyIsNewest)
            {
                lock (this)
                {
                    if (!_ReadOnlyIsNewest)
                    {
                        _ReadOnlyMap = new ReadOnlyDictionary<string, int>(_ValuesMap);
                        _ReadOnlyIsNewest = true;

                    }
                }
            }
            return _ReadOnlyMap;
        }

        /// <summary>
        /// 更新阈值
        /// </summary>
        /// <param name="newThreadHold">更新的阈值</param>
        public void UpdateThreadHold(int newThreadHold)
        {
            if (newThreadHold < 0)
            {
                lock (this)
                {
                    Threshold = 0;
                    foreach (var key in _ValuesMap.Keys.ToList())
                    {
                        _ValuesMap[key] = 0;
                    }
                    _ReadOnlyIsNewest = false;
                }
                return;
            }
            lock (this)
            {
                _ReadOnlyIsNewest = false;
                int changeThreadHold = 0;
                if (_LimitValueRule == LimitValueRule.EQUALS)
                {
                    changeThreadHold = newThreadHold - Threshold;
                }
                else if (_LimitValueRule == LimitValueRule.LESS_OR_EQUALS)
                {
                    changeThreadHold = newThreadHold - _ValuesMap.Values.Sum();
                    if (changeThreadHold > 0)
                    {
                        Threshold = newThreadHold;
#if DEBUG
                        Console.WriteLine($"update newThreadHold={newThreadHold}");
                        Console.WriteLine(OutputDebug());
#endif
                        return;
                    }
                }
                Threshold = newThreadHold;
                int restChangeThreadHold = changeThreadHold % _ValuesMap.Count;
                int avgChangeThreadHold = (changeThreadHold - restChangeThreadHold) / _ValuesMap.Count;
                int afterVal;
                foreach (var key in _ValuesMap.Keys.ToList())
                {
                    afterVal = _ValuesMap[key] + avgChangeThreadHold;
                    if (afterVal >= 0)
                    {
                        _ValuesMap[key] = afterVal;
                    }
                    else
                    {
                        _ValuesMap[key] = 0;
                        restChangeThreadHold += afterVal;
                    }
                }
                if (restChangeThreadHold != 0)
                {
                    foreach (var key in _ValuesMap.Keys.ToList())
                    {
                        afterVal = restChangeThreadHold + _ValuesMap[key];
                        if (afterVal < 0)
                        {
                            _ValuesMap[key] = 0;
                            restChangeThreadHold = afterVal;
                        }
                        else
                        {
                            _ValuesMap[key] = afterVal;
                            break;
                        }
                    }
                }
#if DEBUG
                Console.WriteLine($"update newThreadHold={newThreadHold}");
                Console.WriteLine(OutputDebug());
#endif
            }
        }

        /// <summary>
        /// 获取变量值
        /// </summary>
        /// <param name="key">变量名</param>
        /// <returns>返回变量值</returns>
        public int GetVal(string key)
        {
            lock (this)
            {
                if (!_ValuesMap.ContainsKey(key))
                {
                    throw new Exception("变量不存在，获取失败！");
                }
                return _ValuesMap[key];
            }
        }

        /// <summary>
        /// 追加新变量
        /// </summary>
        /// <param name="key">变量名</param>
        /// <param name="val">变量值</param>
        public void AddVal(string key, int val = 0)
        {
            if (_ValuesMap.ContainsKey(key))
            {
                throw new Exception("变量已存在，无需再次添加！");
            }
            if (val > Threshold)
            {
                val = Threshold;
            }
            else if (val < 0)
            {
                val = 0;
            }
            lock (this)
            {
                _ReadOnlyIsNewest = false;
                _ValuesMap.Add(key, val);
                int lossVal = Threshold - _ValuesMap.Values.Sum();
                if (lossVal >= 0)
                {
#if DEBUG
                    Console.WriteLine($"add {key}={val}");
                    Console.WriteLine(OutputDebug());
#endif
                    return;
                }
                int rest = lossVal % (_ValuesMap.Count - 1);
                int avgLossVal = (lossVal - rest) / (_ValuesMap.Count - 1);
                int afterVal;
                foreach (string tempKey in _ValuesMap.Keys.ToList())
                {
                    if (tempKey != key)
                    {
                        afterVal = _ValuesMap[tempKey] + avgLossVal;
                        if (afterVal < 0)
                        {
                            _ValuesMap[tempKey] = 0;
                            rest += afterVal;
                        }
                        else
                        {
                            _ValuesMap[tempKey] = afterVal;
                        }
                    }
                }
                if (rest != 0)
                {
                    foreach (string tempKey in _ValuesMap.Keys.ToList())
                    {
                        if (tempKey != key && _ValuesMap[tempKey] > 0)
                        {
                            afterVal = _ValuesMap[tempKey] + rest;
                            if (afterVal < 0)
                            {
                                _ValuesMap[tempKey] = 0;
                                rest = afterVal;
                            }
                            else
                            {
                                _ValuesMap[tempKey] = afterVal;
                                break;
                            }
                        }
                    }
                }
#if DEBUG
                Console.WriteLine($"add {key}={val}");
                Console.WriteLine(OutputDebug());
#endif
            }
        }

        /// <summary>
        /// 更新值
        /// </summary>
        /// <param name="key">变量名称</param>
        /// <param name="newVal">新值</param>
        public void UpdateVal(string key, int newVal)
        {
            if (newVal > Threshold)
            {
                newVal = Threshold;
            }
            else if (newVal < 0)
            {
                newVal = 0;
            }
            lock (this)
            {
                _ReadOnlyIsNewest = false;
                _ValuesMap[key] = newVal;
                int lossVal = _ValuesMap.Values.Sum() - Threshold;
                if (_LimitValueRule == LimitValueRule.LESS_OR_EQUALS && lossVal < 0)
                {
#if DEBUG
                    Console.WriteLine($"update {key}={newVal}");
                    Console.WriteLine(OutputDebug());
#endif
                    return;
                }
                int rest = lossVal % (_ValuesMap.Count - 1);
                int lossValAvg = (lossVal - rest) / (_ValuesMap.Count - 1);
                foreach (var tempKey in _ValuesMap.Keys.ToList())
                {
                    if (tempKey != key)
                    {
                        if (_ValuesMap[tempKey] < lossValAvg)
                        {
                            _ValuesMap[tempKey] = 0;
                            rest += lossValAvg - _ValuesMap[tempKey];
                        }
                        else
                        {
                            _ValuesMap[tempKey] -= lossValAvg;
                        }
                    }
                }
                if (rest != 0)
                {
                    foreach (var tempKey in _ValuesMap.Keys.ToList())
                    {
                        if (tempKey != key && _ValuesMap[tempKey] > 0)
                        {
                            if (_ValuesMap[tempKey] < rest)
                            {
                                _ValuesMap[tempKey] = 0;
                                rest -= _ValuesMap[tempKey];
                            }
                            else
                            {
                                _ValuesMap[tempKey] -= rest;
                                break;
                            }
                        }
                    }
                }
#if DEBUG
                Console.WriteLine($"update {key}={newVal}");
                Console.WriteLine(OutputDebug());
#endif
            }
        }
    }
}
