using System.Data;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Newtouch.Core.Common.Extensions;
using System.Xml;
using Newtouch.Core.Common.Utils;
using System;
using System.Reflection;
using System.Linq;
using Newtouch.Core.Common.Security;
using Newtouch.Core.Common.Exceptions;
using System.Xml.Serialization;
using System.Text.RegularExpressions;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class XmlToDtUntility
    {
        /// <summary>
        /// 是否启用加密
        /// </summary>
        private static readonly bool isEnhanced = false;
        /// <summary>
        /// 加密、解密器
        /// </summary>
        private static readonly string rigndae_passPhrase = null;
        /// <summary>
        /// 加密、解密器
        /// </summary>
        private static readonly string rigndae_initVector = null;

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static XmlToDtUntility()
        {
            isEnhanced = ConfigurationHelper.GetAppConfigBoolValue("Optima_API_Enhanced") ?? false;
            if (isEnhanced)
            {
                rigndae_passPhrase = ConfigurationHelper.GetAppConfigValue("Optima_API_passPhrase");
                rigndae_initVector = ConfigurationHelper.GetAppConfigValue("Optima_API_initVector");
            }
        }

        /// <summary>
        /// XML2 the datatable
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static DataSet XmlToDataSet(string xml)
        {
            if (string.IsNullOrWhiteSpace(xml))
            {
                return null;
            }
            try
            {
                DataSet ds = new DataSet();
                using (StringReader strXml = new StringReader(xml))
                {
                    ds.ReadXml(strXml);
                    if (ds.Tables.Count > 0)
                    {
                        return ds;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw new ReadXmlException();
            }
        }

        /// <summary>
        /// xml to list
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static IList<T> XmlToIList<T>(string xml, string tableName = "params")
        {
            var ds = XmlToDataSet(xml);
            return DataSetToIList<T>(ds, tableName);
        }

        public static IList<T> XmlToIList<T>(string xml, string rowtable, string p_TableName = "params")
        {
            var p_DataSet = XmlToDataSet(xml);
            List<T> list = new List<T>();
            int _TableIndex = -1;
            if (p_DataSet == null || p_DataSet.Tables.Count < 0)
                return null;
            if (string.IsNullOrEmpty(p_TableName))
                return null;
            for (int i = 0; i < p_DataSet.Tables.Count; i++)
            {
                // 获取Table名称在Tables集合中的索引值 
                if (p_DataSet.Tables[i].TableName.Equals(rowtable))
                {
                    _TableIndex = i;
                    list.AddRange(DataSetToIList<T>(p_DataSet, _TableIndex));
                }
            }
            if (_TableIndex == -1)
            {
                return null;
            }
            return list;
        }

        /// <summary> 
        /// DataSet装换为泛型集合 
        /// </summary> 
        /// <typeparam name="T"></typeparam> 
        /// <param name="p_DataSet">DataSet</param> 
        /// <param name="p_TableName">待转换数据表名称</param> 
        /// <returns></returns> 
        public static IList<T> DataSetToIList<T>(DataSet p_DataSet, string p_TableName)
        {
            int _TableIndex = -1;
            if (p_DataSet == null || p_DataSet.Tables.Count < 0)
                return null;
            if (string.IsNullOrEmpty(p_TableName))
                return null;
            for (int i = 0; i < p_DataSet.Tables.Count; i++)
            {
                // 获取Table名称在Tables集合中的索引值 
                if (p_DataSet.Tables[i].TableName.Equals(p_TableName))
                {
                    _TableIndex = i;
                    break;
                }
            }
            if (_TableIndex == -1)
            {
                return null;
            }
            return DataSetToIList<T>(p_DataSet, _TableIndex);
        }

        /// <summary> 
        /// DataSet装换为泛型集合 
        /// </summary> 
        /// <typeparam name="T"></typeparam> 
        /// <param name="dataSet">DataSet</param> 
        /// <param name="tableIndex">待转换数据表索引</param> 
        /// <returns></returns>
        private static IList<T> DataSetToIList<T>(DataSet dataSet, int tableIndex = 0)
        {
            if (dataSet == null || dataSet.Tables.Count < 0)
            {
                return null;
            }
            if (tableIndex > dataSet.Tables.Count - 1)
            {
                return null;
            }
            if (tableIndex < 0)
            {
                tableIndex = 0;
            }

            DataTable dt = dataSet.Tables[tableIndex];
            // 返回值初始化 
            IList<T> result = new List<T>();
            RijndaelEnhanced rigndae = null;
            if (isEnhanced)
            {
                rigndae = new RijndaelEnhanced(rigndae_passPhrase, rigndae_initVector);
            }
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                T _t = (T)Activator.CreateInstance(typeof(T));
                PropertyInfo[] propertys = _t.GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        // 属性与字段名称一致的进行赋值   //忽略大小写
                        if (pi.Name.ToLower().Equals(dt.Columns[i].ColumnName.ToLower()))
                        {
                            //得套try catch
                            try
                            {
                                var str = string.Empty;
                                // 数据库NULL值单独处理 
                                if (dt.Rows[j][i] != DBNull.Value)
                                {
                                    str = dt.Rows[j][i].ToString().Trim();  //需要.Trim
                                    var isEnhanceIgnore = pi.GetCustomAttributes(true).Any(a => a is EncryptDecryptIgnorePropertyAttribute);
                                    if (isEnhanced && !isEnhanceIgnore)
                                    {
                                        if (!string.IsNullOrWhiteSpace(str))
                                        {
                                            str = str.Decrypt(rigndae); //先做解密处理
                                        }
                                    }
                                }

                                if (!string.IsNullOrWhiteSpace(str))
                                {
                                    if (Type.GetTypeCode(pi.PropertyType) == TypeCode.String)
                                    {
                                        pi.SetValue(_t, str, null);
                                    }
                                    else if (Type.GetTypeCode(pi.PropertyType) == TypeCode.Int32)
                                    {
                                        pi.SetValue(_t, Convert.ToInt32(str), null);
                                    }
                                    else if (Type.GetTypeCode(pi.PropertyType) == TypeCode.DateTime)
                                    {
                                        pi.SetValue(_t, Convert.ToDateTime(str), null);
                                    }
                                    else if (Type.GetTypeCode(pi.PropertyType) == TypeCode.Decimal)
                                    {
                                        pi.SetValue(_t, Convert.ToDecimal(str), null);
                                    }
                                    else if (Type.GetTypeCode(pi.PropertyType) == TypeCode.Boolean)
                                    {
                                        pi.SetValue(_t, Convert.ToBoolean(str), null);
                                    }
                                    else if (pi.PropertyType.GenericTypeArguments != null && pi.PropertyType.GenericTypeArguments.Count() > 0)
                                    {
                                        if (Type.GetTypeCode(pi.PropertyType.GenericTypeArguments[0]) == TypeCode.Int32)
                                        {
                                            pi.SetValue(_t, Convert.ToInt32(str), null);
                                        }
                                        else if (Type.GetTypeCode(pi.PropertyType.GenericTypeArguments[0]) == TypeCode.DateTime)
                                        {
                                            if (pi.GetCustomAttributes(true).Any(a => a is DateTimeAttribute))
                                            {
                                                //为了后面验证    这里传递了一个错误的格式
                                                pi.SetValue(_t, DateTime.MinValue, null);
                                            }
                                            pi.SetValue(_t, Convert.ToDateTime(str), null);
                                        }
                                        else if (Type.GetTypeCode(pi.PropertyType.GenericTypeArguments[0]) == TypeCode.Decimal)
                                        {
                                            pi.SetValue(_t, Convert.ToDecimal(str), null);
                                        }
                                        else if (Type.GetTypeCode(pi.PropertyType.GenericTypeArguments[0]) == TypeCode.Boolean)
                                        {
                                            pi.SetValue(_t, Convert.ToBoolean(str), null);
                                        }
                                    }

                                }
                            }
                            catch (RijndaelDecrptyException decrptyException)
                            {
                                throw decrptyException;
                            }
                            catch (Exception ex)
                            {

                            }
                            break;
                        }
                    }
                }
                result.Add(_t);
            }
            return result;
        }

        /// <summary>
        /// 将List对象转换成XML字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="is_Attribute"></param>
        /// <param name="rowcount"></param>
        /// <returns></returns>
        public static string ListToXml<T>(IList<T> list, bool is_Attribute = false, int rowcount = 0)
        {
            if (list == null || list.Count == 0)
            {
                return string.Empty;
            }
            RijndaelEnhanced rigndae = null;
            if (isEnhanced)
            {
                rigndae = new RijndaelEnhanced(rigndae_passPhrase, rigndae_initVector);
            }
            StringBuilder builder = new StringBuilder();
            if (is_Attribute)
            {
                builder.Append("<rows>");
                foreach (var item in list)
                {
                    builder.Append("<row ");
                    var props = typeof(T).GetPropertyList();
                    foreach (var prop in props)
                    {
                        var propVal = prop.GetValue(item);
                        var propValStr = propVal == null ? "" : propVal.ToString();

                        if (!string.IsNullOrWhiteSpace(propValStr))
                        {
                            //日期转字符串格式
                            var isDateString = prop.GetCustomAttributes(true).Any(a => a is DateTimeStringAttribute);
                            if (isDateString)
                            {
                                propValStr = (DateTime.Parse(propValStr)).ToString("yyyyMMddHH:mm:ss");
                            }

                            var isEnhanceIgnore = prop.GetCustomAttributes(true).Any(a => a is EncryptDecryptIgnorePropertyAttribute);
                            if (isEnhanced && !isEnhanceIgnore)
                            {
                                //值做加密处理
                                propValStr = rigndae.Encrypt(propValStr);
                            }
                        }

                        builder.Append(string.Format(" {0}='{1}' ", prop.Name, propValStr));
                    }

                    builder.Append(" />");
                }
                builder.Append("</rows>");
            }
            else
            {
                string totlcpount = rowcount == -1 ? "" : "totalrowcount='" + rowcount + "'";
                builder.Append("<root><body><result flag='1' msg='' " + totlcpount + " /><rows>");
                foreach (var item in list)
                {
                    builder.Append("<" + "row" + ">");
                    var props = typeof(T).GetPropertyList();
                    foreach (var prop in props)
                    {
                        var propVal = prop.GetValue(item);
                        var propValStr = propVal == null ? "" : propVal.ToString();

                        if (!string.IsNullOrWhiteSpace(propValStr))
                        {
                            //日期转字符串格式
                            var isDateString = prop.GetCustomAttributes(true).Any(a => a is DateTimeStringAttribute);
                            if (isDateString)
                            {
                                propValStr = (DateTime.Parse(propValStr)).ToString("yyyyMMddHH:mm:ss");
                            }

                            var isEnhanceIgnore = prop.GetCustomAttributes(true).Any(a => a is EncryptDecryptIgnorePropertyAttribute);
                            if (isEnhanced && !isEnhanceIgnore)
                            {
                                //值做加密处理
                                propValStr = rigndae.Encrypt(propValStr);
                            }
                        }

                        builder.Append(string.Format("<{0}>{1}</{2}>", prop.Name, propValStr, prop.Name));
                    }
                    builder.Append("</" + "row" + ">");
                }
                builder.Append("</rows></body></root>");
            }
            return builder.ToString();
        }

        /// <summary>
        /// 返回结果的字符结果
        /// </summary>
        /// <param name="resultmsg"></param>
        /// <param name="msgcontent"></param>
        /// <param name="rowcount"></param>
        /// <returns></returns>
        public static string GetResultMsg(string resultmsg, string msgcontent, int rowcount = 0)
        {
            StringBuilder strb = new StringBuilder();
            string totlcpount = rowcount == -1 ? "" : "totalrowcount='" + rowcount + "'";
            string flagstr = resultmsg.ToLower().ToString() == "ok" ? "1" : "0";
            strb.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
            strb.AppendLine("<root>");
            strb.AppendLine("<body><result flag='" + flagstr + "' msg='" + msgcontent + "' " + totlcpount + " /></body>");
            strb.AppendLine("</root>");
            return strb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="outxml"></param>
        /// <returns></returns>
        public static XmlDocument StrToXml(string outxml)
        {
            XmlDataDocument xmlpatientinfo = new XmlDataDocument();
            xmlpatientinfo.LoadXml(outxml.ToString());
            return xmlpatientinfo;
        }

		/// <summary>
		/// 对象序列化成xml
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="Object"></param>
		/// <returns></returns>
		public static string XmlSerialize<T>(T Object)
		{
			if (Object == null || string.IsNullOrWhiteSpace(Object.ToString()))
			{
				return "";
			}
			using (MemoryStream ms = new MemoryStream())
			{
				StreamWriter writer = new StreamWriter(ms);
				XmlSerializer serializer = new XmlSerializer(typeof(T));

				//设置命名空间
				XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
				ns.Add(string.Empty, string.Empty);

				try
				{
					//序列化对象
					serializer.Serialize(writer, Object, ns);
				}
				catch (Exception ex)
				{
					throw ex;
				}
				finally
				{
					writer.Close();
				}
				//return cleanStringEmpty(Encoding.UTF8.GetString(ms.ToArray()));
				return ReplaceXmlHeader(cleanStringEmpty(Encoding.UTF8.GetString(ms.ToArray())));
			}
		}
		public static string ReplaceXmlHeader(string xml)
		{
			Regex regex = new Regex(@"^<\?xml[\s\S]*\?>");
			if (regex.IsMatch(xml))
				xml = regex.Replace(xml, "<?xml version=\"1.0\" encoding=\"utf-8\"?>");
			return xml;
		}
		private static string cleanStringEmpty(string str)
		{
			if (!string.IsNullOrEmpty(str))
			{
				StringBuilder sb = new StringBuilder();
				string[] newStr = str.ToString().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
				for (int i = 0; i < newStr.Length; i++)
				{
					sb.Append(newStr[i].Trim());
				}
				return sb.ToString();
			}
			else
			{
				return null;
			}
		}
		/// <summary>
		/// XML String 反序列化成对象
		/// </summary>
		public static T XmlDeserialize<T>(string xmlString)
		{
			T t = default(T);

			XmlDocument xmlDoc = new XmlDocument();
			try
			{
				xmlDoc.LoadXml(xmlString);
			}
			catch
			{
				return t;
			}
			if (xmlDoc.ChildNodes[0].Name.ToLower() != "result")
				return t;

			XmlNode nodeOutput = xmlDoc.ChildNodes[0];
			if (nodeOutput.Name.ToLower() == "result")
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
				using (Stream xmlStream = new MemoryStream(Encoding.UTF8.GetBytes(nodeOutput.OuterXml)))
				{
					using (XmlReader xmlReader = XmlReader.Create(xmlStream))
					{
						Object obj = xmlSerializer.Deserialize(xmlReader);
						t = (T)obj;
					}
				}
			}
			return t;
		}

	}

    /// <summary>
    /// 
    /// </summary>
    public class DateTimeAttribute : Attribute
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public class DateTimeStringAttribute : Attribute
    {

    }
}