using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("xt_xyzd")]
    public class WMDiagnosisEntity : IEntity<WMDiagnosisEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string xyzdId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 关联就诊表
        /// </summary>
        public string jzId { get; set; }

        /// <summary>
        /// 枚举 1 主诊断 2 辅诊断
        /// </summary>
        public int zdlx { get; set; }

        /// <summary>
        /// 关联base库 系统诊断表
        /// </summary>
        public string zdCode { get; set; }

        /// <summary>
        /// 冗余字段
        /// </summary>
        public string zdmc { get; set; }

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
        
        /// <summary>
        /// 疑似标志
        /// </summary>
        public bool ysbz { get; set; }
        /// <summary>
        /// 西医诊断备注
        /// </summary>
        public string zdbz { get; set; }
        /// <summary>
        /// 牙位图类型
        /// </summary>
        public string ywlx { get; set; }
        /// <summary>
        /// 牙位图位置
        /// </summary>
        public string ywstr { get; set; }
        /// <summary>
        /// 牙位图显示
        /// </summary>
        public string ywxs { get; set; }
    }
}
