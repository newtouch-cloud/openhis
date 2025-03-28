using Newtouch.HIS.API.Common;

namespace Newtouch.PDS.Requset.PharmacyDepartment
{
    /// <summary>
    /// 药房部门
    /// </summary>
    public class SysPharmacyDepartmentResponseDto 
    {
        /// <summary>
        /// Id
        /// </summary>
        public int yfbmId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string yfbmmc { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// 组织机构
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 门诊住院标志
        /// </summary>
        public string mzzybz { get; set; }

        /// <summary>
        /// 药剂部门级别
        /// </summary>
        public byte yjbmjb { get; set; }

        /// <summary>
        /// 关联科室
        /// </summary>
        public string ksCode { get; set; }

        /// <summary>
        /// 有效标志
        /// </summary>
        public string zt { get; set; }
    }
}