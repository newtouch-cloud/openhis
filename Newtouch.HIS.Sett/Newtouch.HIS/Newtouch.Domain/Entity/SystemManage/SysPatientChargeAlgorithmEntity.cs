using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("xt_brsfsf")]
    public class SysPatientChargeAlgorithmEntity : IEntity<SysPatientChargeAlgorithmEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int brsfsfbh { get; set; }

        /// <summary>
        /// 组织机构Id（有具体业务的医院）
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string brxz { get; set; }

        /// <summary>
        /// *代表所有收费大类 (dl)
        /// </summary>
        public string dl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfxm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte sfjb { get; set; }

        /// <summary>
        /// 什么范围内的费用参与自费比例的计算      0 可记帐部分参与计算（暂无）   1 可记帐＋分类自负（离休、慈善等大多数……）   2 可记帐＋分类自负＋自理－绝对自理（离休挂号费0.1时,绝对自理处理离休特需挂号等不可报部分－见系统收费项目.自负性质）      “同大类”的“所有级别”的可报范围必须一致
        /// </summary>
        public string fyfw { get; set; }

        /// <summary>
        /// 确定记帐、自负比例，无论医保是否可保（见自费标志）   当比例为负数时，表示定额自负，不是按比例了。例如（离休挂号费定额支付）
        /// </summary>
        public decimal zfbl { get; set; }

        /// <summary>
        /// 确定按自负比例计算出来的自负部分的性质   0 自负（同医保交易后自负现金）   1 自理
        /// </summary>
        public string zfxz { get; set; }

        /// <summary>
        /// 0 所有费用皆使用此比例，不判断上限
        /// </summary>
        public decimal fysx { get; set; }

        /// <summary>
        /// 0：通用，1：门诊，2：住院
        /// </summary>
        public string mzzybz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

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

    }
}
