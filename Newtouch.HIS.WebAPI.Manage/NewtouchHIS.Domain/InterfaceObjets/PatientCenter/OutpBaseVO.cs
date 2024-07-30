using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Domain.InterfaceObjets.PatientCenter
{
    /// <summary>
    /// 门诊患者基本信息
    /// </summary>
    public class OutpBaseVO
    {
        /// <summary>
        /// 机构编码
        /// </summary>
        public string? OrganizeId { get; set; }
        /// <summary>
        /// 患者Id
        /// </summary>
        public string? patid { get; set; }
        /// <summary>
        /// 门诊号
        /// </summary>
        public string? mzh { get; set; }
    }
}
