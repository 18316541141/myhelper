using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper.Helper
{
    /// <summary>
    /// 含有分隔符的字符串帮助类，该类不是线程安全的。
    /// </summary>
    public class SplitStrHelper
    {
        /// <summary>
        /// 拼接字符串的数组
        /// </summary>
        List<string> _SplitStrArray;

        /// <summary>
        /// 获取连接串的字符串数量
        /// </summary>
        public int Length
        {
            get
            {
                return _SplitStrArray.Count;
            }
        }

        /// <summary>
        /// 获取拼接字符串的数组的副本
        /// </summary>
        public List<string> SplitStrArray
        {
            get
            {
                List<string> ret = new List<string>();
                for (int i = 0, len = _SplitStrArray.Count; i < len; i++)
                {
                    ret.Add(_SplitStrArray[i]);
                }
                return ret;
            }
        }

        /// <summary>
        /// 连接字符
        /// </summary>
        string _ConnChar;

        /// <summary>
        /// 含有分隔符的字符串
        /// </summary>
        string _SplitStr;

        /// <summary>
        /// 含有分隔符的字符串
        /// </summary>
        public string SplitStr
        {
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _SplitStrArray = new List<string>();
                }
                else
                {
                    _SplitStrArray = new List<string>(value.Split(new string[] { _ConnChar }, StringSplitOptions.RemoveEmptyEntries));
                }
                _SplitStr = value;
            }
            get
            {
                return _SplitStr;
            }
        }

        /// <summary>
        /// 含有分隔符的字符串帮助类，用于统一处理类似：'aaa、bbb、ccc' 或 'ααα|βββ|γγγ'
        /// 之类的字符串。
        /// </summary>
        /// <param name="connChar">连接字符</param>
        /// <param name="splitStr">初始字符串</param>
        public SplitStrHelper(string connChar, string splitStr)
        {
            if (string.IsNullOrEmpty(connChar))
            {
                throw new Exception("请使用正确的连接字符");
            }
            if (string.IsNullOrEmpty(splitStr) || connChar == splitStr)
            {
                _SplitStrArray = new List<string>();
            }
            else
            {
                _SplitStrArray = new List<string>(splitStr.Split(new string[] { connChar }, StringSplitOptions.RemoveEmptyEntries));
            }
            _ConnChar = connChar;
            _SplitStr = splitStr;
        }

        /// <summary>
        /// 在结尾追加字符串
        /// </summary>
        /// <param name="str">追加的字符串</param>
        /// <returns></returns>
        public SplitStrHelper Add(string str)
        {
            _SplitStrArray.Add(str);
            _SplitStr = string.Join(_ConnChar, _SplitStrArray);
            return this;
        }

        /// <summary>
        /// 在特定位置插入字符串
        /// </summary>
        /// <param name="index">插入的索引位置</param>
        /// <param name="str">插入的字符串</param>
        /// <returns></returns>
        public SplitStrHelper Insert(int index, string str)
        {
            _SplitStrArray.Insert(index, str);
            _SplitStr = string.Join(_ConnChar, _SplitStrArray);
            return this;
        }



        /// <summary>
        /// 按照下标删除字符串
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public SplitStrHelper Del(int index)
        {
            _SplitStrArray.RemoveAt(index);
            _SplitStr = string.Join(_ConnChar, _SplitStrArray);
            return this;
        }

        /// <summary>
        /// 根据字符串删除字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public SplitStrHelper Del(string str)
        {
            _SplitStrArray.Remove(str);
            _SplitStr = string.Join(_ConnChar, _SplitStrArray);
            return this;
        }



        /// <summary>
        /// 去除重复项
        /// </summary>
        public SplitStrHelper RemoveRepeat()
        {
            List<int> repeatIndexArray = new List<int>();
            for (int i = 0, len_i = _SplitStrArray.Count - 1; i < len_i; i++)
            {
                for (int j = i + 1, len_j = _SplitStrArray.Count; j < len_j; j++)
                {
                    if (_SplitStrArray[i] == _SplitStrArray[j])
                    {
                        repeatIndexArray.Add(j);
                    }
                }
            }
            repeatIndexArray.Sort((a, b) => b - a);
            foreach (int index in repeatIndexArray)
            {
                _SplitStrArray.RemoveAt(index);
            }
            _SplitStr = string.Join(_ConnChar, _SplitStrArray);
            return this;
        }

        /// <summary>
        /// 对每一个分割字符去除前后空格操作
        /// </summary>
        public SplitStrHelper Trim()
        {
            for (int i = 0, len_i = _SplitStrArray.Count; i < len_i; i++)
            {
                _SplitStrArray[i] = _SplitStrArray[i].Trim();
            }
            _SplitStr = string.Join(_ConnChar, _SplitStrArray);
            return this;
        }
    }
}