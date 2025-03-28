namespace Newtouch.HIS.Domain.ValueObjects.SystemManage
{
    public class jgszchargemoneyVO
    {
        //public string Id { get; set; }
        //public DateTime sfrq { get; set; }
        //public string dlmc { get; set; }

        /// <summary>
        /// 收费项目代码
        /// </summary>
        public string sfxmCode { get; set; }

        /// <summary>
        /// 收费项目名称
        /// </summary>
        public string sfxmmc { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal dj { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int? sl { get; set; }

        /// <summary>
        /// 结算总额
        /// </summary>
        public decimal zje { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// 记账计划总额
        /// </summary>
        public decimal? jhzje { get; set; }

        /// <summary>
        /// 退费总额  jhzje-zje=tzje
        /// </summary>
        public decimal? tzje { get; set; }
    }
}
