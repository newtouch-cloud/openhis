namespace Newtouch.HIS.Domain.ValueObjects.OutPatientPharmacy
{
    public class pyConfig
    {
        /// <summary>
        /// 排药查询间隔时间
        /// </summary>
        public int pycxjgsj { get; set; }
        /// <summary>
        /// 自动排药
        /// </summary>
        public string IsAutoPy { get; set; }
        /// <summary>
        /// 自动发药
        /// </summary>
        public string IsAutoFy { get; set; }
    }
}
