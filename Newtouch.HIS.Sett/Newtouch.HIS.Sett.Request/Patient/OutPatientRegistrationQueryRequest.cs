using Newtouch.Core.Common;
using Newtouch.HIS.API.Common;
using System;

namespace Newtouch.HIS.Sett.Request.Patient
{
    /// <summary>
    /// 门诊挂号查询
    /// </summary>
    public class OutPatientRegistrationQueryRequest : RequestBase
    {
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime? lastUpdateTime { get; set; }

        /// <summary>
        /// 门诊号
        /// </summary>
        public string outpatientNo { get; set; }

        /// <summary>
        /// 科室编码
        /// </summary>
        public string ksCode { get; set; }

        /// <summary>
        /// 医生工号
        /// </summary>
        public string ysgh { get; set; }

        /// <summary>
        /// 门急诊标志
        /// </summary>
        public string mjzbz { get; set; }

        /// <summary>
        /// 就诊标志
        /// </summary>
        public string jiuzhenbz { get; set; }

        /// <summary>
        /// 关键字 可匹配 mzh blh xm
        /// </summary>
        public string keyword { get; set; }
        /// <summary>
        /// 转诊患者 1包含 0不包含
        /// </summary>
        public string zzhz { get; set; }

        /// <summary>
        /// 分页
        /// </summary>
        public Pagination pagination { get; set; }

    }
}
