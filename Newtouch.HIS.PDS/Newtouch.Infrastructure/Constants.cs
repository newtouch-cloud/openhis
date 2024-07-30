using Newtouch.Common.Operator;
using Newtouch.Core.Redis;
using Newtouch.Infrastructure.Model;
using System;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// 
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
        /// 应用Id
        /// </summary>
        private static string _AppId;

        /// <summary>
        /// 组织机构Id（顶级，系统配置）
        /// </summary>
        public static string AppId
        {
            get
            {
                if (_AppId == null)
                {
                    _AppId = System.Configuration.ConfigurationManager.AppSettings["AppId"] as string;
                    if (string.IsNullOrWhiteSpace(_AppId))
                    {
                        //throw new Exception("AppId未配置");
                    }
                }
                return _AppId;
            }
        }

        #region 登录用户当前的药房部门信息 永不过期 不同系统之间会共享

        /// <summary>
        /// 获取当前的药剂部门（药房或药库）
        /// </summary>
        public static LoginUserCurrentYfbmModel CurrentYfbm
        {
            get
            {
                var operate = OperatorProvider.GetCurrent();
                return GetCurrentYfbm(operate.UserId);
            }
        }

        /// <summary>
        /// 获取当前的药剂部门（药房或药库）
        /// </summary>
        /// <param name="userId"></param>
        public static LoginUserCurrentYfbmModel GetCurrentYfbm(string userId)
        {
            return RedisHelper.Get<LoginUserCurrentYfbmModel>(string.Format(CacheKey.CurrentYfbmInfoEntityKey, userId)) ?? new LoginUserCurrentYfbmModel();
        }

        /// <summary>
        /// 设置当前的药剂部门（药房或药库）
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="value"></param>
        public static void SetCurrentYfbm(string userId, LoginUserCurrentYfbmModel value)
        {
            if (value == null)
            {
                RedisHelper.Remove(string.Format(CacheKey.CurrentYfbmInfoEntityKey, userId));
            }
            else
            {
                RedisHelper.Set(string.Format(CacheKey.CurrentYfbmInfoEntityKey, userId), value);
            }
        }

        /// <summary>
        /// 返回最小时间 1970-01-01 00:00:000
        /// </summary>
        public static DateTime MinDateTime
        {
            get { return Convert.ToDateTime("1970-01-01"); }
        }

        #endregion

        /// <summary>
        /// 时间格式化
        /// </summary>
        public const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";


        #region log tags

        /// <summary>
        /// 客户流水号
        /// </summary>
        public const string ClientNo = "clientNo";
        /// <summary>
        /// 医嘱ID
        /// </summary>
        public const string ZyId = "zxId";
        /// <summary>
        /// 医药代码
        /// </summary>
        public const string YpCode = "ypCode";
        /// <summary>
        /// 处方号
        /// </summary>
        public const string Cfh = "Cfh";
        /// <summary>
        /// 处方内码
        /// </summary>
        public const string Cfnm = "Cfnm";
        /// <summary>
        /// 组织机构代码
        /// </summary>
        public const string OrganizeId = "OrganizeId";
        /// <summary>
        /// 药房部门
        /// </summary>
        public const string Yfbm = "Yfbm";
        /// <summary>
        /// 当前操作员
        /// </summary>
        public const string CreatorCode = "CreatorCode";
        #endregion
    }
}
