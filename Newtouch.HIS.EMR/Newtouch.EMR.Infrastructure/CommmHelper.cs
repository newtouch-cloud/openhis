using Newtouch.Core.Common.Exceptions;
using System;
using System.Collections.Generic;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// 帮助类
    /// </summary>
    public partial class CommmHelper
    {
        /// <summary>
        /// 将枚举转换成字典
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <returns></returns>
        public static Dictionary<string, int> GetDic<TEnum>()
        {
            Dictionary<string, int> dic = new Dictionary<string, int>();
            Type t = typeof(TEnum);
            var arr = Enum.GetValues(t);
            foreach (var item in arr)
            {
                dic.Add(item.ToString(), (int)item);
            }

            return dic;
        }
        /// <summary>
        /// 计算年龄
        /// </summary>
        /// <param name="birthDate"></param>
        /// <param name="now"></param>
        /// <returns></returns>
        public static int CalculateAgeCorrect(DateTime birthDate, DateTime now)
        {
            int age = now.Year - birthDate.Year;
            if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day)) age--;
            return age;
        }
    }
}
