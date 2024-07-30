using Newtouch.HIS.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 
    /// </summary>
    [NotMapped]
    public class SysItemsDetailVO : SysItemsDetailEntity
    {
        /// <summary>
        /// 是否允许编辑
        /// </summary>
        public bool AllowEdit { get; set; }

    }

    public class ProjectZbVO {
        public int? id { get; set; } 
        public string kmmc { get; set; } 
        public string kmdm { get; set; } 
        public string sjkmdm { get; set; } 
        public int? xmlx { get; set; } 
        public int? gmlbz { get; set; } 
        public decimal? sl { get; set; } 
        public DateTime? CreateTime { get; set; } 
        public string CreatorCode { get; set; } 
        public DateTime? LastModifyTime { get; set; } 
        public string LastModierCode { get; set; } 
        public string Organizeld { get; set; } 
        public int? zt { get; set; } 
    }
    public class ProjectMxVO {
        public int? id { get; set; }
        public string kmdm { get; set; }
        public string xmdm { get; set; }
        public DateTime? CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModierCode { get; set; }
        public string Organizeld { get; set; }
        public int? zt { get; set; }
    }
    public class XmandYp
    {
        public int sfxmid { get; set; }
        public string sfxmcode { get; set; }
        public string sfxmmc { get; set; }
    }
}
