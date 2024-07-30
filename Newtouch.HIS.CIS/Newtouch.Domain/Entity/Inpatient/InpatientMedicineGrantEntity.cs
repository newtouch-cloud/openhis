using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2018-06-28 15:37
    /// 描 述：住院发药请求库
    /// </summary>
    [Table("zy_fyqqk")]
    public class InpatientMedicineGrantEntity : IEntity<InpatientMedicineGrantEntity>
    {
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// OrganizeId
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }
        /// <summary>
        /// lyxh
        /// </summary>
        /// <returns></returns>
        public decimal lyxh { get; set; }
        /// <summary>
        /// zyh
        /// </summary>
        /// <returns></returns>
        public string zyh { get; set; }
        /// <summary>
        /// hzxm
        /// </summary>
        /// <returns></returns>
        public string hzxm { get; set; }
        /// <summary>
        /// yzxh
        /// </summary>
        /// <returns></returns>
        public string yzxh { get; set; }
        /// <summary>
        /// fzxh
        /// </summary>
        /// <returns></returns>
        public int? fzxh { get; set; }
        /// <summary>
        /// yfdm
        /// </summary>
        /// <returns></returns>
        public string yfdm { get; set; }
        /// <summary>
        /// WardCode
        /// </summary>
        /// <returns></returns>
        public string WardCode { get; set; }
        /// <summary>
        /// DeptCode
        /// </summary>
        /// <returns></returns>
        public string DeptCode { get; set; }
        /// <summary>
        /// ysgh
        /// </summary>
        /// <returns></returns>
        public string ysgh { get; set; }
        /// <summary>
        /// zxrq
        /// </summary>
        /// <returns></returns>
        public DateTime zxrq { get; set; }
        /// <summary>
        /// qqrq
        /// </summary>
        /// <returns></returns>
        public DateTime qqrq { get; set; }
        /// <summary>
        /// fyrq
        /// </summary>
        /// <returns></returns>
        public DateTime? fyrq { get; set; }
        /// <summary>
        /// ypdm
        /// </summary>
        /// <returns></returns>
        public string ypdm { get; set; }
        /// <summary>
        /// ypmc
        /// </summary>
        /// <returns></returns>
        public string ypmc { get; set; }
        /// <summary>
        /// ypsl
        /// </summary>
        /// <returns></returns>
        public int ypsl { get; set; }
        /// <summary>
        /// ypgg
        /// </summary>
        /// <returns></returns>
        public string ypgg { get; set; }
        /// <summary>
        /// ypdw
        /// </summary>
        /// <returns></returns>
        public string ypdw { get; set; }
        /// <summary>
        /// dwxs
        /// </summary>
        /// <returns></returns>
        public decimal? dwxs { get; set; }
        /// <summary>
        /// ykxs
        /// </summary>
        /// <returns></returns>
        public decimal? ykxs { get; set; }
        /// <summary>
        /// ypdj
        /// </summary>
        /// <returns></returns>
        public decimal? ypdj { get; set; }
        /// <summary>
        /// zxcs
        /// </summary>
        /// <returns></returns>
        public int zxcs { get; set; }
        /// <summary>
        /// （1发药 2退药）
        /// </summary>
        /// <returns></returns>
        public int? tybz { get; set; }
        /// <summary>
        /// fyczyh
        /// </summary>
        /// <returns></returns>
        public string fyczyh { get; set; }
        /// <summary>
        /// （0临时1长期）
        /// </summary>
        /// <returns></returns>
        public int? yzxz { get; set; }
        /// <summary>
        /// zbbz
        /// </summary>
        /// <returns></returns>
        public int? zbbz { get; set; }
        /// <summary>
        /// memo
        /// </summary>
        /// <returns></returns>
        public string memo { get; set; }
        /// <summary>
        /// mcsl
        /// </summary>
        /// <returns></returns>
        public decimal? mcsl { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>
        /// <returns></returns>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// CreatorCode
        /// </summary>
        /// <returns></returns>
        public string CreatorCode { get; set; }
        /// <summary>
        /// LastModifyTime
        /// </summary>
        /// <returns></returns>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// LastModifierCode
        /// </summary>
        /// <returns></returns>
        public string LastModifierCode { get; set; }
        /// <summary>
        /// 0无效1有效
        /// </summary>
        /// <returns></returns>
        public string zt { get; set; }

        public int? ytsl { get; set; }
    }
}