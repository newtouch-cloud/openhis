using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Newtouch.Infrastructure
{
    public static class EnumHelper
    {
        #region 获取枚举列表

        /// <summary>
        /// 通过枚举对象获取枚举列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static List<T> GetEnumList<T>(this T value)
        {
            var list = new List<T>();
            if (value is Enum)
            {
                var valData = Convert.ToInt32((T)Enum.Parse(typeof(T), value.ToString()));
                var tps = Enum.GetValues(typeof(T));

                list.AddRange(from object tp in tps where ((int)Convert.ToInt32((T)Enum.Parse(typeof(T), tp.ToString())) & valData) == valData select (T)tp);
            }

            return list;
        }
 
        /// <summary>
        /// 通过枚举类型获取枚举列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static List<T> GetEnumList<T>()
        {
            List<T> list = Enum.GetValues(typeof(T)).OfType<T>().ToList();
            return list;
        }
         

        /// <summary>
        /// 通过枚举对象获取所有枚举
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static IEnumerable<T> GetAllItems<T>(this Enum value)
        {
            foreach (object item in Enum.GetValues(typeof(T)))
            {
                yield return (T)item;
            }
        }

        /// <summary>
        /// Gets all items for an enum type.（通过枚举类型获取所有枚举）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> GetAllItems<T>() where T : struct
        {
            foreach (object item in Enum.GetValues(typeof(T)))
            {
                yield return (T)item;
            }
        }

        #endregion

        #region dictionary
        /// <summary>
        /// Enum转Dictionary
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, string> GetEnumDic<TEnum>()
        {
            return Enum.GetValues(typeof(TEnum))
                .Cast<TEnum>()
                .ToDictionary(t => Convert.ToInt32(t), t => t.ToString());
        }
        /// <summary>
        /// 枚举转字典集合(Key是value,Value是description（如果不存在description 则是name）)
        /// </summary>
        /// <typeparam name="T">枚举类名称</typeparam>
        /// <param name="keyDefault">默认key值</param>
        /// <param name="valueDefault">默认value值</param>
        /// <returns>返回生成的字典集合</returns>
        public static Dictionary<string, object> EnumListDic<T>(string keyDefault, string valueDefault = "")
        {
            Dictionary<string, object> dicEnum = new Dictionary<string, object>();
            Type enumType = typeof(T);
            if (!enumType.IsEnum)
            {
                return dicEnum;
            }
            if (!string.IsNullOrEmpty(keyDefault)) //判断是否添加默认选项
            {
                dicEnum.Add(keyDefault, valueDefault);
            }
            string[] fieldstrs = Enum.GetNames(enumType); //获取枚举字段数组
            foreach (var item in fieldstrs)
            {
                string description = string.Empty;
                var field = enumType.GetField(item);
                object[] arr = field.GetCustomAttributes(typeof(DescriptionAttribute), true); //获取属性字段数组
                if (arr != null && arr.Length > 0)
                {
                    description = ((DescriptionAttribute)arr[0]).Description;   //属性描述
                }
                else
                {
                    description = item;  //描述不存在取字段名称
                }
                dicEnum.Add(description, (int)Enum.Parse(enumType, item));  //不用枚举的value值作为字典key值的原因从枚举例子能看出来，其实这边应该判断他的值不存在，默认取字段名称
            }
            return dicEnum;
        }

        #endregion
    }
}
