using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1.Service
{
    /// <summary>
    /// 办事易项目组专用的命名转换规则
    /// </summary>
    public class BsyNameTrans : INameTrans
    {
        NameTransService _nameTransService;

        public BsyNameTrans()
        {
            _nameTransService = new NameTransService();
        }

        public string ColNameToPropName(string colName)=>
            _nameTransService.UnderlineToHump(colName);

        public string TableNameToEntityName(string tableName)=>
            _nameTransService.UnderlineToBigHump(tableName);
    }
}
