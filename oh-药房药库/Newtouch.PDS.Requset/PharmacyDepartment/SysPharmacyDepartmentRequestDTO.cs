using Newtouch.HIS.API.Common;

namespace Newtouch.PDS.Requset.PharmacyDepartment
{
    /// <summary>
    /// 系统药房部门
    /// </summary>
    public class SysPharmacyDepartmentRequestDto : RequestBase
    {
        /// <summary>
        /// 药品Id
        /// </summary>
        public string ypId { get; set; }

        /// <summary>
        /// 组织机构ID
        /// </summary>
        public string organizeId { get; set; }
    }
}