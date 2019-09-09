using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication1.Intf;
using WindowsFormsApplication1.Service;

namespace WindowsFormsApplication1.NameTrans
{
    /// <summary>
    /// 办事易项目组专用的命名转换规则
    /// </summary>
    public sealed class BsyNameTrans : INameTrans
    {
        NameTransService _nameTransService;

        public BsyNameTrans()
        {
            _nameTransService = new NameTransService();
        }

        public string ColNameToPropName(string colName)=>colName;

        public string TableNameToEntityName(string tableName) =>
            _nameTransService.HumpToBigHump(tableName.Replace("t_", ""));
    }
}
