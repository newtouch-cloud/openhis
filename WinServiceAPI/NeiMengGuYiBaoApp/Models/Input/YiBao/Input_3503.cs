namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
    public class Input_3503 : InputBase
    {
        public Purcinfo3503 purcinfo { get; set; }
    }

    public class Purcinfo3503
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
        /// 随货单号 字符型 50
        /// </summary>
        public string dynt_no { get; set; }

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
        /// 生产批号 字符型 30 Y
        /// </summary>
        public string manu_lotnum { get; set; }

        /// <summary>
        /// 生产厂家名称 字符型 200 Y
        /// </summary>
        public string prodentp_name { get; set; }

        /// <summary>
        /// 批准文号 字符型 100 Y
        /// </summary>
        public string aprvno { get; set; }

        /// <summary>
        /// 生产日期 日期型 Y yyyy-MM-dd
        /// </summary>
        public string manu_date { get; set; }

        /// <summary>
        /// 有效期止 日期型 Y yyyy-MM-dd
        /// </summary>
        public string expy_end { get; set; }

        /// <summary>
        /// 最终成交单价 数值型 16,6
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
        /// 采购发票号 字符型 50
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
        /// 商品赠送标志 字符型 3 0-否；1-是
        /// </summary>
        public string prod_geay_flag { get; set; }

        /// <summary>
        /// 备注 字符型 500
        /// </summary>
        public string memo { get; set; }
    }

}
