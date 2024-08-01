using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworkBase.MultiOrg.Domain.Entity
{
    /// <summary>
    /// 系统用户
    /// </summary>
    [Table("V_C_Sys_User")]
    public class SysUserVEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 顶级组织机构
        /// </summary>
        public string TopOrganizeId { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 语言类型
        /// </summary>
        public string LanguageType { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string UserPassword { get; set; }

        /// <summary>
        /// 密码Key
        /// </summary>
        public string UserSecretkey { get; set; }

        /// <summary>
        /// 是否被锁定。true锁定，否则未锁定
        /// </summary>
        public bool? Locked { get; set; }

        /// <summary>
        /// 有效标志
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 微软邮箱
        /// </summary>
        public string MsEmail { get; set; }

    }
}
