using Newtouch.Core.Common;
using Newtouch.HIS.API.Common;
using System;

namespace Newtouch.HIS.Sett.Request
{
    /// <summary>
    /// 住院患者查询
    /// </summary>
    public class InPatientQueryRequest : RequestBase
    {
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime? lastUpdateTime { get; set; }

        /// <summary>
        /// 住院号
        /// </summary>
        public string zyh { get; set; }

        /// <summary>
        /// 病区编码
        /// </summary>
        public string bqCode { get; set; }

        /// <summary>
        /// 在院标志（多个用英文逗号分割）
        /// </summary>
        public string zybz { get; set; }

        /// <summary>
        /// 关键字 可匹配 zyh blh xm
        /// </summary>
        public string keyword { get; set; }

        /// <summary>
        /// 分页
        /// </summary>
        public Pagination pagination { get; set; }

    }
}