using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 系统收费项目调价
    /// </summary>
    [Table("xt_sfxmtj")]
    public class SysChargeItemPriceAdjustEntity : IEntity<SysChargeItemPriceAdjustEntity>
    {
        /// <summary>
        /// 调价编号
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int tjbh { get; set; }

        /// <summary>
        /// 组织机构Id（有具体业务的医院）
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 收费项目
        /// </summary>
        public string sfxm { get; set; }

        /// <summary>
        /// 收费项目名称
        /// </summary>
        public string sfxmmc { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal dj { get; set; }

        /// <summary>
        /// 自负比例
        /// 确定自负现金比例，无关这部分的性质（分类自负、自理）   见字段zfxz      注意：当自负比例为负数时，表示定额自负   (例：某材料12300，可报10000,那么自负比例＝-2300)
        /// </summary>
        public decimal? zfbl { get; set; }

        /// <summary>
        /// 自负性质
        /// 确定自负部分（费用×自负比例）的性质   1 自理 2 分类自负 3 绝对自理（处理离休专家、特需挂号费）      （费用×（1－自负比例） 为可记帐部分）
        /// </summary>
        public string zfxz { get; set; }

        /// <summary>
        /// 状态
        /// 0：无效  1：有效
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 执行标志
        /// 0：未执行  1：已执行
        /// </summary>
        public string zxbz { get; set; }

        /// <summary>
        /// 执行日期
        /// </summary>
        public DateTime zxrq { get; set; }

        /// <summary>
        /// 定点生效日期
        /// </summary>
        public DateTime ddsxrq { get; set; }

        /// <summary>
        /// 生效标志
        /// 0：即时生效  1：定点生效
        /// </summary>
        public string sxbz { get; set; }

        /// <summary>
        /// 创建用户ID
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 最后修改用户ID
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? px { get; set; }

    }
}
