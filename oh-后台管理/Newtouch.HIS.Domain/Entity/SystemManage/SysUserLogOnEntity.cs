using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("Sys_UserLogOn")]
    public class SysUserLogOnEntity : IEntity<SysUserLogOnEntity>
    {
        /// <summary>
        /// 用户登录主键
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 用户主键
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string UserPassword { get; set; }

        /// <summary>
        /// 用户秘钥
        /// </summary>
        public string UserSecretkey { get; set; }

        /// <summary>
        /// 上一次访问时间
        /// </summary>
        public DateTime? PreviousVisitTime { get; set; }

        /// <summary>
        /// 最后访问时间
        /// </summary>
        public DateTime? LastVisitTime { get; set; }

        /// <summary>
        /// 登录次数
        /// </summary>
        public int? LogOnCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }

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
        public string zt { get; set; }

        /// <summary>
        /// 是否被锁定。true锁定，否则未锁定
        /// </summary>
        public bool? Locked { get; set; }

		/// <summary>
		/// 明文登录密码
		/// </summary>
		public string UserPwdPlaintext { get; set; }

	}
}
