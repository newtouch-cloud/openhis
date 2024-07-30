using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtouch.Infrastructure.EF.Attributes;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 系统药品损益信息
    /// </summary>
    [Table("xt_yp_syxx")]
    public class SysMedicineProfitLossEntity : IEntity<SysMedicineProfitLossEntity>
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public string syId { get; set; }

        /// <summary>
        /// 组织机构
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 药房代码编号
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// 药品代码
        /// </summary>
        public string Ypdm { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Ph { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        public DateTime? Yxq { get; set; }

        /// <summary>
        /// 报告时间 默认值Getdate()
        /// </summary>
        public DateTime Bgsj { get; set; }

        /// <summary>
        /// 正数：报溢，负数：报损  最小单位数量
        /// </summary>
        public int Sysl { get; set; }

        /// <summary>
        /// 批发价
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Pfj { get; set; }

        /// <summary>
        /// 零售价
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Lsj { get; set; }

        /// <summary>
        /// 药库批发价
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Ykpfj { get; set; }

        /// <summary>
        /// 药库零售价
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Yklsj { get; set; }

        /// <summary>
        /// 转化因子
        /// </summary>
        public int Zhyz { get; set; }

        /// <summary>
        /// 损益原因
        /// </summary>
        public string Syyy { get; set; }

        /// <summary>
        /// 责任人
        /// </summary>
        public string Zrr { get; set; }

        /// <summary>
        /// 单据号
        /// </summary>
        public string Djh { get; set; }

        /// <summary>
        /// 剩余库存
        /// </summary>
        public int Sykc { get; set; }

        /// <summary>
        /// 进价
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Jj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// 批次
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

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
        public string remark { get; set; }

    }
}
