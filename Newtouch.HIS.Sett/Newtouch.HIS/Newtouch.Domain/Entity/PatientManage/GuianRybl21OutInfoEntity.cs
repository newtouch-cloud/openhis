using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("Gzyb_OutRYBL_21")]
    public class GuianRybl21OutInfoEntity : IEntity<GuianRybl21OutInfoEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        /// <summary>
        /// 住院号
        /// </summary>
        public string prm_ykc010 { get; set; }
        /// <summary>
        /// 就诊编号
        /// </summary>
        public string prm_akc190 { get; set; }
        /// <summary>
        /// 分中心编号(HIS必须保存该字段，作为以后交易的输入)
        /// </summary>
        public string prm_yab003 { get; set; }
        /// <summary>
        /// 支付类别
        /// </summary>
        public string prm_aka130 { get; set; }
        /// <summary>
        /// 个人编号
        /// </summary>
        public string prm_aac001 { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string prm_aac003 { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string prm_aac004 { get; set; }
        /// <summary>
        /// 身份号码
        /// </summary>
        public string prm_aac002 { get; set; }
        /// <summary>
        /// 单位编码
        /// </summary>
        public string prm_aab001 { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        public string prm_aab004 { get; set; }
        /// <summary>
        /// 人员状态
        /// </summary>
        public string prm_akc021 { get; set; }
        /// <summary>
        /// 实足年龄
        /// </summary>
        public int prm_akc023 { get; set; }
        /// <summary>
        /// 社会保险办法(HIS必须保存该字段，作为以后交易的输入)
        /// </summary>
        public string prm_ykb065 { get; set; }
        /// <summary>
        /// 组织机构Id
        /// </summary>
        public string OrganizeId { get; set; }
        /// <summary>
        /// 交易流水号
        /// </summary>
        public string jylsh { get; set; }
        /// <summary>
        /// 交易验证码
        /// </summary>
        public string jyyzm { get; set; }
    }
}
