namespace Newtouch.Herp.Domain.Entity.VEntity
{
    /// <summary>
    /// 发票和供应商信息
    /// </summary>
    public class VFphAndGysEntity
    {
        /// <summary>
        /// 发票号
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// 供应商ID
        /// </summary>
        public string gysId { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        public string gysmc { get; set; }
    }
}
