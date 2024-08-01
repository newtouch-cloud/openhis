using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworkBase.Domain.Entity
{
    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2017-11-17 16:15
    /// 描 述：用户登录信息表
    /// </summary>
    [Table("Sys_UserLogOn")]
    public class SysUserLogOnEntity : IEntity<SysUserLogOnEntity>
    {
        /// <summary>
        /// 用户登录主键
        /// </summary>
        /// <returns></returns>
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// 用户主键
        /// </summary>
        /// <returns></returns>
        public string UserId { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        /// <returns></returns>
        public string UserPassword { get; set; }
        /// <summary>
        /// 用户秘钥
        /// </summary>
        /// <returns></returns>
        public string UserSecretkey { get; set; }
        /// <summary>
        /// 上一次访问时间
        /// </summary>
        /// <returns></returns>
        public DateTime? PreviousVisitTime { get; set; }
        /// <summary>
        /// 最后访问时间
        /// </summary>
        /// <returns></returns>
        public DateTime? LastVisitTime { get; set; }
        /// <summary>
        /// 登录次数
        /// </summary>
        /// <returns></returns>
        public int? LogOnCount { get; set; }
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
        public string zt { get; set; }
        /// <summary>
        /// Locked
        /// </summary>
        /// <returns></returns>
        public bool? Locked { get; set; }
    }
}