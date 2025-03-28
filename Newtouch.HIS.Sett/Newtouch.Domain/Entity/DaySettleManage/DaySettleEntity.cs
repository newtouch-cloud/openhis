using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("xt_daysettle")]
    public class DaySettleEntity : IEntity<DaySettleEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 本次结算金额
        /// </summary>
        public decimal bcje { get; set; }

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
        public DateTime? LastJsTime { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string fphs { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string jsnms { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string fphBack { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal ybzfje { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal qtzfje { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string mzzybz { get; set; }
    }
}
