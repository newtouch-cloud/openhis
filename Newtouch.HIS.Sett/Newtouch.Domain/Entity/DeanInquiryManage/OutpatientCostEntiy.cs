using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.Entity.DeanInquiryManage
{
    public class OutpatientCostEntiy
    {
        /// <summary>
        /// 收入分类
        /// </summary>
        public string srfl { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal? je { get; set; }
    }

    /// <summary>
    /// 门诊费用分类分析_图表（患者人均费用分析图表也用这个字段都是一样的）
    /// </summary>
    public class OutpatientCostEntiy_Mzfyflfx_tb
    {
        /// <summary>
        /// 年份
        /// </summary>
        public string 年份 { get; set; }
        /// <summary>
        /// 总费用
        /// </summary>
        public decimal? 总费用 { get; set; }
        /// <summary>
        /// 药品费
        /// </summary>
        public decimal? 药品费 { get; set; }
        /// <summary>
        /// 耗材费
        /// </summary>
        public decimal? 耗材费 { get; set; }
        /// <summary>
        /// 检验费
        /// </summary>
        public decimal? 检验费 { get; set; }
        /// <summary>
        /// 检查费
        /// </summary>
        public decimal? 检查费 { get; set; }
        /// <summary>
        /// 资料费
        /// </summary>
        public decimal? 资料费 { get; set; }
        /// <summary>
        /// 其他
        /// </summary>
        public decimal? 其他 { get; set; }
    }

    public class OutpatientCostEntiy_Hzrjfyfx
    {
        /// <summary>
        /// 人均费用分类
        /// </summary>
        public string srfl { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal? je { get; set; }
    }
}
