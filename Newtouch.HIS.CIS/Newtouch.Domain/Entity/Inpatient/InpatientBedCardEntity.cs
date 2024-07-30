using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.Domain.Entity.Inpatient
{
	[Table("zy_bedCard")]
	public class InpatientBedCardEntity : IEntity<InpatientBedCardEntity>
	{
		/// <summary>
		/// id
		/// </summary>
		/// <returns></returns>
		[Key]
		public string Id { get; set; }
		/// <summary>
		/// zyh
		/// </summary>
		/// <returns></returns>
		public string zyh { get; set; }
		/// <summary>
		/// zrhs
		/// </summary>
		/// <returns></returns>
		public string zrhs { get; set; }
		/// <summary>
		/// zrzz
		/// </summary>
		/// <returns></returns>
		public string zrzz { get; set; }
		/// <summary>
		/// gms
		/// </summary>
		/// <returns></returns>
		public string gms { get; set; }
		/// <summary>
		/// fbsx
		/// </summary>
		/// <returns></returns>
		public string fbsx { get; set; }
		/// <summary>
		/// sjys
		/// </summary>
		/// <returns></returns>
		public string sjys { get; set; }
		/// <summary>
		/// hsz
		/// </summary>
		/// <returns></returns>
		public string hsz { get; set; }
		/// <summary>
		/// kzr
		/// </summary>
		/// <returns></returns>
		public string kzr { get; set; }
		/// <summary>
		/// xh
		/// </summary>
		/// <returns></returns>
		public string xh { get; set; }
		/// <summary>
		/// jb
		/// </summary>
		/// <returns></returns>
		public string jb { get; set; }
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
		/// OrganizeId
		/// </summary>
		/// <returns></returns>
		public string OrganizeId { get; set; }
	}
}
