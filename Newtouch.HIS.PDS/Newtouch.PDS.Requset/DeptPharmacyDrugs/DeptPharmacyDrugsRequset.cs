using Newtouch.HIS.API.Common;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.PDS.Requset.DeptPharmacyDrugs
{
    /// <summary>
    /// 获取部门药房药品
    /// </summary>
    public class DeptPharmacyDrugsRequset : RequestBase
    {
        /// <summary>
        /// 药房部门 yfdm 如：mzyf、zyyf、yjk
        /// </summary>
        [Required]
        public string DeptCode { get; set; }
    }
}
