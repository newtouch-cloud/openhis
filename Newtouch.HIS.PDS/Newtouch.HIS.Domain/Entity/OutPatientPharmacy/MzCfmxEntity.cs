using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtouch.Infrastructure.EF.Attributes;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 门诊处方明细
    /// </summary>
    [Table("mz_cfmx")]
    public class MzCfmxEntity : IEntity<MzCfmxEntity>
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        /// <summary>
        /// 处方号
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// 药品代码
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// 药品名称
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// 处方明细内码
        /// </summary>
        public long Cfmxnm { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// 单位数量
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// 转换因子
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// 最小单位单价
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal? dj { get; set; }

        /// <summary>
        /// 生产厂商
        /// </summary>
        public string ycmc { get; set; }

        /// <summary>
        /// 金额 sl*dj
        /// </summary>
        public decimal je { get; set; }

        /// <summary>
        /// 剂量
        /// </summary>
        public decimal? jl { get; set; }

        /// <summary>
        /// 剂量单位
        /// </summary>
        public string jldw { get; set; }

        /// <summary>
        /// 用法名称
        /// </summary>
        public string yfmc { get; set; }

        /// <summary>
        /// 医生嘱托(备注)
        /// </summary>
        public string bz { get; set; }

        /// <summary>
        /// 药品分组
        /// </summary>
        public string czh { get; set; }

        /// <summary>
        /// 组织机构Id
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 状态 0-无效 1-有效
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 创建人员
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 修改人员
        /// </summary>
        public string LastModiFierCode { get; set; }

        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
    }
}
