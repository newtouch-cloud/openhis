using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;

namespace Newtouch.MR.ManageSystem.Domain.Entity
{
	public class itemEntity : IEntity<itemEntity>
	{
		public int sfxmId { get; set; }
		public string sfxmCode { get; set; }
		public string sfxmmc { get; set; }
		public string sfdlCode { get; set; }
		public string badlCode { get; set; }
		public string nbdlCode { get; set; }
		public string OrganizeId { get; set; }
		public string py { get; set; }
		public string dw { get; set; }
		public Decimal dj { get; set; }
		public Decimal zfbl { get; set; }
		public string zfxz { get; set; }
		public string mzzybz { get; set; }
		public string ssbz { get; set; }
		public string tsbz { get; set; }
		public string sfbz { get; set; }
		public string ybdm { get; set; }
		public string wjdm { get; set; }
		public string CreatorCode { get; set; }
		public DateTime? CreateTime { get; set; }
		public DateTime? LastModifyTime { get; set; }
		public string LastModifierCode { get; set; }
		public int? px { get; set; }
		public string zt { get; set; }
		public int? duration { get; set; }
		public string bz { get; set; }
		public int? dwjls { get; set; }
		public int? jjcl { get; set; }
		public string zxks { get; set; }
		public string gg { get; set; }
		public string sqlx { get; set; }
		public string ybbz { get; set; }
		public string xnhybdm { get; set; }
	}
}