using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.EMR.Domain.Entity
{
    /// <summary>
    /// 创 建：chl
    /// 日 期：2020-03-13 11:29
    /// 描 述：病案首页-诊断记录
    /// </summary>
    [Table("mr_basy_zd")]
    public class MrbasyzdEntity : IEntity<MrbasyzdEntity>
    {
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// BAH
        /// </summary>
        /// <returns></returns>
        public string BAH { get; set; }

        /// <summary>
        /// ZYH
        /// </summary>
        /// <returns></returns>
        public string ZYH { get; set; }
        /// <summary>
        /// WM  TCM
        /// </summary>
        public string ZDLB { get; set; }
        /// <summary>
        /// ZDLX 1主要诊断 2其他诊断 3 中医主病 4 中医主证
        /// </summary>
        /// <returns></returns>
        public string ZDLX { get; set; }
        /// <summary>
        /// ZDOrder
        /// </summary>
        /// <returns></returns>
        public int ZDOrder { get; set; }

        /// <summary>
        /// JBDM
        /// </summary>
        /// <returns></returns>
        public string JBDM { get; set; }

        /// <summary>
        /// JBMC
        /// </summary>
        /// <returns></returns>
        public string JBMC { get; set; }

        /// <summary>
        /// RYBQ
        /// </summary>
        /// <returns></returns>
        public string RYBQ { get; set; }

        /// <summary>
        /// RYBQMS
        /// </summary>
        /// <returns></returns>
        public string RYBQMS { get; set; }

        /// <summary>
        /// CYQK
        /// </summary>
        /// <returns></returns>
        public string CYQK { get; set; }

        /// <summary>
        /// CYQKMS
        /// </summary>
        /// <returns></returns>
        public string CYQKMS { get; set; }

        /// <summary>
        /// zt
        /// </summary>
        /// <returns></returns>
        public string zt { get; set; }

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
        /// OrganizeId
        /// </summary>
        /// <returns></returns>
        public string OrganizeId { get; set; }

    }
}