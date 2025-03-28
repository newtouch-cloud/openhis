using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace HIS.SSO.Domain.Entity
{
    ///<summary>
    ///系统模块
    ///</summary>
    [Tenant(DBEnum.UnionDb)]
    [SugarTable("Sys_Module", "系统模块")]
    public partial class SysModuleEntity : IEntity
    {
        /// <summary>
        /// Desc:模块主键
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }

        /// <summary>
        /// Desc:父级
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "ParentId长度限制为50")]
        public string ParentId { get; set; }

        /// <summary>
        /// Desc:名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "Name不可为空")]
        [StringLength(50, ErrorMessage = "Name长度限制为50")]
        public string Name { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "EnName长度限制为50")]
        public string? EnName { get; set; }

        /// <summary>
        /// Desc:编码
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "Code长度限制为20")]
        public string? Code { get; set; }

        /// <summary>
        /// Desc:图标
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "Icon长度限制为50")]
        public string? Icon { get; set; }

        /// <summary>
        /// Desc:连接
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(500, ErrorMessage = "UrlAddress长度限制为500")]
        public string? UrlAddress { get; set; }

        /// <summary>
        /// Desc:目标
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "Target长度限制为50")]
        public string? Target { get; set; }

        /// <summary>
        /// Desc:排序码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? px { get; set; }

        /// <summary>
        /// Desc:描述
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
        [StringLength(50, ErrorMessage = "AppId长度限制为50")]
        public string? AppId { get; set; }

    }
}
