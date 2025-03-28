using Newtouch.HIS.API.Common;

namespace Newtouch.HIS.Sett.Request.OutPatientPharmacy
{
    /// <summary>
    /// 发药处方明细查询
    /// </summary>
    public class DeliveryRpDetailQueryRequestDto : RequestBase
    {

        /// <summary>
        /// 处方号
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// 领药药房
        /// </summary>
        public string lyyf { get; set; }

        /// <summary>
        /// 组织机构ID
        /// </summary>
        public string organizeId { get; set; }
    }
}