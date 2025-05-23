﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity.Settlement
{
    [Table("xt_qxkz_rel")]
    public class SysMedicineAuthorityRelationEntity : IEntity<SysMedicineAuthorityRelationEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string qxId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string gh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }
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
        public string memo { get; set; }

    }
}
