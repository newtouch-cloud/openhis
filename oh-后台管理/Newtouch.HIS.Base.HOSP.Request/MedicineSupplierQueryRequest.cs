namespace Newtouch.HIS.Base.HOSP.Request
{
    /// <summary>
    /// 药品供应商 查询
    /// </summary>
    public class MedicineSupplierQueryRequest : OrgRequestBase
    {
        /// <summary>
        /// 关键字
        /// </summary>
        public string keyword { get; set; }

    }
}
