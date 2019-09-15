using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication1.entity;

namespace WindowsFormsApplication1.Service
{
    /// <summary>
    /// 生成内存表操作的实体信息
    /// </summary>
    public class GenMemoryEntityService
    {
        /// <summary>
        /// 生成实体
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public Entity GenTemplateEntity(Type type)
        {
            NameTransService nameTransService = new NameTransService();
            Entity entity = new Entity
            {
                EntityName = type.Name,
                EntityNames = nameTransService.SingleToComplex(type.Name)
            };
            foreach (PropertyInfo propertyInfo in type.GetProperties())
            {
                KeyAttr keyAttr = propertyInfo.GetCustomAttribute<KeyAttr>();
                if (keyAttr != null)
                {
                    entity.KeyName = propertyInfo.Name;
                }
                entity.PropList.Add(new Prop
                {
                    PropName = propertyInfo.Name,
                    CapUpperPropName = nameTransService.HumpToBigHump(propertyInfo.Name),
                    IsKey = keyAttr != null,
                    PropType = propertyInfo.PropertyType.Name
                });
                if (propertyInfo.PropertyType.Name== "string")
                {
                    entity.PropList.Add(new Prop
                    {
                        PropName = propertyInfo.Name+"Like",
                        CapUpperPropName = nameTransService.HumpToBigHump(propertyInfo.Name) + "Like",
                        IsKey = keyAttr != null,
                        PropType = propertyInfo.PropertyType.Name,
                        ParamsType = "like",
                        SrcPropName = propertyInfo.Name
                    });
                }
                else if (propertyInfo.PropertyType.Name == "long" || propertyInfo.PropertyType.Name == "DateTime" || propertyInfo.PropertyType.Name == "int32" || propertyInfo.PropertyType.Name == "sbyte")
                {
                    entity.PropList.Add(new Prop
                    {
                        PropName = propertyInfo.Name + "Start",
                        CapUpperPropName = nameTransService.HumpToBigHump(propertyInfo.Name) + "Start",
                        IsKey = keyAttr != null,
                        PropType = propertyInfo.PropertyType.Name,
                        ParamsType = "rangStart",
                        SrcPropName = propertyInfo.Name
                    });
                    entity.PropList.Add(new Prop
                    {
                        PropName = propertyInfo.Name+ "End",
                        CapUpperPropName = nameTransService.HumpToBigHump(propertyInfo.Name) + "End",
                        IsKey = keyAttr != null,
                        PropType = propertyInfo.PropertyType.Name,
                        ParamsType = "rangEnd",
                        SrcPropName = propertyInfo.Name
                    });
                }
            }
            return entity;
        }
    }
}
