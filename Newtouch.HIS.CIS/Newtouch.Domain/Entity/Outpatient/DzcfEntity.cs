using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    [Table("xt_dzcf")]
    public class DzcfEntity : IEntity<DzcfEntity>
    {

        /// <summary>
        /// 处方id
        /// </summary>
        [Key]
        public string cfId { get; set; }

        /// <summary>
        /// 组织机构id
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 就诊id
        /// </summary>
        public string jzId { get; set; }

        /// <summary>
        /// 收费标志
        /// </summary>
        public bool sfbz { get; set; }

        /// <summary>
        /// 处方类型
        /// </summary>
        public int cflx { get; set; }

        /// <summary>
        /// 处方号
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// 总金额
        /// </summary>
        public decimal zje { get; set; }

        /// <summary>
        /// 医生
        /// </summary>
        public string ys { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建用户
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

        /// <summary>
        /// 状态
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 贴数
        /// </summary>
        public int? tieshu { get; set; }

        /// <summary>
        /// 处方
        /// </summary>
        public string cfyf { get; set; }

        /// <summary>
        /// 是否代煎   1是  0否
        /// </summary>
        public bool? djbz { get; set; }

        /// <summary>
        /// 科室
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// 同步标志
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
        /// 申请单号
        /// </summary>
        public string sqdh { get; set; }

        /// <summary>
        /// 远程医疗上传状态
        /// </summary>
        public string SyncStatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cftag { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string djfs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string djts { get; set; }

        /// <summary>
        /// 处方状态
        /// </summary>
        public string cfzt { get; set; }

        /// <summary>
        /// 处方整剂医嘱说明
        /// </summary>
        public string rxDrordDscr { get; set; }

        /// <summary>
        /// 处方有效天数
        /// </summary>
        public int? valiDays { get; set; }

        /// <summary>
        /// 复用(多次)使用标志
        /// </summary>
        public string reptFlag { get; set; }

        /// <summary>
        /// 最大使用次数
        /// </summary>
        public int? maxReptCnt { get; set; }

        /// <summary>
        /// 使用最小间隔(天数)
        /// </summary>
        public int? minInrvDays { get; set; }

        /// <summary>
        /// 续方标志
        /// </summary>
        public string rxCotnFlag { get; set; }

        /// <summary>
        /// 长期处方标志
        /// </summary>
        public string longRxFlag { get; set; }

        /// <summary>
        /// 处方追溯码
        /// </summary>
        public string rxTraceCode { get; set; }

        /// <summary>
        /// 医保处方编号
        /// </summary>
        public string hiRxno { get; set; }
    }
}
