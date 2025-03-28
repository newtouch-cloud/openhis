using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity.SystemManage
{
    /// <summary>
    /// GRS收入信息详情表
    /// </summary>
    [Table("jgss_srxxdetail")]
    public class jgss_EarningDetailInfoEntity : IEntity<jgss_EarningDetailInfoEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string srxxId { get; set; }
        /// <summary>
        /// 门诊收费金额
        /// </summary>
        public decimal mzsfje { get; set; }
        /// <summary>
        /// 门诊执行金额
        /// </summary>
        public decimal mzzxje { get; set; }
        /// <summary>
        /// 住院收费金额
        /// </summary>
        public decimal zysfje { get; set; }
        /// <summary>
        /// 住院执行金额
        /// </summary>
        public decimal zyzxje { get; set; }
        /// <summary>
        /// 总收入
        /// </summary>
        public decimal zsr { get; set; }
        /// <summary>
        /// 大类code
        /// </summary>
        public string dlCode { get; set; }
        /// <summary>
        /// 大类名称
        /// </summary>
        public string dlmc { get; set; }
        public string CreatorCode { get; set; }
        public DateTime CreateTime { get; set; }
        public string LastModifierCode { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string zt { get; set; }
        /// <summary>
        /// 核实收入
        /// </summary>
        public decimal hssr { get; set; }
        /// <summary>
        /// 差额
        /// </summary>
        public decimal ce { get; set; }
        /// <summary>
        /// 调整说明
        /// </summary>
        public string tzsm { get; set; }
        /// <summary>
        /// 分成比例
        /// </summary>
        public decimal fcbl { get; set; }
    }
}
