using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 住院药品医嘱操作记录
    /// </summary>
    [Table("zy_returnDrugApplyBillDetail")]
    public class ZyReturnDrugApplyBillDetailEntity : IEntity<ZyReturnDrugApplyBillDetailEntity>
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 退药申请单Id  zy_returnDrugApplyBill.Id
        /// </summary>
        public string rabId { get; set; }

        /// <summary>
        /// 药品代码
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// 退药数量（部门单位）
        /// </summary>
        public int tysl { get; set; }

        /// <summary>
        /// 转换因子（tysl*zhyz=最小单位数量）  默认住院拆零数
        /// </summary>
        public int? zhyz { get; set; }

        /// <summary>
        /// 组号
        /// </summary>
        public string zh { get; set; }

        /// <summary>
        /// 状态：0-无效 ；1-有效
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

        public long lyxh { get; set; }

        public string zytyapplyno { get; set; }
    }
}