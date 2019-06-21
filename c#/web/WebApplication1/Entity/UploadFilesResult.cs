using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Entity
{
    /// <summary>
    /// 上传文件的返回值
    /// </summary>
    public class UploadFilesResult
    {
        /// <summary>
        /// 文件扩展名
        /// </summary>
        public string extension { set; get; }

        /// <summary>
        /// 文件的sha1
        /// </summary>
        public string sha1 { set; get; }
    }
}