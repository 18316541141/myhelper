using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CommonHelper.Helper
{
    public delegate double ActivateFunc(double x);

    /// <summary>
    /// 激活函数的帮助类
    /// </summary>
    public static class ActivateFuncHelper
    {
        /// <summary>
        /// Signmoid函数的导函数
        /// </summary>
        /// <param name="y">Signmoid函数的输出值</param>
        /// <returns>返回该Signmoid的输出值对应的x值的斜率</returns>
        public static double Dsignmoid(double y) => y * (1 - y);

        /// <summary>
        /// 对矩阵进行Dsignmoid求导
        /// </summary>
        /// <param name="martix">矩阵</param>
        /// <returns>返回激活后的矩阵</returns>
        public static double[,] Dsignmoid(double[,] martix)
        {
            double[,] ret = new double[martix.GetLength(0), martix.GetLength(1)];
            for (int i = 0, len_i = martix.GetLength(0); i < len_i; i++)
                for (int j = 0, len_j = martix.GetLength(1); j < len_j; j++)
                    ret[i, j] = Dsignmoid(martix[i, j]);
            return ret;
        }

        /// <summary>
        /// Signmoid函数
        /// </summary>
        /// <param name="X"></param>
        /// <returns></returns>
        public static double Signmoid(double x) => 1 / (1 + Math.Exp(-x));


        /// <summary>
        /// 对矩阵进行Signmoid激活
        /// </summary>
        /// <param name="martix">矩阵</param>
        /// <returns>返回激活后的矩阵</returns>
        public static double[,] Signmoid(double[,] martix)
        {
            double[,] ret = new double[martix.GetLength(0), martix.GetLength(1)];
            for (int i = 0, len_i = martix.GetLength(0); i < len_i; i++)
                for (int j = 0, len_j = martix.GetLength(1); j < len_j; j++)
                    ret[i, j] = Signmoid(martix[i, j]);
            return ret;
        }
    }
}
