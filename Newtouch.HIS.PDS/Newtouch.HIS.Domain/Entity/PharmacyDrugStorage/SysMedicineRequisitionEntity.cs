using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 系统药品申领单
    /// </summary>
    [Table("xt_yp_sld")]
    public class SysMedicineRequisitionEntity : IEntity<SysMedicineRequisitionEntity>
    {
        /// <summary>
        /// 申领单ID
        /// </summary>
        [Key]
        public string sldId { get; set; }

        /// <summary>
        /// 组织机构ID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 申领单号
        /// </summary>
        public string Sldh { get; set; }

        /// <summary>
        /// 申领部门
        /// </summary>
        public string Slbm { get; set; }

        /// <summary>
        /// 出库部门
        /// </summary>
        public string Ckbm { get; set; }

        /// <summary>
        /// 发放状态 0:未发 1:已发部分 2:已全发 3:已终止
        /// </summary>
        public int ffzt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

    }
}
