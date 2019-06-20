using CommonHelper.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
namespace PA_Robot
{
    static class Program
    {

        [DllImport("kernel32.dll")]
        public static extern Boolean AllocConsole();

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            WebProjectHelper.RefreshVersion(@"E:\git_projects\myhelper\c#\web\WebApplication1\");
            //Random r = new Random();
            //SparseArray<int> sa = new SparseArray<int>(10000, 0);
            //int[] ia = new int[10000];
            //int rIndex;
            //for (int i = 0; i < 10000; i++)
            //{
            //    rIndex = r.Next(10000);
            //    ia[rIndex] = sa[rIndex] = r.Next(int.MinValue, int.MaxValue);
            //}
            //for (int i = 0; i < 10000; i++)
            //{
            //    if (ia[i] != sa[i])
            //    {
            //        Console.WriteLine();
            //    }
            //}

            //SparseMatrix sm = new SparseMatrix(10, 10);
            //decimal[,] dm = new decimal[10, 10];
            //int row, col;
            //decimal val;
            //for (int i = 0; i < 10; i++)
            //{
            //    val = new decimal(r.NextDouble());
            //    row = r.Next(10);
            //    col = r.Next(10);
            //    dm[row, col] = sm[row, col] = val;
            //}
            //sm = sm.Kronecker((SparseMatrix)sm.Clone());
            //dm = MatrixHelper.MatrixKronecker(dm,dm);
            //for (int i = 0; i < 100; i++)
            //{
            //    for (int j = 0; j < 100; j++)
            //    {
            //        if (sm[i, j] != dm[i, j])
            //        {
            //            Console.WriteLine();
            //            decimal d = sm[i, j];
            //        }
            //    }
            //}

            //SparseMatrix sm1 = new SparseMatrix(1000, 800);
            //SparseMatrix sm2 = new SparseMatrix(800, 1000);
            //decimal[,] dm1 = new decimal[1000, 800];
            //decimal[,] dm2 = new decimal[800, 1000];
            //int ranRow1, ranCol1, ranRow2, ranCol2;
            //for (int i = 0; i < 8000; i++)
            //{
            //    ranRow1 = r.Next(1000);
            //    ranCol1 = r.Next(800);
            //    ranRow2 = r.Next(800);
            //    ranCol2 = r.Next(1000);
            //    dm1[ranRow1, ranCol1] = sm1[ranRow1, ranCol1] = new decimal(r.NextDouble());
            //    dm2[ranRow2, ranCol2] = sm2[ranRow2, ranCol2] = new decimal(r.NextDouble());
            //}
            //SparseMatrix sm3 = sm1 * sm2;
            //decimal[,] dm3 = MatrixHelper.MatrixMultiMatrix(dm1, dm2);
            //for (int i = 0; i < 1000; i++)
            //{
            //    for (int j = 0; j < 1000; j++)
            //    {
            //        if (dm3[i, j] != sm3[i, j])
            //        {
            //            string f1 = SparseMatrix.TestMatrixMultiMatrix(sm1, sm2, i, j);
            //            string f2 = MatrixHelper.TestMatrixMultiMatrix(dm1, dm2, i, j);
            //            sm3 = sm1 * sm2;
            //            Console.WriteLine();
            //        }
            //    }
            //}
            //DataImageConvertHelper.TextToBarCodeImg("6070003452469",200,100).Save(@"C:\Users\Administrator\Desktop\差别\adasd.png");
            //Console.WriteLine();
        }
    }
}