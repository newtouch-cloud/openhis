namespace Newtouch.Domain.DTO
{
    /// <summary>
    /// 集成辩证论治
    /// </summary>
    public class GetAeRequestDto
    {
        /// <summary>
        /// 门诊号 接诊编号
        /// </summary>
        public string mzh { get; set; }

        /// <summary>
        /// 医生工号：即医生登录系统的用户名
        /// </summary>
        public string jzys { get; set; }

        /// <summary>
        /// 组织结构ID
        /// </summary>
        public string organizeId { get; set; }
    }
}
