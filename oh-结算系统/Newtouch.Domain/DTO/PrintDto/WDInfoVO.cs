namespace Newtouch.HIS.Domain.PrintDto
{
    public class WDInfoVO
    {
        /// <summary>
        /// 病人内码
        /// </summary>
        public string patid { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public string csrq { get; set; }

        /// <summary>
        /// 科室病区
        /// </summary>
        public string ksmc { get; set; }

        /// <summary>
        /// 病区名称
        /// </summary>
        public string bqmc { get; set; }

        /// <summary>
        /// 住院号
        /// </summary>
        public string zyh { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string xb { get; set; }

        /// <summary>
        /// 打印类别
        /// </summary>
        public string Flag { get; set; }

    }
}
