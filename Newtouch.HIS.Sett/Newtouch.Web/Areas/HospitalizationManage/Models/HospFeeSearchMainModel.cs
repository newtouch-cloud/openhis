using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Newtouch.HIS.Web.Areas.HospitalizationManage.Models
{
    /// <summary>
    /// 费用查询主信息
    /// </summary>
    public class HospFeeSearchMainModel
    {

        /// <summary>
        /// 收费日期
        /// </summary>
        public string Sfrq { get; set; }

        /// <summary>
        /// 床位费大类编码
        /// </summary>
        public string CWDlCode { get; set; }

        /// <summary>
        /// 床位费
        /// </summary>
        public decimal CWJe { get; set; }

        /// <summary>
        /// 护理费大类编码
        /// </summary>
        public string HLDlCode { get; set; }

        /// <summary>
        /// 护理费
        /// </summary>
        public decimal HLJe { get; set; }

        /// <summary>
        /// 治疗费大类编码
        /// </summary>
        public string ZLDlCode { get; set; }

        /// <summary>
        /// 治疗费
        /// </summary>
        public decimal ZLJe { get; set; }

        /// <summary>
        /// 其他费大类编码
        /// </summary>
        public string QTDlCode { get; set; }

        /// <summary>
        /// 其他费
        /// </summary>
        public decimal QTJe { get; set; }

        /// <summary>
        /// 西药费大类编码
        /// </summary>
        public string XYDlCode { get; set; }

        /// <summary>
        /// 西药费
        /// </summary>
        public decimal XYJe { get; set; }

        /// <summary>
        ///中成药大类编码
        /// </summary>
        public string ZCYDlCode { get; set; }

        /// <summary>
        /// 中成药
        /// </summary>
        public decimal ZCYJe { get; set; }

        /// <summary>
        /// 化验费大类编码
        /// </summary>
        public string HYDlCode { get; set; }

        /// <summary>
        /// 化验费
        /// </summary>
        public decimal HYJe { get; set; }

        /// <summary>
        /// 检查费大类编码
        /// </summary>
        public string JCDlCode { get; set; }

        /// <summary>
        /// 检查费
        /// </summary>
        public decimal JCJe { get; set; }

        /// <summary>
        /// 材料费大类编码
        /// </summary>
        public string CLDlCode { get; set; }

        /// <summary>
        /// 材料费
        /// </summary>
        public decimal CLJe { get; set; }

        /// <summary>
        /// 合计
        /// </summary>
        public decimal HJ { get; set; }

    }
}