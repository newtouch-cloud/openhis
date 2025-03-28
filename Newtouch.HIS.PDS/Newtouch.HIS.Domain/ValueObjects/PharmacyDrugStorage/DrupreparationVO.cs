using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 单据查询主记录
    /// </summary>
    public class DrupreparationVO
    {
        public string Id { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        public string shzt { get; set; }

        /// <summary>
        /// 申请病区
        /// </summary>
        public string bqmc { get; set; }

        /// <summary>
        /// 出库部门名称
        /// </summary>
        public string yfbmmc { get; set; }

        /// <summary>
        /// 入库部门名称
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        public decimal? pfj { get; set; }

        /// <summary>
        /// 出入库单据主表ID
        /// </summary>
        public decimal? lsj { get; set; }

        /// <summary>
        /// 单据号
        /// </summary>
        public string CreateTime { get; set; }
    }
}
