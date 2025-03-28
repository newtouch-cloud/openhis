using Newtouch.HIS.API.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.HIS.Sett.Request.Therapist
{
    /// <summary>
    /// 更新建议的转换状态
    /// </summary>
    public class UpdateSuggestionCvStatusRequest : OrgRequestBase
    {
        /// <summary>
        /// 列表
        /// </summary>
        [Required]
        public IList<SuggestionCvStatuDTO> CvList { get; set; }

    }
}
