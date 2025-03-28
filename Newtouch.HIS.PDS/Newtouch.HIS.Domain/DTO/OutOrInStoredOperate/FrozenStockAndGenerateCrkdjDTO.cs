using Newtouch.HIS.Domain.Entity.V;
using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.DTO.OutOrInStoredOperate
{
    /// <summary>
    /// 冻结库存并生成单据信息处理过程参数
    /// </summary>
    public class FrozenStockAndGenerateCrkdjDTO
    {

        /// <summary>
        /// 当前药房部门 出库部门
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// 组织机构
        /// </summary>
        public string organizeId { get; set; }

        /// <summary>
        /// 操作员
        /// </summary>
        public string userCode { get; set; }

        /// <summary>
        /// 入库部门
        /// </summary>
        public string rkbm { get; set; }

        /// <summary>
        /// 需冻结的药品信息
        /// </summary>
        public List<FrozenedMedicineInfoDTO> frozenseMedicines { get; set; }
    }

    /// <summary>
    /// 需冻结的药品信息
    /// </summary>
    public class FrozenedMedicineInfoDTO
    {

        /// <summary>
        /// 总共需要冻结的数量  最小单位
        /// </summary>
        public int djsl { get; set; }

        /// <summary>
        /// 药品代码
        /// </summary>
        public string ypdm { get; set; }

        /// <summary>
        /// 申领单明细ID
        /// </summary>
        public string sldmxId { get; set; }

        /// <summary>
        /// 转化因子 目前为最小单位转化因子
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// 包装数
        /// </summary>
        public int bzs { get; set; }

        /// <summary>
        /// 可用库存 最小单位
        /// </summary>
        public int kykc { get; set; }

        /// <summary>
        /// 最小单位进价
        /// </summary>
        public decimal zxdwjj { get; set; }

        /// <summary>
        /// 最小单位批发价
        /// </summary>
        public decimal zxdwpfj { get; set; }

        /// <summary>
        /// 最小单位零售价
        /// </summary>
        public decimal zxdwlsj { get; set; }

        /// <summary>
        /// 冻结药品批次信息
        /// </summary>
        public List<FrozenBatchesDetailVEntity> frozenedMedicineBatchs { get; set; }
    }

}
