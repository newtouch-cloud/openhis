using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Outpatient
{
    /// <summary>
    /// 门诊患者信息
    /// </summary>
    public class OutHosPatientVO
    {
        /// <summary>
        /// 住院号
        /// </summary>
        public string jzId { get; set; }

        /// <summary>
        /// 病历号
        /// </summary>
        public string blh { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string xb { get; set; }

        /// <summary>
        /// 出生年月
        /// </summary>
        public DateTime csny { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        public string ghksmc { get; set; }

        /// <summary>
        /// 科室
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// 拼音
        /// </summary>
        public string py { get; set; }
    }
}
