using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper.Helper
{
    /// <summary>
    /// 生成器帮助类
    /// </summary>
    public static class GenerateHelper
    {
        /// <summary>
        /// 把单个json转化为实体类代码
        /// </summary>
        /// <param name="json"></param>
        /// <returns>返回实体类代码</returns>
        public static string JsonGenEntity(string json)
        {
            JObject jobject = JObject.Parse(json);
            StringBuilder sb = new StringBuilder("public class 类名\r\n{\r\n");
            foreach (KeyValuePair<string, JToken> keyVal in jobject)
                sb.Append($"public string {keyVal.Key} {{ set; get; }}\r\n");
            sb.Append("}");
            return sb.ToString();
        }

        /// <summary>
        /// 通过单个json对象生成数据表MySql语句
        /// </summary>
        /// <param name="json"></param>
        /// <returns>返回建表的Mysql语句</returns>
        public static string JsonGenCreateTableByMySql(string json)
        {
            JObject jobject = JObject.Parse(json);
            StringBuilder sb = new StringBuilder("CREATE TABLE `TABLE_NAME`(\r\n");
            foreach (KeyValuePair<string,JToken> keyVal in jobject)
                sb.Append($"`{keyVal.Key.ToUpper()}` varchar(32) NOT NULL COMMENT '{keyVal.Key.ToUpper()}的注释',\r\n");
            sb.Remove(sb.Length-1,1);
            sb.Append(") ENGINE=InnoDB DEFAULT CHARSET=utf8;");
            return sb.ToString();
        }

        /// <summary>
        /// 通过单个json对象生成插入的sql语句
        /// </summary>
        /// <param name="json">json对象</param>
        /// <returns>返回</returns>
        public static string JsonGenInsertBySql(string json)
        {
            JObject jobject = JObject.Parse(json);
            StringBuilder keys = new StringBuilder();
            StringBuilder values = new StringBuilder();
            foreach (KeyValuePair<string, JToken> keyVal in jobject)
            {
                keys.Append($"`{keyVal.Key}`,");
                values.Append($"'{Convert.ToString(keyVal.Value)}',");
            }
            keys.Remove(keys.Length - 1, 1);
            values.Remove(values.Length - 1, 1);
            return $"INSERT INTO `TABLE_NAME`({keys.ToString()})VALUES({values.ToString()});";
        }

        /// <summary>
        /// 根据html的表格头生成MySql语句
        /// </summary>
        /// <param name="tableHead">表格头<thead></thead>标签的内容，含有thead</param>
        /// <returns>返回建表的Mysql语句</returns>
        public static string TableHeadGenCreateTableByMySql(string tableHead)
        {
            HtmlDocument doc= new HtmlDocument();
            doc.LoadHtml(tableHead);
            HtmlNode htmlNode = doc.DocumentNode;
            StringBuilder sb = new StringBuilder("CREATE TABLE `TABLE_NAME`(\r\n");
            foreach (HtmlNode th in htmlNode.SelectNodes("th"))
                sb.Append($"`{th.InnerText.ToUpper()}` varchar(32) NOT NULL COMMENT '{th.InnerText.ToUpper()}的注释',\r\n");
            foreach (HtmlNode td in htmlNode.SelectNodes("td"))
                sb.Append($"`{td.InnerText.ToUpper()}` varchar(32) NOT NULL COMMENT '{td.InnerText.ToUpper()}的注释',\r\n");
            sb.Remove(sb.Length - 1, 1);
            sb.Append(") ENGINE=InnoDB DEFAULT CHARSET=utf8;");
            return sb.ToString();
        }

        public static string TableRowGenInsertBySql(string tableRow)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(tableRow);
            HtmlNode htmlNode = doc.DocumentNode;
            StringBuilder keys = new StringBuilder();
            StringBuilder values = new StringBuilder();
            foreach (HtmlNode td in htmlNode.SelectNodes("td"))
            {
                keys.Append($"`{td.InnerText}`,");
                values.Append($"'{td.InnerText}',");
            }
            keys.Remove(keys.Length - 1, 1);
            values.Remove(values.Length - 1, 1);
            return $"INSERT INTO `TABLE_NAME`({keys.ToString()})VALUES({values.ToString()});";
        }
    }
}
