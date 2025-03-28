using System;
using Newtouch.Herp.Infrastructure.Model;
using Newtouch.Core.Redis;
using Newtouch.Common.Operator;

namespace Newtouch.Herp.Infrastructure
{
    /// <summary>
    /// 系统常量
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// 顶级组织机构Id
        /// </summary>
        private static string _topOrganizeId;

        /// <summary>
        /// 组织机构Id（顶级，系统配置）
        /// </summary>
        public static string TopOrganizeId
        {
            get
            {
                if (_topOrganizeId != null)
                {
                    return _topOrganizeId;
                }
                _topOrganizeId = System.Configuration.ConfigurationManager.AppSettings["Top_OrganizeId"];
                if (string.IsNullOrWhiteSpace(_topOrganizeId))
                {
                    throw new Exception("Top_OrganizeId未配置");
                }
                return _topOrganizeId;
            }
        }

        /// <summary>
        /// 返回最小时间 1970-01-01 00:00:000
        /// </summary>
        public static DateTime MinDateTime
        {
            get { return Convert.ToDateTime("1970-01-01"); }
        }

        /// <summary>
        /// 时间格式化
        /// </summary>
        public const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

        #region tag

        /// <summary>
        /// 组织机构代码
        /// </summary>
        public const string OrganizeId = "OrganizeId";
        /// <summary>
        /// 库房ID
        /// </summary>
        public const string KfId = "KfId";
        /// <summary>
        /// 当前操作员
        /// </summary>
        public const string CreatorCode = "CreatorCode";

        #endregion

        #region context info
        /// <summary>
        /// 应用Id
        /// </summary>
        private static string _appId;
        /// <summary>
        /// 组织机构Id（顶级，系统配置）
        /// </summary>
        public static string AppId
        {
            get
            {
                if (_appId != null) return _appId;
                _appId = System.Configuration.ConfigurationManager.AppSettings["AppId"];
                return _appId;
            }
        }

        /// <summary>
        /// 设置当前的库房
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="value"></param>
        public static void SetCurrentKf(string userId, LoginUserCurrentKfModel value)
        {
            if (value == null)
            {
                RedisHelper.Remove(string.Format(CacheKey.CurrentKfInfoEntityKey, userId));
            }
            else
            {
                RedisHelper.Set(string.Format(CacheKey.CurrentKfInfoEntityKey, userId), value);
            }
        }

        /// <summary>
        /// 获取当前的库房信息
        /// </summary>
        /// <param name="userId"></param>
        private static LoginUserCurrentKfModel GetCurrentKf(string userId)
        {
            return RedisHelper.Get<LoginUserCurrentKfModel>(string.Format(CacheKey.CurrentKfInfoEntityKey, userId)) ?? new LoginUserCurrentKfModel();
        }

        /// <summary>
        /// 获取当前的库房信息
        /// </summary>
        public static LoginUserCurrentKfModel CurrentKf
        {
            get
            {
                var operate = OperatorProvider.GetCurrent();
                return GetCurrentKf(operate.UserId);
            }
        }

        #endregion
    }
}