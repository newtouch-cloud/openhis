using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Herp.Domain.Entity.VEntity
{
    /// <summary>
    /// 出入库单据主信息
    /// </summary>
    public class V_fphEntity
    {
        /// <summary>
        /// 发票号
        /// </summary>
        public string fph { get; set; }
        /// <summary>
        /// 开票时间
        /// </summary>
        public string kprq { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string gysCode { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string gysmc { get; set; }

    }
}
