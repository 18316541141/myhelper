using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper.Helper
{
    /// <summary>
    /// 物理帮助类
    /// </summary>
    public static class PhysicsHelper
    {
        /// <summary>
        /// 在Firefox浏览器模拟人手拖拉拼图验证码
        /// </summary>
        /// <param name="totalDistance">总距离</param>
        /// <param name="time">总时长</param>
        /// <param name="part">分多次完成</param>
        /// <returns>返回一个集合，集合的每个元素是一个长度为2的数组，数组第一个元素是移动距离，第二个元素是多少毫秒后移动</returns>
        public static List<int> HandMoveDistanceByFirefox(int totalDistance, int addRange)
        {
            Random random = new Random();
            List<int> retList=new List<int>();
            int beforeDistance = (int)(totalDistance*.7*random.NextDouble());
            int movDistance = 0;
            int sum = 0;
            for (int i = addRange; sum <= beforeDistance;) {
                movDistance += i;
                sum += movDistance;
                retList.Add(movDistance);
            }
            int afterDistance = totalDistance - sum;
            sum = 0;
            for (int i = addRange; sum <=afterDistance;)
            {
                if(movDistance-i>0)
                    movDistance -= i;
                sum += movDistance;
                retList.Add(movDistance);
            }
            retList[retList.Count - 1] += afterDistance - sum;
            return retList;
        }

        /// <summary>
        /// 在Chrome浏览器模拟人手拖拉拼图验证码
        /// </summary>
        /// <param name="totalDistance">总距离</param>
        /// <param name="time">总时长</param>
        /// <param name="part">分多次完成</param>
        /// <returns>返回一个集合，集合的每个元素是一个长度为2的数组，数组第一个元素是移动距离，第二个元素是多少毫秒后移动</returns>
        public static List<int[]> HandMoveDistanceByChrome(int totalDistance, double time,int part=100)
        {
            List<int[]> retList = new List<int[]>();
            Random random = new Random();
            time = time + random.NextDouble()-.5;
            double randomIndex=random.NextDouble();
            int beforeDistance = (int)(totalDistance * .6* randomIndex);
            double beforeTime=time * .6 * randomIndex;
            int beforePart=(int)(part * .6 * randomIndex);
            retList.AddRange(ConstantAccelerationDistanceChange(beforeDistance, beforeTime, beforePart));

            int afterDistance = totalDistance - beforeDistance;
            double afterTime = time - beforeTime;
            int afterPart = part - beforePart;
            retList.AddRange(ConstantDecelerationDistanceChange(afterDistance, afterTime, afterPart));
            return retList;
        }

        /// <summary>
        /// 使用牛顿第二定律计算物体做匀加速直线运动时每次移动的距离
        /// </summary>
        /// <param name="time">运动到指定位置所需时间（单位是秒）</param>
        /// <param name="totalDistance">总距离</param>
        /// <param name="part">分多次完成</param>
        /// <returns>返回每次移动的距离（相对上一次）和移动该距离所需时间（毫秒）</returns>
        public static List<int[]> ConstantAccelerationDistanceChange(int totalDistance, double time, int part = 100)
        {
            List<int[]> lenList = ConstantAccelerationMoveDistance(totalDistance, time, part);
            for (int i = part - 1; i > 0; i--)
                lenList[i][0] = lenList[i][0] - lenList[i - 1][0];
            return lenList;
        }

        /// <summary>
        /// 使用牛顿第二定律计算物体做匀减速直线运动时每次移动的距离
        /// </summary>
        /// <param name="time">运动到指定位置所需时间（单位是秒）</param>
        /// <param name="totalDistance">总距离</param>
        /// <param name="part">分多次完成</param>
        /// <returns>返回每次移动的距离（相对上一次）以及移动该距离的所需时间（毫秒）</returns>
        public static List<int[]> ConstantDecelerationDistanceChange(int totalDistance, double time, int part = 100)
        {
            List<int[]> lenList = ConstantAccelerationDistanceChange(totalDistance, time, part);
            lenList.Reverse();
            return lenList;
        }

        /// <summary>
        /// 使用牛顿第二定律计算物体做匀加速直线运动时每个时间点移动的距离
        /// </summary>
        /// <param name="time">运动到指定位置所需时间（单位是秒）</param>
        /// <param name="totalDistance">总距离</param>
        /// <param name="part">分多次完成</param>
        /// <returns>返回每个时间点移动的距离以及移动该距离的所需时间（毫秒）</returns>
        public static List<int[]> ConstantAccelerationMoveDistance(int totalDistance, double time, int part = 100)
        {
            double acceleration = totalDistance * 2.0 / Math.Pow(time, 2);
            List<int[]> lenList = new List<int[]>();
            for (double i = 1, partLen = time / part; i <= part; i++)
            {
                if (i % 2 == 0)
                    lenList.Add(new int[] { (int)Math.Ceiling(Math.Pow(i * partLen, 2) * .5 * acceleration), (int)Math.Ceiling(partLen) });
                else
                    lenList.Add(new int[] { (int)Math.Floor(Math.Pow(i * partLen, 2) * .5 * acceleration), (int)Math.Floor(partLen) });
            }
            return lenList;
        }

        /// <summary>
        /// 使用牛顿第二定律计算物体做匀减速直线运动时每个时间点移动的距离
        /// </summary>
        /// <param name="time">运动到指定位置所需时间（单位是秒）</param>
        /// <param name="totalDistance">总距离</param>
        /// <param name="part">分多次完成</param>
        /// <returns>返回每个时间点移动的距离以及移动该距离的所需时间（毫秒）</returns>
        public static List<int[]> ConstantDecelerationMoveDistance(int totalDistance, double time, int part = 100)
        {
            List<int[]> lenList = ConstantAccelerationMoveDistance(totalDistance, time, part);
            lenList.Reverse();
            return lenList;
        }
    }
}
