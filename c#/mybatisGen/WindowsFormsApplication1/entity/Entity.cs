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
        /// 实体名称
        /// </summary>
        public string EntityName { set; get; }

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
        /// 数据库列名称
        /// </summary>
        public string ColName { set; get; }

        /// <summary>
        /// 属性类型
        /// </summary>
        public string PropType { set; get; }

        /// <summary>
        /// 参数类型
        /// </summary>
        public string ParamsType { set; get; }

        /// <summary>
        /// 属性注释
        /// </summary>
        public string PropNotes { set; get; }
    }
}
