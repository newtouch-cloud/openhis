using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.sm2sm4
{
    public class SignUtil
    {
        private static List<String> ignoreSign = new List<String>() { "signData", "encData", "extra" };

        public static String getSignText(JObject jsonObject, String appSecret)
        {
            SortedDictionary<String, String> signMap = new SortedDictionary<String, String>(StringComparer.Ordinal);

            foreach (var entry in jsonObject)
            {
                if (!String.IsNullOrEmpty(entry.Value.ToString()) && !ignoreSign.Contains(entry.Key))
                {
                    signMap.Add(entry.Key, getValue(entry.Value));
                }
            }


            List<String> list = new List<String>();

            foreach (var entry in signMap)
            {
                if (!String.IsNullOrEmpty(getObjString(entry.Value)))
                {
                    list.Add((String)entry.Key + "=" + (String)entry.Value + "&");
                }
            }

            int size = list.Count();
            String[] arrayToSort = (String[])list.ToArray();
            Array.Sort(arrayToSort, new CaseInsensitiveComparer());
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < size; ++i)
            {
                sb.Append(arrayToSort[i]);
            }

            String signText = sb.Append("key=").Append(appSecret).ToString();
            return signText;
        }

        public static String getObjString(Object obj)
        {
            return obj == null ? "" : (String)obj;
        }

        private static String getValue(Object value)
        {
            return value is String ? getObjString(value) : treeJsonParam(value);
        }

        private static String treeJsonParam(Object value)
        {
            String jsonParam = null;
            if (value is Dictionary<Object, Object>)
            {
                SortedDictionary<String, Object> treeNestedMap = new SortedDictionary<String, Object>(StringComparer.Ordinal);
                Dictionary<Object, Object> nestedMap = (Dictionary<Object, Object>)value;

                foreach (var entry in nestedMap)
                {
                    treeNestedMap.Add(entry.Key.ToString(), entry.Value);
                }
                jsonParam = JsonConvert.SerializeObject(treeParams(treeNestedMap), Formatting.None);
            }
            else if (value is List<Object>)
            {
                List<Object> ar = (List<Object>)value;
                if (ar != null && ar.Count() != 0)
                    jsonParam = JsonConvert.SerializeObject(treeList(ar), Formatting.None);
            }
            else if (value is JObject)
            {
                SortedDictionary<String, Object> treeNestedMap = new SortedDictionary<String, Object>(StringComparer.Ordinal);
                JObject nestedMap = (JObject)value;
                foreach (var entry in nestedMap)
                {
                    treeNestedMap.Add(entry.Key.ToString(), entry.Value);
                }
                jsonParam = JsonConvert.SerializeObject(treeParams(treeNestedMap), Formatting.None);
            }
            else if (value is JArray)
            {
                JArray ar = (JArray)value;
                if (ar != null && ar.Count() != 0)
                    jsonParam = JsonConvert.SerializeObject(treeJsonArray(ar), Formatting.None);
            }
            else if (value is JValue)
            {
                JValue jval = (JValue)value;
                if (jval != null && !String.IsNullOrEmpty(jval.ToString()))
                {
                    if (jval.ToString().ToLower().Trim().Equals("true") || jval.ToString().ToLower().Trim().Equals("false"))
                        jsonParam = jval.ToString().ToLower().Trim();
                    else
                        jsonParam = jval.Value.ToString();
                }


            }
            else if (value is JProperty)
            {
                SortedDictionary<String, Object> treeNestedMap = new SortedDictionary<String, Object>(StringComparer.Ordinal);
                JProperty nestedMap = (JProperty)value;
                treeNestedMap.Add(nestedMap.Name, nestedMap.Value);
                jsonParam = JsonConvert.SerializeObject(treeParams(treeNestedMap), Formatting.None);
            }
            else
            {
                jsonParam = value.ToString();
            }

            return jsonParam;
        }

        private static SortedDictionary<String, Object> treeParams(SortedDictionary<String, Object> param)
        {
            if (param == null)
            {
                return new SortedDictionary<String, Object>(StringComparer.Ordinal);
            }
            else
            {
                SortedDictionary<String, Object> treeParam = new SortedDictionary<String, Object>(StringComparer.Ordinal);

                while (true)
                {
                    foreach (var entry in param)
                    {

                        String key = (String)entry.Key;
                        Object value = entry.Value;
                        if (value is Dictionary<Object, Object>)
                        {
                            SortedDictionary<String, Object> treeNestedMap = new SortedDictionary<String, Object>(StringComparer.Ordinal);
                            Dictionary<Object, Object> nestedMap = (Dictionary<Object, Object>)value;

                            foreach (var nestedEntry in nestedMap)
                            {
                                treeNestedMap.Add(nestedEntry.Key.ToString(), nestedEntry.Value);
                            }

                            treeParam.Add(key, treeParams(treeNestedMap));
                        }
                        else if (value is List<Object>)
                        {
                            List<Object> ar = (List<Object>)value;
                            if (ar != null && ar.Count() != 0)
                                treeParam.Add(key, treeList(ar));
                        }
                        else if (value is JArray)
                        {
                            JArray ar = (JArray)value;
                            if (ar != null && ar.Count() != 0)
                                treeParam.Add(key, treeJsonArray(ar));
                        }
                        else if (value is JObject)
                        {
                            SortedDictionary<String, Object> treeNestedMap = new SortedDictionary<String, Object>(StringComparer.Ordinal);
                            JObject nestedMap = (JObject)value;
                            foreach (var nestedEntry in nestedMap)
                            {
                                treeNestedMap.Add(nestedEntry.Key.ToString(), nestedEntry.Value);
                            }
                            treeParam.Add(key, treeParams(treeNestedMap));
                        }
                        else if (value is JValue)
                        {
                            JValue jval = (JValue)value;
                            if (jval != null && !String.IsNullOrEmpty(jval.ToString()))
                            {
                                if (jval.ToString().ToLower().Trim().Equals("true") || jval.ToString().ToLower().Trim().Equals("false"))
                                    treeParam.Add(key, jval.ToString().ToLower().Trim());
                                else
                                    treeParam.Add(key, jval.ToString());
                            }
                        }
                        else if (value is JProperty)
                        {
                            SortedDictionary<String, Object> treeNestedMap = new SortedDictionary<String, Object>(StringComparer.Ordinal);
                            JProperty nestedMap = (JProperty)value;
                            treeNestedMap.Add(nestedMap.Name, nestedMap.Value);
                            treeParam.Add(key, treeParams(treeNestedMap));
                        }
                        else if (!"".Equals(value) && value != null)
                        {
                            treeParam.Add(key, value.ToString());
                        }
                    }
                    return treeParam;
                }
            }
        }

        private static List<Object> treeList(List<Object> list)
        {
            if (list != null && list.Count() != 0)
            {
                JArray jsonArray = new JArray();
                int size = list.Count();

                for (int i = 0; i < size; ++i)
                {
                    jsonArray.Add(list[i]);
                }

                return treeJsonArray(jsonArray);
            }
            else
            {
                return null;
            }
        }

        private static List<Object> treeJsonArray(JArray jarr)
        {
            if (jarr != null && jarr.Count() != 0)
            {
                List<Object> jsonArray = new List<Object>();
                int size = jarr.Count();

                for (int i = 0; i < size; ++i)
                {
                    Object value = jarr[i];
                    if (value is List<Object>)
                    {
                        List<Object> ar = (List<Object>)value;
                        if (ar != null && ar.Count() != 0)
                            jsonArray.Add(treeList(ar));
                    }
                    else if (value is JArray)
                    {
                        JArray ar = (JArray)value;
                        if (ar != null && ar.Count() != 0)
                            jsonArray.Add(treeJsonArray(ar));
                    }
                    else if (value is JObject)
                    {
                        SortedDictionary<String, Object> treeNestedMap = new SortedDictionary<String, Object>(StringComparer.Ordinal);
                        JObject nestedMap = (JObject)value;
                        foreach (var nestedEntry in nestedMap)
                        {
                            treeNestedMap.Add(nestedEntry.Key.ToString(), nestedEntry.Value);
                        }
                        jsonArray.Add(treeParams(treeNestedMap));

                    }
                    else if (value is JValue)
                    {
                        JValue jval = (JValue)value;
                        if (jval != null && !String.IsNullOrEmpty(jval.ToString()))
                        {
                            if (jval.ToString().ToLower().Trim().Equals("true") || jval.ToString().ToLower().Trim().Equals("false"))
                                jsonArray.Add(jval.ToString().ToLower().Trim());
                            else
                                jsonArray.Add(jval.ToString());
                        }
                    }
                    else if (value is JProperty)
                    {
                        SortedDictionary<String, Object> treeNestedMap = new SortedDictionary<String, Object>(StringComparer.Ordinal);
                        JProperty nestedMap = (JProperty)value;
                        treeNestedMap.Add(nestedMap.Name, nestedMap.Value);
                        jsonArray.Add(treeParams(treeNestedMap));
                    }
                    else if (!"".Equals(value))
                    {
                        jsonArray.Add(value.ToString());
                    }

                }

                return jsonArray;
            }
            else
            {
                return null;
            }
        }

    }
}
