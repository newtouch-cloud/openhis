using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain
{
    /// <summary>
    /// 系统用户
    /// </summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("V_C_Sys_User")]
    [Description("系统用户视图")]
    public class SysUserVEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        [Required, MaxLength(50)]
        public string Id { get; set; }

        /// <summary>
        /// 顶级组织机构
        /// </summary>
        [Required, MaxLength(50)]
        public string TopOrganizeId { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        [Required, MaxLength(20)]
        public string Account { get; set; }

        /// <summary>
        /// 语言类型
        /// </summary>
        [Required, MaxLength(50)]
        public string? LanguageType { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required, MaxLength(50)]
        public string UserPassword { get; set; }

        /// <summary>
        /// 密码Key
        /// </summary>
        [Required, MaxLength(50)]
        public string UserSecretkey { get; set; }

        /// <summary>
        /// 是否被锁定。true锁定，否则未锁定
        /// </summary>
        [MaxLength(1)]
        public bool? Locked { get; set; }

        /// <summary>
        /// 有效标志
        /// </summary>
        [Required, MaxLength(1)]
        public string zt { get; set; }

    }
}
