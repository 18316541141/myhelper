using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CommonHelper.Helper
{
    /// <summary>
    /// 自定义一个匹配规则，当前遍历的元素和自定义规则匹配返回true，否则返回false
    /// </summary>
    /// <typeparam name="T">当前遍历到的元素类型</typeparam>
    /// <param name="val">当前遍历到的元素</param>
    /// <returns>返回是否匹配</returns>
    public delegate bool MatchRule<T>(T val);

    /// <summary>
    /// 迭代器帮助类
    /// </summary>
    public static class EnumerableHelper
    {
        /// <summary>
        /// 删除map匹配的元素，该方法不是线程安全。
        /// </summary>
        /// <typeparam name="K">map的泛类型</typeparam>
        /// <param name="map">被删除的map</param>
        /// <param name="matchRule">匹配规则</param>
        public static void DelEleFromMap<K,V>(IDictionary<K, V> map,MatchRule<V> matchRule)
        {
            List<K> removeKeyList = new List<K>();
            foreach (KeyValuePair<K, V> keyVal in map)
                if (matchRule(keyVal.Value))
                    removeKeyList.Add(keyVal.Key);
            foreach (K key in removeKeyList)
                map.Remove(key);
        }

        /// <summary>
        /// 删除list匹配的元素，该方法不是线程安全的
        /// </summary>
        /// <typeparam name="V">list的泛类型</typeparam>
        /// <param name="list">被删除的list</param>
        /// <param name="matchRule">匹配规则</param>
        public static void DelEleFromList<V>(IList<V> list, MatchRule<V> matchRule)
        {
            for (int i=list.Count-1;i>-1 ;i--)
                if (matchRule(list[i]))
                    list.Remove(list[i]);
        }
    }
}
