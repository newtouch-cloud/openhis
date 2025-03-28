using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.BusinessObjects
{
    /// <summary>
    /// 根据病人性质和诊断判断，报销费用的上限
    /// </summary>
    [Serializable]
    public class BrxzZdBxsxBO
    {
        /// <summary>
        /// 病人性质
        /// </summary>
        public string brxz { get; set; }

        /// <summary>
        /// icd10
        /// </summary>
        public string icd10 { get; set; }

        /// <summary>
        /// 报销上限
        /// </summary>
        public decimal bxsx { get; set; }
    }
}
