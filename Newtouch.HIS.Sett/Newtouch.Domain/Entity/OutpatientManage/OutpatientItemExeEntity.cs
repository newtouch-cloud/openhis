using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 门诊项目
    /// </summary>
    [Table("mz_jzjhmxzx")]
    public class OutpatientItemExeEntity : IEntity<OutpatientItemExeEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 关联mz_jzjhmx表主键
        /// </summary>
        public string jzjhmxId { get; set; }

        /// <summary>
        /// 组织机构Id（有具体业务的医院）
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 执行时间
        /// </summary>
        public DateTime zxsj { get; set; }

        /// <summary>
        /// 康复类别（字典RTM）
        /// </summary>
        public string kflb { get; set; }

        /// <summary>
        /// 治疗量
        /// </summary>
        public int? zll { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// 创建人
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
        /// 最后修改人
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 治疗师工号
        /// </summary>
        public string zlsgh { get; set; }

    }
}
