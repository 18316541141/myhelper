using CommonHelper.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication1.entity;

namespace WindowsFormsApplication1.Service
{
    /// <summary>
    /// 实体信息转为代码
    /// </summary>
    public class EntityTemplateToCode
    {
        TemplateHelper _templateHelper;

        public EntityTemplateToCode()
        {
            _templateHelper = TemplateHelper.New("AllTemplate");
        }

        char s = Path.AltDirectorySeparatorChar;

        /// <summary>
        /// 把实体类转为.net ef框架的代码
        /// </summary>
        /// <param name="entity"></param>
        public void EntityFrameworkCode(Entity entity)
        {
            string dateStr=DateTime.Now.ToString("yyyyMMddHHmmss");
            string paramsPath = $@"GenTarget{s}{dateStr}{s}Params{s}CodeGenerator{s}";
            string orderByPath = $@"GenTarget{s}{dateStr}{s}OrderBy{s}CodeGenerator{s}";
            string entityPath = $@"GenTarget{s}{dateStr}{s}Entity{s}CodeGenerator{s}";
            string repositoryPath = $@"GenTarget{s}{dateStr}{s}Repository{s}CodeGenerator{s}";
            string servicePath = $@"GenTarget{s}{dateStr}{s}Service{s}";
            string controllersPath = $@"GenTarget{s}{dateStr}{s}Controllers{s}";
            string mappingPath = $@"GenTarget{s}{dateStr}{s}Mapping{s}CodeGenerator{s}";
            string basePath = $@"GenTarget{s}{dateStr}{s}";
            Directory.CreateDirectory(paramsPath);
            Directory.CreateDirectory(orderByPath);
            Directory.CreateDirectory(entityPath);
            Directory.CreateDirectory(repositoryPath);
            Directory.CreateDirectory(servicePath);
            Directory.CreateDirectory(controllersPath);
            Directory.CreateDirectory(mappingPath);
            Directory.CreateDirectory(basePath);
            File.WriteAllText($"{paramsPath}{entity.EntityName}Params.cs", _templateHelper.EntityToStr(entity, "CSharpParams"), Encoding.UTF8);
            File.WriteAllText($"{paramsPath}{entity.EntityName}SetNullParams.cs", _templateHelper.EntityToStr(entity, "CSharpSetNullParams"), Encoding.UTF8);
            File.WriteAllText($"{orderByPath}{entity.EntityName}OrderBy.cs", _templateHelper.EntityToStr(entity, "CSharpOrderBy"), Encoding.UTF8);
            File.WriteAllText($"{entityPath}{entity.EntityName}.cs", _templateHelper.EntityToStr(entity, "CSharpEntity"), Encoding.UTF8);
            File.WriteAllText($"{repositoryPath}{entity.EntityName}Repository.cs", _templateHelper.EntityToStr(entity, "EFRepository"), Encoding.UTF8);
            File.WriteAllText($"{servicePath}{entity.EntityName}Service.cs", _templateHelper.EntityToStr(entity, "CSharpService"), Encoding.UTF8);
            File.WriteAllText($"{controllersPath}{entity.EntityName}Controller.cs", _templateHelper.EntityToStr(entity, "CSharpController"), Encoding.UTF8);
            File.WriteAllText($"{mappingPath}{entity.EntityName}Map.cs", _templateHelper.EntityToStr(entity, "EFMapping"), Encoding.UTF8);
            File.WriteAllText($"{basePath}ElemntUIList.vue", _templateHelper.EntityToStr(entity, "ElemntUIList"), Encoding.UTF8);
            File.WriteAllText($"{basePath}EFDbContext.txt", _templateHelper.EntityToStr(entity, "EFDbContext"), Encoding.UTF8);
        }

        /// <summary>
        /// 把实体类转为mybatis框架的代码
        /// </summary>
        /// <param name="entity"></param>
        public void MyBatisCode(Entity entity)
        {
            string dateStr = DateTime.Now.ToString("yyyyMMddHHmmss");
            string entityPath = $@"GenTarget{s}{dateStr}{s}web{s}template{s}entity{s}codeGenerator{s}";
            string paramsPath = $@"GenTarget{s}{dateStr}{s}web{s}template{s}entity{s}params{s}codeGenerator{s}";
            string mapperPath = $@"GenTarget{s}{dateStr}{s}mapper{s}codeGenerator{s}";
            Directory.CreateDirectory(paramsPath);
            Directory.CreateDirectory(entityPath);
            Directory.CreateDirectory(mapperPath);
            File.WriteAllText($"{entityPath}{entity.EntityName}.java", _templateHelper.EntityToStr(entity, "JavaEntity"), Encoding.UTF8);
            File.WriteAllText($"{paramsPath}{entity.EntityName}OrderBy.java", _templateHelper.EntityToStr(entity, "JavaOrderBy"), Encoding.UTF8);
            File.WriteAllText($"{paramsPath}{entity.EntityName}Params.java", _templateHelper.EntityToStr(entity, "JavaParams"), Encoding.UTF8);
            File.WriteAllText($"{mapperPath}{entity.EntityName}Mapper.xml", _templateHelper.EntityToStr(entity, "MybatisMapper"), Encoding.UTF8);
        }
    }
}
