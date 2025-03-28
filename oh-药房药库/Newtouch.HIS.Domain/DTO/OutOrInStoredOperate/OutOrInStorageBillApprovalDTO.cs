using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.DTO.OutOrInStoredOperate
{
    /// <summary>
    /// 提交单据审核参数
    /// </summary>
    public class OutOrInStorageBillApprovalDTO
    {
        /// <summary>
        /// 出入库ID
        /// </summary>
        public string crkId { get; set; }

        /// <summary>
        /// 单据类型
        /// </summary>
        public int djlx { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public string shzt { get; set; }

        /// <summary>
        /// 审核员
        /// </summary>
        public string auditor { get; set; }

        /// <summary>
        /// 出入库单据明细
        /// </summary>
        public List<SysMedicineStorageIOReceiptDetailEntity> djmx { get; set; }

        /// <summary>
        /// 出入库单据主表信息
        /// </summary>
        public SysMedicineStorageIOReceiptEntity dj { get; set; }
    }
}
