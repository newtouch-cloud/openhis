using System;

namespace Newtouch.Infrastructure.Common
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
            if (s.IndexOf("exec")>-1)
            {
                s = s.Replace("exec", "");
                throw new Exception("参数中不能出现[ exec ],请规范您的输入");
            }
            if (s.IndexOf("delete") > -1)
            {
                s = s.Replace("delete", "");
                throw new Exception("参数中不能出现[ delete ],请规范您的输入");
            }
            if (s.IndexOf("select") > -1)
            {
                s = s.Replace("select", "");
                throw new Exception("参数中不能出现[ select ],请规范您的输入");
            }
            if (s.IndexOf("update") > -1)
            {
                s = s.Replace("update", "");
                throw new Exception("参数中不能出现[ update ],请规范您的输入");
            }
            if (s.IndexOf("master") > -1)
            {
                s = s.Replace("master", "");
                throw new Exception("参数中不能出现[ master ],请规范您的输入");
            }
            if (s.IndexOf("truncate") > -1)
            {
                s = s.Replace("truncate", "");
                throw new Exception("参数中不能出现[ truncate ],请规范您的输入");
            }
            if (s.IndexOf("declare") > -1)
            {
                s = s.Replace("declare", "");
                throw new Exception("参数中不能出现[ declare ],请规范您的输入");
            }
            if (s.IndexOf("create") > -1)
            {
                s = s.Replace("create", "");
                throw new Exception("参数中不能出现[ create ],请规范您的输入");
            }
            if (s.IndexOf("xp_") > -1)
            {
                s = s.Replace("xp_", "");
                throw new Exception("参数中不能出现[ xp_ ],请规范您的输入");
            }
            if (s.IndexOf("-") > -1)
            {
                s = s.Replace("-", "");
                throw new Exception("参数中不能出现[ - ],请规范您的输入");
            }
            if (s.IndexOf("#") > -1)
            {
                s = s.Replace("#", "");
                throw new Exception("参数中不能出现[ # ],请规范您的输入");
            }
            if (s.IndexOf("or") > -1)
            {
                s = s.Replace("or", "");
                throw new Exception("参数中不能出现[ or ],请规范您的输入");
            }
            if (s.IndexOf("and") > -1)
            {
                s = s.Replace("and", "");
                throw new Exception("参数中不能出现[ and ],请规范您的输入");
            }
            if (s.IndexOf("'") > -1)
            {
                s = s.Replace("'", "");
                throw new Exception("参数中不能出现[ ' ],请规范您的输入");
            }
            return s;
        }
    }
}
