using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtouch.Infrastructure.Log;
using Newtouch.Tools;

namespace Newtouch.Infrastructure.Enum
{
    /// <summary>
    /// 枚举使用
    /// </summary>
    public class EnumCommon
    {
        #region 单例
        private static readonly EnumCommon _instance = new EnumCommon();

        static EnumCommon()
        {
        }

        public static EnumCommon Instance()
        {
            return _instance;
        }

        #endregion

        /// <summary>
        /// 获取发药标志枚举描述
        /// </summary>
        /// <param name="fybz"></param>
        /// <returns></returns>
        public string GetFybzCn(string fybz)
        {
            try
            {
                var t = (EnumFybz)Convert.ToInt32(fybz);
                switch (t)
                {
                    case EnumFybz.Wp:
                        return EnumFybz.Wp.GetDescription();
                    case EnumFybz.Yf:
                        return EnumFybz.Yf.GetDescription();
                    case EnumFybz.Yp:
                        return EnumFybz.Yp.GetDescription();
                    case EnumFybz.Yt:
                        return EnumFybz.Yt.GetDescription();
                    default:
                        return "";
                }
            }
            catch (Exception ex)
            {
                LogCore.Error("EnumCommon GetFybzCn Error", ex);
                return "";
            }
        }

        /// <summary>
        /// 获取发药标志枚举描述
        /// </summary>
        /// <param name="fybz"></param>
        /// <returns></returns>
        public string GetFybzCn(int fybz)
        {
            try
            {
                var t = (EnumFybz)fybz;
                switch (t)
                {
                    case EnumFybz.Wp:
                        return EnumFybz.Wp.GetDescription();
                    case EnumFybz.Yf:
                        return EnumFybz.Yf.GetDescription();
                    case EnumFybz.Yp:
                        return EnumFybz.Yp.GetDescription();
                    case EnumFybz.Yt:
                        return EnumFybz.Yt.GetDescription();
                    default:
                        return "";
                }
            }
            catch (Exception ex)
            {
                LogCore.Error("EnumCommon GetFybzCn Error", ex);
                return "";
            }
        }

        /// <summary>
        /// 获取枚举名称集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public string[] GetNamesArr<T>()
        {
            return System.Enum.GetNames(typeof(T));
        }

        /// <summary>
        /// 将枚举转换成字典集合
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <returns></returns>
        public Dictionary<string, int> GetEnumDic<T>()
        {
            var resultList = new Dictionary<string, int>();
            var type = typeof(T);
            var strList = GetNamesArr<T>().ToList();
            foreach (var key in strList)
            {
                var val = System.Enum.Format(type, System.Enum.Parse(type, key), "d");
                resultList.Add(key, int.Parse(val));
            }
            return resultList;
        }

        /// <summary>
        /// 将枚举转换成字典
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <returns></returns>
        public Dictionary<string, int> GetEnum<TEnum>()
        {
            var t = typeof(TEnum);
            var arr = System.Enum.GetValues(t);
            return arr.Cast<object>().ToDictionary(item => item.ToString(), item => (int)item);
        }

        /// <summary>
        /// 将枚举转换成字典
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <returns></returns>
        public Dictionary<string, int> GetEnumDesAndVal<TEnum>()
        {
            var t = typeof(TEnum);
            var arr = System.Enum.GetValues(t);
            return arr.Cast<object>().ToDictionary(item => GetFybzCn((int)item), item => (int)item);
        }
    }
}
