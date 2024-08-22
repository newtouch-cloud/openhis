using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("zy_ypjfb")]
    public class HospDrugBillingEntity : IEntity<HospDrugBillingEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int jfbbh { get; set; }

        /// <summary>
        /// 组织机构Id（有具体业务的医院）
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zyh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string yzwym { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? zxid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? mxid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime tdrq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string yp { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string yfdm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string bzdm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte? ts { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public short? cls { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zlff { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ycqh { get; set; }

        /// <summary>
        /// 暂时不使用
        /// </summary>
        public decimal? yl { get; set; }

        /// <summary>
        /// 暂时不使用
        /// </summary>
        public string yldw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ys { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ysmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ksmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal dj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? fwfdj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string jfdw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? jmje { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? jmbl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? zfbl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zfxz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string lyyf { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public short? pyry { get; set; }

        /// <summary>
        /// 暂行规则：打印发药单即更新配药日期，且减库存。发药日期不处理。   	待今后需要护士领药时减库存再模仿门诊发药流程改造
        /// </summary>
        public DateTime? pyrq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string fyry { get; set; }

        /// <summary>
        /// 对应同步翻云医嘱的医嘱执行时间
        /// </summary>
        public DateTime? fyrq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zxks { get; set; }

        /// <summary>
        /// 1 临时医嘱   2 长期医嘱   3 出院医嘱
        /// </summary>
        public string yzxz { get; set; }

        /// <summary>
        /// 1 未撤销   2 已撤销   
        /// </summary>
        public string yzzt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cxry { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? cxrq { get; set; }

        /// <summary>
        /// 默认0   表示未撤销结算
        /// </summary>
        public int cxzyjfbbh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? price { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? lb { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ybbm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zltime { get; set; }

        /// <summary>
        /// 绑定执行Id
        /// </summary>
        public string bdzxid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

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
        public int? px { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string bq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string jzjhmxId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string kflb { get; set; }
        public int? zzfbz { get; set; }
    }
}
