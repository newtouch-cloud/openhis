using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.EMR.Domain.Entity
{
    /// <summary>
    /// 创 建：hyj
    /// 日 期：2018-09-07 15:46
    /// 描 述：词条管理
    /// </summary>
    [Table("bl_ctgl")]
    public class BlctglEntity : IEntity<BlctglEntity>
    {
        /// <summary>
        /// ID
        /// </summary>
        /// <returns></returns>
        [Key]
        public string ID { get; set; }
        /// <summary>
        /// OrganizeId
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }
        /// <summary>
        /// 0 通用 1个人 2科室
        /// </summary>
        /// <returns></returns>
        public int? qx { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        /// <returns></returns>
        public string mc { get; set; }

        /// <summary>
        /// ksbm
        /// </summary>
        /// <returns></returns>
        public string ksbm { get; set; }
        /// <summary>
        /// 词条内容
        /// </summary>
        /// <returns></returns>
        public string ctnr { get; set; }
        /// <summary>
        /// py
        /// </summary>
        /// <returns></returns>
        public string py { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>
        /// <returns></returns>
        /// 
        public string parentId { get; set; }
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
        /// zt
        /// </summary>
        /// <returns></returns>
        public string zt { get; set; }

    }
}