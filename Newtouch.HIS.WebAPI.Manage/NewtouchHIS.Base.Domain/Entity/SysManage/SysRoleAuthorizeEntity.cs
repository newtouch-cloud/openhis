using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain.Entity
{
    ///<summary>
    ///角色授权表
    ///</summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("Sys_RoleAuthorize", "角色菜单权限")]
    public partial class SysRoleAuthorizeEntity:ISysEntity
    {
        /// <summary>
        /// Desc:角色授权主键
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }

        /// <summary>
        /// Desc:对象主键
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "RoleId不可为空")]
        [StringLength(50, ErrorMessage = "RoleId长度限制为50")]
        public string RoleId { get; set; }

        /// <summary>
        /// Desc:项目主键
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "ItemId不可为空")]
        [StringLength(50, ErrorMessage = "ItemId长度限制为50")]
        public string ItemId { get; set; }

        /// <summary>
        /// Desc:排序码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? px { get; set; }

       
    }
}
