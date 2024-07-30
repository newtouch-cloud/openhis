namespace Newtouch.HIS.Domain.Entity.V
{
    /// <summary>
    /// 医嘱信息
    /// </summary>
    public class YzxxVEntity
    {
        /// <summary>
        /// 发药标志
        /// </summary>
        public string fybz { get; set; }

        /// <summary>
        /// 申请退药标志 0：否  1：是
        /// </summary>
        public int? sqtybz { get; set; }

        /// <summary>
        /// 药品名称
        /// </summary>
        public string ypmc { get; set; }
    }
}