using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain.Entity.SysManage
{
    ///<summary>
    /// 系统岗位
    ///</summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("Sys_Duty", "系统岗位")]
    public partial class SysDutyEntity : ISysEntity
    {
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "Name不可为空")]
        [StringLength(50, ErrorMessage = "Name长度限制为50")]
        public string Name { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "Code不可为空")]
        [StringLength(20, ErrorMessage = "Code长度限制为20")]
        public string Code { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(500, ErrorMessage = "Description长度限制为500")]
        public string? Description { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? px { get; set; }
    }
}
