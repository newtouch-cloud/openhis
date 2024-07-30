namespace Newtouch.HIS.Domain.VO
{
    /// <summary>
    /// 门诊处方排药信息
    /// </summary>
    public class MzcfphxxVO
    {
        /// <summary>
        /// 最小单位数量
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// 批次
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// 批号
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// 处方号
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// 药品代码
        /// </summary>
        public string ypCode { get; set; }

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