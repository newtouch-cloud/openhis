using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 智能审核日志
    /// </summary>
    [Table("qhd_znshsqtx")]
    public class QhdZnshSqtxEntity : IEntity<QhdZnshSqtxEntity>
    {
        [Key]
        public string Id { get; set; }
        public string OrganizeId { get; set; }
        /// <summary>
        /// 交易代码
        /// </summary>
        public string jydm { get; set; }
        /// <summary>
        /// 门诊住院号
        /// </summary>
        public string mzzyh { get; set; }
        /// <summary>
        /// 1：门诊 2：住院
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 入参xml
        /// </summary>
        public string XmlRequest { get; set; }
        /// <summary>
        /// 出参xml
        /// </summary>
        public string XmlResponse { get; set; }
        public string zt { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreatorCode { get; set; }
        public DateTime? LastModifyTime { get; set; }
        public string LastModifierCode { get; set; }
    }
}
