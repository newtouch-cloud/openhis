using Newtouch.Core.Common;
using Newtouch.Domain.DTO.InputDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Inpatient
{
    public class InPatientQueryResponseVO
    {
        /// <summary>
        /// 
        /// </summary>
        public Pagination pagination { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<InPatientQueryDto> list { get; set; }
    }

    //public class InPatientDetailQueryResponseVO
    //{
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    //public Pagination pagination { get; set; }

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public InPatientDetailQueryDto InPatientDetailQueryDto { get; set; }
    //}
}
