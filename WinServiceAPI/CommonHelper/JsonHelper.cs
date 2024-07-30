using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper
{
    public class JsonHelper
    {
        /// <summary>
		/// 格式化json字符串
		/// </summary>
		/// <param name="sourceStr">要过滤的源字符串</param>
		/// <returns>返回过滤的字符串</returns>
		public static string JsonCharFilter(string i_html)
        {
            return i_html = i_html.Replace("\\", "\\\\");
        }

        /// <summary>  
        /// DataTable转换成Json格式  
        /// </summary>  
        /// <param name="dtSource">DataTable数据源</param>  
        /// <returns>json格式的数据表</returns>  
        public static string ToJson(DataTable dtSource)
        {
            if (dtSource == null || dtSource.Rows.Count == 0) return string.Empty;

            StringBuilder jsonBuilder = new StringBuilder();

            for (int rowIndex = 0; rowIndex < dtSource.Rows.Count; rowIndex++)
            {
                if (rowIndex == 0)
                {
                    jsonBuilder.Append("[");
                }

                jsonBuilder.Append("{");

                for (int columnsIndex = 0; columnsIndex < dtSource.Columns.Count; columnsIndex++)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(dtSource.Columns[columnsIndex].ColumnName);
                    string itemContent = dtSource.Rows[rowIndex][columnsIndex].ToString().Replace("\"", "\\\"").Replace("\r\n", "\\r\\n");

                    if (dtSource.Columns[columnsIndex].DataType == Type.GetType("System.Int32"))
                    {
                        jsonBuilder.Append("\":");
                        jsonBuilder.Append(itemContent);
                        jsonBuilder.Append(",");
                    }
                    else
                    {
                        jsonBuilder.Append("\":\"");
                        jsonBuilder.Append(itemContent);
                        jsonBuilder.Append("\",");
                    }
                }

                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("},");

            }

            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");

            return jsonBuilder.ToString();
        }

        /// <summary>
        /// json转对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static T ObjectFromJson<T>(string jsonString)
        {
            if (string.IsNullOrEmpty(jsonString))
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        /// <summary>
        /// Json对象转换为JArray对象
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static JArray JsonToObject(string value)
        {
            return (JArray)JsonConvert.DeserializeObject(value);
        }

        /// <summary>
        /// 将数据表转换成JSON类型串
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static string DataTableToJSON(System.Data.DataTable dataTable, int totalCount)
        {
            StringBuilder jsonSb = new StringBuilder();
            string t_json_data = "{\"total\":" + totalCount.ToString() + ",";
            jsonSb.Append(t_json_data);

            jsonSb.Append(DataTableToJSON(dataTable, true, true).ToString());

            jsonSb.Append("}");
            return jsonSb.ToString();
        }

        /// <summary>
        /// 将数据表转换成JSON类型串
        /// </summary>
        /// <param name="dt">要转换的数据表</param>
        /// <returns></returns>
        public static string DataTableToJSON(System.Data.DataTable dataTable)
        {
            string jsonData = DataTableToJSON(dataTable, true, false).ToString();
            if (!string.IsNullOrEmpty(jsonData))
                return string.Format("[{0}]", jsonData);

            return "[]";
        }

        /// <summary>
        /// 将数据表转换成JSON类型串
        /// </summary>
        /// <param name="dt">要转换的数据表</param>
        /// <param name="dispose">数据表转换结束后是否dispose掉</param>
        /// <returns></returns>
        public static string DataTableToJSON(System.Data.DataTable dataTable, bool disposeDataTable, bool isHaveTotalCount)
        {
            StringBuilder jsonSb = new StringBuilder();
            if (isHaveTotalCount == true) //一条数据
            {
                jsonSb.Append("\"rows\":[\r\n");
            }

            //数据表字段名和类型数组
            string[] dataTableFields = new string[dataTable.Columns.Count];
            int index = 0;
            string formatString = "{{";
            string fieldType = "";
            foreach (System.Data.DataColumn t_data_colume in dataTable.Columns)
            {
                dataTableFields[index] = t_data_colume.Caption.ToLower().Trim();
                formatString += "\"" + t_data_colume.Caption.ToLower().Trim() + "\":";
                fieldType = t_data_colume.DataType.ToString().Trim().ToLower();
                if (fieldType.IndexOf("int") > 0 || fieldType.IndexOf("deci") > 0 ||
                    fieldType.IndexOf("floa") > 0 || fieldType.IndexOf("doub") > 0 ||
                    fieldType.IndexOf("bool") > 0)
                {
                    formatString += "{" + index + "}";
                }
                else
                {
                    formatString += "\"{" + index + "}\"";
                }
                formatString += ",";
                index++;
            }

            if (formatString.EndsWith(","))
                formatString = formatString.Substring(0, formatString.Length - 1);//去掉尾部","号

            formatString += "}},";

            index = 0;
            object[] objectArray = new object[dataTableFields.Length];
            foreach (System.Data.DataRow dr in dataTable.Rows)
            {

                foreach (string fieldname in dataTableFields)
                {   //对 \ , ' 符号进行转换 
                    objectArray[index] = dr[dataTableFields[index]].ToString().Trim();
                    switch (objectArray[index].ToString())
                    {
                        case "True":
                            {
                                objectArray[index] = "true"; break;
                            }
                        case "False":
                            {
                                objectArray[index] = "false"; break;
                            }
                        case "":
                            {
                                objectArray[index] = ""; break;
                            }
                        default: break;
                    }
                    index++;
                }
                index = 0;
                jsonSb.Append(string.Format(formatString, objectArray));
            }
            if (jsonSb.ToString().EndsWith(","))
                jsonSb.Remove(jsonSb.Length - 1, 1);//去掉尾部","号

            if (disposeDataTable)
                dataTable.Dispose();
            if (isHaveTotalCount == true)
            {
                return jsonSb.Append("\r\n]").ToString();
            }
            else
            {
                return jsonSb.ToString();
            }
        }

        /// <summary>
        /// 将对象转换成easyUI的json
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToEasyUIJson(object obj, int totalCount)
        {
            StringBuilder jsonSb = new StringBuilder();
            string jsonData = "{\"total\":" + totalCount.ToString() + ",";
            jsonSb.Append(jsonData);
            jsonSb.Append("\"rows\":");
            jsonSb.Append(ObjToJson(obj));
            jsonSb.Append("}");
            return jsonSb.ToString();
        }

        /// <summary>  
        /// DataSet转换成Json格式  
        /// </summary>  
        /// <param name="dsSource">DataSet数据源</param>  
        /// <returns></returns>  
        public static string ToJson(DataSet dsSource)
        {
            StringBuilder jsonResult = new StringBuilder();

            if (dsSource != null && dsSource.Tables.Count > 0)
            {
                jsonResult.Append("{");

                for (int index = 0; index < dsSource.Tables.Count; index++)
                {
                    jsonResult.Append("\"");
                    jsonResult.Append(dsSource.Tables[index].TableName);
                    jsonResult.Append("\":");
                    jsonResult.Append(ToJson(dsSource.Tables[index]));

                    if (index != dsSource.Tables.Count - 1)
                    {
                        jsonResult.Append(",");
                    }
                }
                jsonResult.Append("}");
            }
            return jsonResult.ToString();
        }

        /// <summary>
        /// 将对象转换成JSON格式数据，不支持值类型直接传入
        /// </summary>
        /// <param name="obj">需要转换的对象</param>
        /// <returns>json格式的对象</returns>
        public static string ObjToJson(object obj)
        {
            if (obj == null)
                return "null";

            Type type = obj.GetType();

            if (type == typeof(DataTable))
            {
                return ToJson((obj as DataTable));
            }
            else if (type == typeof(DataSet))
            {
                return ToJson((obj as DataSet));
            }
            else
            {
                return JsonConvert.SerializeObject(obj);
            }
        }

        /// <summary>
        /// 将JSON字符串反序列化成对象
        /// </summary>
        /// <param name="value">要反序列化的JSON字符串</param>
        /// <param name="type">类型</param>
        /// <returns>传入类型的对象</returns>
        public static object FromJson(string value, Type type)
        {
            return JsonConvert.DeserializeObject(value, type);
        }

        public static void JsonRemoveNode(string jsonStr, string node)
        {
            JObject json = JObject.Parse(jsonStr);
            JObject jsonNode = (JObject)json["input"];
            jsonNode[node].Remove();
        }
    }
}
