using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.Entity.DeanInquiryManage
{
    public class OutpatientBenefitsEntiy
    {
        /// <summary>
        /// 门诊科室
        /// </summary>
        public string mzks { get; set; }
        /// <summary>
        /// 门诊人次
        /// </summary>
        public int? mzzlrc { get; set; }
        /// <summary>
        /// 门诊收入
        /// </summary>
        public decimal? mzsr { get; set; }
        /// <summary>
        /// 人均诊疗费用
        /// </summary>
        public decimal? rjfy { get; set; }
        /// <summary>
        /// 自费费用占比
        /// </summary>
        public decimal? zffyzb { get; set; }
        /// <summary>
        /// 医保费用占比
        /// </summary>
        public decimal? ybfyzb { get; set; }
        /// <summary>
        /// 平均诊疗时长
        /// </summary>
        public int? pjzlsc { get; set; }
        /// <summary>
        /// 复诊率
        /// </summary>
        public decimal? fzl { get; set; }
        public string DeptmentCode { get; set; }
    }

    public class OutpatientBenefitsEntiy_Ysxy
    {
        /// <summary>
        /// 医生
        /// </summary>
        public string mzks { get; set; }
        /// <summary>
        /// 门诊人次
        /// </summary>
        public int? mzzlrc { get; set; }
        /// <summary>
        /// 门诊收入
        /// </summary>
        public decimal? mzsr { get; set; }
        /// <summary>
        /// 人均诊疗费用
        /// </summary>
        public decimal? rjzlfy { get; set; }
        /// <summary>
        /// 自费费用占比
        /// </summary>
        public decimal? zffyzb { get; set; }
        /// <summary>
        /// 医保费用占比
        /// </summary>
        public decimal? ybfyzb { get; set; }
        /// <summary>
        /// 平均诊疗时长
        /// </summary>
        public int? pjzlsc { get; set; }
        /// <summary>
        /// 复诊率
        /// </summary>
        public decimal? fzl { get; set; }
    }
    /// <summary>
    /// 科室效益排名
    /// </summary>
    public class KSXYPMDTO
    {
        public string Name { get; set; }
        public int? keyword { get; set; }
    }
    public class KSXYPMBySJSRDTO
    {
        public string Name { get; set; }
        public decimal? keyword { get; set; }
    }
    public class OutpatientBenefitsEntiy_Lsqs
    {
        /// <summary>
        /// 周一
        /// </summary>
        public int mon { get; set; }
        /// <summary>
        /// 周二
        /// </summary>
        public int tue { get; set; }
        /// <summary>
        /// 周三
        /// </summary>
        public int wed { get; set; }
        /// <summary>
        /// 周四
        /// </summary>
        public int thu { get; set; }
        /// <summary>
        /// 周五
        /// </summary>
        public int fri { get; set; }
        /// <summary>
        /// 周六
        /// </summary>
        public int sat { get; set; }
        /// <summary>
        /// 周日
        /// </summary>
        public int sun { get; set; }
    }

}
