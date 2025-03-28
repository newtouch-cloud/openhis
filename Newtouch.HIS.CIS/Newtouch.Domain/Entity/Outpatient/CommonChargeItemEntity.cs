using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("mz_cyxm")]
    public class CommonChargeItemEntity : IEntity<CommonChargeItemEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string cyxmId { get; set; }

        /// <summary>
        /// 枚举 1 科室常用 2 个人常用
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// 关联base库的科室表
        /// </summary>
        public string ksCode { get; set; }

        /// <summary>
        /// 关联base库的人员表
        /// </summary>
        public string ysgh { get; set; }

        /// <summary>
        /// 枚举 1 康复项目 2 检验项目 3检查项目
        /// </summary>
        public int xmlx { get; set; }

        /// <summary>
        /// 关联base库的收费项目表
        /// </summary>
        public string xmCode { get; set; }

        /// <summary>
        /// 冗余字段
        /// </summary>
        public string xmmc { get; set; }

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
