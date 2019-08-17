using CommonHelper.Helper;
using System;
using System.Collections.Generic;
using WebApplication1.Entity;
using WebApplication1.ExcelEntity;
namespace WebApplication1.Entity
{
    public partial class IRobotQrCodePayTask
    {
        /// <summary>
        /// 单个excel数据转为IRobotQrCodePayTask实体数据，这里需要用户自定义转换规则
        /// </summary>
        /// <param name="irobotQrCodePayTaskExcel"></param>
        public IRobotQrCodePayTask(IRobotQrCodePayTaskExcel irobotQrCodePayTaskExcel)
        {
            irTaskID = irobotQrCodePayTaskExcel.irTaskID;
            irOrderNo = irobotQrCodePayTaskExcel.irOrderNo;
            irWeiXinNickName = irobotQrCodePayTaskExcel.irWeiXinNickName;
            irWeiXinHeaderImage = irobotQrCodePayTaskExcel.irWeiXinHeaderImage;
            irQrCodeImagePath = irobotQrCodePayTaskExcel.irQrCodeImagePath;
            irHandleState = irobotQrCodePayTaskExcel.irHandleState;
            irHandleMessage = irobotQrCodePayTaskExcel.irHandleMessage;
            irHandleTime = irobotQrCodePayTaskExcel.irHandleTime;
            irCreateTime = irobotQrCodePayTaskExcel.irCreateTime;
            irReportPicPathJson = irobotQrCodePayTaskExcel.irReportPicPathJson;
            irTakeMoney = irobotQrCodePayTaskExcel.irTakeMoney;
            irRobotId = irobotQrCodePayTaskExcel.irRobotId;
            irRemark = irobotQrCodePayTaskExcel.irRemark;
            irPushState = irobotQrCodePayTaskExcel.irPushState;
            irPushTime = irobotQrCodePayTaskExcel.irPushTime;
            irScanPayNotifyRet = irobotQrCodePayTaskExcel.irScanPayNotifyRet;
        }
    }

}
namespace WebApplication1.ExcelEntity
{

    [ExcelSheet(SheetName = "sheet1")]
    public class IRobotQrCodePayTaskExcel
    {
        /// <summary>
        /// 多个IRobotQrCodePayTask实体转为excel的数据
        /// </summary>
        /// <param name="irobotQrCodePayTaskList">多个IRobotQrCodePayTask实体</param>
        /// <returns>返回excel数据</returns>
        public static List<IRobotQrCodePayTaskExcel> NewExcelList(List<IRobotQrCodePayTask> irobotQrCodePayTaskList)
        {
            List<IRobotQrCodePayTaskExcel> irobotQrCodePayTaskExcelList = new List<IRobotQrCodePayTaskExcel>();
            foreach (IRobotQrCodePayTask irobotQrCodePayTask in irobotQrCodePayTaskList)
            {
                irobotQrCodePayTaskExcelList.Add(new IRobotQrCodePayTaskExcel(irobotQrCodePayTask));
            }
            return irobotQrCodePayTaskExcelList;
        }

        /// <summary>
        /// 多个excel数据转为IRobotQrCodePayTask实体数据
        /// </summary>
        /// <param name="irobotQrCodePayTaskExcelList">多个excel数据</param>
        /// <returns>返回IRobotQrCodePayTask实体数据</returns>
        public static List<IRobotQrCodePayTask> NewList(List<IRobotQrCodePayTaskExcel> irobotQrCodePayTaskExcelList)
        {
            List<IRobotQrCodePayTask> irobotQrCodePayTaskList = new List<IRobotQrCodePayTask>();
            foreach (IRobotQrCodePayTaskExcel irobotQrCodePayTaskExcel in irobotQrCodePayTaskExcelList)
            {
                irobotQrCodePayTaskList.Add(new IRobotQrCodePayTask(irobotQrCodePayTaskExcel));
            }
            return irobotQrCodePayTaskList;
        }

        public IRobotQrCodePayTaskExcel()
        {

        }

        /// <summary>
        /// 单个IRobotQrCodePayTask实体转为excel的数据，这里需要用户自定义转换规则
        /// </summary>
        /// <param name="irobotQrCodePayTask"></param>
        public IRobotQrCodePayTaskExcel(IRobotQrCodePayTask irobotQrCodePayTask)
        {
            irTaskID = irobotQrCodePayTask.irTaskID;
            irOrderNo = irobotQrCodePayTask.irOrderNo;
            irWeiXinNickName = irobotQrCodePayTask.irWeiXinNickName;
            irWeiXinHeaderImage = irobotQrCodePayTask.irWeiXinHeaderImage;
            irQrCodeImagePath = irobotQrCodePayTask.irQrCodeImagePath;
            irHandleState = irobotQrCodePayTask.irHandleState;
            irHandleMessage = irobotQrCodePayTask.irHandleMessage;
            irHandleTime = irobotQrCodePayTask.irHandleTime;
            irCreateTime = irobotQrCodePayTask.irCreateTime;
            irReportPicPathJson = irobotQrCodePayTask.irReportPicPathJson;
            irTakeMoney = irobotQrCodePayTask.irTakeMoney;
            irRobotId = irobotQrCodePayTask.irRobotId;
            irRemark = irobotQrCodePayTask.irRemark;
            irPushState = irobotQrCodePayTask.irPushState;
            irPushTime = irobotQrCodePayTask.irPushTime;
            irScanPayNotifyRet = irobotQrCodePayTask.irScanPayNotifyRet;
        }

        [ExcelCol(ColName = "irTaskID", ColIndex = 0)]
        public virtual int? irTaskID { set; get; }
        [ExcelCol(ColName = "irOrderNo", ColIndex = 1)]
        public virtual string irOrderNo { set; get; }
        [ExcelCol(ColName = "irWeiXinNickName", ColIndex = 2)]
        public virtual string irWeiXinNickName { set; get; }
        [ExcelCol(ColName = "irWeiXinHeaderImage", ColIndex = 3)]
        public virtual string irWeiXinHeaderImage { set; get; }
        [ExcelCol(ColName = "irQrCodeImagePath", ColIndex = 4)]
        public virtual string irQrCodeImagePath { set; get; }
        [ExcelCol(ColName = "irHandleState", ColIndex = 5)]
        public virtual int? irHandleState { set; get; }
        [ExcelCol(ColName = "irHandleMessage", ColIndex = 6)]
        public virtual string irHandleMessage { set; get; }
        [ExcelCol(ColName = "irHandleTime", ColIndex = 7)]
        public virtual DateTime? irHandleTime { set; get; }
        [ExcelCol(ColName = "irCreateTime", ColIndex = 8)]
        public virtual DateTime? irCreateTime { set; get; }
        [ExcelCol(ColName = "irReportPicPathJson", ColIndex = 9)]
        public virtual string irReportPicPathJson { set; get; }
        [ExcelCol(ColName = "irTakeMoney", ColIndex = 10)]
        public virtual int? irTakeMoney { set; get; }
        [ExcelCol(ColName = "irRobotId", ColIndex = 11)]
        public virtual string irRobotId { set; get; }
        [ExcelCol(ColName = "irRemark", ColIndex = 12)]
        public virtual string irRemark { set; get; }
        [ExcelCol(ColName = "irPushState", ColIndex = 13)]
        public virtual string irPushState { set; get; }
        [ExcelCol(ColName = "irPushTime", ColIndex = 14)]
        public virtual DateTime? irPushTime { set; get; }

        [ExcelCol(ColName = "irScanPayNotifyRet", ColIndex = 15)]
        public virtual string irScanPayNotifyRet { set; get; }
    }
}