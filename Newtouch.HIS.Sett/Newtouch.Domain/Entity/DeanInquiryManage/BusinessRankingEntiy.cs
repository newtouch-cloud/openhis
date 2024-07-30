using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.Entity.DeanInquiryManage
{
    /// <summary>
    /// 门诊医生工作量排名
    /// </summary>
    public class BusinessRankingEntiy
    {
        public string ys { get; set; }
        public string mzks { get; set; }
        public int mzl { get; set; }
    }

    /// <summary>
    /// 门诊科室收入排名
    /// </summary>
    public class BusinessRankingEntiy_mzkssr
    {
        /// <summary>
        /// 门诊科室
        /// </summary>
        public string mzks { get; set; }
        /// <summary>
        /// 门诊收入
        /// </summary>
        public decimal mzsr { get; set; }
        /// <summary>
        /// 药品收入
        /// </summary>
        public decimal ypsr { get; set; }
        /// <summary>
        /// 药品占比
        /// </summary>
        public string ypzb { get; set; }
    }

    /// <summary>
    /// 住院业务科室排名
    /// </summary>
    public class BusinessRankingEntiy_zyywks
    {

        public string bqCode { get; set; }
        /// <summary>
        /// 住院科室
        /// </summary>
        public string zyks { get; set; }
        /// <summary>
        /// 出院人数
        /// </summary>
        public int cyrs { get; set; }
        /// <summary>
        /// 入院人数
        /// </summary>
        public int ryrs { get; set; }
        /// <summary>
        /// 手术人数
        /// </summary>
        public int ssrs { get; set; }
        /// <summary>
        /// 住院总费用
        /// </summary>
        public decimal? zyzfy { get; set; }
        /// <summary>
        /// 次均费用
        /// </summary>
        public decimal? cjfy { get; set; }
        /// <summary>
        /// 平均住院天数
        /// </summary>
        public decimal? pjzyts { get; set; }
    }

    /// <summary>
    /// 医生业务排名
    /// </summary>
    public class BusinessRankingEntiy_ysyw
    {
        /// <summary>
        /// 医生
        /// </summary>
        public string ys { get; set; }
        /// <summary>
        /// 出院人数
        /// </summary>
        public int cyrs { get; set; }
        /// <summary>
        /// 入院人数
        /// </summary>
        public int ryrs { get; set; }
        /// <summary>
        /// 手术人数
        /// </summary>
        public int ssrs { get; set; }
        /// <summary>
        /// 住院总费用
        /// </summary>
        public decimal? zyzfy { get; set; }
        /// <summary>
        /// 次均费用
        /// </summary>
        public decimal? cjfy { get; set; }
        /// <summary>
        /// 平均住院天数
        /// </summary>
        public decimal? pjzyts { get; set; }
    }
}
