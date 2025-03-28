using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Newtouch.EMR.Domain.Entity
{

    [Table("bl_hljl_srscl")]
    public class bl_hljl_srsclEntity : IEntity<bl_hljl_srsclEntity>
    {
        [Key]
        public string Id { get; set; }
        public string rq { get; set; }
        public string sj { get; set; }
        public string zyh { get; set; }
        /// <summary>
        /// 护理类型 0一般 1危重 2手术
        /// </summary>
        public string bllx { get; set; }
        /// <summary>
        /// 摄入摄出量类型 0摄入 1摄出
        /// </summary>
        public string srsclx { get; set; }
        public string mc { get; set; }
        public string ml { get; set; }
        public string dw { get; set; }
        /// <summary>
        /// 途径
        /// </summary>
        public string tj { get; set; }
        /// <summary>
        /// 颜色形状
        /// </summary>
        public string ysxz { get; set; }
        public System.DateTime CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public Nullable<System.DateTime> LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
        public string zt { get; set; }
        public string OrganizeId { get; set; }
    }
}
