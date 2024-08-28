using System;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class CacheKey
    {
        /// <summary>
        /// 当前登录的用户的药房部门信息  {0}UserId
        /// </summary>
        public static string CurrentYfbmInfoEntityKey = "string:CurrentYfbmInfo_{0}";

        /// <summary>
        /// 组织机构的用户树（得对应人员）缓存   {0}OrganizeId
        /// </summary>
        public static string OrganizeUserTreeSetKey = "set:OrganizeUserTree_{0}";

        /// <summary>
        /// 组织机构的人员树缓存   {0}OrganizeId
        /// </summary>
        public static string OrganizeStaffTreeSetKey = "set:OrganizeStaffTree_{0}";

        /// <summary>
        /// 系统业务处理 错误提示 的 code msg 映射 {0}TopOrganizeId
        /// </summary>
        public static string SysFailedCodeMessageMapListListSetKey = "set:SysFailedCodeMessageMapListList_{0}";

    }
}
