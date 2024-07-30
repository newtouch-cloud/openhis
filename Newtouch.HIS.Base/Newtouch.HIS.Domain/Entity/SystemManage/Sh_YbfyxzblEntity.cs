using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.Entity.SystemManage
{
    /// <summary>
    /// 医保病人性质
    /// </summary>
    [Table("Sh_Ybfyxzbl")]
    public class Sh_YbfyxzblEntity :IEntity<Sh_YbfyxzblEntity>
    {
        [Key]
        public int Id { get; set; }
        public string OrganizeId { get; set; }
        public string xmcode { get; set; }
        public string xmmc { get; set; }
        public string xzId { get; set; }
        public string xzmc { get; set; }
        public decimal? zfbl { get; set; }
        public decimal? fyxe { get; set; }
        public decimal? cxbl { get; set; }
        public string CreatorCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }
    }
}
