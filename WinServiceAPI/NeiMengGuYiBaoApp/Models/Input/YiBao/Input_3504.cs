namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
    public class Input_3504 : InputBase
    {
        public Purcinfo3504 purcinfo { get; set; }
    }

    public class Purcinfo3504
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
        /// 供应商名称 字符型 200 Y
        /// </summary>
        public string spler_name { get; set; }

        /// <summary>
        /// 供应商许可证号 字符型 50
        /// </summary>
        public string spler_pmtno { get; set; }

        /// <summary>
        /// 生产日期 日期型 Y yyyy-MM-dd
        /// </summary>
        public string manu_date { get; set; }

        /// <summary>
        /// 有效期止 日期型 Y yyyy-MM-dd
        /// </summary>
        public string expy_end { get; set; }

        /// <summary>
        /// 最终成交单价 数值型 16,6 采购/退货单价
        /// </summary>
        public decimal? finl_trns_pric { get; set; }

        /// <summary>
        /// 采购/退货数量 数值型 16,4 Y
        /// </summary>
        public decimal? purc_retn_cnt { get; set; }

        /// <summary>
        /// 采购发票编码 字符型 50
        /// </summary>
        public string purc_invo_codg { get; set; }

        /// <summary>
        /// 采购发票号 字符型 50 Y
        /// </summary>
        public string purc_invo_no { get; set; }

        /// <summary>
        /// 处方药标志 字符型 3 Y Y
        /// </summary>
        public string rx_flag { get; set; }

        /// <summary>
        /// 采购/退货入库时间 日期时间型 Y yyyy-MM-dd HH:mm:ss
        /// </summary>
        public string purc_retn_stoin_time { get; set; }

        /// <summary>
        /// 采购/退货经办人姓名 字符型 50 Y
        /// </summary>
        public string purc_retn_opter_name { get; set; }

        /// <summary>
        /// 备注 字符型 500
        /// </summary>
        public string memo { get; set; }

        /// <summary>
        /// 商品采购流水号 字符型 50
        /// </summary>
        public string medins_prod_purc_no { get; set; }
    }

}
