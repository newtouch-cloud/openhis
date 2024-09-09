using System;
using System.Collections.Generic;

namespace NeiMengGuYiBaoApp.Models.Output.YiBao
{
    public class Output_5402 : OutputBase
    {
        /// <summary>
        /// 检查报告明细信息
        /// </summary>
        public List<CheckReportDetails> checkReportDetails { get; set; }
        /// <summary>
        /// -检验报告信息
        /// </summary>
        public List<InspectionReportInformation> inspectionReportInformation { get; set; }
        /// <summary>
        ///  检验明细信息
        /// </summary>
        public List<InspectionDetails> inspectiondetails { get; set; }
    }

    public class CheckReportDetails
    {
        /// <summary>
        /// 人员编号 (必填, 长度: 30)
        /// </summary>
        public string psn_no { get; set; }

        /// <summary>
        /// 报告单号 (必填, 长度: 30)
        /// </summary>
        public string rpotc_no { get; set; }

        /// <summary>
        /// 报告日期 (日期型)
        /// </summary>
        public DateTime? rpt_date { get; set; }

        /// <summary>
        /// 报告单类别代码 (长度: 30)
        /// </summary>
        public string rpotc_type_code { get; set; }

        /// <summary>
        /// 检查报告单名称 (长度: 50)
        /// </summary>
        public string exam_rpotc_name { get; set; }

        /// <summary>
        /// 检查结果阳性标志 (长度: 2)
        /// </summary>
        public string exam_rslt_poit_flag { get; set; }

        /// <summary>
        /// 检查异常标志 (长度: 10)
        /// </summary>
        public string exam_rslt_abn { get; set; }

        /// <summary>
        /// 检查结论 (长度: 1000)
        /// </summary>
        public string exam_ccls { get; set; }

        /// <summary>
        /// 报告医师 (长度: 50)
        /// </summary>
        public string bilgDrName { get; set; }
    }

    public class InspectionReportInformation
    {
        /// <summary>
        /// 人员编号 (必填, 长度: 30)
        /// </summary>
        public string psn_no { get; set; }

        /// <summary>
        /// 报告单号 (长度: 30)
        /// </summary>
        public string rpotc_no { get; set; }

        /// <summary>
        /// 检验-项目代码 (长度: 30)
        /// </summary>
        public string exam_item_code { get; set; }

        /// <summary>
        /// 检验-项目名称 (长度: 300)
        /// </summary>
        public string exam_item_name { get; set; }

        /// <summary>
        /// 报告日期 (日期型)
        /// </summary>
        public DateTime? rpt_date { get; set; }

        /// <summary>
        /// 报告医师 (长度: 50)
        /// </summary>
        public string rpot_doc { get; set; }
    }

    public class InspectionDetails
    {
        /// <summary>
        /// 报告单号 (长度: 30)
        /// </summary>
        public string rpotc_no { get; set; }

        /// <summary>
        /// 检验方法 (长度: 50)
        /// </summary>
        public string exam_mtd { get; set; }

        /// <summary>
        /// 参考值 (长度: 20)
        /// </summary>
        public string ref_val { get; set; }

        /// <summary>
        /// 检验-计量单位 (长度: 200)
        /// </summary>
        public string exam_unt { get; set; }

        /// <summary>
        /// 检验-结果(数值) (数值型, 长度: 11)
        /// </summary>
        public decimal? exam_rslt_val { get; set; }

        /// <summary>
        /// 检验 - 结果(定性) (长度: 2000)
        /// </summary>
        public string exam_rslt_qual { get; set; }

        /// <summary>
        /// 检验 - 项目明细代码 (长度: 30)
        /// </summary>
        public string exam_item_detl_code { get; set; }

        /// <summary>
        /// 检验 - 项目明细名称 (长度: 300)
        /// </summary>
        public string exam_item_detl_name { get; set; }

        /// <summary>
        /// 检查 / 检验结果异常标识 (长度: 10)
        /// </summary>
        public string exam_rslt_abn { get; set; }
    }

}
