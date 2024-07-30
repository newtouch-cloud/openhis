using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.BusinessObjects
{
    /// <summary>
    /// 
    /// </summary>
    public class VisitNumBO
    {
        /// <summary>
        /// 
        /// </summary>
        public List<OutpatientVisitNumVO> OutpatientList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<InpatientVisitNumVO> InpatientList { get; set; }

    }
}
