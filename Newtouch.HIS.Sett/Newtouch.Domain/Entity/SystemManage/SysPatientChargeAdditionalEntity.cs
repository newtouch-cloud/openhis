using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 系统病人收费附加（17楼）
    /// </summary>
    [Table("xt_brsffj")]
    public class SysPatientChargeAdditionalEntity : IEntity<SysPatientChargeAdditionalEntity>
    {
        /// <summary>
        /// 病人收费附加编号
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int brsffjbh { get; set; }

        /// <summary>
        /// 组织机构Id（有具体业务的医院）
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 病人性质
        /// </summary>
        public string brxz { get; set; }

        /// <summary>
        /// 附加显示大类
        /// </summary>
        public string fjxsdl { get; set; }

        /// <summary>
        /// 服务费比例
        /// </summary>
        public decimal fwfbl { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 创建用户ID
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 最后修改用户ID
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? px { get; set; }
        /// <summary>
        /// 大类
        /// </summary>
        public string dl { get; set; }
        /// <summary>
        /// 收费项目
        /// </summary>
        public string sfxm { get; set; }

    }
}
