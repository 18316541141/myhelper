using System.Collections.Generic;
namespace CommonHelper.Helper
{
    /// <summary>
    /// 关联数据的帮助类
    /// </summary>
    public static class RelationDataHelper
    {
        /// <summary>
        /// 主表数据委托
        /// </summary>
        /// <typeparam name="M">主表类</typeparam>
        /// <typeparam name="C">主表的主键</typeparam>
        /// <param name="main">主表对象</param>
        /// <returns>返回主表的主键</returns>
        public delegate C MainData<M,C>(M main);

        /// <summary>
        /// 分表数据委托
        /// </summary>
        /// <typeparam name="O">分表类</typeparam>
        /// <typeparam name="C">分表关联主表的值</typeparam>
        /// <param name="other">分表对象</param>
        /// <returns>返回分表关联主表的值</returns>
        public delegate C OtherData<O,C>(O other);

        /// <summary>
        /// 关联数据委托
        /// </summary>
        /// <typeparam name="M">主表类</typeparam>
        /// <typeparam name="O">分表类</typeparam>
        /// <param name="main">主表对象</param>
        /// <param name="other">分表对象</param>
        public delegate void ConnectDataFunc<M, O>(M main, O other);

        /// <summary>
        /// 关联主表和分表的数据
        /// </summary>
        /// <typeparam name="M">主表类</typeparam>
        /// <typeparam name="O">分表类</typeparam>
        /// <typeparam name="C">关联值</typeparam>
        /// <param name="mainList">主表数据集合</param>
        /// <param name="otherList">分表数据集合</param>
        /// <param name="mainData">主表主键委托</param>
        /// <param name="otherData">分表关联值委托</param>
        /// <param name="connectDataFunc">关联委托</param>
        public static void RelationList<M,O,C>(List<M> mainList, MainData<M, C> mainData, List<O> otherList, OtherData<O, C> otherData, ConnectDataFunc<M,O> connectDataFunc)
        {
            if (mainList == null || mainList.Count == 0 || otherList == null || otherList.Count == 0)
                return;
            Dictionary<C, O> otherMap = new Dictionary<C, O>();
            foreach (O other in otherList)
            {
                otherMap.Add(otherData(other),other);
            }
            C connVal;
            foreach (M main in mainList)
            {
                connVal = mainData(main);
                if (otherMap.ContainsKey(connVal))
                {
                    connectDataFunc(main, otherMap[connVal]);
                }
            }
        }

        /// <summary>
        /// 多次关联主表和分表的数据
        /// </summary>
        /// <typeparam name="M">主表类</typeparam>
        /// <typeparam name="C">关联值</typeparam>
        /// <param name="mainList">主表数据集合</param>
        /// <param name="mainData">主表主键委托</param>
        /// <returns>返回多次关联管理对象</returns>
        public static MultiRelation<M, C> RelationList<M, C>(List<M> mainList, MainData<M, C> mainData)
        {
            return new MultiRelation<M, C>(mainList, mainData);
        }

        /// <summary>
        /// 多次关联管理对象
        /// </summary>
        /// <typeparam name="M">主表类</typeparam>
        /// <typeparam name="C">管理值</typeparam>
        public class MultiRelation<M,C>
        {

            List<M> mainList { set; get; }

            Dictionary<C,M> mainMap { set; get; }

            MainData<M, C> mainData { set; get; }

            public MultiRelation(List<M> mainList, MainData<M, C> mainData)
            {
                this.mainList = mainList;
                this.mainData = mainData;
                mainMap = new Dictionary<C, M>();
                foreach (M main in mainList)
                {
                    mainMap.Add(mainData(main), main);
                }
            }

            /// <summary>
            /// 关联方法，可多次使用，每次关联不同的分表。
            /// </summary>
            /// <typeparam name="O"></typeparam>
            /// <param name="otherList"></param>
            /// <param name="otherData"></param>
            /// <param name="connectDataFunc"></param>
            /// <returns></returns>
            public MultiRelation<M, C> Relation<O>(List<O> otherList, OtherData<O, C> otherData, ConnectDataFunc<M, O> connectDataFunc)
            {
                if (mainList == null || mainList.Count == 0 || otherList == null || otherList.Count == 0)
                    return this;
                C connVal;
                foreach (O other in otherList)
                {
                    connVal = otherData(other);
                    if (mainMap.ContainsKey(connVal))
                    {
                        connectDataFunc(mainMap[connVal], other);
                    }
                }
                return this;
            }
        }
    }
}
