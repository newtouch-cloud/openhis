using Newtouch.Domain.DTO.InputDto.Inpatient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Inpatient
{
    public class InPatientInfoVO
    {
        /// <summary>
        /// 住院号
        /// </summary>
        public string zyh { get; set; }
        /// <summary>
        /// 病人性质
        /// </summary>
        public string brxz { get; set; }
        /// <summary>
        /// 科室名称
        /// </summary>
        public string ksmc { get; set; }
        /// <summary>
        /// 住院医师
        /// </summary>
        public string zyys { get; set; }
        /// <summary>
        /// 主治医生
        /// </summary>
        public string zzys { get; set; }
        /// <summary>
        /// 主任医生
        /// </summary>
        public string zrys { get; set; }
        /// <summary>
        /// 入院诊断
        /// </summary>
        public string ryzd { get; set; }
        /// <summary>
        /// 出院诊断
        /// </summary>
        public string cyzd { get; set; }
        /// <summary>
        /// 入院日期
        /// </summary>
        public DateTime? ryrq { get; set; }
        /// <summary>
        /// 出院日期
        /// </summary>
        public DateTime? cyrq { get; set; }
        /// <summary>
        /// 在院标志
        /// </summary>
        public string zybz { get; set; }
        public List<WMDiagnosisHtmlVO> ryzdList { get; set; }
        public List<WMDiagnosisHtmlVO> cyzdList { get; set; }
    }
}
