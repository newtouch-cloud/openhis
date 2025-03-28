namespace Newtouch.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class CacheKey
    {
        /// <summary>
        /// 系统业务处理 错误提示 的 code msg 映射 {0}TopOrganizeId
        /// </summary>
        public static string SysFailedCodeMessageMapListListSetKey = "set:SysFailedCodeMessageMapListList_{0}";
        /// <summary>
        /// 消息通知用户链接字典 {0} 机构代码 {1} 当前系统AppId {2} 用户
        /// </summary>
        public static string NoticeConnectionKey = "MsgNotice:{0}:{1}_{2}";
    }
}
