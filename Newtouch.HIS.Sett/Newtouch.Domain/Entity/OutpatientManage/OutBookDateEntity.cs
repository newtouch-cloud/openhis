using Newtouch.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.Entity.OutpatientManage
{
    [Table("mz_ghpb_date")]
    public class OutBookDateEntity : IEntity<OutBookDateEntity>
    { /// <summary>
      /// 主键
      /// </summary>
      /// <returns></returns>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ghpbId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Weekdd { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Weekddname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Period { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int TotalNum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string IsBook { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }
        /// <summary>
        /// 
        /// </summary>
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
        public int px { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string timeslot { get; set; }
    }
}
