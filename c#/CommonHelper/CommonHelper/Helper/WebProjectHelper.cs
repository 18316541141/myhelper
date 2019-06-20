using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper.Helper
{
    /// <summary>
    /// web项目帮助类
    /// </summary>
    public class WebProjectHelper
    {
        /// <summary>
        /// 刷新web项目
        /// </summary>
        /// <param name="webRootPath">web项目根目录（例如：E:\git_projects\myhelper\c#\web\WebApplication1）</param>
        public static void RefreshVersion(string webRootPath)
        {
            string version=DateTime.Now.ToString("yyyyMMddHHmmss");
            File.WriteAllText($"{webRootPath}index.aspx", $"<script type='text/javascript'>location.href = '/Content/index.html?v={version}';</script>");
            HtmlDocument doc= new HtmlDocument();
            doc.Load($"{webRootPath}Content{Path.DirectorySeparatorChar}index.html");
            HtmlNode node = doc.DocumentNode;
            node.SelectSingleNode("//div[@ng-include]").SetAttributeValue("ng-include", $"x.url+'?v={version}'");
            node.SelectSingleNode("//script[starts-with(@src,'my-js/project.js')]").SetAttributeValue("src", $"my-js/project.js?v={version}");
            doc.Save($"{webRootPath}Content{Path.DirectorySeparatorChar}index.html");
        }
    }
}
