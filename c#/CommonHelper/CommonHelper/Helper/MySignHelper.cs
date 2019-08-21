using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CommonHelper.Helper
{
    /// <summary>
    /// 专用的签名帮助类，仅在此项目专用，不通用。
    /// </summary>
    public class MySignHelper
    {
        private MySignHelper() { }

        /// <summary>
        /// 签名的key
        /// </summary>
        string SignKey { set; get; }

        /// <summary>
        /// 签名的密钥
        /// </summary>
        string SignSecret { set; get; }

        /// <summary>
        /// 签名参数组成的键值对
        /// </summary>
        Dictionary<string,string> KeyValItemMap { set; get; }

        /// <summary>
        /// 创建签名实例
        /// </summary>
        /// <param name="signKey">签名key</param>
        /// <param name="signSecret">签名密钥</param>
        public static MySignHelper New(string signKey, string signSecret)
        {
            return new MySignHelper
            {
                SignKey = signKey,
                SignSecret = signSecret,
                KeyValItemMap = new Dictionary<string, string>()
            }.Add("createDate", DateTime.Now.ToString("yyyyMMddHHmmss"))
            .Add("r", Guid.NewGuid().ToString());
        }

        /// <summary>
        /// 添加键值对
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public MySignHelper Add(string key,string val)
        {
            if (KeyValItemMap.ContainsKey(key))
            {
                KeyValItemMap[key] = val;
            }
            else
            {
                KeyValItemMap.Add(key, val);
            }
            return this;
        }

        /// <summary>
        /// 生成请求用的参数
        /// </summary>
        /// <returns></returns>
        public Dictionary<string,string> Params()
        {
            List<string> signParamList = new List<string>();
            Dictionary<string, string> retMap = new Dictionary<string, string>();
            foreach (KeyValuePair<string,string> keyVal in KeyValItemMap)
            {
                signParamList.Add(keyVal.Key+"="+ keyVal.Value);
                retMap.Add(keyVal.Key, keyVal.Value);
            }
            retMap.Add("signKey", SignKey);
            retMap.Add("signChar", EncrypHelper.GetSha1FromStr($"{string.Join("&", signParamList)}&signKey={SignKey}&signSecret={SignSecret}"));
            return retMap;
        }
    }
}