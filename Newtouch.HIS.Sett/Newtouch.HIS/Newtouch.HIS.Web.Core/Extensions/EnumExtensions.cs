using System;
using System.Web.Mvc;
using System.Linq;
using Newtouch.Common;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Newtouch.Tools;

namespace Newtouch.HIS.Web.Core.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Name - Id
        /// </summary>
        private static ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<TextValuePair>> typesNameIdEnumerableResult
            = new ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<TextValuePair>>();

        /// <summary>
        /// Description - Id
        /// </summary>
        private static ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<TextValuePair>> typesDescIdEnumerableResult
            = new ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<TextValuePair>>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enumeration"></param>
        /// <returns></returns>
        public static SelectList ToSelectList(this Enum enumeration)
        {
            return new SelectList(getNameIdEnumrator(enumeration), "Value", "Text");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enumeration"></param>
        /// <returns></returns>
        public static SelectList ToDescSelectList(this Enum enumeration)
        {
            return new SelectList(getDescIdEnumrator(enumeration), "Value", "Text");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enumeration"></param>
        /// <param name="selectedValue"></param>
        /// <returns></returns>
        public static SelectList ToDescSelectList(this Enum enumeration, object selectedValue)
        {
            return new SelectList(getDescIdEnumrator(enumeration), "Value", "Text", selectedValue);
        }

        /// <summary>
        /// 提供部分选项
        /// </summary>
        /// <param name="enumeration"></param>
        /// <param name="someValues"></param>
        /// <returns></returns>
        public static SelectList ToSomeDescSelectList(this Enum enumeration, string someValues)
        {
            var list = getDescIdEnumrator(enumeration);
            if (!string.IsNullOrEmpty(someValues))
            {
                int[] arrValue = ArrayConvertor.Convert(someValues);
                list = list.Where(e => arrValue.Contains(e.Value)).ToList();
            }
            return new SelectList(list, "Value", "Text");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enumeration"></param>
        /// <param name="someValues"></param>
        /// <param name="selectedValue"></param>
        /// <returns></returns>
        public static SelectList ToSomeDescSelectList(this Enum enumeration, string someValues, object selectedValue)
        {
            var list = getDescIdEnumrator(enumeration);
            if (!string.IsNullOrEmpty(someValues))
            {
                int[] arrValue = ArrayConvertor.Convert(someValues);
                list = list.Where(e => arrValue.Contains(e.Value)).ToList();
            }
            return new SelectList(list, "Value", "Text", selectedValue);
        }

        #region private methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enumeration"></param>
        private static IEnumerable<TextValuePair> getNameIdEnumrator(this Enum enumeration)
        {
            IEnumerable<TextValuePair> result;
            if (typesNameIdEnumerableResult.TryGetValue(enumeration.GetType().TypeHandle, out result))
            {
                return result;
            }
            result = (from Enum d in Enum.GetValues(enumeration.GetType())
                      select new TextValuePair
                      {
                          Value = (int)Enum.Parse(enumeration.GetType(), Enum.GetName(enumeration.GetType(), d)),
                          Text = d.ToString()
                      }).ToList();
            typesNameIdEnumerableResult.TryAdd(enumeration.GetType().TypeHandle, result);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enumeration"></param>
        private static IEnumerable<TextValuePair> getDescIdEnumrator(this Enum enumeration)
        {
            IEnumerable<TextValuePair> result;
            if (typesDescIdEnumerableResult.TryGetValue(enumeration.GetType().TypeHandle, out result))
            {
                return result;
            }
            result = (from Enum d in Enum.GetValues(enumeration.GetType())
                      select new TextValuePair
                      {
                          Value = (int)Enum.Parse(enumeration.GetType(), Enum.GetName(enumeration.GetType(), d)),
                          Text = d.GetDescription()
                      }).ToList();
            typesDescIdEnumerableResult.TryAdd(enumeration.GetType().TypeHandle, result);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        private class TextValuePair
        {
            /// <summary>
            /// 
            /// </summary>
            public int Value { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string Text { get; set; }
        }

        #endregion

    }

}
