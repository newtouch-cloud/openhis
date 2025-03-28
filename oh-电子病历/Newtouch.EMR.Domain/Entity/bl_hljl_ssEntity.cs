using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Newtouch.EMR.Domain.Entity
{

    [Table("bl_hljl_ss")]
    public  class bl_hljl_ssEntity : IEntity<bl_hljl_ssEntity>
    {
        [Key]
        public string Id { get; set; }
        public string rq { get; set; }
        public string sj { get; set; }
        public string zyh { get; set; }
        public string xm { get; set; }
        public string hldj { get; set; }
        public string hllx { get; set; }
        public string gms { get; set; }
        public string tz { get; set; }
        public string tw { get; set; }
        public string hxpl { get; set; }
        public string ml { get; set; }
        public string ssy { get; set; }
        public string szy { get; set; }
        public string xybhd { get; set; }
        public string xy { get; set; }
        public string zbdmbz { get; set; }
        public string ysqk { get; set; }
        public string dghl { get; set; }
        public string qghl { get; set; }
        public string twhl { get; set; }
        public string pfhl { get; set; }
        public string yyhl { get; set; }
        public string yszd { get; set; }
        public string xlhl { get; set; }
        public string aqhl { get; set; }
        public string jybq { get; set; }
        public string hlgcxm { get; set; }
        public string hlgcjg { get; set; }
        public string hlczmc { get; set; }
        public string hlczxmmc { get; set; }
        public string hlczjg { get; set; }
        public string fcssaqb { get; set; }
        public string shssaqb { get; set; }
        public string fcssfxpgb { get; set; }
        public string shssfxpgb { get; set; }
        public string glbz { get; set; }
        public string glzl { get; set; }
        public string hsqm { get; set; }
        public string hsqmrqsj { get; set; }
        public string ys { get; set; }
        public string tk_z { get; set; }
        public string tk_y { get; set; }
        public string dgfs_z { get; set; }
        public string dgfs_y { get; set; }
        public System.DateTime CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public Nullable<System.DateTime> LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
        public string zt { get; set; }
        public string OrganizeId { get; set; }
    }
}
