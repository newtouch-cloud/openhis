
namespace Newtouch.Domain.ValueObjects
{
    public class OutpatientNurseTreeVO
    {
        /// <summary>
        /// 就诊ID
        /// </summary>
        public string jzId { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string xm { get; set; }
        /// <summary>
        /// 患者性别
        /// </summary>
        public string sex { get; set; }

        /// <summary>
        /// 患者年龄
        /// </summary>
        public string nlshow { get; set; }
        /// <summary>
        /// 门诊号
        /// </summary>
        public string mzh { get; set; }

        /// <summary>
        /// 病历号
        /// </summary>
        public string blh { get; set; }

        /// <summary>
        /// 挂号科室名称
        /// </summary>
        public string ghksmc { get; set; }
    }
}
