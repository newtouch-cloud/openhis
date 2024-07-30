using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 单据查询明细
    /// </summary>
    public class DrupreparationMXVO
    {
        /// <summary>
        /// 药品大类代码
        /// </summary>
        public string ypdlCode { get; set; }

        /// <summary>
        /// 药品类别名称
        /// </summary>
        public string yplbmc { get; set; }

        /// <summary>
        /// 拼音
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// 药品名称
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        ///生产厂家
        /// </summary>
        public string sccj { get; set; }
        

        /// <summary>
        /// 数量 申请数量
        /// </summary>
        public string sl { get; set; }

        /// <summary>
        /// 数量 带单位
        /// </summary>
        public string slanddw { get; set; }

        /// <summary>
        /// 批发总额
        /// </summary>
        public decimal? pjze { get; set; }

        /// <summary>
        ///零售总额
        /// </summary>
        public decimal? ljze { get; set; }
    }
}
