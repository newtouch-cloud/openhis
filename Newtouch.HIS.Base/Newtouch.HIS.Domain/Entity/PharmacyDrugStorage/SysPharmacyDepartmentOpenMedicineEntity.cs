﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("xt_yfbm_yp")]
    public class SysPharmacyDepartmentOpenMedicineEntity : IEntity<SysPharmacyDepartmentOpenMedicineEntity>
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 药房部门编号
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// 大类编号
        /// </summary>
        public string dlCode { get; set; }

        /// <summary>
        /// 组织机构Id
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 修改用户
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 最后修改用户
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 状态 0-无效  1-有效
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? px { get; set; }

    }
}
