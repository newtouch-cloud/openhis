namespace Newtouch.Domain.ValueObjects
{
    public class MedicalRecordTreeVO
    {
        /// <summary>
        /// 
        /// </summary>
        public string jzId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string blmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int jzzt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int lsblly { get; set; }

        /// <summary>
        /// 挂号科室名称
        /// </summary>
        public string ghksmc { get; set; }

        /// <summary>
        /// 最后看诊医生
        /// </summary>
        public string jzysmc { get; set; }
    }
}
