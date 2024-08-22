using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 新农合门诊补偿序号关联表
    /// </summary>
    [Table("mz_xnh_outpIdRel")]
    public class OutpatientXnhOutpIdRelEntity : IEntity<OutpatientXnhOutpIdRelEntity>
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 组织机构Id（有具体业务的医院）
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 挂号内码
        /// </summary>
        public int ghnm { get; set; }

        /// <summary>
        /// 补偿序号
        /// </summary>
        public string outpId { get; set; }

        /// <summary>
        /// 门诊号
        /// </summary>
        public string mzh { get; set; }

        /// <summary>
        /// 处理状态 0-门诊回退 1-明细已上传 2-已结算 3-已红冲 
        /// </summary>
        public int processingStatus { get; set; }

        /// <summary>
        /// 状态 1-有效  0-无效
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 最后修改者
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
    }
}