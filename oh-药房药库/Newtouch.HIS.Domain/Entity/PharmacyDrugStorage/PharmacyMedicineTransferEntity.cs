using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity.PharmacyDrugStorage
{
    /// <summary>
    /// 药房药品调拨
    /// </summary>
    [Table("yf_ypdbsq")]
    public class PharmacyMedicineTransferEntity : IEntity<PharmacyMedicineTransferEntity>
    {
        /// <summary>
        /// 申领单ID
        /// </summary>
        [Key]
        public long dbId { get; set; }

        /// <summary>
        /// 组织机构ID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 申领单号
        /// </summary>
        public string Dbdh { get; set; }

        /// <summary>
        /// 申请药房
        /// </summary>
        public string SqyfCode { get; set; }

        /// <summary>
        /// 出库药房
        /// </summary>
        public string CkyfCode { get; set; }

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
