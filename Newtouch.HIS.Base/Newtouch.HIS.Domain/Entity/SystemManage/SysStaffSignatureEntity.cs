using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 医生电子签名
    /// </summary>
    [Table("Sys_StaffSignature")]
    public class SysStaffSignatureEntity : IEntity<SysStaffSignatureEntity>
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// 组织机构Id（科室所对应的OrganizeId）
        /// </summary>
        public string OrganizeId { get; set; }
        /// <summary>
        /// 职工ID
        /// </summary>
        public string StaffId { get; set; }

        /// <summary>
        /// 图片名称
        /// </summary>
        public string ImageName { get; set; }
        /// <summary>
        /// 类型(jpg,pgn)
        /// </summary>
        public string ImageTpye { get; set; }
        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// base64编码
        /// </summary>
        public string ImagePrefix { get; set; }
        /// <summary>
        /// 图片二进制数据
        /// </summary>
        public string ImageData { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建用户
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 最后修改用户
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }
    }
}
