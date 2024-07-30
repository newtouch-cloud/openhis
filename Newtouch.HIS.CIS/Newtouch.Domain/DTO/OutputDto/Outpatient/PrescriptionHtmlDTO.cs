using Newtouch.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.Domain.DTO.OutputDto
{
    /// <summary>
    /// 页面提交过来的
    /// </summary>
    public class PrescriptionHtmlDTO
    {
        /// <summary>
        /// 处方类型
        /// </summary>
        public string cflx { get; set; }

        /// <summary>
        /// 处方信息
        /// </summary>
        public List<PrescriptionHtmlVO> cfHtml;
    }
}
