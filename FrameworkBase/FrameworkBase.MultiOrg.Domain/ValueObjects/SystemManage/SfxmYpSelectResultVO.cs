namespace FrameworkBase.MultiOrg.Domain.ValueObjects
{
    /// <summary>
    /// 药品项目检索VO
    /// </summary>
    public class SfxmYpSelectResultVO
    {
        /// <summary>
        /// 收费项目Code/药品Code
        /// </summary>
        public string sfxmCode { get; set; }

        /// <summary>
        /// 收费项目名称/药品名称
        /// </summary>
        public string sfxmmc { get; set; }

        /// <summary>
        /// 拼音
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// 收费大类Code
        /// </summary>
        public string sfdlCode { get; set; }

        /// <summary>
        /// 收费大类名称
        /// </summary>
        public string sfdlmc { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal dj { get; set; }

        /// <summary>
        /// 类型 1药品 2项目
        /// </summary>
        public string yzlx { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string bz { get; set; }

        /// <summary>
        /// （仅收费项目）默认耗时（分）
        /// </summary>
        public int? duration { get; set; }

        /// <summary>
        /// （仅收费项目）单位计量数
        /// </summary>
        public int? dwjls { get; set; }

        /// <summary>
        /// （仅收费项目）计价策略
        /// </summary>
        public int? jjcl { get; set; }

        /// <summary>
        /// （仅药品）剂量单位
        /// </summary>
        public string jldw { get; set; }

        /// <summary>
        /// （仅药品）剂量单位
        /// </summary>
        public decimal? jldwzhxs { get; set; }

        /// <summary>
        /// 自负性质
        /// </summary>
        public string zfxz { get; set; }

        /// <summary>
        /// 自负比例
        /// </summary>
        public decimal zfbl { get; set; }

        /// <summary>
        /// （仅药品）药品拆零系数
        /// </summary>
        public decimal? cls { get; set; }

        /// <summary>
        /// （仅药品）药房部门
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// （仅药品）药房部门名称
        /// </summary>
        public string yfbmmc { get; set; }

        /// <summary>
        /// （仅药品）药品库存数量
        /// </summary>
        public decimal? kcsl { get; set; }

        /// <summary>
        /// 规格（目前仅药品）
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// 药品剂型Code
        /// </summary>
        public string ypjxCode { get; set; }

        /// <summary>
        /// 控制标志（药品被控制 不能开立）
        /// </summary>
        public string kzbz { get; set; }

        /// <summary>
        /// 住院医嘱取整类型
        /// </summary>
        public string zyqzlx { get; set; }

        /// <summary>
        /// 执行科室（默认）
        /// </summary>
        public string zxks { get; set; }

        /// <summary>
        /// 执行科室名称（默认）
        /// </summary>
        public string zxksmc { get; set; }

        /// <summary>
        /// 医保代码
        /// </summary>
        public string ybdm { get; set; }

        /// <summary>
        /// 限制用药
        /// </summary>
        public bool? xzyy { get; set; }

        /// <summary>
        /// 限制用药说明
        /// </summary>
        public string xzyysm { get; set; }

        /// <summary>
        /// 默认剂量
        /// </summary>
        public decimal? mrjl { get; set; }

        /// <summary>
        /// 默认频次
        /// </summary>
        public string mrpc { get; set; }

        /// <summary>
        /// 默认频次名称
        /// </summary>
        public string mrpcmc { get; set; }

        /// <summary>
        /// 是否抗生素
        /// </summary>
        public string isKss { get; set; }

        /// <summary>
        /// 单次剂量(起始)
        /// </summary>
        public decimal? jlfwBegin { get; set; }

        /// <summary>
        /// 单次剂量(结束)
        /// </summary>
        public decimal? jlfwEnd { get; set; }

        /// <summary>
        /// 频次范围(起始)
        /// </summary>
        public decimal? pcfwBegin { get; set; }

        /// <summary>
        /// 频次范围(结束)
        /// </summary>
        public decimal? pcfwEnd { get; set; }

        /// <summary>
        /// 抗生素可用(1 可用,0 不可用)
        /// </summary>
        public string kssKy { get; set; }

        public string jybz { get; set; }
        /// <summary>
        /// 超限价金额
        /// </summary>
        public decimal? cxjje { get; set; }
        /// <summary>
        /// 特殊药品标志
        /// </summary>
        public string tsypbz { get; set; }
        /// <summary>
        /// 抗生素权限级别
        /// </summary>
        public string kssqxjb { get; set; }
    }
}
