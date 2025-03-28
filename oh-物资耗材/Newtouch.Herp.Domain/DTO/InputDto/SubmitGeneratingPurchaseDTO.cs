using System.Collections.Generic;
using Newtouch.Herp.Domain.Entity.VEntity;

namespace Newtouch.Herp.Domain.DTO
{
    /// <summary>
    /// 提交采购
    /// </summary>
    public class SubmitGeneratingPurchaseDto
    {
        /// <summary>
        /// 采购单明细
        /// </summary>
        public List<VCgPurchaseOrderDetailEntity> cgmx { get; set; }

        /// <summary>
        /// 操作员
        /// </summary>
        public string userCode { get; set; }

        /// <summary>
        /// 组织机构ID
        /// </summary>
        public string organizeId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
    }
}