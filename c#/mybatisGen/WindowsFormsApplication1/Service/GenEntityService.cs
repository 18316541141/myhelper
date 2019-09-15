using CommonHelper.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication1.entity;
using WindowsFormsApplication1.Intf;

namespace WindowsFormsApplication1.Service
{
    /// <summary>
    /// 生成数据库操作的实体信息
    /// </summary>
    public sealed class GenEntityService
    {
        /// <summary>
        /// 命名转换规则
        /// </summary>
        public INameTrans NameTrans { set; get; }

        /// <summary>
        /// 数据库信息
        /// </summary>
        public ISqlInfo SqlInfo { set; get; }

        /// <summary>
        /// 数据库类型转编程语言类型
        /// </summary>
        public IDbTypeTrans DbTypeTrans { set; get; }

        /// <summary>
        /// 返回生成模板所需entity对象
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <returns></returns>
        public Entity GenTemplateEntity(string tableName)
        {
            NameTransService nameTransService = new NameTransService();
            Dictionary<string, string> commentMap = SqlInfo.CommentMap(tableName);
            string tableNodes = SqlInfo.TableNodes(tableName);
            string keyCol = SqlInfo.KeyCol(tableName);
            DataTable dataTable = SqlInfo.Columns(tableName);
            if (dataTable.Rows.Count == 0)
            {
                throw new Exception("找不到该数据表！");
            }
            string entityName = NameTrans.TableNameToEntityName(tableName);
            Entity entity = new Entity
            {
                TableName = tableName,
                EntityName = entityName,
                EntityNames = nameTransService.SingleToComplex(entityName),
                EntityNotes = tableNodes,
                KeyCol= keyCol,
                KeyName = NameTrans.ColNameToPropName(keyCol)
            };
            foreach (DataRow cl in dataTable.Rows)
            {
                string colName = (string)cl.ItemArray[3];
                string dbType = (string)cl.ItemArray[SqlInfo.ColTypeInfoIndex()];
                string srcPropName = NameTrans.ColNameToPropName(colName);
                entity.PropList.Add(new Prop
                {
                    IsKey= colName==keyCol,
                    ColName = colName,
                    SrcPropName= srcPropName,
                    PropName = srcPropName,
                    CapUpperPropName= nameTransService.HumpToBigHump(srcPropName),
                    PropType = DbTypeTrans.SqlType2EntityType(dbType),
                    PropNotes = commentMap.ContainsKey(colName)?commentMap[colName]:"",
                    ParamsType= "equal",
                    IsDeleteProp = DbTypeTrans.ColIsDeleteProp(colName)
                });
                string paramsType= DbTypeTrans.SqlType2ParamsType(dbType);
                if (paramsType == "range")
                {
                    entity.PropList.Add(new Prop
                    {
                        IsKey = colName == keyCol,
                        ColName = colName,
                        SrcPropName= srcPropName,
                        CapUpperPropName = nameTransService.HumpToBigHump(srcPropName + "Start"),
                        PropName = srcPropName + "Start",
                        PropType = DbTypeTrans.SqlType2EntityType(dbType),
                        PropNotes = commentMap.ContainsKey(colName) ? commentMap[colName] : "",
                        ParamsType="rangeStart"
                    });
                    entity.PropList.Add(new Prop
                    {
                        IsKey = colName == keyCol,
                        ColName = colName,
                        SrcPropName= srcPropName,
                        CapUpperPropName = nameTransService.HumpToBigHump(srcPropName + "End"),
                        PropName = srcPropName + "End",
                        PropType = DbTypeTrans.SqlType2EntityType(dbType),
                        PropNotes = commentMap.ContainsKey(colName) ? commentMap[colName] : "",
                        ParamsType = "rangeEnd"
                    });
                }
                else if (paramsType == "like")
                {
                    entity.PropList.Add(new Prop
                    {
                        IsKey = colName == keyCol,
                        ColName = colName,
                        SrcPropName= srcPropName,
                        CapUpperPropName = nameTransService.HumpToBigHump(srcPropName + "Like"),
                        PropName = srcPropName + "Like",
                        PropType = DbTypeTrans.SqlType2EntityType(dbType),
                        PropNotes = commentMap.ContainsKey(colName) ? commentMap[colName] : "",
                        ParamsType = "like"
                    });
                }
                if (DbTypeTrans.SqlTypeIsChangeType(dbType))
                {
                    entity.PropList.Add(new Prop
                    {
                        ColName=colName,
                        SrcPropName = srcPropName,
                        CapUpperPropName = nameTransService.HumpToBigHump(srcPropName + "Change"),
                        PropName = srcPropName + "Change",
                        PropType = DbTypeTrans.SqlType2EntityType(dbType),
                        PropNotes = commentMap.ContainsKey(colName) ? commentMap[colName] : "",
                        ParamsType = "change"
                    });
                }
            }
            return entity;
        }
    }
}
