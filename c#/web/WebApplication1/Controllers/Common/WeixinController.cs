using CommonHelper.Helper;
using CommonHelper.Helper.CommonEntity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Entity.Common;

namespace WebApplication1.Controllers.Common
{
    /// <summary>
    /// 微信接口、jssdk专用的控制器（登录除外，因为登录会修改session，所以登录放在SessionController、
    /// </summary>
    public sealed class WeixinController : FastController
    {
        /// <summary>
        /// 微信公众平台专用的图片上传功能
        /// </summary>
        /// <param name="serverId">微信图片服务器端ID</param>
        /// <param name="pathName">图片保存的路径名称</param>
        /// <returns></returns>
        public JsonResult UploadSingleWxImage(string serverId, string pathName)
        {
            Image image = HttpWebRequestHelper.HttpGet($"http://file.api.weixin.qq.com/cgi-bin/media/get?access_token=access_token&media_id={serverId}").GetImage();
            string imgName = FileHelper.SaveImageBySha1(image, $"{Server.MapPath("~/uploadFiles/")}{pathName}");
            using (Image img = Image.FromFile($"{Server.MapPath("~/uploadFiles/")}{pathName}{Path.DirectorySeparatorChar}{imgName}"))
            {
                using (Image thumbnailImg = img.GetThumbnailImage(150, img.Height * 150 / img.Width, () => false, IntPtr.Zero))
                {
                    string thumbnailName = FileHelper.SaveImageBySha1(thumbnailImg, $"{Server.MapPath("~/uploadFiles/")}{pathName}");
                    return MyJson(new Result { code = 0, data = new { thumbnailName = thumbnailName, imgName = imgName, imgWidth = img.Width, imgHeight = img.Height } });
                }
            }
        }

        /// <summary>
        /// 微信jssdk接口使用时，加载的配置信息。
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        public JsonResult WxCfg(string routerPath)
        {
            return MyJson(new Result { code = 0 });
        }
    }
}