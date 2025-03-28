using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 住院药品医嘱操作记录
    /// </summary>
    [Table("zy_ypyzczjl")]
    public class ZyYpyzczjlEntity : IEntity<ZyYpyzczjlEntity>
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// 组织机构Id
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 药品医嘱信息ID zy_ypyzxx表主键ID
        /// </summary>
        public long ypyzxxId { get; set; }

        /// <summary>
        /// 医嘱执行ID
        /// </summary>
        public string zxId { get; set; }

        /// <summary>
        /// 医嘱ID
        /// </summary>
        public string yzId { get; set; }

        /// <summary>
        /// 1：发药 2：退药
        /// </summary>
        public string operateType { get; set; }

        /// <summary>
        /// 药品编号
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// 最小单位数量
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string bz { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 修改用户
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 最后修改用户
        /// </summary>
        public string LastModifierCode { get; set; }

        public string zytyapplyno { get; set; }

        /// <summary>
        /// 住院发药申请ID
        /// </summary>
        public string zyfyapplyno { get; set; }

        /// <summary>
        /// 追溯码
        /// </summary>
        public string zsm { get; set; }

        /// <summary>
        /// 是否拆零
        /// 1： 是
        /// 2： 否
        /// </summary>
        public int? sfcl { get; set; }
    }
}
