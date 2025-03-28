using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage
{
    public class Sh_YbfyxzblVO : IEntity<Sh_YbfyxzblVO>
    {
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