namespace Newtouch.HIS.Domain.VO
{

    /// <summary>
    /// 取消排药信息
    /// </summary>
    public class RpCancelVo
    {
        /// <summary>
        /// 处方号
        /// </summary>
        public string cfh { get;set;}

        /// <summary>
        /// 组织机构ID
        /// </summary>
        public string OrganizeId { get; set; }
        
        /// <summary>
        /// 药房部门编码
        /// </summary>
        public string yfbmCode { get; set; }
    }
}
