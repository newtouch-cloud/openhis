namespace Newtouch.Herp.Domain.DTO.InputDto
{
    /// <summary>
    /// 单据审核
    /// </summary>
    public class BillApprovalDTO
    {
        /// <summary>
        /// 单据ID
        /// </summary>
        public long djId { get; set; }

        /// <summary>
        /// 单据类型
        /// </summary>
        public int djlx { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public string shzt { get; set; }

        /// <summary>
        /// 组织机构
        /// </summary>
        public string OrganizeId { get; set; }
    }
}
