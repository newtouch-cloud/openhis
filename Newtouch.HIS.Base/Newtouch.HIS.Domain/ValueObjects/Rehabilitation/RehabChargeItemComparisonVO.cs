namespace Newtouch.HIS.Domain.ValueObjects.Rehabilitation
{
    /// <summary>
    /// 
    /// </summary>
    public class RehabChargeItemComparisonVO
    {         
        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sfxmdzId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string kfsfxmName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string kfsfxmCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string xtsfxmName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string xtsfxmCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? xtsfxm_duration { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal? dj { get; set; }
    }
}
