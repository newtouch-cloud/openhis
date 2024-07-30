using System.Collections.Generic;
using Newtouch.Herp.Domain.Entity;

namespace Newtouch.Herp.Domain.DTO.InputDto
{
    /// <summary>
    /// 单据内容
    /// </summary>
    public class DjInfDTO
    {
        /// <summary>
        /// 单据主表信息
        /// </summary>
        public KfCrkdjEntity crkdj { get; set; }

        /// <summary>
        /// 单据明细信息
        /// </summary>
        public List<KfCrkmxEntity> crkdjmx { get; set; }

        /// <summary>
        /// 申领明细
        /// </summary>
        public List<VApplyBillDetailEntity> applyBillDetail { get; set; }
    }
}
