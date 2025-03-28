using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity.SystemManage
{
    /// <summary>
    /// 系统诊室
    /// </summary>
    [Table("xt_zs")]
    public class SysConsultEntity : IEntity<SysConsultEntity>
    {
        [Key]
        public int zsId { get; set; }
        /// <summary>
        /// 诊室代码
        /// </summary>
        public string zsCode { get; set; }
        /// <summary>
        /// 诊室名称
        /// </summary>
        public string zsmc { get; set; }
        /// <summary>
        /// 科室编码
        /// </summary>
        public string ksCode { get; set; }
        /// <summary>
        /// 医生工号
        /// </summary>
        public string ys { get; set; }
        /// <summary>
        /// 组织机构Id
        /// </summary>
        public string OrganizeId { get; set; }
        /// <summary>
        /// 拼音
        /// </summary>
        public string py { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public int xh { get; set; }
        public string CreatorCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }
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
        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

    }
}
