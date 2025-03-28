using NewtouchHIS.Domain.Entity.PatientCenter;

namespace NewtouchHIS.WebAPI.Manage.Models.OrderCenter
{
    /// <summary>
    /// 患者账单
    /// </summary>
    public class PatBillRequest
    {
        /// <summary>
        /// 住院号
        /// </summary>
        public string? zyh { get; set; }
        /// <summary>
        /// 门诊号
        /// </summary>
        public string? mzh { get; set; }
        /// <summary>
        /// 就诊卡号
        /// </summary>
        public string? kh { get; set; }
        /// <summary>
        /// 证件号
        /// </summary>
        public string? zjh { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string? OrderNo { get; set; }
        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal? OrderAmt { get; set; }
        /// <summary>
        /// 订单类型 EnumOrderType 1 门诊，2 住院
        /// </summary>
        public int? OrderType { get; set; }
        //public SysPatientAddressEntity AddressInfo { get; set; }
        public AddressInfo? addressInfo { get; set; }

        public class AddressInfo
        {
            public int patid { get; set; }
            public string? OrganizeId { get; set; }
            public string xm { get; set; }
            public string dh { get; set; }
            public string xian_sheng { get; set; }
            public string xian_shi { get; set; }
            public string xian_xian { get; set; }
            public string xian_dz { get; set; }
        }
    }
}
