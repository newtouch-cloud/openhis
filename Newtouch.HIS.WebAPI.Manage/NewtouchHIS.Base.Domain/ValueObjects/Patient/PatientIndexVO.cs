using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Base.Domain.ValueObjects
{
    public class PatientIndexVO
    {
        /// <summary>
        /// 患者身份唯一标识
        /// </summary>
        public int? PatId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string? PatName { get; set; }
        /// <summary>
        /// 就诊卡号
        /// </summary>
        public string? Jzkh { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>
        public string? IdCard { get; set; }
        /// <summary>
        /// 病人性质 医保/自费
        /// </summary>
        public string? Brxz { get; set; }

    }
}
