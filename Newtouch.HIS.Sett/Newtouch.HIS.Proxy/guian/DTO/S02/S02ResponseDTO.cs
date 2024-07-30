namespace Newtouch.HIS.Proxy.guian.DTO.S02
{
    /// <summary>
    /// 客户端传入用户名密码进行身份认证返回票据代码，密码需要MD5加密32位大写 response dto
    /// </summary>
    public class S02ResponseDTO
    {
        /// <summary>
        /// 票据单号
        /// </summary>
        public string billCode { get; set; }

        /// <summary>
        /// 用户属性
        /// </summary>
        public UserProp userProp { get; set; }
    }

    /// <summary>
    /// 用户属性
    /// </summary>
    public class UserProp
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string userId { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// 用户账号
        /// </summary>
        public string userAccount { get; set; }

        /// <summary>
        /// 机构ID
        /// </summary>
        public string orgId { get; set; }

        /// <summary>
        /// 机构名称
        /// </summary>
        public string orgName { get; set; }

        /// <summary>
        /// is Manager
        /// </summary>
        public string isManager { get; set; }

        /// <summary>
        /// 行政编码
        /// </summary>
        public string areaCode { get; set; }

        /// <summary>
        /// 行政名称
        /// </summary>
        public string areaName { get; set; }

        /// <summary>
        /// 机构级别
        /// </summary>
        public string orgLevel { get; set; }

    }
}
