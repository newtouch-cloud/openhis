using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.DTO.InputDto
{
    /// <summary>
    /// 费用一日清
    /// </summary>
    public class InpatientDayFeeDTO
    {
        /// <summary>
        /// 日期
        /// </summary>
        public string rq { get; set; }
        /// <summary>
        /// 大类名称
        /// </summary>
        public string dlmc { get; set; }
        /// <summary>
        /// 大类金额
        /// </summary>
        public decimal dlje { get; set; }
        /// <summary>
        /// 当日金额
        /// </summary>
        public decimal drje { get; set; }
        /// <summary>
        /// 总金额
        /// </summary>
        public decimal zje { get; set; }
    }

    public class InpatientDayFeeResp {
        public string zyh { get; set; }
        public decimal zje { get; set; }
        public List<InpatientDayFeeRQ> data { get; set; }
    }

    public class InpatientDayFeeRQ
    {
        /// <summary>
        /// 日期
        /// </summary>
        public string rq { get; set; }
      
        /// <summary>
        /// 当日金额
        /// </summary>
        public decimal drzje { get; set; }
        public List<InpatientDayFeeDL> data { get; set; }
    }

    public class InpatientDayFeeDL{
        /// <summary>
        /// 大类名称
        /// </summary>
        public string dlmc { get; set; }
        /// <summary>
        /// 大类金额
        /// </summary>
        public decimal dlje { get; set; }
    }
}
