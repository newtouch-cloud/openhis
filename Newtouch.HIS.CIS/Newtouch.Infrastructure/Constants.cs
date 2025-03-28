using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Common.Operator;
using Newtouch.Core.Redis;
using System;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// 组织机构Id（顶级，系统配置）
        /// </summary>
        public static string TopOrganizeId
        {
            get
            {
                return ConstantsBase.TopOrganizeId;
            }
        }

        /// <summary>
        /// 组织机构Id（顶级，系统配置）
        /// </summary>
        public static string AppId
        {
            get
            {
                return ConstantsBase.AppId;
            }
        }

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
    }
}
