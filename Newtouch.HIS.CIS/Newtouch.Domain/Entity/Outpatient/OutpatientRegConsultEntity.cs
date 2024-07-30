using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.Entity.Outpatient
{
    /// <summary>
    /// 门诊挂号诊室关系表
    /// </summary>
    [Table("mz_ghzs")]
    public class OutpatientRegConsultEntity: IEntity<OutpatientRegConsultEntity>
    {
        [Key]
        public int Id { get; set; }
        public int ghnm { get; set; }
        public string zsCode { get; set; }
        public string OrganizeId { get; set; }
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }

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
        /// <summary>
        /// 叫号状态 （待叫号1，已叫2，过号3 应答4 重新叫号5）
        /// </summary>
        public int? calledstu { get; set; }

    }
}
