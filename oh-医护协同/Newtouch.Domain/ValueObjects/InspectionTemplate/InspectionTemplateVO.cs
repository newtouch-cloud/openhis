namespace Newtouch.Domain.ValueObjects
{
    public class InspectionTemplateVO
    {
        /// <summary>
        /// 
        /// </summary>
        public string mbId { get; set; }
        /// <summary>
        /// 1 检验 2 检查
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string mbmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dlCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zxks { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zxksmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }
    }
}
