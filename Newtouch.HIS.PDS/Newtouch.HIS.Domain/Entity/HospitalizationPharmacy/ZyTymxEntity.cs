using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 住院退药明细
    /// </summary>
    [Table("zy_tymx")]
    public class ZyTymxEntity : IEntity<ZyTymxEntity>
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
        /// 退药单号
        /// </summary>
        public string returnDrugBillNo { get; set; }

        /// <summary>
        /// 药品编号
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// 医嘱执行ID
        /// </summary>
        public string zxId { get; set; }

        /// <summary>
        /// 医嘱ID
        /// </summary>
        public string yzId { get; set; }

        /// <summary>
        /// 批号
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// 批次
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// 最小单位数量
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 状态  0-无效  1-有效
        /// </summary>
        public string zt { get; set; }

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
    }
}