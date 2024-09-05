using System.Collections.Generic;

namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
    public class Input_3505 : InputBase
    {
        public Selinfo3505 selinfo { get; set; }
    }
    public class Selinfo3505
    {
        /// <summary>
        /// 医疗目录编码 字符型 50 Y
        /// </summary>
        public string med_list_codg { get; set; }

        /// <summary>
        /// 定点医药机构目录编号 字符型 30 Y
        /// </summary>
        public string fixmedins_hilist_id { get; set; }

        /// <summary>
        /// 定点医药机构目录名称 字符型 200 Y
        /// </summary>
        public string fixmedins_hilist_name { get; set; }

        /// <summary>
        /// 定点医药机构批次流水号 字符型 30 Y
        /// </summary>
        public string fixmedins_bchno { get; set; }

        /// <summary>
        /// 开方医师证件类型 字符型 6
        /// </summary>
        public string prsc_dr_cert_type { get; set; }

        /// <summary>
        /// 开方医师证件号码 字符型 50
        /// </summary>
        public string prsc_dr_certno { get; set; }

        /// <summary>
        /// 开方医师姓名 字符型 50 Y
        /// </summary>
        public string prsc_dr_name { get; set; }

        /// <summary>
        /// 药师证件类型 字符型 6
        /// </summary>
        public string phar_cert_type { get; set; }

        /// <summary>
        /// 药师证件号码 字符型 50
        /// </summary>
        public string phar_certno { get; set; }

        /// <summary>
        /// 药师姓名 字符型 50 Y
        /// </summary>
        public string phar_name { get; set; }

        /// <summary>
        /// 药师执业资格证号 字符型 50 Y
        /// </summary>
        public string phar_prac_cert_no { get; set; }

        /// <summary>
        /// 医保费用结算类型 字符型 6 Y
        /// </summary>
        public string hi_feesetl_type { get; set; }

        /// <summary>
        /// 结算 ID 字符型 30 医保病人必填
        /// </summary>
        public string setl_id { get; set; }

        /// <summary>
        /// 就医流水号 字符型 30 Y
        /// </summary>
        public string mdtrt_sn { get; set; }

        /// <summary>
        /// 人员编号 字符型 30
        /// </summary>
        public string psn_no { get; set; }

        /// <summary>
        /// 人员证件类型 字符型 6 Y
        /// </summary>
        public string psn_cert_type { get; set; }

        /// <summary>
        /// 证件号码 字符型 50
        /// </summary>
        public string certno { get; set; }

        /// <summary>
        /// 人员姓名 字符型 50
        /// </summary>
        public string psn_name { get; set; }

        /// <summary>
        /// 生产批号 字符型 30 Y
        /// </summary>
        public string manu_lotnum { get; set; }

        /// <summary>
        /// 生产日期 日期型 Y yyyy-MM-dd
        /// </summary>
        public string manu_date { get; set; }

        /// <summary>
        /// 有效期止 日期型 yyyy-MM-dd
        /// </summary>
        public string expy_end { get; set; }

        /// <summary>
        /// 处方药标志 字符型 3 Y Y
        /// </summary>
        public string rx_flag { get; set; }

        /// <summary>
        /// 拆零标志 字符型 3 Y Y 0-否；1-是
        /// </summary>
        public string trdn_flag { get; set; }

        /// <summary>
        /// 最终成交单价 数值型 16,6
        /// </summary>
        public decimal finl_trns_pric { get; set; }

        /// <summary>
        /// 处方号 字符型 40
        /// </summary>
        public string rxno { get; set; }

        /// <summary>
        /// 外购处方标志 字符型 3 Y
        /// </summary>
        public string rx_circ_flag { get; set; }

        /// <summary>
        /// 零售单据号 字符型 40 Y
        /// </summary>
        public string rtal_docno { get; set; }

        /// <summary>
        /// 销售出库单据号 字符型 40
        /// </summary>
        public string stoout_no { get; set; }

        /// <summary>
        /// 批次号 字符型 30
        /// </summary>
        public string bchno { get; set; }

        /// <summary>
        /// 药品条形码 字符型 30
        /// </summary>
        public string drug_prod_barc { get; set; }

        /// <summary>
        /// 货架位 字符型 20
        /// </summary>
        public string shelf_posi { get; set; }

        /// <summary>
        /// 销售/退货数量 数值型 16,4 Y
        /// </summary>
        public decimal sel_retn_cnt { get; set; }

        /// <summary>
        /// 销售/退货时间 日期时间型 Y yyyy-MM-dd HH:mm:ss
        /// </summary>
        public string sel_retn_time { get; set; }

        /// <summary>
        /// 销售/退货经办人姓名 字符型 50 Y
        /// </summary>
        public string sel_retn_opter_name { get; set; }

        /// <summary>
        /// 备注 字符型 500
        /// </summary>
        public string memo { get; set; }

        /// <summary>
        /// 就诊结算类型 VARCHAR 6 否
        /// </summary>
        public string MDTRT_SETL_TYPE { get; set; }

        /// <summary>
        /// 溯源码节点信息
        /// </summary>
        public List<Drugtracinfo> drugtracinfo { get; set; }
    }

}
