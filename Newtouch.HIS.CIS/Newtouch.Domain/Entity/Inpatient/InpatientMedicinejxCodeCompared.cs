using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.Entity.Inpatient
{
    [Table("zy_ypjxyfdz")]
    public class InpatientMedicinejxCodeComparedEntity : IEntity<InpatientMedicinejxCodeComparedEntity>
    {
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        [Key]
        public string Id { get; set; }

        public string OrganizeId { get; set; }

        public string jxCode { get; set; }

        public string yfbmCode { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>
        /// <returns></returns>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// CreatorCode
        /// </summary>
        /// <returns></returns>
        public string CreatorCode { get; set; }
        /// <summary>
        /// LastModifyTime
        /// </summary>
        /// <returns></returns>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// LastModifierCode
        /// </summary>
        /// <returns></returns>
        public string LastModifierCode { get; set; }
        /// <summary>
        /// 0无效1有效
        /// </summary>
        /// <returns></returns>
        public string zt { get; set; }

    }
}
