using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworkBase.MultiOrg.Domain.Entity
{
    /// <summary>
    /// 系统病房
    /// </summary>
    [Table("V_S_xt_bf")]
    public class SysWardRoomVEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        public int bfId { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string bfCode { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string bfNo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ksCode { get; set; }

        /// <summary>
        /// 组织机构
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 所属病区
        /// </summary>
        public string bqCode { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string bz { get; set; }

        /// <summary>
        /// 有效标志
        /// </summary>
        public string zt { get; set; }
    }
}
