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

        /// <summary>
        /// 追溯码
        /// </summary>
        public string zsm { get; set; }

        /// <summary>
        /// 是否拆零
        /// 1： 是
        /// 2： 否
        /// </summary>
        public int sfcl { get; set; }
    }
}
