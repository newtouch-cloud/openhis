using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    [Table("xt_dzcfmx")]
    public class DzcfmxEntity : IEntity<DzcfmxEntity>
    {
        /// <summary>
        /// 处方明细id
        /// </summary>
        [Key]
        public string cfmxId { get; set; }

        /// <summary>
        /// 组织机构id
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 处方id
        /// </summary>
        public string cfId { get; set; }

        /// <summary>
        /// 项目代码
        /// </summary>
        public string xmCode { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string xmmc { get; set; }

        /// <summary>
        /// 药品代码
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// 药品名称
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal? dj { get; set; }

        /// <summary>
        /// 每次治疗量
        /// </summary>
        public int? mczll { get; set; }

        /// <summary>
        /// 每次剂量
        /// </summary>
        public decimal? mcjl { get; set; }

        /// <summary>
        /// 每次剂量单位
        /// </summary>
        public string mcjldw { get; set; }

        /// <summary>
        /// 用法代码
        /// </summary>
        public string yfCode { get; set; }

        /// <summary>
        /// 频次代码
        /// </summary>
        public string pcCode { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int? sl { get; set; }

        /// <summary>
        /// 总量
        /// </summary>
        public int? zl { get; set; }

        /// <summary>
        /// 总量单位
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal? je { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 创建用户id
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 最后修改用户id
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 组号
        /// </summary>
        public string zh { get; set; }

        /// <summary>
        /// 贴数
        /// </summary>
        public string bw { get; set; }

        /// <summary>
        /// 加急
        /// </summary>
        public string urgent { get; set; }

        /// <summary>
        /// 目的
        /// </summary>
        public string purpose { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 执行科室
        /// </summary>
        public string zxks { get; set; }

        /// <summary>
        /// 执行时间
        /// </summary>
        public DateTime? zxsj { get; set; }

        /// <summary>
        /// 医保唯一码
        /// </summary>
        public string ybwym { get; set; }

        /// <summary>
        /// 限制使用标志
        /// </summary>
        public string xzsybz { get; set; }

        /// <summary>
        /// 天数
        /// </summary>
        public decimal? ts { get; set; }

        /// <summary>
        /// 抗生素使用原因
        /// </summary>
        public string kssReason { get; set; }

        /// <summary>
        /// 组套id
        /// </summary>
        public string ztId { get; set; }

        /// <summary>
        /// 组套名称
        /// </summary>
        public string ztmc { get; set; }

        /// <summary>
        /// 部位
        /// </summary>
        public string bwff { get; set; }

        /// <summary>
        /// 申请类型
        /// </summary>
        public string sqlx { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// 滴速
        /// </summary>
        public int? ds { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? zzfbz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ghlybz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string syncfbz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ispsbz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string islgbz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? ztsl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? sfzt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zysm { get; set; }

    }
}
