using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper.Helper
{
    /// <summary>
    /// 稀疏矩阵的元素
    /// </summary>
    [Serializable]
    public class SparseMatrixCell
    {
        /// <summary>
        /// 行索引
        /// </summary>
        public int RowIndex { set; get; }

        /// <summary>
        /// 列索引
        /// </summary>
        public int ColIndex { set; get; }

        /// <summary>
        /// 元素值
        /// </summary>
        public decimal Val { set; get; }

        /// <summary>
        /// 结构体的下面的值
        /// </summary>
        public SparseMatrixCell Down { set; get; }

        /// <summary>
        /// 结构体的上面的值
        /// </summary>
        public SparseMatrixCell Up { set; get; }

        /// <summary>
        /// 结构体的下面的值
        /// </summary>
        public SparseMatrixCell Left { set; get; }

        /// <summary>
        /// 结构体的下面的值
        /// </summary>
        public SparseMatrixCell Right { set; get; }
    }

    /// <summary>
    /// 稀疏矩阵类，功能和普通矩阵一样，例如矩阵A：
    /// A = 1,2,3   A[0,0]=1
    ///     4,5,6   A[1,2]=6
    ///     7,8,9   A[2,0]=7
    /// </summary>
    [Serializable]
    public class SparseMatrix : ICloneable
    {
        SparseArray<SparseMatrixCell> _colHead;
        SparseArray<SparseMatrixCell> _rowHead;
        SparseArray<SparseMatrixCell> _colTail;
        SparseArray<SparseMatrixCell> _rowTail;


        /// <summary>
        /// 行数
        /// </summary>
        public int RowCount { private set; get; }

        /// <summary>
        /// 列数
        /// </summary>
        public int ColCount { private set; get; }

        /// <summary>
        /// 把值追加到指定位置上。
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="colIndex"></param>
        /// <param name="val"></param>
        private void AddTo(int rowIndex, int colIndex, decimal val)
        {
            if (val == 0)
            {
                return;
            }
            if (colIndex < -1 || rowIndex < -1 || colIndex >= ColCount || rowIndex >= RowCount)
            {
                throw new Exception($"索引错误，矩阵的大小是：{RowCount}x{ColCount}");
            }
            SparseMatrixCell setVal = new SparseMatrixCell
            {
                Val = val,
                ColIndex = colIndex,
                RowIndex = rowIndex
            };
            SparseMatrixCell temp = _colHead[colIndex];
            while (true)
            {
                if (temp == null)
                {
                    _colHead[colIndex] = setVal;
                }
                else if (temp.RowIndex == rowIndex)
                {
                    temp.Val += val;
                    return;
                }
                else if (temp.RowIndex < rowIndex)
                {
                    if (temp.Down == null)
                    {
                        temp.Down = setVal;
                        setVal.Up = temp;
                    }
                    else
                    {
                        temp = temp.Down;
                        continue;
                    }
                }
                else if (temp.RowIndex < rowIndex && temp.Down.RowIndex > rowIndex)
                {
                    temp.Down.Up = setVal;
                    setVal.Down = temp.Down;
                    temp.Down = setVal;
                    setVal.Up = temp;
                }
                else
                {
                    continue;
                }
                break;
            }
            temp = _rowHead[rowIndex];
            while (true)
            {
                if (temp == null)
                {
                    _rowHead[rowIndex] = setVal;
                }
                else if (temp.ColIndex == colIndex)
                {
                    temp.Val += val;
                    return;
                }
                else if (temp.ColIndex < colIndex)
                {
                    if (temp.Right == null)
                    {
                        temp.Right = setVal;
                        setVal.Left = temp;
                    }
                    else
                    {
                        temp = temp.Right;
                        continue;
                    }
                }
                else if (temp.ColIndex < colIndex && temp.Right.ColIndex > colIndex)
                {
                    temp.Right.Left = setVal;
                    setVal.Right = temp.Right;
                    temp.Right = setVal;
                    setVal.Left = temp;
                }
                else
                {
                    continue;
                }
                break;
            }
        }

        /// <summary>
        /// 把数据追加到结尾
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="colIndex"></param>
        /// <param name="val"></param>
        void AppendToTail(int rowIndex, int colIndex, decimal val)
        {
            SparseMatrixCell cell=new SparseMatrixCell
            {
                ColIndex = colIndex,
                RowIndex = rowIndex,
                Val = val
            };
            SparseMatrixCell tempColTail=_colTail[colIndex];
            if (tempColTail == null)
            {
                _colHead[colIndex]=cell;
            }
            else
            {
                (cell.Up = tempColTail).Down = cell;
            }
            _colTail[colIndex] =cell;
            SparseMatrixCell tempRowTail = _rowTail[rowIndex];
            if (tempRowTail == null)
            {
                _rowHead[rowIndex]=cell;
            }
            else
            {
                (cell.Left = tempRowTail).Right = cell;
            }
            _rowTail[rowIndex]=cell;
        }

        /// <summary>
        /// 读取/写入稀疏矩阵指定行列的元素
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="colIndex"></param>
        /// <returns></returns>
        public decimal this[int rowIndex, int colIndex]
        {
            set
            {
                if (value == 0)
                {
                    return;
                }
                if (colIndex < -1 || rowIndex < -1 || colIndex >= ColCount || rowIndex >= RowCount)
                {
                    throw new Exception($"索引错误，矩阵的大小是：{RowCount}x{ColCount}");
                }
                SparseMatrixCell setVal = new SparseMatrixCell
                {
                    Val = value,
                    ColIndex = colIndex,
                    RowIndex = rowIndex
                };
                SparseMatrixCell tempHead = _colHead[colIndex];
                SparseMatrixCell tempTail;
                if (tempHead == null)
                {
                    _colHead[colIndex] = setVal;
                    _colTail[colIndex] = setVal;
                }
                else
                {
                    tempTail = _colTail[colIndex];
                }
                while (tempHead != null)
                {
                    if (tempHead.RowIndex == rowIndex)
                    {
                        tempHead.Val = value;
                        return;
                    }
                    else if (tempHead.RowIndex < rowIndex)
                    {
                        if (tempHead.Down == null)
                        {
                            tempHead.Down = setVal;
                            setVal.Up = tempHead;
                            _colTail[colIndex] = setVal;
                            break;
                        }
                        else
                        {
                            tempHead = tempHead.Down;
                        }
                    }
                    else if (tempHead.RowIndex > rowIndex)
                    {
                        if (tempHead.Up == null)
                        {
                            tempHead.Up = setVal;
                            setVal.Down = tempHead;
                            _colHead[colIndex] = setVal;
                        }
                        else
                        {
                            tempHead.Up.Down = setVal;
                            setVal.Up= tempHead.Up;
                            setVal.Down = tempHead;
                            tempHead.Up = setVal;
                        }
                        break;
                    }
                }
                tempHead = _rowHead[rowIndex];
                if (tempHead == null)
                {
                    _rowHead[rowIndex] = setVal;
                    _rowTail[rowIndex] = setVal;
                    return;
                }
                while (tempHead != null)
                {
                    if (tempHead.ColIndex == colIndex)
                    {
                        tempHead.Val = value;
                        return;
                    }
                    else if (tempHead.ColIndex < colIndex)
                    {
                        if (tempHead.Right == null)
                        {
                            tempHead.Right = setVal;
                            setVal.Left = tempHead;
                            _rowTail[rowIndex] = setVal;
                            return;
                        }
                        else
                        {
                            tempHead = tempHead.Right;
                        }
                    }
                    else if (tempHead.ColIndex > colIndex)
                    {
                        if (tempHead.Left==null)
                        {
                            tempHead.Left = setVal;
                            setVal.Right = tempHead;
                            _rowHead[rowIndex] = setVal;
                        }
                        else
                        {
                            tempHead.Left.Right = setVal;
                            setVal.Left = tempHead.Left;
                            setVal.Right = tempHead;
                            tempHead.Left = setVal;
                        }
                        return;
                    }
                }
            }
            get
            {
                if (colIndex < -1 || rowIndex < -1 || colIndex >= ColCount || rowIndex >= RowCount)
                {
                    throw new Exception($"索引错误，矩阵的大小是：{RowCount}x{ColCount}");
                }
                SparseMatrixCell lastHead;
                SparseMatrixCell lastTail;
                if (rowIndex > colIndex)
                {
                    lastHead = _rowHead[rowIndex];
                    if (lastHead == null)
                    {
                        return 0;
                    }
                    lastTail = _rowTail[rowIndex];
                    bool nearHead = (colIndex << 1) < lastTail.ColIndex + lastHead.ColIndex;
                    if (nearHead)
                    {
                        while (true)
                        {
                            if (lastHead.ColIndex == colIndex)
                            {
                                return lastHead.Val;
                            }
                            else if (lastHead.ColIndex < colIndex)
                            {
                                if (lastHead.Right == null)
                                {
                                    return 0;
                                }
                                else
                                {
                                    lastHead = lastHead.Right;
                                }
                            }
                            else if (lastHead.ColIndex > colIndex)
                            {
                                return 0;
                            }
                        }
                    }
                    else
                    {
                        while (true)
                        {
                            if (lastTail.ColIndex == colIndex)
                            {
                                return lastTail.Val;
                            }
                            else if (lastTail.ColIndex > colIndex)
                            {
                                if (lastTail.Left == null)
                                {
                                    return 0;
                                }
                                else
                                {
                                    lastTail = lastTail.Left;
                                }
                            }
                            else if (lastTail.ColIndex < colIndex)
                            {
                                return 0;
                            }
                        }
                    }
                }
                else
                {
                    lastHead = _colHead[colIndex];
                    if (lastHead == null)
                    {
                        return 0;
                    }
                    lastTail = _colTail[colIndex];
                    bool nearHead = (rowIndex << 1) < lastHead.RowIndex + lastTail.RowIndex ;
                    if (nearHead)
                    {
                        while (true)
                        {
                            if (lastHead.RowIndex == rowIndex)
                            {
                                return lastHead.Val;
                            }
                            else if (lastHead.RowIndex < rowIndex)
                            {
                                if (lastHead.Down == null)
                                {
                                    return 0;
                                }
                                else
                                {
                                    lastHead = lastHead.Down;
                                }
                            }
                            else if (lastHead.RowIndex > rowIndex)
                            {
                                return 0;
                            }
                        }
                    }
                    else
                    {
                        while (true)
                        {
                            if (lastTail.RowIndex == rowIndex)
                            {
                                return lastTail.Val;
                            }
                            else if (lastTail.RowIndex > rowIndex)
                            {
                                if (lastTail.Up == null)
                                {
                                    return 0;
                                }
                                else
                                {
                                    lastTail = lastTail.Up;
                                }
                            }
                            else if (lastTail.RowIndex < rowIndex)
                            {
                                return 0;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 初始化稀疏矩阵，创建rowCount行，colCount列的矩阵，所有元素都为0
        /// </summary>
        /// <param name="rowCount"></param>
        /// <param name="colCount"></param>
        public SparseMatrix(int rowCount, int colCount)
        {
            RowCount = rowCount;
            ColCount = colCount;
            _colHead = new SparseArray<SparseMatrixCell>(colCount, null);
            _rowHead = new SparseArray<SparseMatrixCell>(rowCount, null);
            _colTail = new SparseArray<SparseMatrixCell>(colCount, null);
            _rowTail = new SparseArray<SparseMatrixCell>(rowCount, null);
        }

        /// <summary>
        /// 矩阵转置
        /// </summary>
        /// <returns></returns>
        public SparseMatrix Turn()
        {
            SparseMatrix newMatrix = new SparseMatrix(ColCount, RowCount);
            newMatrix._colHead = _rowHead;
            newMatrix._rowHead = _colHead;
            newMatrix._colTail = _rowTail;
            newMatrix._rowTail = _colTail;
            newMatrix = (SparseMatrix)newMatrix.Clone();
            SparseMatrixCell current;
            SparseMatrixCell temp;
            foreach (SparseMatrixCell head in newMatrix._colHead)
            {
                current = head;
                while (current != null)
                {
                    current.ColIndex += current.RowIndex;
                    current.RowIndex = current.ColIndex - current.RowIndex;
                    current.ColIndex = current.ColIndex - current.RowIndex;
                    temp = current.Right;
                    current.Right = current.Down;
                    current.Down = temp;
                    current = temp;
                }
            }
            return newMatrix;
        }

        /// <summary>
        /// 矩阵对角化
        /// </summary>
        public SparseMatrix Diag()
        {
            if (ColCount != 1)
            {
                throw new Exception("只有向量才能对角化！");
            }
            SparseMatrix newMatrix = new SparseMatrix(RowCount, RowCount);
            SparseMatrixCell current = _colHead[0], temp;
            for (int i; current != null;)
            {
                i = current.RowIndex;
                newMatrix._colTail.AppendToTail(current, i);
                newMatrix._rowTail.AppendToTail(current, i);
                newMatrix._colHead.AppendToTail(current, i);
                newMatrix._rowHead.AppendToTail(current, i);
                current.ColIndex = i;
                temp = current;
                current = current.Down;
                temp.Down = temp.Up = temp.Left = temp.Right = null;
            }
            return newMatrix;
        }

        /// <summary>
        /// 矩阵加法
        /// </summary>
        /// <param name="matrix1"></param>
        /// <param name="matrix2"></param>
        /// <returns></returns>
        public static SparseMatrix operator +(SparseMatrix matrix1, SparseMatrix matrix2)
        {
            SparseMatrix newMatrix = (SparseMatrix)matrix1.Clone();
            SparseMatrixCell temp;
            foreach (SparseMatrixCell head in matrix2._colHead)
            {
                temp = head;
                while (temp != null)
                {
                    newMatrix.AddTo(temp.RowIndex, temp.ColIndex, temp.Val);
                    temp = temp.Down;
                }
            }
            return newMatrix;
        }

        /// <summary>
        /// 矩阵减法
        /// </summary>
        /// <param name="matrix1"></param>
        /// <param name="matrix2"></param>
        /// <returns></returns>
        public static SparseMatrix operator -(SparseMatrix matrix1, SparseMatrix matrix2)
        {
            SparseMatrix newMatrix = (SparseMatrix)matrix1.Clone();
            SparseMatrixCell temp;
            foreach (SparseMatrixCell head in matrix2._colHead)
            {
                temp = head;
                while (temp != null)
                {
                    newMatrix.AddTo(temp.RowIndex, temp.ColIndex, -temp.Val);
                    temp = temp.Down;
                }
            }
            return newMatrix;
        }

        /// <summary>
        /// 矩阵乘标量
        /// </summary>
        /// <param name="matrix1"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public static SparseMatrix operator *(SparseMatrix matrix, decimal scalar)
        {
            SparseMatrix newMatrix = (SparseMatrix)matrix.Clone();
            SparseArray<SparseMatrixCell> colHead = newMatrix._colHead;
            SparseMatrixCell tempCell;
            foreach (SparseMatrixCell head in colHead)
            {
                tempCell = head;
                while (tempCell != null)
                {
                    tempCell.Val *= scalar;
                    tempCell = tempCell.Down;
                }
            }
            return newMatrix;
        }

        /// <summary>
        /// 检测两个矩阵的矩阵乘法后得到的新矩阵在row和col
        /// 列的算式。
        /// </summary>
        /// <param name="matrix1"></param>
        /// <param name="matrix2"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public static string TestMatrixMultiMatrix(SparseMatrix matrix1, SparseMatrix matrix2, int row, int col)
        {
            decimal sumVal=0;
            SparseMatrixCell rowHead = matrix1._rowHead[row];
            SparseMatrixCell colHead = matrix2._colHead[col];
            StringBuilder sb = new StringBuilder();
            string connChar = "";
            while (rowHead != null && colHead != null)
            {
                if (rowHead.ColIndex == colHead.RowIndex)
                {
                    sb.Append(connChar).Append(rowHead.Val).Append("*").Append(colHead.Val);
                    connChar = "+";
                    sumVal += rowHead.Val * colHead.Val;
                    colHead = colHead.Down;
                    rowHead = rowHead.Right;
                }
                else if (rowHead.ColIndex > colHead.RowIndex)
                {
                    colHead = colHead.Down;
                }
                else if (rowHead.ColIndex < colHead.RowIndex)
                {
                    rowHead = rowHead.Right;
                }
            }
            sb.Append("=").Append(sumVal);
            return sb.ToString();
        }

        /// <summary>
        /// 标量乘矩阵
        /// </summary>
        /// <param name="scalar"></param>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static SparseMatrix operator *(decimal scalar, SparseMatrix matrix) => matrix * scalar;

        /// <summary>
        /// 矩阵乘矩阵
        /// </summary>
        /// <param name="matrix1"></param>
        /// <param name="matrix2"></param>
        /// <returns></returns>
        public static SparseMatrix operator *(SparseMatrix matrix1, SparseMatrix matrix2)
        {
            SparseArray<SparseMatrixCell> rowHead = matrix1._rowHead;
            SparseArray<SparseMatrixCell> colHead = matrix2._colHead;
            SparseMatrixCell currentRowCell;
            SparseMatrixCell currentColCell;
            SparseMatrix newMatrix = new SparseMatrix(matrix1.RowCount, matrix2.ColCount);
            decimal sumVal;
            foreach (SparseMatrixCell rHead in rowHead)
            {
                if (rHead != null)
                {
                    foreach (SparseMatrixCell cHead in colHead)
                    {
                        currentRowCell = rHead;
                        currentColCell = cHead;
                        sumVal = 0;
                        while (currentRowCell != null && currentColCell != null)
                        {
                            if (currentRowCell.ColIndex == currentColCell.RowIndex)
                            {
                                sumVal += currentRowCell.Val * currentColCell.Val;
                                currentColCell = currentColCell.Down;
                                currentRowCell = currentRowCell.Right;
                            }
                            else if (currentRowCell.ColIndex > currentColCell.RowIndex)
                            {
                                currentColCell = currentColCell.Down;
                            }
                            else if (currentRowCell.ColIndex < currentColCell.RowIndex)
                            {
                                currentRowCell = currentRowCell.Right;
                            }
                        }
                        if (sumVal != 0)
                        {
                            newMatrix.AppendToTail(rHead.RowIndex, cHead.ColIndex, sumVal);
                        }
                    }
                }
            }
            return newMatrix;
        }

        /// <summary>
        /// 矩阵的克罗内克积
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public SparseMatrix Kronecker(SparseMatrix matrix)
        {
            SparseMatrix newMatrix = new SparseMatrix(RowCount * matrix.RowCount, ColCount * matrix.ColCount);
            SparseMatrixCell m1Cell;
            SparseMatrixCell m2Cell;
            foreach (SparseMatrixCell cHead in _colHead)
            {
                m1Cell = cHead;
                while (m1Cell != null)
                {
                    foreach (SparseMatrixCell mcHead in matrix._colHead)
                    {
                        m2Cell = mcHead;
                        while (m2Cell != null)
                        {
                            int rowIndex = (m1Cell.RowIndex * matrix.RowCount) + m2Cell.RowIndex;
                            int colIndex = (m1Cell.ColIndex * matrix.ColCount) + m2Cell.ColIndex;
                            newMatrix.AppendToTail(rowIndex, colIndex, m1Cell.Val * m2Cell.Val);
                            m2Cell = m2Cell.Down;
                        }
                    }
                    m1Cell = m1Cell.Down;
                }
            }
            return newMatrix;
        }

        /// <summary>
        /// 返回修改权值的矩阵
        /// </summary>
        /// <param name="rowCount">矩阵行数</param>
        /// <returns></returns>
        public decimal[,] ChangeWeight(int rowCount)
        {
            decimal[,] ret = new decimal[rowCount, RowCount / rowCount];
            SparseMatrixCell current;
            foreach (SparseMatrixCell head in _colHead)
            {
                current = head;
                while (current != null)
                {
                    ret[current.RowIndex % rowCount, (current.RowIndex - current.RowIndex % rowCount) / rowCount] = current.Val;
                    current = current.Down;
                }
            }
            return ret;
        }

        /// <summary>
        /// 矩阵向量化
        /// </summary>
        public SparseMatrix Vec()
        {
            if (ColCount == 1)
            {
                return (SparseMatrix)Clone();
            }
            SparseMatrix newMatrix = new SparseMatrix(ColCount * RowCount, 1);
            //find=true（寻找头部）、find=false（寻找尾部）
            SparseMatrixCell newHead = null;
            SparseMatrixCell tempTail = null;
            IEnumerator ie = _colHead.GetEnumerator();
            for (; ie.MoveNext();)
            {
                newHead = (SparseMatrixCell)ie.Current;
                if (newHead != null)
                {
                    tempTail = _colTail[newHead.ColIndex];
                    break;
                }
            }
            if (newHead != null)
            {
                SparseMatrixCell tempHead = null;
                for (; ie.MoveNext();)
                {
                    tempHead = (SparseMatrixCell)ie.Current;
                    if (tempHead != null)
                    {
                        tempTail.Down = tempHead;
                        tempHead.Up = tempTail;
                        tempTail = _colTail[tempHead.ColIndex];
                    }
                }
            }
            newMatrix._colHead[0] = newHead;
            SparseMatrixCell newTail = newHead;
            while (true)
            {
                newTail.RowIndex = RowCount * newTail.ColIndex + newTail.RowIndex;
                newTail.ColIndex = 0;
                newTail.Right = newTail.Left = null;
                newMatrix._rowTail.AppendToTail(newTail, newTail.RowIndex);
                newMatrix._rowHead.AppendToTail(newTail, newTail.RowIndex);
                if (newTail.Down == null)
                {
                    break;
                }
                else
                {
                    newTail = newTail.Down;
                }
            } 
            newMatrix._colTail[0] = newTail;
            return newMatrix;
        }

        /// <summary>
        /// 这是深克隆，慎用，可能会占用大量内存
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            object obj = null;
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (MemoryStream inputStream = new MemoryStream())
            {
                binaryFormatter.Serialize(inputStream, this);
                using (MemoryStream outputStream = new MemoryStream(inputStream.ToArray()))
                {
                    obj = binaryFormatter.Deserialize(outputStream);
                }
            }
            return obj;
        }

        /// <summary>
        /// 初始化稀疏矩阵，矩阵大小和内容等效于matrix
        /// </summary>
        /// <param name="matrix"></param>
        public SparseMatrix(Matrix matrix) : this(matrix.InnerMatrix)
        {

        }

        /// <summary>
        /// 初始化稀疏的单位方阵，
        /// </summary>
        /// <param name="width"></param>
        public SparseMatrix(int width) : this(width, width)
        {
            for (int i = 0; i < width; i++)
            {
                _colHead[i] = _colTail[i] = _rowHead[i] = _rowTail[i] = new SparseMatrixCell
                {
                    ColIndex = i,
                    RowIndex = i,
                    Val = 1
                };
            }
        }

        /// <summary>
        /// 初始化稀疏矩阵，矩阵大小和内容等效于matrix
        /// </summary>
        /// <param name="matrix"></param>
        public SparseMatrix(decimal[,] matrix) : this(matrix.GetLength(0), matrix.GetLength(1))
        {
            SparseMatrixCell sparseMatrixCell;
            for (int i = 0, len_i = matrix.GetLength(0); i < len_i; i++)
            {
                for (int j = 0, len_j = matrix.GetLength(1); j < len_j; j++)
                {
                    if (matrix[i, j] != 0)
                    {
                        sparseMatrixCell = new SparseMatrixCell
                        {
                            RowIndex = i,
                            ColIndex = j,
                            Val = matrix[i, j]
                        };
                        if (_colHead[j] == null || _colHead[j].Val == 0)
                        {
                            _colHead[j] = sparseMatrixCell;
                        }
                        else
                        {
                            _colTail[j].Down = sparseMatrixCell;
                            sparseMatrixCell.Up = _colTail[j];
                        }
                        if (_rowHead[i] == null || _rowHead[i].Val == 0)
                        {
                            _rowHead[i] = sparseMatrixCell;
                        }
                        else
                        {
                            _rowTail[i].Right = sparseMatrixCell;
                            sparseMatrixCell.Left = _rowTail[i];
                        }
                        _colTail[j] = sparseMatrixCell;
                        _rowTail[i] = sparseMatrixCell;
                    }
                }
            }
        }
    }
}