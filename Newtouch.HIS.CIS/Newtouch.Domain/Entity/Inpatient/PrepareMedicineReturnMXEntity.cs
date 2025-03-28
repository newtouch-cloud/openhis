using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.Entity.Inpatient
{
    [Table("zy_ksbythmx")]
    public class PrepareMedicineReturnMXEntity:IEntity<PrepareMedicineReturnMXEntity>
    {
        [Key]
        public string Id { get; set; }
        public string byId { get; set; }
        public string OrganizeId { get; set; }
        public string ypdm { get; set; }
        public string ypmc { get; set; }
        public string yplb { get; set; }
        public decimal tsl { get; set; }
        public string dw { get; set; }
        public string gg { get; set; }
        public DateTime? yxq { get; set; }
        //public string yfbm { get; set; }
        public string sccj { get; set; }
        //public string thyy { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string zt { get; set; }


        /// <summary>
        /// 修改用户
        /// </summary>
        public string CreatorCode { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime CreateTime { get; set; }


        /// <summary>
        /// 最后修改用户
        /// </summary>
        public string LastModifierCode { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
        public string ph { get; set; }
        public string pc { get; set; }
    }
}
