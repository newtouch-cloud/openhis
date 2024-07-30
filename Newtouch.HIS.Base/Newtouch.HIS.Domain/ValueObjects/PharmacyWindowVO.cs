using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 
    /// </summary>
    public class PharmacyWindowVO
    {
        /// <summary>
        /// 药房窗口Id
        /// </summary>
        public int yfckId { get; set; }

        /// <summary>
        /// 药房窗口代码
        /// </summary>
        public string yfckCode { get; set; }

        /// <summary>
        /// 药房窗口名称
        /// </summary>
        public string yfckmc { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? px { get; set; }

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
        /// 顶级组织机构Id
        /// </summary>
        public string TopOrganizeId { get; set; }
    }
}
