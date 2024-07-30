using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 出诊科室设置
    /// </summary>
    [Table("visit_deptSet")]
    public class VisitDeptSetEntity : IEntity<VisitDeptSetEntity>
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
        /// 医生工号
        /// </summary>
        public string ysgh { get; set; }

        /// <summary>
        /// 出诊科室代码(包含所属科室)
        /// </summary>
        public string visitksCode { get; set; }

        /// <summary>
        /// 出诊类型  1-普通门诊；2-急诊；3-专家门诊
        /// </summary>
        public int czlx { get; set; }

        /// <summary>
        /// 所属科室代码
        /// </summary>
        public string SubordinateDepartments { get; set; }

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