using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 住院_退药申请单
    /// </summary>
    [Table("zy_returnDrugApplyBill")]
    public class ZyReturnDrugApplyBillEntity : IEntity<ZyReturnDrugApplyBillEntity>
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 申请单号
        /// </summary>
        public string applyNo { get; set; }

        /// <summary>
        /// 组织机构ID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 住院号
        /// </summary>
        public string zyh { get; set; }

        /// <summary>
        /// 医嘱ID
        /// </summary>
        public string yzId { get; set; }

        /// <summary>
        /// 发药药房
        /// </summary>
        public string fyyf { get; set; }

        /// <summary>
        /// 科室代码
        /// </summary>
        public string ksCode { get; set; }

        /// <summary>
        /// 病区代码
        /// </summary>
        public string bqCode { get; set; }

        /// <summary>
        /// 处理状态： 0-待处理 ；1-处理中 ；2-处理完成 
        /// </summary>
        public int ProcessState { get; set; }

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

        public string zytyapplyno { get; set; }
    }
}