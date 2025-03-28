namespace NewtouchHIS.WebAPI.Manage.Models.OutPatient
{
    public class OutpPresInfoRequest: OutpRegistQueryRequest
    {
        /// <summary>
        /// 处方Id
        /// </summary>
        public string? PresId { get; set; }
        /// <summary>
        /// 处方Id 集合
        /// </summary>
        public string[] PresIds { get; set; }
    }

    public class OutpPresInfoHisApiRequest
    {
        /// <summary>
        /// 处方Id
        /// </summary>
        public string PresId { get; set; }
        /// <summary>
        /// 门诊号
        /// </summary>
        public string mzh { get; set; }
    }

    public class OutpBillRequest
    {
        /// <summary>
        /// 处方Id
        /// </summary>
        public string kh { get; set; }
        /// <summary>
        /// 门诊号
        /// </summary>
        public string? mzh { get; set; }
        public string? OrderNo { get; set; }
        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal? OrderAmt { get; set; }
    }
     

}
