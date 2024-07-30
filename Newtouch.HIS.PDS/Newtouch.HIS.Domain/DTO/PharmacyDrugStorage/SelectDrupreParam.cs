using Newtouch.Core.Common;
using System;

namespace Newtouch.HIS.Domain.DTO
{
    /// <summary>
    /// 单据查询条件
    /// </summary>
    public class SelectDrupreParam
    {
        /// <summary>
        /// 翻页
        /// </summary>
        public Pagination pagination { get; set; }

        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime? qsrj { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime? jsrj { get; set; }

       
        /// <summary>
        /// 审核状态
        /// </summary>
        public string shzt { get; set; }

       
        /// <summary>
        /// 组织结构
        /// </summary>
        public string orgId { get; set; }

        /// <summary>
        /// 当前药房部门代码
        /// </summary>
        public string curYfbmCode { get; set; }

    }
}