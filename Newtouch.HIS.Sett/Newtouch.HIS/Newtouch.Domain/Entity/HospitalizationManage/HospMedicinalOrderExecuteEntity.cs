using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("Zy_Ypyzzx")]
    public class HospMedicinalOrderExecuteEntity : IEntity<HospMedicinalOrderExecuteEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int yzzxId { get; set; }

        public int yzId { get; set; }//新增 医嘱ID
        /// <summary>
        /// 组织机构Id（有具体业务的医院）
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Dj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Sl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Je { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Zfbl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Zfxz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Ks { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string bqCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Cw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Fybz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Lyyf { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Pyry { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? Pyrq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Fyry { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? Fyrq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Cxry { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? Cxrq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Ypfz { get; set; }

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

    }
}
