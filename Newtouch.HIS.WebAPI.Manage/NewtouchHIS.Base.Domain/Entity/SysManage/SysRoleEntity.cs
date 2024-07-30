using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain.Entity.SysManage
{
    ///<summary>
    ///角色表
    ///</summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("Sys_Role", "系统角色")]
    public partial class SysRoleEntity : IEntity
    {
        /// <summary>
        /// Desc:角色主键
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }


        /// <summary>
        /// Desc:名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "Name不可为空")]
        [StringLength(50, ErrorMessage = "Name长度限制为50")]
        public string Name { get; set; }

        /// <summary>
        /// Desc:编号
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "Code长度限制为20")]
        public string? Code { get; set; }

        /// <summary>
        /// Desc:描述
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(500, ErrorMessage = "Description长度限制为500")]
        public string? Description { get; set; }

        /// <summary>
        /// Desc:排序码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? px { get; set; }

       
    }
}
