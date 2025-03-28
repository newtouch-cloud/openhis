using Newtouch.HIS.Domain.Entity.Settlement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects
{
    public class SysMedicineAuthorityRelationVO
    {
        /// <summary>
        /// 权限编码
        /// </summary>
        public string qxCode { get; set; }
        /// <summary>
        /// 权限名称
        /// </summary>
        public string qxmc { get; set; }
        /// <summary>
        /// 医生姓名
        /// </summary>
        public string name { get; set; }
        public string departmentCode { get; set; }
        public string departmentName { get; set; }
        public string id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string qxId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string gh { get; set; }

        public string ysxm { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }
        /// <summary>
        /// 
        /// </summary>
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
        public string memo { get; set; }
    }
}
