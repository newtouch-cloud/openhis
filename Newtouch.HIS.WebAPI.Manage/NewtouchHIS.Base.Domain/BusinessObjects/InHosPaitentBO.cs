using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Base.Domain.BusinessObjects
{
    public class InHosPaitentBO
    {
        public string? zyh { get; set; }
        public string? xm { get; set; }
        public string? OrganizeId { get; set; }
        public string? jsonpCallback { get; set; }
        public string? ResponseColumns { get; set; }
        public string AppId { get; set; }
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// 令牌类型
        /// </summary>
        public string TokenType { get; set; }

        /// <summary>
        /// 访问令牌
        /// </summary>
        public string Token { get; set; }
    }
}
