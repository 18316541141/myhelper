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
        /// 等于或小于限制，变量和必须小于等于阈值，当
        /// 变量增加时并超出限制时，自动的从其他变量扣减
        /// </summary>
        LESS_OR_EQUALS_AUTO,

        /// <summary>
        /// 等于或小于限制，变量和必须小于等于阈值，当
        /// 变量增加时并超出限制时，不会自动扣减，而是停止增加；
        /// 用户需要手动从其他变量扣减
        /// </summary>
        LESS_OR_EQUALS_MANUAL
    }

    /// <summary>
    /// 非负数整数限制值，假设池中有a、b、c三个变量，在满足变量和
    /// 等于阈值时，三个变量也必须是非负数
    /// </summary>
    public class NonNegativeIntLimitValue
    {
        static NonNegativeIntLimitValue()
        {
            _DebugOutput = true;
        }

        /// <summary>
        /// 调试输出的开关
        /// </summary>
        static bool _DebugOutput { set; get; }

        /// <summary>
        /// 关闭调试输出
        /// </summary>
        public static void CloseDebugOutput()
        {
            _DebugOutput = false;
        }

        /// <summary>
        /// 阈值
        /// </summary>
        public long Threshold { private set; get; }

        /// <summary>
        /// 变量表
        /// </summary>
        Dictionary<string, long> _ValuesMap { set; get; }

        /// <summary>
        /// 只读变量表
        /// </summary>
        ReadOnlyDictionary<string, long> _ReadOnlyMap { set; get; }

        /// <summary>
        /// 只读表是否为最新版
        /// </summary>
        bool _ReadOnlyIsNewest { set; get; }

        /// <summary>
        /// 限制规则
        /// </summary>
        LimitValueRule _LimitValueRule { set; get; }

        /// <summary>
        /// 创建非负整数限制
        /// </summary>
        /// <param name="threshold">阈值，所有变量的和必须等于阈值</param>
        /// <param name="limitValueRule">限制规则</param>
        /// <param name="keys">变量的key值</param>
        public NonNegativeIntLimitValue(long threshold, LimitValueRule limitValueRule, params string[] keys)
        {
            _LimitValueRule = limitValueRule;
            Threshold = threshold;
            _ReadOnlyIsNewest = true;
            _ValuesMap = new Dictionary<string, long>();
            if(_LimitValueRule == LimitValueRule.EQUALS)
            {
                long rest = threshold % keys.Length;
                long avg = (threshold - rest) / keys.Length;
                foreach (string key in keys)
                {
                    _ValuesMap.Add(key, avg);
                }
                _ValuesMap[keys[0]] += rest;
            }
            else
            {
                foreach (string key in keys)
                {
                    _ValuesMap.Add(key, 0);
                }
            }
            _ReadOnlyMap = new ReadOnlyDictionary<string, long>(_ValuesMap);
#if DEBUG
            if (_DebugOutput)
            {
                Console.WriteLine(OutputDebug());
            }
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
            long sum = 0;
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
        public ReadOnlyDictionary<string, long> GetDictionary()
        {
            if (!_ReadOnlyIsNewest)
            {
                lock (this)
                {
                    if (!_ReadOnlyIsNewest)
                    {
                        _ReadOnlyMap = new ReadOnlyDictionary<string, long>(_ValuesMap);
                        _ReadOnlyIsNewest = true;

                    }
                }
            }
            return _ReadOnlyMap;
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
        /// <param name="newThreshold">更新的阈值</param>
        public void UpdateThreshold(long newThreshold)
        {
            if (newThreshold < 0)
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
                long changeThreshold = 0;
                if (_LimitValueRule == LimitValueRule.EQUALS)
                {
                    changeThreshold = newThreshold - Threshold;
                }
                else if (_LimitValueRule == LimitValueRule.LESS_OR_EQUALS_AUTO || _LimitValueRule == LimitValueRule.LESS_OR_EQUALS_MANUAL)
                {
                    changeThreshold = newThreshold - _ValuesMap.Values.Sum();
                    if (changeThreshold > 0)
                    {
                        Threshold = newThreshold;
#if DEBUG
                        if (_DebugOutput)
                        {
                            Console.WriteLine($"update newThreshold={newThreshold}");
                            Console.WriteLine(OutputDebug());
                        }
#endif
                        return;
                    }
                }
                Threshold = newThreshold;
                long restChangeThreshold = changeThreshold % _ValuesMap.Count;
                long avgChangeThreshold = (changeThreshold - restChangeThreshold) / _ValuesMap.Count;
                long afterVal;
                foreach (var key in _ValuesMap.Keys.ToList())
                {
                    afterVal = _ValuesMap[key] + avgChangeThreshold;
                    if (afterVal >= 0)
                    {
                        _ValuesMap[key] = afterVal;
                    }
                    else
                    {
                        _ValuesMap[key] = 0;
                        restChangeThreshold += afterVal;
                    }
                }
                if (restChangeThreshold != 0)
                {
                    foreach (var key in _ValuesMap.Keys.ToList())
                    {
                        afterVal = restChangeThreshold + _ValuesMap[key];
                        if (afterVal < 0)
                        {
                            _ValuesMap[key] = 0;
                            restChangeThreshold = afterVal;
                        }
                        else
                        {
                            _ValuesMap[key] = afterVal;
                            break;
                        }
                    }
                }
#if DEBUG
                if (_DebugOutput)
                {
                    Console.WriteLine($"update newThreshold={newThreshold}");
                    Console.WriteLine(OutputDebug());
                }
#endif
            }
        }

        /// <summary>
        /// 获取变量值
        /// </summary>
        /// <param name="key">变量名</param>
        /// <returns>返回变量值</returns>
        public long GetVal(string key)
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
        /// 在“全等限制”下，所有变量
        /// </summary>
        /// <param name="key">变量名</param>
        /// <param name="val">变量值</param>
        public void AddVal(string key, long val = 0)
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
                long lossVal = Threshold - _ValuesMap.Values.Sum();
                if (lossVal >= 0)
                {
#if DEBUG
                    if (_DebugOutput)
                    {
                        Console.WriteLine($"add {key}={val}");
                        Console.WriteLine(OutputDebug());
                    }
#endif
                    return;
                }
                if (_LimitValueRule == LimitValueRule.LESS_OR_EQUALS_MANUAL)
                {
                    _ValuesMap[key] = 0;
#if DEBUG
                    if (_DebugOutput)
                    {
                        Console.WriteLine($"add {key}={val}");
                        Console.WriteLine(OutputDebug());
                    }
#endif
                    return;
                }
                long rest = lossVal % (_ValuesMap.Count - 1);
                long avgLossVal = (lossVal - rest) / (_ValuesMap.Count - 1);
                long afterVal;
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
                if (_DebugOutput)
                {
                    Console.WriteLine($"add {key}={val}");
                    Console.WriteLine(OutputDebug());
                }
#endif
            }
        }

        /// <summary>
        /// 更新值
        /// </summary>
        /// <param name="key">变量名称</param>
        /// <param name="newVal">新值</param>
        public void UpdateVal(string key, long newVal)
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
                long lossVal = _ValuesMap.Values.Sum() - Threshold;
                if (lossVal <= 0 && _LimitValueRule == LimitValueRule.LESS_OR_EQUALS_AUTO)
                {
#if DEBUG
                    if (_DebugOutput)
                    {
                        Console.WriteLine($"update {key}={newVal}");
                        Console.WriteLine(OutputDebug());
                    }
#endif
                    return;
                }
                if(_LimitValueRule == LimitValueRule.LESS_OR_EQUALS_MANUAL)
                {
                    if (lossVal > 0)
                    {
                        _ValuesMap[key] -= lossVal;
                    }
#if DEBUG
                    if (_DebugOutput)
                    {
                        Console.WriteLine($"update {key}={newVal}");
                        Console.WriteLine(OutputDebug());
                    }
#endif
                    return;
                }
                long rest = lossVal % (_ValuesMap.Count - 1);
                long lossValAvg = (lossVal - rest) / (_ValuesMap.Count - 1);
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
                if (_DebugOutput)
                {
                    Console.WriteLine($"update {key}={newVal}");
                    Console.WriteLine(OutputDebug());
                }
#endif
            }
        }
    }
}
