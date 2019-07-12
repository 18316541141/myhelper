﻿using CommonHelper.Helper;
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
            string paramsPath = $@"GenTarget{s}{dateStr}{s}Params{s}codeGenerator{s}";
            string entityPath = $@"GenTarget{s}{dateStr}{s}Entity{s}codeGenerator{s}";
            string repositoryPath = $@"GenTarget{s}{dateStr}{s}Repository{s}codeGenerator{s}";
            string mappingPath = $@"GenTarget{s}{dateStr}{s}Mapping{s}codeGenerator{s}";
            Directory.CreateDirectory(paramsPath);
            Directory.CreateDirectory(entityPath);
            Directory.CreateDirectory(repositoryPath);
            Directory.CreateDirectory(mappingPath);
            File.WriteAllText($"{paramsPath}{entity.EntityName}Params.cs", _templateHelper.EntityToStr(entity, "CSharpParams"), Encoding.UTF8);
            File.WriteAllText($"{paramsPath}{entity.EntityName}OrderBy.cs", _templateHelper.EntityToStr(entity, "CSharpOrderBy"), Encoding.UTF8);
            File.WriteAllText($"{entityPath}{entity.EntityName}.cs", _templateHelper.EntityToStr(entity, "CSharpEntity"), Encoding.UTF8);
            File.WriteAllText($"{repositoryPath}{entity.EntityName}Repository.cs", _templateHelper.EntityToStr(entity, "EFRepository"), Encoding.UTF8);
            File.WriteAllText($"{mappingPath}{entity.EntityName}Map.cs", _templateHelper.EntityToStr(entity, "EFMapping"), Encoding.UTF8);
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