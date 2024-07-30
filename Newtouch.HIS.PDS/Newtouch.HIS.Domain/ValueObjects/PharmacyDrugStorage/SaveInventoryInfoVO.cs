namespace Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage
{
    /// <summary>
    /// 保存盘点
    /// </summary>
    public class SaveInventoryInfoVO
    {
        /// <summary>
        /// 盘点明细ID
        /// </summary>
        public string pdmxId { get; set; }

        /// <summary>
        /// 实际数量
        /// </summary>
        public int sjsl { get; set; }
    }
}
