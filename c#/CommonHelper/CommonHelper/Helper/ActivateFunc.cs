using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CommonHelper.Helper
{
    /// <summary>
    /// Sigmoid激活函数
    /// </summary>
    public class Signmoid : ActivateFunc
    {
        public override decimal Active(decimal input) => new decimal(1 / (1 + Math.Exp(-decimal.ToDouble(input))));

        public override decimal Dactive(decimal input) => input * (1 - input);
    }

    /// <summary>
    /// 激活函数抽象类
    /// </summary>
    public abstract class ActivateFunc
    {
        /// <summary>
        /// 激活函数的导函数
        /// </summary>
        /// <param name="input">激活函数的输入值</param>
        /// <returns>返回该激活函数的输出值对应的x值的斜率</returns>
        public abstract decimal Dactive(decimal input);

        /// <summary>
        /// 激活函数
        /// </summary>
        /// <param name="input">激活函数输入值</param>
        /// <returns></returns>
        public abstract decimal Active(decimal input);


        /// <summary>
        /// 激活函数的导函数矩阵
        /// </summary>
        /// <param name="input">激活函数的输入值</param>
        /// <returns>返回该激活函数的导函数矩阵</returns>
        public decimal[,] Dactive(decimal[,] martix)
        {
            decimal[,] ret = new decimal[martix.GetLength(0), martix.GetLength(1)];
            for (int i = 0, len_i = martix.GetLength(0); i < len_i; i++)
                for (int j = 0, len_j = martix.GetLength(1); j < len_j; j++)
                    ret[i, j] = Dactive(martix[i, j]);
            return ret;
        }

        /// <summary>
        /// 激活函数的导函数矩阵
        /// </summary>
        /// <param name="input">激活函数的输入值</param>
        /// <returns>返回该激活函数的导函数矩阵</returns>
        public Matrix Dactive(Matrix martix) => new Matrix(Dactive(martix.InnerMatrix));


        /// <summary>
        /// 对矩阵的激活函数
        /// </summary>
        /// <param name="martix"></param>
        /// <returns></returns>
        public decimal[,] Active(decimal[,] martix)
        {
            decimal[,] ret = new decimal[martix.GetLength(0), martix.GetLength(1)];
            for (int i = 0, len_i = martix.GetLength(0); i < len_i; i++)
                for (int j = 0, len_j = martix.GetLength(1); j < len_j; j++)
                    ret[i, j] = Active(martix[i, j]);
            return ret;

        }

        /// <summary>
        /// 对矩阵的激活函数
        /// </summary>
        /// <param name="martix"></param>
        /// <returns></returns>
        public Matrix Active(Matrix martix) => new Matrix(Active(martix.InnerMatrix));
    }
}
