using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 系统人员
    /// </summary>
    [Table("xt_ry")]
    [Obsolete("please use the view")]
    public class SysStaffEntity : IEntity<SysStaffEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int rybh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public short ry { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string rymc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string yhm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string mm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string gh { get; set; }

        /// <summary>
        /// 0 非医生 1 实习医生 2 轮转医生    3 住院医生 4 主治医生 5 副主任医生 6 主任医生
        /// </summary>
        public string ysbz { get; set; }

        /// <summary>
        /// 0 不可操作系统 1 可操作系统
        /// </summary>
        public string czbz { get; set; }

        /// <summary>
        /// 1 有最大权限 0 无最大权限
        /// </summary>
        public string zdqxbz { get; set; }

        /// <summary>
        /// 0 不可用 1 可用
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string bz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zp { get; set; }

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

        /// <summary>
        /// 
        /// </summary>
        public int ryCode { get; set; }

    }
}
