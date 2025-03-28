using Newtouch.HIS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.DTO.OutputDto
{
    /// <summary>
    /// 
    /// </summary>
    public class InpatientHosAccountingPatStatusDetailDto
    {
        /// <summary>
        /// 住院病人基本信息
        /// </summary>
        public HospPatientInfoVO patInfo { get; set; }

    }
}
