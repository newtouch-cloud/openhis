using Newtouch.HIS.API.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.HIS.Sett.Request.Therapist
{
    /// <summary>
    /// 待转换治疗建议 查询
    /// </summary>
    public class TherapistSuggestionQueryRequest : OrgRequestBase
    {
        /// <summary>
        /// 病人类型 1门诊 2住院
        /// </summary>
        [Required]
        public string brlx { get; set; }

        /// <summary>
        /// 门诊住院号
        /// </summary>
        [Required]
        public string mzzyh { get; set; }

        /// <summary>
        /// 开立的起始时间筛选
        /// </summary>
        [Required]
        public DateTime? startTime { get; set; }
    }
}
