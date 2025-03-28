using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Base.Domain.Entity.SysManage
{
    ///<summary>
    ///明细字典表
    ///</summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("Sys_ItemsDetail", "明细字典表")]
    public partial class SysItemsDetailEntity : IEntity
    {
        /// <summary>
        /// Desc:明细主键
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
        [Required(ErrorMessage = "TopOrganizeId不可为空")]
        [StringLength(50, ErrorMessage = "TopOrganizeId长度限制为50")]
        public string TopOrganizeId { get; set; }

        /// <summary>
        /// Desc:主表主键
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "ItemId不可为空")]
        [StringLength(50, ErrorMessage = "ItemId长度限制为50")]
        public string ItemId { get; set; }

        /// <summary>
        /// Desc:父级
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "ParentId长度限制为50")]
        public string? ParentId { get; set; }

        /// <summary>
        /// Desc:名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "Name不可为空")]
        [StringLength(500, ErrorMessage = "Name长度限制为500")]
        public string Name { get; set; }

        /// <summary>
        /// Desc:编码
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "Code不可为空")]
        [StringLength(20, ErrorMessage = "Code长度限制为20")]
        public string Code { get; set; }

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
