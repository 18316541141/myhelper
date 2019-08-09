using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace WebApplication1.MyJsonConverter
{
    /// <summary>
    /// 屏蔽转换器，用于屏蔽隐私信息，例如：18316541141 => 183****1141（手机号码）
    /// </summary>
    public class StarStrConverter : JsonConverter
    {
        /// <summary>
        /// 起始屏蔽索引
        /// </summary>
        public int StartIndex { set; get; }

        /// <summary>
        /// 屏蔽的字符数量
        /// </summary>
        public int? Len { set; get; }

        /// <summary>
        /// 屏蔽用的字符
        /// </summary>
        public char Star { set; get; }

        public StarStrConverter()
        {
            Star = '*';
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(string) == objectType;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return existingValue;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            string beforeVal=Convert.ToString(value);
            string afterVal;
            if (beforeVal.Length < StartIndex + 1)
            {
                afterVal = beforeVal;
            }
            else
            {
                if (Len == null)
                {
                    Len = beforeVal.Length - StartIndex;
                }
                char[] afterChars = new char[beforeVal.Length];
                for (int i=0,len=beforeVal.Length;i<len ;i++)
                {
                    if (i < StartIndex)
                    {
                        afterChars[i] = beforeVal[i];
                    }
                    else
                    {
                        afterChars[i] = Star;
                    }
                }
                afterVal = new string(afterChars);
            }
            writer.WriteValue(beforeVal);
        }
    }
}