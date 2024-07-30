using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;
namespace Newtouch.EMR.Domain.Entity
{
    /// <summary>
    /// 创 建：mhz
    /// 日 期：2023-04-17 11:24
    /// 描 述：元素
    /// </summary>
    [Table("bl_ysmx")]
    public class BlysMXEntity : IEntity<BlysMXEntity>
    {
        public int Id { get; set; }
        public string OrganizeId { get; set; }
        public string YsId { get; set; }
        public string YsmxCode { get; set; }
        public string YsmxName { get; set; }
        public int Px { get; set; }
        public string Py { get; set; }
        public int Sybz { get; set; }
        public string Remark { get; set; }
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
        /// zt
        /// </summary>
        /// <returns></returns>
        public int Zt { get; set; }
        

        
    }
}