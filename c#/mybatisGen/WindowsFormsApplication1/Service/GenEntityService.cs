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

namespace WindowsFormsApplication1.Service
{
    public class GenEntityService
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
                EntityNames = new NameTransService().SingleToComplex(entityName),
                EntityNotes = tableNodes,
                KeyCol= keyCol,
                KeyName = NameTrans.ColNameToPropName(keyCol)
            };
            foreach (DataRow cl in dataTable.Rows)
            {
                string colName = (string)cl.ItemArray[3];
                string dbType = (string)cl.ItemArray[7];
                string srcPropName = NameTrans.ColNameToPropName(colName);
                entity.PropList.Add(new Prop
                {
                    ColName = colName,
                    SrcPropName= srcPropName,
                    PropName = srcPropName,
                    PropType = DbTypeTrans.SqlType2EntityType(dbType),
                    PropNotes = commentMap[colName],
                    ParamsType= "equal",
                });
                string paramsType= DbTypeTrans.SqlType2ParamsType(dbType);
                if (paramsType == "range")
                {
                    entity.PropList.Add(new Prop
                    {
                        ColName = colName,
                        SrcPropName= srcPropName,
                        PropName = srcPropName + "Start",
                        PropType = DbTypeTrans.SqlType2EntityType(dbType),
                        PropNotes = commentMap[colName],
                        ParamsType="rangeStart"
                    });
                    entity.PropList.Add(new Prop
                    {
                        ColName = colName,
                        SrcPropName= srcPropName,
                        PropName = srcPropName + "End",
                        PropType = DbTypeTrans.SqlType2EntityType(dbType),
                        PropNotes = commentMap[colName],
                        ParamsType = "rangeEnd"
                    });
                }
                else if (paramsType == "like")
                {
                    entity.PropList.Add(new Prop
                    {
                        ColName = colName,
                        SrcPropName= srcPropName,
                        PropName = srcPropName + "Like",
                        PropType = DbTypeTrans.SqlType2EntityType(dbType),
                        PropNotes = commentMap[colName],
                        ParamsType = "like"
                    });
                }
            }
            return entity;
        }
    }
}
