using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 系统预约挂号维护
    /// </summary>
    [Table("xt_bespeakRegister")]
    public class SysBespeakRegisterEntity : IEntity<SysBespeakRegisterEntity>
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
        /// 门诊住院标志 1：普通门诊  2：急诊   3：专家门诊
        /// </summary>
        public string mzlx { get; set; }

        /// <summary>
        /// 挂号日期
        /// </summary>
        public DateTime regDate { get; set; }

        /// <summary>
        /// 挂号开始时段
        /// </summary>
        public DateTime regBeginTime { get; set; }

        /// <summary>
        /// 挂号结束时段
        /// </summary>
        public DateTime regEndTime { get; set; }

        /// <summary>
        /// 挂号时段,挂号开始时段和挂号结束时段结合
        /// </summary>
        public string regTime { get; set; }

        /// <summary>
        /// 科室代码
        /// </summary>
        public string departmentCode { get; set; }

        /// <summary>
        /// 医生工号
        /// </summary>
        public string ysgh { get; set; }

        /// <summary>
        /// 最大预约数
        /// </summary>
        public int bespeakMaxCount { get; set; }

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

        /// <summary>
        /// 状态     1:有效  0：无效
        /// </summary>
        public string zt { get; set; }
    }
}
