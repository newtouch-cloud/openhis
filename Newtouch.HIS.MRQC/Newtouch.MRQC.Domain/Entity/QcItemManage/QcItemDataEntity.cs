using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.MRQC.Domain.Entity.QcItemManage
{
    /// <summary>
    /// 
    /// </summary>
    [Table("MR_Qc_ItemData")]
    public class QcItemDataEntity : IEntity<QcItemDataEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        ///erm 病历类型、模板Id
        /// </summary>
        public string BlmbId { get; set; }

        /// <summary>
        /// 病历类型、模板名称
        /// </summary>
        public string Bllmbmc { get; set; }

        /// <summary>
        /// 质控类型 1:病历类型  2：病历模板
        /// </summary>
        public string zklx { get; set; }

        /// <summary>
        /// 质控代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 质控项名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 质控评分
        /// </summary>
        public decimal Score { get; set; }
        /// <summary>
        /// 评分说明
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int? Px { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string zt { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string LastModifierCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }
    }
}
