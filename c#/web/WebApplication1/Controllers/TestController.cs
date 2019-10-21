using CommonHelper.CommonEntity;
using CommonHelper.Helper;
using CommonHelper.Helper.CommonEntity;
using CommonWeb.Entity.Common;
using CommonWeb.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Controllers.Common;
namespace WebApplication1.Controllers
{
    public class TestController: FastController
    {
        public LogEntityService LogEntityService { set; get; }

        public ExcelResult<LogEntity> ExportExcelTest(LogEntityParams logEntityParams,int currentPageIndex, int pageSize)
        {
            return new ExcelResult<LogEntity>
            {
                DataList = LogEntityService.Page(logEntityParams, currentPageIndex, pageSize).PageDataList,
                FileName = "测试导出.xlsx"
            };
        }

        public JsonResult LoadTreeNode()
        {
            List<TreeFormNode> resultList = new List<TreeFormNode>();
            TreeFormNode tree1 = new TreeFormNode
            {
                Id=1,
                Name="广东省"
            };
            tree1.Children.Add(new TreeFormNode
            {
                Id = 4,
                Name = "北京镇"
            });
            resultList.Add(tree1);
            TreeFormNode tree2 = new TreeFormNode
            {
                Id = 2,
                Name = "顺德省"
            };
            resultList.Add(tree2);
            TreeFormNode tree3 = new TreeFormNode
            {
                Id = 3,
                Name = "容桂省"
            };
            resultList.Add(tree3);
            return MyJson(new Result
            {
                code=0,
                data = resultList
            });
        }
    }
}