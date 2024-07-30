using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Herp.Infrastructure.Common
{
    /// <summary>
    /// 工具类
    /// </summary>
    public static class Tools
    {
        /// <summary>
        /// 过滤SQL语句,防止注入
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string FilterSql(this string s)
        {
            s = s.ToLower().Trim();
            s = s.Replace("exec", "");
            s = s.Replace("delete", "");
            s = s.Replace("select", "");
            s = s.Replace("update", "");
            s = s.Replace("master", "");
            s = s.Replace("truncate", "");
            s = s.Replace("declare", "");
            s = s.Replace("create", "");
            s = s.Replace("xp_", "no");
            return s;
        }

        /// <summary>
        /// 获取文件的扩展名
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetFileNameExt(this string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName) || fileName.IndexOf(".") < 0)
            {
                return "";
            }

            return fileName.Substring(fileName.IndexOf("."), fileName.Length - fileName.IndexOf("."));
        }
    }
}
