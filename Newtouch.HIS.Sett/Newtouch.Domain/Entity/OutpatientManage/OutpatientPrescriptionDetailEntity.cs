using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 门诊处方明细
    /// </summary>
    [Table("mz_cfmx")]
    public class OutpatientPrescriptionDetailEntity : IEntity<OutpatientPrescriptionDetailEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int cfmxId { get; set; }

        /// <summary>
        /// 组织机构Id（有具体业务的医院）
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int cfnm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string yp { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal dj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal je { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? zfbl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zfxz { get; set; }

        /// <summary>
        /// 用量
        /// </summary>
        public decimal? yl { get; set; }

        /// <summary>
        /// 用量单位
        /// </summary>
        public string yldw { get; set; }

        /// <summary>
        /// 剂量
        /// </summary>
        public decimal? jl { get; set; }

        /// <summary>
        /// 剂量单位
        /// </summary>
        public string jldw { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// 退药标志。0 未退 1 已退 2 部分退(尚未启用)   药房收到退药后,此标志置1,   收费处退费后，此标志不操作（未发药则仍然是0，已发药仍为1）   --2007.03.27注释 收费处退费后,此标志置1(处理未领药之处方)      --2008-01-06 如果以后做部分退（收了2盒，退1盒），必须启用2(部分退)，而且要检查所有的报表和程序处理这个标志的地方
        /// </summary>
        public string tybz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string bz { get; set; }

        /// <summary>
        /// 门诊拆零数（对应多少个最小单位）
        /// </summary>
        public short? cls { get; set; }

        /// <summary>
        /// 药房代码（应该和主表一致）（一个药品处方的药应该是一个药房）
        /// </summary>
        public string yfdm { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 成组号 放在处方明细表
        /// </summary>
        public string czh { get; set; }

        /// <summary>
        /// 就诊计划明细ID
        /// </summary>
        public string jzjhmxId { get; set; }

        /// <summary>
        /// 康复类别
        /// </summary>
        public string kflb { get; set; }

        /// <summary>
        /// 用法代码
        /// </summary>
        public string yfCode { get; set; }
        /// <summary>
        /// 转自费标志
        /// </summary>
        public int? zzfbz { get; set; }
        /// <summary>
        /// 频次
        /// </summary>
        public string pcCode { get; set; }
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
    }
}
