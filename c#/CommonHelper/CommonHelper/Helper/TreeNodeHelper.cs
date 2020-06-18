using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper.Helper
{
    /// <summary>
    /// 树节点帮助类
    /// </summary>
    public class TreeNodeHelper<N,Key>
    {
        /// <summary>
        /// 无参构造方法
        /// </summary>
        public TreeNodeHelper()
        {

        }

        /// <summary>
        /// 传入自定义数节点构造方法
        /// </summary>
        /// <param name="TreeNode"></param>
        public TreeNodeHelper(TreeNode<N, Key> TreeNode)
        {
            this.TreeNode = TreeNode;
        }

        /// <summary>
        /// 树节点
        /// </summary>
        public TreeNode<N, Key> TreeNode { set; get; }

        /// <summary>
        /// 树排序
        /// </summary>
        /// <param name="treeList">树集合</param>
        public void TreeSort(ref List<N> treeList)
        {
            treeList.Sort(TreeNode);
            foreach (N node in treeList)
            {
                List<N> childTreeList = TreeNode.ChildList(node);
                if (childTreeList.Count > 0)
                {
                    TreeSort(ref childTreeList);
                }
            }
        }

        /// <summary>
        /// 把集合转化为树集合
        /// </summary>
        /// <param name="list">需要转化的集合</param>
        /// <param name="maxLevel">树集合的最大层级，默认不限制，全部转化</param>
        /// <returns></returns>
        public List<N> ListToTreeList(List<N> list,int maxLevel = int.MaxValue)
        {
            List<N> treeList = new List<N>();
            Dictionary<Key, int[]> indexMap = new Dictionary<Key, int[]>();
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (TreeNode.IsRoot(list[i]))
                {
                    treeList.Add(list[i]);
                    indexMap.Add(TreeNode.GetKey(list[i]),new int[] { treeList.Count - 1 });
                    list.RemoveAt(i);
                }
                else
                {
                    Key parentKey =TreeNode.GetParentKey(list[i]);
                    if (indexMap.ContainsKey(parentKey))
                    {
                        int[] indexs = indexMap[parentKey];
                        if(indexs.Length < maxLevel)
                        {
                            FindParentAndAddChild(list[i], parentKey, indexs, indexMap, treeList);
                        }
                        list.RemoveAt(i);
                    }
                }
            }
            int prevCount = 0, currentCount = list.Count;
            while (prevCount != currentCount)
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    Key parentKey = TreeNode.GetParentKey(list[i]);
                    if (indexMap.ContainsKey(parentKey))
                    {
                        int[] indexs = indexMap[parentKey];
                        if (indexs.Length < maxLevel)
                        {
                            FindParentAndAddChild(list[i], parentKey , indexs, indexMap, treeList);
                        }
                        list.RemoveAt(i);
                    }
                }
                prevCount = currentCount;
                currentCount = list.Count;
            }
            return treeList;
        }

        /// <summary>
        /// 寻找节点的父节点并把节点放入父节点的子节点列表中。
        /// </summary>
        /// <param name="node">游离的节点</param>
        /// <param name="parentKey">父节点id</param>
        /// <param name="indexs">父节点的坐标</param>
        /// <param name="indexMap">节点坐标</param>
        /// <param name="treeList">树</param>
        void FindParentAndAddChild(N node, Key parentKey, int[] indexs, Dictionary<Key, int[]> indexMap, List<N> treeList)
        {
            N parent = GetNodeFromIndex(treeList, indexs);
            TreeNode.ChildList(parent).Add(node);
            int[] newIndexs = new int[indexs.Length + 1];
            Array.Copy(indexs, newIndexs, indexs.Length);
            newIndexs[newIndexs.Length - 1] = TreeNode.ChildList(parent).Count - 1;
            indexMap.Add(TreeNode.GetKey(node), newIndexs);
        }

        /// <summary>
        /// 从下标列表中获取节点
        /// </summary>
        /// <param name="treeList">树集合</param>
        /// <param name="indexs">下标列表</param>
        /// <returns></returns>
        N GetNodeFromIndex(List<N> treeList,int[] indexs)
        {
            List<N> tempList = treeList;
            for (int i=0,len=indexs.Length-1;i<len ;i++)
            {
                tempList = TreeNode.ChildList(tempList[indexs[i]]);
            }
            return tempList[indexs[indexs.Length-1]];
        }
    }

    /// <summary>
    /// 树节点通用接口
    /// </summary>
    /// <typeparam name="N"></typeparam>
    /// <typeparam name="Key"></typeparam>
    public interface TreeNode<N, Key>: IComparer<N>
    {
        /// <summary>
        /// 获取父级主键
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        Key GetParentKey(N node);

        /// <summary>
        /// 获取主键
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        Key GetKey(N node);

        /// <summary>
        /// 检查是否为根节点
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        bool IsRoot(N node);

        /// <summary>
        /// 获取子节点列表
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        List<N> ChildList(N node);
    }
}