using Newtouch.HIS.API.Common;

namespace Newtouch.HIS.Sett.Request.OutPatientPharmacy
{
    public class OutpatientDrugWithdrawalNotifyRequest : RequestBase
    {
        /// <summary>
        /// 处方内码
        /// </summary>
        public int cfnm { get; set; }

        /// <summary>
        /// 药品编码
        /// </summary>
        public string yp { get; set; }

        /// <summary>
        /// 组织机构ID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 退药数量
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// 成组号
        /// </summary>
        public string czh { get; set; }

    }
}
