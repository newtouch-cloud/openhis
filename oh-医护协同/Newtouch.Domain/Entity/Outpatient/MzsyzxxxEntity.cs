using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 门诊输液执行信息
    /// </summary>
    [Table("mz_syzxxx")]
    public class MzsyzxxxEntity : IEntity<MzsyzxxxEntity>
    {

        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 组织机构
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// mz_syypxx表主键
        /// </summary>
        public long syypxxId { get; set; }

        /// <summary>
        /// 座位号
        /// </summary>
        public string seatNum { get; set; }

        /// <summary>
        /// 输液开始时间
        /// </summary>
        public DateTime? sykssj { get; set; }

        /// <summary>
        /// 输液结束时间
        /// </summary>
        public DateTime? syjssj { get; set; }

        /// <summary>
        /// 配药师工号
        /// </summary>
        public string dispenser { get; set; }

        /// <summary>
        /// 执行人员工号
        /// </summary>
        public string executor { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 状态 0：无效  1：有效
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
