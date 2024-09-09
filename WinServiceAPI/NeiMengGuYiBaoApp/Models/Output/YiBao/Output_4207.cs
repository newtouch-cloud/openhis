using System;
using System.Collections.Generic;

namespace NeiMengGuYiBaoApp.Models.Output.YiBao
{
    public class Output_4207 : OutputBase
    {
        public List<Output4207> data { get; set; }
    }

    /// <summary>
    /// 自费病人就医费用明细信息（节点标识：data）
    /// </summary>
    public class Output4207
    {
        /// <summary>
        /// 医药机构就诊 ID (长度: 30)
        /// </summary>
        public string fixmedinsMdtrtId { get; set; }

        /// <summary>
        /// 记账流水号 (长度: 30, 单次就诊内唯一)
        /// </summary>
        public string bkkpSn { get; set; }

        /// <summary>
        /// 费用发生时间 (格式: yyyy-MM-dd HH:mm:ss)
        /// </summary>
        public DateTime feeOcurTime { get; set; }

        /// <summary>
        /// 定点医药机构编号 (长度: 30)
        /// </summary>
        public string fixmedinsCode { get; set; }

        /// <summary>
        /// 定点医药机构名称 (长度: 200)
        /// </summary>
        public string fixmedinsName { get; set; }

        /// <summary>
        /// 数量 (数值型: 16,4)
        /// </summary>
        public decimal cnt { get; set; }

        /// <summary>
        /// 单价 (数值型: 16,6)
        /// </summary>
        public decimal pric { get; set; }

        /// <summary>
        /// 明细项目费用总额 (数值型: 16,2)
        /// </summary>
        public decimal detItemFeeSumamt { get; set; }

        /// <summary>
        /// 医疗目录编码 (长度: 50)
        /// </summary>
        public string medListCodg { get; set; }

        /// <summary>
        /// 医药机构目录编码 (长度: 150)
        /// </summary>
        public string medinsListCodg { get; set; }

        /// <summary>
        /// 医药机构目录名称 (长度: 100)
        /// </summary>
        public string medinsListName { get; set; }

        /// <summary>
        /// 医疗收费项目类别 (长度: 6)
        /// </summary>
        public string medChrgitmType { get; set; }

        /// <summary>
        /// 商品名 (长度: 200)
        /// </summary>
        public string prodname { get; set; }

        /// <summary>
        /// 开单科室编码 (长度: 30)
        /// </summary>
        public string bilgDeptCodg { get; set; }

        /// <summary>
        /// 开单科室名称 (长度: 100)
        /// </summary>
        public string bilgDeptName { get; set; }

        /// <summary>
        /// 开单医生编码 (长度: 30)
        /// </summary>
        public string bilgDrCode { get; set; }

        /// <summary>
        /// 开单医生姓名 (长度: 50)
        /// </summary>
        public string bilgDrName { get; set; }

        /// <summary>
        /// 受单科室编码 (长度: 30)
        /// </summary>
        public string acordDeptCodg { get; set; }

        /// <summary>
        /// 受单科室名称 (长度: 100)
        /// </summary>
        public string acordDeptName { get; set; }

        /// <summary>
        /// 受单医生编码 (长度: 30)
        /// </summary>
        public string acordDrCode { get; set; }

        /// <summary>
        /// 受单医生姓名 (长度: 50)
        /// </summary>
        public string acordDrName { get; set; }

        /// <summary>
        /// 中药使用方式 (长度: 6)
        /// </summary>
        public string tcmdrugUsedWay { get; set; }

        /// <summary>
        /// 外检标志 (长度: 3)
        /// </summary>
        public string etipFlag { get; set; }

        /// <summary>
        /// 外检医院编码 (长度: 30)
        /// </summary>
        public string etipHospCode { get; set; }

        /// <summary>
        /// 出院带药标志 (长度: 3)
        /// </summary>
        public string dscgTkdrugFlag { get; set; }

        /// <summary>
        /// 单次剂量描述 (长度: 200)
        /// </summary>
        public string sinDosDscr { get; set; }

        /// <summary>
        /// 使用频次描述 (长度: 200)
        /// </summary>
        public string usedFrquDscr { get; set; }

        /// <summary>
        /// 周期天数 (数值型: 4,2)
        /// </summary>
        public decimal prdDays { get; set; }

        /// <summary>
        /// 用药途径描述 (长度: 200)
        /// </summary>
        public string medcWayDscr { get; set; }

        /// <summary>
        /// 备注 (长度: 500)
        /// </summary>
        public string memo { get; set; }

        /// <summary>
        /// 全自费金额 (数值型: 16,2)
        /// </summary>
        public decimal fulamtOwnpayAmt { get; set; }

        /// <summary>
        /// 超限价自费金额 (数值型: 16,2)
        /// </summary>
        public decimal overlmtSelfpay { get; set; }

        /// <summary>
        /// 先行自付金额 (数值型: 16,2)
        /// </summary>
        public decimal preselfpayAmt { get; set; }

        /// <summary>
        /// 符合政策范围金额 (数值型: 16,2)
        /// </summary>
        public decimal inscpAmt { get; set; }

        /// <summary>
        /// 有效标志 (长度: 3)
        /// </summary>
        public string valiFlag { get; set; }

        /// <summary>
        /// 唯一记录号 (长度: 30)
        /// </summary>
        public string rid { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime updtTime { get; set; }

        /// <summary>
        /// 创建人 ID (长度: 20)
        /// </summary>
        public string crterId { get; set; }

        /// <summary>
        /// 创建人姓名 (长度: 50)
        /// </summary>
        public string crterName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime crteTime { get; set; }

        /// <summary>
        /// 创建机构 (长度: 20)
        /// </summary>
        public string crteOptinsNo { get; set; }

        /// <summary>
        /// 经办人 ID (长度: 20)
        /// </summary>
        public string opterId { get; set; }

        /// <summary>
        /// 经办人姓名 (长度: 50)
        /// </summary>
        public string opterName { get; set; }

        /// <summary>
        /// 经办时间
        /// </summary>
        public DateTime optTime { get; set; }

        /// <summary>
        /// 经办机构 (长度: 20)
        /// </summary>
        public string optinsNo { get; set; }

        /// <summary>
        /// 统筹区编码 (长度: 10)
        /// </summary>
        public string poolareaNo { get; set; }

        /// <summary>
        /// 审核通过标识 (长度: 3)
        /// </summary>
        public string chkPassFlag { get; set; }

        /// <summary>
        /// 处方号 (长度: 30)
        /// </summary>
        public string rxno { get; set; }
    }

}
