﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.OR.ManageSystem.Domain.Entity
{
    /// <summary>
    /// 创 建：chl
    /// 日 期：2019-10-31 15:44
    /// 描 述：麻醉字典表
    /// </summary>
    [Table("OR_Anesthesia")]
    public class ORAnesthesiaEntity : IEntity<ORAnesthesiaEntity>
    {
        /// <summary>
        /// OrganizeId
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 主键
        /// </summary>
        /// <returns></returns>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// AnesCode
        /// </summary>
        /// <returns></returns>
        public string AnesCode { get; set; }

        /// <summary>
        /// AnesName
        /// </summary>
        /// <returns></returns>
        public string AnesName { get; set; }

        /// <summary>
        /// Aneszjm
        /// </summary>
        /// <returns></returns>
        public string Aneszjm { get; set; }

        /// <summary>
        /// 状态     1:有效  0：无效
        /// </summary>
        /// <returns></returns>
        public string zt { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        /// <returns></returns>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        /// <returns></returns>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        /// <returns></returns>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 修改者
        /// </summary>
        /// <returns></returns>
        public string LastModifierCode { get; set; }

    }
}