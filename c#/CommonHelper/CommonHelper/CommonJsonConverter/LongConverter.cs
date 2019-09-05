using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonHelper.Helper.CommonJsonConverter
{
    /// <summary>
    /// 全局统一转化long类型属性为string，避免前端使用long类型溢出。
    /// </summary>
    public sealed class LongConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType==typeof(long) || objectType == typeof(long?);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            try
            {
                return Convert.ToInt64(existingValue);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(Convert.ToString(value));
        }
    }
}