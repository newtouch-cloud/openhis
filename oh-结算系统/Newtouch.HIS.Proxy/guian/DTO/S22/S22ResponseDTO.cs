namespace Newtouch.HIS.Proxy.guian.DTO.S22
{
    /// <summary>
    /// 查询当此门诊已上传费用明细 返回报文
    /// </summary>
    public class item
    {
        /// <summary>
        ///  地区编码
        /// </summary>
        public string areaCode { get; set; }

        /// <summary>
        /// 参合年度
        /// </summary>
        public string year { get; set; }

        /// <summary>
        /// 病人所属县区编号
        /// </summary>
        public string countryCode { get; set; }

        /// <summary>
        ///  门诊处方流水号
        /// </summary>
        public string detailNo { get; set; }

        /// <summary>
        ///  补偿序号
        /// </summary>
        public string outpId { get; set; }

        /// <summary>
        ///  明细ID
        /// </summary>
        public string detailId { get; set; }

        /// <summary>
        ///  农合编码
        /// </summary>
        public string detailCode { get; set; }

        /// <summary>
        ///  类别代码(见字典目录类别)
        /// </summary>
        public string typeCode { get; set; }

        /// <summary>
        /// 项目类别名称
        /// </summary>
        public string typeName { get; set; }

        /// <summary>
        /// 明细中文名
        /// </summary>
        public string detailName { get; set; }

        /// <summary>
        /// 数量（四位小数精度）
        /// </summary>
        public decimal num { get; set; }

        /// <summary>
        /// 单价（四位小数精度）
        /// </summary>
        public decimal price { get; set; }

        /// <summary>
        /// 总价（四位小数精度）
        /// </summary>
        public decimal totalCost { get; set; }

        /// <summary>
        ///  用药日期(yyyy-mm-dd)
        /// </summary>
        public string date { get; set; }

        /// <summary>
        ///  规格
        /// </summary>
        public string standard { get; set; }

        /// <summary>
        ///  剂型
        /// </summary>
        public string formulations { get; set; }

        /// <summary>
        /// 保内或保内（1-保内，0 保外）
        /// </summary>
        public string flag { get; set; }

        /// <summary>
        /// 报销比例
        /// </summary>
        public decimal proportion { get; set; }

        /// <summary>
        /// 是否中药
        /// </summary>
        public string isCm { get; set; }

        /// <summary>
        /// 限制值
        /// </summary>
        public string limitValue { get; set; }

        /// <summary>
        /// 是否需要审批
        /// </summary>
        public string isAudit { get; set; }

        /// <summary>
        /// 入库日期
        /// </summary>
        public string storageTime { get; set; }

        /// <summary>
        /// 是否修改
        /// </summary>
        public string isModify { get; set; }

        /// <summary>
        /// 医院使用名称
        /// </summary>
        public string detailLocalName { get; set; }
    }
}