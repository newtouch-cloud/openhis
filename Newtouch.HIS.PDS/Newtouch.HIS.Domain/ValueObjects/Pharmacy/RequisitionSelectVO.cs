using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 申领单 查询
    /// </summary>
    public class RequisitionSelectVO
    {
        /// <summary>
        /// 申领单Id
        /// </summary>
        public string sldId { get; set; }

        /// <summary>
        /// 申领单号
        /// </summary>
        public string Sldh { get; set; }

        /// <summary>
        /// 申领部门名称
        /// </summary>
        public string slbmmc { get; set; }

        /// <summary>
        /// 出库部门名称
        /// </summary>
        public string ckbmmc { get; set; }

        /// <summary>
        /// 申领时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 发放状态
        /// </summary>
        public int ffzt { get; set; }

    }
}
