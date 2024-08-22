using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("xt_card")]
    public class SysCardEntity : IEntity<SysCardEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string CardId { get; set; }

        /// <summary>
        /// 组织机构Id（有具体业务的医院）
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNo { get; set; }

        /// <summary>
        /// 卡类型
        /// </summary>
        public string CardType { get; set; }

        /// <summary>
        /// 卡类型名称
        /// </summary>
        public string CardTypeName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int patid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string hzxm { get; set; }

        /// <summary>
        /// 个人医保号
        /// </summary>
        public string grbh { get; set; }

        /// <summary>
        /// 参保类别
        /// </summary>
        public string cblb { get; set; }
        /// <summary>
        /// 参保地代码
        /// </summary>
        public string cbdbm { get; set; }
        /// <summary>
        /// 险种类型
        /// </summary>
        public string xzlx { get; set; }
        public string brxz { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Memo { get; set; }

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
