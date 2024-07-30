using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.Entity.V
{
    /// <summary>
    /// 申领单主信息
    /// </summary>
    public class ApplyMainVEntity
    {

        /// <summary>
        /// 申领单ID
        /// </summary>
        public string sldId { get; set; }

        /// <summary>
        /// 申领单号
        /// </summary>
        public string Sldh { get; set; }

        /// <summary>
        /// 发放状态
        /// </summary>
        public int ffzt { get; set; }

        /// <summary>
        /// 申领部门名称
        /// </summary>
        public string slbmmc { get; set; }

        /// <summary>
        /// 出库部门名称
        /// </summary>
        public string ckbmmc { get; set; }

        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 申请人
        /// </summary>
        public string CreatorCode { get; set; }
        
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
    }
}
