using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Model;

namespace NewtouchHIS.HangFire.UI.Utilities
{
    public static class ApiManageHelper
    {
        /// <summary>
        /// 接口中心Host
        /// </summary>
        public static string ApiManageHost
        {
            get
            {
                var host = ConfigInitHelper.SysConfig?.AppAPIHost?.SiteApiManageHost;
                if (string.IsNullOrWhiteSpace(host))
                {
                    throw new FailedException("接口中心初始化异常：请检查配置");
                }
                return host;
            }
        }
        /// <summary>
        /// 待处理消息列表
        /// </summary>
        public static string NoticeImmediatelyWaitProcApi = $"{ApiManageHost}/api/NoticeJob/NoticeImmediatelyWaitProc";
        /// <summary>
        /// 超时未读消息列表
        /// </summary>
        public static string NoticeUnReadProcApi = $"{ApiManageHost}/api/NoticeJob/NoticeUnReadProc";
        /// <summary>
        /// 消息处理成功
        /// </summary>
        public static string NoticeProcSuccessApi = $"{ApiManageHost}/api/NoticeJob/NoticeProcSuccess";
        /// <summary>
        /// 消息处理失败
        /// </summary>
        public static string NoticeProcFailedApi = $"{ApiManageHost}/api/NoticeJob/NoticeProcFailed";
        /// <summary>
        /// 消息发送成功
        /// </summary>
        public static string NoticeSendSuccessApi = $"{ApiManageHost}/api/NoticeJob/NoticeSendSuccess";
        /// <summary>
        /// 消息发送失败
        /// </summary>
        public static string NoticeSendFailedApi = $"{ApiManageHost}/api/NoticeJob/NoticeSendFailed";

    }
}
