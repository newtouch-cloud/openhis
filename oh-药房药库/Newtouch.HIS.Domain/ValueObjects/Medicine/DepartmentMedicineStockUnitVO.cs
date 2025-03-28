namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 药房部门 药品 库存数量、单位
    /// </summary>
    public class DepartmentMedicineStockUnitVO
    {
        /// <summary>
        /// 发药部门单位
        /// </summary>
        public string fybmdw { get; set; }

        /// <summary>
        /// 发药部门转化因子
        /// </summary>
        public decimal fybmzhyz { get; set; }

        /// <summary>
        /// 库存数量（对应最小单位）
        /// </summary>
        public int kcsl { get; set; }

        /// <summary>
        /// 现有库存数量+单位（以药库的方式来显示）
        /// </summary>
        public string kcslandDw { get; set; }

        /// <summary>
        /// 单位（当前药房部门 药品 单位）
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// 本部该改药品 转换因子
        /// </summary>
        public decimal bbmzhyz { get; set; }
    }
}
