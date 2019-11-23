using WindowsFormsApplication1.Intf;
using WindowsFormsApplication1.Service;

namespace WindowsFormsApplication1.NameTrans
{
    /// <summary>
    /// 机器人项目组专用的c#命名转换规则
    /// </summary>
    public sealed class RobotNameCSharpTrans2 : INameTrans
    {
        NameTransService nameTransService { set; get; }

        public RobotNameCSharpTrans2()
        {
            nameTransService = new NameTransService();
        }

        /// <summary>
        /// 列名称转属性名称
        /// </summary>
        /// <param name="colName">表列名称</param>
        /// <returns>返回属性名称</returns>
        public string ColNameToPropName(string colName)
        {
            return colName.Replace("_", "");
        }

        /// <summary>
        /// 表名称转实体名称
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <returns>返回实体名称</returns>
        public string TableNameToEntityName(string tableName)
        {
            return tableName.Replace("_", "");
        }
    }
}
