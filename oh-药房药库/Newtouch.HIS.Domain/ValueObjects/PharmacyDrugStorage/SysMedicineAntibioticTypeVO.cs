using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage
{
    public class SysMedicineAntibioticTypeVO
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 类别名称
        /// </summary>
        public string typeName { get; set; }

        /// <summary>
        /// 类别等级
        /// </summary>
        public string typelevel { get; set; }

        /// <summary>
        /// 权限级别(0 非限制使用药物,1 限制使用药物,2 特殊使用药物)
        /// </summary>
        public string qxjb { get; set; }

        /// <summary>
        /// 权限级别名称
        /// </summary>
        public string qxjbmc { get; set; }

        /// <summary>
        /// 上级类别Id
        /// </summary>
        public string parentId { get; set; }

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
        /// 1：有效 0：无效
        /// </summary>
        public string zt { get; set; }
    }
}
