using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace CommonHelper.Helper.CommonJsonConverter
{
    /// <summary>
    /// 屏蔽转换器，用于屏蔽隐私信息，例如：18316541141 => 183****1141（手机号码）
    /// </summary>
    public sealed class StarStrConverter : JsonConverter
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

        /// <summary>
        /// 把一个字符串的指定数量的字符用特殊符号替换。
        /// </summary>
        /// <param name="beforeVal">替换前的字符串</param>
        /// <param name="startIndex">起始替换索引</param>
        /// <param name="len">替换长度</param>
        /// <param name="star">替换所使用的字符串</param>
        /// <returns></returns>
        private static string ConvertStar(string beforeVal, int startIndex, int? len, char star = '*')
        {
            string afterVal;
            if (beforeVal.Length < startIndex + 1)
            {
                afterVal = beforeVal;
            }
            else
            {
                if (len == null)
                {
                    len = beforeVal.Length - startIndex;
                }
                char[] afterChars = new char[beforeVal.Length];
                for (int i = 0, len_i = beforeVal.Length; i < len_i; i++)
                {
                    if (i < startIndex || i >= startIndex + len)
                    {
                        afterChars[i] = beforeVal[i];
                    }
                    else
                    {
                        afterChars[i] = star;
                    }
                }
                afterVal = new string(afterChars);
            }
            return afterVal;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(ConvertStar(Convert.ToString(value),StartIndex,Len,Star));
        }
    }
}