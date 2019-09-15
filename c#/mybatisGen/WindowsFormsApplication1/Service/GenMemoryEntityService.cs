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
        public Entity GenTemplateEntity(object obj)
        {
            Type type=obj.GetType();
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
                if (propertyInfo.PropertyType.Name== "String")
                {
                    entity.PropList.Add(new Prop
                    {
                        PropName = propertyInfo.Name,
                        CapUpperPropName = nameTransService.HumpToBigHump(propertyInfo.Name),
                        IsKey = keyAttr != null,
                        ParamsType = "equal",
                        PropType = nameTransService.BigHumpToHump(propertyInfo.PropertyType.Name),
                        SrcPropName = propertyInfo.Name,
                    });
                    entity.PropList.Add(new Prop
                    {
                        PropName = propertyInfo.Name+"Like",
                        CapUpperPropName = nameTransService.HumpToBigHump(propertyInfo.Name) + "Like",
                        IsKey = keyAttr != null,
                        PropType = nameTransService.BigHumpToHump(propertyInfo.PropertyType.Name),
                        ParamsType = "like",
                        SrcPropName = propertyInfo.Name
                    });
                }
                else if (propertyInfo.PropertyType.Name.StartsWith("Nullable"))
                {
                    Type temp=propertyInfo.GetValue(obj).GetType();
                    string typeName = nameTransService.BigHumpToHump(temp.Name);
                    if(typeName=="int64" || typeName == "int32" || typeName == "int16")
                    {
                        if(typeName == "int64")
                        {
                            typeName = "long";
                        }
                        else if (typeName == "int32")
                        {
                            typeName = "int";
                        }
                        else if (typeName == "int16")
                        {
                            typeName = "byte";
                        }
                        entity.PropList.Add(new Prop
                        {
                            PropName = propertyInfo.Name,
                            CapUpperPropName = nameTransService.HumpToBigHump(propertyInfo.Name),
                            IsKey = keyAttr != null,
                            PropType = typeName + "?",
                            ParamsType = "equal",
                            SrcPropName = propertyInfo.Name
                        });
                        entity.PropList.Add(new Prop
                        {
                            PropName = propertyInfo.Name + "Start",
                            CapUpperPropName = nameTransService.HumpToBigHump(propertyInfo.Name) + "Start",
                            IsKey = keyAttr != null,
                            PropType = typeName + "?",
                            ParamsType = "rangStart",
                            SrcPropName = propertyInfo.Name
                        });
                        entity.PropList.Add(new Prop
                        {
                            PropName = propertyInfo.Name+ "End",
                            CapUpperPropName = nameTransService.HumpToBigHump(propertyInfo.Name) + "End",
                            IsKey = keyAttr != null,
                            PropType = typeName + "?",
                            ParamsType = "rangEnd",
                            SrcPropName = propertyInfo.Name
                        });
                    }
                }
                else if (propertyInfo.PropertyType.Name=="DateTime")
                {
                    entity.PropList.Add(new Prop
                    {
                        PropName = propertyInfo.Name,
                        CapUpperPropName = nameTransService.HumpToBigHump(propertyInfo.Name),
                        IsKey = keyAttr != null,
                        ParamsType = "equal",
                        PropType = propertyInfo.PropertyType.Name+"?",
                        SrcPropName = propertyInfo.Name
                    });
                    entity.PropList.Add(new Prop
                    {
                        PropName = propertyInfo.Name + "Start",
                        CapUpperPropName = nameTransService.HumpToBigHump(propertyInfo.Name) + "Start",
                        IsKey = keyAttr != null,
                        PropType = propertyInfo.PropertyType.Name + "?",
                        ParamsType = "rangStart",
                        SrcPropName = propertyInfo.Name
                    });
                    entity.PropList.Add(new Prop
                    {
                        PropName = propertyInfo.Name + "End",
                        CapUpperPropName = nameTransService.HumpToBigHump(propertyInfo.Name) + "End",
                        IsKey = keyAttr != null,
                        PropType = propertyInfo.PropertyType.Name + "?",
                        ParamsType = "rangEnd",
                        SrcPropName = propertyInfo.Name
                    });
                }
            }
            return entity;
        }
    }
}
