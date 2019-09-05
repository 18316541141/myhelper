using Antlr3.ST;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
namespace CommonHelper.Helper
{
    /// <summary>
    /// 模板帮助类
    /// </summary>
    public sealed class TemplateHelper
    {
        string _templateBasePath, _smallTemplatePath;
        static TemplateHelper _templateHelper;

        /// <summary>
        /// 模板缓存
        /// </summary>
        Dictionary<string, string> _cache;

        /// <summary>
        /// 模板文件夹的相对路径，相对于项目根目录
        /// </summary>
        /// <param name="templateBasePath"></param>
        private TemplateHelper(string templateBasePath,string smallTemplatePath=null)
        {
            _templateBasePath = templateBasePath;
            _smallTemplatePath = smallTemplatePath;
            _attrRendererMap = new Dictionary<string, Dictionary<Type, IAttributeRenderer>>();
            _cache = new Dictionary<string, string>();
            Directory.CreateDirectory(_templateBasePath);
            foreach (string file in Directory.GetFiles(_templateBasePath))
                _cache.Add(new FileInfo(file).Name,File.ReadAllText(file));
            foreach (string path in Directory.GetDirectories(_templateBasePath,"*.*",SearchOption.AllDirectories))
                foreach (string file in Directory.GetFiles(path))
                    _cache.Add(string.Join(".",new FileInfo(file).FullName.Replace(_templateBasePath, "").Split(Path.DirectorySeparatorChar)), File.ReadAllText(file));
            if (_smallTemplatePath != null)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load($"{_templateBasePath}{Path.DirectorySeparatorChar}{_smallTemplatePath}");
                foreach (XmlNode node in doc.SelectNodes("/root/*"))
                    if (node.NodeType == XmlNodeType.Element)
                        _cache.Add(((XmlElement)node).Name, ((XmlElement)node).InnerText);
            }
        }

        /// <summary>
        /// 把实体的数据融合到模板中，并返回融合结果，会把数据整合为一行，减少空间占用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">实体类</param>
        /// <param name="templateNames">模板名称，可以有多级，多级用“.”分隔，对于小模板只需提供节点名称就可以了</param>
        /// <returns></returns>
        public string EntityToRow<T>(T entity,string templateName)
        {
            StringBuilder templateSb = new StringBuilder();
            foreach (string row in _cache[templateName].Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
                templateSb.Append(row);
            StringTemplate stringTemplate = new StringTemplate(templateSb.ToString());
            if (_attrRendererMap.ContainsKey(templateName))
                foreach (KeyValuePair<Type, IAttributeRenderer> keyVal in _attrRendererMap[templateName])
                    stringTemplate.RegisterRenderer(keyVal.Key, keyVal.Value);
            stringTemplate.SetAttribute("entity",entity);
            return stringTemplate.ToString().Trim();
        }

        /// <summary>
        /// 把实体的数据融合到模板中，并返回融合结果，会保留数据的空格，换行，减少空间占用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">实体类</param>
        /// <param name="templateNames">模板名称，可以有多级，多级用“.”分隔，对于小模板只需提供节点名称就可以了</param>
        /// <returns></returns>
        public string EntityToStr<T>(T entity, string templateName)
        {
            StringTemplate stringTemplate = new StringTemplate(_cache[templateName]);
            if (_attrRendererMap.ContainsKey(templateName))
                foreach (KeyValuePair<Type, IAttributeRenderer> keyVal in _attrRendererMap[templateName])
                    stringTemplate.RegisterRenderer(keyVal.Key, keyVal.Value);
            stringTemplate.SetAttribute("entity", entity);
            return stringTemplate.ToString().Trim();
        }

        /// <summary>
        /// 把实体的数据融合到模板中，并返回融合结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="templateName"></param>
        /// <returns></returns>
        public JObject EntityToJObject<T>(T entity,string templateName)=>
            JObject.Parse(EntityToRow<T>(entity, templateName));

        /// <summary>
        /// 创建一个模板帮助类
        /// </summary>
        /// <param name="templateBasePath"></param>
        /// <returns></returns>
        public static TemplateHelper New(string templateBasePath,string smallTemplatePath=null)
        {
            if (_templateHelper == null)
                lock (typeof(TemplateHelper))
                    if (_templateHelper == null)
                        _templateHelper = new TemplateHelper(templateBasePath, smallTemplatePath);
            return _templateHelper;
        }

        Dictionary<string, Dictionary<Type, IAttributeRenderer>> _attrRendererMap;

        /// <summary>
        /// 注册类型转换器
        /// </summary>
        /// <param name="templateName">模板名称</param>
        /// <param name="type">需要转换的类型</param>
        /// <param name="attributeRenderer">该类型的转换器</param>
        public void RegisterRenderer(string templateName,Type type, IAttributeRenderer attributeRenderer)
        {
            Dictionary<Type, IAttributeRenderer> tempMap;
            if (_attrRendererMap.ContainsKey(templateName))
                tempMap = _attrRendererMap[templateName];
            else
                _attrRendererMap.Add(templateName, tempMap = new Dictionary<Type, IAttributeRenderer>());
            if (tempMap.ContainsKey(type))
                tempMap[type] = attributeRenderer;
            else
                tempMap.Add(type,attributeRenderer);
        }
    }
}