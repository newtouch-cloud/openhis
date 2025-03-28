using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.Entity.HospitalizationPharmacy
{
    /// <summary>
    /// 科室备药退回
    /// </summary>
    [Table("xt_ksbyth")]
    public class XtksbythEntity : IEntity<XtksbythEntity>
    {

        [Key]
        public string Id { get; set; }
        public string OrganizeId { get; set; }
        public string djh { get; set; }
        public string thzt { get; set; }
        public string yfbm { get; set; }
        public string ksbm { get; set; }
        public string bqbm { get; set; }
        public DateTime? tjsj { get; set; }
        public string thyy { get; set; }
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
    }
}
