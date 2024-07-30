using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.Entity
{
    /// <summary>
    /// 系统病区
    /// </summary>
    public class xt_bq
    {
        /// <summary>
        ///病区ID
        /// </summary>
        public string bqCode { get; set; }
        /// <summary>
        /// 病区名称
        /// </summary>
        public string bqmc { get; set; }
    }
}
