using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.Entity.Settlement
{
    public class GetSfDlMc
    {
        public string Id { get; set; }

        /// <summary>
        /// 医疗机构
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 类型 关联字典ChargeCateType的字典项
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 收费大类编码
        /// </summary>
        public string dlCode { get; set; }

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

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

        public string dlmc { get; set; }
    }
}
