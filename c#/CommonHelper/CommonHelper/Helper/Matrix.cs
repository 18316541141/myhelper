using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper.Helper
{
    /// <summary>
    /// 矩阵
    /// </summary>
    public class Matrix
    {
        public int Row
        {
            get
            {
                return InnerMatrix.GetLength(0);
            }
        }

        public int Col
        {
            get
            {
                return InnerMatrix.GetLength(1);
            }
        }
        public decimal[,] InnerMatrix { set; get; }


        public Matrix(decimal[,] matrix)
        {
            InnerMatrix = matrix;
        }

        /// <summary>
        /// 创建单位矩阵
        /// </summary>
        /// <param name="width"></param>
        public Matrix(int width)
        {
            InnerMatrix = MatrixHelper.CreateIdentityMatrix(width);
        }

        /// <summary>
        /// 传入求导的矩阵结果，修改矩阵的全职
        /// </summary>
        /// <returns></returns>
        public Matrix ChangeWeight(Matrix matrix)
        {
            return new Matrix(InnerMatrix.MatrixChangeWeight(matrix.InnerMatrix));
        }

        /// <summary>
        /// 矩阵加法
        /// </summary>
        /// <param name="matrix1"></param>
        /// <param name="matrix2"></param>
        /// <returns></returns>
        public static Matrix operator +(Matrix matrix1, Matrix matrix2)
        {
            return new Matrix(matrix1.InnerMatrix.MatrixAndMatrix(matrix2.InnerMatrix));
        }

        /// <summary>
        /// 矩阵减法
        /// </summary>
        /// <param name="matrix1"></param>
        /// <param name="matrix2"></param>
        /// <returns></returns>
        public static Matrix operator -(Matrix matrix1, Matrix matrix2)
        {
            return new Matrix(matrix1.InnerMatrix.MatrixReduceMatrix(matrix2.InnerMatrix));
        }

        /// <summary>
        /// 矩阵乘法
        /// </summary>
        /// <param name="matrix1"></param>
        /// <param name="matrix2"></param>
        /// <returns></returns>
        public static Matrix operator *(Matrix matrix1, Matrix matrix2)
        {
            return new Matrix(matrix1.InnerMatrix.MatrixMultiMatrix(matrix2.InnerMatrix));
        }

        /// <summary>
        /// 矩阵乘标量
        /// </summary>
        /// <param name="matrix1"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public static Matrix operator *(Matrix matrix1, decimal scalar)
        {
            return new Matrix(matrix1.InnerMatrix.MatrixMultiScalar(scalar));
        }

        /// <summary>
        /// 标量乘矩阵
        /// </summary>
        /// <param name="matrix1"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public static Matrix operator *(decimal scalar, Matrix matrix1)
        {
            return new Matrix(matrix1.InnerMatrix.MatrixMultiScalar(scalar));
        }

        /// <summary>
        /// 矩阵转置
        /// </summary>
        /// <returns></returns>
        public Matrix Turn()
        {
            return new Matrix(InnerMatrix.Turn());
        }

        /// <summary>
        /// 逐元素乘法
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public Matrix Dot(Matrix matrix)
        {
            return new Matrix(MatrixHelper.MatrixDotMatrix(InnerMatrix, matrix.InnerMatrix));
        }

        /// <summary>
        /// 克罗内克积
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public Matrix Kronecker(Matrix matrix)
        {
            return new Matrix(MatrixHelper.MatrixKronecker(InnerMatrix, matrix.InnerMatrix));
        }

        /// <summary>
        /// 矩阵向量化
        /// </summary>
        /// <returns></returns>
        public Matrix Vec()
        {
            return new Matrix(InnerMatrix.MatrixVec());
        }

        /// <summary>
        /// 向量对角化
        /// </summary>
        /// <returns></returns>
        public Matrix Diag()
        {
            return new Matrix(InnerMatrix.VecDiag());
        }
    }
}
