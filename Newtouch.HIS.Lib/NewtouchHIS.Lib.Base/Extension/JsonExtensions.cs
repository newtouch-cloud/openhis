/*******************************************************************************
 * Copyright © 2023 Newtouch 版权所有
 * Author: Newtouch
 * Description: 
*********************************************************************************/
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using NewtouchHIS.Lib.Base.Utilities.Json;
using System.Data;

namespace NewtouchHIS.Lib.Base.Extension
{
    /// <summary>
    /// Json序列化 帮助类
    /// </summary>
    public static class Json
    {
        /// <summary>
        /// string反序列化至 object
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static object? ToJson(this string json)
        {
            return json == null ? null : JsonConvert.DeserializeObject(json);
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(this object obj)
        {
            return obj.ToJson("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="datetimeformats">日期格式</param>
        /// <returns></returns>
        public static string ToJson(this object obj, string? datetimeformats)
        {
            if (string.IsNullOrWhiteSpace(datetimeformats))
            {
                var timeConverter = new IsoDateTimeConverter { DateTimeFormat = datetimeformats };
                return JsonConvert.SerializeObject(obj, timeConverter);
            }
            else
            {
                return JsonConvert.SerializeObject(obj);
            }
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="retainProps">指定序列化字段</param>
        /// <param name="dateFormatString"></param>
        /// <returns></returns>
        public static string ToJson(this object obj, string[] retainProps, string dateFormatString = "yyyy-MM-dd HH:mm:ss")
        {
            var jsetting = new JsonSerializerSettings();
            jsetting.ContractResolver = new LimitPropsContractResolver(retainProps);
            if (!string.IsNullOrWhiteSpace(dateFormatString))
            {
                jsetting.DateFormatString = dateFormatString;
            }
            return JsonConvert.SerializeObject(obj, Formatting.Indented, jsetting);
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Json"></param>
        /// <returns></returns>
        public static T? ToObject<T>(this string Json)
        {
            return Json == null ? default : JsonConvert.DeserializeObject<T>(Json);
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static List<T>? ToList<T>(this string json)
        {
            return json == null ? null : JsonConvert.DeserializeObject<List<T>>(json);
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string TableToJson(this DataTable data)
        {
            return JsonConvert.SerializeObject(data);
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static DataTable? ToTable(this string json)
        {
            return json == null ? null : JsonConvert.DeserializeObject<DataTable>(json);
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static JObject ToJObject(this string json)
        {
            return json == null ? JObject.Parse("{}") : JObject.Parse(json.Replace("&nbsp;", ""));
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static IEnumerable<JProperty> ToJProperties(this string json)
        {
            return json.ToJObject().Properties();
        }

        /// <summary>
        /// 向json object追加item，并返回序列化后的string
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string? Merge(object obj, object content)
        {
            try
            {
                var reqStr = obj!.ToJson();
                JObject? jobject = string.IsNullOrWhiteSpace(reqStr) ? null : (JObject)JsonConvert.DeserializeObject(reqStr);
                if (content is JObject)
                {
                    jobject!.Merge(content);
                }
                else
                {
                    jobject!.Merge(JObject.FromObject(content));
                }
                return jobject.ToJson();
            }
            catch
            {
                //非json格式
                return obj == null ? "" : obj.ToString();
            }
        }
        /// <summary>
        /// 将json字符串反序列化为字典类型
        /// </summary>
        /// <typeparam name="TKey">字典key</typeparam>
        /// <typeparam name="TValue">字典value</typeparam>
        /// <param name="jsonStr">json字符串</param>
        /// <returns>字典数据</returns>
        public static Dictionary<TKey, TValue>? JsonStringToDictionary<TKey, TValue>(string jsonStr)
        {
            if (string.IsNullOrEmpty(jsonStr))
                return null;

            Dictionary<TKey, TValue> jsonDict = JsonConvert.DeserializeObject<Dictionary<TKey, TValue>>(jsonStr);
            return jsonDict;

        }
    }
}
