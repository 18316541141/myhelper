using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace WindowsFormsApplication1.entity
{
    public class Entity
    {
        /// <summary>
        /// 数据库类型
        /// </summary>
        public string DbType { set; get; }

        /// <summary>
        /// 实体名称
        /// </summary>
        public string EntityName { set; get; }

        /// <summary>
        /// 实体名称的复数形式
        /// </summary>
        public string EntityNames { set; get; }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { set; get; }

        /// <summary>
        /// 主键名
        /// </summary>
        public string KeyName { set; get; }

        /// <summary>
        /// 主键列
        /// </summary>
        public string KeyCol { set; get; }

        /// <summary>
        /// 实体注释
        /// </summary>
        public string EntityNotes { set; get; }

        /// <summary>
        /// 属性列表
        /// </summary>
        public List<Prop> PropList { set; get; }

        public Entity()
        {
            PropList = new List<Prop>();
        }
    }

    public class Prop
    {
        /// <summary>
        /// 属性名称
        /// </summary>
        public string PropName { set; get; }

        /// <summary>
        /// 当ParamsType不为equal时，需要提供PropName的原始属性名称，例如：nameLike是模糊搜索属性，
        /// 原始属性是name
        /// </summary>
        public string SrcPropName { set; get; }

        /// <summary>
        /// 数据库列名称
        /// </summary>
        public string ColName { set; get; }

        /// <summary>
        /// 属性类型
        /// </summary>
        public string PropType { set; get; }

        /// <summary>
        /// 参数类型，equal：全等、rangeStart：始区间、rangeEnd：终区间、like：模糊匹配
        /// </summary>
        public string ParamsType { set; get; }

        /// <summary>
        /// 是否为主键
        /// </summary>
        public bool IsKey { get; set; }

        /// <summary>E:\git_projects\myhelper\c#\mybatisGen\WindowsFormsApplication1\AllTemplate\EFRepository
        /// 参数类型是equal时为true，否则为false
        /// </summary>
        public bool ParamsTypeIsEqual { get { return ParamsType == "equal"; } }

        /// <summary>
        /// 参数类型是rangeStart时为true，否则为false
        /// </summary>
        public bool ParamsTypeIsRangeStart { get { return ParamsType == "rangeStart"; } }

        /// <summary>
        /// 参数类型是rangeEnd时为true，否则为false
        /// </summary>
        public bool ParamsTypeIsRangeEnd { get { return ParamsType == "rangeEnd"; } }

        /// <summary>
        /// 参数类型是like时为true，否则为false
        /// </summary>
        public bool ParamsTypeIsLike { get { return ParamsType == "like"; } }

        /// <summary>
        /// 属性注释
        /// </summary>
        public string PropNotes { set; get; }
    }
}
