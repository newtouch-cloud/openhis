using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Newtouch.HIS.Proxy
{
    public class XMLHelper
    {
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

        public static string ReplaceXmlHeader(string xml)
        {
            Regex regex = new Regex(@"^<\?xml[\s\S]*\?>");
            if (regex.IsMatch(xml))
                xml = regex.Replace(xml, "<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            return xml;
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
}
