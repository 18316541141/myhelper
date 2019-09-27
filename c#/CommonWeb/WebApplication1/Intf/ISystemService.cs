using CommonHelper.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonWeb.Intf
{
    /// <summary>
    /// 系统业务接口，用于提供系统常用的功能
    /// </summary>
    public interface ISystemService
    {
        /// <summary>
        /// 上传文件的保存位置
        /// </summary>
        /// <returns></returns>
        string UploadFilePath();

        /// <summary>
        /// 对已停止运行的机器人发送报警处理
        /// </summary>
        /// <param name="heartbeatEntity">可能是已停止运行的机器人，需要发送报警</param>
        void SendWarning(HeartbeatEntity heartbeatEntity);
    }
}