using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Newtouch.Infrastructure.Log;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// xml序列化扩展
    /// </summary>
    public static class XmlSerializerExt
    {
        /// <summary>
        /// object 转 xml
        /// </summary>
        /// <param name="ob"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string XmlSerialize(this object ob, Encoding encoding = null)
        {
            if (encoding == null) encoding = Encoding.UTF8;
            return XMLSerializer.Serialize(ob, ob.GetType(), encoding);
        }

        /// <summary>
        /// xml 转 object
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static T XmlDeSerialize<T>(this string xml, Encoding encoding = null)
        {
            if (encoding == null) encoding = Encoding.UTF8;
            return (T)XMLSerializer.DeSerialize(xml, typeof(T), encoding);
        }
    }

    /// <summary>
    /// xml serializer
    /// </summary>
    public class XMLSerializer
    {

        public static string Serialize(object ob, Type type)
        {
            return XMLSerializer.Serialize(ob, type, Encoding.UTF8);
        }

        public static string SerializeUTF8(object ob, Type type)
        {
            return XMLSerializer.Serialize(ob, type, Encoding.UTF8);
        }

        public static string Serialize(object ob, Type type, Encoding encode)
        {
            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    var xmlSerializer = new XmlSerializer(type);
                    var namespaces = new XmlSerializerNamespaces();
                    namespaces.Add(string.Empty, string.Empty);
                    xmlSerializer.Serialize(memoryStream, ob, namespaces);
                    return encode.GetString(memoryStream.GetBuffer()).TrimEnd(new char[1]);
                }
            }
            catch (Exception ex)
            {
                LogCore.Error("Xml Serialize error", ex);
                return "";
            }
        }

        public static object DeSerialize(string xml, Type type)
        {
            return XMLSerializer.DeSerialize(xml, type, Encoding.UTF8);
        }

        public static object DeSerializeUTF8(string xml, Type type)
        {
            return XMLSerializer.DeSerialize(xml, type, Encoding.UTF8);
        }

        public static object DeSerialize(string xml, Type type, Encoding encode)
        {
            return XMLSerializer.DeSerialize(xml, type, encode, false);
        }

        public static object DeSerialize(string xml, Type type, Encoding encode, bool needException)
        {
            try
            {
                var settings = new XmlReaderSettings
                {
                    CheckCharacters = false
                };
                using (var memoryStream = new MemoryStream(encode.GetBytes(xml)))
                {
                    using (var xmlReader = XmlReader.Create(memoryStream, settings))
                    {
                        return new XmlSerializer(type).Deserialize(xmlReader);
                    }
                }
            }
            catch (Exception ex)
            {
                LogCore.Error("Xml Deserialize error", ex);
                if (!needException)
                {
                    return null;
                }
                throw;
            }
        }

        public static void SerializeToFile(object ob, Type type, string filepath)
        {
            try
            {
                using (StreamWriter streamWriter = new StreamWriter(filepath))
                    new XmlSerializer(type).Serialize((TextWriter)streamWriter, ob);
            }
            catch (Exception ex)
            {
                LogCore.Error("Xml SerializeToFile error", ex);
            }
        }

        public static object DeSerializeFromFile(string filepath, Type type)
        {
            try
            {
                using (FileStream fileStream = new FileStream(filepath, FileMode.Open))
                {
                    return new XmlSerializer(type).Deserialize((Stream)fileStream);
                }
            }
            catch (Exception ex)
            {
                LogCore.Error("Xml DeSerializeFromFile error", ex);
                return null;
            }
        }
    }
}
