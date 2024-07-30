using System.Collections.Concurrent;
using System.Reflection;

namespace NewtouchHIS.Lib.Base.Extension
{
    /// <summary>
    /// Type扩展
    /// </summary>
    public static class TypeExtension
    {
        /// <summary>
        /// 
        /// </summary>
        private static ConcurrentDictionary<RuntimeTypeHandle, PropertyInfo[]> typesProperties = new ConcurrentDictionary<RuntimeTypeHandle, PropertyInfo[]>();

        /// <summary>
        /// 获取 Type 的 PropertyInfo List
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IList<PropertyInfo> GetPropertyList(this Type type)
        {
            PropertyInfo[] list;
            if (typesProperties.TryGetValue(type.TypeHandle, out list))
            {
                return list;
            }
            list = type.GetProperties();
            typesProperties.TryAdd(type.TypeHandle, list);
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        private static ConcurrentDictionary<RuntimeTypeHandle, ConcurrentDictionary<PropertyInfo, string>> typesPropertyInfoNamePairCollection = new ConcurrentDictionary<RuntimeTypeHandle, ConcurrentDictionary<PropertyInfo, string>>();

        /// <summary>
        /// 获取 Type 的 PropertyInfo、Name Pair Collection
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ConcurrentDictionary<PropertyInfo, string> GetPropertyInfoNamePairCollection(this Type type)
        {
            ConcurrentDictionary<PropertyInfo, string> dict;
            if (typesPropertyInfoNamePairCollection.TryGetValue(type.TypeHandle, out dict))
            {
                return dict;
            }
            dict = new ConcurrentDictionary<PropertyInfo, string>();
            //反射获取type的所有属性
            var propertyList = type.GetPropertyList();
            foreach (PropertyInfo property in propertyList)
            {
                var name = property.Name;
                var xmlIgnoreAttr = property.GetCustomAttributes(true).Where(attr => attr.GetType().Name == "XmlIgnoreAttribute").SingleOrDefault() as dynamic;
                if (xmlIgnoreAttr == null)
                {
                    var xmlElementAttr = property.GetCustomAttributes(true).Where(attr => attr.GetType().Name == "XmlElementAttribute").SingleOrDefault() as dynamic;
                    if (xmlElementAttr != null)
                    {
                        name = xmlElementAttr.ElementName;
                    }
                    if (!string.IsNullOrWhiteSpace(name) && !dict.ContainsKey(property))
                    {
                        //属于该赋值项
                        dict.TryAdd(property, name);
                    }
                }
            }
            typesPropertyInfoNamePairCollection[type.TypeHandle] = dict;
            return dict;
        }

        /// <summary>
        /// 获取对象的某个属性值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static object GetPropertyVal<T>(this object obj, string propertyName)
        {
            PropertyInfo pi = GetPropertyInfo<T>(propertyName);
            if (pi != null)
            {
                return pi.GetValue(obj, null);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据属性名称 获取PropertyInfo
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static PropertyInfo GetPropertyInfo<T>(string propertyName)
        {
            foreach (PropertyInfo prop in typeof(T).GetPropertyList())
            {
                if (prop.Name == propertyName)
                {
                    return prop;
                }
            }
            return null;
        }

        /// <summary>
        /// 给属性赋值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <param name="strVal"></param>
        public static void SetPropertyVal<T>(this T obj, string propertyName, string strVal)
             where T : class
        {
            PropertyInfo pi = GetPropertyInfo<T>(propertyName);

            if (pi != null)
            {
                SetPropertyVal(obj, pi, strVal);
            }
            else
            {
                throw new Exception(string.Format("类型{0}未找到属性{1}", typeof(T).FullName, propertyName));
            }
        }

        public static PropertyInfo GetPropertyByValue(this object obj, object value)
        {
            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (property.GetValue(obj) != null && property.GetValue(obj)!.Equals(value))
                {
                    return property;
                }
            }

            return null;
        }
        public static PropertyInfo GetPropertyByValue(this object obj, string value)
        {
            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (property.GetValue(obj) != null && property.GetValue(obj).ToString().Equals(value,StringComparison.OrdinalIgnoreCase))
                {
                    return property;
                }
            }

            return null;
        }

        /// <summary>
        /// 给属性赋值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="property"></param>
        /// <param name="strVal"></param>
        public static void SetPropertyVal(this object obj, PropertyInfo property, string strVal)
        {
            if (obj == null || property == null || strVal == null)
            {
                return;
            }
            if (Type.GetTypeCode(property.PropertyType) == TypeCode.String)
            {
                property.SetValue(obj, strVal);
            }
            else if (Type.GetTypeCode(property.PropertyType) == TypeCode.Int32)
            {
                Int32 objVal = 0;
                if (Int32.TryParse(strVal, out objVal))
                {
                    property.SetValue(obj, objVal);
                }
            }
            else if (Type.GetTypeCode(property.PropertyType) == TypeCode.Int64)
            {
                Int64 objVal = 0;
                if (Int64.TryParse(strVal, out objVal))
                {
                    property.SetValue(obj, objVal);
                }
            }
            else if (Type.GetTypeCode(property.PropertyType) == TypeCode.DateTime)
            {
                DateTime objVal = DateTime.MinValue;
                if (DateTime.TryParse(strVal, out objVal))
                {
                    property.SetValue(obj, objVal);
                }
            }
            else if (Type.GetTypeCode(property.PropertyType) == TypeCode.Double)
            {
                Double objVal = 0;
                if (Double.TryParse(strVal, out objVal))
                {
                    property.SetValue(obj, objVal);
                }
            }
            else if (Type.GetTypeCode(property.PropertyType) == TypeCode.Decimal)
            {
                Decimal objVal = 0;
                if (Decimal.TryParse(strVal, out objVal))
                {
                    property.SetValue(obj, objVal);
                }
            }
            else if (Type.GetTypeCode(property.PropertyType) == TypeCode.Single)
            {
                Single objVal = 0;
                if (Single.TryParse(strVal, out objVal))
                {
                    property.SetValue(obj, objVal);
                }
            }
            else if (property.PropertyType.IsGenericType
&& property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                //Nullable
                if (Type.GetTypeCode(property.PropertyType.GetGenericArguments()[0]) == TypeCode.Int32)
                {
                    Int32? objVal = null;
                    Int32 i = 0;
                    if (Int32.TryParse(strVal, out i))
                    {
                        objVal = i;
                        property.SetValue(obj, objVal);
                    }
                }
                else if (Type.GetTypeCode(property.PropertyType.GetGenericArguments()[0]) == TypeCode.Int64)
                {
                    Int64? objVal = null;
                    Int64 i = 0;
                    if (Int64.TryParse(strVal, out i))
                    {
                        objVal = i;
                        property.SetValue(obj, objVal);
                    }
                }
                if (Type.GetTypeCode(property.PropertyType.GetGenericArguments()[0]) == TypeCode.DateTime)
                {
                    DateTime? objVal = null;
                    DateTime i = DateTime.MinValue;
                    if (DateTime.TryParse(strVal, out i))
                    {
                        objVal = i;
                        property.SetValue(obj, objVal);
                    }
                }
                if (Type.GetTypeCode(property.PropertyType.GetGenericArguments()[0]) == TypeCode.Double)
                {
                    Double? objVal = null;
                    Double i = 0;
                    if (Double.TryParse(strVal, out i))
                    {
                        objVal = i;
                        property.SetValue(obj, objVal);
                    }
                }
                if (Type.GetTypeCode(property.PropertyType.GetGenericArguments()[0]) == TypeCode.Decimal)
                {
                    Decimal? objVal = null;
                    Decimal i = 0;
                    if (Decimal.TryParse(strVal, out i))
                    {
                        objVal = i;
                        property.SetValue(obj, objVal);
                    }
                }
                if (Type.GetTypeCode(property.PropertyType.GetGenericArguments()[0]) == TypeCode.Single)
                {
                    Single? objVal = null;
                    Single i = 0;
                    if (Single.TryParse(strVal, out i))
                    {
                        objVal = i;
                        property.SetValue(obj, objVal);
                    }
                }
            }
        }

    }
}
