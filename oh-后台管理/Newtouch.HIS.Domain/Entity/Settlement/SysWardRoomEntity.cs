using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 病房
    /// </summary>
    [Table("xt_bf")]
    public class SysWardRoomEntity : IEntity<SysWardRoomEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int bfId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string bfCode { get; set; }

        /// <summary>
        /// 房间号
        /// </summary>
        public string bfNo { get; set; }

        /// <summary>
        /// 病区
        /// </summary>
        public string bqCode { get; set; }

        /// <summary>
        /// 科室
        /// </summary>
        public string ksCode { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string bz { get; set; }

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
