using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 门诊已预约挂号
    /// </summary>
    [Table("mz_yygh")]
    public class MzyyghEntity : IEntity<MzyyghEntity>
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 组织结构
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// xt_bespeakRegister 主键ID
        /// </summary>
        public string brId { get; set; }

        /// <summary>
        /// 证件类型
        /// </summary>
        public int? zjlx { get; set; }

        /// <summary>
        /// 证件号
        /// </summary>
        public string zjh { get; set; }

        /// <summary>
        /// 病历号
        /// </summary>
        public string blh { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        public string kh { get; set; }

        /// <summary>
        /// 预约号
        /// </summary>
        public int bespeakNo { get; set; }

        /// <summary>
        /// 赴约时间
        /// </summary>
        public DateTime? arrivalDate { get; set; }

        /// <summary>
        /// 现场处理挂号人员
        /// </summary>
        public string arrivalOpereater { get; set; }

        /// <summary>
        /// 状态     1:有效  0：无效
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 修改者
        /// </summary>
        public string LastModifierCode { get; set; }
    }
}
