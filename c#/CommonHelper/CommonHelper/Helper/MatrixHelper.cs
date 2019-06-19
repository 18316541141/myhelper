using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper.Helper
{
    /// <summary>
    /// 矩阵帮助类
    /// </summary>
    public static class MatrixHelper
    {
        /// <summary>
        /// 创建期望值矩阵
        /// </summary>
        /// <param name="typeArrays">期望值类型</param>
        /// <param name="totalTypeCount">所有情况的总数</param>
        /// <returns>返回期望值矩阵</returns>
        public static decimal[,] CreateExpectMatrix(int[] typeArrays,int totalTypeCount)
        {
            decimal[,] expectMatrix = new decimal[totalTypeCount, typeArrays.Length];
            for (int i=0,len= typeArrays.Length;i<len ;i++)
            {
                expectMatrix[typeArrays[i], i]=1;
            }
            return expectMatrix;
        }

        /// <summary>
        /// 矩阵乘标量
        /// </summary>
        /// <param name="Matrix"></param>
        /// <param name="Scalar"></param>
        /// <returns></returns>
        public static decimal[,] MatrixMultiScalar(this decimal[,] Matrix, decimal Scalar)
        {
            decimal[,] Ret = new decimal[Matrix.GetLength(0), Matrix.GetLength(1)];
            for (int i = 0, len_i = Matrix.GetLength(0); i < len_i; i++)
            {
                for (int j = 0, len_j = Matrix.GetLength(1); j < len_j; j++)
                    Ret[i, j] = Matrix[i, j] * Scalar;
            }
            return Ret;
        }

        /// <summary>
        /// 创建单位矩阵
        /// </summary>
        /// <param name="width">单位矩阵的宽度</param>
        /// <returns>返回单位矩阵</returns>
        public static decimal[,] CreateIdentityMatrix(int width)
        {
            decimal[,] identityMatrix = new decimal[width, width];
            for (int i=0;i<width;i++)
            {
                identityMatrix[i, i] = 1;
            }
            return identityMatrix;
        }

        /// <summary>
        /// 矩阵减矩阵
        /// </summary>
        /// <param name="Matrix1"></param>
        /// <param name="Matrix2"></param>
        /// <returns></returns>
        public static decimal[,] MatrixReduceMatrix(this decimal[,] Matrix1, decimal[,] Matrix2)
        {
            decimal[,] Ret = new decimal[Matrix1.GetLength(0), Matrix1.GetLength(1)];
            for (int i = 0, len_i = Matrix1.GetLength(0); i < len_i; i++)
            {
                for (int j = 0, len_j = Matrix1.GetLength(1); j < len_j; j++)
                    Ret[i, j] = Matrix1[i, j] - Matrix2[i, j];
            }
            return Ret;
        }

        /// <summary>
        /// 矩阵转置
        /// </summary>
        /// <param name="Matrix"></param>
        /// <returns></returns>
        public static decimal[,] Turn(this decimal[,] matrix)
        {
            decimal[,] ret = new decimal[matrix.GetLength(1), matrix.GetLength(0)];
            for (int i = 0, len_i = matrix.GetLength(1); i < len_i; i++)
            {
                for (int j = 0, len_j = matrix.GetLength(0); j < len_j; j++)
                    ret[i, j] = matrix[j, i];
            }
            return ret;
        }

        /// <summary>
        /// 对矩阵权值进行调整。
        /// </summary>
        /// <param name="matrix1">权值矩阵</param>
        /// <param name="matrix2">调整值</param>
        /// <returns></returns>
        public static decimal[,] MatrixChangeWeight(this decimal[,] matrix1, decimal[,] matrix2)
        {
            int rowCount=matrix1.GetLength(0);
            int colCount=matrix1.GetLength(1);
            decimal[,] ret = new decimal[rowCount, colCount];
            for (int i=0,y,x, len_i= matrix2.GetLength(0);i<len_i ;i++)
            {
                for (int j = 0, len_j = matrix2.GetLength(1); j < len_j; j++)
                {
                    y = i % rowCount;
                    x = (i - i % rowCount) / rowCount;
                    ret[y,x] = matrix1[y, x] + matrix2[i, j];
                }
            }
            return ret;
        }

        /// <summary>
        /// 矩阵加矩阵
        /// </summary>
        /// <param name="Matrix1"></param>
        /// <param name="Matrix2"></param>
        /// <returns></returns>
        public static decimal[,] MatrixAndMatrix(this decimal[,] Matrix1, decimal[,] Matrix2)
        {
            decimal[,] Ret = new decimal[Matrix1.GetLength(0), Matrix1.GetLength(1)];
            for (int i = 0, len_i = Matrix1.GetLength(0); i < len_i; i++)
            {
                for (int j = 0, len_j = Matrix1.GetLength(1); j < len_j; j++)
                    Ret[i, j] = Matrix1[i, j] + Matrix2[i, j];
            }
            return Ret;
        }

        /// <summary>
        /// 矩阵向量化
        /// </summary>
        /// <param name="matrix">传入需要向量化的矩阵</param>
        /// <returns>返回向量</returns>
        public static decimal[,] MatrixVec(this decimal[,] matrix)
        {
            decimal[,] ret = new decimal[matrix.GetLength(0)* matrix.GetLength(1), 1];
            for (int i=0,len_i=matrix.GetLength(1);i<len_i ;i++)
            {
                for (int j = 0, len_j = matrix.GetLength(0); j < len_j; j++)
                {
                    ret[i*len_j+j,0] = matrix[j, i];
                }
            }
            return ret;
        }

        /// <summary>
        /// 向量对角化
        /// </summary>
        /// <param name="vec">向量</param>
        /// <returns>返回对角化矩阵</returns>
        public static decimal[,] VecDiag(this decimal[,] vec)
        {
            decimal[,] diagMatrix = new decimal[vec.GetLength(0), vec.GetLength(0)];
            for (int i=0,len=vec.GetLength(0);i<len ;i++)
            {
                diagMatrix[i, i] = vec[i, 0];
            }
            return diagMatrix;
        }

        /// <summary>
        /// 矩阵表，把矩阵以表格形式输出
        /// </summary>
        /// <param name="matrix">矩阵</param>
        /// <param name="path">表格的输出路径</param>
        /// <returns></returns>
        public static void MatrixTable(decimal[,] matrix,string path)
        {
            StringBuilder table = new StringBuilder("<table><tbody>");
            for (int i = 0, len_i = matrix.GetLength(0); i < len_i; i++)
            {
                table.Append("<tr>");
                for (int j = 0, len_j = matrix.GetLength(1); j < len_j; j++)
                {
                    table.Append("<td>").Append(matrix[i,j]).Append("</td>");
                }
                table.Append("</tr>");
            }
            table.Append("</tbody></table>");
            File.WriteAllText(path, table.ToString());
        }

        /// <summary>
        /// 矩阵的克罗内克积
        /// </summary>
        /// <param name="matrix1">矩阵1</param>
        /// <param name="matrix2">矩阵2</param>
        /// <returns></returns>
        public static decimal[,] MatrixKronecker(this decimal[,] matrix1, decimal[,] matrix2)
        {
            decimal[,] ret = new decimal[matrix1.GetLength(0)* matrix2.GetLength(0), matrix1.GetLength(1)* matrix2.GetLength(1)];
            for (int i=0,len_i=matrix1.GetLength(0);i<len_i ;i++)
            {
                for (int j = 0, len_j = matrix1.GetLength(1); j < len_j; j++)
                {
                    for (int k = 0, len_k = matrix2.GetLength(0); k < len_k; k++)
                    {
                        for (int l = 0, len_l = matrix2.GetLength(1); l < len_l; l++)
                        {
                            ret[len_k * i + k, len_l * j + l] = matrix1[i, j] * matrix2[k, l];
                        }
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// 矩阵的逐元素相乘
        /// </summary>
        /// <param name="matrixs">多个行列数一样的矩阵</param>
        /// <returns>返回相乘后的矩阵</returns>
        public static decimal[,] MatrixDotMatrix(params decimal[][,] matrixs)
        {
            decimal[,] ret = new decimal[matrixs[0].GetLength(0), matrixs[0].GetLength(1)];
            for (int i = 0, len_i = matrixs[0].GetLength(0); i < len_i; i++)
                for (int j = 0, len_j = matrixs[0].GetLength(1); j < len_j; j++)
                {
                    decimal retVal=1;
                    for(int k=0,len_k=matrixs.Length;k<len_k ;k++)
                        retVal*=matrixs[k][i, j];
                    ret[i, j] = retVal;
                }
            return ret;
        }

        /// <summary>
        /// 检测两个矩阵的矩阵乘法后得到的新矩阵在row和col
        /// 列的算式。
        /// </summary>
        /// <param name="Matrix1"></param>
        /// <param name="Matrix2"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public static string TestMatrixMultiMatrix(this decimal[,] Matrix1, decimal[,] Matrix2,int row,int col)
        {
            StringBuilder sb = new StringBuilder();
            decimal Ret = 0;
            string conChar="";
            for (int k = 0, len_k = Matrix1.GetLength(1); k < len_k; k++)
            {
                sb.Append(conChar).Append(Matrix1[row, k]).Append("*").Append(Matrix2[k, col]);
                conChar = "+";
                Ret += Matrix1[row, k] * Matrix2[k, col];
            }
            sb.Append("=").Append(Ret);
            return sb.ToString();
        }

        /// <summary>
        /// 矩阵乘矩阵
        /// </summary>
        /// <param name="Matrix1"></param>
        /// <param name="Matrix2"></param>
        /// <returns></returns>
        public static decimal[,] MatrixMultiMatrix(this decimal[,] Matrix1, decimal[,] Matrix2)
        {
            decimal[,] Ret = new decimal[Matrix1.GetLength(0), Matrix2.GetLength(1)];
            for (int i = 0, len_i = Matrix1.GetLength(0); i < len_i; i++)
            {
                for (int j = 0, len_j = Matrix2.GetLength(1); j < len_j; j++)
                {
                    for (int k = 0, len_k = Matrix1.GetLength(1); k < len_k; k++)
                    {
                        Ret[i, j] += Matrix1[i, k] * Matrix2[k, j];
                    }
                }
            }
            return Ret;
        }
    }
}
