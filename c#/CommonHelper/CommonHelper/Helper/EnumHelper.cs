using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper.Helper
{
    /// <summary>
    /// 枚举帮助业务类
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// 根据枚举类型把相关的枚举分解为特定类型
        /// </summary>
        /// <typeparam name="T">返回集合的元素类型</typeparam>
        /// <typeparam name="N">枚举值类型</typeparam>
        /// <param name="type">枚举类型</param>
        /// <param name="convert">转换器</param>
        /// <param name="where">全等筛选条件</param>
        /// <returns>返回枚举的键值对</returns>
        public static List<T> EnumToList<T, N>(Type type, Conver<T, N> convert, Where<N> where = null)
        {
            List<T> ret = new List<T>();
            if (where == null)
            {
                foreach (var item in Enum.GetValues(type))
                {
                    KeyValuePair<string, N> keyVal = new KeyValuePair<string, N>(Enum.GetName(type, item), (N)item);
                    ret.Add(convert(keyVal));
                }
            }
            else
            {
                foreach (var item in Enum.GetValues(type))
                {
                    KeyValuePair<string, N> keyVal = new KeyValuePair<string, N>(Enum.GetName(type, item), (N)item);
                    if (where(keyVal))
                    {
                        ret.Add(convert(keyVal));
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// 根据枚举类型把相关的枚举分解为键值对
        /// </summary>
        /// <typeparam name="N">枚举值类型</typeparam>
        /// <param name="type">枚举类型</param>
        /// <param name="where">全等筛选条件</param>
        /// <returns>返回枚举的键值对</returns>
        public static Dictionary<string, N> EnumToMap<N>(Type type, Where<N> where = null)
        {
            Dictionary<string, N> ret = new Dictionary<string, N>();
            if (where == null)
            {
                foreach (var item in Enum.GetValues(type))
                {
                    ret.Add(Enum.GetName(type, item), (N)item);
                }
            }
            else
            {
                foreach (var item in Enum.GetValues(type))
                {
                    KeyValuePair<string, N> keyVal = new KeyValuePair<string, N>(Enum.GetName(type, item), (N)item);
                    if (where(keyVal))
                    {
                        ret.Add(keyVal.Key, keyVal.Value);
                    }
                }
            }
            return ret;
        }
    }

    /// <summary>
    /// 类型转化委托
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="keyVal"></param>
    /// <returns></returns>
    public delegate T Conver<T, N>(KeyValuePair<string, N> keyVal);

    /// <summary>
    /// 筛选委托
    /// </summary>
    /// <param name="keyVal"></param>
    /// <returns></returns>
    public delegate bool Where<N>(KeyValuePair<string, N> keyVal);
}
