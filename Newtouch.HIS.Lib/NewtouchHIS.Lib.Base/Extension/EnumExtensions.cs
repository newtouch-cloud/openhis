using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace NewtouchHIS.Lib.Base.Extension
{
    public static class EnumExtensions
    {
        /// <summary>
        /// 获取特性 (DisplayAttribute) 的名称；如果未使用该特性，则返回枚举的名称。
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetDisplayName(this System.Enum enumValue)
        {
            FieldInfo fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
            DisplayAttribute[] attrs =
                fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false) as DisplayAttribute[];

            return attrs.Length > 0 ? attrs[0].Name : enumValue.ToString();
        }

        /// <summary>
        /// 获取特性 (DisplayAttribute) 的说明；如果未使用该特性，则返回枚举的名称。
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetDisplayDescription(this System.Enum enumValue)
        {
            FieldInfo fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
            DisplayAttribute[] attrs =
                fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false) as DisplayAttribute[];

            return attrs.Length > 0 ? attrs[0].Description : enumValue.ToString();
        }

        /// <summary>
        /// 获取特性 (DescriptionAttribute) 的说明；如果未使用该特性，则返回枚举的名称。
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetDescription(this System.Enum enumValue)
        {
            FieldInfo fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
            DescriptionAttribute[] attrs =
                fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            return attrs.Length > 0 ? attrs[0].Description : enumValue.ToString();
        }

        /// <summary>
        /// 直接获取特性（更轻量、更容易使用，不用封装“获取每一个自定义特性”的扩展方法）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static T GetAttributeOfType<T>(this System.Enum enumValue) where T : Attribute
        {
            Type type = enumValue.GetType();
            MemberInfo[] memInfo = type.GetMember(enumValue.ToString());
            object[] attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return (attributes.Length > 0) ? (T)attributes[0] : null;
        }

        /// <summary>
        /// 根据枚举类型获取枚举项信息
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static IList<EnumItemInfo> getEnumNameValueDescInfo(this Type t)
        {
            var result = (from Enum d in Enum.GetValues(t)
                          select new EnumItemInfo()
                          {
                              Name = d.ToString(),
                              Value = (int)Enum.Parse(t, Enum.GetName(t, d)),
                              Desc = d.GetDescription()
                          }).ToList();
            return result;
        }

        /// <summary>
        /// （呈现枚举下拉用）获取 SelectList Text：Name Value：Id
        /// </summary>
        /// <param name="enumeration"></param>
        /// <returns></returns>
        //public static SelectList ToSelectList(this System.Enum enumeration)
        //{
        //    return new SelectList(getNameIdEnumrator(enumeration), "Value", "Text");
        //}

        ///// <summary>
        ///// （呈现枚举下拉用）获取 SelectList Text：Desc Value：Id
        ///// </summary>
        ///// <param name="enumeration"></param>
        ///// <returns></returns>
        //public static SelectList ToDescSelectList(this System.Enum enumeration)
        //{
        //    return new SelectList(getDescIdEnumrator(enumeration), "Value", "Text");
        //}

        ///// <summary>
        ///// （呈现枚举下拉用）获取 SelectList Text：Desc Value：Id
        ///// </summary>
        ///// <param name="enumeration"></param>
        ///// <param name="selectedValue">选中项</param>
        ///// <returns></returns>
        //public static SelectList ToDescSelectList(this System.Enum enumeration, object selectedValue)
        //{
        //    return new SelectList(getDescIdEnumrator(enumeration), "Value", "Text", selectedValue);
        //}

        ///// <summary>
        ///// （呈现枚举下拉用）提供部分选项  Text：Desc Value：Id
        ///// </summary>
        ///// <param name="enumeration"></param>
        ///// <param name="someValues"></param>
        ///// <returns></returns>
        //public static SelectList ToSomeDescSelectList(this System.Enum enumeration, string someValues)
        //{
        //    var list = getDescIdEnumrator(enumeration);
        //    if (!string.IsNullOrEmpty(someValues))
        //    {
        //        int[] arrValue = ArrayConvertor.Convert(someValues);
        //        list = list.Where(e => arrValue.Contains(e.Value)).ToList();
        //    }
        //    return new SelectList(list, "Value", "Text");
        //}

        ///// <summary>
        ///// （呈现枚举下拉用）提供部分选项  Text：Desc Value：Id
        ///// </summary>
        ///// <param name="enumeration"></param>
        ///// <param name="someValues"></param>
        ///// <param name="selectedValue">选中项</param>
        ///// <returns></returns>
        //public static SelectList ToSomeDescSelectList(this System.Enum enumeration, string someValues, object selectedValue)
        //{
        //    var list = getDescIdEnumrator(enumeration);
        //    if (!string.IsNullOrEmpty(someValues))
        //    {
        //        int[] arrValue = ArrayConvertor.Convert(someValues);
        //        list = list.Where(e => arrValue.Contains(e.Value)).ToList();
        //    }
        //    return new SelectList(list, "Value", "Text", selectedValue);
        //}

        #region private
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="enumeration"></param>
        //private static IEnumerable<TextValuePair> getNameIdEnumrator(this System.Enum enumeration)
        //{
        //    IEnumerable<TextValuePair> result;
        //    if (typesNameIdEnumerableResult.TryGetValue(enumeration.GetType().TypeHandle, out result))
        //    {
        //        return result;
        //    }
        //    result = (from Enum d in Enum.GetValues(enumeration.GetType())
        //              select new TextValuePair
        //              {
        //                  Value = (int)Enum.Parse(enumeration.GetType(), Enum.GetName(enumeration.GetType(), d)),
        //                  Text = d.ToString()
        //              }).ToList();
        //    typesNameIdEnumerableResult.TryAdd(enumeration.GetType().TypeHandle, result);
        //    return result;
        //}
        #endregion
        public class TextValuePair
        {
            /// <summary>
            /// Value
            /// </summary>
            public int Value { get; set; }

            /// <summary>
            /// Text
            /// </summary>
            public string Text { get; set; }
        }
        public class EnumItemInfo
        {
            public string Name { get; set; }
            public int Value { get; set; }
            public string? Desc { get; set; }

        }
    }
}