using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper.Helper
{
    /// <summary>
    /// BP算法帮助类
    /// </summary>
    public static class BpHelper
    {
        public static ActivateFunc DefaultActivateFunc { set; get; }

        static BpHelper()
        {
            DefaultActivateFunc = new Signmoid();
        }

        /// <summary>
        /// 把指定文件的内容，转化为权值矩阵列表
        /// </summary>
        /// <param name="filePath"></param>
        public static List<Matrix> ParseWeightMatrixList2(string filePath)
        {
            JArray jarray = JArray.Parse(File.ReadAllText(filePath));
            List<Matrix> ret = new List<Matrix>();
            decimal[,] matrix;
            foreach (JArray jmatrix in jarray)
            {
                matrix = new decimal[jmatrix.Count, ((JArray)jmatrix.First).Count];
                int i = 0,j;
                foreach (JArray jrow in jmatrix)
                {
                    j = 0;
                    foreach (JValue jcol in jrow)
                    {
                        matrix[i, j] = new Decimal(Convert.ToDouble(jcol));
                        j++;
                    }
                    i++;
                }
                ret.Add(new Matrix(matrix));
            }
            return ret;
        }


        /// <summary>
        /// 把指定文件的内容，转化为权值矩阵列表
        /// </summary>
        /// <param name="filePath"></param>
        public static List<decimal[,]> ParseWeightMatrixList(string filePath)
        {
            JArray jarray = JArray.Parse(File.ReadAllText(filePath));
            List<decimal[,]> ret = new List<decimal[,]>();
            decimal[,] matrix;
            foreach (JArray jmatrix in jarray)
            {
                matrix = new decimal[jmatrix.Count, ((JArray)jmatrix.First).Count];
                ret.Add(matrix);
                int i = 0,j=0;
                foreach (JArray jrow in jmatrix)
                {
                    foreach (JValue jcol in jrow)
                    {
                        matrix[i, j] = new Decimal(Convert.ToDouble(jcol));
                        j++;
                    }
                    i++;
                }

            }
            return ret;
        }

        /// <summary>
        /// 保存权值矩阵列表
        /// </summary>
        /// <param name="weightMatrixList">权值矩阵列表</param>
        /// <param name="filename">矩阵权值列表保存的路径，建议是json格式</param>
        public static void SaveWeightMatrixList(List<Matrix> weightMatrixList, string filename)
        {
            SaveWeightMatrixList(weightMatrixList, File.Open(filename,FileMode.Create));
        }

        /// <summary>
        /// 保存权值矩阵列表
        /// </summary>
        /// <param name="weightMatrixList">权值矩阵列表</param>
        /// <param name="filename">矩阵权值列表保存的路径，建议是json格式</param>
        public static void SaveWeightMatrixList(List<decimal[,]> weightMatrixList, string filename)
        {
            SaveWeightMatrixList(weightMatrixList,File.Open(filename, FileMode.Create));
        }

        /// <summary>
        /// 保存权值矩阵列表
        /// </summary>
        /// <param name="weightMatrixList">权值矩阵列表</param>
        /// <param name="outputStream">把矩阵列表输出到输出流</param>
        /// <param name="autoClose">是否自动关闭流，默认是</param>
        public static void SaveWeightMatrixList(List<Matrix> weightMatrixList, Stream outputStream, bool autoClose = true)
        {
            JArray jarray = new JArray();
            JArray martixJArray, colJArray;
            decimal[,] temp;
            foreach (Matrix weightMatrix in weightMatrixList)
            {
                temp = weightMatrix.InnerMatrix;
                jarray.Add(martixJArray = new JArray());
                for (int i = 0, len_i = temp.GetLength(0); i < len_i; i++)
                {
                    martixJArray.Add(colJArray = new JArray());
                    for (int j = 0, len_j = temp.GetLength(1); j < len_j; j++)
                        colJArray.Add(temp[i, j]);
                }
            }
            byte[] data = Encoding.UTF8.GetBytes(jarray.ToString());
            outputStream.Write(data, 0, data.Length);
            if (autoClose) using (outputStream) { }
        }

        /// <summary>
        /// 保存权值矩阵列表
        /// </summary>
        /// <param name="weightMatrixList">权值矩阵列表</param>
        /// <param name="outputStream">把矩阵列表输出到输出流</param>
        /// <param name="autoClose">是否自动关闭流，默认是</param>
        public static void SaveWeightMatrixList(List<decimal[,]> weightMatrixList,Stream outputStream,bool autoClose=true)
        {
            JArray jarray = new JArray();
            JArray martixJArray,colJArray;
            foreach (decimal[,] weightMatrix in weightMatrixList)
            {
                jarray.Add(martixJArray=new JArray());
                for (int i = 0, len_i = weightMatrix.GetLength(0); i < len_i; i++)
                {
                    martixJArray.Add(colJArray = new JArray());
                    for (int j = 0, len_j = weightMatrix.GetLength(1); j < len_j; j++)
                        colJArray.Add(weightMatrix[i,j]);
                }
            }
            byte[] data=Encoding.UTF8.GetBytes(jarray.ToString());
            outputStream.Write(data,0, data.Length);
            if(autoClose) using (outputStream) { }
        }

        public static List<Matrix> GenWeightMatrixList(params int[][] rowColInfos)
        {
            if (rowColInfos.Length < 1) throw new Exception("至少生成1个矩阵！");
            Random random = new Random();
            List<Matrix> ret = new List<Matrix>();
            decimal[,] temp;
            foreach (int[] rowColInfo in rowColInfos)
            {
                temp = new decimal[rowColInfo[0], rowColInfo[1]];
                for (int i = 0, len_i = temp.GetLength(0); i < len_i; i++)
                    for (int j = 0, len_j = temp.GetLength(1); j < len_j; j++)
                        temp[i, j] =new decimal(random.NextDouble());
                ret.Add(new Matrix(temp));
            }
            return ret;
        }

        /// <summary>
        /// 生成权值矩阵
        /// </summary>
        /// <param name="counts">
        ///     每个参数都是长度为2的数组，第一个数字是矩阵行数（变换后的列向量维度数），
        ///     第二个数字是矩阵的列数（当前层的特征向量的维度数）
        /// </param>
        /// <returns></returns>
        public static List<decimal[,]> GenWeightMatrixArrayList(params int[][] rowColInfos)
        {
            if (rowColInfos.Length < 2) throw new Exception("至少生成2个矩阵！");
            Random random = new Random();
            List<decimal[,]> ret = new List<decimal[,]>();
            decimal[,] temp;
            foreach (int[] rowColInfo in rowColInfos)
            {
                temp = new decimal[rowColInfo[0], rowColInfo[1]];
                for (int i = 0, len_i = temp.GetLength(0); i < len_i; i++)
                    for (int j = 0, len_j = temp.GetLength(1); j < len_j; j++)
                        temp[i, j] = new decimal(random.NextDouble());
                ret.Add(temp);
            }
            return ret;
        }

        public static int Classify(List<Matrix> weightMatrixList,Matrix vector)
        {
            foreach (Matrix weightMatrix in weightMatrixList)
                vector = DefaultActivateFunc.Active(weightMatrix * vector);
            int type = -1;
            int count = 0;
            for (int i = 0, len = vector.Row; i < len; i++)
                if (1M - vector.InnerMatrix[i, 0] <= .1M)
                {
                    type = i;
                    count++;
                }
            return count > 1 ? -1 : type;
        }

        /// <summary>
        /// 对特征向量进行分类
        /// </summary>
        /// <param name="weightMatrixList">权值矩阵列表，对样本进行线性变换</param>
        /// <param name="vector">特征列向量</param>
        /// <returns>返回分类结果，如果返回-1则表示分类失败！</returns>
        public static int Classify(List<decimal[,]> weightMatrixList, decimal[,] vector)
        {
            foreach (decimal[,] weightMatrix in weightMatrixList)
                vector= DefaultActivateFunc.Active(MatrixHelper.MatrixMultiMatrix(weightMatrix, vector));
            int type =-1;
            int count = 0;
            for (int i=0,len=vector.GetLength(0);i<len;i++)
                if (1M - vector[i, 0] <= .1M)
                {
                    type = i;
                    count++;
                }
            return count > 1?-1:type;
        }

        /// <summary>
        /// 训练方法
        /// </summary>
        /// <param name="weightMatrixList">权值矩阵列表，对样本进行线性变换</param>
        /// <param name="matrix">输入矩阵，每一列就是一个样本的特征向量</param>
        /// <param name="desiredMatrix">期望矩阵</param>
        /// <returns>返回训练后的权值矩阵</returns>
        public static List<Matrix> Train(List<Matrix> weightMatrixList, Matrix matrix, Matrix desiredMatrix)
        {
            decimal learnRate = new decimal(.11);
            List<Matrix> prevInput = new List<Matrix>();
            List<Matrix> prevWeight = new List<Matrix>();
            List<Matrix> prevOutput = new List<Matrix>();
            List<Matrix> prevActive = new List<Matrix>();
            Matrix output;
            foreach (Matrix weightMatrix in weightMatrixList)
            {
                prevWeight.Insert(0, weightMatrix);
                prevInput.Insert(0, matrix);
                output = weightMatrix * matrix;
                prevOutput.Insert(0, output);
                prevActive.Insert(0, matrix = DefaultActivateFunc.Active(output));
            }
            List<SparseMatrix> partialWeight = new List<SparseMatrix>();
            SparseMatrix backPro = new SparseMatrix((desiredMatrix - prevActive[0]).Vec()).Diag() * new SparseMatrix(-1 * DefaultActivateFunc.Dactive(prevOutput[0]).Vec()).Diag();
            partialWeight.Add((backPro * new SparseMatrix(prevInput[0]).Turn().Kronecker(new SparseMatrix(prevWeight[0].Row))).Turn());
            for (int i = 1, len = prevWeight.Count; i < len; i++)
            {
                backPro = backPro * new SparseMatrix(prevActive[i].Col).Kronecker(new SparseMatrix(prevWeight[i - 1])) * new SparseMatrix(DefaultActivateFunc.Dactive(prevOutput[i]).Vec()).Diag();
                partialWeight.Insert(0, (backPro * new SparseMatrix(prevInput[i].Turn()).Kronecker(new SparseMatrix(prevWeight[i].Row))).Turn());
            }
            for (int i = 0, len = weightMatrixList.Count; i < len; i++)
            {
                weightMatrixList[i] = weightMatrixList[i]+ new Matrix(partialWeight[i].ChangeWeight(weightMatrixList[i].Row).MatrixMultiScalar(learnRate));
            }
            return weightMatrixList;
        }

        /// <summary>
        /// 训练方法
        /// </summary>
        /// <param name="weightMatrixList">权值矩阵列表，对样本进行线性变换</param>
        /// <param name="matrix">输入矩阵，每一列就是一个样本的特征向量</param>
        /// <param name="desiredMatrix">期望矩阵</param>
        /// <returns>返回训练后的权值矩阵</returns>
        public static List<decimal[,]> Train(List<decimal[,]> weightMatrixList, decimal[,] matrix , decimal[,] desiredMatrix)
        {
            decimal learnRate = new decimal(.11);
            List<decimal[,]> prevInput = new List<decimal[,]>();
            List<decimal[,]> prevWeight = new List<decimal[,]>();
            List<decimal[,]> prevOutput = new List<decimal[,]>();
            List<decimal[,]> prevActive = new List<decimal[,]>();
            decimal[,] output;
            foreach (decimal[,] weightMatrix in weightMatrixList)
            {
                prevWeight.Insert(0, weightMatrix);
                prevInput.Insert(0, matrix);
                matrix = DefaultActivateFunc.Active(output = weightMatrix.MatrixMultiMatrix(matrix));
                prevOutput.Insert(0, output);
                prevActive.Insert(0, matrix);
            }
            decimal[,] backPro=desiredMatrix.MatrixReduceMatrix(prevActive[0]).MatrixVec().VecDiag().MatrixMultiMatrix(
                DefaultActivateFunc.Dactive(prevOutput[0]).MatrixVec().VecDiag().MatrixMultiScalar(-1));
            List<decimal[,]> partialWeight = new List<decimal[,]>();
            partialWeight.Insert(0,backPro.MatrixMultiMatrix(prevInput[0].Turn().MatrixKronecker(MatrixHelper.CreateIdentityMatrix(prevWeight[0].GetLength(0)))).Turn());
            for (int i=1,len= weightMatrixList.Count;i<len ;i++)
            {
                backPro = backPro.MatrixMultiMatrix(MatrixHelper.CreateIdentityMatrix(prevActive[i].GetLength(1)).MatrixKronecker(weightMatrixList[i - 1]))
                .MatrixMultiMatrix(DefaultActivateFunc.Dactive(prevActive[i]).MatrixVec().VecDiag());
                partialWeight.Insert(0, backPro.MatrixMultiMatrix(prevInput[i].Turn().MatrixKronecker(MatrixHelper.CreateIdentityMatrix(weightMatrixList[i].GetLength(0)))));
            }
            for(int i=0,len= weightMatrixList.Count;i<len;i++)
            {
                weightMatrixList[i] = weightMatrixList[i].MatrixAndMatrix(partialWeight[i].MatrixMultiScalar(learnRate));
            }
            return weightMatrixList;
        }
    }
}
