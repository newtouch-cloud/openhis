using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.Entity.DeanInquiryManage
{
    public class OutpatientComprehensiveAnalysisEntiy
    {
        /// <summary>
        /// 门诊人次
        /// </summary>
        public int mzrc { get; set; }
        /// <summary>
        /// 门诊人次同比
        /// </summary>
        public decimal? mzrc_tb { get; set; }
        /// <summary>
        /// 门诊人次环比
        /// </summary>
        public decimal? mzrc_hb { get; set; }
        /// <summary>
        /// 门诊人数
        /// </summary>
        public int? mzrs { get; set; }
        /// <summary>
        /// 门诊人数同比
        /// </summary>
        public decimal? mzrs_tb { get; set; }
        /// <summary>
        /// 门诊人数环比
        /// </summary>
        public decimal? mzrs_hb { get; set; }
        /// <summary>
        /// 复诊率
        /// </summary>
        public decimal? fzl { get; set; }
        /// <summary>
        /// 复诊率同比
        /// </summary>
        public decimal? fzl_tb { get; set; }
        /// <summary>
        /// 复诊率环比
        /// </summary>
        public decimal? fzl_hb { get; set; }
        /// <summary>
        /// 诊次费用
        /// </summary>
        public decimal? zcfy { get; set; }
        /// <summary>
        /// 诊次费用同比
        /// </summary>
        public decimal? zcfy_tb { get; set; }
        /// <summary>
        /// 诊次费用环比
        /// </summary>
        public decimal? zcfy_hb { get; set; }
        /// <summary>
        /// 平均诊疗时长
        /// </summary>
        public decimal? pjzlsc { get; set; }
        /// <summary>
        /// 平均诊疗时长同比
        /// </summary>
        public decimal? pjzlsc_tb { get; set; }
        /// <summary>
        /// 平均诊疗时长环比
        /// </summary>
        public decimal? pjzlsc_hb { get; set; }
    }

    public class OutpatientComprehensiveAnalysisEntiy_Mzhzfx
    {
        /// <summary>
        /// 患者性质占比_医保
        /// </summary>
        public int hzxzb_yb { get; set; }
        /// <summary>
        /// 患者性质占比_自费
        /// </summary>
        public int hzxzb_zf { get; set; }
        /// <summary>
        /// 门诊患者性别占比_男性
        /// </summary>
        public int mzhzxbzb_m { get; set; }
        /// <summary>
        /// 门诊患者性别占比_女性
        /// </summary>
        public int mzhzxbzb_w { get; set; }
        /// <summary>
        ///  不同患者年龄段_3岁以下
        /// </summary>
        public int age_3 { get; set; }
        /// <summary>
        ///  不同患者年龄段_3-16岁
        /// </summary>
        public int age_3_16 { get; set; }
        /// <summary>
        ///  不同患者年龄段_17-65岁
        /// </summary>
        public int age_17_65 { get; set; }
        /// <summary>
        ///  不同患者年龄段_65-75岁
        /// </summary>
        public int age_65_75 { get; set; }
        /// <summary>
        ///  不同患者年龄段_75岁以上
        /// </summary>
        public int age_75 { get; set; }
    }
}
