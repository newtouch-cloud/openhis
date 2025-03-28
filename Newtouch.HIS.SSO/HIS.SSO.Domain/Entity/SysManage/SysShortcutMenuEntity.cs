using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.SSO.Domain.Entity.SysManage
{
	///<summary>
	///系统模块
	///</summary>
	[Tenant(DBEnum.UnionDb)]
	[SugarTable("Sys_ShortcutMenu", "系统快捷菜单")]
	public partial class SysShortcutMenuEntity : IEntity
	{
		/// <summary>
		/// Desc:模块主键
		/// Default:
		/// Nullable:False
		/// </summary>           
		[SugarColumn(IsPrimaryKey = true)]
		public string Id { get; set; }
		/// <summary>
		/// Desc: 对应系统
		/// Default:
		/// Nullable:False
		/// </summary>           
		[StringLength(10, ErrorMessage = "AppId长度限制为10")]
		public string AppId { get; set; }

		/// <summary>
		/// Desc:快捷菜单名称
		/// Default:
		/// Nullable:False
		/// </summary>           
		[Required(ErrorMessage = "MenuName不可为空")]
		[StringLength(50, ErrorMessage = "MenuName长度限制为30")]
		public string MenuName { get; set; }

		/// <summary>
		/// Desc:菜单地址url
		/// Default:
		/// Nullable:False
		/// </summary>           
		[Required(ErrorMessage = "MenuUrl不可为空")]
		[StringLength(50, ErrorMessage = "MenuUrl长度限制为200")]
		public string MenuUrl { get; set; }

		/// <summary>
		/// Desc:组织机构
		/// Default:
		/// Nullable:False
		/// </summary>           
		//[Required(ErrorMessage = "OrganizeId不可为空")]
		//[StringLength(50, ErrorMessage = "OrganizeId长度限制为50")]
		//public string OrganizeId { get; set; }
		///// <summary>
		/// Desc:创建人
		/// Default:
		/// Nullable:False
		/// </summary>           
		//[Required(ErrorMessage = "CreatorCode不可为空")]
		//[StringLength(20, ErrorMessage = "CreatorCode长度限制为20")]
		//public string CreatorCode { get; set; }

		/// <summary>
		/// Desc:创建时间
		/// Default:
		/// Nullable:False
		/// </summary>           
		//[Required(ErrorMessage = "Createtime不可为空")]
		//public DateTime Createtime { get; set; }

		/// <summary>
		/// Desc:修改时间
		/// Default:
		/// Nullable:True
		/// </summary>           
		//public DateTime? LastModifyTime { get; set; }

		/// <summary>
		/// Desc:修改人
		/// Default:
		/// Nullable:True
		/// </summary>           
		//[StringLength(20, ErrorMessage = "LastModifierCode长度限制为500")]
		//public string? LastModifierCode { get; set; }

		/// <summary>
		/// Desc:排序
		/// Default:
		/// Nullable:True
		/// </summary>           
		public int? px { get; set; }

		/// <summary>
		/// Desc:状态
		/// Default:
		/// Nullable:False
		/// </summary>           
		//[StringLength(1, ErrorMessage = "zt长度限制为1")]
		//public string zt { get; set; }
	}
}
