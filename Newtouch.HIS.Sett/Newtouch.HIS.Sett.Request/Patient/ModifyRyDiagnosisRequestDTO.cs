using System.Collections.Generic;
using Newtouch.HIS.API.Common;

namespace Newtouch.HIS.Sett.Request
{
    /// <summary>
    /// 修改入区诊断请求报文
    /// </summary>
    public class ModifyRyDiagnosisRequestDTO : RequestBase
    {
        /// <summary>
        /// 住院号
        /// </summary>
        public string zyh { get; set; }

        /// <summary>
        /// 组织机构ID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 当前登录者
        /// </summary>
        public string userCode { get; set; }

        /// <summary>
        /// 入院诊断内容
        /// </summary>
        public List<RyDiagnosisDetail> RyDiagnosisDetails { get; set; }
    }

    /// <summary>
    /// 入院诊断内容
    /// </summary>
    public class RyDiagnosisDetail
    {

        /// <summary>
        /// 入区诊断1名称
        /// </summary>
        public string ryzdmc { get; set; }

        /// <summary>
        /// 入区诊断1代码
        /// </summary>
        public string ryzddm { get; set; }

        /// <summary>
        /// 入区诊断1ICD10
        /// </summary>
        public string ryzdICD10 { get; set; }

        /// <summary>
        /// 诊断排序  1：诊断1 2：诊断2  3：诊断3
        /// </summary>
        public string zdpx { get; set; }
    }
}