using System;

namespace Newtouch.HIS.Domain.VO
{
    /// <summary>
    /// 台账查询返回结果
    /// </summary>
    public class StandingBookInventoryDetail
    {
        /// <summary>
        /// 出入库说明
        /// </summary>
        public string crksm { get;set; }

        /// <summary>
        /// 入库数量+单位
        /// </summary>
        public string rkslanddw { get; set; }

        /// <summary>
        /// 出库数量+单位
        /// </summary>
        public string ckslanddw { get; set; }

        /// <summary>
        /// 结转数量+单位
        /// </summary>
        public string jzslanddw { get; set; }

        /// <summary>
        /// 票单号
        /// </summary>
        public string pzh { get; set; }

        /// <summary>
        /// 科室
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// 发生日期
        /// </summary>
        public DateTime fsrq { get; set; }
    }
}
