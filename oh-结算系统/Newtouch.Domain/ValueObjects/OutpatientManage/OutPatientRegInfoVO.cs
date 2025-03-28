using System.ComponentModel.DataAnnotations.Schema;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 门诊挂号信息
    /// </summary>
    [NotMapped]
    public class OutPatientRegInfoVO : OutpatientRegistEntity
    {
        public string ksmc { get; set; }
        public string ysmc { get; set; }
        public string brxzmc { get; set; }
        public string py { get; set; }

    }

    /// <summary>
    /// 门诊挂号信息
    /// </summary>
    public class OutPatientRegVO
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// 病历号
        /// </summary>
        public string blh { get; set; }

        /// <summary>
        /// 门诊号
        /// </summary>
        public string mzh { get; set; }

        /// <summary>
        /// 是否欠费预结
        /// </summary>
        public bool iqQfyj { get; set; }

        /// <summary>
        /// 患者主索引
        /// </summary>
        public int? patid { get; set; }

        /// <summary>
        /// 拼音
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// 新农合个人编码
        /// </summary>
        public string xnhgrbm { get; set; }
    }
}
