using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("V_C_Sys_User")]
    public class SysUserVEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TopOrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LanguageType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UserPassword { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UserSecretkey { get; set; }

        /// <summary>
        /// 是否被锁定。true锁定，否则未锁定
        /// </summary>
        public bool? Locked { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

    }
}
