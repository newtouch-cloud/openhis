using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("xt_cf")]
    public class PrescriptionEntity : IEntity<PrescriptionEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string cfId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 关联就诊表
        /// </summary>
        public string jzId { get; set; }

        /// <summary>
        /// billing系统的收费状态
        /// </summary>
        public bool sfbz { get; set; }

        /// <summary>
        /// 枚举 1 西药处方 2 中药处方 3 康复处方 4 检验处方 5 检查处方 6 输液处方
        /// </summary>
        public int cflx { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal zje { get; set; }

        /// <summary>
        /// 关联base库的人员工号
        /// </summary>
        public string ys { get; set; }

        /// <summary>
        /// 科室
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? tieshu { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cfyf { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? djbz { get; set; }

        /// <summary>
        /// 同步标志，是否已同步至HIS（后判断是否已同步 改成依赖操作记录表）
        /// </summary>
        public bool? tbbz { get; set; }

        /// <summary>
        /// 临床原因
        /// </summary>
        public string lcyx { get; set; }
        /// <summary>
        /// 申请备注
        /// </summary>
        public string sqbz { get; set; }
        /// <summary>
        /// 退标志
        /// </summary>
        public bool? tbz { get; set; }

        /// <summary>
        /// 申请单号（远程医疗申请需要guid）
        /// </summary>
        public string sqdh { get; set; }
        /// <summary>
        /// 处方标签 JI 精I JII 精II MZ 麻醉
        /// </summary>
        public string cftag { get; set; }
        /// <summary>
        /// 代煎方式
        /// </summary>
        public string djfs { get; set; }
        // <summary>
        /// 代煎付数
        /// </summary>
        public string djts { get; set; }
        // <summary>
        /// 嘱托
        /// </summary>
        public string cfzt { get; set; }
       

    }
}
