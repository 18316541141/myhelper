using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
namespace CommonHelper.Helper
{
    public static class BigIntegerHelper
    {
        /// <summary>
        /// 检查该bigInteger的二进制第index位是否为1.
        /// </summary>
        /// <param name="bigInteger">被检查的bigInteger</param>
        /// <param name="index">二进制位置</param>
        /// <returns></returns>
        public static bool TestBit(this BigInteger bigInteger, int index) =>
            (bigInteger >> (index - 1) & 1) > 0;

        /// <summary>
        /// 设置bigInteger的二进制的第index位为status
        /// </summary>
        /// <param name="bigInteger">被设置的bigInteger</param>
        /// <param name="index">二进制位置</param>
        /// <param name="status">状态：true,false</param>
        /// <returns></returns>
        public static BigInteger SetBit(this BigInteger bigInteger, int index, bool status)
        {
            if (bigInteger.TestBit(index) != status)
                if (status) return bigInteger + (new BigInteger(1) << index - 1);
                else return bigInteger - (new BigInteger(1) << index - 1);
            return bigInteger;
        }
    }
}
