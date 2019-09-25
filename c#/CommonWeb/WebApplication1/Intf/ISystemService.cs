using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonWeb.Intf
{
    /// <summary>
    /// 系统业务接口，用于提供系统信息
    /// </summary>
    public interface ISystemService
    {
        /// <summary>
        /// 上传文件的保存位置
        /// </summary>
        /// <returns></returns>
        string UploadFilePath();
    }
}