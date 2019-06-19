using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper.Helper
{
    /// <summary>
    /// 组合帮助类
    /// </summary>
    public static class CombineHelper
    {

        /// <summary>
        /// 把每一种特定样本的可能情况用数组传入，返回该特定顺序下
        /// 所有可能的情况返回。
        /// </summary>
        /// <param name="indexLens">
        ///     特定顺序的样本每项可能的情况，例如传入{3,2,1}表示第一位有3种情况、
        ///     第二位有2种情况、第三位有1种情况
        /// </param>
        /// <returns>
        ///     返回所有可能情况，例如：传入{3,2,1}，返回：
        ///     {{0,0,0}、{1,0,0}、{2,0,0}、
        ///     {0,1,0}、{1,1,0}、{2,1,0}} 六种可能性。
        /// </returns>
        public static List<int[]> AllCombineList(int[] indexLens)
        {
            int partLen = indexLens.Length;
            long totalLen = 1;
            foreach (int len in indexLens) totalLen *= len;
            List<int[]> allCombineList = new List<int[]>();
            int[] zeroArrays = new int[indexLens.Length];
            for (int i = 0, j = 0; i < totalLen; i++)
            {
                int[] newArray = new int[indexLens.Length];
                Array.Copy(zeroArrays, newArray, indexLens.Length);
                allCombineList.Add(newArray);
                zeroArrays[j = 0]++;
                while (zeroArrays[j] == indexLens[j] && j < partLen)
                {
                    zeroArrays[j] = 0;
                    zeroArrays[j = ++j % partLen]++;
                }
            }
            return allCombineList;
        }
    }
}
