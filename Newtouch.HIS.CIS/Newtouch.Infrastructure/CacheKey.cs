namespace Newtouch.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class CacheKey
    {
        public static string ValidSystemDiagnosisListSetKey = "set:VDList_{0}";
        public static string ValidSysMedicalOrderFrequencyListSetKey = "set:VMOFList_{0}";
        public static string ValidSysMedicineUsageListSetKey = "set:VMUList";
        public static string ValidSysDaiJianFeeSetKey = "set:VDFSetKey_{0}";
        public static string ValidSysBodyPartsSetKey_ = "set:VBodyPSetKey_{0}";
        public static string ValidSysAuxiliaryDictionaryListSetKey = "set:VADList_{0}";
        public static string ValidSystemBwFlSetKey = "set:VBodyDlSetKey_{0}";

        /// <summary>
        /// 当前登录的用户的药房部门信息  {0}UserId
        /// </summary>
        public static string CurrentYfbmInfoEntityKey = "string:CurrentYfbmInfo_{0}";

        /// <summary>
        /// 用户登陆第三方授权token {0} 当前系统AppId {1} 机构代码 {2} 用户
        /// </summary>
        public static string AccessToken = "${0}${1}:accesstoken_{2}";
    }
}
