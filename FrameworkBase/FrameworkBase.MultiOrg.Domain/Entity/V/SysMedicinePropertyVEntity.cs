using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworkBase.MultiOrg.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("V_S_xt_ypsx")]
    public class SysMedicinePropertyVEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        public int ypsxId { get; set; }

        /// <summary>
        /// 关联药品Id
        /// </summary>
        public int ypId { get; set; }

        /// <summary>
        /// 药品Code
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// 0 普通项目 1 特殊项目特殊药品说明是特定的病人性质才可使用的。
        /// </summary>
        public string tsbz { get; set; }

        /// <summary>
        /// 贵重药
        /// </summary>
        public string gzy { get; set; }

        /// <summary>
        /// 麻醉药
        /// </summary>
        public string mzy { get; set; }

        /// <summary>
        /// 精神药
        /// </summary>
        public string yljsy { get; set; }

        /// <summary>
        /// 治疗方法（同样 关联 药品用法）
        /// </summary>
        public string zlff { get; set; }

        /// <summary>
        /// 时间安排
        /// </summary>
        public string sjap { get; set; }

        /// <summary>
        /// 用量
        /// </summary>
        public decimal? yl { get; set; }

        /// <summary>
        /// 用量单位
        /// </summary>
        public string yldw { get; set; }

        /// <summary>
        /// 药品规格
        /// </summary>
        public string ypgg { get; set; }

        /// <summary>
        /// 医保代码
        /// </summary>
        public string ybdm { get; set; }

        /// <summary>
        /// 使用天数（医保控量设置字段）
        /// </summary>
        public int? syts { get; set; }

        /// <summary>
        /// 单次最大剂量（医保控量设置字段）
        /// </summary>
        public decimal? dczdjl { get; set; }

        /// <summary>
        /// 单次最大数量（医保控量设置字段）
        /// </summary>
        public decimal? dczdsl { get; set; }

        /// <summary>
        /// 累计最大剂量（医保控量设置字段）
        /// </summary>
        public decimal? ljzdjl { get; set; }

        /// <summary>
        /// 累计最大数量（医保控量设置字段）
        /// </summary>
        public decimal? ljzdsl { get; set; }

        /// <summary>
        /// 批准文号
        /// </summary>
        public string pzwh { get; set; }

        /// <summary>
        /// 药品特殊属性
        /// </summary>
        public string yptssx { get; set; }

        /// <summary>
        /// 药品分类Code
        /// </summary>
        public string ypflCode { get; set; }

        /// <summary>
        /// 记账类型    0：每顿取整收费1：每次取整收费
        /// </summary>
        public string jzlx { get; set; }

        /// <summary>
        /// 默认保质期
        /// </summary>
        public int? mrbzq { get; set; }

        /// <summary>
        /// 供货单位
        /// </summary>
        public string ghdw { get; set; }

        /// <summary>
        /// 药品产地
        /// </summary>
        public int? ypcd { get; set; }

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
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

    }
}
