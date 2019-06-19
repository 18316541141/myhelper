using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper.Helper
{
    [Serializable]
    public class SparseArrayCell<T>
    {
        public int Index { set; get; }
        public T Val { set; get; }
        public SparseArrayCell<T> Prev { set; get; }
        public SparseArrayCell<T> Next { set; get; }
    }

    /// <summary>
    /// 稀疏数组
    /// </summary>
    [Serializable]
    public class SparseArray<T>: IEnumerable
    {
        SparseArrayCell<T> _head;
        SparseArrayCell<T> _tail;

        /// <summary>
        /// map缓存，加快寻找数据的速度。
        /// </summary>
        Dictionary<int, T> _indexMap;

        T _notSetVal;

        public SparseArray(int len,T notSetVal)
        {
            Length = len;
            _notSetVal = notSetVal;
            _indexMap = new Dictionary<int, T>();
        }

        /// <summary>
        /// 稀疏数组长度
        /// </summary>
        public int Length { private set; get; }

        /// <summary>
        /// 把元素追加到尾部
        /// </summary>
        /// <param name="val">数据</param>
        /// <param name="index">下标</param>
        public void AppendToTail(T val,int index)
        {
            SparseArrayCell<T> newCell= new SparseArrayCell<T>
            {
                Val = val,
                Index = index,
                Prev = _tail
            };
            if (_tail == null)
            {
                _head=newCell;
            }
            else
            {
                _tail.Next = newCell;
            }
            _tail = newCell;
        }

        /// <summary>
        /// 读写稀疏数组的值
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T this[int index]
        {
            set
            {
                if (value==null || value.Equals(_notSetVal))
                {
                    return;
                }
                if (index >= Length || index<0)
                {
                    throw new Exception($"稀疏数组长度为{Length}，超出范围。");
                }
                SparseArrayCell<T> newCell = new SparseArrayCell<T>
                {
                    Index = index,
                    Val = value,
                };
                _indexMap[index]=value;
                SparseArrayCell<T> current = _head;
                if (current==null)
                {
                    _tail = _head = new SparseArrayCell<T> { Index=index,Val=value};
                    return;
                }
                bool nearHead=(index << 1) < _tail.Index + _head.Index;
                if (nearHead)
                {
                    current = _head;
                    while (true)
                    {
                        if (current.Index == index)
                        {
                            current.Val = value;
                            return;
                        }
                        else if (current.Index < index)
                        {
                            if (current.Next == null)
                            {
                                newCell.Prev = current;
                                _tail = current.Next = newCell;
                                return;
                            }
                            else
                            {
                                current = current.Next;
                            }
                        }
                        else if (current.Index > index)
                        {
                            if (current.Prev == null)
                            {
                                newCell.Next = current;
                                _head = current.Prev = newCell;
                            }
                            else
                            {
                                newCell.Prev = current.Prev;
                                newCell.Next = current;
                                current.Prev = current.Prev.Next = newCell;
                            }
                            return;
                        }
                    }
                }
                else
                {
                    current = _tail;
                    while (true)
                    {
                        if (current.Index == index)
                        {
                            current.Val = value;
                            return;
                        }
                        else if (current.Index > index)
                        {
                            if (current.Prev == null)
                            {
                                newCell.Next = current;
                                _head = current.Prev = newCell;
                                return;
                            }
                            else
                            {
                                current = current.Prev;
                            }
                        }
                        else if (current.Index < index)
                        {
                            if (current.Next == null)
                            {
                                newCell.Prev = current;
                                _tail = current.Next = newCell;
                            }
                            else
                            {
                                newCell.Prev = current;
                                newCell.Next = current.Next;
                                current.Next = current.Next.Prev = newCell;
                            }
                            return;
                        }
                    }
                }
            }
            get
            {
                if (index >= Length || index < 0)
                {
                    throw new Exception($"稀疏数组长度为{Length}，超出范围。");
                }
                T val;
                _indexMap.TryGetValue(index, out val);
                return val;
                //SparseArrayCell<T> current = _head;
                //if (current == null)
                //{
                //    return _notSetVal;
                //}
                //bool nearHead = (index << 1) < _tail.Index + _head.Index;
                //if (nearHead)
                //{
                //    current = _head;
                //    while (true)
                //    {
                //        if (current.Index == index)
                //        {
                //            return current.Val;
                //        }
                //        else if (current.Index< index)
                //        {
                //            current=current.Next;
                //        }
                //        else if (current.Index > index)
                //        {
                //            return _notSetVal;
                //        }
                //    }
                //}
                //else
                //{
                //    current = _tail;
                //    while (true)
                //    {
                //        if (current.Index == index)
                //        {
                //            return current.Val;
                //        }
                //        else if (current.Index > index)
                //        {
                //            current = current.Prev;
                //        }
                //        else if (current.Index < index)
                //        {
                //            return _notSetVal;
                //        }
                //    }
                //}
            }
        }

        /// <summary>
        /// 迭代器
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            SparseArrayCell<T> current= _head;
            if (current == null)
            {
                yield return _notSetVal;
            }
            else
            {
                do
                {
                    yield return current.Val;
                }
                while ((current = current.Next) != null);
            }
        }
    }
}
